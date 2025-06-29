using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace GTPriceImporterService
{
    public class SqlHelper
    {
        public int ConnectionCommantTimeout = 30;

        static SqlConnectionStringBuilder ConnectionBuilder = new SqlConnectionStringBuilder
        {
            DataSource = "(local)",
            InitialCatalog = "gt_rates",
            IntegratedSecurity = true
        };

        public SqlHelper(string databae = "gt_rates")
        {
            ConnectionBuilder.InitialCatalog = databae;
        }

        public async Task<DataTable> QueryAsync(string CommandText, CommandType SqlCommandType, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = ConnectionCommantTimeout;

                    cmd.Connection = cnn;
                    cmd.CommandType = SqlCommandType;
                    cmd.CommandText = CommandText;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    await cnn.OpenAsync();
                    using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                    {
                        await Task.Run(() => dt.Load(rd));
                    }
                    cnn.Close();
                }
                if (cnn.State != ConnectionState.Closed)
                {
                    cnn.Dispose();
                }
            }

            return dt;
        }

        public async Task<DataTable> TransactionQueryAsync(List<string> CommandText, List<CommandType> SqlCommandType, List<SqlParameter[]> parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
            {
                SqlTransaction transaction = null;

                try
                {
                    await cnn.OpenAsync();
                    transaction = cnn.BeginTransaction();

                    for (int i = 0; i < CommandText.Count; i++)
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandTimeout = ConnectionCommantTimeout;

                            cmd.Connection = cnn;
                            cmd.CommandType = SqlCommandType[i];
                            cmd.CommandText = CommandText[i];

                            if (parameters[i] != null)
                            {
                                cmd.Parameters.AddRange(parameters[i]);
                            }

                            using (SqlDataReader rd = await cmd.ExecuteReaderAsync())
                            {
                                await Task.Run(() => dt.Load(rd));
                            }
                            cnn.Close();
                        }
                    }

                    transaction.Commit();

                    if (cnn.State != ConnectionState.Closed)
                    {
                        cnn.Dispose();
                    }

                    return dt;

                }
                catch (System.Exception)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (System.Exception)
                    {
                    }

                    return null;
                }
            }
        }

        public async Task ExecuteAsync(string CommandText, CommandType SqlCommandType, SqlParameter[] parameters = null)
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = ConnectionCommantTimeout;

                    cmd.Connection = cnn;
                    cmd.CommandType = SqlCommandType;
                    cmd.CommandText = CommandText;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    await cnn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    cnn.Close();
                }
            }
        }

        public async Task<object> QueryScalarAsync(string CommandText, SqlParameter[] parameters = null)
        {
            using (SqlConnection cnn = new SqlConnection(ConnectionBuilder.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandTimeout = ConnectionCommantTimeout;

                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = CommandText;

                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    await cnn.OpenAsync();
                    object o = await cmd.ExecuteScalarAsync();
                    cnn.Close();

                    return o;
                }
            }
        }
    }
}