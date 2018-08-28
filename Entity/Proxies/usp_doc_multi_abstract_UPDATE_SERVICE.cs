using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_abstract_UPDATE_SERVICE
    {


        public static int usp_doc_multi_abstract_UPDATE(
			Int32 id,
			string _abstract,
			System.Data.SqlClient.SqlTransaction trx		//
		)
		{
            //
            SqlCommand cmd = new SqlCommand();
            if (null == trx)
            {
				cmd.Connection =
					DbLayer.ConnectionManager.connectWithCustomSingleXpath(
						"ProxyGeneratorConnections/strings",// compulsory xpath
						"cv_db_app"
					);
            }
            else
            {
                cmd.Connection = trx.Connection;
                cmd.Transaction = trx;
            }            
            if( null==cmd.Connection)
                return -1;// no conn
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_doc_multi_abstract_UPDATE";
            //
			int writingSucceeded = -1;// init to error:no_connection.
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
            System.Data.SqlClient.SqlParameter par_abstract = new SqlParameter();
            par_abstract.Direction = ParameterDirection.Input;
            par_abstract.DbType = DbType.String;
            par_abstract.ParameterName = "@_abstract";
			cmd.Parameters.Add( par_abstract);// add to command
			if( null!=_abstract && ""!=_abstract )
			{
				par_abstract.Value = _abstract;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				par_abstract.Value = System.DBNull.Value;
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
                        "eccezione in DataAccess::usp_doc_multi_abstract_UPDATE_SERVICE : " + ex.Message,
						0 // verbosity
                );
                //
            }// end catch
            finally
            {
				if( null == trx)
				{
					if (null != cmd.Connection)
						if (System.Data.ConnectionState.Open == cmd.Connection.State)
							cmd.Connection.Close();
                }// else preserve transaction
            }
            // ready
            return writingSucceeded;// writing result is an integer.
        }// end service


    }// end class
}// end namespace
