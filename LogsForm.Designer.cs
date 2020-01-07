namespace SmsMon
{
    partial class LogsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogsForm));
            this.btnBack = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnGenData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSmsStart = new System.Windows.Forms.Button();
            this.btnGenStart = new System.Windows.Forms.Button();
            this.btnAlarm = new System.Windows.Forms.Button();
            this.lblHistoricalData = new System.Windows.Forms.Label();
            this.btnService = new System.Windows.Forms.Button();
            this.btnGenFuel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DarkCyan;
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(23, 349);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 30);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "<= Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Display";
            // 
            // btnGenData
            // 
            this.btnGenData.ForeColor = System.Drawing.Color.Yellow;
            this.btnGenData.Location = new System.Drawing.Point(58, 27);
            this.btnGenData.Name = "btnGenData";
            this.btnGenData.Size = new System.Drawing.Size(75, 23);
            this.btnGenData.TabIndex = 7;
            this.btnGenData.Text = "Gen Data";
            this.btnGenData.UseVisualStyleBackColor = true;
            this.btnGenData.Click += new System.EventHandler(this.bGenData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Location = new System.Drawing.Point(5, 60);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.Size = new System.Drawing.Size(650, 276);
            this.dataGridView1.TabIndex = 8;
            // 
            // btnSmsStart
            // 
            this.btnSmsStart.ForeColor = System.Drawing.Color.Yellow;
            this.btnSmsStart.Location = new System.Drawing.Point(320, 29);
            this.btnSmsStart.Name = "btnSmsStart";
            this.btnSmsStart.Size = new System.Drawing.Size(75, 23);
            this.btnSmsStart.TabIndex = 9;
            this.btnSmsStart.Text = "SMS Start";
            this.btnSmsStart.UseVisualStyleBackColor = true;
            this.btnSmsStart.Click += new System.EventHandler(this.btnSmsStart_Click);
            // 
            // btnGenStart
            // 
            this.btnGenStart.ForeColor = System.Drawing.Color.Yellow;
            this.btnGenStart.Location = new System.Drawing.Point(235, 29);
            this.btnGenStart.Name = "btnGenStart";
            this.btnGenStart.Size = new System.Drawing.Size(75, 23);
            this.btnGenStart.TabIndex = 10;
            this.btnGenStart.Text = "Gen Start";
            this.btnGenStart.UseVisualStyleBackColor = true;
            this.btnGenStart.Click += new System.EventHandler(this.btnGenStart_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.ForeColor = System.Drawing.Color.Yellow;
            this.btnAlarm.Location = new System.Drawing.Point(146, 28);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Size = new System.Drawing.Size(75, 23);
            this.btnAlarm.TabIndex = 11;
            this.btnAlarm.Text = "Gen Alarms";
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // lblHistoricalData
            // 
            this.lblHistoricalData.AutoSize = true;
            this.lblHistoricalData.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblHistoricalData.Location = new System.Drawing.Point(21, 10);
            this.lblHistoricalData.Name = "lblHistoricalData";
            this.lblHistoricalData.Size = new System.Drawing.Size(126, 13);
            this.lblHistoricalData.TabIndex = 12;
            this.lblHistoricalData.Text = "Generator Historical Data";
            // 
            // btnService
            // 
            this.btnService.ForeColor = System.Drawing.Color.Yellow;
            this.btnService.Location = new System.Drawing.Point(490, 29);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(75, 23);
            this.btnService.TabIndex = 13;
            this.btnService.Text = "Gen Service";
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // btnGenFuel
            // 
            this.btnGenFuel.ForeColor = System.Drawing.Color.Yellow;
            this.btnGenFuel.Location = new System.Drawing.Point(405, 29);
            this.btnGenFuel.Name = "btnGenFuel";
            this.btnGenFuel.Size = new System.Drawing.Size(75, 23);
            this.btnGenFuel.TabIndex = 14;
            this.btnGenFuel.Text = "Fuel Status";
            this.btnGenFuel.UseVisualStyleBackColor = true;
            this.btnGenFuel.Click += new System.EventHandler(this.btnGenFuel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 360);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = " Please select date from here =>";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(348, 353);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(179, 20);
            this.dateTimePicker1.TabIndex = 16;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // LogsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 384);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenFuel);
            this.Controls.Add(this.btnService);
            this.Controls.Add(this.lblHistoricalData);
            this.Controls.Add(this.btnAlarm);
            this.Controls.Add(this.btnGenStart);
            this.Controls.Add(this.btnSmsStart);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnGenData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBack);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogsForm";
            this.Text = "SMS Monitor - Logs";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.LogsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSmsStart;
        private System.Windows.Forms.Button btnGenStart;
        private System.Windows.Forms.Button btnAlarm;
        private System.Windows.Forms.Label lblHistoricalData;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.Button btnGenFuel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}