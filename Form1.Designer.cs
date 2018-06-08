namespace MappingTool
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtDbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.r1 = new System.Windows.Forms.RadioButton();
            this.r2 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblLink = new System.Windows.Forms.Label();
            this.btnMap = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.btnDapper = new System.Windows.Forms.Button();
            this.btnMysql = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "DB Name";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(109, 10);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(140, 20);
            this.txtIp.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 73);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // txtDbName
            // 
            this.txtDbName.Location = new System.Drawing.Point(109, 104);
            this.txtDbName.Name = "txtDbName";
            this.txtDbName.Size = new System.Drawing.Size(140, 20);
            this.txtDbName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Class Type";
            // 
            // r1
            // 
            this.r1.AutoSize = true;
            this.r1.Location = new System.Drawing.Point(6, 19);
            this.r1.Name = "r1";
            this.r1.Size = new System.Drawing.Size(48, 17);
            this.r1.TabIndex = 7;
            this.r1.TabStop = true;
            this.r1.Text = "Form";
            this.r1.UseVisualStyleBackColor = true;
            // 
            // r2
            // 
            this.r2.AutoSize = true;
            this.r2.Location = new System.Drawing.Point(60, 19);
            this.r2.Name = "r2";
            this.r2.Size = new System.Drawing.Size(48, 17);
            this.r2.TabIndex = 8;
            this.r2.TabStop = true;
            this.r2.Text = "Web";
            this.r2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.r1);
            this.groupBox1.Controls.Add(this.r2);
            this.groupBox1.Location = new System.Drawing.Point(109, 137);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 61);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Save as";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(109, 243);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblLink
            // 
            this.lblLink.AutoSize = true;
            this.lblLink.Location = new System.Drawing.Point(16, 269);
            this.lblLink.Name = "lblLink";
            this.lblLink.Size = new System.Drawing.Size(0, 13);
            this.lblLink.TabIndex = 12;
            // 
            // btnMap
            // 
            this.btnMap.Location = new System.Drawing.Point(19, 307);
            this.btnMap.Name = "btnMap";
            this.btnMap.Size = new System.Drawing.Size(75, 23);
            this.btnMap.TabIndex = 13;
            this.btnMap.Text = "Map";
            this.btnMap.UseVisualStyleBackColor = true;
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(19, 349);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(230, 55);
            this.txtResult.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(109, 44);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(140, 20);
            this.txtUsername.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(19, 208);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Namespace";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(109, 208);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(140, 20);
            this.txtNamespace.TabIndex = 18;
            // 
            // btnDapper
            // 
            this.btnDapper.Location = new System.Drawing.Point(100, 307);
            this.btnDapper.Name = "btnDapper";
            this.btnDapper.Size = new System.Drawing.Size(75, 23);
            this.btnDapper.TabIndex = 19;
            this.btnDapper.Text = "Map Dapper";
            this.btnDapper.UseVisualStyleBackColor = true;
            this.btnDapper.Click += new System.EventHandler(this.btnDapper_Click);
            // 
            // btnMysql
            // 
            this.btnMysql.Location = new System.Drawing.Point(181, 307);
            this.btnMysql.Name = "btnMysql";
            this.btnMysql.Size = new System.Drawing.Size(75, 23);
            this.btnMysql.TabIndex = 20;
            this.btnMysql.Text = "Map Mysql";
            this.btnMysql.UseVisualStyleBackColor = true;
            this.btnMysql.Click += new System.EventHandler(this.btnMysql_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 416);
            this.Controls.Add(this.btnMysql);
            this.Controls.Add(this.btnDapper);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.lblLink);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDbName);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Mapping Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtDbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton r1;
        private System.Windows.Forms.RadioButton r2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblLink;
        private System.Windows.Forms.Button btnMap;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNamespace;
        private System.Windows.Forms.Button btnDapper;
        private System.Windows.Forms.Button btnMysql;
    }
}

