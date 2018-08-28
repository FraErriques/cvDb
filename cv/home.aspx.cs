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



public partial class home : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        string Lang = null;
        if (Request.UserLanguages != null)
        {
            if (0 < Request.UserLanguages.Length)
            {
                Lang = Request.UserLanguages[0];
                if (Lang != null)
                {
                    // TODO
                }
                else
                {
                    // TODO
                }
                /* ---------------------------- tips & tricks ----------------------------------
                 * 
                 * System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Lang) ;
                 * if (CurrencySymbol != null && CurrencySymbol != "")
                 * {
                 *      CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = CurrencySymbol;
                 * }// else skip
                 * 
                 */
            }// else skip
        }// else skip
        string currentCurrencySymbol = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol;
        //
        //--check just onEntry, since also un-logged can enter here.-----
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "home"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        LogSinkFs.Wrappers.LogWrappers.SectionOpen("cv_db::home,Page_Load", 0);
        LogSinkDb.Wrappers.LogWrappers.SectionOpen("cv_db::home,Page_Load", 0);
        //
        LogSinkFs.Wrappers.LogWrappers.SectionContent(this.Request.UserHostName, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent(this.Request.UserHostName, 0);
        //
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.AnonymousID = " + this.Request.AnonymousID, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.ApplicationPath = " + this.Request.ApplicationPath, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.AppRelativeCurrentExecutionFilePath = " + this.Request.AppRelativeCurrentExecutionFilePath, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.CurrentExecutionFilePath = " + this.Request.CurrentExecutionFilePath, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.FilePath = " + this.Request.FilePath, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.Form = " + this.Request.Form, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.IsAuthenticated = " + this.Request.IsAuthenticated, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.IsLocal = " + this.Request.IsLocal, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.LogonUserIdentity = " + this.Request.LogonUserIdentity, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.PhysicalApplicationPath = " + this.Request.PhysicalApplicationPath, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.QueryString = " + this.Request.QueryString, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.ServerVariables = " + this.Request.ServerVariables, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.UserHostAddress = " + this.Request.UserHostAddress, 0);
        LogSinkDb.Wrappers.LogWrappers.SectionContent("this.Request.UserHostName = " + this.Request.UserHostName, 0);
        //
        //
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.AnonymousID = " + this.Request.AnonymousID, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.ApplicationPath = " + this.Request.ApplicationPath, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.AppRelativeCurrentExecutionFilePath = " + this.Request.AppRelativeCurrentExecutionFilePath, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.CurrentExecutionFilePath = " + this.Request.CurrentExecutionFilePath, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.FilePath = " + this.Request.FilePath, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.Form = " + this.Request.Form, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.IsAuthenticated = " + this.Request.IsAuthenticated, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.IsLocal = " + this.Request.IsLocal, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.LogonUserIdentity = " + this.Request.LogonUserIdentity, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.PhysicalApplicationPath = " + this.Request.PhysicalApplicationPath, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.QueryString = " + this.Request.QueryString, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.ServerVariables = " + this.Request.ServerVariables, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.UserHostAddress = " + this.Request.UserHostAddress, 0);
        LogSinkFs.Wrappers.LogWrappers.SectionContent("this.Request.UserHostName = " + this.Request.UserHostName, 0);
        //
        LogSinkFs.Wrappers.LogWrappers.SectionClose();
        LogSinkDb.Wrappers.LogWrappers.SectionClose();
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
    }// end Page_Load()


}//
