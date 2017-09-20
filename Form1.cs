using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MappingTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mgrDataSQL.connStr))
            {
                txtIp.Text = "172.25.215.17";
                txtDbName.Text = "MBO";
                txtUsername.Text = "sa";
                txtPassword.Text = "pvst123@";
            }
            r1.Checked = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (!string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                lblLink.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            var location = lblLink.Text;
            var template = (r1.Checked) ?  "../../Template/Form.cs" : "../../Template/Web.cs";
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please choose file path");
                return;
            }
           
            FileHandler filehandler = new FileHandler();
            DbHandler db = new DbHandler();
            string connectionString = mgrDataSQL.connStr;
            if (string.Compare(txtDbName.Text.Trim(), "MBO") != 0)
                connectionString = "Data Source="+txtIp.Text+";Initial Catalog="+txtDbName.Text+";User ID="+txtUsername.Text
                    +";Password="+txtPassword.Text+";Integrated Security=False";
            DataTable tables = db.GetTableNames(txtDbName.Text, connectionString);
            foreach (DataRow row in tables.Rows)
            {
                var tbname = row["NAME"].ToString();
                string content = "public class " + tbname + "\n    {" + System.Environment.NewLine;
                DataTable columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = MakeConstructor(tbname, columns);
                var selFunc= MakeSelectFunc(tbname);
               
                var selPagingFunc= MakeSelectPagingFunc(tbname);
                var selCountFunc= MakeCountFunc(tbname);
                var insertFunc = MakeInsert(tbname, columns);
                var updateFunc = MakeUpdateFunc(tbname, columns);
                var deleteFunc = MakeDeleteFunc(tbname);
                foreach (DataRow col in columns.Rows)
                {
                    var colName = col["COLUMN_NAME"].ToString();
                    var type =db.GetType( col["DATA_TYPE"].ToString());
                    content += "        public " + type + " " + colName + " { get; set; }" + System.Environment.NewLine;
                   
                   
                   
                }
               
                string strTemplate = filehandler.ReadFile(template);
                strTemplate = strTemplate.Replace("MappingTool.Template", txtNamespace.Text );
                content += System.Environment.NewLine;

                content += constructor + System.Environment.NewLine;
                content += selFunc + System.Environment.NewLine;
                
                content += selPagingFunc + System.Environment.NewLine;
                content += selCountFunc + System.Environment.NewLine;
                content += insertFunc + System.Environment.NewLine;
                content += updateFunc + System.Environment.NewLine;
                content += deleteFunc + System.Environment.NewLine;
                content += "\r\n    }" + System.Environment.NewLine;
               
                if (r1.Checked)
                {
                    strTemplate = strTemplate.Replace("class Form{}", content );
                }
                else
                {
                    strTemplate = strTemplate.Replace("class Web{}", content );
                }
              
                if (filehandler.FileExist(location + "\\" + tbname+".cs"))
                {
                    filehandler.DeleteFile(location + "\\" + tbname + ".cs");
                }
                filehandler.WriteFile(strTemplate, location + "\\" + tbname + ".cs");

                txtResult.Text += "Mapping " + tbname + ".cs success" + System.Environment.NewLine;
            }
            MessageBox.Show("Mapping done!");
        }

        private string MakeInsert(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();
            var insert = "INSERT INTO "+table+"(";
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
            insertFunc += "            var sql = \"" + insert.Remove(insert.Length-1) + ")\";" + System.Environment.NewLine;
            insertFunc += dictionary + System.Environment.NewLine;
            insertFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine; 
            insertFunc += "        }" + System.Environment.NewLine;
            return insertFunc;
        }
        private string MakeUpdateFunc(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();
            var update = "UPDATE " + table +" SET ";
           
            var parameter = "";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            foreach (DataRow col in columns.Rows)
            {
                var colName = col["COLUMN_NAME"].ToString();
                var type = db.GetType(col["DATA_TYPE"].ToString());
                parameter += type + " " + colName + ", ";
                if (string.Compare(colName, "ID") != 0)
                {
                    update += colName + "=@"+colName+",";
                }
                dictionary += "            param.Add(\"@" + colName + "\", " + colName + ");" + System.Environment.NewLine;
            }
            update = update.Remove(update.Length - 1) + " WHERE ID=@ID\"";

            string UpdateFunc = "        public int Update(" + parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += "            var sql = \"" + update + ";"  + System.Environment.NewLine;
            UpdateFunc += dictionary + System.Environment.NewLine;
            UpdateFunc += System.Environment.NewLine;
            UpdateFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine; 
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }
        private string MakeDeleteFunc(string table)
        {
            var del = "DELETE FROM " + table + " WHERE ID=@ID";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@ID\",ID);";
            string deleteFunc = "        public int Delete(int ID)" + System.Environment.NewLine;
            deleteFunc += "        {" + System.Environment.NewLine;
            deleteFunc += "            var sql = \"" + del + ";\";"+System.Environment.NewLine;
            deleteFunc += dictionary + System.Environment.NewLine;
            deleteFunc += "            return mgrDataSQL.ExecuteNonQuery(sql, param);" + System.Environment.NewLine; 
            deleteFunc += "        }" + System.Environment.NewLine;
            return deleteFunc;
          
        }

        private string MakeSelectFunc(string table, string id="")
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

        private string MakeSelectPagingFunc(string table)
        {
            var sel = "SELECT * FROM(SELECT ROW_NUMBER() OVER (order by id) AS ROWNUM, * FROM "+table+") as u  WHERE   RowNum >= @start   AND RowNum < @end ORDER BY RowNum";
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@start\",start);"+ System.Environment.NewLine;;
            dictionary += "            param.Add(\"@end\",end);" + System.Environment.NewLine; ;
            string selectFunc = "        public DataTable SelectPaging(int start=0, int end=0)" + System.Environment.NewLine;
            selectFunc += "        {" + System.Environment.NewLine;
            selectFunc += "            var sql = \"" + sel + ";\";" + System.Environment.NewLine;
            selectFunc += System.Environment.NewLine;
            selectFunc += dictionary + System.Environment.NewLine;
            selectFunc += "            return mgrDataSQL.ExecuteReader(sql, param);" + System.Environment.NewLine;
            selectFunc += "        }" + System.Environment.NewLine;
            return selectFunc;

        }
        private string MakeCountFunc(string table)
        {
            var sel = "SELECT COUNT(1) AS CNT FROM " + table;
            var dictionary = "            Dictionary<string, object> param = new Dictionary<string, object>();" + System.Environment.NewLine;
            dictionary += "            param.Add(\"@start\",start);" + System.Environment.NewLine; ;
            dictionary += "            param.Add(\"@end\",end);" + System.Environment.NewLine; ;
            string selectFunc = "        public int GetCount()" + System.Environment.NewLine;
            selectFunc += "        {" + System.Environment.NewLine;
            selectFunc += "            var sql = \"" + sel + ";\";" + System.Environment.NewLine;

            selectFunc += "            return Convert.ToInt32(mgrDataSQL.ExecuteScalar(sql));" + System.Environment.NewLine;
            selectFunc += "        }" + System.Environment.NewLine;
            return selectFunc;

        }
        private string MakeConstructor(string table, DataTable columns)
        {
            DbHandler db = new DbHandler();
           
            var parameter = "";
            var content = "";
            foreach (DataRow col in columns.Rows)
            {
                var colName = col["COLUMN_NAME"].ToString();
                var type = db.GetType(col["DATA_TYPE"].ToString());
                parameter += type + " " + colName + ", ";
                content += "            this."+colName+" = "+colName+";"+System.Environment.NewLine;
            }
          
            string UpdateFunc = "        public "+table +"("+ parameter.Remove(parameter.Length - 2) + ")" + System.Environment.NewLine;
            UpdateFunc += "        {" + System.Environment.NewLine;
            UpdateFunc += content;
            UpdateFunc += "        }" + System.Environment.NewLine;
            return UpdateFunc;
        }
    }
}
