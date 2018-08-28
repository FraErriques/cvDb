using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_dataMining_SERVICE
    {


        public static System.Data.DataSet usp_doc_multi_dataMining(
			Int32 id_last_phase		//
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
            cmd.CommandText = "usp_doc_multi_dataMining";
            //
            System.Data.DataSet resultset = new System.Data.DataSet("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parid_last_phase = new SqlParameter();
            parid_last_phase.Direction = ParameterDirection.Input;
            parid_last_phase.DbType = DbType.Int32;
            parid_last_phase.ParameterName = "@id_last_phase";
			cmd.Parameters.Add( parid_last_phase);// add to command
			parid_last_phase.Value = id_last_phase;// checks ok -> ProxyParemeter value assigned to the SqlParameter.

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
					"eccezione in DataAccess::usp_doc_multi_dataMining_SERVICE : " + ex.Message,
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
