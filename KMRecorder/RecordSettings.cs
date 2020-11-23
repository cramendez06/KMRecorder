using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KMRecorder
{
    public partial class RecordSettings : Form
    {
        ///----------------------------------------------------------------------/
        ///
        /// System name：KMRecorder V1
        /// Program title：RecordSettings
        /// Overview: Record Settings Form
        ///
        ///      Author： M.Mendez    CREATE 2017/12/04          【P-10000】
        ///      
        ///     Copyright (C)
        ///----------------------------------------------------------------------/
        //=======================================================================
        //                        Main Constuctor Method
        //=======================================================================
        public RecordSettings()
        {
            InitializeComponent();
            InitializeDisplay();
        }

        /// <summary>
        /// Initialize Display
        /// </summary>
        private void InitializeDisplay() {
            cbMouseClick.Checked = KMRecorder.m_iniMouseClick;
            cbMouseMove.Checked = KMRecorder.m_iniMouseMove;
            cbKeyboard.Checked = KMRecorder.m_iniKeyboard;
            cbTime.Checked = KMRecorder.m_iniTime;
        }

        /// <summary>
        /// Checkbox MouseClick Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMouseClick_CheckedChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("MouseClick");
            KMRecorder.m_sSection.AddKey("MouseClick").Value = cbMouseClick.Checked.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Checkbox Keyboard Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbKeyboard_CheckedChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("Keyboard");
            KMRecorder.m_sSection.AddKey("Keyboard").Value = cbKeyboard.Checked.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Checkbox MouseMove Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbMouseMove_CheckedChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("MouseMove");
            KMRecorder.m_sSection.AddKey("MouseMove").Value = cbMouseMove.Checked.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Checkbox Time Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTime_CheckedChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("Time");
            KMRecorder.m_sSection.AddKey("Time").Value = cbTime.Checked.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }
    }
}
