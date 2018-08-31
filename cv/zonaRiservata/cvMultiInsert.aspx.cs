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


public partial class zonaRiservata_cvMultiInsert : System.Web.UI.Page
{


    private int int_ref_candidato_id;
    public struct UploadElement
    {
        public string client_path;
        public string web_server_path;
    };



    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
        object ref_candidato_id = this.Session["ref_candidato_id"];
        if (null == ref_candidato_id)
        {
            throw new System.Exception("ref_candidato_id cannot be missing, in this page.");
        }// else continue.
        try
        {
            this.int_ref_candidato_id = (int)ref_candidato_id;
        }
        catch (System.Exception ex)
        {
            string dbg = ex.Message;
            this.int_ref_candidato_id = -1;// Proxy will manage.
        }
        //
        /*
         * NB. page state check.-----------------------------------------------------------------
         * 
         */
        PageStateChecker.PageStateChecker_SERVICE(
            "zonaRiservata_cvMultiInsert"
            , this.Request
            , this.IsPostBack
            , this.Session
        );
        //----------------------------------------------- END  page state check.-----------------
        if (!this.IsPostBack)
        { }
        else
        { }
    }// end Page_Load



    #region multi_upload
    //------------------------------------------------------------------------------------------------

    protected void btnAllega_Click(object sender, EventArgs e)
    {
        string dbg = this.uploadFile.Value;
        if (null == this.Session["arlUploadPaths"])//----------TODO  dbg
        {
            this.Session["arlUploadPaths"] = new ArrayList();
        }// else already built.
        //
        if (// add to chkList only valid items
            null != dbg
            && "" != dbg
            )
        {
            this.chkMultiDoc.Items.Add(new ListItem(dbg));
            int presentCardinality = this.chkMultiDoc.Items.Count;
            this.chkMultiDoc.Items[presentCardinality - 1].Selected = true;
            // NB. the upload must be performed, before emptying the upload-html-control.
            this.allegaSingoloFile();// on current selection; i.e. a scalar item. throws on empty selection.
        }// else skip an invalid selection.
        // ready
    }//


    /// <summary>
    /// btnSubmit is used to communicate the final decision of the user:
    ///     after he reviews the check-list of the web_srv uploaded files, he finally
    ///     confirms what to send to the db_srv( i.e. the checked ones only).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDocsFromWebToDb_Click(object sender, EventArgs e)
    {
        string _abstract;
        int ref_candidato_id;
        string sourceName;
        bool result = true;// bool mask.
        //
        bool validForWriting = default(bool);// a not-valid-forWriting item does not affect the whole insertion.
        int acc = 0;
        foreach (ListItem Item in this.chkMultiDoc.Items)
        {
            if (Item.Selected)
            {
                validForWriting = this.validationForWrite(
                    out _abstract,
                    out ref_candidato_id,
                    ref acc // the accumulator is used, as index, to acces an array in Session and incremented by the calee.
                    , out sourceName
                );
                if (true == validForWriting)
                {
                    Entity.BusinessEntities.Doc_multi dm = new Entity.BusinessEntities.Doc_multi();
                    int entityDbInsertionResult =
                        dm.FILE_from_FS_insertto_DB(
                            ref_candidato_id,
                            _abstract,
                            sourceName
                        );
                    result &= (0 < entityDbInsertionResult);// each insertion must return the lastGeneratedId, which>0.
                }// else NOTvalidForWriting -> skip item.
                result &= validForWriting;// update the result, to last insertion outcome.
            }// else skip an item that has been un-checked, after uploading from client to web_srv.
        }// end foreach item that has been uploaded from client to web_srv.
        //
        //
        // ready
        if (!result)
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.DecideAndLog(
                null,// original exception type
                "Non e' stato possibile inserire il lavoro.",
                0);
            //this.pnlInteractiveControls.Enabled = true;// let the user correct errors on page.
            this.divUpload.Enabled = true;// let the user correct errors on page.
            this.lblEsito.Text = "Non e' stato possibile inserire il lavoro.";
            this.lblEsito.BackColor = System.Drawing.Color.Red;
            return;// on page
        }
        else
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.DecideAndLog(
                null,// original exception type
                "Il lavoro e' stato inserito correttamente.",
                0);
            //----navigate to "scadenziario", where the inserted phase is visible.
            this.lblEsito.Text = "";
            this.lblEsito.BackColor = System.Drawing.Color.Transparent;
            this.Session["errore"] = null;// gc
            this.Session["arlUploadPaths"] = null;// gc
            this.Session["ref_candidato_id"] = null;// gc --- NB.
            this.Response.Redirect("candidatoLoad.aspx");
        }
    }//---end submit()




    /// <summary>
    /// upload a single token( file) from an attachment-list( check-list).
    /// </summary>
    /// <param name="theFileToBeUploaded"></param>
    protected void allegaSingoloFile()
    {
        UploadElement uploadElement = new UploadElement();// TODO store
        // Get the filename_only from client_fullpath.
        string fileName_only = System.IO.Path.GetFileName(this.uploadFile.PostedFile.FileName);
        uploadElement.client_path = this.uploadFile.PostedFile.FileName;
        //
        ConfigurationLayer.ConfigurationService cs = new
            ConfigurationLayer.ConfigurationService("FileTransferTempPath/fullpath");
        string serverPath = cs.GetStringValue("path");
        //-Gestione dismessa perchè scrive in directory di sistema-- NB. it's different for every user, included ASPNETusr ---------
        //-Gestione dismessa perchè scrive in directory di sistema-string serverPath = Environment.GetEnvironmentVariable("tmp", EnvironmentVariableTarget.User);
        //-Gestione dismessa perchè va corretta a mano per ogni macchina string serverPath = @"C:\root\LogSinkFs\cv";// TODO adapt to the server file sysetm.
        // add ending part.
        serverPath += "\\upload";
        //
        // Ensure the folder exists
        if (!System.IO.Directory.Exists(serverPath))
        {
            System.IO.Directory.CreateDirectory(serverPath);
        }// else already present on the web server file system.
        // Save the file to the folder, on the web-server.
        string fullPath_onWebServer = System.IO.Path.Combine(serverPath, fileName_only);
        uploadElement.web_server_path = fullPath_onWebServer;// TODO dbg.
        if (null == this.Session["arlUploadPaths"])
        {
            throw new System.Exception("TODO call btnAllega() first!");
        }// else ok.
        ((System.Collections.ArrayList)(this.Session["arlUploadPaths"])).Add(uploadElement);
        // ready
        this.uploadFile.PostedFile.SaveAs(fullPath_onWebServer);//--NB.---crucial system call: from client do web-srv.
    }// end uploadButton_Click()




    //------------------------------------------------------------------------------------------------
    #endregion multi_upload




    private bool validationForWrite(
        //----parametri validazione---------
            out string _abstract,
            out int ref_candidato_id,
            ref int acc,// NB. "ref" and not "out" since it needs to be updated, not assigned from scratch.
            out string sourceName
        )
    {
        bool result = true;// used as bitmask with & operator.
        //
        _abstract = this.txtAbstract.Text;
        if (
            null != _abstract
            && "" != _abstract
            )
        {
            result &= true;
        }
        else
        {
            result &= false;
        }
        //
        ref_candidato_id = this.int_ref_candidato_id;
        if (
            0 < ref_candidato_id)
        {
            result &= true;
        }
        else
        {
            result &= false;
        }
        /*
         * NB. the acc index is incremented, AFTER array-access, and returned to the caller, 
         * which manages the loop.
         */
        sourceName = ((UploadElement)((System.Collections.ArrayList)(this.Session["arlUploadPaths"]))[acc++]).web_server_path; //sourceName
        if (
            null != sourceName
            && "" != sourceName
            )
        {
            result &= true;
        }
        else
        {
            result &= false;
        }
        //
        // ready
        return result;
    }// end validationForWrite.


}//
