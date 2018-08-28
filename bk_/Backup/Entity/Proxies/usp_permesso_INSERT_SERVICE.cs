using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_permesso_INSERT_SERVICE
    {


        public static int usp_permesso_INSERT(
			Int32 id_utente,
			Int32 id_permissionLevel,
			Int32 id_settore,
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
            cmd.CommandText = "usp_permesso_INSERT";
            //
			int writingSucceeded = -1;// init to error:no_connection.
			//
			//
            System.Data.SqlClient.SqlParameter parid_utente = new SqlParameter();
            parid_utente.Direction = ParameterDirection.Input;
            parid_utente.DbType = DbType.Int32;
            parid_utente.ParameterName = "@id_utente";
			cmd.Parameters.Add( parid_utente);// add to command
			if( 0<id_utente )
			{
				parid_utente.Value = id_utente;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parid_utente.Value = System.DBNull.Value;
			}
			//
            System.Data.SqlClient.SqlParameter parid_permissionLevel = new SqlParameter();
            parid_permissionLevel.Direction = ParameterDirection.Input;
            parid_permissionLevel.DbType = DbType.Int32;
            parid_permissionLevel.ParameterName = "@id_permissionLevel";
			cmd.Parameters.Add( parid_permissionLevel);// add to command
			if( 0<id_permissionLevel )
			{
				parid_permissionLevel.Value = id_permissionLevel;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parid_permissionLevel.Value = System.DBNull.Value;
			}
			//
            System.Data.SqlClient.SqlParameter parid_settore = new SqlParameter();
            parid_settore.Direction = ParameterDirection.Input;
            parid_settore.DbType = DbType.Int32;
            parid_settore.ParameterName = "@id_settore";
			cmd.Parameters.Add( parid_settore);// add to command
			if( 0<id_settore )
			{
				parid_settore.Value = id_settore;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parid_settore.Value = System.DBNull.Value;
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
                        "eccezione in DataAccess::usp_permesso_INSERT_SERVICE : " + ex.Message,
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
