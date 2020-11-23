namespace KMRecorder
{
    partial class Hotkeys
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Hotkeys));
            this.label1 = new System.Windows.Forms.Label();
            this.tbNew = new System.Windows.Forms.TextBox();
            this.tbRecord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPlayback = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPause = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.tbNewRec = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "New";
            // 
            // tbNew
            // 
            this.tbNew.Location = new System.Drawing.Point(190, 21);
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(72, 20);
            this.tbNew.TabIndex = 1;
            this.tbNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // tbRecord
            // 
            this.tbRecord.Location = new System.Drawing.Point(190, 47);
            this.tbRecord.Name = "tbRecord";
            this.tbRecord.Size = new System.Drawing.Size(72, 20);
            this.tbRecord.TabIndex = 3;
            this.tbRecord.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Record/Pause Record";
            // 
            // tbPlayback
            // 
            this.tbPlayback.Location = new System.Drawing.Point(190, 100);
            this.tbPlayback.Name = "tbPlayback";
            this.tbPlayback.Size = new System.Drawing.Size(72, 20);
            this.tbPlayback.TabIndex = 5;
            this.tbPlayback.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Playback/Stop Playback";
            // 
            // tbPause
            // 
            this.tbPause.Location = new System.Drawing.Point(190, 126);
            this.tbPause.Name = "tbPause";
            this.tbPause.Size = new System.Drawing.Size(72, 20);
            this.tbPause.TabIndex = 7;
            this.tbPause.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pause/Resume Playback";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(190, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // tbNewRec
            // 
            this.tbNewRec.Location = new System.Drawing.Point(190, 74);
            this.tbNewRec.Name = "tbNewRec";
            this.tbNewRec.Size = new System.Drawing.Size(72, 20);
            this.tbNewRec.TabIndex = 10;
            this.tbNewRec.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "New with Record/Pause Record";
            // 
            // Hotkeys
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 205);
            this.Controls.Add(this.tbNewRec);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbPause);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbPlayback);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRecord);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNew);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hotkeys";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hotkeys";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNew;
        private System.Windows.Forms.TextBox tbRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPlayback;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPause;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbNewRec;
        private System.Windows.Forms.Label label5;
    }
}