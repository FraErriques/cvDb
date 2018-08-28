using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_doc_multi_INSERT_SERVICE
    {


        public static int usp_doc_multi_INSERT(
			Int32 ref_job_id,
			Int32 ref_candidato_id,
			string _abstract,
			string sourceName,
			byte[] doc,
			ref Int32 result,
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
            cmd.CommandText = "usp_doc_multi_INSERT";
            //
			int writingSucceeded = -1;// init to error:no_connection.
			//
			//
            System.Data.SqlClient.SqlParameter parref_job_id = new SqlParameter();
            parref_job_id.Direction = ParameterDirection.Input;
            parref_job_id.DbType = DbType.Int32;
            parref_job_id.ParameterName = "@ref_job_id";
			cmd.Parameters.Add( parref_job_id);// add to command
			parref_job_id.Value = ref_job_id;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			//
            System.Data.SqlClient.SqlParameter parref_candidato_id = new SqlParameter();
            parref_candidato_id.Direction = ParameterDirection.Input;
            parref_candidato_id.DbType = DbType.Int32;
            parref_candidato_id.ParameterName = "@ref_candidato_id";
			cmd.Parameters.Add( parref_candidato_id);// add to command
			parref_candidato_id.Value = ref_candidato_id;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
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
            System.Data.SqlClient.SqlParameter parsourceName = new SqlParameter();
            parsourceName.Direction = ParameterDirection.Input;
            parsourceName.DbType = DbType.String;
            parsourceName.ParameterName = "@sourceName";
			cmd.Parameters.Add( parsourceName);// add to command
			if( null!=sourceName && ""!=sourceName )
			{
				parsourceName.Value = sourceName;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				parsourceName.Value = System.DBNull.Value;
			}
			//
            System.Data.SqlClient.SqlParameter pardoc = new SqlParameter();
            pardoc.Direction = ParameterDirection.Input;
            pardoc.DbType = DbType.Binary;
            pardoc.ParameterName = "@doc";
			cmd.Parameters.Add( pardoc);// add to command
			if( null!=doc && 0!=doc.Length )
			{
				pardoc.Value = doc;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
			}
			else
			{
				pardoc.Value = System.DBNull.Value;
			}
			//
            System.Data.SqlClient.SqlParameter parresult = new SqlParameter();
            parresult.Direction = ParameterDirection.InputOutput;
            parresult.DbType = DbType.Int32;
            parresult.ParameterName = "@result";
			cmd.Parameters.Add( parresult);// add to command
			parresult.Value = result;// checks ok -> ProxyParemeter value assigned to the SqlParameter.

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
                object obj_result = cmd.Parameters["@result"].Value;
                if (null != obj_result && System.DBNull.Value != obj_result)
                    result = (Int32)(cmd.Parameters["@result"].Value);
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
                        "eccezione in DataAccess::usp_doc_multi_INSERT_SERVICE : " + ex.Message,
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
