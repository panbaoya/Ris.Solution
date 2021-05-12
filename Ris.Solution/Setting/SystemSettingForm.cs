using Ris.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui.Setting
{
    public partial class SystemSettingForm : BaseForm
    {
        public SystemSettingForm()
        {
            InitializeComponent();
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            SetStyle();
            Init();
        }

        private void Init()
        {
            txtHospitalCode.Text = AppConfSetting.HospitalCode;
            txtHospitalName.Text = AppConfSetting.HospitalName;
            txtAesKey.Text = AppConfSetting.AesKey;
            txtPostAddress.Text = AppConfSetting.PostAddress;
            txtPort.Text = AppConfSetting.Port.ToString() ;
            cmbGlobalFontSize.Text = AppConfSetting.GlobalFontSize.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAesKey.TextLength!=16)
            {
                this.ShowInfo("密钥应为16位.");
            }
            AppConfSetting.SaveSettiongs("PostAddress", txtPostAddress.Text);
            AppConfSetting.SaveSettiongs("Port", txtPort.Text);
            AppConfSetting.SaveSettiongs("GlobalFontSize", cmbGlobalFontSize.Text);
            AppConfSetting.SaveSettiongs("HospitalName", txtHospitalName.Text);
            AppConfSetting.SaveSettiongs("HospitalCode", txtHospitalCode.Text);
            AppConfSetting.SaveSettiongs("AesKey", txtAesKey.Text);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
