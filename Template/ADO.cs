using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MappingTool.Template
{
    public class ADO
    {
public string ID { get; set; }

        public ADO() { }

        void constructor() { }

        public int Update1Column(int id, string COLUMN, string VALUE)
        {
            string sql = string.Format(@"UPDATE ADO SET {0}=@VALUE WHERE ID=@ID", COLUMN);
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", id);
            param.Add("@VALUE", VALUE);

            return mgrDataSQL.ExecuteNonQuery(sql, param);
        }
        public int Delete(int id)
        {
            string sql = "DELETE ADO WHERE ID=@ID";
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@ID", id);
            return mgrDataSQL.ExecuteNonQuery(sql, param);
        }

        public DataTable GetAll(string query = "", string listcolumn = "")
        {
            var sql = "SELECT * FROM ADO WHERE 1=1 " + query;
            if (!string.IsNullOrEmpty(listcolumn))
            {
                sql = sql.Replace("*", listcolumn);
            }
            return mgrDataSQL.ExecuteReader(sql);
        }

        public void DeleteAll()
        {
            mgrDataSQL.ExecuteNonQuery("TRUNCATE TABLE ADO");
        }

        public int GetCount(string query = "")
        {
            if (string.IsNullOrWhiteSpace(query))
                return (int)mgrDataSQL.ExecuteScalar("SELECT COUNT(1) FROM ADO WHERE 1=1");
            return (int)mgrDataSQL.ExecuteScalar("SELECT COUNT(1) FROM ADO WHERE 1=1 " + query);
        }
        public DataTable GetAllPaging(int start = 1, int end = 10, string query = "", string listcolumn = "")
        {
            string sql = "SELECT * FROM(SELECT ROW_NUMBER() OVER (order by id DESC) AS ROWNUM,* FROM ADO WHERE 1=1 " + query + ") AS V WHERE   RowNum >= @start   AND RowNum < @end ORDER BY RowNum ";
            if (!string.IsNullOrEmpty(listcolumn))
            {
                sql = sql.Replace("*", listcolumn);
            }
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@start", start);
            param.Add("@end", end);
            return mgrDataSQL.ExecuteReader(sql, param);

        }

        void insertfunc() { }
        void updatefunc() { }
    }
}
