namespace SmsMon
{
    partial class PollGens
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PollGens));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCustId = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxOrgs = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPolling = new System.Windows.Forms.Label();
            this.picPollGreenLed = new System.Windows.Forms.PictureBox();
            this.lblPollSeconds = new System.Windows.Forms.Label();
            this.btnAddPollGen = new System.Windows.Forms.Button();
            this.btnLocGenRemove = new System.Windows.Forms.Button();
            this.btnLocGenReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxAllOrgs = new System.Windows.Forms.CheckBox();
            this.checkBoxLocGen = new System.Windows.Forms.CheckBox();
            this.lblStoppedLed = new System.Windows.Forms.Label();
            this.picPollStopedLed = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbTimer = new System.Windows.Forms.RadioButton();
            this.rbPoll = new System.Windows.Forms.RadioButton();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.listGenLoc = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNetworkStatus = new System.Windows.Forms.Button();
            this.btnGsmModule = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPollGreenLed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPollStopedLed)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 20000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCustId
            // 
            this.lblCustId.AutoSize = true;
            this.lblCustId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustId.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblCustId.Location = new System.Drawing.Point(39, 29);
            this.lblCustId.Name = "lblCustId";
            this.lblCustId.Size = new System.Drawing.Size(109, 17);
            this.lblCustId.TabIndex = 13;
            this.lblCustId.Text = "Organizations";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(260, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Select Locations to Monitor ";
            // 
            // listBoxOrgs
            // 
            this.listBoxOrgs.FormattingEnabled = true;
            this.listBoxOrgs.HorizontalScrollbar = true;
            this.listBoxOrgs.Location = new System.Drawing.Point(12, 57);
            this.listBoxOrgs.Name = "listBoxOrgs";
            this.listBoxOrgs.Size = new System.Drawing.Size(162, 160);
            this.listBoxOrgs.TabIndex = 15;
            this.listBoxOrgs.SelectedIndexChanged += new System.EventHandler(this.listBoxOrgs_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPolling);
            this.groupBox1.Controls.Add(this.picPollGreenLed);
            this.groupBox1.Controls.Add(this.lblPollSeconds);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkCyan;
            this.groupBox1.Location = new System.Drawing.Point(12, 228);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 62);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Monitor Every";
            // 
            // lblPolling
            // 
            this.lblPolling.AutoSize = true;
            this.lblPolling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPolling.Location = new System.Drawing.Point(47, 25);
            this.lblPolling.Name = "lblPolling";
            this.lblPolling.Size = new System.Drawing.Size(38, 13);
            this.lblPolling.TabIndex = 32;
            this.lblPolling.Text = "Polling";
            // 
            // picPollGreenLed
            // 
            this.picPollGreenLed.Image = global::SmsMon.Properties.Resources.led_grn12x12;
            this.picPollGreenLed.Location = new System.Drawing.Point(24, 25);
            this.picPollGreenLed.Name = "picPollGreenLed";
            this.picPollGreenLed.Size = new System.Drawing.Size(13, 13);
            this.picPollGreenLed.TabIndex = 31;
            this.picPollGreenLed.TabStop = false;
            // 
            // lblPollSeconds
            // 
            this.lblPollSeconds.AutoSize = true;
            this.lblPollSeconds.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPollSeconds.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblPollSeconds.Location = new System.Drawing.Point(110, 4);
            this.lblPollSeconds.Name = "lblPollSeconds";
            this.lblPollSeconds.Size = new System.Drawing.Size(59, 13);
            this.lblPollSeconds.TabIndex = 31;
            this.lblPollSeconds.Text = " 20 seonds";
            // 
            // btnAddPollGen
            // 
            this.btnAddPollGen.BackColor = System.Drawing.Color.DarkCyan;
            this.btnAddPollGen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPollGen.ForeColor = System.Drawing.Color.Yellow;
            this.btnAddPollGen.Location = new System.Drawing.Point(450, 87);
            this.btnAddPollGen.Name = "btnAddPollGen";
            this.btnAddPollGen.Size = new System.Drawing.Size(79, 23);
            this.btnAddPollGen.TabIndex = 20;
            this.btnAddPollGen.Text = " Add";
            this.btnAddPollGen.UseVisualStyleBackColor = false;
            this.btnAddPollGen.Click += new System.EventHandler(this.btnAddPollGen_Click);
            // 
            // btnLocGenRemove
            // 
            this.btnLocGenRemove.BackColor = System.Drawing.Color.DarkCyan;
            this.btnLocGenRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocGenRemove.ForeColor = System.Drawing.Color.Yellow;
            this.btnLocGenRemove.Location = new System.Drawing.Point(454, 128);
            this.btnLocGenRemove.Name = "btnLocGenRemove";
            this.btnLocGenRemove.Size = new System.Drawing.Size(79, 23);
            this.btnLocGenRemove.TabIndex = 21;
            this.btnLocGenRemove.Text = " Remove";
            this.btnLocGenRemove.UseVisualStyleBackColor = false;
            this.btnLocGenRemove.Click += new System.EventHandler(this.btnLocGenRemove_Click);
            // 
            // btnLocGenReset
            // 
            this.btnLocGenReset.BackColor = System.Drawing.Color.DarkCyan;
            this.btnLocGenReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLocGenReset.ForeColor = System.Drawing.Color.Yellow;
            this.btnLocGenReset.Location = new System.Drawing.Point(450, 306);
            this.btnLocGenReset.Name = "btnLocGenReset";
            this.btnLocGenReset.Size = new System.Drawing.Size(79, 23);
            this.btnLocGenReset.TabIndex = 23;
            this.btnLocGenReset.Text = " Reset All";
            this.btnLocGenReset.UseVisualStyleBackColor = false;
            this.btnLocGenReset.Click += new System.EventHandler(this.btnLocGenReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(442, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Re-initialize Polling";
            // 
            // checkBoxAllOrgs
            // 
            this.checkBoxAllOrgs.AutoSize = true;
            this.checkBoxAllOrgs.Location = new System.Drawing.Point(180, 61);
            this.checkBoxAllOrgs.Name = "checkBoxAllOrgs";
            this.checkBoxAllOrgs.Size = new System.Drawing.Size(70, 17);
            this.checkBoxAllOrgs.TabIndex = 4;
            this.checkBoxAllOrgs.Text = "Select All";
            this.checkBoxAllOrgs.UseVisualStyleBackColor = true;
            this.checkBoxAllOrgs.CheckedChanged += new System.EventHandler(this.checkBoxAllOrgs_CheckedChanged);
            // 
            // checkBoxLocGen
            // 
            this.checkBoxLocGen.AutoSize = true;
            this.checkBoxLocGen.Location = new System.Drawing.Point(450, 61);
            this.checkBoxLocGen.Name = "checkBoxLocGen";
            this.checkBoxLocGen.Size = new System.Drawing.Size(70, 17);
            this.checkBoxLocGen.TabIndex = 28;
            this.checkBoxLocGen.Text = "Select All";
            this.checkBoxLocGen.UseVisualStyleBackColor = true;
            this.checkBoxLocGen.CheckedChanged += new System.EventHandler(this.checkBoxLocGen_CheckedChanged);
            // 
            // lblStoppedLed
            // 
            this.lblStoppedLed.AutoSize = true;
            this.lblStoppedLed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoppedLed.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblStoppedLed.Location = new System.Drawing.Point(56, 271);
            this.lblStoppedLed.Name = "lblStoppedLed";
            this.lblStoppedLed.Size = new System.Drawing.Size(50, 13);
            this.lblStoppedLed.TabIndex = 32;
            this.lblStoppedLed.Text = " Stopped";
            // 
            // picPollStopedLed
            // 
            this.picPollStopedLed.Image = global::SmsMon.Properties.Resources.led_rex12x12;
            this.picPollStopedLed.Location = new System.Drawing.Point(38, 274);
            this.picPollStopedLed.Name = "picPollStopedLed";
            this.picPollStopedLed.Size = new System.Drawing.Size(13, 13);
            this.picPollStopedLed.TabIndex = 29;
            this.picPollStopedLed.TabStop = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.DarkCyan;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.Yellow;
            this.btnBack.Location = new System.Drawing.Point(27, 309);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(79, 23);
            this.btnBack.TabIndex = 33;
            this.btnBack.Text = " <= Back";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.groupBox2.Controls.Add(this.rbTimer);
            this.groupBox2.Controls.Add(this.rbPoll);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkCyan;
            this.groupBox2.Location = new System.Drawing.Point(451, 165);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 64);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Poll Type";
            // 
            // rbTimer
            // 
            this.rbTimer.AutoSize = true;
            this.rbTimer.Location = new System.Drawing.Point(6, 39);
            this.rbTimer.Name = "rbTimer";
            this.rbTimer.Size = new System.Drawing.Size(70, 20);
            this.rbTimer.TabIndex = 36;
            this.rbTimer.TabStop = true;
            this.rbTimer.Text = "Timed";
            this.rbTimer.UseVisualStyleBackColor = true;
            this.rbTimer.CheckedChanged += new System.EventHandler(this.rbTimer_CheckedChanged);
            // 
            // rbPoll
            // 
            this.rbPoll.AutoSize = true;
            this.rbPoll.BackColor = System.Drawing.Color.Transparent;
            this.rbPoll.Location = new System.Drawing.Point(6, 18);
            this.rbPoll.Name = "rbPoll";
            this.rbPoll.Size = new System.Drawing.Size(70, 20);
            this.rbPoll.TabIndex = 0;
            this.rbPoll.TabStop = true;
            this.rbPoll.Text = "polled";
            this.rbPoll.UseVisualStyleBackColor = false;
            this.rbPoll.CheckedChanged += new System.EventHandler(this.rbPoll_CheckedChanged);
            // 
            // listGenLoc
            // 
            this.listGenLoc.AllowDrop = true;
            this.listGenLoc.FormattingEnabled = true;
            this.listGenLoc.Location = new System.Drawing.Point(269, 57);
            this.listGenLoc.Name = "listGenLoc";
            this.listGenLoc.Size = new System.Drawing.Size(170, 160);
            this.listGenLoc.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.CadetBlue;
            this.label2.Location = new System.Drawing.Point(268, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "Network Status";
            // 
            // btnNetworkStatus
            // 
            this.btnNetworkStatus.BackColor = System.Drawing.Color.Red;
            this.btnNetworkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNetworkStatus.ForeColor = System.Drawing.Color.White;
            this.btnNetworkStatus.Location = new System.Drawing.Point(367, 268);
            this.btnNetworkStatus.Name = "btnNetworkStatus";
            this.btnNetworkStatus.Size = new System.Drawing.Size(66, 28);
            this.btnNetworkStatus.TabIndex = 39;
            this.btnNetworkStatus.Text = "Fail";
            this.btnNetworkStatus.UseVisualStyleBackColor = false;
            // 
            // btnGsmModule
            // 
            this.btnGsmModule.BackColor = System.Drawing.Color.Red;
            this.btnGsmModule.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGsmModule.ForeColor = System.Drawing.Color.White;
            this.btnGsmModule.Location = new System.Drawing.Point(367, 229);
            this.btnGsmModule.Name = "btnGsmModule";
            this.btnGsmModule.Size = new System.Drawing.Size(66, 28);
            this.btnGsmModule.TabIndex = 38;
            this.btnGsmModule.Text = "Fail";
            this.btnGsmModule.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.CadetBlue;
            this.label4.Location = new System.Drawing.Point(282, 240);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "GSM Module";
            // 
            // PollGens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(549, 344);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNetworkStatus);
            this.Controls.Add(this.btnGsmModule);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listGenLoc);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblStoppedLed);
            this.Controls.Add(this.picPollStopedLed);
            this.Controls.Add(this.checkBoxLocGen);
            this.Controls.Add(this.checkBoxAllOrgs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnLocGenReset);
            this.Controls.Add(this.btnLocGenRemove);
            this.Controls.Add(this.btnAddPollGen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBoxOrgs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCustId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PollGens";
            this.Text = "Intensive Monitoring";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PollGens_FormClosing);
            this.Load += new System.EventHandler(this.PollGens_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPollGreenLed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPollStopedLed)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCustId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxOrgs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddPollGen;
        private System.Windows.Forms.Button btnLocGenRemove;
        private System.Windows.Forms.Button btnLocGenReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxAllOrgs;
        private System.Windows.Forms.CheckBox checkBoxLocGen;
        private System.Windows.Forms.PictureBox picPollStopedLed;
        private System.Windows.Forms.Label lblPollSeconds;
        private System.Windows.Forms.Label lblStoppedLed;
        private System.Windows.Forms.PictureBox picPollGreenLed;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPoll;
        private System.Windows.Forms.RadioButton rbTimer;
        private System.Windows.Forms.Label lblPolling;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.ListBox listGenLoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNetworkStatus;
        private System.Windows.Forms.Button btnGsmModule;
        private System.Windows.Forms.Label label4;
    }
}