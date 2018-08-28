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


public partial class zonaRiservata_changePwd : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        // log-fs
        LogSinkFs.Wrappers.LogWrappers.SectionOpen(
            "pagina:ChangePwd, metodo:Page_Load, " +
            this.Session.SessionID + " ip_client: " +
            this.Request.UserHostAddress, 0);
        // end log-fs
        // log-db
        LogSinkDb.Wrappers.LogWrappers.SectionOpen(
            "pagina:ChangePwd, metodo:Page_Load", 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent(
            this.Session.SessionID + " ip_client: " +
            this.Request.UserHostAddress, 0);
        // end log-db
        //
        if (!VerificaLasciapassare.CanLogOn(
                this.Session,
                this.Request.UserHostAddress
                )
            )
        {// un-logged user. Close both logs and goto error.
            LogSinkFs.Wrappers.LogWrappers.SectionClose();
            LogSinkDb.Wrappers.LogWrappers.SectionClose();
            this.Response.Redirect("../errore.aspx");
        }// else il lasciapassare e' valido
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_changePwd"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        LogSinkFs.Wrappers.LogWrappers.SectionClose();
        LogSinkDb.Wrappers.LogWrappers.SectionClose();
    }// end Page_Load



    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        bool result =
            Process.utente.utente_changePwd.CambioPassword(
                ((Entity.BusinessEntities.Permesso.Patente)(this.Session["lasciapassare"])).username,
                this.txtOldPwd.Text,
                this.txtNewPwd.Text,
                this.txtConfirmNewPwd.Text
            );
        // write result.
        if (result)
        {
            this.lblStato.Text = "La password e' stata modificata.";
            this.lblStato.BackColor = System.Drawing.Color.Gray;
        }
        else
        {
            this.lblStato.Text = "Non e' stato possibile modificare la password.";
            this.lblStato.BackColor = System.Drawing.Color.Red;
        }
    }// end btnChangePwd_Click


}// end class
