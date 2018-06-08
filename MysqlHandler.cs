using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace MappingTool
{
    class MysqlHandler
    {
        public List<dynamic> GetAllDbName()
        {
            var sql = "SELECT name from sys.databases where name not in('master','tempdb','model','msdb','ReportServer','ReportServerTempDB')";
            return MysqlManager<dynamic>.ExecuteDynamic(sql, null);
        }
        public List<string> GetTableNames(string db, string connectionString)
        {
            var sql = string.Format(@"SELECT table_name FROM information_schema.tables WHERE table_schema='{0}'", db);
            return MysqlManager<string>.ExecuteReader(sql, null);
        }
        public List<dynamic> GetColumnsNames(string dbName, string table, string connectionString)
        {
            var sql = string.Format(@"SELECT COLUMN_NAME,DATA_TYPE  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}';", dbName, table);
            return MysqlManager<dynamic>.ExecuteDynamic(sql, null);
        }

        public string GetType(string dataType)
        {
            if (dataType.Contains("char") || dataType.Contains("text"))
                return "string";
            if (dataType.Contains("int"))
                return "int";
            if (dataType.Contains("float"))
                return "float";
            if (dataType.Contains("double"))
                return "double";
            if (dataType.Contains("date"))
                return "DateTime";
            if (dataType.Contains("decimal"))
                return "decimal";
            return "string";
        }
    }
}
