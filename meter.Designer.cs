namespace SmsMon
{
    partial class meter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(meter));
            this.aquaGauge1 = new AquaControls.AquaGauge();
            this.lblGenID = new System.Windows.Forms.Label();
            this.lblvolt0 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gen_id = new System.Windows.Forms.ComboBox();
            this.maintDue = new System.Windows.Forms.TextBox();
            this.maintMonths = new System.Windows.Forms.TextBox();
            this.maintDays = new System.Windows.Forms.TextBox();
            this.maintHours = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.engRhrs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aquaGauge1
            // 
            this.aquaGauge1.BackColor = System.Drawing.Color.Transparent;
            this.aquaGauge1.DecimalPlaces = 0;
            this.aquaGauge1.DialAlpha = 255;
            this.aquaGauge1.DialBorderColor = System.Drawing.SystemColors.Control;
            this.aquaGauge1.DialColor = System.Drawing.SystemColors.Control;
            this.aquaGauge1.DialText = null;
            this.aquaGauge1.DialTextColor = System.Drawing.Color.Black;
            this.aquaGauge1.DialTextFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aquaGauge1.DialTextVOffset = 0;
            this.aquaGauge1.DigitalValue = 0F;
            this.aquaGauge1.DigitalValueBackAlpha = 1;
            this.aquaGauge1.DigitalValueBackColor = System.Drawing.SystemColors.Control;
            this.aquaGauge1.DigitalValueColor = System.Drawing.SystemColors.Control;
            this.aquaGauge1.DigitalValueDecimalPlaces = 0;
            this.aquaGauge1.Glossiness = 40F;
            this.aquaGauge1.Location = new System.Drawing.Point(12, 36);
            this.aquaGauge1.MaxValue = 300F;
            this.aquaGauge1.MinValue = 0F;
            this.aquaGauge1.Name = "aquaGauge1";
            this.aquaGauge1.NoOfDivisions = 5;
            this.aquaGauge1.NoOfSubDivisions = 5;
            this.aquaGauge1.PointerColor = System.Drawing.Color.Black;
            this.aquaGauge1.RimAlpha = 255;
            this.aquaGauge1.RimColor = System.Drawing.Color.Black;
            this.aquaGauge1.ScaleColor = System.Drawing.Color.Black;
            this.aquaGauge1.ScaleFontSizeDivider = 18;
            this.aquaGauge1.Size = new System.Drawing.Size(136, 136);
            this.aquaGauge1.TabIndex = 0;
            this.aquaGauge1.Threshold1Color = System.Drawing.Color.LawnGreen;
            this.aquaGauge1.Threshold1Start = 0F;
            this.aquaGauge1.Threshold1Stop = 0F;
            this.aquaGauge1.Threshold2Color = System.Drawing.Color.Red;
            this.aquaGauge1.Threshold2Start = 0F;
            this.aquaGauge1.Threshold2Stop = 0F;
            this.aquaGauge1.Value = 0F;
            this.aquaGauge1.ValueToDigital = false;
            this.aquaGauge1.Load += new System.EventHandler(this.aquaGauge1_Load);
            // 
            // lblGenID
            // 
            this.lblGenID.AutoSize = true;
            this.lblGenID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGenID.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblGenID.Location = new System.Drawing.Point(12, 9);
            this.lblGenID.Name = "lblGenID";
            this.lblGenID.Size = new System.Drawing.Size(84, 13);
            this.lblGenID.TabIndex = 1;
            this.lblGenID.Text = "Generator ID:";
            // 
            // lblvolt0
            // 
            this.lblvolt0.AutoSize = true;
            this.lblvolt0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvolt0.Location = new System.Drawing.Point(53, 143);
            this.lblvolt0.Name = "lblvolt0";
            this.lblvolt0.Size = new System.Drawing.Size(26, 16);
            this.lblvolt0.TabIndex = 2;
            this.lblvolt0.Text = "0V";
            // 
            // gen_id
            // 
            this.gen_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gen_id.FormattingEnabled = true;
            this.gen_id.Location = new System.Drawing.Point(102, 6);
            this.gen_id.Name = "gen_id";
            this.gen_id.Size = new System.Drawing.Size(139, 21);
            this.gen_id.TabIndex = 3;
            // 
            // maintDue
            // 
            this.maintDue.BackColor = System.Drawing.SystemColors.Window;
            this.maintDue.Location = new System.Drawing.Point(28, 231);
            this.maintDue.Name = "maintDue";
            this.maintDue.Size = new System.Drawing.Size(213, 20);
            this.maintDue.TabIndex = 4;
            // 
            // maintMonths
            // 
            this.maintMonths.Location = new System.Drawing.Point(28, 273);
            this.maintMonths.Name = "maintMonths";
            this.maintMonths.Size = new System.Drawing.Size(42, 20);
            this.maintMonths.TabIndex = 5;
            // 
            // maintDays
            // 
            this.maintDays.Location = new System.Drawing.Point(76, 273);
            this.maintDays.Name = "maintDays";
            this.maintDays.Size = new System.Drawing.Size(44, 20);
            this.maintDays.TabIndex = 6;
            // 
            // maintHours
            // 
            this.maintHours.Location = new System.Drawing.Point(126, 273);
            this.maintHours.Name = "maintHours";
            this.maintHours.Size = new System.Drawing.Size(47, 20);
            this.maintHours.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label1.Location = new System.Drawing.Point(25, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Maintenance Due Time:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label2.Location = new System.Drawing.Point(25, 257);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Months    Days     Time:";
            // 
            // engRhrs
            // 
            this.engRhrs.Location = new System.Drawing.Point(334, 231);
            this.engRhrs.Name = "engRhrs";
            this.engRhrs.Size = new System.Drawing.Size(164, 20);
            this.engRhrs.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkCyan;
            this.label3.Location = new System.Drawing.Point(331, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Engine Running Hours:";
            // 
            // meter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 335);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.engRhrs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.maintHours);
            this.Controls.Add(this.maintDays);
            this.Controls.Add(this.maintMonths);
            this.Controls.Add(this.maintDue);
            this.Controls.Add(this.gen_id);
            this.Controls.Add(this.lblvolt0);
            this.Controls.Add(this.lblGenID);
            this.Controls.Add(this.aquaGauge1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "meter";
            this.Text = "Analog View";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AquaControls.AquaGauge aquaGauge1;
        private System.Windows.Forms.Label lblGenID;
        private System.Windows.Forms.Label lblvolt0;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox gen_id;
        private System.Windows.Forms.TextBox maintDue;
        private System.Windows.Forms.TextBox maintMonths;
        private System.Windows.Forms.TextBox maintDays;
        private System.Windows.Forms.TextBox maintHours;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox engRhrs;
        private System.Windows.Forms.Label label3;
    }
}