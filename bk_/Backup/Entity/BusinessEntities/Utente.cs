using System;
using System.Text;
using Common.CryptoStore.Callers;
using Common.CryptoStore;
using Common.CryptoStore.Macro;
using Entity.Proxies;


namespace Entity.BusinessEntities
{


    public class Utente
    {
        public int id;
        public string username;
        public string password;
        public string kkey;
        public char mode;//{'s', 'm', 'o'}



        public bool Insert()
        {
            bool result = false;
            char mode;
            CryptoEngine.theReturnType combinazioneCriptata =
                Common.CryptoStore.Callers.Criptazione.CriptazioneSequenza(
                this.password,
                out mode
            );
            //		-insert service: gets{ web_username, web_pwd, kkey}, returns a status int.
            int insertionStatus =
                usp_utente_INSERT_SERVICE.usp_utente_INSERT(
                this.username,
                combinazioneCriptata.cryptedSequence,
                (string)(combinazioneCriptata.kkey),
                new string(mode, 1)
            );
            //		-return a status bool, based on the service status int.
            if (0 == insertionStatus)
                result = true;
            else
                result = false;
            // ready
            return result;
        }


        public bool Update()
        {
            return false;
        }

        public bool Delete()
        {
            return false;
        }

        private void resetMembers()
        {
            this.username = null;
            this.password = null;
            this.kkey = null;
            this.mode = 'o';// off
        }//


        /// <summary>
        /// NB. wrong usr is a blocking error. Nothing can be said
        ///		about the password, if the usr is wrong, since no
        ///		rows comes out from the query.
        /// </summary>
        /// <param name="trx"></param>
        /// <returns>
        ///		0 ==  ok.
        ///		1 ==  no db connection.
        ///		2 ==  no such row; i.e. wrong usr.
        ///		3 ==  unexpected data irregularity.
        /// </returns>
        public int LoadSingleRow(System.Data.SqlClient.SqlTransaction trx)
        {
            int result = -1;// init to invalid.
            //		-query service: gets{ usr}, returns{ pwd, kkey}.
            System.Data.DataTable rigaUtente =
                usp_utente_LOADSINGLE_SERVICE.usp_utente_LOADSINGLE(
                    this.username
                );
            if (null == rigaUtente)
            {// no such user.
                this.resetMembers();
                return 1; // ==  no db connection
            }// no such user.
            if (0 == rigaUtente.Rows.Count)
            {
                return 2; // ==  no such row; i.e. wrong usr.
            }
            // else continue
            try
            {
                this.id = (int)(rigaUtente.Rows[0]["id"]);
                this.password = (string)(rigaUtente.Rows[0]["password"]);
                this.kkey = (string)(rigaUtente.Rows[0]["kkey"]);
                this.mode = ((string)(rigaUtente.Rows[0]["mode"]))[0];
                result = 0;// ok
            }
            catch (System.Exception ex)
            {
                string s = ex.Message;// dbg
                this.resetMembers();
                result = 3;// unexpected data irregularity.
            }// no such user.
            // ready
            return result;
        }//



        /// <summary>
        /// Summary description for Login:
        ///		compares a couple{ web_username, web_pwd} with a db-row{ username, password, kkey}.
        ///		
        /// steps:
        ///		-query service: gets{ usr}, returns{ pwd, kkey}.
        ///		-call common::component: gets{ pwd, kkey}, returns{ decripted_pwd}.
        ///		-compare{ web_pwd, decripted_pwd} -> return bool.
        ///		
        /// NB. wrong usr is a blocking error. Nothing can be said
        ///		about the password, if the usr is wrong, since no
        ///		rows comes out from the query.
        /// </summary>
        /// <param name="web_username"></param>
        /// <param name="web_pwd"></param>
        /// <returns>
        ///		0 ==  ok.
        ///		1 ==  no db connection.
        ///		2 ==  no such row; i.e. wrong usr.
        ///		3 ==  unexpected data irregularity.
        ///		4 ==  good username, wrong password.
        /// </returns>
        public int canLogOn(
            // web_usr was set in the Entity-member and used by LoadSingleRow.
            string web_pwd,			// password filled on the web-form.
            System.Data.SqlClient.SqlTransaction trx
            )
        {
            int usrQueryResult =
                this.LoadSingleRow(trx);// sets pwd,kky, mode.
            if (0 == usrQueryResult)
            {
                //		-call common::component: gets{ pwd, kkey, mode}, returns{ decripted_pwd}.
                string passwordInChiaro =
                    Decriptazione.DecriptazioneSequenza(
                    this.password,
                    this.kkey,
                    this.mode
                    );
                //		-compare{ web_pwd, decripted_pwd} -> return bool.
                if (web_pwd == passwordInChiaro)
                    usrQueryResult = 0;// login ok.
                else
                    usrQueryResult = 4;//==good username, wrong password.
            }// else result already contains the answer.
            // ready
            return usrQueryResult;
        }//



        /// <summary>
        /// the Entity instance requires this.username and this.password to be initialized, in order for this method to work.
        /// the correctness of (usr, pwd) is checked before the substitution, even if the caller page is in zonaRiservata. So this Entity
        ///     is suitable even to be called from pages in free-zone.
        /// coherence between new_pwd and confirm_new_pwd is in BPL.
        /// web_new_pwd will overwrite the existing one, providing a new couple (kkey, mode).
        /// </summary>
        /// <param name="web_new_pwd"></param>
        /// <returns></returns>
        public bool ChangePwd(
            // web_usr was set in the Entity-member and used by LoadSingleRow.
            string old_password,
            string web_new_pwd		// new password filled on the web-form. Verified in BPL that it's well confirmed.
            // System.Data.SqlClient.SqlTransaction trx COMPULSORY
          )
        {
            bool result = false;
            //
            //-------START---------------- blocchi open-close transazione -------------
            //---transazione-----
            System.Data.SqlClient.SqlTransaction trx = null;
            System.Data.SqlClient.SqlConnection conn =
                DbLayer.ConnectionManager.connectWithCustomSingleXpath(
                    "ProxyGeneratorConnections/strings",// compulsory xpath
                    "cv_db_app"
                );
            if (null != conn)
                trx = conn.BeginTransaction();
            else
                return false;// no db connection.
            //--END--open transazione-----
            //
            int usrQueryResult =
                this.LoadSingleRow(trx);// get user row: usr,pwd,kky,mode.
            string old_pwd_chiaro =
                Common.CryptoStore.Callers.Decriptazione.DecriptazioneSequenza(
                    this.password,
                    this.kkey,
                    this.mode
                    );
            if (old_password != old_pwd_chiaro)
            {
                usrQueryResult = -1;// set error
            }// else continue
            if (0 == usrQueryResult)
            {
                char mode;
                Common.CryptoStore.Macro.CryptoEngine.theReturnType newPwdCrypted =
                    Common.CryptoStore.Callers.Criptazione.CriptazioneSequenza(
                        web_new_pwd,
                        out mode
                        );
                int updatePwdResult =
                    Proxies.usp_utente_ChangePwd_SERVICE.usp_utente_ChangePwd(
                        this.username,
                        newPwdCrypted.cryptedSequence,
                        (string)(newPwdCrypted.kkey),
                        new string(mode, 1),
                        trx
                        );
                if (0 == updatePwdResult)// success
                {
                    trx.Commit();
                    result = true;
                }
                else// failure
                {
                    trx.Rollback();
                    result = false;
                }
            }// else
            else
            {
                trx.Rollback();
                result = false;
            }
            if (null != trx.Connection)
                if (System.Data.ConnectionState.Open == trx.Connection.State)
                    trx.Connection.Close();
            //--END--close transazione------
            // ready
            return result;
        }// ChangePwd






        public System.Data.DataTable LoadMultipleRows(System.Data.SqlClient.SqlTransaction trx)
        {
            return null;
        }//


    }// end class


}// end nmsp
