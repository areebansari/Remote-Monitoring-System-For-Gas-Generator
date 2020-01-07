namespace SmsMon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnGsmModule = new System.Windows.Forms.Button();
            this.btnNetworkStatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.monthCalendar2 = new System.Windows.Forms.MonthCalendar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btQuery = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.CurrentTime = new System.Windows.Forms.Label();
            this.BtnClear = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnLogs = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LblOrganization = new System.Windows.Forms.LinkLabel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btBack = new System.Windows.Forms.Button();
            this.lblOrg = new System.Windows.Forms.Label();
            this.lblLoc = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(438, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "GSM Module";
            // 
            // btnGsmModule
            // 
            this.btnGsmModule.BackColor = System.Drawing.Color.Red;
            this.btnGsmModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGsmModule.ForeColor = System.Drawing.Color.White;
            this.btnGsmModule.Location = new System.Drawing.Point(523, 3);
            this.btnGsmModule.Name = "btnGsmModule";
            this.btnGsmModule.Size = new System.Drawing.Size(66, 28);
            this.btnGsmModule.TabIndex = 1;
            this.btnGsmModule.Text = "Fail";
            this.btnGsmModule.UseVisualStyleBackColor = false;
            // 
            // btnNetworkStatus
            // 
            this.btnNetworkStatus.BackColor = System.Drawing.Color.Red;
            this.btnNetworkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkStatus.ForeColor = System.Drawing.Color.White;
            this.btnNetworkStatus.Location = new System.Drawing.Point(523, 42);
            this.btnNetworkStatus.Name = "btnNetworkStatus";
            this.btnNetworkStatus.Size = new System.Drawing.Size(66, 28);
            this.btnNetworkStatus.TabIndex = 2;
            this.btnNetworkStatus.Text = "Fail";
            this.btnNetworkStatus.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CadetBlue;
            this.label2.Location = new System.Drawing.Point(424, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Network Status";
            // 
            // monthCalendar2
            // 
            this.monthCalendar2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monthCalendar2.Location = new System.Drawing.Point(382, 106);
            this.monthCalendar2.Name = "monthCalendar2";
            this.monthCalendar2.TabIndex = 5;
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(11, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(175, 24);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btQuery
            // 
            this.btQuery.BackColor = System.Drawing.Color.DarkCyan;
            this.btQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btQuery.Location = new System.Drawing.Point(198, 42);
            this.btQuery.Name = "btQuery";
            this.btQuery.Size = new System.Drawing.Size(52, 28);
            this.btQuery.TabIndex = 7;
            this.btQuery.Text = "Query";
            this.btQuery.UseVisualStyleBackColor = false;
            this.btQuery.Click += new System.EventHandler(this.btQuery_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(344, 192);
            this.textBox1.TabIndex = 8;
            // 
            // CurrentTime
            // 
            this.CurrentTime.AutoSize = true;
            this.CurrentTime.ForeColor = System.Drawing.Color.CadetBlue;
            this.CurrentTime.Location = new System.Drawing.Point(11, 9);
            this.CurrentTime.Name = "CurrentTime";
            this.CurrentTime.Size = new System.Drawing.Size(0, 20);
            this.CurrentTime.TabIndex = 10;
            // 
            // BtnClear
            // 
            this.BtnClear.BackColor = System.Drawing.Color.DarkCyan;
            this.BtnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClear.Location = new System.Drawing.Point(270, 42);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(50, 28);
            this.BtnClear.TabIndex = 11;
            this.BtnClear.Text = "Clear";
            this.BtnClear.UseVisualStyleBackColor = false;
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnStatus
            // 
            this.btnStatus.BackColor = System.Drawing.Color.DarkCyan;
            this.btnStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatus.Location = new System.Drawing.Point(411, 288);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(58, 28);
            this.btnStatus.TabIndex = 12;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = false;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnLogs
            // 
            this.btnLogs.BackColor = System.Drawing.Color.DarkCyan;
            this.btnLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogs.Location = new System.Drawing.Point(504, 288);
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(53, 28);
            this.btnLogs.TabIndex = 13;
            this.btnLogs.Text = "Logs";
            this.btnLogs.UseVisualStyleBackColor = false;
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 288);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(176, 46);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // LblOrganization
            // 
            this.LblOrganization.AutoSize = true;
            this.LblOrganization.DisabledLinkColor = System.Drawing.Color.Black;
            this.LblOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrganization.LinkColor = System.Drawing.Color.Blue;
            this.LblOrganization.Location = new System.Drawing.Point(13, 2);
            this.LblOrganization.Name = "LblOrganization";
            this.LblOrganization.Size = new System.Drawing.Size(0, 15);
            this.LblOrganization.TabIndex = 16;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // btBack
            // 
            this.btBack.BackColor = System.Drawing.Color.DarkCyan;
            this.btBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBack.Location = new System.Drawing.Point(216, 289);
            this.btBack.Name = "btBack";
            this.btBack.Size = new System.Drawing.Size(56, 28);
            this.btBack.TabIndex = 18;
            this.btBack.Text = "<= Back";
            this.btBack.UseVisualStyleBackColor = false;
            this.btBack.Click += new System.EventHandler(this.btBack_Click);
            // 
            // lblOrg
            // 
            this.lblOrg.AutoSize = true;
            this.lblOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrg.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblOrg.Location = new System.Drawing.Point(11, 5);
            this.lblOrg.Name = "lblOrg";
            this.lblOrg.Size = new System.Drawing.Size(86, 13);
            this.lblOrg.TabIndex = 19;
            this.lblOrg.Text = "Organization: ";
            this.lblOrg.Click += new System.EventHandler(this.lblCustId_Click);
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblLoc.Location = new System.Drawing.Point(11, 18);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(64, 13);
            this.lblLoc.TabIndex = 20;
            this.lblLoc.Text = "Location: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(622, 346);
            this.Controls.Add(this.lblLoc);
            this.Controls.Add(this.lblOrg);
            this.Controls.Add(this.btBack);
            this.Controls.Add(this.LblOrganization);
            this.Controls.Add(this.btnLogs);
            this.Controls.Add(this.btnStatus);
            this.Controls.Add(this.BtnClear);
            this.Controls.Add(this.CurrentTime);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btQuery);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.monthCalendar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNetworkStatus);
            this.Controls.Add(this.btnGsmModule);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "SMS Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGsmModule;
        private System.Windows.Forms.Button btnNetworkStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MonthCalendar monthCalendar2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btQuery;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label CurrentTime;
        private System.Windows.Forms.Button BtnClear;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Button btnLogs;
        private System.Windows.Forms.LinkLabel LblOrganization;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btBack;
        private System.Windows.Forms.Label lblOrg;
        private System.Windows.Forms.Label lblLoc;

        
    }
}

