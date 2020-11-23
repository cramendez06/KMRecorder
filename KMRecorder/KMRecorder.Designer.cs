namespace KMRecorder
{
    partial class KMRecorder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KMRecorder));
            this.mHook = new WindowsHookLib.MouseHook();
            this.kHook = new WindowsHookLib.KeyboardHook();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hotkeysToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onWindowsStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noMouseMovementToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayWhenClose = new System.Windows.Forms.ToolStripMenuItem();
            this.TrayWhenMin = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTimeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.pbPlayingTime = new System.Windows.Forms.ProgressBar();
            this.btnPlayback = new System.Windows.Forms.PictureBox();
            this.btnRecord = new System.Windows.Forms.PictureBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlayback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "KMRecorder File|*.kmr";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(241, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.newToolStripMenuItem.Text = "&New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem1
            // 
            this.optionsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playbackToolStripMenuItem,
            this.recordToolStripMenuItem,
            this.hotkeysToolStripMenuItem2,
            this.settingsToolStripMenuItem1});
            this.optionsToolStripMenuItem1.Name = "optionsToolStripMenuItem1";
            this.optionsToolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem1.Text = "&Options";
            // 
            // playbackToolStripMenuItem
            // 
            this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            this.playbackToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.playbackToolStripMenuItem.Text = "&Playback...";
            this.playbackToolStripMenuItem.Click += new System.EventHandler(this.playbackToolStripMenuItem_Click);
            // 
            // recordToolStripMenuItem
            // 
            this.recordToolStripMenuItem.Name = "recordToolStripMenuItem";
            this.recordToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.recordToolStripMenuItem.Text = "&Record...";
            this.recordToolStripMenuItem.Click += new System.EventHandler(this.recordToolStripMenuItem_Click);
            // 
            // hotkeysToolStripMenuItem2
            // 
            this.hotkeysToolStripMenuItem2.Name = "hotkeysToolStripMenuItem2";
            this.hotkeysToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.hotkeysToolStripMenuItem2.Text = "&Hotkeys...";
            this.hotkeysToolStripMenuItem2.Click += new System.EventHandler(this.hotkeysToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.onWindowsStartupToolStripMenuItem,
            this.noMouseMovementToolStripMenuItem1,
            this.TrayWhenClose,
            this.TrayWhenMin});
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // onWindowsStartupToolStripMenuItem
            // 
            this.onWindowsStartupToolStripMenuItem.Name = "onWindowsStartupToolStripMenuItem";
            this.onWindowsStartupToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.onWindowsStartupToolStripMenuItem.Text = "On Window\'s Startup";
            this.onWindowsStartupToolStripMenuItem.Click += new System.EventHandler(this.onWindowsStartupToolStripMenuItem_Click);
            // 
            // noMouseMovementToolStripMenuItem1
            // 
            this.noMouseMovementToolStripMenuItem1.Name = "noMouseMovementToolStripMenuItem1";
            this.noMouseMovementToolStripMenuItem1.Size = new System.Drawing.Size(232, 22);
            this.noMouseMovementToolStripMenuItem1.Text = "No mouse movement";
            this.noMouseMovementToolStripMenuItem1.Click += new System.EventHandler(this.noMouseMovementToolStripMenuItem_Click);
            // 
            // TrayWhenClose
            // 
            this.TrayWhenClose.Name = "TrayWhenClose";
            this.TrayWhenClose.Size = new System.Drawing.Size(232, 22);
            this.TrayWhenClose.Text = "Minimize to Tray if Closed";
            this.TrayWhenClose.Click += new System.EventHandler(this.TrayWhenClose_Click);
            // 
            // TrayWhenMin
            // 
            this.TrayWhenMin.Name = "TrayWhenMin";
            this.TrayWhenMin.Size = new System.Drawing.Size(232, 22);
            this.TrayWhenMin.Text = "Minimize to Tray if Minimized";
            this.TrayWhenMin.Click += new System.EventHandler(this.TrayWhenMin_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.instructionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.instructionsToolStripMenuItem.Text = "&Instructions";
            this.instructionsToolStripMenuItem.Click += new System.EventHandler(this.instructionsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "KMRecorder File|*.kmr";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblTimeStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 120);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(241, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblStatus.Margin = new System.Windows.Forms.Padding(3, 3, 0, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 17);
            this.lblStatus.Text = "No file";
            // 
            // lblTimeStatus
            // 
            this.lblTimeStatus.Margin = new System.Windows.Forms.Padding(140, 3, 0, 2);
            this.lblTimeStatus.Name = "lblTimeStatus";
            this.lblTimeStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTimeStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // pbPlayingTime
            // 
            this.pbPlayingTime.Location = new System.Drawing.Point(0, 116);
            this.pbPlayingTime.Name = "pbPlayingTime";
            this.pbPlayingTime.Size = new System.Drawing.Size(241, 5);
            this.pbPlayingTime.TabIndex = 9;
            // 
            // btnPlayback
            // 
            this.btnPlayback.BackgroundImage = global::KMRecorder.Properties.Resources._1;
            this.btnPlayback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPlayback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlayback.Location = new System.Drawing.Point(127, 27);
            this.btnPlayback.Name = "btnPlayback";
            this.btnPlayback.Size = new System.Drawing.Size(89, 79);
            this.btnPlayback.TabIndex = 8;
            this.btnPlayback.TabStop = false;
            this.btnPlayback.Click += new System.EventHandler(this.btnPlayback_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.BackgroundImage = global::KMRecorder.Properties.Resources._21;
            this.btnRecord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecord.Location = new System.Drawing.Point(26, 27);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(89, 79);
            this.btnRecord.TabIndex = 7;
            this.btnRecord.TabStop = false;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "Program is running\r\nDouble-click icon to show";
            this.notifyIcon.BalloonTipTitle = "KMRecorder";
            this.notifyIcon.Text = "KMRecorder";
            this.notifyIcon.Visible = true;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_BalloonTipClicked);
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // KMRecorder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 142);
            this.Controls.Add(this.pbPlayingTime);
            this.Controls.Add(this.btnPlayback);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "KMRecorder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KMRecorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KMRecorder_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.KMRecorder_FormClosed);
            this.SizeChanged += new System.EventHandler(this.KMRecorder_SizeChanged);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlayback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private WindowsHookLib.MouseHook mHook;
        private WindowsHookLib.KeyboardHook kHook;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.PictureBox btnRecord;
        private System.Windows.Forms.PictureBox btnPlayback;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.ProgressBar pbPlayingTime;
        private System.Windows.Forms.ToolStripStatusLabel lblTimeStatus;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem onWindowsStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noMouseMovementToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hotkeysToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem TrayWhenClose;
        private System.Windows.Forms.ToolStripMenuItem TrayWhenMin;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

