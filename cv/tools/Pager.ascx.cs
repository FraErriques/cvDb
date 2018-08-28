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


/// <summary>
/// Class Name: Pager
/// Description: shared user control, for the pages that show a datagrid with cache
/// 
/// Link:
///         From:
///                 ~/Presentation/*
///         To:
///                 ~/Presentation/*
/// 
/// BPL:
/// 
/// Entity:
/// 
/// Session:
///                 Cacher                      read
///                 DataChunk                   read-write
///                 lastBurnIndex               read-write
///                 RowsInCache                 read
///                 RowsInChunk                 read
/// 
/// 
/// History:
///             02/02/2007   Created           Francesco Erriques
///             .....        various changes  
///             14/03/2007   fixed a serious bug about preexisting query strings (cheched in from Sachin's machine)
///                     performed tests on preexisting query strings
///    primo
///       http://localhost:26161/Labora/Presentation/Cittadino_EsitoRicercaCittadini.aspx?parPage=1&pippo=123&pluto=asdrakan
///
///    intermedio
///       http://localhost:26161/Labora/Presentation/Cittadino_EsitoRicercaCittadini.aspx?pippo=123&parPage=1&pluto=asdrakan
///       
///    ultimo
///       http://localhost:26161/Labora/Presentation/Cittadino_EsitoRicercaCittadini.aspx?pippo=123&pluto=asdrakan&parPage=1
///
/// 
///           12/07/2010 fixed a bug about empty-query-strings, like
///           http://server/app/page.aspx
///           or
///           http://server/app/page.aspx?
///           they were not supported: now they are. The correction is marked with its date.
///           Also corrected the current page, giving it more evidence.
/// 
/// </summary>
public partial class tools_Pager : System.Web.UI.UserControl
{
    private int lastBurnIndex = -1;
    private int RowsInChunk = 20;

    public int SetRowsInChunk
    {
        get { return RowsInChunk; }
        set { RowsInChunk = value; }
    }


    public void Page_Load(object sender, EventArgs e)
    {
        object objCacher = this.Session["Cacher"];
        if (null == objCacher)
        {
            return;// component called when the cache is not yet ready
        }
        //
        string parPage = Request.Params["parPage"];
        int paginaDesiderata = 1;// default
        try
        {
            paginaDesiderata = int.Parse(parPage);
        }
        catch
        {
            paginaDesiderata = 1;// default on missing or wrong get-parameter "parPage".
        }
        this.PageIndexManager(paginaDesiderata);
        //
        string currentPage = base.Request.AppRelativeCurrentExecutionFilePath;
        string currentPageHtmlForm = this.Parent.ID;
        //
        System.Data.DataTable dt = null;
        object objDataChunk = this.Session["DataChunk"];
        if (null != objDataChunk)
        {
            dt = (System.Data.DataTable)objDataChunk;
        }
        // each container page that includes Pager.ascx must have a gridView called "grdDatiPaginati".
        System.Web.UI.WebControls.GridView grdDatiPaginati = (System.Web.UI.WebControls.GridView)Parent.FindControl("grdDatiPaginati");
        grdDatiPaginati.DataSource = dt;
        grdDatiPaginati.DataBind();
    }//




    /// <summary>
    /// Responsabile della creazione dinamica degli indici di pagina disponibili.
    /// Visualizza massimo dieci pagine, centrate in quella selezionata.
    /// Se ce ne sono meno di dieci vanno visualizzate tutte quelle disponibili, centrate
    /// in quella selezionata.
    /// Il raggiungimento di pagina va fatto mediante Session["lastBurnIndex"], indifferentemente
    /// con Cacher::GetPreviousChunk o Cacher::GetnextChunk, passando il Session["lastBurnIndex"]
    /// appropriato.
    /// </summary>
    /// <param name="paginaDesiderata">viene decisa dal chiamante, che e' Page_Load, in base
    /// al parametro "parPage", passato in Request, dallo href dell'indice selezionato.
    /// Nel caso (!IsPostBack) viene passato "1" di default, e la visualizzazione va da uno
    /// alla ultima disponibile, col vincolo di non superare l'indice dieci.
    /// </param>
    protected void PageIndexManager(int paginaDesiderata)
    {
        object objlastBurnIndex = this.Session["lastBurnIndex"];
        if (null != objlastBurnIndex)
        {
            this.lastBurnIndex = (int)objlastBurnIndex;
        }
        else
        {
            this.lastBurnIndex = -1;// default
        }
        //
        int totaleRighe = 20;
        object objtotaleRighe = this.Session["RowsInCache"];
        if (null != objtotaleRighe)
        {
            totaleRighe = (int)objtotaleRighe;
        }
        else
        {
            totaleRighe = ((Cacher)(this.Session["Cacher"])).GetRowsInCache();
        }
        //
        int pagineDisponibili =
            (int)System.Math.Ceiling((double)totaleRighe /
            (double)this.RowsInChunk);
        int pagineDisponibiliLeft = paginaDesiderata - 1;
        int pagineDisponibiliRight = pagineDisponibili - paginaDesiderata;
        int paginaMinima = (paginaDesiderata - 4 > 0) ? paginaDesiderata - 4 : 1;
        int pagineUtilizzateASinistra = paginaDesiderata - paginaMinima;
        int pagineDisponibiliADestra = 10 - (pagineUtilizzateASinistra + 1);
        int paginaMassima = (paginaDesiderata + pagineDisponibiliADestra <= pagineDisponibili) ? paginaDesiderata + pagineDisponibiliADestra : pagineDisponibili;
        //
        this.lastBurnIndex = (paginaDesiderata - 1) * this.RowsInChunk - 1;
        //
        // decorazione html.table all'interno del panel
        string currentFullPath = base.Request.AppRelativeCurrentExecutionFilePath;
        string currentPage = currentFullPath.Substring(currentFullPath.LastIndexOf('/') + 1);
        string rawUrl = base.Request.RawUrl;
        string remainingQueryString = "";
        string queryStringPreesistente = "";
        int questionMarkIndex = rawUrl.IndexOf('?', 0, rawUrl.Length);
        if (-1 == questionMarkIndex)// caso NON esistano altri get-params
        {
            queryStringPreesistente = "?";// NB. dbg 20100712
            remainingQueryString = "";
        }// end caso NON esistano altri get-params
        else// caso esistano altri get-params
        {
            remainingQueryString = rawUrl.Substring(questionMarkIndex + 1);
            int indexStartParPage = remainingQueryString.IndexOf("parPage=", 0, remainingQueryString.Length);
            int indexEndParPage = -1;
            string prefix = "";// porzione queryString precedente "parPage=xxx"
            string suffix = "";// porzione queryString successiva "parPage=xxx"
            if (-1 == indexStartParPage)
            {
                indexEndParPage = -1;
                prefix = rawUrl.Substring(questionMarkIndex + 1);
            }
            else
            {
                if (0 == indexStartParPage)
                {
                    prefix = "";
                }
                else if (0 < indexStartParPage)
                {
                    prefix = remainingQueryString.Substring(0, indexStartParPage - 1);// exclude '&' at end.
                }
                indexEndParPage = remainingQueryString.IndexOf("&", indexStartParPage);
            }

            if (-1 != indexEndParPage)
            {
                suffix = remainingQueryString.Substring(indexEndParPage);// da '&' compreso
            }
            else
            {
                suffix = "";
            }
            queryStringPreesistente = "?" + prefix + suffix;// Query String privata del parPage
            if (1 < queryStringPreesistente.Length)
                queryStringPreesistente += "&";
        }// end caso esistano altri get-params
        //
        for (int c = paginaMinima; c <= paginaMassima; c++)
        {
            System.Web.UI.WebControls.LinkButton x = new System.Web.UI.WebControls.LinkButton();
            x.ID = "pag_" + c.ToString();
            x.Text = "&nbsp;" + c.ToString() + "&nbsp;";
            x.Visible = true;
            x.Enabled = true;
            x.Attributes.Add("href", currentPage + queryStringPreesistente + "parPage=" + c.ToString());//   parametro_Get per specificare la redirect_Page
            this.pnlPageNumber.Controls.Add(x);
            //
            if (paginaDesiderata == c)
            {
                x.ForeColor = System.Drawing.Color.Yellow;
                System.Web.UI.WebControls.Unit selectedPage_fontSize = x.Font.Size.Unit;
                x.Font.Size = new FontUnit( selectedPage_fontSize.Value + 24.0);
                x.Font.Bold = true;
            }
        }// end loop of addition of dynamic link buttons
        System.Web.UI.WebControls.Label y = new System.Web.UI.WebControls.Label();
        y.Text = " (totale " + pagineDisponibili.ToString() + " pagine)";
        this.pnlPageNumber.Controls.Add(y);
        //
        Cacher cacher = null;
        try
        {
            object objCacher = this.Session["Cacher"];
            System.Data.DataTable dt = null;// default
            if (null != objCacher)
            {
                cacher = (Cacher)objCacher;
                dt =
                    cacher.GetNextChunk(
                        this.RowsInChunk, ref this.lastBurnIndex);
            }
            this.Session["lastBurnIndex"] = this.lastBurnIndex;// update
            this.Session["DataChunk"] = dt;// update
        }
        catch (System.Exception ex)
        {
            string s = ex.Message;
        }
    }// end PageIndexManager


    #region bottoni_scorrimento
    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnRight_Click(object sender, EventArgs e)
    //{
    //    Labora.Common.Cacher Cacher = null;
    //    try
    //    {
    //        Cacher = (Labora.Common.Cacher)this.Session["Cacher"];
    //        System.Data.DataTable dt =
    //            Cacher.GetNextChunk(
    //                this.RowsInChunk, ref this.lastBurnIndex);
    //        this.Session["lastBurnIndex"] = this.lastBurnIndex;// update
    //        GridViewElencoCittadini.DataSource = dt;
    //        GridViewElencoCittadini.DataBind();
    //    }
    //    catch (System.Exception ex)
    //    {
    //        string s = ex.Message;
    //    }
    //}//


    //protected void btnLeft_Click(object sender, EventArgs e)
    //{
    //    Labora.Common.Cacher Cacher = null;
    //    try
    //    {
    //        Cacher = (Labora.Common.Cacher)this.Session["Cacher"];
    //        System.Data.DataTable dt =
    //            Cacher.GetPreviousChunk(
    //                this.RowsInChunk, ref this.lastBurnIndex);
    //        this.Session["lastBurnIndex"] = this.lastBurnIndex;// update
    //        GridViewElencoCittadini.DataSource = dt;
    //        GridViewElencoCittadini.DataBind();
    //    }
    //    catch (System.Exception ex)
    //    {
    //        string s = ex.Message;
    //    }
    //}//
    #endregion bottoni_scorrimento


}//
