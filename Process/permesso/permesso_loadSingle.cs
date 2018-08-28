using System;



namespace Process.permesso
{


    public static class permesso_loadSingle
    {



        /// <summary>
        /// requires valorization of Permessi::username.
        /// returns Entity.BusinessEntities.Permesso.Patente, which is hold in Session, to characterize the user, who had a successful login.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Entity.BusinessEntities.Permesso.Patente</returns>
        public static Entity.BusinessEntities.Permesso.Patente GetPatente(
            string username
            )
        {
            Entity.BusinessEntities.Permesso permesso =
                new Entity.BusinessEntities.Permesso(username);
            //
            Entity.BusinessEntities.Permesso.Patente result = permesso.GetPatente();
            // NB. returns null on error.
            return result;
        }//


    }// end class


}// end nmsp
