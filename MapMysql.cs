using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingTool
{
    class MapMysql
    {
        
        public string MakeInsert(string table, List<dynamic> columns)
        {
            DbHandler db = new DbHandler();
            var insert = "INSERT INTO " + table + "(";
            var tempInsert = "";
            var parameter = "";
            var parameter1 = "new { ";
            foreach (var col in columns)
            {
                var type = db.GetType(col.DATA_TYPE);

                if (string.Compare(col.COLUMN_NAME, "ID") != 0)
                {
                    parameter += type + " " + col.COLUMN_NAME + ",";
                    insert += col.COLUMN_NAME + ",";
                    tempInsert += "@" + col.COLUMN_NAME + ",";
                    parameter1 += col.COLUMN_NAME + " = " + col.COLUMN_NAME + ",";

                }
            }
            insert = insert.Remove(insert.Length - 1) + ") VALUES(" + tempInsert.Remove(tempInsert.Length - 1) + ")";

            string insertFunc = "public virtual int Insert(" + parameter.Remove(parameter.Length - 1) + ")" + System.Environment.NewLine;
            insertFunc += "        {" + System.Environment.NewLine;
            insertFunc += "            var sql = \"" + insert.Remove(insert.Length - 1) + ")\";" + System.Environment.NewLine;
            insertFunc += "            return MysqlManager<" + table + ">.Execute(sql, " + parameter1.Remove(parameter1.Length - 1) + "});" + System.Environment.NewLine;
            insertFunc += "        }" + System.Environment.NewLine;
            return insertFunc;
        }
       
        public string MakeUpdateFunc(string table, List<dynamic> columns)
        {
            DbHandler db = new DbHandler();
            var update = "UPDATE " + table + " SET ";

            var parameter1 = " new { ";
            var parameter = "";
            foreach (var col in columns)
            {
                var type = db.GetType(col.DATA_TYPE);
                parameter += type + " " + col.COLUMN_NAME + ", ";
                parameter1 += col.COLUMN_NAME + " = " + col.COLUMN_NAME + ",";
                if (string.Compare(col.COLUMN_NAME, "ID") != 0)
                {
                    update += col.COLUMN_NAME + "=@" + col.COLUMN_NAME + ",";
                }
            }
            update = update.Remove(update.Length - 1) + " WHERE ID=@ID\"";

            string UpdateFunc = "public virtual int Update(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += "            var sql = \"" + update + ";" + System.Environment.NewLine;
            UpdateFunc += System.Environment.NewLine;
            UpdateFunc += "            return MysqlManager<" + table + ">.Execute(sql, " + parameter1.Remove(parameter1.Length - 1) + "});" + System.Environment.NewLine;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }

      
        public string MakeConstructor(string table, List<dynamic> columns)
        {
            DbHandler db = new DbHandler();

            var parameter = "";
            var content = "";
            foreach (var col in columns)
            {
                var type = db.GetType(col.DATA_TYPE);
                parameter += type + " " + col.COLUMN_NAME + ", ";
                content += "            this." + col.COLUMN_NAME + " = " + col.COLUMN_NAME + ";" + System.Environment.NewLine;
            }

            string UpdateFunc = "public " + table + "(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += content;
            UpdateFunc += "        }" + System.Environment.NewLine;
            UpdateFunc += "        public " + table + "() { }" + System.Environment.NewLine;
            return UpdateFunc;
        }
    }
}
