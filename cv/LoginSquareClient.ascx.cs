using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


public partial class LoginSquareClient : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {

    }// end Page_Load



    protected void itnLogin_Click(object sender, ImageClickEventArgs e)
    {
        LogSinkFs.Wrappers.LogWrappers.SectionOpen("LoginSquareClient", 5);
        LogSinkDb.Wrappers.LogWrappers.SectionOpen("LoginSquareClient", 5);
        //---filter username----NB. no filtering on pwd----------
        string filtered_username = Process.utente.utente_login.filterUsername(this.txtUser.Text);
        //
        int loginResult =
            Process.utente.utente_login.canLogOn(
                filtered_username,
                this.txtPwd.Text //--NB. no filtering on pwd---------- 
            );
        // cache data to be used it in permission-management, iff 0==loginresult.
        Entity.BusinessEntities.Permesso.Patente patente = null;
        if (0 == loginResult)
        {
            patente =
                Process.permesso.permesso_loadSingle.GetPatente(
                    filtered_username
                );
        }// else "patente" stays null.
        //
        if (
            0 == loginResult
            && null != patente
          )
        {//--ok
            this.Session["lasciapassare"] = patente;
            this.Session["errore"] = null;
            //
            LogSinkFs.Wrappers.LogWrappers.SectionContent(
                "Login valido per l'utente " + ((Entity.BusinessEntities.Permesso.Patente)(this.Session["lasciapassare"])).username +
                " IP client=" + this.Request.UserHostAddress +
                " SessionId=" + this.Session.SessionID,
                5);
            LogSinkDb.Wrappers.LogWrappers.SectionContent(
                "Login valido per l'utente " + ((Entity.BusinessEntities.Permesso.Patente)(this.Session["lasciapassare"])).username +
                " IP client=" + this.Request.UserHostAddress +
                " SessionId=" + this.Session.SessionID,
                5);
            //
            LogSinkFs.Wrappers.LogWrappers.SectionClose();
            LogSinkDb.Wrappers.LogWrappers.SectionClose();
            this.Response.Redirect("~/zonaRiservata/candidatoLoad.aspx");
        }
        else// if 0<loginResult -> get error msg.
        {//--out
            this.Session["lasciapassare"] = null;
            this.Session["errore"] = Process.utente.utente_login.loginMessages[loginResult];
            //
            LogSinkFs.Wrappers.LogWrappers.SectionContent(
                "Login fallito per l'utente " + this.txtUser.Text + " tradotto in " + filtered_username +
                " IP client=" + this.Request.UserHostAddress +
                " SessionId=" + this.Session.SessionID,
                5);
            LogSinkDb.Wrappers.LogWrappers.SectionContent(
                "Login fallito per l'utente " + this.txtUser.Text + " tradotto in " + filtered_username +
                " IP client=" + this.Request.UserHostAddress +
                " SessionId=" + this.Session.SessionID,
                5);
            //
            LogSinkFs.Wrappers.LogWrappers.SectionClose();
            LogSinkDb.Wrappers.LogWrappers.SectionClose();
            this.Response.Redirect("~/errore.aspx");
        }
    }// end itnLogin_Click()


}// end class
