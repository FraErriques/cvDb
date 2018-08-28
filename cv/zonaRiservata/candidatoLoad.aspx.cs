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


public partial class zonaRiservata_candidatoLoad : System.Web.UI.Page
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
            "zonaRiservata_candidatoLoad"
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
                // don't throw new System.Exception(" candidatoLoad::Page_Load . this.Session[CacherDbView] is null. ");
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




    /// <summary>
    /// NB.----- query on the db_index, NOT on the combo index!------
    ///  mai usare:  this.ddlSettori.SelectedIndex
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlSettoriRefreshQuery(object sender, EventArgs e)
    {
        int int_sector = default(int);
        try//---if ddlSettori.SelectedItem==null will throw.
        {
            int_sector = int.Parse(this.ddlSettori.SelectedItem.Value);
            this.Session["comboSectors_selectedValue"] = int_sector;// NB.---cache across postbacks.-----
        }
        catch (System.Exception ex)
        {
            string dbg = ex.Message;
            int_sector = -1;// invalid.
        }
        finally
        {
            this.loadData(int_sector);
        }
    }// end ddlSettoriRefreshQuery





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
                    "candidatoLoad::loadData ex = "
                    +ex.Message
                    +" ___ stack = " +ex.StackTrace);
            }
        }
        else
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "candidatoLoad::  Debug: Session[_indexOfAllSectors_] is null -> refreshing combo. "
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
            throw new System.Exception(" CandidatoLoad::combo::indexes <0: should never pass here.");
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
            throw new System.Exception("Presentation::candidatoLoad::loadData() failed CacherDbView Ctor. ");
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
        if(
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


}// end class


#region cantina
//object id_condizione_obj = this.dtCandidati.Rows[c]["id_condizione"];// stato-fase, i.e. {"accesa", "spenta"}.
//object id_statoLavorazione_obj = this.dtCandidati.Rows[c]["id_statoLavorazione"];// {sospesa,..,etc.}
////
////-------------START individuazione lavori scaduti--------------------------------------------------
//object scadenzaRiga_obj =
//    this.dtCandidati.Rows[c]["orizzonteProposto"];
//DateTime scadenzaRiga = DateTime.MaxValue;// init to "far-deadline".
//if (
//    System.DBNull.Value == scadenzaRiga_obj
//    || null == scadenzaRiga_obj
//    )
//{// keep initialization to "far-deadline".
//}
//else
//{
//    scadenzaRiga = (DateTime)scadenzaRiga_obj;
//    if (scadenzaRiga < DateTime.Now)
//    {
//        ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).BackColor =
//            System.Drawing.Color.YellowGreen;// expired deadline.
//    }// else leave its beige default.
//}
//-------------END individuazione lavori scaduti--------------------------------------------------
//

//-----caso del destinatario, che fa avanzare la applicazione-------------
//if (
//    1==1
//    //(str_parRuolo == "addressee" || str_parRuolo == "requirer")
//    //&& (str_parEditMode == "insert" || str_parEditMode == "updateFull")
//    //&& (str_parCriterio == "1" || str_parCriterio == "3")
//   )
//{// scelta colore in base ad "fase-accesa"-"fase-spenta".
//    if (2 != (int)id_condizione_obj)// if != "accesa"
//    {
//        ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Enabled = false;// disable entire row.
//        ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).BackColor = System.Drawing.Color.BurlyWood;
//        ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[10]).Visible = true;//  "avanzamento" icon: visible and enabled, but readonly when needed so.
//        //
//        ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Visible = true;// shift-arrows" icon
//        ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Enabled = false;// shift-arrows" icon
//    }// else leave it enabled.
//}// end scelta colore in base ad "fase-accesa"-"fase-spenta".
//else if (
//    2==5
//    // sola lettura delle pratiche "proprie" (richiedente o destinatario), anche spente.
//    //(str_parRuolo == "addressee" || str_parRuolo == "requirer")
//    //&& (str_parEditMode == "read")
//    //&& (str_parCriterio == "5")
//   )
//{// righe "fase-spenta" gia' scartate via query.
//    ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Enabled = true;// enable entire row.
//    // in default color, without horizont-arrows, with edit-btn in read only.
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[10]).Visible = true;//  "avanzamento" icon: visible and enabled, but readonly when needed so.
//    //
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Visible = true;// shift-arrows" icon
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Enabled = false;// shift-arrows" icon
//}// end scelta colore in base ad "fase-accesa"-"fase-spenta".
//else if (
//    2==2
////"stranger" == str_parRuolo                // neither requirer nor addressee, i.e. translator
////    // && 2 == (int)id_condizione_obj         // translate dead phases too.
////            && 7 > (int)(id_statoLavorazione_obj)     // i.e. non ancora tradotta.
////            && "updateTranslate" == str_parEditMode   // from menu "Traduzioni"
//    )
//{// traduzioni
//    ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Enabled = true;// enable entire row.
//    ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).BackColor = System.Drawing.Color.Aquamarine;
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[10]).Visible = true;// change "avanzamento" icon into traduzioni
//    System.Web.UI.Control mycontrol_btnStatoLavorazione =
//        (this.dgrCandidati.Items[c]).FindControl("btnStatoLavorazione");
//    ((System.Web.UI.WebControls.ImageButton)mycontrol_btnStatoLavorazione).ImageUrl =
//        "~/img/traduzioni.jpg";
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Visible = true;// shift-arrows" icon
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Enabled = false;// shift-arrows" icon
//}// end traduzioni
//else if (
//    2==3
//    //"stranger" == str_parRuolo    // neither requirer nor addressee, nor translator but reader
//    //&& "read" == str_parEditMode   // from menu "mera elencazione"
//    )
//{// reader
//    ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Enabled = true;// disable entire row.
//    ((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).BackColor = System.Drawing.Color.BurlyWood;
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[10]).Visible = true;//  "avanzamento" icon: visible and enabled, but readonly when needed so.
//    //
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Visible = true;// shift-arrows" icon
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[13]).Enabled = false;// shift-arrows" icon
//}// end reader
//
//-------------------------------------loop on each job------------------------------
//for (int c = 0; c < nLavori; c++)
//{
//if (  // esempio di variazione su colonne, operate riga per riga.
//        6 == logged_usr_id // admin for dbg
//  )
//{
//    // colonna UpdateAbstract visible.
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[6]).Visible = true;
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[6]).Enabled = true;
//}
//else
//{
//    // colonna UpdateAbstract NOT visible.
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[6]).Visible = false;
//    ((((System.Web.UI.WebControls.TableRow)(this.dgrCandidati.Items[c])).Cells)[6]).Enabled = false;
//}
//}// end for each row

#endregion cantina
