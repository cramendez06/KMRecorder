using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMRecorder
{
    public partial class KMRecorder : Form
    {
        ///----------------------------------------------------------------------/
        ///
        /// System name：KMRecorder V1
        /// Program title：KMRecorder
        /// Overview: Keyboard and Mouse Recorder
        ///
        ///      Author： M.Mendez    CREATE 2017/12/04          【P-10000】
        ///      
        ///     Copyright (C)
        ///----------------------------------------------------------------------/
        //=======================================================================
        //                        Declaration of Variables
        //=======================================================================
        private List<string> m_eventLines; //Main events array storage
        private Stopwatch m_stopwatchRec; //Stopwatch for recording
        private Stopwatch m_stopwatchPlay; //Stopwatch for playing

        private int m_recNo; //New file number in current session
        private long m_openFileElapsedTime; //Last elapsed time of an opened file
        private long m_lastElapsedTime; //Last elapsed time in the current events array

        private string m_currentFile; //Current file name and path
        private string m_strNewAndRec; //String to concat if New and Record

        private Image m_disabledRec; //Disabled image of Record button
        private Image m_disabledPlay; //Disabled image of Playback button

        private bool m_isDoubleClicked; //If the event recorder is Double click
        private bool m_isPlaying; //If there is a file currently playing
        private bool m_isPaused; //If Playing is paused
        private bool m_isCancelled; //Is new file cancelled
        private bool m_isUnsaved; //If there is an unsaved file
        private bool m_isPendingDialog; //If there is already a pending dialog

        public static IniFile m_iniFile; //Main .ini file
        public static string m_iniPath; //.ini file path
        public static IniFile.IniSection m_sSection; //.ini file section

        public static Keys m_hkNew; //Hotkey for New
        public static Keys m_hkRecord; //Hotkey for Record
        public static Keys m_hkNewRec; //Hotkey for New and Record
        public static Keys m_hkPlayback; //Hotkey for Playback
        public static Keys m_hkPause; //Hotkey for Pause

        public static string m_strNew; //Key string for New
        public static string m_strRecord; //Key string for Record
        public static string m_strNewRec; //Key string for New and Record
        public static string m_strPlayback; //Key string for Playback
        public static string m_strPause; //Key string for Pause

        public static int m_iniRepeatInterval; //.ini Setting for Repeat Intervals
        public static int m_iniRepeatNo; //.ini Setting for Repeat no.
        public static int m_iniSpeed; //.ini Setting for Speed

        public static bool m_iniMouseClick; //.ini Setting for Mouse click recording
        public static bool m_iniMouseMove; //.ini Setting for Mouse move recording
        public static bool m_iniKeyboard; //.ini Setting for Keyboard recording
        public static bool m_iniTime; //.ini Setting for Time recording

        private bool m_iniOnTop; //.ini Setting for Always on top
        private bool m_iniOnStartUp; //.ini Setting for On Window's Startup
        private bool m_iniNoMouseMove; //.ini Setting for No Mouse Movement
        private bool m_iniTrayWhenMin; //.ini Setting for Minimize to Tray When Minimized
        private bool m_iniTrayWhenClose; //.ini Setting for Minimize to Tray When Close

        #region Structure Constants
        /// <summary>
        /// Event Constants
        /// </summary>
        public struct KMEvents
        {
            public const string MOUSEDOWN = "0";
            public const string MOUSEUP = "1";
            public const string MOUSEDOUBLECLICK = "2";
            public const string MOUSEWHEEL = "3";
            public const string MOUSEMOVE = "4";
            public const string KEYDOWN = "5";
            public const string KEYUP = "6";
        }

        /// <summary>
        /// Detail Constants
        /// </summary>
        public struct KMDetails
        {
            public const string LEFT = "Left";
            public const string RIGHT = "Right";
            public const string MIDDLE = "Middle";
        }
        #endregion

        #region Win32Api
        //Constant Variables
        public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;
        public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        public const uint MOUSEEVENTF_LEFTUP = 0x0004;
        public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        public const uint MOUSEEVENTF_MOVE = 0x0001;
        public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        public const uint MOUSEEVENTF_XDOWN = 0x0080;
        public const uint MOUSEEVENTF_XUP = 0x0100;
        public const uint MOUSEEVENTF_WHEEL = 0x0800;
        public const uint MOUSEEVENTF_HWHEEL = 0x01000;

        public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        public const uint KEYEVENTF_KEYUP = 0x0002;

        //Mouse Event API
        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        //Keyboard Event API
        [DllImport("user32.dll")]
        static extern void keybd_event(uint bVk, int bScan, uint dwFlags, int dwExtraInfo);

        //Set Cursor Position
        [DllImport("User32.dll")]
        public static extern void SetCursorPos(int X, int Y);
        #endregion

        /// <summary>
        /// Main Constructor Method
        /// </summary>
        public KMRecorder()
        {
            InitializeReferences();
            InitializeComponent();
            InitializeIni();
            InitializeHooks();
            InitializeDisplays();
            
        }

        #region Initialization
        /// <summary>
        /// Initialize DLL References
        /// </summary>
        private void InitializeReferences() {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                string resourceName = new AssemblyName(args.Name).Name + ".dll";
                string resource = Array.Find(this.GetType().Assembly.GetManifestResourceNames(), element => element.EndsWith(resourceName));

                using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    Byte[] assemblyData = new Byte[stream.Length];
                    stream.Read(assemblyData, 0, assemblyData.Length);
                    return Assembly.Load(assemblyData);
                }
            };
        }

        /// <summary>
        /// Initialize Keyboard and Mouse hooks events
        /// </summary>
        private void InitializeHooks()
        {
            mHook.MouseDown += MouseHook_MouseDown;
            mHook.MouseUp += MouseHook_MouseUp;
            mHook.MouseDoubleClick += MouseHook_MouseDoubleClick;
            mHook.MouseWheel += MouseHook_MouseWheel;
            mHook.MouseMove += MouseHook_MouseMove;
            kHook.KeyDown += KeyboardHook_KeyDown;
            kHook.KeyUp += KeyboardHook_KeyUp;

            kHook.InstallHook();
        }

        /// <summary>
        /// Initialize .ini file settings
        /// </summary>
        private void InitializeIni()
        {
            m_iniPath = System.AppDomain.CurrentDomain.BaseDirectory + "KMRecorderSettings.ini";

            //Checks if the cursor doesn't exist
            if (!File.Exists(m_iniPath))
            {
                //Create .ini file
                CreateIniFile();
                //Set the cursor file as hidden
                File.SetAttributes(m_iniPath, File.GetAttributes(m_iniPath) | FileAttributes.Hidden);
            }

            m_iniFile = new IniFile();
            m_iniFile.Load(m_iniPath);
            m_sSection = m_iniFile.GetSection("Settings");

            GetPlaybackSettings();
            GetRecordSettings();
            GetHotkeys();
            GetOtherSettings();
        }

        /// <summary>
        /// Initialize Displays and Flags
        /// </summary>
        private void InitializeDisplays()
        {
            m_disabledPlay = ToolStripRenderer.CreateDisabledImage(Properties.Resources._1);
            m_disabledRec = ToolStripRenderer.CreateDisabledImage(Properties.Resources._21);
            btnRecord.BackgroundImage = m_disabledRec;
            btnPlayback.BackgroundImage = m_disabledPlay;
            btnRecord.Enabled = false;
            btnPlayback.Enabled = false;
            m_isPlaying = false;
            m_isDoubleClicked = false;
            m_isPaused = false;

            NewFile();
        }

        /// <summary>
        /// Create .ini file if non-existent
        /// </summary>
        public void CreateIniFile()
        {
            m_iniFile = new IniFile(m_iniPath);

            m_iniFile.AddSection("Settings").AddKey("Time").Value = "True";
            m_iniFile.AddSection("Settings").AddKey("Keyboard").Value = "True";
            m_iniFile.AddSection("Settings").AddKey("OnStartUp").Value = "False";
            m_iniFile.AddSection("Settings").AddKey("MouseMove").Value = "True";
            m_iniFile.AddSection("Settings").AddKey("TrayWhenClose").Value = "False";
            m_iniFile.AddSection("Settings").AddKey("TrayWhenMin").Value = "False";
            m_iniFile.AddSection("Settings").AddKey("MouseClick").Value = "True";
            m_iniFile.AddSection("Settings").AddKey("OnTop").Value = "False";
            m_iniFile.AddSection("Settings").AddKey("hkRecord").Value = "F4";
            m_iniFile.AddSection("Settings").AddKey("hkNew").Value = "F3";
            m_iniFile.AddSection("Settings").AddKey("hkPause").Value = "Pause";
            m_iniFile.AddSection("Settings").AddKey("hkPlayback").Value = "F2";
            m_iniFile.AddSection("Settings").AddKey("NoMouseMove").Value = "False";
            m_iniFile.AddSection("Settings").AddKey("hkNewRec").Value = "F1";
            m_iniFile.AddSection("Settings").AddKey("RepeatNo").Value = "0";
            m_iniFile.AddSection("Settings").AddKey("RepeatInterval").Value = "0";
            m_iniFile.AddSection("Settings").AddKey("Speed").Value = "0";

            m_iniFile.Save(m_iniPath);
        }
        #endregion

        #region GetSettings
        /// <summary>
        /// Get Hotkeys Settings
        /// </summary>
        private void GetHotkeys()
        {
            m_strNew = m_iniFile.GetKeyValue("Settings", "hkNew");
            m_strRecord = m_iniFile.GetKeyValue("Settings", "hkRecord");
            m_strNewRec = m_iniFile.GetKeyValue("Settings", "hkNewRec");
            m_strPlayback = m_iniFile.GetKeyValue("Settings", "hkPlayback");
            m_strPause = m_iniFile.GetKeyValue("Settings", "hkPause");

            m_hkNew = ConvertToKeys(m_strNew);
            m_hkRecord = ConvertToKeys(m_strRecord);
            m_hkNewRec = ConvertToKeys(m_strNewRec);
            m_hkPlayback = ConvertToKeys(m_strPlayback);
            m_hkPause = ConvertToKeys(m_strPause);
        }

        /// <summary>
        /// Get Playback Settings
        /// </summary>
        private void GetPlaybackSettings()
        {
            m_iniRepeatInterval = Convert.ToInt32(m_iniFile.GetKeyValue("Settings", "RepeatInterval"));
            m_iniRepeatNo = Convert.ToInt32(m_iniFile.GetKeyValue("Settings", "RepeatNo"));
            m_iniSpeed = Convert.ToInt32(m_iniFile.GetKeyValue("Settings", "Speed"));
        }

        /// <summary>
        /// Get Record Settings
        /// </summary>
        private void GetRecordSettings()
        {
            m_iniMouseClick = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "MouseClick"));
            m_iniMouseMove = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "MouseMove"));
            m_iniKeyboard = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "Keyboard"));
            m_iniTime = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "Time"));
        }

        /// <summary>
        /// Get Misc Settings
        /// </summary>
        private void GetOtherSettings()
        {
            m_iniOnTop = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "OnTop"));
            m_iniOnStartUp = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "OnStartUp"));
            m_iniNoMouseMove = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "NoMouseMove"));
            m_iniTrayWhenMin = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "TrayWhenMin"));
            m_iniTrayWhenClose = Convert.ToBoolean(m_iniFile.GetKeyValue("Settings", "TrayWhenClose"));

            alwaysOnTopToolStripMenuItem.Checked = m_iniOnTop;
            onWindowsStartupToolStripMenuItem.Checked = m_iniOnStartUp;
            noMouseMovementToolStripMenuItem1.Checked = m_iniNoMouseMove;
            TrayWhenClose.Checked = m_iniTrayWhenClose;
            TrayWhenMin.Checked = m_iniTrayWhenMin;
        }
        #endregion

        #region EventHooks
        /// <summary>
        /// Global Mouse Down Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHook_MouseDown(object sender, WindowsHookLib.MouseEventArgs e)
        {
            //Checks if Mouse Click is recordable
            if (m_iniMouseClick)
                //Checks if Time is recordable
                if (m_iniTime)
                {
                    //Get new elapsed time for opened files and record it
                    m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                    m_eventLines.Add(KMEvents.MOUSEDOWN + ";" + e.Button + ";" + m_lastElapsedTime);
                }
                else
                {
                    //Record Mouse Down Event
                    m_eventLines.Add(KMEvents.MOUSEDOWN + ";" + e.Button);
                }
        }

        /// <summary>
        /// Global Mouse Up Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHook_MouseUp(object sender, WindowsHookLib.MouseEventArgs e)
        {
            //Checks if Double Click Flag is not true
            if (!m_isDoubleClicked)
            {
                //Checks if Mouse Click is recordable
                if (m_iniMouseClick)
                    //Checks if Time is recordable
                    if (m_iniTime)
                    {
                        //Get new elapsed time for opened files and record it
                        m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                        m_eventLines.Add(KMEvents.MOUSEUP + ";" + e.Button + ";" + m_lastElapsedTime);
                    }
                    else
                    {
                        //Record Mouse Up Event
                        m_eventLines.Add(KMEvents.MOUSEUP + ";" + e.Button);
                    }
            }
            else
            {
                //Set Double Click Flag to false
                m_isDoubleClicked = false;
            }
        }

        /// <summary>
        /// Global Mouse Double Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHook_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //Remove initially wrong input detection
            m_eventLines.RemoveRange(m_eventLines.Count - 3, 3);
            //Set Double Click Flag to true
            m_isDoubleClicked = true;
            //Checks if Mouse Click is recordable
            if (m_iniMouseClick)
                //Checks if Time is recordable
                if (m_iniTime)
                {
                    //Get new elapsed time for opened files and record it
                    m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                    m_eventLines.Add(KMEvents.MOUSEDOUBLECLICK + ";" + e.Button + ";" + m_lastElapsedTime);
                }
                else
                {
                    //Record Mouse Double Click Event
                    m_eventLines.Add(KMEvents.MOUSEDOUBLECLICK + ";" + e.Button);
                }
        }

        /// <summary>
        /// Global Mouse Wheel Roll Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHook_MouseWheel(object sender, WindowsHookLib.MouseEventArgs e)
        {
            //Checks if Mouse Click is recordable
            if (m_iniMouseClick)
                //Checks if Time is recordable
                if (m_iniTime)
                {
                    //Get new elapsed time for opened files and record it
                    m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                    m_eventLines.Add(KMEvents.MOUSEWHEEL + ";" + e.Delta + ";" + m_lastElapsedTime);
                }
                else
                {
                    //Record Mouse Wheel Roll Event
                    m_eventLines.Add(KMEvents.MOUSEWHEEL + ";" + e.Delta);
                }

        }

        /// <summary>
        /// Global Mouse Move Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseHook_MouseMove(object sender, WindowsHookLib.MouseEventArgs e)
        {
            //Checks if Mouse Click is recordable
            if (m_iniMouseMove)
                //Checks if Time is recordable
                if (m_iniTime)
                {
                    //Get new elapsed time for opened files and record it
                    m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                    m_eventLines.Add(KMEvents.MOUSEMOVE + ";" + e.Location.X + "," + e.Location.Y + ";" + m_lastElapsedTime);
                }
                else
                {
                    //Record Mouse Move Event
                    m_eventLines.Add(KMEvents.MOUSEMOVE + ";" + e.Location.X + "," + e.Location.Y);
                }
        }

        /// <summary>
        /// Global Keyboard Key Down Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardHook_KeyDown(object sender, WindowsHookLib.KeyboardEventArgs e)
        {
            //Checks if recording is in progress
            if (mHook.State == WindowsHookLib.HookState.Installed)
            {
                //Checks if Keyboard events are recordable
                if (m_iniKeyboard)
                    //Checks if button pressed doesn't belong in the Hotkeys
                    if (e.VirtualKeyCode != m_hkNew &&
                        e.VirtualKeyCode != m_hkNewRec &&
                        e.VirtualKeyCode != m_hkPause &&
                        e.VirtualKeyCode != m_hkPlayback &&
                        e.VirtualKeyCode != m_hkRecord)
                    {
                        //Checks if Time is recordable
                        if (m_iniTime)
                        {
                            //Get new elapsed time for opened files and record it
                            m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                            m_eventLines.Add(KMEvents.KEYDOWN + ";" + e.VirtualKeyCode + ";" + m_lastElapsedTime);
                        }
                        else
                        {
                            //Record Key Down Event
                            m_eventLines.Add(KMEvents.KEYDOWN + ";" + e.VirtualKeyCode);
                        }
                    }
            }
        }

        /// <summary>
        /// Global Keyboard Key Up Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardHook_KeyUp(object sender, WindowsHookLib.KeyboardEventArgs e)
        {
            //Checks if Key pressed is key shortcut for New
            if (e.VirtualKeyCode == m_hkNew)
            {
                newToolStripMenuItem.PerformClick();
                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = "New file created.";
                    notifyIcon.ShowBalloonTip(0);
                }
            }
            //Checks if Key pressed is key shortcut for Record
            else if (e.VirtualKeyCode == m_hkRecord)
            {
                if (btnRecord.Enabled) btnRecord_Click(this, null);
            }
            //Checks if Key pressed is key shortcut for New then Record
            else if (e.VirtualKeyCode == m_hkNewRec)
            {
                //Checks if recording is not in progress
                if (mHook.State != WindowsHookLib.HookState.Installed)
                {
                    m_strNewAndRec = "New file created. ";
                    newToolStripMenuItem.PerformClick();
                    if (btnRecord.Enabled && !m_isCancelled) btnRecord_Click(this, null);
                    m_isCancelled = false;
                    m_strNewAndRec = "";
                }
                else
                {
                    if (btnRecord.Enabled) btnRecord_Click(this, null);
                }
            }
            //Checks if Key pressed is key shortcut for Playback
            else if (e.VirtualKeyCode == m_hkPlayback)
            {
                if (btnPlayback.Enabled) btnPlayback_Click(this, null);
            }
            //Checks if Key pressed is key shortcut for Pause/Unpause Playback
            else if (e.VirtualKeyCode == m_hkPause)
            {
                //Checks if Pause Flag is true
                if (m_isPaused)
                {
                    m_isPaused = false;

                    if (notifyIcon.Visible == true)
                    {
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon.BalloonTipText = "Playback is paused.";
                        notifyIcon.ShowBalloonTip(0);
                    }
                }
                else
                {
                    m_isPaused = true;

                    if (notifyIcon.Visible == true)
                    {
                        notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon.BalloonTipText = "Playback is unpaused";
                        notifyIcon.ShowBalloonTip(0);
                    }
                }
            }
            //Checks if recording is in progress
            else if (mHook.State == WindowsHookLib.HookState.Installed)
            {
                //Checks if Keyboard events are recordable
                if (m_iniKeyboard)
                    //Checks if Time is recordable
                    if (m_iniTime)
                    {
                        //Get new elapsed time for opened files and record it
                        m_lastElapsedTime = m_stopwatchRec.ElapsedMilliseconds + m_openFileElapsedTime;
                        m_eventLines.Add(KMEvents.KEYUP + ";" + e.VirtualKeyCode + ";" + m_lastElapsedTime);
                    }
                    else
                    {
                        //Record Key Up Event
                        m_eventLines.Add(KMEvents.KEYUP + ";" + e.VirtualKeyCode);
                    }
            }
        }
        #endregion

        #region Utils&Functions
        /// <summary>
        /// Playback without time frame
        /// </summary>
        private async void RunPlaybackNoTime()
        {
            //Invoke a delegate for cross-threading of UI changes
            BeginInvoke(new Action(() =>
            {
                btnPlayback.BackgroundImage = Properties.Resources._3;
                btnRecord.BackgroundImage = m_disabledRec;
                btnRecord.Enabled = false;
                pbPlayingTime.Maximum = m_eventLines.Count;
                pbPlayingTime.Value = 0;
                menuStrip.Enabled = false;
            }));

            //Set number of loops
            int w_loops = m_iniRepeatNo + 1;
            if (w_loops == 0) w_loops = 1;

            //Initialize Temporary Array Storage
            string[] w_befData = { "", "", "" };

            //Iterate through number of repeats
            for (int i = 0; i < w_loops; i++)
            {
                //Iterate through all recorded event lines
                foreach (string w_line in m_eventLines.ToList())
                {
                    //Split an event line to 2 data (Event (0), Detail (1))
                    string[] w_data = w_line.Split(new string[] { ";" }, StringSplitOptions.None);

                    //If event is not Mouse Move or No mouse movement setting is false
                    if (w_data[0] != KMEvents.MOUSEMOVE || !noMouseMovementToolStripMenuItem1.Checked)
                    {
                        //Check Event type
                        switch (w_data[0])
                        {
                            //If Mouse Down Event
                            case KMEvents.MOUSEDOWN:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(w_befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Up Event
                            case KMEvents.MOUSEUP:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(w_befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Double Click Event
                            case KMEvents.MOUSEDOUBLECLICK:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(w_befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(1);
                                    //Emulate LMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(1);
                                    //Emulate RMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(1);
                                    //Emulate MMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Wheel Roll Event
                            case KMEvents.MOUSEWHEEL:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(w_befData);

                                //Emulate Mouse Wheel Roll Event
                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Convert.ToInt32(w_data[1]), 0);
                                break;
                            //If Mouse Move Event
                            case KMEvents.MOUSEMOVE:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Set Cursor position from the Detail
                                string[] xy = w_data[1].Split(new string[] { "," }, StringSplitOptions.None);
                                SetCursorPos(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
                                break;
                            //If Key Down Event
                            case KMEvents.KEYDOWN:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Emulate Key Down Event for the Key in Detail
                                keybd_event((uint)ConvertToKeys(w_data[1]), 0, 0, 0);
                                break;
                            //If Key Up Event
                            case KMEvents.KEYUP:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Emulate Key Up Event for the Key Detail
                                keybd_event((uint)ConvertToKeys(w_data[1]), 0, KEYEVENTF_KEYUP, 0);
                                break;
                        }

                        //Delay
                        await Task.Delay(1);
                    }

                    //Checks if Playing flag is false
                    if (!m_isPlaying) break;
                    //Save current data to temporary data
                    w_befData = w_data;

                    //Invoke a delegate for cross-threading of UI changes
                    BeginInvoke(new Action(() =>
                    {
                        pbPlayingTime.Value = ++pbPlayingTime.Value;
                        pbPlayingTime.Value = --pbPlayingTime.Value;
                        pbPlayingTime.Value = ++pbPlayingTime.Value;
                    }));
                }

                //Set delay for each loops
                int w_delay = 1;

                //Checks if Infinite loop
                if (m_iniRepeatNo == -1)
                {
                    //Decrement loop
                    i--;
                    w_delay = m_iniRepeatInterval;
                }
                //Checks if repating for specific number of times
                else if (w_loops > 1)
                {
                    w_delay = m_iniRepeatInterval;
                }

                //Delay
                await Task.Delay(w_delay);
                //Checks if Playing flag is false
                if (!m_isPlaying) break;
            }

            //Invoke a delegate for cross-threading of UI changes
            BeginInvoke(new Action(() =>
            {
                btnPlayback.BackgroundImage = Properties.Resources._1;
                btnRecord.BackgroundImage = Properties.Resources._21;
                m_isPlaying = false;
                m_isPaused = false;
                btnRecord.Enabled = true;
                pbPlayingTime.Value = 0;
                menuStrip.Enabled = true;
            }));
        }

        /// <summary>
        /// Playback with time frame
        /// </summary>
        private async void RunPlayback()
        {
            //Invoke a delegate for cross-threading of UI changes
            BeginInvoke(new Action(() =>
            {
                btnPlayback.BackgroundImage = Properties.Resources._3;
                btnRecord.BackgroundImage = m_disabledRec;
                btnRecord.Enabled = false;
                pbPlayingTime.Maximum = (int)Math.Ceiling(m_lastElapsedTime / 1000.0); //m_eventLines.Count;
                pbPlayingTime.Value = 0;
                menuStrip.Enabled = false;
            }));

            //Set number of loops
            int w_loops = m_iniRepeatNo + 1;
            if (w_loops == 0) w_loops = 1;
            //Initialize Action time variable
            long w_actionTime;

            //Initialize Temporary Array Storage
            string[] befData = { "", "", "" };

            //Iterate through number of repeats
            for (int i = 0; i < w_loops; i++)
            {
                //Instanciate Stopwatch
                m_stopwatchPlay = new Stopwatch();

                //Iterate through all recorded event lines
                foreach (string w_line in m_eventLines.ToList())
                {
                    //Split an event line to 2 data (Event (0), Detail (1))
                    string[] w_data = w_line.Split(new string[] { ";" }, StringSplitOptions.None);
                    //Compute action time based on set speed
                    w_actionTime = ComputeSpeed(m_iniSpeed, Convert.ToInt32(w_data[2]));

                    //Start Stopwatch
                    m_stopwatchPlay.Start();

                    //While Action time is greater than current Elapsed time
                    while (w_actionTime > m_stopwatchPlay.ElapsedMilliseconds)
                    {
                        //Nothing
                    }

                    //Pause Stopwatch
                    m_stopwatchPlay.Stop();

                    //If event is not Mouse Move or No mouse movement setting is false
                    if (w_data[0] != KMEvents.MOUSEMOVE || !noMouseMovementToolStripMenuItem1.Checked)
                    {
                        //Check Event type
                        switch (w_data[0])
                        {
                            //If Mouse Down Event
                            case KMEvents.MOUSEDOWN:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Down Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Up Event
                            case KMEvents.MOUSEUP:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Up Event
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Double Click Event
                            case KMEvents.MOUSEDOUBLECLICK:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(befData);

                                //Checks if the Detail is Left
                                if (w_data[1] == KMDetails.LEFT)
                                {
                                    //Emulate LMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(10);
                                    //Emulate LMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Right
                                else if (w_data[1] == KMDetails.RIGHT)
                                {
                                    //Emulate RMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(10);
                                    //Emulate RMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                //Checks if the Detail is Middle
                                else
                                {
                                    //Emulate MMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                    //Delay
                                    await Task.Delay(10);
                                    //Emulate MMB Mouse Click Event
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                }
                                break;
                            //If Mouse Whell Roll Event
                            case KMEvents.MOUSEWHEEL:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Move Cursor based on the last Event line
                                MoveToClick(befData);

                                //Emulate Mouse Wheel Roll Event
                                mouse_event(MOUSEEVENTF_WHEEL, 0, 0, Convert.ToInt32(w_data[1]), 0);
                                break;
                            //If Mouse Move Event
                            case KMEvents.MOUSEMOVE:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Set Cursor position from the Detail
                                string[] xy = w_data[1].Split(new string[] { "," }, StringSplitOptions.None);
                                SetCursorPos(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
                                break;
                            //If Key Down Event
                            case KMEvents.KEYDOWN:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Emulate Key Down Event for the Key in Detail
                                keybd_event((uint)ConvertToKeys(w_data[1]), 0, 0, 0);
                                break;
                            //If Key Up Event
                            case KMEvents.KEYUP:
                                //While Pause flag is true
                                while (m_isPaused)
                                {
                                    //Checks if Playing flag is false
                                    if (!m_isPlaying) break;
                                    await Task.Delay(1);
                                }

                                //Checks if Playing flag is false
                                if (!m_isPlaying) break;

                                //Emulate Key Up Event for the Key in Detail
                                keybd_event((uint)ConvertToKeys(w_data[1]), 0, KEYEVENTF_KEYUP, 0);
                                break;
                        }
                    }

                    //Checks if Playing flag is false
                    if (!m_isPlaying) break;
                    //Save current data to temporary data
                    befData = w_data;

                    //Invoke a delegate for cross-threading of UI changes
                    BeginInvoke(new Action(() =>
                    {
                        int w_secElapsed = (int)Math.Ceiling(m_stopwatchPlay.ElapsedMilliseconds / 1000.0);

                        if (w_secElapsed < pbPlayingTime.Maximum)
                        {
                            pbPlayingTime.Value = w_secElapsed + 1;
                            pbPlayingTime.Value = w_secElapsed;
                            pbPlayingTime.Value = w_secElapsed + 1;
                        }
                    }));
                }

                //Stop Stopwatch
                m_stopwatchPlay.Stop();

                //Set delay for each loops
                int w_delay = 1;

                //Checks if Infinite loop
                if (m_iniRepeatNo == -1)
                {
                    //Decrement loop
                    i--;
                    w_delay = m_iniRepeatInterval;
                }
                //Checks if repating for specific number of times
                else if (w_loops > 1)
                {
                    w_delay = m_iniRepeatInterval;
                }

                //Delay
                await Task.Delay(w_delay);
                //Checks if Playing flag is false
                if (!m_isPlaying) break;
            }

            //Invoke a delegate for cross-threading of UI changes
            BeginInvoke(new Action(() =>
            {
                btnPlayback.BackgroundImage = Properties.Resources._1;
                btnRecord.BackgroundImage = Properties.Resources._21;
                m_isPlaying = false;
                m_isPaused = false;
                btnRecord.Enabled = true;
                pbPlayingTime.Value = 0;
                menuStrip.Enabled = true;
            }));
        }

        /// <summary>
        /// Compute Time base on Speed set
        /// </summary>
        /// <param name="p_speed">Speed from settings</param>
        /// <param name="p_time">Elapsed Time of Event</param>
        /// <returns>Time of Execution</returns>
        private int ComputeSpeed(int p_speed, int p_time)
        {
            //Checks if Speed is Positive or Increasing
            if (p_speed > 0)
            {
                return p_time / p_speed;
            }
            //Checks if Speed is Negative or Decreasing
            else if (p_speed < 0)
            {
                return p_time * Math.Abs(p_speed);
            }
            //Checks if Speed is 0
            else
            {
                return p_time;
            }
        }

        /// <summary>
        /// New File Event Method
        /// </summary>
        private void NewFile()
        {
            //Display and Variable Initialization
            btnPlayback.Enabled = true;
            btnRecord.Enabled = true;
            btnRecord.BackgroundImage = Properties.Resources._21;
            btnPlayback.BackgroundImage = Properties.Resources._1;
            lblStatus.Image = null;
            m_isUnsaved = false;

            //Create new Recording Stopwatch
            m_stopwatchRec = new Stopwatch();
            //Increment Record No
            m_recNo++;
            lblStatus.Text = "Ready";
            //Reset other variables
            m_currentFile = "";
            m_eventLines = new List<string>();
            m_isPlaying = false;
            m_openFileElapsedTime = 0;
            m_lastElapsedTime = 0;

            //Uninstall MouseHook
            if (mHook.State == WindowsHookLib.HookState.Installed) mHook.RemoveHook();
        }

        /// <summary>
        /// Open File Event Method
        /// </summary>
        /// <param name="p_filePath">Record File Path</param>
        private void OpenFile(string p_filePath)
        {
            //Display and Variable Initialization
            btnPlayback.Enabled = true;
            btnRecord.Enabled = true;
            btnRecord.BackgroundImage = Properties.Resources._21;
            btnPlayback.BackgroundImage = Properties.Resources._1;
            lblStatus.Image = null;
            m_isUnsaved = false;

            //Save filepath to Global Variable
            m_currentFile = p_filePath;
            lblStatus.Text = Path.GetFileName(m_currentFile);

            //Re initialize Variables
            m_eventLines = new List<string>();
            m_stopwatchRec = new Stopwatch();

            //Open File to Stream Reader
            using (StreamReader w_reader = new StreamReader(openFileDialog.FileName))
            {
                //While not in the end
                while (!w_reader.EndOfStream)
                {
                    //Add Event line to Global Array Variable
                    m_eventLines.Add(w_reader.ReadLine());
                }

                //Get Last Elapsed Time
                string[] w_data = m_eventLines.Last().Split(new string[] { ";" }, StringSplitOptions.None);
                if (w_data.Count() == 3) m_openFileElapsedTime = Convert.ToInt64(w_data[2]);
                m_lastElapsedTime = m_openFileElapsedTime;
            }
        }

        /// <summary>
        /// Save File Event Method
        /// </summary>
        /// <param name="p_fileStream">Stream of File to be saved</param>
        /// <param name="p_filePath">Path of File to be saved</param>
        private void SaveFile(Stream p_fileStream = null, string p_filePath = "")
        {
            StreamWriter w_writer = null;

            //Checks if filepath is not empty
            if (p_filePath != "")
            {
                //Open file to Stream Writer
                w_writer = new StreamWriter(p_filePath);
            }
            //Checks if filestream is not null
            else if (p_fileStream != null)
            {
                //Open File Stream to Stream Writer
                w_writer = new StreamWriter(p_fileStream);
            }

            //Iterate through all Event Lines
            foreach (string line in m_eventLines.ToList())
            {
                //Write Event Line to file
                w_writer.WriteLine(line);
            }

            //Dispose Stream Writer
            w_writer.Dispose();
            w_writer.Close();
        }

        /// <summary>
        /// Move to Point for Event
        /// </summary>
        /// <param name="p_befData">Data array</param>
        private void MoveToClick(string[] p_befData)
        {
            //Checks if No mouse Movement is Checked
            if (noMouseMovementToolStripMenuItem1.Checked)
            {
                //If Event is Mouse Move
                if (p_befData[0] == KMEvents.MOUSEMOVE)
                {
                    //Set Cursor position from the Detail
                    string[] w_befxy = p_befData[1].Split(new string[] { "," }, StringSplitOptions.None);
                    SetCursorPos(Convert.ToInt32(w_befxy[0]), Convert.ToInt32(w_befxy[1]));
                }
            }
        }

        /// <summary>
        /// Convert Key String to Virtual KeyCodes
        /// </summary>
        /// <param name="p_virCode">Key String</param>
        /// <returns></returns>
        private Keys ConvertToKeys(string p_virCode)
        {
            KeysConverter w_kc = new KeysConverter();
            Keys w_kys = (Keys)w_kc.ConvertFromString(p_virCode);

            return w_kys;
        }

        /// <summary>
        /// Sets the Application in startup list
        /// </summary>
        /// <param name="isChecked"></param>
        private void RegisterInStartup(bool isChecked)
        {
            //Sets the Registry key
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            //Checks if to be inserted in startup
            if (isChecked)
            {
                registryKey.SetValue(Application.ProductName, Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue(Application.ProductName);
            }
        }
        #endregion

        /// <summary>
        /// Record button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecord_Click(object sender, EventArgs e)
        {
            //Checks if recording
            if (mHook.State == WindowsHookLib.HookState.Installed)
            {
                //Remove Mousehook
                mHook.RemoveHook();

                //Revert Main button display
                btnPlayback.Enabled = true;
                btnPlayback.BackgroundImage = Properties.Resources._1;
                btnRecord.BackgroundImage = Properties.Resources._21;

                //Pauses Record Stopwatch
                m_stopwatchRec.Stop();

                //Unsave status icon displayed
                lblStatus.Image = Properties.Resources.unsaver;
                m_isUnsaved = true;

                menuStrip.Enabled = true;

                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = "Recording is paused/stopped.";
                    notifyIcon.ShowBalloonTip(0);
                }
            }
            else
            {
                //Install Mousehook
                mHook.InstallHook();

                //Change Main button display
                btnPlayback.Enabled = false;
                btnPlayback.BackgroundImage = m_disabledPlay;
                btnRecord.BackgroundImage = Properties.Resources._6;

                //Start/Resume Record Stopwatch
                m_stopwatchRec.Start();

                //Save status icon displayed
                lblStatus.Image = Properties.Resources.icon;
                m_isUnsaved = true;

                menuStrip.Enabled = false;

                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = m_strNewAndRec + "Recording started.";
                    notifyIcon.ShowBalloonTip(0);
                }
            }
        }

        /// <summary>
        /// Playback button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayback_Click(object sender, EventArgs e)
        {
            //Checks if a record is currently not playing
            if (!m_isPlaying)
            {
                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = "Playing recorder file.";
                    notifyIcon.ShowBalloonTip(0);
                }

                m_isPlaying = true;
                //Checks if BackgroundWorker is not running
                if (!backgroundWorker.IsBusy)
                    backgroundWorker.RunWorkerAsync();
            }
            else
            {
                m_isPlaying = false;

                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = "Playing has been stopped.";
                    notifyIcon.ShowBalloonTip(0);
                }
            }
        }

        /// <summary>
        /// BackgroundWorker DoWork Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //Retrieve last event data
                string[] w_data = m_eventLines[0].Split(new string[] { ";" }, StringSplitOptions.None);

                //Checks if the event has recorded time
                if (w_data.Count() == 3)
                {
                    //Run record with time
                    RunPlayback();
                }
                else
                {
                    //Run record without time
                    RunPlaybackNoTime();
                }

                if (notifyIcon.Visible == true)
                {
                    notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon.BalloonTipText = "Playback finished.";
                    notifyIcon.ShowBalloonTip(0);
                }
            }
            catch (Exception)
            {
                m_isPlaying = false;
            }
        }

        /// <summary>
        /// New ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string w_currFile;
            m_isCancelled = true;
            m_isPaused = true;

            //Checks if there is a file loaded
            if (m_currentFile != "")
            {
                w_currFile = Path.GetFileName(m_currentFile);
            }
            else
            {
                w_currFile = "recording_" + m_recNo;
            }

            //Checks if there is an unsaved recording
            if (m_isUnsaved)
            {
                DialogResult w_result;

                //If there is no pending dialog
                if (!m_isPendingDialog)
                {
                    m_isPendingDialog = true;

                    w_result = MessageBox.Show("Do you want to save changes to " + w_currFile + "?", "KMRecorder", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (w_result == DialogResult.Yes)
                    {
                        saveAsToolStripMenuItem_Click(this, null);
                        NewFile();
                        m_isCancelled = false;
                    }
                    else if (w_result == DialogResult.No)
                    {
                        NewFile();
                        m_isCancelled = false;
                    }
                    else if (w_result == DialogResult.Cancel)
                    {
                        //Nothing
                    }

                    m_isPendingDialog = false;
                }
            }
            else
            {
                NewFile();
                m_isCancelled = false;
            }

            m_isPaused = false;
        }

        /// <summary>
        /// Open ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult w_result = openFileDialog.ShowDialog();
            if (w_result == DialogResult.OK)
            {
                OpenFile(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Save ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Checks if File Exist in the current file Path
            if (!File.Exists(m_currentFile))
            {
                //Show Save Dialog
                saveFileDialog.FileName = "recording_" + m_recNo;
                DialogResult w_result = saveFileDialog.ShowDialog();
                if (w_result == DialogResult.OK)
                {
                    m_currentFile = saveFileDialog.FileName;

                    //Save
                    SaveFile(saveFileDialog.OpenFile());

                    lblStatus.Text = Path.GetFileName(m_currentFile);
                    lblStatus.Image = Properties.Resources.save;
                    m_isUnsaved = false;
                }
            }
            else
            {
                //Rewrite whole file
                File.WriteAllText(m_currentFile, String.Empty);
                //Save
                SaveFile(null, m_currentFile);
                lblStatus.Image = Properties.Resources.save;
                m_isUnsaved = false;
            }
        }

        /// <summary>
        /// SaveAs ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Show Save Dialog
            saveFileDialog.FileName = "";
            DialogResult w_result = saveFileDialog.ShowDialog();
            if (w_result == DialogResult.OK)
            {
                m_currentFile = saveFileDialog.FileName;

                //Save
                SaveFile(saveFileDialog.OpenFile());

                lblStatus.Text = Path.GetFileName(m_currentFile);
                lblStatus.Image = Properties.Resources.save;
                m_isUnsaved = false;
            }
        }

        /// <summary>
        /// No Mouse Movement ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void noMouseMovementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declare flag variable
            bool w_noMouseMoveFLG;

            //Checks if alwaysOnTopToolStripMenuItem is checked
            if (noMouseMovementToolStripMenuItem1.Checked)
            {
                noMouseMovementToolStripMenuItem1.Checked = false;
                w_noMouseMoveFLG = false;
            }
            else
            {
                noMouseMovementToolStripMenuItem1.Checked = true;
                w_noMouseMoveFLG = true;
            }

            //Change OnTop Settings and saves it
            m_sSection.RemoveKey("NoMouseMove");
            m_sSection.AddKey("NoMouseMove").Value = w_noMouseMoveFLG + "";
            m_iniFile.Save(m_iniPath);
        }

        /// <summary>
        /// Hotkeys ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hotkeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hotkeys w_hk = new Hotkeys();
            kHook.RemoveHook();
            w_hk.ShowDialog();
            kHook.InstallHook();
            GetHotkeys();
        }

        /// <summary>
        /// Instructions ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder w_sb = new StringBuilder();
            w_sb.AppendLine("Instructions:");
            w_sb.AppendLine();
            w_sb.AppendLine("-Create new file to start.");
            w_sb.AppendLine("-Press Record button to start recording mouse and keyboard events.");
            w_sb.AppendLine("-Press Record button again to Pause/Stop.");
            w_sb.AppendLine("-Press Playback button to Play the currently loaded or recorded file.");
            w_sb.AppendLine("-Press Playback button again to stop current file from playing.");
            w_sb.AppendLine("-Recorded files can be saved using the Save button in File Menu.");
            w_sb.AppendLine("-Saved files can be opened using the Open button in File Menu.");
            w_sb.AppendLine("-Repeat, Repeat interval, and Speed settings can be changed in");
            w_sb.AppendLine("  Playback button in Options Menu");
            w_sb.AppendLine("-To choose which events are to be recorded, uncheck events in Record");
            w_sb.AppendLine("  button in Options Menu");
            w_sb.AppendLine("-Hotkeys can be set in Hotkeys button in Options Menu.");
            w_sb.AppendLine("-Other settings can be found in Settings button in Options Menu.");


            MessageBox.Show(w_sb.ToString(), "KMRecorder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// About ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder w_sb = new StringBuilder();
            w_sb.AppendLine("About:");
            w_sb.AppendLine();
            w_sb.AppendLine("Name: KMRecorder V1.0");
            w_sb.AppendLine("Version: 1.0");
            w_sb.AppendLine("License: GPL");
            w_sb.AppendLine("Manufactured By: Nicart & Friends Inc.");
            w_sb.AppendLine();
            w_sb.AppendLine("**DISCLAIMER**");
            w_sb.AppendLine("THIS MATERIAL IS PROVIDED \"AS IS\" WITHOUT WARRANTY OF ANY");
            w_sb.AppendLine("KIND, EITHER EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED");
            w_sb.AppendLine("TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR");
            w_sb.AppendLine("A PARTICULAR PURPOSE, OR NON - INFRINGEMENT. SOME");
            w_sb.AppendLine("JURISDICTIONS DO NOT ALLOW THE EXCLUSION OF IMPLIED");
            w_sb.AppendLine("WARRANTIES, SO THE ABOVE EXCLUSION MAY NOT APPLY TO YOU.");
            w_sb.AppendLine("IN NO EVENT WILL I BE LIABLE TO ANY PARTY FOR ANY DIRECT,");
            w_sb.AppendLine("INDIRECT, SPECIAL OR OTHER CONSEQUENTIAL DAMAGES FOR ANY");
            w_sb.AppendLine("USE OF THIS MATERIAL INCLUDING, WITHOUT LIMITATION, ANY");
            w_sb.AppendLine("LOST PROFITS, BUSINESS INTERRUPTION, LOSS OF PROGRAMS OR");
            w_sb.AppendLine("OTHER DATA ON YOUR INFORMATION HANDLING SYSTEM OR");
            w_sb.AppendLine("OTHERWISE, EVEN IF WE ARE EXPRESSLY ADVISED OF THE");
            w_sb.AppendLine("POSSIBILITY OF SUCH DAMAGES.");

            MessageBox.Show(w_sb.ToString(), "KMRecorder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Playback ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaybackSettings w_pbSet = new PlaybackSettings();
            w_pbSet.ShowDialog();
            GetPlaybackSettings();
        }

        /// <summary>
        /// Record ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RecordSettings w_rcSet = new RecordSettings();
            w_rcSet.ShowDialog();
            GetRecordSettings();
        }

        /// <summary>
        /// Always on top ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declare flag variable
            bool w_onTopFLG;

            //Checks if alwaysOnTopToolStripMenuItem is checked
            if (alwaysOnTopToolStripMenuItem.Checked)
            {
                alwaysOnTopToolStripMenuItem.Checked = false;
                w_onTopFLG = false;
                this.TopMost = false;
            }
            else
            {
                alwaysOnTopToolStripMenuItem.Checked = true;
                w_onTopFLG = true;
                this.TopMost = true;
            }

            //Change OnTop Settings and saves it
            m_sSection.RemoveKey("OnTop");
            m_sSection.AddKey("OnTop").Value = w_onTopFLG + "";
            m_iniFile.Save(m_iniPath);
        }

        /// <summary>
        /// On Window's Startup ToolStripMenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onWindowsStartupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Declare flag variable
            bool w_onStartUpFLG;

            try
            {
                //Checks if StartupToolStripMenuItem is checked
                if (onWindowsStartupToolStripMenuItem.Checked)
                {
                    onWindowsStartupToolStripMenuItem.Checked = false;
                    RegisterInStartup(false);
                    w_onStartUpFLG = false;
                }
                else
                {
                    onWindowsStartupToolStripMenuItem.Checked = true;
                    RegisterInStartup(true);
                    w_onStartUpFLG = true;
                }

                //Change OnStartUp Settings and saves it
                m_sSection.RemoveKey("OnStartUp");
                m_sSection.AddKey("OnStartUp").Value = w_onStartUpFLG + "";
                m_iniFile.Save(m_iniPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error occured: " + ex.Message, "GSWindowText", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// TrayWhenMin MenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrayWhenMin_Click(object sender, EventArgs e)
        {
            //Declare flag variable
            bool w_TrayWhenMinFLG;

            //Checks if TrayWhenMin MenuStrip is checked
            if (TrayWhenMin.Checked)
            {
                TrayWhenMin.Checked = false;
                w_TrayWhenMinFLG = false;
            }
            else
            {
                TrayWhenMin.Checked = true;
                w_TrayWhenMinFLG = true;
            }

            //Change TrayWhenMin Settings and saves it
            m_sSection.RemoveKey("TrayWhenMin");
            m_sSection.AddKey("TrayWhenMin").Value = w_TrayWhenMinFLG + "";
            m_iniFile.Save(m_iniPath);
        }

        /// <summary>
        /// Tray When Close MenuItem Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrayWhenClose_Click(object sender, EventArgs e)
        {
            //Declare flag variable
            bool w_TrayWhenCloseFLG;

            //Checks if TrayWhenClose MenuStrip is checked
            if (TrayWhenClose.Checked)
            {
                TrayWhenClose.Checked = false;
                w_TrayWhenCloseFLG = false;
            }
            else
            {
                TrayWhenClose.Checked = true;
                w_TrayWhenCloseFLG = true;
            }

            //Change TrayWhenClose Settings and saves it
            m_sSection.RemoveKey("TrayWhenClose");
            m_sSection.AddKey("TrayWhenClose").Value = w_TrayWhenCloseFLG + "";
            m_iniFile.Save(m_iniPath);
        }

        /// <summary>
        /// Windows Resize Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KMRecorder_SizeChanged(object sender, EventArgs e)
        {
            //Checks if the TrayWhenMin is checked and it is minimized
            if (TrayWhenMin.Checked && this.WindowState == FormWindowState.Minimized)
            {
                //Shows the System tray icon
                notifyIcon.Visible = true;
                notifyIcon.Icon = this.Icon;
                notifyIcon.BalloonTipText = "Program is running" + Environment.NewLine + "Double - click icon to show";
                notifyIcon.ShowBalloonTip(0);

                //Hides the windows form
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        /// <summary>
        /// NotifyIconBalloon Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            //Shows the windows form and hides the System Tray Icon
            this.Show();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        /// <summary>
        /// NotifyIcon Double-click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Shows the windows form and hides the System Tray Icon
            this.Show();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void KMRecorder_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Checks if the TrayWhenClose is checked
            if (TrayWhenClose.Checked)
            {
                //Cancels the closing of form
                e.Cancel = true;

                //Shows the System tray icon
                notifyIcon.Visible = true;
                notifyIcon.Icon = this.Icon;
                notifyIcon.ShowBalloonTip(0);

                //Hides the windows form
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                this.ShowInTaskbar = false;
                this.Hide();
            }
            else
            {
                //Checks if there is an unsaved recording
                if (m_isUnsaved)
                {
                    DialogResult w_result;

                    string w_currFile;
                    m_isCancelled = true;
                    m_isPaused = true;

                    //Checks if there is a file loaded
                    if (m_currentFile != "")
                    {
                        w_currFile = Path.GetFileName(m_currentFile);
                    }
                    else
                    {
                        w_currFile = "recording_" + m_recNo;
                    }

                    w_result = MessageBox.Show("Do you want to save changes to " + w_currFile + "?", "KMRecorder", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (w_result == DialogResult.Yes)
                    {
                        saveAsToolStripMenuItem_Click(this, null);
                    }
                    else if (w_result == DialogResult.No)
                    {
                        //Nothing
                    }
                }
            }
        }

        /// <summary>
        /// Load form Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KMRecorder_Load(object sender, EventArgs e)
        {
            //Checks if opened from startup
            if (onWindowsStartupToolStripMenuItem.Checked)
            {
                //Resize to minimize
                this.WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// Main Form Closed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KMRecorder_FormClosed(object sender, FormClosedEventArgs e)
        {
            kHook.RemoveHook();
        }

    }
}
