using Ris.Bll;
using Ris.IBll;
using Ris.Models.User;
using Ris.Tools;
using Ris.Tools.Nlog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui
{
    public partial class LoginForm : BaseForm
    {
        IUserBll _userBll;
        public LoginForm()
        {
            InitializeComponent();
            _userBll = new UserBll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                if (string.IsNullOrEmpty(txtPhone.Text))
                {
                    this.ShowInfo("请输入手机.");
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    this.ShowInfo("请输入用户名..");
                }
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                this.ShowInfo("请输入密码.");
            }
            UserModel userModel = new UserModel
            {
                UserName = txtUserName.Text,
                Phone = txtPhone.Text,
                Password = txtPassword.Text,
                Status = 1,
            };
            var user = _userBll.Login(userModel);
            if (user != null)
            {
                BaseForm.CurrentUser = user;
                var msg = $"操作员:{user.Name},工号:{user.UserName},{DateTime.Now.ToString()}登录成功.";
                NLogger.LogInfo(msg, user.UserName);
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            else
            {
                var msg = $"工号/手机号:{txtUserName.Text + txtPhone.Text},{DateTime.Now.ToString()}登录失败.";
                NLogger.LogInfo(msg, user.UserName);
                this.ShowInfo("用户名或密码错误.");
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            SetStyle();
            checkBox1.Checked = false;
            txtPhone.Visible = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtPhone.Visible = true;
                txtPhone.Enabled = true;
                lblPhone.Visible = true;
                lblUserName.Visible = false;
                txtUserName.Visible = false;
                txtUserName.Enabled = false;
            }
            else
            {
                txtPhone.Visible = false;
                txtPhone.Enabled = false;
                lblPhone.Visible = false;
                lblUserName.Visible = true;
                txtUserName.Visible = true;
                txtUserName.Enabled = true;
            }
        }
    }
}
