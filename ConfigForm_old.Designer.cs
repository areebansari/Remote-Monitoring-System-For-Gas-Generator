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
            this.comboBoxPollTimer = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxPollModem = new System.Windows.Forms.ComboBox();
            this.comboBoxRxModem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.LightBlue;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel7.Controls.Add(this.comboBoxPollTimer);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.comboBoxPollModem);
            this.panel7.Controls.Add(this.comboBoxRxModem);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.ForeColor = System.Drawing.Color.CadetBlue;
            this.panel7.Location = new System.Drawing.Point(-3, -25);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(650, 380);
            this.panel7.TabIndex = 3;
            // 
            // comboBoxPollTimer
            // 
            this.comboBoxPollTimer.FormattingEnabled = true;
            this.comboBoxPollTimer.Location = new System.Drawing.Point(288, 98);
            this.comboBoxPollTimer.Name = "comboBoxPollTimer";
            this.comboBoxPollTimer.Size = new System.Drawing.Size(83, 21);
            this.comboBoxPollTimer.TabIndex = 6;
            this.comboBoxPollTimer.SelectedIndexChanged += new System.EventHandler(this.comboBoxPollTimer_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(285, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Gen Poll Time";
            // 
            // comboBoxPollModem
            // 
            this.comboBoxPollModem.FormattingEnabled = true;
            this.comboBoxPollModem.Location = new System.Drawing.Point(162, 97);
            this.comboBoxPollModem.Name = "comboBoxPollModem";
            this.comboBoxPollModem.Size = new System.Drawing.Size(69, 21);
            this.comboBoxPollModem.TabIndex = 4;
            this.comboBoxPollModem.SelectedIndexChanged += new System.EventHandler(this.comboBoxPollModem_SelectedIndexChanged);
            // 
            // comboBoxRxModem
            // 
            this.comboBoxRxModem.FormattingEnabled = true;
            this.comboBoxRxModem.Location = new System.Drawing.Point(47, 96);
            this.comboBoxRxModem.Name = "comboBoxRxModem";
            this.comboBoxRxModem.Size = new System.Drawing.Size(69, 21);
            this.comboBoxRxModem.TabIndex = 3;
            this.comboBoxRxModem.SelectedIndexChanged += new System.EventHandler(this.comboBoxRxModem_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(146, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Gen Poll Modem";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SMS Rx Modem";
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxPollModem;
        private System.Windows.Forms.ComboBox comboBoxRxModem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;


    }
}