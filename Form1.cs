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
                string content = "public class " + tbname + "\n    {" + System.Environment.NewLine;
                DataTable columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = map.MakeConstructor(tbname, columns);
                var selFunc = map.MakeSelectFunc(tbname);

                var selPagingFunc = map.MakeSelectPagingFunc(tbname);
                var selCountFunc = map.MakeCountFunc(tbname);
                var insertFunc = map.MakeInsert(tbname, columns);
                var updateFunc = map.MakeUpdateFunc(tbname, columns);
                var deleteFunc = map.MakeDeleteFunc(tbname);
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
            var template = (r1.Checked) ? "../../Template/Form.cs" : "../../Template/Web.cs";
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
                string content = "public class " + tbname + "\n    {" + System.Environment.NewLine;
                DataTable columns = db.GetColumnsNames(txtDbName.Text, tbname, connectionString);

                var constructor = map.MakeConstructor(tbname, columns);
                var selFunc = map.MakeSelectFunc(tbname);

                var selPagingFunc = map.MakeSelectPagingFunc(tbname);
                var selCountFunc = map.MakeCountFunc(tbname);
                var insertFunc = map.MakeInsert(tbname, columns);
                var updateFunc = map.MakeUpdateFunc(tbname, columns);
                var deleteFunc = map.MakeDeleteFunc(tbname);

                foreach (DataRow col in columns.Rows)
                {
                    var colName = col["COLUMN_NAME"].ToString();
                    var type = db.GetType(col["DATA_TYPE"].ToString());
                    content += "        public " + type + " " + colName + " { get; set; }" + System.Environment.NewLine;
                }

                string strTemplate = filehandler.ReadFile(template);
               
                strTemplate = strTemplate.Replace("MappingTool.Template", txtNamespace.Text);
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
                    strTemplate = strTemplate.Replace("class Form{}", content);
                }
                else
                {
                    strTemplate = strTemplate.Replace("class Web{}", content);
                }

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
