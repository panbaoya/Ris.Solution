using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui.Helper
{
    public partial class AboutUsForm : BaseForm
    {
        public AboutUsForm()
        {
            InitializeComponent();
        }

        private void AboutUsForm_Load(object sender, EventArgs e)
        {
            label1.Text = "上海图耘职能科技有限公司\r\nShangHaiTuYunZhiNeng Co., Ltd";
            label2.Text = "联系地址:金沙江商务广场1988号\r\nAddress:JinShaJiangShangWuGuangChang No.1988.";
            label3.Text = "联系电话:19921615203\r\nPhone:19921615203";
        }

        public override void SetStyle()
        {
            label1.Font = new Font("宋体", 20);
            label2.Font = new Font("宋体", 13);
            label3.Font = new Font("宋体", 13);
        }
    }
}
