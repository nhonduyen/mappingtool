using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MappingTool
{
    public class mgrDataSQL
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["cnnString"].ConnectionString;
        public static string mysql = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;

        public static DataTable ExecuteReader(string sql, Dictionary<string, object> param = null, string ConnectionString=null)
        {
            var connectionStr = connStr;
            if (!string.IsNullOrEmpty(ConnectionString)) connectionStr = ConnectionString;
            using (SqlConnection connect = new SqlConnection(connectionStr))
            {
                DataTable dtb = new DataTable();

                try
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }

                        SqlDataReader reader = command.ExecuteReader();
                        dtb.Load(reader);
                    }
                    return dtb;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }

        }
        public static int ExecuteNonQuery(string sql, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        SqlTransaction transaction;
                        transaction = connect.BeginTransaction();
                        command.Transaction = transaction;
                        command.Notification = null;
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }

                        int count = command.ExecuteNonQuery();
                        transaction.Commit();

                        return count;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }

       
        public static object ExecuteScalar(string sql, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(sql, connect))
                    {
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }
                        connect.Open();
                        object result = command.ExecuteScalar();
                        return result;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }
        // store procedure
        public static DataTable ExecuteStoreNoCacheReader(string storename, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                DataTable dtb = new DataTable();
                try
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand(storename, connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            dtb.Load(reader);
                        }
                        return dtb;

                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }

        public static DataTable ExecuteStoreReader(string storename, Dictionary<string, object> param = null)
        {
            SqlConnection connect = new SqlConnection(connStr);
            DataTable dtb = null;

            if (dtb == null)
            {
                try
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand(storename, connect);
                    command.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        foreach (var item in param)
                        {
                            command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                        }
                    }

                    SqlDataReader reader = command.ExecuteReader();
                    dtb = new DataTable();
                    dtb.Load(reader);
                    // put data in cache
                    return dtb;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
            else
            {
                //get cache
                return dtb;
            }
        }
        public static int ExecuteStoreNonQuery(string storename, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    connect.Open();
                    using (SqlCommand command = new SqlCommand(storename, connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        SqlTransaction transaction;
                        transaction = connect.BeginTransaction();
                        command.Transaction = transaction;
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }

                        int count = command.ExecuteNonQuery();
                        transaction.Commit();
                        return count;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }
        public static object ExecuteStoreScalar(string storename, Dictionary<string, object> param = null)
        {
            using (SqlConnection connect = new SqlConnection(connStr))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand(storename, connect))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                        {
                            foreach (var item in param)
                            {
                                command.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                            }
                        }
                        connect.Open();
                        object result = command.ExecuteScalar();
                        return result;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connect.Dispose();
                }
            }
        }
    }
}
