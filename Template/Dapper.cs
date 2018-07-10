using System;
using System.Collections.Generic;

namespace MappingTool.Template
{
    public class Dapper
    {
        public int ID { get; set; }

        void constructor() { }

        public virtual List<Dapper> Select(int ID = 0, string listcolumn = "")
        {
            var sql = "SELECT * FROM Dapper ";
            if (!string.IsNullOrEmpty(listcolumn)) sql = sql.Replace("*", listcolumn);
            if (ID == 0) return DBManager<Dapper>.ExecuteReader(sql);
            sql += " WHERE ID=@ID";

            return DBManager<Dapper>.ExecuteReader(sql, new { ID = ID });
        }

        public virtual List<Dapper> SelectPaging(int start = 0, int end = 10, string query = "", string listcolumn = "")
        {
            var sql = "SELECT * FROM(SELECT ROW_NUMBER() OVER (order by id) AS ROWNUM, * FROM Dapper WHERE 1=1 " + query + ") as u  WHERE   RowNum BETWEEN @start AND @end ORDER BY RowNum;";
            if (!string.IsNullOrEmpty(listcolumn)) sql = sql.Replace("*", listcolumn);

            return DBManager<Dapper>.ExecuteReader(sql, new { start = start, end = end });
        }

        public virtual int GetCount(string query = "")
        {
            var sql = "SELECT COUNT(1) AS CNT FROM Dapper WHERE 1=1 " + query;
            return (int)DBManager<Dapper>.ExecuteScalar(sql);
        }
        public virtual int Update1Column(int ID, string COLUMN, string VALUE)
        {
            var sql = string.Format(@"UPDATE Dapper SET {0}=@VALUE WHERE ID=@ID", COLUMN);

            return DBManager<Dapper>.Execute(sql, new { ID = ID, VALUE = VALUE });
        }
        public virtual int Delete(int ID = 0)
        {
            var sql = "DELETE FROM Dapper ";
            if (ID == 0) return DBManager<Dapper>.Execute(sql);
            sql += " WHERE ID=@ID ";
            return DBManager<Dapper>.Execute(sql, new { ID = ID });
        }

        public void DeleteAll()
        {
            DBManager<Dapper>.Execute("TRUNCATE TABLE Dapper");
        }
        public int SpecialCount()
        {
            var sql = string.Format(@"
SELECT SUM (row_count)
FROM sys.dm_db_partition_stats
WHERE object_id=OBJECT_ID('Dapper')   
AND (index_id=0 or index_id=1);
");
            var result = DBManager<Dapper>.ExecuteScalar(sql);
            return Convert.ToInt32(result);
        }

        void insert() { }
        void update() { }
    }
}
