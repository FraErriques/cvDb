using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_getId_at_refCandidatoId_SERVICE
    {


        public static System.Data.DataTable usp_doc_multi_getId_at_refCandidatoId(
			Int32 ref_candidato_id		//
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
            cmd.CommandText = "usp_doc_multi_getId_at_refCandidatoId";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parref_candidato_id = new SqlParameter();
            parref_candidato_id.Direction = ParameterDirection.Input;
            parref_candidato_id.DbType = DbType.Int32;
            parref_candidato_id.ParameterName = "@ref_candidato_id";
			cmd.Parameters.Add( parref_candidato_id);// add to command
			if( 0<ref_candidato_id )
			{
				parref_candidato_id.Value = ref_candidato_id;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parref_candidato_id.Value = System.DBNull.Value;
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
					"eccezione in DataAccess::usp_doc_multi_getId_at_refCandidatoId_SERVICE : " + ex.Message,
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
