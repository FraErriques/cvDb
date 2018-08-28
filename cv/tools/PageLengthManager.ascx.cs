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


public partial class tools_PageLengthManager : System.Web.UI.UserControl
{


    protected void Page_Load(object sender, EventArgs e)
    {
        ////  NB. not from ascx
        ///*
        // * NB. page state check.-----------------------------------------------------------------
        // * 
        // */
        //PageStateChecker.PageStateChecker_SERVICE(
        //    "tools_PageLengthManager"
        //    , this.Request
        //    , this.IsPostBack
        //    , this.Session
        //);
        ////----------------------------------------------- END  page state check.-----------------
        if (!this.IsPostBack)
        {
            object obj_CacherDbView = this.Session["CacherDbView"];
            if (null != obj_CacherDbView)
            {
                this.txtRowsInPage.Text = ((CacherDbView)(this.Session["CacherDbView"])).RowsInChunk.ToString();
            }// else skip.
        }
    }




    protected void btnRowsInPage_Click(object sender, EventArgs e)
    {
        object obj_CacherDbView = this.Session["CacherDbView"];
        if (null == obj_CacherDbView)
        {
            return;// on page; not yet loaded, the View.
        }
        else
        {
            try
            {
                System.Web.UI.WebControls.GridView grdDatiPaginati = (System.Web.UI.WebControls.GridView)Parent.FindControl("grdDatiPaginati");
                System.Web.UI.WebControls.Panel pnlPageNumber = (System.Web.UI.WebControls.Panel)Parent.FindControl("pnlPageNumber");
                CacherDbView cacherDbView = (CacherDbView)(obj_CacherDbView);
                int tmpRowsInPage = int.Parse(this.txtRowsInPage.Text);
                //----range check-------
                if( 0 >= tmpRowsInPage )
                {
                    throw new System.Exception(" non-positive number of rowsPerPage required. ");
                }
                else
                {
                    cacherDbView.RowsInChunk = tmpRowsInPage;
                }
                //
                cacherDbView.Pager_EntryPoint(
                    this.Session
                    , this.Request
                    , grdDatiPaginati
                    , pnlPageNumber
                );
            }
            catch (System.Exception ex)
            {
                LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                    "Presentation::PrimeGrid: failed to change page lenght: ex = " + ex.Message
                    , 0);
            }
        }
    }//


}//
