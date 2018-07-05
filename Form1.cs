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
            var template = "../../Template/ADO.cs";
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please choose file path");
                return;
            }
            MapADOFunction map = new MapADOFunction();
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
             
                DataTable columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = map.MakeConstructor(tbname, columns);
                var insert = map.MakeInsert(tbname, columns);
                var update = map.MakeUpdateFunc(tbname, columns);
                var property = "";
                foreach (DataRow col in columns.Rows)
                {
                    var colName = col["COLUMN_NAME"].ToString();
                    var type =db.GetType( col["DATA_TYPE"].ToString());
                    property += "        public " + type + " " + colName + " { get; set; }" + System.Environment.NewLine;
             
                }
               
                string strTemplate = filehandler.ReadFile(template); 
                strTemplate = strTemplate.Replace("MappingTool.Template", txtNamespace.Text );
                strTemplate = strTemplate.Replace("ADO",tbname );
                strTemplate = strTemplate.Replace("public string ID { get; set; }", property);
                strTemplate = strTemplate.Replace("void constructor() { }", constructor);
                strTemplate = strTemplate.Replace("void insertfunc() { }", insert);
                strTemplate = strTemplate.Replace("void updatefunc() { }", update);
               
              
                if (filehandler.FileExist(location + "\\" + tbname+".cs"))
                {
                    filehandler.DeleteFile(location + "\\" + tbname + ".cs");
                }

                try
                {
                    filehandler.WriteFile(strTemplate, location + "\\" + tbname + ".cs");
                }
                catch
                {
                    MessageBox.Show("Cannot open file");
                    return;
                }
                txtResult.Text += "Mapping " + tbname + ".cs success" + System.Environment.NewLine;
            }
            MessageBox.Show("Mapping done!");
        }

        private void btnDapper_Click(object sender, EventArgs e)
        {
            var location = lblLink.Text;
            var template ="../../Template/Dapper.cs";
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please choose file path");
                return;
            }
            MapDapper map = new MapDapper();
            FileHandler filehandler = new FileHandler();
            DbHandler db = new DbHandler();
            string connectionString = mgrDataSQL.connStr;
            if (string.Compare(txtDbName.Text.Trim(), "MBO") != 0)
                connectionString = "Data Source=" + txtIp.Text + ";Initial Catalog=" + txtDbName.Text + ";User ID=" + txtUsername.Text
                    + ";Password=" + txtPassword.Text + ";Integrated Security=False";
            DataTable tables = db.GetTableNames(txtDbName.Text, connectionString);
            foreach (DataRow row in tables.Rows)
            {
                var tbname = row["NAME"].ToString();
                DataTable columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = map.MakeConstructor(tbname, columns);
                var insert = map.MakeInsert(tbname, columns);
                var update = map.MakeUpdateFunc(tbname, columns);
                var param = "";
                foreach (DataRow col in columns.Rows)
                {
                    var colName = col["COLUMN_NAME"].ToString();
                    var type = db.GetType(col["DATA_TYPE"].ToString());
                    param += "        public " + type + " " + colName + " { get; set; }" + System.Environment.NewLine;
                }

                string strTemplate = filehandler.ReadFile(template);
                strTemplate = strTemplate.Replace("MappingTool.Template", txtNamespace.Text);
                strTemplate = strTemplate.Replace("Dapper", tbname);
                strTemplate = strTemplate.Replace("void constructor() { }", constructor);
                strTemplate = strTemplate.Replace("void insert() { }", insert);
                strTemplate = strTemplate.Replace("void update() { }", update);
                strTemplate = strTemplate.Replace("public int ID { get; set; }", param);
                if (filehandler.FileExist(location + "\\" + tbname + ".cs"))
                {
                    filehandler.DeleteFile(location + "\\" + tbname + ".cs");
                }
                try
                {
                    filehandler.WriteFile(strTemplate, location + "\\" + tbname + ".cs");
                }
                catch
                {
                    MessageBox.Show("Cannot open file");
                    return;
                }
                

                txtResult.Text += "Mapping " + tbname + ".cs success" + System.Environment.NewLine;
            }
            MessageBox.Show("Mapping done!");
        }

        private void btnMysql_Click(object sender, EventArgs e)
        {
            var location = lblLink.Text;
            var template ="../../Template/MySql.cs" ;
            if (string.IsNullOrWhiteSpace(location))
            {
                MessageBox.Show("Please choose file path");
                return;
            }
            MapMysql map = new MapMysql();
            FileHandler filehandler = new FileHandler();
            MysqlHandler db = new MysqlHandler();
            string connectionString = mgrDataSQL.mysql;
            if (string.Compare(txtDbName.Text.Trim(), "test") != 0)
                connectionString = "Server=" + txtIp.Text + ";Database=" + txtDbName.Text + ";Uid=" + txtUsername.Text
                    + ";Pwd=" + txtPassword.Text + ";";
            List<string> tables = db.GetTableNames(txtDbName.Text, connectionString);
            foreach (var tbname in tables)
            {
                string content = "public class " + tbname + "\n    {" + System.Environment.NewLine;
                var columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = map.MakeConstructor(tbname, columns);
               
                var insertFunc = map.MakeInsert(tbname, columns);
                var updateFunc = map.MakeUpdateFunc(tbname, columns);
                var param = "";
                foreach (var col in columns)
                {

                    var type = db.GetType(col.DATA_TYPE);
                    param  += "        public " + type + " " + col.COLUMN_NAME + " { get; set; }" + System.Environment.NewLine;
                }

                string strTemplate = filehandler.ReadFile(template);

                strTemplate = strTemplate.Replace("MappingTool.Template", txtNamespace.Text);
                strTemplate = strTemplate.Replace("public int ID { get; set; }", param);
                strTemplate = strTemplate.Replace("void constructor() { }", constructor );
                strTemplate = strTemplate.Replace("MySql", tbname);
                strTemplate = strTemplate.Replace("void insert() { }", insertFunc);
                strTemplate = strTemplate.Replace("void update() { }", updateFunc);
                
              
                if (filehandler.FileExist(location + "\\" + tbname + ".cs"))
                {
                    filehandler.DeleteFile(location + "\\" + tbname + ".cs");
                }
                try
                {
                    filehandler.WriteFile(strTemplate, location + "\\" + tbname + ".cs");
                }
                catch
                {
                    MessageBox.Show("Cannot open file");
                    return;
                }


                txtResult.Text += "Mapping " + tbname + ".cs success" + System.Environment.NewLine;
            }
            MessageBox.Show("Mapping done!");
        }

       
    }
}
