using System;
using System.Text;



namespace Entity.BusinessEntities
{


    public static class Uploader_from_path_
    {


        /// <summary>
        /// Upload a single token( file) from an attachment-list( check-list).
        /// The document has already been uploaded from client to web_srv. 
        /// Next upload will be from web_srv to db_srv.
        /// </summary>
        /// <param name="theFileToBeUploaded"></param>
        public static bool uploadButton_Click(
            string fullPath_onWebServer,// the document has already been uploaded from client to web_srv. Next upload will be from web_srv to db_srv.
            int ref_candidato_id, // id del candidato, de cuius.
            string this_txtAbstract_Text,// the abstract
            int id_table_job // id of the row in table "job", which this doc_multi refers to.
            , ref int lastInsertedChunk// ref : the caller needs to know the docMulti_lastChunk.
          )
        {
            bool res = true;// bool mask.
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "Entity.BusinessEntities.Uploader_from_path_.uploadButton_Click: "
                + fullPath_onWebServer
                , 0);
            // Now tokenize in chunks and send them to the db-server.
            Entity.BusinessEntities.Doc_multi dm = new Entity.BusinessEntities.Doc_multi();
            lastInsertedChunk =// the only one with a not-fake abstract.
                dm.FILE_from_FS_insertto_DB(
                    ref_candidato_id,
                    this_txtAbstract_Text,
                    fullPath_onWebServer
                );
            res &= (lastInsertedChunk > 0);// >0 means a valid id.
            // ready
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "Uploaded: " + fullPath_onWebServer + " _ result = " + res.ToString(),
                5
            );
            // ready
            return res;
        }// end uploadButton_Click()


    }// end class


}// end nmsp
