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
    public partial class Hotkeys : Form
    {
        ///----------------------------------------------------------------------/
        ///
        /// System name：KMRecorder V1
        /// Program title：Hotkeys
        /// Overview: Hotkeys Setting Form
        ///
        ///      Author： M.Mendez    CREATE 2017/12/04          【P-10000】
        ///      
        ///     Copyright (C)
        ///----------------------------------------------------------------------/
        //=======================================================================
        //                        Main Consturtor Method
        //=======================================================================
        public Hotkeys()
        {
            InitializeComponent();
            Initialize();
        }

        /// <summary>
        /// Initialize Displays
        /// </summary>
        public void Initialize()
        {
            tbNew.Text = KMRecorder.m_strNew;
            tbRecord.Text = KMRecorder.m_strRecord;
            tbNewRec.Text = KMRecorder.m_strNewRec;
            tbPlayback.Text = KMRecorder.m_strPlayback;
            tbPause.Text = KMRecorder.m_strPause;
        }

        /// <summary>
        /// OK Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            KMRecorder.m_sSection.RemoveKey("hkNew");
            KMRecorder.m_sSection.AddKey("hkNew").Value = tbNew.Text;

            KMRecorder.m_sSection.RemoveKey("hkRecord");
            KMRecorder.m_sSection.AddKey("hkRecord").Value = tbRecord.Text;

            KMRecorder.m_sSection.RemoveKey("hkNewRec");
            KMRecorder.m_sSection.AddKey("hkNewRec").Value = tbNewRec.Text;

            KMRecorder.m_sSection.RemoveKey("hkPlayback");
            KMRecorder.m_sSection.AddKey("hkPlayback").Value = tbPlayback.Text;

            KMRecorder.m_sSection.RemoveKey("hkPause");
            KMRecorder.m_sSection.AddKey("hkPause").Value = tbPause.Text;

            KMRecorder.m_iniFile.Save(KMRecorder.m_iniPath);

            this.Close();
        }

        /// <summary>
        /// KeyUp Event Method for Textboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox currTB = (TextBox)sender;
            currTB.Text = e.KeyCode.ToString();
        }
    }
}
