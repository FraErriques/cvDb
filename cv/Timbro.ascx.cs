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


public partial class Timbro : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        // in test
        //string lang = ClientLanguageChecker.ClientLanguageGet( this.Request);
        //if ("it" == lang.ToLower().Trim())
        //{
        //    // TODO this.hplCandidatoLoad.Text = "test:_label_italiana_";
        //}
        //else
        //{
        //    // TODO 
        //}
        //
        // tasto di chiusura-browser. Inutilizzato in questa implementazione.
        //this.btnTerminateSession.Attributes["onclick"] = "ChiusuraSessione()";
        //
        if (
             null == this.Session["lasciapassare"]
            )
        {
            this.lblStato.Text = "unbekannter Benutzer / L'utente non e' ancora stato riconosciuto. Procedere alla Login per fare accesso all'applicazione.";
            this.tblTimbro.Enabled = false;
            //----- start admin privileges---
            this.pnlAdminLinks.Enabled = false;
            this.pnlAdminLinks.Visible = false;
            //
            this.pnlInsert.Enabled = false;
            this.pnlInsert.Visible = false;
            //-----end admin privileges-----
        }
        else// user logged, differentiate by level.
        {
            this.lblStato.Text = "Benutzer: / L'utente collegato e': " + ((Entity.BusinessEntities.Permesso.Patente)(this.Session["lasciapassare"])).username;
            this.tblTimbro.Enabled = true;
            //
            int loggedUsrLevel = RoleChecker.TryRoleChecker(
                this.Session,
                this.Request.UserHostAddress
            );
            ////----- start differentiate by level---
            /*
             *  0  unlogged
             *  1  admin
             *  2  writer
             *  3  reader
             * 
             */
            if (
                1 == loggedUsrLevel  // admin
                )
            {
                this.pnlAdminLinks.Enabled = true;
                this.pnlAdminLinks.Visible = true;
                //
                this.pnlInsert.Enabled = true;
                this.pnlInsert.Visible = true;
                this.hplCategoriaInsert.Enabled = true;
                this.hplCategoriaInsert.Visible = true;
            }
            else if (
                2 == loggedUsrLevel  // writer
                )
            {
                this.pnlAdminLinks.Enabled = false;
                this.pnlAdminLinks.Visible = false;
                //
                this.pnlInsert.Enabled = true;
                this.pnlInsert.Visible = true;
                this.hplCategoriaInsert.Enabled = true;
                this.hplCategoriaInsert.Visible = true;
            }
            else // reader
            {
                this.pnlAdminLinks.Enabled = false;
                this.pnlAdminLinks.Visible = false;
                //
                this.pnlInsert.Enabled = false;
                this.pnlInsert.Visible = false;
                this.hplCategoriaInsert.Enabled = false;
                this.hplCategoriaInsert.Visible = false;
            }
            ////----- end differentiate by level---
            //
            string relativeUrl = this.Request.AppRelativeCurrentExecutionFilePath;
            string pageName = relativeUrl.Substring(
                relativeUrl.LastIndexOf("/") + 1
                );
            pageName = pageName.Substring(0, pageName.Length - 5);// 5==(4 of .ext + 1 for zero-based -str)
            //
            // NB. ReEntrantChecker not here, since it is executed AFTER the container_Page Page_Load().
            // each container page Page_Load already called ReEntrantChecker.
            if (// going to a NOT_View_enabled_Page -> Dtor(View).
                "candidatoLoad" != pageName
                && "cvMultiRead" != pageName
                && "PrimeDataGrid" != pageName
                && "queryCandidato" != pageName
                // add here:  ||"otherName" != pageName
                )
            {
                //----NB.--------------
                this.View_DROP_SERVICE();
                //
            }
            // NB. the following code destructs the View, even when it must NOT be done. DON'T DO THAT.
            //else // going to a View_enabled_Page -> check if I have to preserve the existing view, or build a new one. Don't: the Cacher::Ctor does it.
            //{
            //    if ( false==((bool)(this.Session["IsReEntrant"]) ) )// NOT IsReEntrant: change between pages, both ViewEnabled, but with DIFFERENT Views.
            //    {
            //    }// else navigating back on the same view-anabled page -> preserve existing view.
            //}
            //
            switch (pageName)
            {
                default: // stands for phase*  (ex writeSingleProc)
                case "Default":
                case "home":
                case "errore":
                    {
                        if (
                             null == this.Session["lasciapassare"]
                            )
                        {
                            this.hplCandidatoLoad.Enabled = false;
                            this.hplCandidatoInsert.Enabled = false;
                            this.hplLogViewerWeb.Enabled = false;
                            this.hplChangePwd.Enabled = false;
                            this.hplCategoriaInsert.Enabled = false;
                            this.hplPrimes.Enabled = false;
                        }
                        else
                        {
                            this.hplCandidatoLoad.Enabled = true;
                            this.hplCandidatoInsert.Enabled = true;
                            this.hplLogViewerWeb.Enabled = true;
                            this.hplChangePwd.Enabled = true;
                            this.hplCategoriaInsert.Enabled = true;
                            this.hplPrimes.Enabled = true;
                        }
                        break;
                    }
                case "candidatoLoad":
                    {
                        this.hplCandidatoLoad.Enabled = false;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "candidatoInsert":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = false;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "categoriaInsert":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = false;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "queryCandidato":
                    {
                        this.hplQueryCandidato.Enabled = false;
                        //
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "cvMultiRead":// NB. dalle pagine docMulti si deve poter navigare su quelle generali( i.e. di tutti i candidati).
                case "cvMultiInsert":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "changePwd":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = false;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
                case "PrimeDataGrid":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = true;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = false;
                        break;
                    }
                case "LogViewerWeb":
                    {
                        this.hplCandidatoLoad.Enabled = true;
                        this.hplCandidatoInsert.Enabled = true;
                        this.hplLogViewerWeb.Enabled = false;
                        this.hplChangePwd.Enabled = true;
                        this.hplCategoriaInsert.Enabled = true;
                        this.hplPrimes.Enabled = true;
                        break;
                    }
            }// end switch
        }// end "user-logged".
        //
        // ready
    }// end Page_Load




    protected void btnLogout_Click(object sender, EventArgs e)
    {
        this.Session.Remove("lasciapassare");// better the next; it's enough it alone.
        this.Session["lasciapassare"] = null;
        this.Session["RoleChecker"] = null;//----NB.--------------
        //----NB.--------------
        this.View_DROP_SERVICE();
        //
        this.Response.Redirect("~/home.aspx");
    }


    private void View_DROP_SERVICE()
    {
        // no matter the int_result; it's !=0, also onSuccess.
        Entity.Proxies.usp_ViewCacher_generic_DROP_SERVICE.usp_ViewCacher_generic_DROP(
            ViewNameDecorator.ViewNameDecorator_SERVICE(this.Session.SessionID) // delete view, generated by this.Session.
        );
        this.Session["CacherDbView"] = null;
        this.Session["DynamicPortionPtr"] = null;
        //
    }


}// end class


#region cantina
//if (
//    1 == loggedUsrLevel
//    || 2 == loggedUsrLevel
//    )
//{
//    this.pnlAdminLinks.Enabled = false;
//    this.pnlAdminLinks.Visible = false;
//    //
//    this.pnlInsert.Enabled = false;
//    this.pnlInsert.Visible = false;
//}
//else
//{
//    this.pnlAdminLinks.Enabled = true;
//    this.pnlAdminLinks.Visible = true;
//    //
//    this.pnlInsert.Enabled = true;
//    this.pnlInsert.Visible = true;
//    this.hplCategoriaInsert.Enabled = true;
//    this.hplCategoriaInsert.Visible = true;
//}
////-----end admin privileges-----
#endregion cantina
