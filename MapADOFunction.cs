using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MappingTool
{
    class MapADOFunction
    {
        public string MakeInsert(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();
            var insert = "INSERT INTO " + table + "(";
            var tempInsert = "";
            var parameter = "";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            foreach (DataRow col in columns.Rows)
            {
                var colName = col["COLUMN_NAME"].ToString();
                var type = db.GetType(col["DATA_TYPE"].ToString());

                if (string.Compare(colName, "ID") != 0)
                {
                    parameter += type + " " + colName + ",";
                    insert += colName + ",";
                    tempInsert += "@" + colName + ",";
                    dictionary += "            param.Add(\"@" + colName + "\", " + colName + ");" + System.Environment.NewLine;
                }
            }
            insert = insert.Remove(insert.Length - 1) + ") VALUES(" + tempInsert.Remove(tempInsert.Length - 1) + ")";

            string insertFunc = "        public int Insert(" + parameter.Remove(parameter.Length - 1) + ")" + System.Environment.NewLine;
            insertFunc += "        {" + System.Environment.NewLine;
            insertFunc += "            var sql = \"" + insert.Remove(insert.Length - 1) + ")\";" + System.Environment.NewLine;
            insertFunc += dictionary + System.Environment.NewLine;
            insertFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine;
            insertFunc += "        }" + System.Environment.NewLine;
            return insertFunc;
        }
        public string MakeUpdateFunc(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();
            var update = "UPDATE " + table + " SET ";

            var parameter = "";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            foreach (DataRow col in columns.Rows)
            {
                var colName = col["COLUMN_NAME"].ToString();
                var type = db.GetType(col["DATA_TYPE"].ToString());
                parameter += type + " " + colName + ", ";
                if (string.Compare(colName, "ID") != 0)
                {
                    update += colName + "=@" + colName + ",";
                }
                dictionary += "            param.Add(\"@" + colName + "\", " + colName + ");" + System.Environment.NewLine;
            }
            update = update.Remove(update.Length - 1) + " WHERE ID=@ID\"";

            string UpdateFunc = "        public int Update(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += "            var sql = \"" + update + ";" + System.Environment.NewLine;
            UpdateFunc += dictionary + System.Environment.NewLine;
            UpdateFunc += System.Environment.NewLine;
            UpdateFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }
        public string MakeDeleteFunc(string table)
        {
            var del = "DELETE FROM " + table + " WHERE ID=@ID";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@ID\",ID);";
            string deleteFunc = "        public int Delete(int ID)" + System.Environment.NewLine;
            deleteFunc += "        {" + System.Environment.NewLine;
            deleteFunc += "            var sql = \"" + del + ";\";" + System.Environment.NewLine;
            deleteFunc += dictionary + System.Environment.NewLine;
            deleteFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine;
            deleteFunc += "        }" + System.Environment.NewLine;
            return deleteFunc;

        }

        public string MakeSelectFunc(string table, string id = "")
        {
            var sel = "SELECT * FROM " + table;
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@ID\",ID);";
            string selectFunc = "        public DataTable Select(int ID=0)" + System.Environment.NewLine;
            selectFunc += "        {" + System.Environment.NewLine;
            selectFunc += "            var sql = \"" + sel + " \";" + System.Environment.NewLine;
            selectFunc += "            if (ID == 0) return mgrDataSQL.ExecuteReader(sql);" + System.Environment.NewLine;
            selectFunc += "            sql +=\" WHERE ID=@ID\";" + System.Environment.NewLine;
            selectFunc += System.Environment.NewLine;
            selectFunc += dictionary + System.Environment.NewLine;
            selectFunc += "            return mgrDataSQL.ExecuteReader(sql, param);" + System.Environment.NewLine;

            selectFunc += "        }" + System.Environment.NewLine;
            return selectFunc;

        }

        public string MakeSelectPagingFunc(string table)
        {
            var sel = "SELECT * FROM(SELECT ROW_NUMBER() OVER (order by id) AS ROWNUM, * FROM " + table + ") as u  WHERE   RowNum >= @start   AND RowNum < @end ORDER BY RowNum";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@start\",start);" + System.Environment.NewLine; ;
            dictionary += "            param.Add(\"@end\",end);" + System.Environment.NewLine; ;
            string selectFunc = "        public DataTable SelectPaging(int start=0, int end=10)" + System.Environment.NewLine;
            selectFunc += "        {" + System.Environment.NewLine;
            selectFunc += "            var sql = \"" + sel + ";\";" + System.Environment.NewLine;
            selectFunc += System.Environment.NewLine;
            selectFunc += dictionary + System.Environment.NewLine;
            selectFunc += "            return mgrDataSQL.ExecuteReader(sql, param);" + System.Environment.NewLine;
            selectFunc += "        }" + System.Environment.NewLine;
            return selectFunc;

        }
        public string MakeCountFunc(string table)
        {
            var sel = "SELECT COUNT(1) AS CNT FROM " + table;
            
            string selectFunc = "        public int GetCount()" + System.Environment.NewLine;
            selectFunc += "        {" + System.Environment.NewLine;
            selectFunc += "            var sql = \"" + sel + ";\";" + System.Environment.NewLine;

            selectFunc += "            return Convert.ToInt32(mgrDataSQL.ExecuteScalar(sql));" + System.Environment.NewLine;
            selectFunc += "        }" + System.Environment.NewLine;
            return selectFunc;

        }
        public string MakeConstructor(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();

            var parameter = "";
            var content = "";
            foreach (DataRow col in columns.Rows)
            {
                var colName = col["COLUMN_NAME"].ToString();
                var type = db.GetType(col["DATA_TYPE"].ToString());
                parameter += type + " " + colName + ", ";
                content += "            this." + colName + " = " + colName + ";" + System.Environment.NewLine;
            }

            string UpdateFunc = "        public " + table + "(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += content;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }
    }
}
