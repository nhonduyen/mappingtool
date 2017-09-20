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
    }
}
