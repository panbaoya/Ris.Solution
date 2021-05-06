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
    public partial class SystemSettingForm : Form,IBaseForm
    {
        public SystemSettingForm()
        {
            InitializeComponent();
        }

        public void SetFont()
        {
            this.Font = new Font("宋体", AppConfSetting.GlobalFontSize);
        }

        private void SystemSetting_Load(object sender, EventArgs e)
        {
            SetFont();
        }
    }
}
