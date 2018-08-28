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


public partial class zonaRiservata_LogViewerWeb : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        //
        if (!VerificaLasciapassare.CanLogOn(
                this.Session,
                this.Request.UserHostAddress
                )
            )
        {// un-logged user. Close both logs and goto error.
            this.Response.Redirect("../errore.aspx");
        }// else il lasciapassare e' valido
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_LogViewerWeb"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        if( ! this.IsPostBack)
        {
            int int_month = DateTime.Today.Month;
            string str_month;
            if (10 > int_month)
            {
                str_month = "0" + int_month.ToString();
            }
            else
            {
                str_month = int_month.ToString();
            }
            //
            int int_day = DateTime.Today.Day;
            string str_day;
            if (10 > int_day)
            {
                str_day = "0" + int_day.ToString();
            }
            else
            {
                str_day = int_day.ToString();
            }
            //
            string from = DateTime.Today.Year.ToString() + str_month + str_day;
            string   to = DateTime.Today.Year.ToString() + str_month + str_day;
            System.Data.DataTable webLogTbl =
                Entity.Proxies.LogViewer_web_cvDb_SERVICE.LogViewer_web_cvDb(
                    from,
                    to
                );
            if (
                null == webLogTbl
                || 0 == webLogTbl.Rows.Count
                )
            {
                this.grdLogging.DataSource = null;
            }
            else
            {
                this.grdLogging.DataSource = webLogTbl;
                this.grdLogging.DataBind();
            }           
        }// leave existing data on PostBack.
    }//


}// end class
