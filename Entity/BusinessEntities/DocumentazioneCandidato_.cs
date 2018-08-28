using System;
using System.Collections.Generic;
using System.Text;


namespace Entity.BusinessEntities
{


    public class DocumentazioneCandidato_ : IDisposable
    {


        public bool insertAttachedDocMulti(
            object fullPath_onWebServer,
            object this_txtAbstract_Text,
            object id_table_job,
            object id_statoLavorazione,
            object passo,//------------------fase procedura-------
            object responsabileProposto_content,
            object id_richiedente,
            object intApprovalProgressive  // TODO analogia con table::delibere_2009::column::progressivo_delibera.
          )
        {
            bool result = true;// bool mask.
            //
            int ref_i = default(int);
            result &=
                Entity.BusinessEntities.Uploader_from_path_.uploadButton_Click(
                    "fullPath_onWebServer",
                    (int)id_richiedente,
                    "this_txtAbstract_Text",
                    (int)id_table_job
                    , ref ref_i
                );
            return true;
        }






        // alzata la chiamata in Process
        // NB never call this, without calling BEFORE the "insert()"; it's needed to valorize this.job_id_identity.
        // per avvenuto scorporo per chiamata in "foreach allegato".
        public bool insertAttachedDocMulti(
            string fullPath_onWebServer,
            string this_txtAbstract_Text,
            int id_table_job,
            Int32 id_statoLavorazione,
            Int32 passo,
            string responsabileProposto_content,
            Int32 id_richiedente,
            int intApprovalProgressive
            )
        {
            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
                "Entity.BusinessEntities.ProcedureTypes.A_Organizzazione_Societaria.AVA_01_Affidamenti_.insertAttachedDocMulti: "
                + fullPath_onWebServer
                , 0);
            //if (0 == this.job_id_identity)
            //{
            //    throw new System.Exception("TODO never call this, without calling BEFORE the insert(); it's needed to valorize this.job_id_identity.");
            //}// else continue.
            bool result = true;// used as a bool_mask.
            int docMulti_lastChunk = -1;//init to invalid.
            //
            result &=
                Entity.BusinessEntities.Uploader_from_path_.uploadButton_Click(
                    fullPath_onWebServer,
                    id_richiedente,
                    this_txtAbstract_Text,
                    id_table_job
                    , ref docMulti_lastChunk
                );
            // NB. iff passo==5 and id_statoLavorazione==4 -> [affidamenti_current_year] insert.
            //if (
            //    5 == passo                  // fase APPROVAZIONE-RIGETTO per AVA
            //    && 4 == id_statoLavorazione // stato "APPROVATA"
            //    )
            //{
            //    string strRichiedente = "invalid";// init to invalid
            //    string strIncaricato = "invalid";// init to invalid
            //    try
            //    {
            //        System.Data.DataTable tblRichiedente =
            //            Entity.Proxies.usp_utente_LOADDECODEDSINGLE_SERVICE.usp_utente_LOADDECODEDSINGLE(
            //                id_richiedente);
            //        strRichiedente = ((string)(tblRichiedente.Rows[0].ItemArray[0]));
            //        System.Data.DataTable tblIncaricato =
            //            Entity.Proxies.usp_utente_LOADDECODEDSINGLE_SERVICE.usp_utente_LOADDECODEDSINGLE(
            //                int.Parse(responsabileProposto_content));
            //        strIncaricato = ((string)(tblIncaricato.Rows[0].ItemArray[0]));
            //    }
            //    catch (System.Exception ex)
            //    {
            //        string dbg = ex.StackTrace + "  " + ex.Message;
            //        strRichiedente = null;// Proxies will send DbNull.
            //        strIncaricato = null;
            //    }
            //    //
            //    int res_affidamenti_current_year_INSERT =
            //        Entity.Proxies.usp_affidamenti_current_yearApprove_INSERT_SERVICE.usp_affidamenti_current_yearApprove_INSERT(
            //            DateTime.Now,
            //            this.job_id_identity,// NB verificato: e' compatibile solo col lastPhase.
            //            docMulti_lastChunk,
            //            intApprovalProgressive, // progressivo_AVA
            //            strRichiedente,
            //            strIncaricato
            //        );
            //    result &= (0 == res_affidamenti_current_year_INSERT);
            //}// else not in approval phase.
            // ready
            return result;
        }// end insertAttachedDocMulti()






        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }


}//
