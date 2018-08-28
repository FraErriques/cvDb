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


public partial class zonaRiservata_queryCandidato : System.Web.UI.Page
{
    private int indexOfAllSectors; // it's one after the last id in the table.


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
            "zonaRiservata_queryCandidato"
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
            ComboManager.populate_Combo_ddlSettore_for_LOAD(//---primo popolamento.
                this.ddlSettori,
                0 // "null" or <0, for no preselection. Instead to preselect choose the ordinal; eg. 2=="Appalti", 0 for "choose your Sector", which performs no query.
                , out indexOfAllSectors
            );
            this.Session["indexOfAllSectors"] = indexOfAllSectors;// NB.---cache across postbacks.-----
            this.Session["comboSectors_selectedValue"] = 0;// NB.---cache across postbacks.-----
            //
            this.loadData(0);// means no query.
        }
        else if (
            !this.IsPostBack//----------------------------------------------------false
            && (bool)(this.Session["IsReEntrant"])//------------------------------true
            )
        {// coming from html-numbers of pager
            // needed combo-refresh, but re-select combo-Value from Session  --------
            //
            int int_comboSectors_selectedValue = (int)(this.Session["comboSectors_selectedValue"]);// NB.---cache across postbacks.-----
            ComboManager.populate_Combo_ddlSettore_for_LOAD(//---primo popolamento.
                this.ddlSettori,
                int_comboSectors_selectedValue // "null" or <0, for no preselection. Instead to preselect choose the ordinal; eg. 2=="Appalti", 0 for "choose your Sector", which performs no query.
                , out indexOfAllSectors
            );
            this.Session["indexOfAllSectors"] = indexOfAllSectors;// NB.---cache across postbacks.-----
            //
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
                loadData(int_comboSectors_selectedValue); // TODO debug
                // don't throw new System.Exception(" queryCandidato::Page_Load . this.Session[CacherDbView] is null. ");
            }
        }
        else if (
            this.IsPostBack//------------------------------------------------------true
            && !(bool)(this.Session["IsReEntrant"])//------------------------------false
            )
        {
            // don't: throw new System.Exception(" impossible case: if IsReEntrant at least one entry occurred. ");
        }
        else if (
            this.IsPostBack//------------------------------------------------------true
            && (bool)(this.Session["IsReEntrant"])//-------------------------------true
            )
        {// coming from combo-index-changed.
            // no combo-refresh.
            // drop the current view and create the new one, by delegate ddlSettoriRefreshQuery.
        }// no "else" possible: case mapping is complete.
    }// end Page_Load

    
    //    if (!VerificaLasciapassare.CanLogOn(
    //            this.Session,
    //            this.Request.UserHostAddress
    //            )
    //        )
    //    {
    //        this.Response.Redirect("../errore.aspx");
    //    }// else il lasciapassare e' valido -> get in.
    //    //
    //    /*
    //     * NB. page state check.-----------------------------------------------------------------
    //     * 
    //     */
    //    PageStateChecker.PageStateChecker_SERVICE(
    //        "zonaRiservata_queryCandidato" // TODO add to "Timbro"
    //        , this.Request
    //        , this.IsPostBack
    //        , this.Session
    //    );
    //    //----------------------------------------------- END  page state check.-----------------
    //    //
    //    int sectorCardinality = 0;
    //    if (!this.IsPostBack)
    //    {
    //        // this.loadData();  which?
    //        ComboManager.populate_Combo_ddlSettore_for_LOAD(
    //            this.ddlSettore,
    //            null,// no pre-selection.
    //            out sectorCardinality
    //        );
    //    }// else don't.
    //    else
    //    {
    //        int tmp = sectorCardinality;
    //    }
    //}// end Page_Load.





    ///// <summary>
    ///// NB.----- query on the db_index, NOT on the combo index!------
    /////  mai usare:  this.ddlSettori.SelectedIndex
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void ddlSettoriRefreshQuery(object sender, EventArgs e)
    //{
    //    int int_sector = default(int);
    //    try//---if ddlSettori.SelectedItem==null will throw.
    //    {
    //        int_sector = int.Parse(this.ddlSettori.SelectedItem.Value);
    //        this.Session["comboSectors_selectedValue"] = int_sector;// NB.---cache across postbacks.-----
    //    }
    //    catch (System.Exception ex)
    //    {
    //        string dbg = ex.Message;
    //        int_sector = -1;// invalid.
    //    }
    //    finally
    //    {
    //        this.loadData(int_sector);
    //    }
    //}// end ddlSettoriRefreshQuery





    /// <summary>
    /// NB.---deve essere il Pager a chiamarlo, quando fa DataBind()--this.prepareLavagnaDynamicPortion()
    /// </summary>
    /// <param name="choosenSector"></param>
    private void loadData(int choosenSector)
    {
        string queryTail;
        object obj_indexOfAllSectors = null;
        obj_indexOfAllSectors = this.Session["indexOfAllSectors"];
        if (
            null != obj_indexOfAllSectors
            )
        {
            try
            {
                this.indexOfAllSectors = (int)(obj_indexOfAllSectors);// NB.---cache across postbacks.-----
            }
            catch (System.Exception ex)
            {
                string dbg = ex.Message;
                throw new System.Exception(
                    "queryCandidato::loadData ex = "
                    + ex.Message
                    + " ___ stack = " + ex.StackTrace);
            }
        }
        else
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "queryCandidato::  Debug: Session[_indexOfAllSectors_] is null -> refreshing combo. "
                , 0
            );
            //
            ComboManager.populate_Combo_ddlSettore_for_LOAD(//---primo popolamento.
                this.ddlSettori,
                choosenSector // "null" or <0, for no preselection. Instead to preselect choose the ordinal; eg. 2=="Appalti", 0 for "choose your Sector", which performs no query.
                , out indexOfAllSectors
            );
            this.Session["indexOfAllSectors"] = indexOfAllSectors;// NB.---cache across postbacks.-----
            this.Session["comboSectors_selectedValue"] = choosenSector;// NB.---cache across postbacks.-----
        }
        //
        if (0 == choosenSector)
        {
            this.grdDatiPaginati.DataSource = null;//  --no query for this selection ---
            this.grdDatiPaginati.DataBind();
            this.prepareLavagnaDynamicPortion();//-------NB.-------------------
            return;// on page: --no query for this selection ---
        }
        else if (
            0 < choosenSector // from 1
            && this.indexOfAllSectors > choosenSector // to the last existing Sector
            )
        {
            queryTail = " and id_settore = " + choosenSector.ToString();
        }
        else if (// "tutti i settori aziendali", i.e. select without "where-tail" -----
            this.indexOfAllSectors == choosenSector)
        {
            queryTail = "";// Proxy will manage it.
        }
        else//  indexes<-1: should never pass here.
        {
            throw new System.Exception(" queryCandidato::combo::indexes <0: should never pass here.");
        }
        //
        System.Web.UI.WebControls.TextBox txtRowsInPage = null;
        int int_txtRowsInPage = default(int);
        try
        {
            txtRowsInPage =
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
                Entity.Proxies.usp_ViewCacher_specific_CREATE_candidato_SERVICE.usp_ViewCacher_specific_CREATE_candidato
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
            throw new System.Exception("Presentation::queryCandidato::loadData() failed CacherDbView Ctor. ");
        }
    }// end loadData()


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
        if (
            1 == loggedUsrLevel
            || 2 == loggedUsrLevel
            )
        {
            this.grdDatiPaginati.Columns[4].Visible = true;// disable column "add-Doc", for ALL rows.
            this.grdDatiPaginati.Columns[6].Visible = true;// disable column "update-Abstract", for ALL rows.
        }
        else
        {
            this.grdDatiPaginati.Columns[4].Visible = false;// disable column "add-Doc", for ALL rows.
            this.grdDatiPaginati.Columns[6].Visible = false;// disable column "update-Abstract", for ALL rows.
        }
    }// end prepareLavagnaDynamicPortion()


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
        switch (Job_Edit_Nature)
        {
            default:
            case "GeneralEdit":
                {
                    this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
                    this.Session["indexOfAllSectors"] = null;// be sure to clean.
                    this.Response.Redirect("cvMultiRead.aspx");
                    break;
                }
            case "AddDocuments":
                {
                    this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
                    this.Session["indexOfAllSectors"] = null;// be sure to clean.
                    this.Response.Redirect("cvMultiInsert.aspx");
                    break;
                }
            case "UpdateAbstract":
                {
                    this.Session["ref_candidato_id"] = id_Candidate_ToEdit;
                    this.Session["AbstractNature"] = "candidato";// {"candidato", "documento"}
                    this.Session["indexOfAllSectors"] = null;// be sure to clean.
                    this.Response.Redirect("UpdateAbstract.aspx");
                    break;
                }
        }// end switch.
        // ready, with tokens in Session.
    }


    /// <summary>
    /// NB. la query-tail che si costruisce in questa funzione va collegata obbligatoriamente in
    /// "and" con la query-tail presente nella stored, in quanto essa utilizza tale tail per 
    /// evitare un prodotto cartesiano fra candidati e categorie.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDoPostback_Click(object sender, EventArgs e)
    {
        // NB. primo "and implicito.
        string queryTail = " and ";
        string indexOfAllSectors = null;
        try
        {
            indexOfAllSectors = ((int)(this.Session["indexOfAllSectors"])).ToString();
        }
        catch// all
        {// indexOfAllSectors will remain null.
        }
        //
        if (
            null == this.ddlSettori.SelectedItem
            || "0" == this.ddlSettori.SelectedItem.Value// no sector selected.
            || null == this.ddlSettori.SelectedItem.Value// managed ex.
            || indexOfAllSectors == this.ddlSettori.SelectedItem.Value// all sectors selected.
            )
        {
            // invalid "sector" characterization -> where condition omitted.
        }
        else
        {
            queryTail += " " + this.ddlSettori.SelectedItem.Value + " = c.id_settore ";
        }
        //
        if( queryTail.Length>5)// something added to the initial "and"->add another logical connector
        {
            if(this.rdeAnd_settore_nominativo.Selected)
            {
                queryTail += " and ";
            }
            else if(this.rdeOr_settore_nominativo.Selected)
            {
                queryTail += " or ";
            }
            else
            {
                throw new System.Exception("Debug needed on page queryCaqndidato ! ");
            }
        }
        else// nothing added to the initial "and" -> DON'T add another logical connector.
        {
        }
        /* 2011.04.19
         * NB. a query like the following does not work, se sono stati inseriti MOLTEPLICI spazi:
         * select * from candidato where nominativo like '%Di Franza%'
         * it needs to be patched like this, in order to work:
         * select * from candidato where nominativo like '%Di%Franza%'
         * 
         */
        string txtNominativoFiltering = this.txtNominativo.Text.Trim();
        txtNominativoFiltering = txtNominativoFiltering.Replace(' ', '%');// NB. see note above.
        txtNominativoFiltering = txtNominativoFiltering.Replace('\'', '%');// NB. single apex is a reserved char in SQL.
        queryTail += " c.nominativo like '%" + txtNominativoFiltering + "%'";
        //
        // it's necessary to add another logical connector.
        if(this.rdeAnd_nominativo_abstract.Selected)
        {
            queryTail += " and ";
        }
        else if(this.rdeOr_nominativo_abstract.Selected)
        {
            queryTail += " or ";
        }
        else
        {
            throw new System.Exception("Debug needed on page queryCaqndidato ! ");
        }
        queryTail += " c.note like '%" + this.txtAbstract.Text.Trim() + "%'";
        //
        //----here ends the query-tail building code and starts the query execution.
        //
        System.Web.UI.WebControls.TextBox txtRowsInPage = null;
        int int_txtRowsInPage = default(int);
        try
        {
            txtRowsInPage =
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
                Entity.Proxies.usp_ViewCacher_specific_CREATE_candidato_SERVICE.usp_ViewCacher_specific_CREATE_candidato
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
            throw new System.Exception("Presentation::queryCandidato::loadData() failed CacherDbView Ctor. ");
        }
    }// end btnDoPostback()


}// end class.
