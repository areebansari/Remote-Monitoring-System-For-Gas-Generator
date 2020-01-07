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
            this.Analog_page = new System.Windows.Forms.TabPage();
            this.panel8 = new System.Windows.Forms.Panel();
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
            this.Analog_page.SuspendLayout();
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
            this.tabControl1.Controls.Add(this.Analog_page);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // AdminPage
            // 
            this.AdminPage.BackColor = System.Drawing.Color.CadetBlue;
            this.AdminPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.AdminPage.Controls.Add(this.panel1);
            resources.ApplyResources(this.AdminPage, "AdminPage");
            this.AdminPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.AdminPage.Name = "AdminPage";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Azure;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.picGenAlarmStatus);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // picGenAlarmStatus
            // 
            this.picGenAlarmStatus.Image = global::SmsMon.Properties.Resources.led_grn12x12;
            resources.ApplyResources(this.picGenAlarmStatus, "picGenAlarmStatus");
            this.picGenAlarmStatus.Name = "picGenAlarmStatus";
            this.picGenAlarmStatus.TabStop = false;
            // 
            // MainPage
            // 
            this.MainPage.BackColor = System.Drawing.Color.CadetBlue;
            this.MainPage.Controls.Add(this.panel2);
            resources.ApplyResources(this.MainPage, "MainPage");
            this.MainPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.MainPage.Name = "MainPage";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // StatusPage
            // 
            this.StatusPage.BackColor = System.Drawing.Color.CadetBlue;
            this.StatusPage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusPage.Controls.Add(this.panel3);
            resources.ApplyResources(this.StatusPage, "StatusPage");
            this.StatusPage.Name = "StatusPage";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightBlue;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // LogsPage
            // 
            this.LogsPage.BackColor = System.Drawing.Color.CadetBlue;
            this.LogsPage.Controls.Add(this.label1);
            this.LogsPage.Controls.Add(this.panel4);
            resources.ApplyResources(this.LogsPage, "LogsPage");
            this.LogsPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.LogsPage.Name = "LogsPage";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Name = "label1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightBlue;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // GenPollPage
            // 
            this.GenPollPage.BackColor = System.Drawing.Color.CadetBlue;
            this.GenPollPage.Controls.Add(this.panel5);
            resources.ApplyResources(this.GenPollPage, "GenPollPage");
            this.GenPollPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.GenPollPage.Name = "GenPollPage";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.LightBlue;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // ReportsPage
            // 
            this.ReportsPage.BackColor = System.Drawing.Color.CadetBlue;
            this.ReportsPage.Controls.Add(this.panel6);
            resources.ApplyResources(this.ReportsPage, "ReportsPage");
            this.ReportsPage.ForeColor = System.Drawing.SystemColors.Highlight;
            this.ReportsPage.Name = "ReportsPage";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.LightBlue;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // ConfigPage
            // 
            this.ConfigPage.BackColor = System.Drawing.Color.CadetBlue;
            this.ConfigPage.Controls.Add(this.panel7);
            resources.ApplyResources(this.ConfigPage, "ConfigPage");
            this.ConfigPage.Name = "ConfigPage";
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightBlue;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // Analog_page
            // 
            this.Analog_page.BackColor = System.Drawing.Color.CadetBlue;
            this.Analog_page.Controls.Add(this.panel8);
            resources.ApplyResources(this.Analog_page, "Analog_page");
            this.Analog_page.Name = "Analog_page";
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Azure;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel8.ForeColor = System.Drawing.SystemColors.Highlight;
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // TabForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "TabForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TabForm_FormClosing);
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
            this.Analog_page.ResumeLayout(false);
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
        private System.Windows.Forms.TabPage Analog_page;
        private System.Windows.Forms.Panel panel8;
    }
}