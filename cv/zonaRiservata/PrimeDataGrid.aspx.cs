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


public partial class zonaRiservata_PrimeDataGrid : System.Web.UI.Page
{
    private int minKey;
    private int maxKey;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!VerificaLasciapassare.CanLogOn(
                this.Session,
                this.Request.UserHostAddress
                )
            )
        {
            this.Session["indexOfAllSectors"] = null;// be sure to clean.
            this.Response.Redirect("../errore.aspx");
        }// else il lasciapassare e' valido -> get in.
        //
        // PostBack or not, refresh in Session, the present addres of the DynamicPortion method, which has
        // to be called from PagerDbView. Such address changes at every round-trip( tested).
        CacherDbView.DynamicPortionPtr dynamicPortionPtr = new CacherDbView.DynamicPortionPtr(
            this.prepareLavagnaDynamicPortion);
        this.Session["DynamicPortionPtr"] = dynamicPortionPtr;
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_PrimeDataGrid"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        if (
            !this.IsPostBack//----------------------------------------------------false
            && !(bool)(this.Session["IsReEntrant"])//-----------------------------false
            )
        {// first absolute entrance
            //
            this.fieldReader_withDefaults_();
            this.loadData( this.minKey, this.maxKey);// min==max means no query, min<max queries.
        }
        else if (
            !this.IsPostBack//----------------------------------------------------false
            && (bool)(this.Session["IsReEntrant"])//------------------------------true
            )
        {// coming from html-numbers of pager
            // needed combo-refresh, but re-select combo-Value from Session  NB.  --------
            // pager will load the new-chunk, based on a get-param.
            object obj_CacherDbView = this.Session["CacherDbView"];
            if (null != obj_CacherDbView)
            {
                ((CacherDbView)obj_CacherDbView).Pager_EntryPoint(
                    this.Session
                    , this.Request
                    , this.grdDatiPaginati
                    , this.pnlPageNumber
                );
            }
            else
            {
                this.fieldReader_withDefaults_();
                this.loadData(this.minKey, this.maxKey);// min==max means no query, min<max queries.
                LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                    " PrimeGrid::Page_Load . this.Session[CacherDbView] is null: re-built. "
                    , 0);
            }
            //
            System.Web.UI.WebControls.TextBox txtRowsInPage = null;
            try
            {
                txtRowsInPage =
                    (System.Web.UI.WebControls.TextBox)(this.PageLengthManager1.FindControl("txtRowsInPage"));
                txtRowsInPage.Text = ((CacherDbView)(this.Session["CacherDbView"])).RowsInChunk.ToString();
            }
            catch (System.Exception ex)
            {
                string dbg = ex.Message + "___" + ex.StackTrace;
            }
        }
        else if (
            this.IsPostBack//------------------------------------------------------true
            && !(bool)(this.Session["IsReEntrant"])//------------------------------false
            )
        {
        }
        else if (
            this.IsPostBack//------------------------------------------------------true
            && (bool)(this.Session["IsReEntrant"])//-------------------------------true
            )
        {// coming from combo-index-changed.
            // system calls btn_delegate()
        }// no "else" possible: case mapping is complete.
    }// end Page_Load



    /// <summary>
    /// NB.---deve essere il Pager a chiamarlo, quando fa DataBind()--this.prepareLavagnaDynamicPortion()
    /// </summary>
    /// <param name="choosenSector"></param>
    private void loadData(
        int min_key
        ,int max_key
      )
    {
        string queryTail; //  " where	ordinal  between  min and  max";
        //
        if (min_key == max_key)
        {
            this.lblStato.Text = " zonaRiservata_PrimeDataGrid: min_key == max_key : means NO_ACTION, i.e. no query. ";
            this.lblStato.BackColor = System.Drawing.Color.Green;
            this.grdDatiPaginati.DataSource = null;//  --no query for this selection ---
            this.grdDatiPaginati.DataBind();
            this.prepareLavagnaDynamicPortion();//-------NB.-------------------
            return;// on page: --no query for this selection ---
        }
        else if (
            min_key < max_key
          )
        {
            queryTail =
                " where	p.ordinal  between "
                + min_key.ToString()
                + " and "
                + max_key.ToString();
            //
            this.lblStato.Text = "";
            this.lblStato.BackColor = System.Drawing.Color.Transparent;
        }
        else
        {
            this.lblStato.Text = " zonaRiservata_PrimeDataGrid: min_key > max_key : Error: ";
            this.lblStato.BackColor = System.Drawing.Color.Green;
            this.grdDatiPaginati.DataSource = null;//  --no query for this selection ---
            this.grdDatiPaginati.DataBind();
            this.prepareLavagnaDynamicPortion();//-------NB.-------------------
            return;// on page: --no query for this selection ---
        }
        //
        //
        int int_txtRowsInPage = default(int);
        try
        {
            System.Web.UI.WebControls.TextBox txtRowsInPage =
                (System.Web.UI.WebControls.TextBox)(this.PageLengthManager1.FindControl("txtRowsInPage"));
            int_txtRowsInPage = int.Parse(txtRowsInPage.Text);
        }
        catch
        {// on error sends zero rows per page, to Pager.
        }
        //
        CacherDbView cacherDbView = new CacherDbView(
            this.Session
            , queryTail
            , ViewNameDecorator.ViewNameDecorator_SERVICE(this.Session.SessionID)
            , new CacherDbView.SpecificViewBuilder(
                Entity.Proxies.usp_ViewCacher_specific_CREATE_Primes_SERVICE.usp_ViewCacher_specific_CREATE_Primes
              )
            , int_txtRowsInPage
            //
            , this.Request
            , this.grdDatiPaginati
            , this.pnlPageNumber
        );
        if (null != cacherDbView)
        {
            this.Session["CacherDbView"] = cacherDbView;
            cacherDbView.Pager_EntryPoint(
                this.Session
                , this.Request
                , this.grdDatiPaginati
                , this.pnlPageNumber
            );
        }
        else
        {
            throw new System.Exception("Presentation::PrimeGrid::loadData() failed CacherDbView Ctor. ");
        }
    }// end loadData()



    protected void grdDatiPaginati_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id_Candidate_ToEdit = default(int);
        string Job_Edit_Nature = default(string);
        try
        {
            Job_Edit_Nature = (string)e.CommandName;
            id_Candidate_ToEdit = int.Parse((string)e.CommandArgument);
        }
        catch (System.Exception ex)
        {
            string dbg = ex.Message;
            return;// out of here.
        }
        //
        //switch (Job_Edit_Nature)
        //{
        //    default:
        //    case "GeneralEdit":
        //        {
        //            this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
        //            this.Session["indexOfAllSectors"] = null;// be sure to clean.
        //            this.Response.Redirect("cvMultiRead.aspx");
        //            break;
        //        }
        //    case "AddDocuments":
        //        {
        //            this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
        //            this.Session["indexOfAllSectors"] = null;// be sure to clean.
        //            this.Response.Redirect("cvMultiInsert.aspx");
        //            break;
        //        }
        //    case "UpdateAbstract":
        //        {
        //            this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
        //            this.Session["AbstractNature"] = "candidato";// {"candidato", "documento"}
        //            this.Session["indexOfAllSectors"] = null;// be sure to clean.
        //            this.Response.Redirect("UpdateAbstract.aspx");
        //            break;
        //        }
        //}// end switch.
        //// ready, with tokens in Session.
    }


    private void prepareLavagnaDynamicPortion()
    {
        int loggedUsrLevel = RoleChecker.TryRoleChecker(
            this.Session,
            this.Request.UserHostAddress
        );
        /*
         *  0  unlogged
         *  1  admin
         *  2  writer
         *  3  reader
         * 
         */
        //if (
        //    1 == loggedUsrLevel
        //    || 2 == loggedUsrLevel
        //    )
        //{
        //    this.grdDatiPaginati.Columns[4].Visible = true;// disable column "add-Doc", for ALL rows.
        //    this.grdDatiPaginati.Columns[6].Visible = true;// disable column "update-Abstract", for ALL rows.
        //}
        //else
        //{
        //    this.grdDatiPaginati.Columns[4].Visible = false;// disable column "add-Doc", for ALL rows.
        //    this.grdDatiPaginati.Columns[6].Visible = false;// disable column "update-Abstract", for ALL rows.
        //}
    }// end prepareLavagnaDynamicPortion()

 


    protected void btn_Family_ChangeViewBoundary_Click( object sender, EventArgs e)
    {
        if (null != sender)
        {
            string tmpBtnText = ((System.Web.UI.WebControls.Button)sender).Text;
            if (// actually no need to recognize the caller here, but this is the technique:
                "min" == tmpBtnText
                || "max" == tmpBtnText
                )
            {
                // check if min & max are coherent
                // new View
                // both done by this.loadData(minKey, maxKey)
                this.fieldReader_minKey_withErrorGeneration_();// both together, otherwise, one of them is zero, from Page_Load Ctor that resets all fields.
                this.fieldReader_maxKey_withErrorGeneration_();
                this.loadData(
                    this.minKey
                    , this.maxKey);
            }
            else
            {
                throw new System.Exception(" PrimeDataGrid::btn_Family_ChangeViewBoundary_Click");
            }
        }
        else
        {
            throw new System.Exception(" PrimeDataGrid::btn_Family_ChangeViewBoundary_Click");
        }
    }//


    /// <summary>
    /// only when loadData() is called from Page_Load, qhen the user didn't have a chance to write in page.
    /// when the user explicitly presses the button, an actual parse is performed and errors block the view creation.
    /// </summary>
    private void fieldReader_withDefaults_()
    {
        try
        {
            this.minKey = int.Parse(this.txtMin.Text);
        }
        catch
        {
            this.minKey = 1;// default
        }
        try
        {
            this.maxKey = int.Parse(this.txtMax.Text);
        }
        catch
        {
            this.maxKey = 9000;// default
        }
    }//


    private void fieldReader_minKey_withErrorGeneration_()
    {
        try
        {
            this.minKey = int.Parse(this.txtMin.Text);
        }
        catch
        {
            this.minKey = -1;// default
        }
    }//


    private void fieldReader_maxKey_withErrorGeneration_()
    {
        try
        {
            this.maxKey = int.Parse(this.txtMax.Text);
        }
        catch
        {
            this.maxKey = -1;// default
        }
    }//


}// end class
