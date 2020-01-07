namespace SmsMon
{
    partial class Admin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin));
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserLogin = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnMonitor = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelUser = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.tblPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblOrguser = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.txtOrganztn = new System.Windows.Forms.TextBox();
            this.btnSaveLocation = new System.Windows.Forms.Button();
            this.btnNewLoc = new System.Windows.Forms.Button();
            this.btnNewCustomer = new System.Windows.Forms.Button();
            this.btnSaveCustomer = new System.Windows.Forms.Button();
            this.comboLoc = new System.Windows.Forms.ComboBox();
            this.comboOrg = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtGenNumber = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtPwdAssigned = new System.Windows.Forms.TextBox();
            this.txtUserAssigned = new System.Windows.Forms.TextBox();
            this.chkGenActive = new System.Windows.Forms.CheckBox();
            this.txtGenID = new System.Windows.Forms.TextBox();
            this.txtGenType = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.txtBuildingNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCustId = new System.Windows.Forms.Label();
            this.process1 = new System.Diagnostics.Process();
            this.btnPollGens = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.timerSummary = new System.Windows.Forms.Timer(this.components);
            this.MonitorDataSet = new SmsMon.MonitorDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonitorDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar2
            // 
            resources.ApplyResources(this.monthCalendar2, "monthCalendar2");
            this.monthCalendar2.Name = "monthCalendar2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Name = "label2";
            // 
            // txtUserLogin
            // 
            resources.ApplyResources(this.txtUserLogin, "txtUserLogin");
            this.txtUserLogin.ForeColor = System.Drawing.Color.Black;
            this.txtUserLogin.Name = "txtUserLogin";
            // 
            // txtUserPassword
            // 
            resources.ApplyResources(this.txtUserPassword, "txtUserPassword");
            this.txtUserPassword.ForeColor = System.Drawing.Color.Black;
            this.txtUserPassword.Name = "txtUserPassword";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.ForeColor = System.Drawing.Color.Snow;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMonitor
            // 
            this.btnMonitor.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnMonitor, "btnMonitor");
            this.btnMonitor.ForeColor = System.Drawing.Color.Snow;
            this.btnMonitor.Name = "btnMonitor";
            this.btnMonitor.UseVisualStyleBackColor = false;
            this.btnMonitor.Click += new System.EventHandler(this.btnMonitor_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.Snow;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panelUser);
            this.panel1.Controls.Add(this.txtLocation);
            this.panel1.Controls.Add(this.txtOrganztn);
            this.panel1.Controls.Add(this.btnSaveLocation);
            this.panel1.Controls.Add(this.btnNewLoc);
            this.panel1.Controls.Add(this.btnNewCustomer);
            this.panel1.Controls.Add(this.btnSaveCustomer);
            this.panel1.Controls.Add(this.comboLoc);
            this.panel1.Controls.Add(this.comboOrg);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.txtGenNumber);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.txtPwdAssigned);
            this.panel1.Controls.Add(this.txtUserAssigned);
            this.panel1.Controls.Add(this.chkGenActive);
            this.panel1.Controls.Add(this.txtGenID);
            this.panel1.Controls.Add(this.txtGenType);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.txtTelephone);
            this.panel1.Controls.Add(this.txtMobile);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtAddress1);
            this.panel1.Controls.Add(this.txtAddress2);
            this.panel1.Controls.Add(this.txtCity);
            this.panel1.Controls.Add(this.txtDistrict);
            this.panel1.Controls.Add(this.txtBuildingNo);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtOwner);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblCustId);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panelUser
            // 
            this.panelUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelUser.Controls.Add(this.label27);
            this.panelUser.Controls.Add(this.tblPanel);
            this.panelUser.Controls.Add(this.label26);
            this.panelUser.Controls.Add(this.label21);
            this.panelUser.Controls.Add(this.label25);
            this.panelUser.Controls.Add(this.lblAddress);
            this.panelUser.Controls.Add(this.label23);
            this.panelUser.Controls.Add(this.lblOrguser);
            resources.ApplyResources(this.panelUser, "panelUser");
            this.panelUser.ForeColor = System.Drawing.Color.DarkCyan;
            this.panelUser.Name = "panelUser";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // tblPanel
            // 
            resources.ApplyResources(this.tblPanel, "tblPanel");
            this.tblPanel.BackColor = System.Drawing.Color.White;
            this.tblPanel.Name = "tblPanel";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // lblOrguser
            // 
            resources.ApplyResources(this.lblOrguser, "lblOrguser");
            this.lblOrguser.Name = "lblOrguser";
            // 
            // txtLocation
            // 
            resources.ApplyResources(this.txtLocation, "txtLocation");
            this.txtLocation.Name = "txtLocation";
            // 
            // txtOrganztn
            // 
            resources.ApplyResources(this.txtOrganztn, "txtOrganztn");
            this.txtOrganztn.Name = "txtOrganztn";
            // 
            // btnSaveLocation
            // 
            this.btnSaveLocation.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnSaveLocation, "btnSaveLocation");
            this.btnSaveLocation.ForeColor = System.Drawing.Color.Snow;
            this.btnSaveLocation.Name = "btnSaveLocation";
            this.btnSaveLocation.UseVisualStyleBackColor = false;
            this.btnSaveLocation.Click += new System.EventHandler(this.btnSaveLocation_Click);
            // 
            // btnNewLoc
            // 
            this.btnNewLoc.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnNewLoc, "btnNewLoc");
            this.btnNewLoc.ForeColor = System.Drawing.Color.Snow;
            this.btnNewLoc.Name = "btnNewLoc";
            this.btnNewLoc.UseVisualStyleBackColor = false;
            this.btnNewLoc.Click += new System.EventHandler(this.btnNewLoc_Click);
            // 
            // btnNewCustomer
            // 
            this.btnNewCustomer.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnNewCustomer, "btnNewCustomer");
            this.btnNewCustomer.ForeColor = System.Drawing.Color.Snow;
            this.btnNewCustomer.Name = "btnNewCustomer";
            this.btnNewCustomer.UseVisualStyleBackColor = false;
            this.btnNewCustomer.Click += new System.EventHandler(this.btnNewCustomer_Click);
            // 
            // btnSaveCustomer
            // 
            this.btnSaveCustomer.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnSaveCustomer, "btnSaveCustomer");
            this.btnSaveCustomer.ForeColor = System.Drawing.Color.Snow;
            this.btnSaveCustomer.Name = "btnSaveCustomer";
            this.btnSaveCustomer.UseVisualStyleBackColor = false;
            this.btnSaveCustomer.Click += new System.EventHandler(this.btnSaveCustomer_Click);
            // 
            // comboLoc
            // 
            this.comboLoc.FormattingEnabled = true;
            resources.ApplyResources(this.comboLoc, "comboLoc");
            this.comboLoc.Name = "comboLoc";
            this.comboLoc.SelectedIndexChanged += new System.EventHandler(this.comboLoc_SelectedIndexChanged);
            // 
            // comboOrg
            // 
            this.comboOrg.FormattingEnabled = true;
            resources.ApplyResources(this.comboOrg, "comboOrg");
            this.comboOrg.Name = "comboOrg";
            this.comboOrg.SelectedIndexChanged += new System.EventHandler(this.comboOrg_SelectedIndexChanged);
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.DarkCyan;
            this.label19.Name = "label19";
            // 
            // txtGenNumber
            // 
            resources.ApplyResources(this.txtGenNumber, "txtGenNumber");
            this.txtGenNumber.Name = "txtGenNumber";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.Color.DarkCyan;
            this.label18.Name = "label18";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.DarkCyan;
            this.label14.Name = "label14";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.DarkCyan;
            this.label13.Name = "label13";
            // 
            // txtPwdAssigned
            // 
            resources.ApplyResources(this.txtPwdAssigned, "txtPwdAssigned");
            this.txtPwdAssigned.ForeColor = System.Drawing.Color.Black;
            this.txtPwdAssigned.Name = "txtPwdAssigned";
            // 
            // txtUserAssigned
            // 
            resources.ApplyResources(this.txtUserAssigned, "txtUserAssigned");
            this.txtUserAssigned.ForeColor = System.Drawing.Color.Black;
            this.txtUserAssigned.Name = "txtUserAssigned";
            // 
            // chkGenActive
            // 
            resources.ApplyResources(this.chkGenActive, "chkGenActive");
            this.chkGenActive.ForeColor = System.Drawing.Color.DarkCyan;
            this.chkGenActive.Name = "chkGenActive";
            this.chkGenActive.UseVisualStyleBackColor = true;
            // 
            // txtGenID
            // 
            resources.ApplyResources(this.txtGenID, "txtGenID");
            this.txtGenID.Name = "txtGenID";
            // 
            // txtGenType
            // 
            resources.ApplyResources(this.txtGenType, "txtGenType");
            this.txtGenType.Name = "txtGenType";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.DarkCyan;
            this.label17.Name = "label17";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.Color.DarkCyan;
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.DarkCyan;
            this.label15.Name = "label15";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.Color.DarkCyan;
            this.label12.Name = "label12";
            // 
            // txtTelephone
            // 
            resources.ApplyResources(this.txtTelephone, "txtTelephone");
            this.txtTelephone.Name = "txtTelephone";
            // 
            // txtMobile
            // 
            resources.ApplyResources(this.txtMobile, "txtMobile");
            this.txtMobile.Name = "txtMobile";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.DarkCyan;
            this.label11.Name = "label11";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.Color.DarkCyan;
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.Color.DarkCyan;
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.DarkCyan;
            this.label8.Name = "label8";
            // 
            // txtAddress1
            // 
            resources.ApplyResources(this.txtAddress1, "txtAddress1");
            this.txtAddress1.Name = "txtAddress1";
            // 
            // txtAddress2
            // 
            resources.ApplyResources(this.txtAddress2, "txtAddress2");
            this.txtAddress2.Name = "txtAddress2";
            // 
            // txtCity
            // 
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // txtDistrict
            // 
            resources.ApplyResources(this.txtDistrict, "txtDistrict");
            this.txtDistrict.Name = "txtDistrict";
            // 
            // txtBuildingNo
            // 
            resources.ApplyResources(this.txtBuildingNo, "txtBuildingNo");
            this.txtBuildingNo.Name = "txtBuildingNo";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.DarkCyan;
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.DarkCyan;
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.DarkCyan;
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.DarkCyan;
            this.label4.Name = "label4";
            // 
            // txtOwner
            // 
            resources.ApplyResources(this.txtOwner, "txtOwner");
            this.txtOwner.Name = "txtOwner";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Name = "label3";
            // 
            // lblCustId
            // 
            resources.ApplyResources(this.lblCustId, "lblCustId");
            this.lblCustId.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblCustId.Name = "lblCustId";
            // 
            // process1
            // 
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.SynchronizingObject = this;
            // 
            // btnPollGens
            // 
            this.btnPollGens.BackColor = System.Drawing.Color.DarkCyan;
            resources.ApplyResources(this.btnPollGens, "btnPollGens");
            this.btnPollGens.ForeColor = System.Drawing.Color.Snow;
            this.btnPollGens.Name = "btnPollGens";
            this.btnPollGens.UseVisualStyleBackColor = false;
            this.btnPollGens.Click += new System.EventHandler(this.btnPollGens_Click);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.DarkCyan;
            this.label20.Name = "label20";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.Color.DarkCyan;
            this.label22.Name = "label22";
            // 
            // timerSummary
            // 
            this.timerSummary.Interval = 5000;
            this.timerSummary.Tick += new System.EventHandler(this.timerSummary_Tick);
            // 
            // MonitorDataSet
            // 
            this.MonitorDataSet.DataSetName = "MonitorDataSet";
            this.MonitorDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Admin
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.btnPollGens);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMonitor);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.txtUserLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.monthCalendar2);
            this.Name = "Admin";
            //this.Load += new System.EventHandler(this.Admin_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelUser.ResumeLayout(false);
            this.panelUser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MonitorDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       
        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserLogin;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnMonitor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCustId;
        private System.Windows.Forms.TextBox txtOwner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.TextBox txtDistrict;
        private System.Windows.Forms.TextBox txtBuildingNo;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtPwdAssigned;
        private System.Windows.Forms.TextBox txtUserAssigned;
        private System.Windows.Forms.CheckBox chkGenActive;
        private System.Windows.Forms.TextBox txtGenID;
        private System.Windows.Forms.TextBox txtGenType;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtGenNumber;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox comboOrg;
        private System.Windows.Forms.ComboBox comboLoc;
        private System.Windows.Forms.Button btnSaveLocation;
        private System.Windows.Forms.Button btnNewLoc;
        private System.Windows.Forms.Button btnNewCustomer;
        private System.Windows.Forms.Button btnSaveCustomer;
        private System.Windows.Forms.TextBox txtOrganztn;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Button btnPollGens;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblOrguser;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Timer timerSummary;
        private MonitorDataSet MonitorDataSet;
        private System.Windows.Forms.TableLayoutPanel tblPanel;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label23;
    }
}