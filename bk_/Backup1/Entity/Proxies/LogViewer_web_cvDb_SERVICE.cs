using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class LogViewer_web_cvDb_SERVICE
    {


        public static System.Data.DataTable LogViewer_web_cvDb(
			string startDate,
			string endDate		//
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
            cmd.CommandText = "LogViewer_web_cvDb";
            //
            System.Data.DataTable resultset = new System.Data.DataTable("ResultSet");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;// bind adapter to the command.
			//
			//
            System.Data.SqlClient.SqlParameter parstartDate = new SqlParameter();
            parstartDate.Direction = ParameterDirection.Input;
            parstartDate.DbType = DbType.String;
            parstartDate.ParameterName = "@startDate";
			cmd.Parameters.Add( parstartDate);// add to command
			if( null!=startDate && ""!=startDate )
			{
				parstartDate.Value = startDate;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parstartDate.Value = System.DBNull.Value;
			}
			//
            System.Data.SqlClient.SqlParameter parendDate = new SqlParameter();
            parendDate.Direction = ParameterDirection.Input;
            parendDate.DbType = DbType.String;
            parendDate.ParameterName = "@endDate";
			cmd.Parameters.Add( parendDate);// add to command
			if( null!=endDate && ""!=endDate )
			{
				parendDate.Value = endDate;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parendDate.Value = System.DBNull.Value;
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
					"eccezione in DataAccess::LogViewer_web_cvDb_SERVICE : " + ex.Message,
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
