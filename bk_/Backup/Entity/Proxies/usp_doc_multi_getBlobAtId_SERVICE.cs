using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_getBlobAtId_SERVICE
    {


        public static System.Data.DataTable usp_doc_multi_getBlobAtId(
			Int32 id_required_phase		//
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
            cmd.CommandText = "usp_doc_multi_getBlobAtId";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parid_required_phase = new SqlParameter();
            parid_required_phase.Direction = ParameterDirection.Input;
            parid_required_phase.DbType = DbType.Int32;
            parid_required_phase.ParameterName = "@id_required_phase";
			cmd.Parameters.Add( parid_required_phase);// add to command
			parid_required_phase.Value = id_required_phase;// checks ok -> ProxyParemeter value assigned to the SqlParameter.

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
					"eccezione in DataAccess::usp_doc_multi_getBlobAtId_SERVICE : " + ex.Message,
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
