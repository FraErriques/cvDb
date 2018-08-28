using System;


namespace Process.utente
{


    /// <summary>
    /// utente_changePwd (i.e. utente_update )
    /// </summary>
    public static class utente_changePwd
    {


        public static bool CambioPassword(
            string username,
            string old_password,
            string new_password,
            string confirm_new_password
            )
        {
            if (new_password != confirm_new_password)
            {// confirmation check in BPL.
                return false;// TODO dettagliare
            }// else go on
            Entity.BusinessEntities.Utente utente =
                new Entity.BusinessEntities.Utente();
            utente.username = username;
            //utente.password will be filled inside the Entity by a load_SINGLE -> check with old_password.
            bool result =
                utente.ChangePwd(
                    old_password,
                    new_password// used to chek, even if the page is in ~/zonaRiservata, i.e. user logged.
                );// to overwrite the existing one.
            // ready
            return result;
        }// end CambioPassword()


    }// end class


}// end nmsp
