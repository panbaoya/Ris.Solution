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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fullAddress = txtPostAddress.Text;
            if (!string.IsNullOrEmpty(txtPort.Text))
            {
                fullAddress += ":" + txtPort.Text+"/";
            }
            txtPostAddress.Text = fullAddress;
            AppConfSetting.SaveSettiongs("PostAddress", fullAddress);
        }
    }
}
