namespace SmsMon
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.btnModemUpdate = new System.Windows.Forms.Button();
            this.comboBoxDBNames = new System.Windows.Forms.ComboBox();
            this.comboBoxServerName = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnUpdateConn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxPollTimer = new System.Windows.Forms.ComboBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.comboBoxPollModem = new System.Windows.Forms.ComboBox();
            this.comboBoxRxModem = new System.Windows.Forms.ComboBox();
            this.lblPollModem = new System.Windows.Forms.Label();
            this.lblRxModem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightBlue;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.panel1);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.btnModemUpdate);
            this.panel7.Controls.Add(this.comboBoxDBNames);
            this.panel7.Controls.Add(this.comboBoxServerName);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Controls.Add(this.btnUpdateConn);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Controls.Add(this.comboBoxPollTimer);
            this.panel7.Controls.Add(this.lblTimer);
            this.panel7.Controls.Add(this.comboBoxPollModem);
            this.panel7.Controls.Add(this.comboBoxRxModem);
            this.panel7.Controls.Add(this.lblPollModem);
            this.panel7.Controls.Add(this.lblRxModem);
            this.panel7.Controls.Add(this.label1);
            this.panel7.ForeColor = System.Drawing.Color.CadetBlue;
            this.panel7.Location = new System.Drawing.Point(-3, -6);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(650, 368);
            this.panel7.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.panel1.Location = new System.Drawing.Point(27, 157);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(469, 10);
            this.panel1.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(404, 79);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Modem / Poll";
            // 
            // btnModemUpdate
            // 
            this.btnModemUpdate.BackColor = System.Drawing.Color.Green;
            this.btnModemUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModemUpdate.ForeColor = System.Drawing.Color.Yellow;
            this.btnModemUpdate.Location = new System.Drawing.Point(416, 100);
            this.btnModemUpdate.Name = "btnModemUpdate";
            this.btnModemUpdate.Size = new System.Drawing.Size(66, 23);
            this.btnModemUpdate.TabIndex = 16;
            this.btnModemUpdate.Text = "Update";
            this.btnModemUpdate.UseVisualStyleBackColor = false;
            this.btnModemUpdate.Click += new System.EventHandler(this.btnModemUpdate_Click);
            // 
            // comboBoxDBNames
            // 
            this.comboBoxDBNames.FormattingEnabled = true;
            this.comboBoxDBNames.Location = new System.Drawing.Point(234, 224);
            this.comboBoxDBNames.Name = "comboBoxDBNames";
            this.comboBoxDBNames.Size = new System.Drawing.Size(137, 21);
            this.comboBoxDBNames.TabIndex = 15;
            // 
            // comboBoxServerName
            // 
            this.comboBoxServerName.FormattingEnabled = true;
            this.comboBoxServerName.Location = new System.Drawing.Point(48, 224);
            this.comboBoxServerName.Name = "comboBoxServerName";
            this.comboBoxServerName.Size = new System.Drawing.Size(157, 21);
            this.comboBoxServerName.TabIndex = 14;
            this.comboBoxServerName.SelectedIndexChanged += new System.EventHandler(this.comboBoxServerName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(393, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "dB Configuration";
            // 
            // btnUpdateConn
            // 
            this.btnUpdateConn.BackColor = System.Drawing.Color.Green;
            this.btnUpdateConn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateConn.ForeColor = System.Drawing.Color.Yellow;
            this.btnUpdateConn.Location = new System.Drawing.Point(415, 224);
            this.btnUpdateConn.Name = "btnUpdateConn";
            this.btnUpdateConn.Size = new System.Drawing.Size(66, 23);
            this.btnUpdateConn.TabIndex = 12;
            this.btnUpdateConn.Text = "Update";
            this.btnUpdateConn.UseVisualStyleBackColor = false;
            this.btnUpdateConn.Click += new System.EventHandler(this.btnUpdateConn_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(242, 203);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Data Base Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(58, 203);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Server  Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "label5";
            // 
            // comboBoxPollTimer
            // 
            this.comboBoxPollTimer.FormattingEnabled = true;
            this.comboBoxPollTimer.Location = new System.Drawing.Point(309, 98);
            this.comboBoxPollTimer.Name = "comboBoxPollTimer";
            this.comboBoxPollTimer.Size = new System.Drawing.Size(83, 21);
            this.comboBoxPollTimer.TabIndex = 6;
            // 
            // lblTimer
            // 
            this.lblTimer.AutoSize = true;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(306, 79);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(59, 13);
            this.lblTimer.TabIndex = 5;
            this.lblTimer.Text = "Poll Time";
            // 
            // comboBoxPollModem
            // 
            this.comboBoxPollModem.FormattingEnabled = true;
            this.comboBoxPollModem.Location = new System.Drawing.Point(183, 97);
            this.comboBoxPollModem.Name = "comboBoxPollModem";
            this.comboBoxPollModem.Size = new System.Drawing.Size(69, 21);
            this.comboBoxPollModem.TabIndex = 4;
            // 
            // comboBoxRxModem
            // 
            this.comboBoxRxModem.FormattingEnabled = true;
            this.comboBoxRxModem.Location = new System.Drawing.Point(37, 96);
            this.comboBoxRxModem.Name = "comboBoxRxModem";
            this.comboBoxRxModem.Size = new System.Drawing.Size(69, 21);
            this.comboBoxRxModem.TabIndex = 3;
            // 
            // lblPollModem
            // 
            this.lblPollModem.AutoSize = true;
            this.lblPollModem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollModem.Location = new System.Drawing.Point(162, 78);
            this.lblPollModem.Name = "lblPollModem";
            this.lblPollModem.Size = new System.Drawing.Size(72, 13);
            this.lblPollModem.TabIndex = 2;
            this.lblPollModem.Text = "Poll Modem";
            // 
            // lblRxModem
            // 
            this.lblRxModem.AutoSize = true;
            this.lblRxModem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRxModem.Location = new System.Drawing.Point(20, 78);
            this.lblRxModem.Name = "lblRxModem";
            this.lblRxModem.Size = new System.Drawing.Size(66, 13);
            this.lblRxModem.TabIndex = 1;
            this.lblRxModem.Text = "Rx Modem";
            this.lblRxModem.Click += new System.EventHandler(this.lblRxModem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.CadetBlue;
            this.label1.Location = new System.Drawing.Point(187, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Systems Configuration";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 350);
            this.Controls.Add(this.panel7);
            this.ForeColor = System.Drawing.Color.CadetBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Opacity = 0.05D;
            this.Text = "System Coonfiguration";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.ComboBox comboBoxPollTimer;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.ComboBox comboBoxPollModem;
        private System.Windows.Forms.ComboBox comboBoxRxModem;
        private System.Windows.Forms.Label lblPollModem;
        private System.Windows.Forms.Label lblRxModem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnUpdateConn;
        private System.Windows.Forms.ComboBox comboBoxServerName;
        private System.Windows.Forms.ComboBox comboBoxDBNames;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnModemUpdate;
        private System.Windows.Forms.Panel panel1;


    }
}