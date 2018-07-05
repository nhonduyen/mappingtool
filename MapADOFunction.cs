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

            string insertFunc = "public virtual int Insert(" + parameter.Remove(parameter.Length - 1) + ")" + System.Environment.NewLine;
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

            string UpdateFunc = "public virtual int Update(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += "            var sql = \"" + update + ";" + System.Environment.NewLine;
            UpdateFunc += dictionary + System.Environment.NewLine;
            UpdateFunc += System.Environment.NewLine;
            UpdateFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
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

            string UpdateFunc = "public " + table + "(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += content;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }
    }
}
