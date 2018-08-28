using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_SEARCH_CandidateDocuments_SERVICE
    {


        public static System.Data.DataTable usp_doc_multi_SEARCH_CandidateDocuments(
			string id_set		//
		)
		{
            //
            SqlCommand cmd = new SqlCommand();
            cmd.Connection =
                DbLayer.ConnectionManager.connectWithCustomSingleXpath(
                    "ProxyGeneratorConnections/strings",// compulsory xpath
                    "cv_db_app"
                );
            if( null==cmd.Connection)
                return null;// no conn
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_doc_multi_SEARCH_CandidateDocuments";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parid_set = new SqlParameter();
            parid_set.Direction = ParameterDirection.Input;
            parid_set.DbType = DbType.String;
            parid_set.ParameterName = "@id_set";
			cmd.Parameters.Add( parid_set);// add to command
			if( null!=id_set && ""!=id_set )
			{
				parid_set.Value = id_set;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parid_set.Value = System.DBNull.Value;
			}

            //
            try
            {
				//
                da.Fill(resultset);
				//
				//
            }
            catch (Exception ex)
            {
				//
				//
                resultset = null;
                //
                //---------------------exception nature discrimination----------------------
                // no integer map in the return value.
                LoggingToolsContainerNamespace.LoggingToolsContainer.DecideAndLog(
					ex,
					"eccezione in DataAccess::usp_doc_multi_SEARCH_CandidateDocuments_SERVICE : " + ex.Message,
                    0 // verbosity
                );
                //
            }// end catch
            finally
            {
                if (null != cmd.Connection)
                    if (System.Data.ConnectionState.Open == cmd.Connection.State)
                        cmd.Connection.Close();
            }
            // ready
            return resultset;// one or more datatables.
        }// end service


    }// end class
}// end namespace
