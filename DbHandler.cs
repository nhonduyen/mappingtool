using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MappingTool
{
    public class DbHandler
    {
        public DataTable GetAllDbName(string connectionString)
        {
            var sql = "SELECT name from sys.databases where name not in('master','tempdb','model','msdb','ReportServer','ReportServerTempDB')";
            return mgrDataSQL.ExecuteReader(sql, null, connectionString);
        }
        public DataTable GetTableNames(string db, string connectionString)
        {
            var sql = "USE " + db +";  SELECT name FROM sys.Tables ";
            return mgrDataSQL.ExecuteReader(sql, null, connectionString);
        }
        public DataTable GetColumnsNames(string dbName,string table, string connectionString)
        {
            var sql = "SELECT COLUMN_NAME, DATA_TYPE FROM "+dbName+".INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'"+table+"'";
            return mgrDataSQL.ExecuteReader(sql, null, connectionString);
        }

        public string GetType(string dataType)
        {
            if (dataType.Contains("char"))
                return "string";
            if (dataType.Contains("int"))
                return "int";
            if (dataType.Contains("float"))
                return "float";
            if (dataType.Contains("double"))
                return "double";
            if (dataType.Contains("date"))
                return "DateTime";
            return "string";
        }
    }
}
