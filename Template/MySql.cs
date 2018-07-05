using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingTool.Template
{
    public class MySql
    {
public int ID { get; set; }

        void constructor() { }

        public virtual List<MySql> Select(int ID = 0, string listcolumn = "")
        {
            var sql = "SELECT * FROM MySql";
            if (!string.IsNullOrEmpty(listcolumn)) sql = sql.Replace("*", listcolumn);
            if (ID == 0) return MysqlManager<MySql>.ExecuteReader(sql);
            sql += " WHERE ID=@ID";

            return MysqlManager<MySql>.ExecuteReader(sql, new { ID = ID });
        }

        public virtual List<MySql> SelectPaging(int start = 0, int end = 10, string query = "", string listcolumn = "")
        {
            var sql = string.Format(@"SELECT * FROM MySql WHERE 1=1 {0} LIMIT @start, @end;", query);
            if (!string.IsNullOrEmpty(listcolumn)) sql = sql.Replace("*", listcolumn);
            return MysqlManager<MySql>.ExecuteReader(sql, new { start = start, end = end });
        }

        public virtual int GetCount(string query = "")
        {
            var sql = "SELECT COUNT(1) AS CNT FROM MySql WHERE 1=1 " + query;
            var result = MysqlManager<MySql>.ExecuteScalar(sql);
            return Convert.ToInt32(result);
        }


        public virtual int UpdateColumn(int ID, string COLUMN, string VALUE)
        {
            var sql = string.Format(@"UPDATE MySql SET {0}=@VALUE WHERE ID=@ID", COLUMN);

            return MysqlManager<MySql>.Execute(sql, new { ID = ID, VALUE = VALUE });
        }

        public virtual int Delete(int ID = 0)
        {
            var sql = "DELETE FROM MySql ";
            if (ID == 0) return MysqlManager<MySql>.Execute(sql);
            sql += " WHERE ID=@ID ";
            return MysqlManager<MySql>.Execute(sql, new { ID = ID });
        }

        void insert() { }
        void update() { }
    }
}
