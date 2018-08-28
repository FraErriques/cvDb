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
/// AppCode::Downloader
/// </summary>
public static class Downloader
{





    ///// <summary>
    ///// on keywords research
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //public static System.Data.DataTable btnKeywords_Click(
    //    string this_txtKeywords_Text
    //    )
    //{
    //    System.Data.DataTable plausibleDocuments =
    //        Process.doc_multi.doc_multi_searchingEngines.SearchByKeywordsOnAbstracts(
    //            this_txtKeywords_Text
    //        );
    //    return plausibleDocuments;
    //}//





    public static void DownloadButton_Click(
        Int32 id_doc_multi,
        System.Web.HttpContext this_Context
        )
    {
        // query for blob at id, by means of Entity::Doc_multi.
        Entity.BusinessEntities.Doc_multi dm = new Entity.BusinessEntities.Doc_multi();
        string webServer_extractionPath = null;
        Int32 res =
            dm.FILE_from_DB_writeto_FS(
                id_doc_multi,
                out webServer_extractionPath // sul web server.
                , this_Context.Request.UserHostName
            );
        //
        // Ensure that Entity::Doc_multi produced an actual file.
        if (
            string.IsNullOrEmpty(webServer_extractionPath)
            || !System.IO.File.Exists(webServer_extractionPath)
          )
        {
            throw new HttpException(404, "doc_multi not found");
        }// else trace client credentials.
        else
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                " document extracted by client: "
                + this_Context.Request.UserHostName
                + "  "
                , 5);
        }
        //
        // portare in download dal web server al client.
        //
        // extract extension
        int extIndex = webServer_extractionPath.LastIndexOf('.');
        string extension = "doc";
        if (-1 < extIndex)
        {
            extension = webServer_extractionPath.Substring(extIndex);
        }// else let it default.
        //
        // extract filename
        int fnameIndex = webServer_extractionPath.LastIndexOf('\\');// it's a reserved char: double it.
        string filename = webServer_extractionPath.Substring(fnameIndex + 1);// exclude the last backslash.
        //
        this_Context.Response.ContentType = Downloader.setContentType(extension);
        //
        // Set the filename
        this_Context.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
        //
        // Stream the file from web_Server to the client filesystem( the client browser will show a dialog).
        this_Context.Response.WriteFile(webServer_extractionPath);
        // ready.
    }// end method download_from_webServer_to_Client.





    /// <summary>
    /// Set the content type, based on extension.
    /// </summary>
    /// <param name="extension"></param>
    /// <returns>the MIME type</returns>
    public static string setContentType(string extension)
    {
        if (
            null == extension
            || "" == extension.Trim()
            )
        {
            throw new System.Exception("invalid extension.");
        }// else continue.
        string the_MIME_type = null;
        //
        string clean_extension = extension.Trim().ToLower();
        switch (clean_extension)
        {
            case ".c":
            case ".txt":
                {
                    the_MIME_type = "text/plain";
                    break;
                }
            case ".jpg":
            case ".jpeg":
                {
                    the_MIME_type = "image/jpeg";
                    break;
                }
            case ".pdf":
                {
                    the_MIME_type = "application/pdf";
                    break;
                }
            case ".xls":
                {
                    the_MIME_type = "application/msexcel";
                    break;
                }
            case ".ppt":
            case ".pps":
                {
                    the_MIME_type = "application/powerpoint";
                    break;
                }
            case ".doc":
                {
                    the_MIME_type = "application/msword";
                    break;
                }
            //-------------------------------------------------------goto Browser ----------------
            default://-----------------------------------------------goto Browser ----------------
            case ".htm":
            case ".html":
            case ".htmls":
            case ".htx":
            case ".mht":
                {
                    the_MIME_type = "text/html";
                    break;
                }
        }// end switch
        // ready.
        return the_MIME_type;
    }//


}// end class
