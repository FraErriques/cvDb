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


public partial class errore : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        //--check just onEntry, since also un-logged can enter here.-----
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "errore"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        object errore = this.Session["errore"];
        if (null == errore)
        {
            this.lblTitolo.Text = "";
            this.lblStato.Text = "";
        }
        else
        {
            this.lblTitolo.Text = "Si e' verificato un errore.";
            this.lblStato.Text = "I dettagli dell'errore verificatosi sono: " + (string)this.Session["errore"];
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                this.lblTitolo.Text
                + this.lblStato.Text
                , 5);
        }
        //
        if (VerificaLasciapassare.CanLogOn(this.Session, this.Request.UserHostAddress))
        {
            try
            {
                Control loginForm = this.LoginSquareClient1.FindControl("divLoginSquareContent");
                loginForm.Visible = false;// hide login.
            }
            catch (System.Exception ex)
            {
                string dbg = ex.Message + "___" + ex.StackTrace;
            }
        }// else show login.
    }// end Page_Load


}
