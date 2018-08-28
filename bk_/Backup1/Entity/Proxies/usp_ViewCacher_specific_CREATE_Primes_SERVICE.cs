using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_ViewCacher_specific_CREATE_Primes_SERVICE
    {


        public static int usp_ViewCacher_specific_CREATE_Primes(
			string where_tail,
			string view_signature		//
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
                return -1;// no conn
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_ViewCacher_specific_CREATE_Primes";
            //
			int writingSucceeded = -1;// init to error:no_connection.
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
            System.Data.SqlClient.SqlParameter parview_signature = new SqlParameter();
            parview_signature.Direction = ParameterDirection.Input;
            parview_signature.DbType = DbType.String;
            parview_signature.ParameterName = "@view_signature";
			cmd.Parameters.Add( parview_signature);// add to command
			if( null!=view_signature && ""!=view_signature )
			{
				parview_signature.Value = view_signature;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parview_signature.Value = System.DBNull.Value;
			}

            //
            try
            {
				//
                int rowsWritten =
                    cmd.ExecuteNonQuery();
                //
                if (1 <= rowsWritten )
                    writingSucceeded = 0;// rows written ok
                else
                    writingSucceeded = 4;// errore logico senza exception
				//
				//
            }
            catch (Exception ex)
            {
				//
				//
				/// <returns>
				/// -1  no connection
				/// 0   ok
				/// 1   sqlException chiave duplicata
				/// 2   sqlException diversa da chiave duplicata
				/// 3   eccezione NON sql
				/// 4   errore logico senza Exception
				/// ...
				/// >4  altre eccezioni TODO:dettagliare in fututo
				/// 
				/// </returns>
                //
                //---------------------exception nature discrimination----------------------
                writingSucceeded =
                    LoggingToolsContainerNamespace.LoggingToolsContainer.DecideAndLog(
                        ex,
                        "eccezione in DataAccess::usp_ViewCacher_specific_CREATE_Primes_SERVICE : " + ex.Message,
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
            return writingSucceeded;// writing result is an integer.
        }// end service


    }// end class
}// end namespace
