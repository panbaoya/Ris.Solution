using Ris.Bll;
using Ris.IBll;
using Ris.Models.User;
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
    public partial class UserSettingForm : Form
    {
        IUserBll _userBll;
        public UserSettingForm()
        {
            InitializeComponent();
            _userBll = new UserBll();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPassword.PasswordChar = new char();
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowInfo("请输入姓名.");
                return;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                this.ShowInfo("请输入密码.");
                return;
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                this.ShowInfo("请输入手机号.");
                return;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                this.ShowInfo("请输入账号.");
                return;
            }
            UserModel model = new UserModel
            {
                UserName = txtUserName.Text,
                Name = txtName.Text,
                Password = txtPassword.Text,
                Phone = txtPhone.Text,
                Status = 1,
            };
            var result=_userBll.AddUser(model);
            if (!result)
            {
                this.ShowInfo("添加失败.");
                return;
            }
            BindData();
        }

        private void UserSettingForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = _userBll.GetUsers();
        }
    }
}
