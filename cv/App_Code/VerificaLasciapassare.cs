using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



/// <summary>
/// VerificaLasciapassare checks if login happened.
/// </summary>
public static class VerificaLasciapassare
{


    /// <summary>
    /// VerificaLasciapassare checks if login happened.
    /// </summary>
    /// <param name="Session"></param>
    /// <returns></returns>
    public static bool CanLogOn(
        System.Web.SessionState.HttpSessionState Session,
        string UserHostAddress
        )
    {
        bool result = false;
        object lasciapassare = Session["lasciapassare"];
        Entity.BusinessEntities.Permesso.Patente actualLasciapassare =
            default(Entity.BusinessEntities.Permesso.Patente);
        if (
            null == lasciapassare
            )
        {
            return false;//cannot enter.
        }// else continue.
        //
        try
        {
            actualLasciapassare = (Entity.BusinessEntities.Permesso.Patente)lasciapassare;// here throws, if cast fails.
            string username = actualLasciapassare.username;
            int id_user = actualLasciapassare.id_username;
            if (
                "utente not found" == username
                || 0 == id_user
                )
            {
                throw new System.Exception("Allarme: avvenuto ingresso di utente NON riconosciuto. ");
            }// else can enter.
            Session["errore"] = null;
            result = true;
        }
        catch (System.Exception ex)
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "VerificaLasciapassare: rilevato tentativo di violazione dell'area riservata. " +
                " IP client=" + UserHostAddress +
                " SessionId=" + Session.SessionID +
                " Ex = " + ex.Message,
                5
            );
            Session["errore"] = "Credenziali non riconosciute.";
            result = false;
        }
        // ready
        return result;
    }//


}// end class
