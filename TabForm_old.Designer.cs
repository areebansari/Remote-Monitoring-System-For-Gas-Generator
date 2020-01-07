namespace SmsMon
{
    partial class TabForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TabForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.AdminPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picGenAlarmStatus = new System.Windows.Forms.PictureBox();
            this.MainPage = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.StatusPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LogsPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.GenPollPage = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ReportsPage = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.ConfigPage = new System.Windows.Forms.TabPage();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.AdminPage.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGenAlarmStatus)).BeginInit();
            this.MainPage.SuspendLayout();
            this.StatusPage.SuspendLayout();
            this.LogsPage.SuspendLayout();
            this.GenPollPage.SuspendLayout();
            this.ReportsPage.SuspendLayout();
            this.ConfigPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.AdminPage);
            this.tabControl1.Controls.Add(this.MainPage);
            this.tabControl1.Controls.Add(this.StatusPage);
            this.tabControl1.Controls.Add(this.LogsPage);
            this.tabControl1.Controls.Add(this.GenPollPage);
            this.tabControl1.Controls.Add(this.ReportsPage);
            this.tabControl1.Controls.Add(this.ConfigPage);
            this.tabControl1.Location = new System.Drawing.Point(10, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(748, 490);
            this.tabControl1.TabIndex = 0;
            // 
            // AdminPage
            // 
            this.AdminPage.BackColor = System.Drawing.Color.CadetBlue;
            this.AdminPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AdminPage.Controls.Add(this.panel1);
            this.AdminPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AdminPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.AdminPage.Location = new System.Drawing.Point(4, 22);
            this.AdminPage.Name = "AdminPage";
            this.AdminPage.Padding = new System.Windows.Forms.Padding(3);
            this.AdminPage.Size = new System.Drawing.Size(740, 464);
            this.AdminPage.TabIndex = 0;
            this.AdminPage.Text = "Administration";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.picGenAlarmStatus);
            this.panel1.Location = new System.Drawing.Point(22, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(686, 373);
            this.panel1.TabIndex = 0;
            // 
            // picGenAlarmStatus
            // 
            this.picGenAlarmStatus.Image = global::SmsMon.Properties.Resources.led_grn12x12;
            this.picGenAlarmStatus.Location = new System.Drawing.Point(13, 389);
            this.picGenAlarmStatus.Name = "picGenAlarmStatus";
            this.picGenAlarmStatus.Size = new System.Drawing.Size(15, 13);
            this.picGenAlarmStatus.TabIndex = 71;
            this.picGenAlarmStatus.TabStop = false;
            // 
            // MainPage
            // 
            this.MainPage.BackColor = System.Drawing.Color.CadetBlue;
            this.MainPage.Controls.Add(this.panel2);
            this.MainPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.MainPage.Location = new System.Drawing.Point(4, 22);
            this.MainPage.Name = "MainPage";
            this.MainPage.Padding = new System.Windows.Forms.Padding(3);
            this.MainPage.Size = new System.Drawing.Size(740, 464);
            this.MainPage.TabIndex = 1;
            this.MainPage.Text = "SMS Main";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(48, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(637, 382);
            this.panel2.TabIndex = 1;
            // 
            // StatusPage
            // 
            this.StatusPage.BackColor = System.Drawing.Color.CadetBlue;
            this.StatusPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusPage.Controls.Add(this.panel3);
            this.StatusPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusPage.Location = new System.Drawing.Point(4, 22);
            this.StatusPage.Name = "StatusPage";
            this.StatusPage.Size = new System.Drawing.Size(740, 464);
            this.StatusPage.TabIndex = 2;
            this.StatusPage.Text = "Gen Status";
            this.StatusPage.ToolTipText = "Gen Alarm Indication";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Location = new System.Drawing.Point(34, 36);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(650, 391);
            this.panel3.TabIndex = 1;
            // 
            // LogsPage
            // 
            this.LogsPage.BackColor = System.Drawing.Color.CadetBlue;
            this.LogsPage.Controls.Add(this.label1);
            this.LogsPage.Controls.Add(this.panel4);
            this.LogsPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogsPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LogsPage.Location = new System.Drawing.Point(4, 22);
            this.LogsPage.Name = "LogsPage";
            this.LogsPage.Size = new System.Drawing.Size(740, 464);
            this.LogsPage.TabIndex = 3;
            this.LogsPage.Text = "Gen Logs";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(81, 465);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 69;
            this.label1.Text = "Gen Alarm Status";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Location = new System.Drawing.Point(26, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(690, 420);
            this.panel4.TabIndex = 1;
            // 
            // GenPollPage
            // 
            this.GenPollPage.BackColor = System.Drawing.Color.CadetBlue;
            this.GenPollPage.Controls.Add(this.panel5);
            this.GenPollPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenPollPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.GenPollPage.Location = new System.Drawing.Point(4, 22);
            this.GenPollPage.Name = "GenPollPage";
            this.GenPollPage.Size = new System.Drawing.Size(740, 464);
            this.GenPollPage.TabIndex = 4;
            this.GenPollPage.Text = "Poll Gens";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel5.Location = new System.Drawing.Point(45, 34);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(650, 380);
            this.panel5.TabIndex = 1;
            // 
            // ReportsPage
            // 
            this.ReportsPage.BackColor = System.Drawing.Color.CadetBlue;
            this.ReportsPage.Controls.Add(this.panel6);
            this.ReportsPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReportsPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ReportsPage.Location = new System.Drawing.Point(4, 22);
            this.ReportsPage.Name = "ReportsPage";
            this.ReportsPage.Size = new System.Drawing.Size(740, 464);
            this.ReportsPage.TabIndex = 5;
            this.ReportsPage.Text = "Reports";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightBlue;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel6.Location = new System.Drawing.Point(45, 41);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(641, 384);
            this.panel6.TabIndex = 1;
            // 
            // ConfigPage
            // 
            this.ConfigPage.BackColor = System.Drawing.Color.CadetBlue;
            this.ConfigPage.Controls.Add(this.panel7);
            this.ConfigPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigPage.Name = "ConfigPage";
            this.ConfigPage.Size = new System.Drawing.Size(740, 464);
            this.ConfigPage.TabIndex = 6;
            this.ConfigPage.Text = "ConfigPage";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightBlue;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Location = new System.Drawing.Point(45, 42);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(650, 380);
            this.panel7.TabIndex = 2;
            // 
            // TabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 514);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TabForm";
            this.Text = "Generator Monitoring System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabForm_FormClosing);
            this.Load += new System.EventHandler(this.TabForm_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.AdminPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGenAlarmStatus)).EndInit();
            this.MainPage.ResumeLayout(false);
            this.StatusPage.ResumeLayout(false);
            this.LogsPage.ResumeLayout(false);
            this.LogsPage.PerformLayout();
            this.GenPollPage.ResumeLayout(false);
            this.ReportsPage.ResumeLayout(false);
            this.ConfigPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage AdminPage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage MainPage;
        private System.Windows.Forms.TabPage StatusPage;
        private System.Windows.Forms.TabPage LogsPage;
        private System.Windows.Forms.TabPage GenPollPage;
        private System.Windows.Forms.TabPage ReportsPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabPage ConfigPage;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picGenAlarmStatus;
    }
}