using System;
using Entity.Proxies;


namespace Entity.BusinessEntities
{


    public class Permesso
    {

        public struct EmployeeLevel
        {
            public int id_permissionLevel;
            public int permissionLevel;
            public string nomePermesso;
        }


        public struct SettorePartecipato
        {
            public int id_settore;
            public string nomeSettore;
        };


        /// <summary>
        /// ad hoc class, for the return value of GetPatente.
        /// It will be held in Session; so better a nested class, without method pointers
        /// that are wasted memory in Session.
        /// </summary>
        public class Patente
        {
            public int id_username;
            public string username;
            //
            public EmployeeLevel[] employeeLevel;
            //
            public SettorePartecipato[] settorePartecipato;
        }//
        //
        // an instance of the nested class.
        Patente patente = null;



        #region Ctors
        //


        public Permesso()// an empty ctor is needed for the LOADMULTI.
        { }



        public Permesso(
            string username
            )
        {
            this.patente = new Patente();
            this.patente.username = username;
        }

        public Permesso(
            int id_utente,
            string username,
            int id_permissionLevel
            )
        {
            this.patente = new Patente();
            this.patente.id_username = id_utente;
            this.patente.username = username;
            //
            this.patente.employeeLevel = new EmployeeLevel[1];// that's a scalar constructor. TODO write one for multiple permission configurations.
            this.patente.employeeLevel[0].id_permissionLevel = id_permissionLevel;
        }

        public Permesso(
            int id_utente,
            string username,
            int id_permissionLevel,
            int id_settore
            )
        {
            this.patente = new Patente();
            this.patente.id_username = id_utente;
            this.patente.username = username;
            //
            this.patente.employeeLevel = new EmployeeLevel[1];// that's a scalar constructor. TODO write one for multiple permission configurations.
            this.patente.employeeLevel[0].id_permissionLevel = id_permissionLevel;
            //
            this.patente.settorePartecipato = new SettorePartecipato[1];// that's a scalar constructor. TODO write one for multiple permission configurations.
            this.patente.settorePartecipato[0].id_settore = id_settore;
        }
        //
        #endregion Ctors




        /// <summary>
        /// requires valorization of this.username.
        /// // returns {int_theLevel, string_AreaAziendale}, filling the members.
        /// </summary>
        public Patente GetPatente()
        {
            Patente result = new Patente();// a brand new copy of "Patente" will be returned.
            //
            System.Data.DataSet ds =
                Entity.Proxies.usp_permesso_LOADSINGLE_SERVICE.usp_permesso_LOADSINGLE(
                    this.patente.username
                );
            // all in try-catch; nullity of anything is trapped this way.
            try //---table 1
            {
                result.id_username = (Int32)ds.Tables[0].Rows[0].ItemArray[0];
                result.username = (string)ds.Tables[0].Rows[0].ItemArray[1];
            }
            catch (System.Exception)
            {
                result.id_username = 0;
                result.username = "utente not found";
            }
            //
            //
            try //---table 2: permesso
            {
                // livello
                int cardinalita_Livelli = ds.Tables[1].Rows.Count;// table livelli
                result.employeeLevel = new EmployeeLevel[cardinalita_Livelli];
                //
                for (int c = 0; c < cardinalita_Livelli; c++)
                {
                    result.employeeLevel[c].id_permissionLevel = (Int32)ds.Tables[1].Rows[c].ItemArray[0];
                    result.employeeLevel[c].permissionLevel = (Int32)ds.Tables[1].Rows[c].ItemArray[1];// permissionLevel numerica
                    result.employeeLevel[c].nomePermesso = (string)ds.Tables[1].Rows[c].ItemArray[2];// permissionLevel nominale
                }
            }
            catch (System.Exception)
            {
                // livello
                result.employeeLevel = null;
            }
            //
            try //---table 3: settore
            {
                // settore
                int cardinalita_SettoriPartecipati = ds.Tables[2].Rows.Count;// table Settori Partecipati
                result.settorePartecipato = new SettorePartecipato[cardinalita_SettoriPartecipati];
                //
                for (int c = 0; c < cardinalita_SettoriPartecipati; c++)
                {
                    result.settorePartecipato[c].id_settore = (Int32)ds.Tables[2].Rows[c].ItemArray[0];
                    result.settorePartecipato[c].nomeSettore = (string)ds.Tables[2].Rows[c].ItemArray[1];
                }
            }
            catch (System.Exception)
            {
                // settore
                result.settorePartecipato = null;
            }
            // ready
            return result;
        }//


        /// <summary>
        ///  returns employees, who still need attributes, to be given them from the gestione personale page.
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetProspettoPermessi()
        {
            return
                Entity.Proxies.usp_permesso_GetNonCensiti_SERVICE.usp_permesso_GetNonCensiti(
                    null // trx
                );
        }


        /// <summary> TODO
        /// requires valorization of all of the fields.
        /// </summary>
        /// <returns></returns>
        public bool Insert()
        {
            bool result = false;
            int proxy_res =
                Entity.Proxies.usp_permesso_INSERT_SERVICE.usp_permesso_INSERT(
                // id_username is always scalar
                    this.patente.id_username,
                // the following two fields can be vectorial
                    this.patente.employeeLevel[0].id_permissionLevel,
                    this.patente.settorePartecipato[0].id_settore,// on id<=0 inserts DbNull, which is allowed.
                    null // trx
            );
            if (0 == proxy_res)
                result = true;
            return result;
        }


    }// end class


}// end nmsp
