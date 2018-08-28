using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


/// <summary>
/// NB.  put first chunk in Session, on ViewCreation!! -------------NB.----------
/// 
/// - CacherDbView creates a dbView onConstruction 
/// - queries on it, in a row-range
/// - deletes it onDestruction
/// </summary>
public class CacherDbView
{
    private string ViewName;// name in the db.
    private int rowCardinality = default(int);// in ALL the VIEW.
    public int RowsInChunk;// in a single Page.
    private string where_tail = null;// cached, for re-building on View-refresh.
    //
    // follows a pointer to function:
    // private void prepareLavagnaDynamicPortion(), when needed, must be in the Page, which contains GridView and Pager.
    public delegate void DynamicPortionPtr();// 
    //
    // follows a pointer to function:
    // the appropriate Proxy, which builds the appropriate View.
        //public static int usp_ViewCacher_specific_CREATE_documento(
        //    string where_tail,
        //    string view_signature		//
        //)
    public delegate int SpecificViewBuilder(
            string where_tail,      // the specific join logic.
            string view_signature	// the View_name, generally the SessionId.
        );
    private SpecificViewBuilder specificViewBuilder = null;// cached, for re-building on View-refresh.
    




    /// <summary>
    /// Ctor()
    /// - CacherDbView creates a dbView onConstruction 
    /// NB. crucial for the Ctor() success is the call to this.GetChunk(firstPage)
    /// this.GetChunk() sets this.Session[CacherDbView] and this is the condition to
    /// let the dbView available; otherwise every call to 
    /// </summary>
    public CacherDbView(
        //---first the Cacher needs:
        System.Web.SessionState.HttpSessionState Session
        , string where_tail
        , string view_signature
        , SpecificViewBuilder specificViewBuilder
        , int RowsInChunk
        //---below follows the Pager need:
        , System.Web.HttpRequest Request
        , System.Web.UI.WebControls.GridView grdDatiPaginati
        , System.Web.UI.WebControls.Panel pnlPageNumber
      )
    {
        if (
            null == view_signature
            || "" == view_signature.Trim()
            || char.IsDigit(view_signature[0])
            )
        {
            throw new System.Exception("CacherDbView::Ctor: illegal view name.");
        }// else can continue.
        this.ViewName = view_signature;
        //
        try
        {
            this.doDestruction();// NB. be sure to drop omonimous objects, before a new fill( eg. for a different DataGrid in the same Session).
            //
            this.specificViewBuilder = specificViewBuilder;
            this.specificViewBuilder(where_tail, this.ViewName);
            //
            this.where_tail = where_tail;
            //
            if (RowsInChunk > 0)
            {
                this.RowsInChunk = RowsInChunk;
            }
            else
            {
                this.RowsInChunk = 5;// default.
            }
            //
            System.Data.DataTable dtViewRowCardinality =
                Entity.Proxies.usp_ViewCacher_generic_LOAD_length_SERVICE.usp_ViewCacher_generic_LOAD_length(
                    view_signature);
            if (
                null == dtViewRowCardinality
                || 0 >= dtViewRowCardinality.Rows.Count
              )
            {
                LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                    " exception :  CacherDbView Ctor failed querying the VIEW row-cardinality. check for View existence. "
                    , 0);
            }// else continue
            this.rowCardinality = (int)(dtViewRowCardinality.Rows[0].ItemArray[0]);
            //
            //---done with the Cacher, now do the Pager.
            // now fill the first page, after the View creation.
            this.Pager_EntryPoint( //    --- TODO debug 
                Session
                , Request
                , grdDatiPaginati
                , pnlPageNumber
            );
        }
        catch (System.Exception ex)// the provided DataTable might be null;
        {
            // Logging
            string m = ex.Message;
            throw new System.Exception(" CacherDbView Ctor failed creating the VIEW. ");
        }
    }// end Ctor



    /// <summary>
    /// in ALL the VIEW.
    /// </summary>
    /// <returns></returns>
    public int GetRowsInCache()
    {
        return this.rowCardinality;
    }
    
    

    /// <summary>
    /// query a chunk. NB: only PageIndexManager() can call it.
    /// </summary>
    /// <param name="Session">needed to set Session["CacherDbView"]</param>
    /// <param name="requiredPage">
    /// viene tradotto in due valori della chiave primaria della View,
    /// fra i quali viene fatto un "between", per estrarre la pagina.
    /// </param>
    /// <returns> the chunk-datatable </returns>
    private System.Data.DataTable GetChunk(
        System.Web.SessionState.HttpSessionState Session,
        int requiredPage
      )
    {
        int RowNumber_min = this.RowsInChunk * (requiredPage - 1) + 1;
        int RowNumber_max = this.RowsInChunk * requiredPage;
        //
        System.Data.DataTable tblCurrentPageData =// chunk-table
            Entity.Proxies.usp_ViewCacher_generic_LOAD_interval_SERVICE.usp_ViewCacher_generic_LOAD_interval(
                RowNumber_min
                , RowNumber_max 
                , this.ViewName
            );
        //
        if (
            null != tblCurrentPageData
            // && 0 < tblCurrentPageData.Rows.Count TODO test
          )
        {
            Session["CacherDbView"] = this;// TODO test
        }
        else
        {
            Session["CacherDbView"] = null; // TODO View dropped.
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "CacherDbView::GetChunk( "
                + " Session.SessionID = " + Session.SessionID
                + " requiredPage = " + requiredPage.ToString()
                + "   failed to retrieve the required page. TODO debug. "
                , 0);
        }
        // ready
        return tblCurrentPageData;
    }// end GetNextChunk



    #region Pager

    /// <summary>
    /// 
    /// NB. EntryPoint : every paging operation enters here.
    /// Request.Params["parPage"] e' il parametro che governa la selezione di pagina.
    /// E' un parametro http-get, contenuto nella url.
    /// Se il Ctor() ha fallito, il metodo abortisce. La verifica di successo del Ctor() e':
    /// null != Session["CacherDbView"]
    /// 
    /// </summary>
    public void Pager_EntryPoint(
        System.Web.SessionState.HttpSessionState Session
        , System.Web.HttpRequest Request
        , System.Web.UI.WebControls.GridView grdDatiPaginati
        , System.Web.UI.WebControls.Panel pnlPageNumber
      )
    {
        object objCacherDbView = Session["CacherDbView"];
        if (null == objCacherDbView)
        {
            return;// component called when the cache is not yet ready -> return on empty page.
        }// else bring to DataBind().
        //
        string parPage = Request.Params["parPage"];
        int paginaDesiderata = 1;// default
        try
        {
            paginaDesiderata = int.Parse(parPage);
            if (
                this.rowCardinality < this.RowsInChunk // caso di una sola pagina ed incompleta
                || System.Math.Ceiling((double)this.rowCardinality / (double)this.RowsInChunk) < (double)paginaDesiderata // pagine disponibili non arrivano al numero di pagina richiesto: NB. double needed to do not loose the last page_fraction.
              )
            {
                paginaDesiderata = 1;// back to default.
            }// else paginaDesiderata is possible.
        }
        catch
        {
            paginaDesiderata = 1;// default on missing or wrong get-parameter "parPage".
        }
        System.Data.DataTable dt = null;
        this.PageIndexManager(//----NB. internal call, for querying required-page data-----------------------------------------------------------
            Session
            , Request
            , paginaDesiderata
            , pnlPageNumber
            , out dt // the chunk-data, to be filled.
        );
        grdDatiPaginati.DataSource = dt;
        grdDatiPaginati.DataBind();
        //
        if (null != Session["DynamicPortionPtr"])// NB. tested: indispensable to renew it in Session, at every round-trip.
        {
            CacherDbView.DynamicPortionPtr dynamicPortionPtr =
                (CacherDbView.DynamicPortionPtr)(Session["DynamicPortionPtr"]);
            dynamicPortionPtr();// finally, call it.
        }// else nothing to be executed post-binding.
    }//




    /// <summary>
    /// 
    /// Responsabile della creazione dinamica degli indici di pagina disponibili.
    /// Visualizza massimo dieci pagine, centrate in quella selezionata.
    /// Se ce ne sono meno di dieci vanno visualizzate tutte quelle disponibili, centrate
    /// in quella selezionata.
    /// </summary>
    /// 
    /// <param name="paginaDesiderata">viene decisa dal chiamante, che e' this.Pager_EntryPoint, in base
    /// al parametro "parPage", passato in Request, dallo href dell'indice selezionato.
    /// In ogni caso in cui manchi il parametro parPage nella url la paginaDesiderata viene portata
    /// ad "1" di default, e la visualizzazione va da uno alla ultima disponibile,
    /// col vincolo di non superare l'indice dieci.
    /// </param>
    /// 
    private void PageIndexManager(
        System.Web.SessionState.HttpSessionState Session
        , System.Web.HttpRequest Request
        , int paginaDesiderata
        , System.Web.UI.WebControls.Panel pnlPageNumber
        , out System.Data.DataTable dt
      )
    {
        int totaleRighe = this.rowCardinality;// tested.
        //----range check-------
        if (0 >= this.RowsInChunk)
        {
            throw new System.Exception(" non-positive number of rowsPerPage required. ");
        }// can continue.
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
        // decorazione html.table all'interno del panel
        string currentFullPath = Request.AppRelativeCurrentExecutionFilePath;
        string currentPage = currentFullPath.Substring(currentFullPath.LastIndexOf('/') + 1);
        string rawUrl = Request.RawUrl;
        LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
            "App_Code::CacherDbView::PageIndexManager: debug QueryStringParser. "
            + " whole_Query_string = " + rawUrl
            , 0);
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
            {
                queryStringPreesistente += "&";
            }// else nothing to add.
        }// end caso esistano altri get-params
        //
        // clean the dynamic-html panel-content, before re-filling.
        int dbg_controlsInDynamicPanel = pnlPageNumber.Controls.Count;
        pnlPageNumber.Controls.Clear();
        //
        for (int c = paginaMinima; c <= paginaMassima; c++)
        {
            System.Web.UI.WebControls.LinkButton x = new System.Web.UI.WebControls.LinkButton();
            x.ID = "pag_" + c.ToString();
            x.Text = "&nbsp;" + c.ToString() + "&nbsp;";
            x.Visible = true;
            x.Enabled = true;
            x.Attributes.Add("href", currentPage + queryStringPreesistente + "parPage=" + c.ToString());//   parametro_Get per specificare la redirect_Page
            //
            if (paginaDesiderata == c)
            {
                x.ForeColor = System.Drawing.Color.Yellow;
                System.Web.UI.WebControls.Unit selectedPage_fontSize = x.Font.Size.Unit;
                x.Font.Size = new FontUnit( /*selectedPage_fontSize.Value + */ 24.0);
                x.Font.Bold = true;
            }
            //
            pnlPageNumber.Controls.Add(x);
        }// end loop of addition of dynamic link buttons
        System.Web.UI.WebControls.Label y = new System.Web.UI.WebControls.Label();
        y.Text = " (totale " + pagineDisponibili.ToString() + " pagine)";
        pnlPageNumber.Controls.Add(y);
        //-----------------------------------------END dynamic html-------------------------------------
        //
        dt = // out par------------------------------
            this.GetChunk(
                Session
                , paginaDesiderata
            );
    }// end PageIndexManager

    #endregion Pager



    ~CacherDbView()
    {// NB. don't do this. The CacherDbView class is istantiated by the Page classes, which get destructed
        // when the WebSrv wants. So, every istance handled by a Page_instance gets destructed at impredictable
        // moments. By several tests, it results that such a destructor lets the View absent on the db,
        // unexpectedly. It's good to rather call the doDestruction() method, manually, from Timbro.ascx.cs.
        //
        // DON'T     this.doDestruction();
    }// end Ctor


    private void doDestruction()
    {
        // NB. always drop the omonimous view, before re-creating. Otherwise:
        //    Msg 2714, Level 16, State 3, Procedure tt, Line 3
        //    There is already an object named 'tt' in the database.
        // no matter the int_result; it's !=0, also onSuccess.
        Entity.Proxies.usp_ViewCacher_generic_DROP_SERVICE.usp_ViewCacher_generic_DROP(
            this.ViewName
        );
        // ready.
    }// end doDestruction


}// end class
