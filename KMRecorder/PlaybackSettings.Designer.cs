namespace KMRecorder
{
    partial class PlaybackSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlaybackSettings));
            this.gbRptSet = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudRpt = new System.Windows.Forms.NumericUpDown();
            this.rbRptNoCount = new System.Windows.Forms.RadioButton();
            this.rbRptCount = new System.Windows.Forms.RadioButton();
            this.gbLoopInt = new System.Windows.Forms.GroupBox();
            this.nudMilSec = new System.Windows.Forms.NumericUpDown();
            this.nudSec = new System.Windows.Forms.NumericUpDown();
            this.nudMin = new System.Windows.Forms.NumericUpDown();
            this.rbMilSec = new System.Windows.Forms.RadioButton();
            this.rbSec = new System.Windows.Forms.RadioButton();
            this.rbMin = new System.Windows.Forms.RadioButton();
            this.gbSpdSet = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSpeed = new System.Windows.Forms.TrackBar();
            this.gbRptSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRpt)).BeginInit();
            this.gbLoopInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMilSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).BeginInit();
            this.gbSpdSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // gbRptSet
            // 
            this.gbRptSet.Controls.Add(this.label1);
            this.gbRptSet.Controls.Add(this.nudRpt);
            this.gbRptSet.Controls.Add(this.rbRptNoCount);
            this.gbRptSet.Controls.Add(this.rbRptCount);
            this.gbRptSet.Location = new System.Drawing.Point(12, 12);
            this.gbRptSet.Name = "gbRptSet";
            this.gbRptSet.Size = new System.Drawing.Size(226, 90);
            this.gbRptSet.TabIndex = 0;
            this.gbRptSet.TabStop = false;
            this.gbRptSet.Text = "Repeat Settings";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "times";
            // 
            // nudRpt
            // 
            this.nudRpt.Location = new System.Drawing.Point(110, 29);
            this.nudRpt.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudRpt.Name = "nudRpt";
            this.nudRpt.Size = new System.Drawing.Size(57, 20);
            this.nudRpt.TabIndex = 4;
            this.nudRpt.ValueChanged += new System.EventHandler(this.nudRpt_ValueChanged);
            this.nudRpt.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudRpt_KeyUp);
            // 
            // rbRptNoCount
            // 
            this.rbRptNoCount.AutoSize = true;
            this.rbRptNoCount.Location = new System.Drawing.Point(33, 57);
            this.rbRptNoCount.Name = "rbRptNoCount";
            this.rbRptNoCount.Size = new System.Drawing.Size(123, 17);
            this.rbRptNoCount.TabIndex = 1;
            this.rbRptNoCount.TabStop = true;
            this.rbRptNoCount.Text = "Repeat until stopped";
            this.rbRptNoCount.UseVisualStyleBackColor = true;
            this.rbRptNoCount.CheckedChanged += new System.EventHandler(this.rbRptNoCount_CheckedChanged);
            // 
            // rbRptCount
            // 
            this.rbRptCount.AutoSize = true;
            this.rbRptCount.Location = new System.Drawing.Point(33, 29);
            this.rbRptCount.Name = "rbRptCount";
            this.rbRptCount.Size = new System.Drawing.Size(78, 17);
            this.rbRptCount.TabIndex = 0;
            this.rbRptCount.TabStop = true;
            this.rbRptCount.Text = "Repeat for ";
            this.rbRptCount.UseVisualStyleBackColor = true;
            this.rbRptCount.CheckedChanged += new System.EventHandler(this.rbRptCount_CheckedChanged);
            // 
            // gbLoopInt
            // 
            this.gbLoopInt.Controls.Add(this.nudMilSec);
            this.gbLoopInt.Controls.Add(this.nudSec);
            this.gbLoopInt.Controls.Add(this.nudMin);
            this.gbLoopInt.Controls.Add(this.rbMilSec);
            this.gbLoopInt.Controls.Add(this.rbSec);
            this.gbLoopInt.Controls.Add(this.rbMin);
            this.gbLoopInt.Location = new System.Drawing.Point(12, 108);
            this.gbLoopInt.Name = "gbLoopInt";
            this.gbLoopInt.Size = new System.Drawing.Size(226, 134);
            this.gbLoopInt.TabIndex = 1;
            this.gbLoopInt.TabStop = false;
            this.gbLoopInt.Text = "Loop Interval Settings";
            // 
            // nudMilSec
            // 
            this.nudMilSec.Location = new System.Drawing.Point(123, 95);
            this.nudMilSec.Maximum = new decimal(new int[] {
            59999,
            0,
            0,
            0});
            this.nudMilSec.Name = "nudMilSec";
            this.nudMilSec.Size = new System.Drawing.Size(57, 20);
            this.nudMilSec.TabIndex = 7;
            this.nudMilSec.ValueChanged += new System.EventHandler(this.nudMilSec_ValueChanged);
            this.nudMilSec.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMilSec_KeyUp);
            // 
            // nudSec
            // 
            this.nudSec.Location = new System.Drawing.Point(123, 63);
            this.nudSec.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudSec.Name = "nudSec";
            this.nudSec.Size = new System.Drawing.Size(57, 20);
            this.nudSec.TabIndex = 6;
            this.nudSec.ValueChanged += new System.EventHandler(this.nudSec_ValueChanged);
            this.nudSec.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudSec_KeyUp);
            // 
            // nudMin
            // 
            this.nudMin.Location = new System.Drawing.Point(123, 31);
            this.nudMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudMin.Name = "nudMin";
            this.nudMin.Size = new System.Drawing.Size(57, 20);
            this.nudMin.TabIndex = 5;
            this.nudMin.ValueChanged += new System.EventHandler(this.nudMin_ValueChanged);
            this.nudMin.KeyUp += new System.Windows.Forms.KeyEventHandler(this.nudMin_KeyUp);
            // 
            // rbMilSec
            // 
            this.rbMilSec.AutoSize = true;
            this.rbMilSec.Location = new System.Drawing.Point(33, 95);
            this.rbMilSec.Name = "rbMilSec";
            this.rbMilSec.Size = new System.Drawing.Size(82, 17);
            this.rbMilSec.TabIndex = 2;
            this.rbMilSec.TabStop = true;
            this.rbMilSec.Text = "Milliseconds";
            this.rbMilSec.UseVisualStyleBackColor = true;
            this.rbMilSec.CheckedChanged += new System.EventHandler(this.rbMilSec_CheckedChanged);
            // 
            // rbSec
            // 
            this.rbSec.AutoSize = true;
            this.rbSec.Location = new System.Drawing.Point(33, 63);
            this.rbSec.Name = "rbSec";
            this.rbSec.Size = new System.Drawing.Size(67, 17);
            this.rbSec.TabIndex = 1;
            this.rbSec.TabStop = true;
            this.rbSec.Text = "Seconds";
            this.rbSec.UseVisualStyleBackColor = true;
            this.rbSec.CheckedChanged += new System.EventHandler(this.rbSec_CheckedChanged);
            // 
            // rbMin
            // 
            this.rbMin.AutoSize = true;
            this.rbMin.Location = new System.Drawing.Point(33, 31);
            this.rbMin.Name = "rbMin";
            this.rbMin.Size = new System.Drawing.Size(57, 17);
            this.rbMin.TabIndex = 0;
            this.rbMin.TabStop = true;
            this.rbMin.Text = "Minute";
            this.rbMin.UseVisualStyleBackColor = true;
            this.rbMin.CheckedChanged += new System.EventHandler(this.rbMin_CheckedChanged);
            // 
            // gbSpdSet
            // 
            this.gbSpdSet.Controls.Add(this.label4);
            this.gbSpdSet.Controls.Add(this.label3);
            this.gbSpdSet.Controls.Add(this.label2);
            this.gbSpdSet.Controls.Add(this.tbSpeed);
            this.gbSpdSet.Location = new System.Drawing.Point(12, 248);
            this.gbSpdSet.Name = "gbSpdSet";
            this.gbSpdSet.Size = new System.Drawing.Size(226, 70);
            this.gbSpdSet.TabIndex = 2;
            this.gbSpdSet.TabStop = false;
            this.gbSpdSet.Text = "Speed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(106, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(203, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "+";
            // 
            // tbSpeed
            // 
            this.tbSpeed.LargeChange = 2;
            this.tbSpeed.Location = new System.Drawing.Point(6, 37);
            this.tbSpeed.Maximum = 8;
            this.tbSpeed.Minimum = -8;
            this.tbSpeed.Name = "tbSpeed";
            this.tbSpeed.Size = new System.Drawing.Size(214, 45);
            this.tbSpeed.SmallChange = 2;
            this.tbSpeed.TabIndex = 0;
            this.tbSpeed.TickFrequency = 2;
            this.tbSpeed.ValueChanged += new System.EventHandler(this.tbSpeed_ValueChanged);
            // 
            // PlaybackSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 330);
            this.Controls.Add(this.gbSpdSet);
            this.Controls.Add(this.gbLoopInt);
            this.Controls.Add(this.gbRptSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlaybackSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Playback Settings";
            this.gbRptSet.ResumeLayout(false);
            this.gbRptSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudRpt)).EndInit();
            this.gbLoopInt.ResumeLayout(false);
            this.gbLoopInt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMilSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMin)).EndInit();
            this.gbSpdSet.ResumeLayout(false);
            this.gbSpdSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbSpeed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbRptSet;
        private System.Windows.Forms.RadioButton rbRptNoCount;
        private System.Windows.Forms.RadioButton rbRptCount;
        private System.Windows.Forms.NumericUpDown nudRpt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbLoopInt;
        private System.Windows.Forms.RadioButton rbMilSec;
        private System.Windows.Forms.RadioButton rbSec;
        private System.Windows.Forms.RadioButton rbMin;
        private System.Windows.Forms.NumericUpDown nudMilSec;
        private System.Windows.Forms.NumericUpDown nudSec;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.GroupBox gbSpdSet;
        private System.Windows.Forms.TrackBar tbSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}