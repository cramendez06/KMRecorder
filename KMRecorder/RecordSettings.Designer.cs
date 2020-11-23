namespace KMRecorder
{
    partial class RecordSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecordSettings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMouseClick = new System.Windows.Forms.CheckBox();
            this.cbKeyboard = new System.Windows.Forms.CheckBox();
            this.cbMouseMove = new System.Windows.Forms.CheckBox();
            this.cbTime = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTime);
            this.groupBox1.Controls.Add(this.cbMouseMove);
            this.groupBox1.Controls.Add(this.cbKeyboard);
            this.groupBox1.Controls.Add(this.cbMouseClick);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Events to record";
            // 
            // cbMouseClick
            // 
            this.cbMouseClick.AutoSize = true;
            this.cbMouseClick.Location = new System.Drawing.Point(30, 29);
            this.cbMouseClick.Name = "cbMouseClick";
            this.cbMouseClick.Size = new System.Drawing.Size(83, 17);
            this.cbMouseClick.TabIndex = 0;
            this.cbMouseClick.Text = "Mouse click";
            this.cbMouseClick.UseVisualStyleBackColor = true;
            this.cbMouseClick.CheckedChanged += new System.EventHandler(this.cbMouseClick_CheckedChanged);
            // 
            // cbKeyboard
            // 
            this.cbKeyboard.AutoSize = true;
            this.cbKeyboard.Location = new System.Drawing.Point(30, 63);
            this.cbKeyboard.Name = "cbKeyboard";
            this.cbKeyboard.Size = new System.Drawing.Size(71, 17);
            this.cbKeyboard.TabIndex = 1;
            this.cbKeyboard.Text = "Keyboard";
            this.cbKeyboard.UseVisualStyleBackColor = true;
            this.cbKeyboard.CheckedChanged += new System.EventHandler(this.cbKeyboard_CheckedChanged);
            // 
            // cbMouseMove
            // 
            this.cbMouseMove.AutoSize = true;
            this.cbMouseMove.Location = new System.Drawing.Point(133, 29);
            this.cbMouseMove.Name = "cbMouseMove";
            this.cbMouseMove.Size = new System.Drawing.Size(87, 17);
            this.cbMouseMove.TabIndex = 2;
            this.cbMouseMove.Text = "Mouse move";
            this.cbMouseMove.UseVisualStyleBackColor = true;
            this.cbMouseMove.CheckedChanged += new System.EventHandler(this.cbMouseMove_CheckedChanged);
            // 
            // cbTime
            // 
            this.cbTime.AutoSize = true;
            this.cbTime.Location = new System.Drawing.Point(133, 63);
            this.cbTime.Name = "cbTime";
            this.cbTime.Size = new System.Drawing.Size(49, 17);
            this.cbTime.TabIndex = 3;
            this.cbTime.Text = "Time";
            this.cbTime.UseVisualStyleBackColor = true;
            this.cbTime.CheckedChanged += new System.EventHandler(this.cbTime_CheckedChanged);
            // 
            // RecordSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 127);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Record Settings";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbTime;
        private System.Windows.Forms.CheckBox cbMouseMove;
        private System.Windows.Forms.CheckBox cbKeyboard;
        private System.Windows.Forms.CheckBox cbMouseClick;
    }
}