using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_utente_LOADSINGLE_SERVICE
    {


        public static System.Data.DataTable usp_utente_LOADSINGLE(
			string username		//
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
            cmd.CommandText = "usp_utente_LOADSINGLE";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parusername = new SqlParameter();
            parusername.Direction = ParameterDirection.Input;
            parusername.DbType = DbType.String;
            parusername.ParameterName = "@username";
			cmd.Parameters.Add( parusername);// add to command
			if( null!=username && ""!=username )
			{
				parusername.Value = username;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parusername.Value = System.DBNull.Value;
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
					"eccezione in DataAccess::usp_utente_LOADSINGLE_SERVICE : " + ex.Message,
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
