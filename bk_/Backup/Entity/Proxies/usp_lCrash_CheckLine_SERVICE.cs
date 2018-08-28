using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace Entity.Proxies
{


    public abstract class usp_lCrash_CheckLine_SERVICE
    {


        /// <summary>
        /// manually adapted Proxy.
        /// </summary>
        /// <param name="id">user_id</param>
        /// <returns>bool: false==line_does_not_exist, true==exists.</returns>
        public static bool usp_lCrash_CheckLine(
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
            if (null == cmd.Connection)
                return false;// no conn
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_lCrash_CheckLine";
            //
            bool result = false;//false==line_does_not_exist, true==exists.
            //
            //
            System.Data.SqlClient.SqlParameter parid = new SqlParameter();
            parid.Direction = ParameterDirection.Input;
            parid.DbType = DbType.Int32;
            parid.ParameterName = "@id";
            cmd.Parameters.Add(parid);// add to command
            if (0 < id)
            {
                parid.Value = id;// checks ok -> ProxyParemeter value assigned to the SqlParameter.
            }
            else
            {
                parid.Value = System.DBNull.Value;
            }
            //
            System.Data.SqlClient.SqlParameter parRetVal = new SqlParameter();
            parRetVal.Direction = ParameterDirection.ReturnValue;
            parRetVal.DbType = DbType.Int32;
            parRetVal.ParameterName = "@Return_Value";
            cmd.Parameters.Add(parRetVal);// add to command

            //
            try
            {
                //
                int rowsWritten =// less one always.
                    cmd.ExecuteNonQuery();
                //
                int how_many_rows_with_such_id = (Int32)((cmd.Parameters["@Return_Value"]).Value);
                if (0 == how_many_rows_with_such_id)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
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
                LoggingToolsContainerNamespace.LoggingToolsContainer.DecideAndLog(
                    ex,
                    "eccezione in DataAccess::usp_lCrash_CheckLine_SERVICE : " + ex.Message,
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
            return result;// writing result is an integer.
        }// end service


    }// end class
}// end namespace
