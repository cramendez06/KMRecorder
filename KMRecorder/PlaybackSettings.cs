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
    public partial class PlaybackSettings : Form
    {
        ///----------------------------------------------------------------------/
        ///
        /// System name：KMRecorder V1
        /// Program title：PlaybackSettings
        /// Overview: Playback Settings Form
        ///
        ///      Author： M.Mendez    CREATE 2017/12/04          【P-10000】
        ///      
        ///     Copyright (C)
        ///----------------------------------------------------------------------/
        //=======================================================================
        //                        Main Constuctor Method
        //=======================================================================
        public PlaybackSettings()
        {
            InitializeComponent();
            InitilizeDisplay();
        }

        /// <summary>
        /// Initialize Display
        /// </summary>
        public void InitilizeDisplay()
        {
            //Checks if Unlimited loop
            if (KMRecorder.m_iniRepeatNo == -1)
            {
                rbRptNoCount.Checked = true;
            }
            else
            {
                nudRpt.Value = KMRecorder.m_iniRepeatNo;
                rbRptCount.Checked = true;
            }
            
            //Checks if the ReoeatInterval in ms is greater than a minute
            if (KMRecorder.m_iniRepeatInterval > 59999)
            {
                int sec = KMRecorder.m_iniRepeatInterval / 1000;
                int min = 0;
                if (sec > 59) {
                    min = sec / 60;
                    sec = sec - (min * 60);
                }
                int ms = KMRecorder.m_iniRepeatInterval - (1000 * sec);

                nudMin.Value = min;
                nudSec.Value = sec;
                nudMilSec.Value = ms;
                rbMin.Checked = true;
            }
            //Checks if RepeatInteval is in Seconds
            else if (KMRecorder.m_iniRepeatInterval > 999)
            {
                int sec = KMRecorder.m_iniRepeatInterval / 1000;
                int ms = KMRecorder.m_iniRepeatInterval - (1000 * sec);

                nudSec.Value = sec;
                nudMilSec.Value = ms;
                rbSec.Checked = true;
            }
            else
            {
                nudMilSec.Value = KMRecorder.m_iniRepeatInterval;
                rbMilSec.Checked = true;
            }

            tbSpeed.Value = KMRecorder.m_iniSpeed;
        }

        /// <summary>
        /// Repeat Count Radio button Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbRptCount_CheckedChanged(object sender, EventArgs e)
        {
            nudRpt.Enabled = true;
        }

        /// <summary>
        /// Repeat Unlimited Radio button Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbRptNoCount_CheckedChanged(object sender, EventArgs e)
        {
            nudRpt.Enabled = false;
        }

        /// <summary>
        /// Minute Radio button Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbMin_CheckedChanged(object sender, EventArgs e)
        {
            nudMin.Enabled = true;
            nudSec.Enabled = false;
            nudMilSec.Enabled = false;
        }

        /// <summary>
        /// Seconds Radio button Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbSec_CheckedChanged(object sender, EventArgs e)
        {
            nudMin.Enabled = false;
            nudSec.Enabled = true;
            nudMilSec.Enabled = false;
        }

        /// <summary>
        /// Milliseconds Radio button Checked Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbMilSec_CheckedChanged(object sender, EventArgs e)
        {
            nudMin.Enabled = false;
            nudSec.Enabled = false;
            nudMilSec.Enabled = true;
        }

        /// <summary>
        /// Repeat no. NumericUpDown Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudRpt_ValueChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("RepeatNo");
            KMRecorder.m_sSection.AddKey("RepeatNo").Value = nudRpt.Value.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Minute NumericUpDown Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMin_ValueChanged(object sender, EventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Milliseconds NumericUpDown Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMilSec_ValueChanged(object sender, EventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Seconds NumericUpDown Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSec_ValueChanged(object sender, EventArgs e)
        {
            SaveRepeatIntervalIni();
        }
        
        /// <summary>
        /// TrackBar Speed Value Changed Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbSpeed_ValueChanged(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("Speed");
            KMRecorder.m_sSection.AddKey("Speed").Value = tbSpeed.Value.ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Repeat no. NumericUpDown KeyUp Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudRpt_KeyUp(object sender, KeyEventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Minute NumericUpDown KeyUp Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMin_KeyUp(object sender, KeyEventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Seconds NumericUpDown KeyUp Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSec_KeyUp(object sender, KeyEventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Milliseconds NumericUpDown KeyUp Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudMilSec_KeyUp(object sender, KeyEventArgs e)
        {
            SaveRepeatIntervalIni();
        }

        /// <summary>
        /// Save RepeatInterval Settings in .ini file
        /// </summary>
        private void SaveRepeatIntervalIni()
        {
            KMRecorder.m_sSection.RemoveKey("RepeatInterval");
            KMRecorder.m_sSection.AddKey("RepeatInterval").Value = TotalMS().ToString();

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);
        }

        /// <summary>
        /// Compute Repeat Interval in Milliseconds
        /// </summary>
        /// <returns>Total milliseconds of Repeat Interval</returns>
        private int TotalMS()
        {
            int ms = 0;

            if (nudSec.Value != 0) ms = (Convert.ToInt32(nudMin.Value) * 60) * 1000;

            if (nudSec.Value != 0) ms += Convert.ToInt32(nudSec.Value) * 1000;

            if (nudMilSec.Value != 0) ms += Convert.ToInt32(nudMilSec.Value);

            return ms;
        }
    }
}
