using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_candidato_LOAD_SERVICE
    {


        public static System.Data.DataTable usp_candidato_LOAD(
			string where_tail		//
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
            cmd.CommandText = "usp_candidato_LOAD";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parwhere_tail = new SqlParameter();
            parwhere_tail.Direction = ParameterDirection.Input;
            parwhere_tail.DbType = DbType.String;
            parwhere_tail.ParameterName = "@where_tail";
			cmd.Parameters.Add( parwhere_tail);// add to command
			if( null!=where_tail && ""!=where_tail )
			{
				parwhere_tail.Value = where_tail;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parwhere_tail.Value = System.DBNull.Value;
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
					"eccezione in DataAccess::usp_candidato_LOAD_SERVICE : " + ex.Message,
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
