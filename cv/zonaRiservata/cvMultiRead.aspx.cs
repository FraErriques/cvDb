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


public partial class zonaRiservata_cvMultiRead : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!VerificaLasciapassare.CanLogOn(
                this.Session,
                this.Request.UserHostAddress
                )
            )
        {
            this.Response.Redirect("../errore.aspx");
        }// else il lasciapassare e' valido -> get in.
        //
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
            "zonaRiservata_cvMultiRead"
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
            object ref_candidato_id = this.Session["ref_candidato_id"];
            if (null == ref_candidato_id)
            {
                throw new System.Exception("ref_candidato_id cannot be missing, in this page.");
            }// else continue.
            int int_ref_candidato_id;
            try
            {
                int_ref_candidato_id = (int)ref_candidato_id;
                System.Data.DataTable tblCandidatoDescription =
                    Entity.Proxies.usp_candidato_LOAD_Description_SERVICE.usp_candidato_LOAD_Description(
                        int_ref_candidato_id
                    );
                // table should be as: {Kral Hannes,	Diritto Societario}
                // es. this.lblDeCuius.Text = "Gino Paoli / settore_BLA";
                this.lblDeCuius.Text =
                    (string)(tblCandidatoDescription.Rows[0].ItemArray[0])
                    + " / "
                    + (string)(tblCandidatoDescription.Rows[0].ItemArray[1]);
                //
                this.lblDeCuius.BackColor = System.Drawing.Color.Transparent;
            }
            catch (System.Exception ex)
            {
                string dbg = ex.Message;
                this.lblDeCuius.Text = "Errore. Utilizzare i menu per il cambio pagina, non i bottoni della cache. ";
                this.lblDeCuius.BackColor = System.Drawing.Color.LightCoral;
                int_ref_candidato_id = -1;// Proxy will manage.
            }
            //
            this.loadData(int_ref_candidato_id);
        }
        else if (
            !this.IsPostBack//----------------------------------------------------false
            && (bool)(this.Session["IsReEntrant"])//------------------------------true
            )
        {// coming from html-numbers of pager
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
                throw new System.Exception(" candidatoLoad::Page_Load . this.Session[CacherDbView] is null. ");
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
        {// coming from RowCommand buttons.
        }// no "else" possible: case mapping is complete.
    }//


    /// <summary>
    /// --NB. fare la stored, che crei la VIEW per ogni tabella da Paginare
    /// -------NB.-----moved in Pager--------this.prepareLavagnaDynamicPortion();
    /// </summary>
    /// <param name="int_ref_candidato_id"></param>
    private void loadData(int int_ref_candidato_id)
    {
        // NB. where_tail template:  and ref_candidato_id = 14 --@ref_candidato_id
        string where_tail = " and ref_candidato_id = " + int_ref_candidato_id.ToString() + " ";
        //
        int int_txtRowsInPage = default( int);
        try
        {
            System.Web.UI.WebControls.TextBox txtRowsInPage =
                (System.Web.UI.WebControls.TextBox)(this.PageLengthManager1.FindControl("txtRowsInPage"));
            int_txtRowsInPage = int.Parse( txtRowsInPage.Text);
        }
        catch
        {
        }
        //
        CacherDbView cacherDbView = new CacherDbView(
            this.Session
            , where_tail
            , ViewNameDecorator.ViewNameDecorator_SERVICE(this.Session.SessionID)
            ,new CacherDbView.SpecificViewBuilder(
                Entity.Proxies.usp_ViewCacher_specific_CREATE_documento_SERVICE.usp_ViewCacher_specific_CREATE_documento
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
            throw new System.Exception("Presentation::cvMultiRead::loadData() failed CacherDbView Ctor. ");
        }
    }// end loadData()


    protected void grdDatiPaginati_RowCommand(object sender, GridViewCommandEventArgs e)
    {        int id_Job_ToEdit = default(int);
        string Job_Edit_Nature = default(string);
        try
        {
            id_Job_ToEdit = int.Parse((string)e.CommandArgument);
            Job_Edit_Nature = (string)e.CommandName;
        }
        catch (System.Exception ex)
        {
            string dbg = ex.Message;
            return;// on page: TODO
        }
        //
        switch (Job_Edit_Nature)
        {
            default:
            case "GeneralEdit":
                {
                    try
                    {
                        this.downloadButton_Click(id_Job_ToEdit);
                    }
                    catch (System.Exception ex)
                    {// let it default to -1
                        string dbg = ex.Message;
                        string stack_dbg = ex.StackTrace;
                    }
                    break;
                }
            case "UpdateAbstract":
                {
                    this.Session["ref_candidato_id"] = id_Job_ToEdit;
                    this.Session["AbstractNature"] = "documento";// {"candidato", "documento"}
                    this.Response.Redirect("UpdateAbstract.aspx");
                    break;
                }
        }// end switch.
    }// end grdDatiPaginati_ItemCommand



    //protected void dgrCandidateDocuments_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    int ParolaDiEx = -1;
    //}






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
            // colonna UpdateAbstract visible.
            this.grdDatiPaginati.Columns[6].Visible = true;// disable column, for ALL rows.
        }
        else
        {
            // colonna UpdateAbstract NOT visible.
            this.grdDatiPaginati.Columns[6].Visible = false;// disable column, for ALL rows.
        }
    }// end prepareLavagnaDynamicPortion()



    /// <summary>
    /// download selected doc, for edit or save.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void downloadButton_Click(int id_doc_multi)
    {
        // query for blob at id, by means of Entity::Doc_multi.
        Downloader.DownloadButton_Click(
            id_doc_multi,
            this.Context
        );
    }//


}// end class


#region cantina
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
