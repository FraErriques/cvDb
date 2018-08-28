using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public static class RoleChecker
{




    /// <summary>
    /// 0  unlogged
    /// 1  admin
    /// 2  writer
    /// 3  reader
    /// </summary>
    /// <returns></returns>
    public static int TryRoleChecker(
        System.Web.SessionState.HttpSessionState Session,
        string UserHostAddress
      )
    {
        int res = default(int);
        object RoleChecker = Session["RoleChecker"];//----NB.--------------
        if (null != RoleChecker)
        {
            res = (int)RoleChecker;
        }
        else
        {
            res = RoleCheckerQuery(Session, UserHostAddress);
        }
        // ready
        return res;
    }// end TryRoleChecker




    /// <summary>
    /// 0  unlogged
    /// 1  admin
    /// 2  writer
    /// 3  reader
    /// </summary>
    /// <returns></returns>
    public static int RoleCheckerQuery(
        System.Web.SessionState.HttpSessionState Session,
        string UserHostAddress
      )
    {
        int res = default(int);
        object lasciapassare = Session["lasciapassare"];
        Entity.BusinessEntities.Permesso.Patente actualLasciapassare =
            default(Entity.BusinessEntities.Permesso.Patente);
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            "RoleCheckerQuery for host " + UserHostAddress, 0);
        //
        if (
            null == lasciapassare
            )
        {
            return 0;//cannot enter.
        }// else continue.
        string username = default(string);
        int id_user = default(int);
        //
        try
        {
            actualLasciapassare = (Entity.BusinessEntities.Permesso.Patente)lasciapassare;// here throws, if cast fails.
            username = actualLasciapassare.username;
            id_user = actualLasciapassare.id_username;
            if (
                "utente not found" == username
                || 0 == id_user
                )
            {
                throw new System.Exception("Allarme: avvenuto ingresso di utente NON riconosciuto. ");
            }// else can enter.
            Session["errore"] = null;
            // still don't know WHO is logged, so nothing about "res" yet.
        }
        catch (System.Exception ex)
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "VerificaLasciapassare: rilevato tentativo di violazione dell'area riservata. " +
                " IP client=" + UserHostAddress +
                " SessionId=" + Session.SessionID +
                " Ex = " + ex.Message,
                0
            );
            Session["errore"] = "Credenziali non riconosciute.";
            res = -1;// unlogged
            return res;
        }
        //
        //
        if (// Administrator: enables LogViewing, more than writing.
            "admin" == username
          )
        {
            res = 1;
        }
        else if (// writer
            "Sandro Francesconi" == username
            || "Thomas Albarello" == username
            || "Daniela Pichierri" == username
            )
        {
            res = 2;
        }
        else
        {// reader
            res = 3;
        }
        Session["RoleChecker"] = res;//----NB.--------------
        // ready
        //
        return res;
    }//


}//
