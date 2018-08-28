using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_candidato_note_LOAD_SERVICE
    {


        public static System.Data.DataTable usp_candidato_note_LOAD(
			Int32 id		//
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
            cmd.CommandText = "usp_candidato_note_LOAD";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parid = new SqlParameter();
            parid.Direction = ParameterDirection.Input;
            parid.DbType = DbType.Int32;
            parid.ParameterName = "@id";
			cmd.Parameters.Add( parid);// add to command
			if( 0<id )
			{
				parid.Value = id;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parid.Value = System.DBNull.Value;
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
					"eccezione in DataAccess::usp_candidato_note_LOAD_SERVICE : " + ex.Message,
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
