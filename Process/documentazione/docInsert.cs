//using System;
//using System.Collections.Generic;
//using System.Text;


//namespace Process.documentazione
//{



//    public class docInsert : IDisposable
//    {
//        public Entity.BusinessEntities.DocumentazioneCandidato_ docs = null;






//        public bool docsCandidato_INSERTattachedDocMulti(
//            Int32 id_richiedente,
//            Int32 id_statoLavorazione,
//            Int32 passo,
//            string responsabileProposto_content,
//            string this_txtAbstract_Text,
//            int intApprovalProgressive, // TODO analogia con table::delibere_2009::column::progressivo_delibera.
//            string fullPath_onWebServer,
//            int id_table_job
//            )
//        {
//            bool result = true;// bool mask.
//            LoggingToolsContainerNamespace.LoggingToolsContainer.LogBothSinks_DbFs(
//                "Process.documentazione.dpcInsert.docsCandidato_INSERTattachedDocMulti: "
//                + fullPath_onWebServer
//                , 0);
//            //
//            if (null == this.docs)
//                throw new System.Exception("TODO never call this, without calling BEFORE the insert(); it's needed to valorize this.job_id_identity.");
//            result &=
//                docs.insertAttachedDocMulti(
//                    fullPath_onWebServer,
//                    this_txtAbstract_Text,
//                    id_table_job,
//                    id_statoLavorazione,
//                    passo,//------------------fase procedura-------
//                    responsabileProposto_content,
//                    id_richiedente,
//                    intApprovalProgressive  // TODO analogia con table::delibere_2009::column::progressivo_delibera.
//                );
//            // ready
//            return result;
//        }





//        #region IDisposable Members

//        public void Dispose()
//        {
//            lock (typeof(Process.documentazione.docInsert))
//            {
//                if (null != this.docs)
//                {
//                    docs.Dispose();// this Entity has been equiped to be IDisposable.
//                    docs = null;// gc
//                }// end if( null!= this.docs). else already null.
//            }// end critical section
//        }// end Dispose


//        #endregion




//    }// end class


//}//
