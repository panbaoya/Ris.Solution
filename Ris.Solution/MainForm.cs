using CustomControl;
using Ris.Bll;
using Ris.IBll;
using Ris.Models.Enums;
using Ris.Models.Register;
using Ris.Models.TypeConfig;
using Ris.Tools;
using Ris.Ui.Helper;
using Ris.Ui.Register;
using Ris.Ui.Setting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Ris.Ui
{
    public partial class MainForm : BaseForm
    {
        private readonly IRegisterBll _registerBll;
        private readonly ITypeConfigBll _typeConfigBll;
        public MainForm()
        {
            InitializeComponent();
            _registerBll = new RegisterBll();
            _typeConfigBll = new TypeConfigBll();
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            //新增登记快捷键
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                RegisterForm registerForm = new RegisterForm();
                registerForm.Show();
            }
        }

        private void tsbRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Rest += BindData;
            var result = registerForm.ShowDialog();
            Init();
            //if (result == DialogResult.OK)
            //{
            //    //重新绑定
            //    BindData();
            //}
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            tssUser.Text = $"当前登录:[{CurrentUser.Name}]";
            dateTimePicker1.Value = DateTime.Now.AddMonths(-3);
            SetStyle();
            this.dgvRegisterList.AutoGenerateColumns = false;
            dgvRegisterList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            foreach (DataGridViewColumn item in dgvRegisterList.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            Init();
            BindData();
        }

        private void Init()
        {
            var typeConfigs = _typeConfigBll.GetTypeConfigs(new RequestTypeConfigModel { IsParent = 0 });
            //就诊类型
            var patientTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.PatientType).ToList();
            patientTypes.Insert(0, new Models.TypeConfig.TypeConfigModel { DataCode = "-1", DataName = "全部" });
            cmbPatientType.DisplayMember = "DataName";
            cmbPatientType.ValueMember = "DataCode";
            cmbPatientType.DataSource = patientTypes;
            var visitTypes= typeConfigs.Where(x => x.DataType == TypeConfigEnum.VisitType).ToList();
            cmbVisitType.DisplayMember = "DataName";
            cmbVisitType.ValueMember = "DataCode";
            cmbVisitType.DataSource = visitTypes;
            //检查类型
            ckbCheckType.Items.Clear();
            var checkTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.CheckType).ToList();
            checkTypes.ForEach(x =>
            {
                ckbCheckType.Items.Add(x.DataCode);
            });
        }

        /// <summary>
        /// 绑定登记列表
        /// </summary>
        public void BindData()
        {
            RequestRegisterModel request = new RequestRegisterModel
            {
                ImageNumber = txtImageNumber.Text,
                PatientType = cmbPatientType.Text,
                VisitType = cmbVisitType.Text,
                StarDate = Convert.ToDateTime(DateTime.Now.ToString("D").ToString()),
                EndDate = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString("D").ToString()).AddSeconds(-1),
                CheckType = new List<string>(),
            };
            if (rdbWeek.Checked)
            {
                request.StarDate = DateTime.Now.AddDays(-7);
                request.EndDate = DateTime.Now;
            }
            if (rdbMonth.Checked)
            {
                request.StarDate = DateTime.Now.AddMonths(-1);
                request.EndDate = DateTime.Now;
            }
            if (rdbSelf.Checked)
            {
                request.StarDate = dateTimePicker1.Value;
                request.EndDate = dateTimePicker2.Value;
            }
            for (int i = 0; i < ckbCheckType.Items.Count; i++)
            {
                if (ckbCheckType.GetItemChecked(i))
                {
                    request.CheckType.Add(ckbCheckType.GetItemText(ckbCheckType.Items[i]));
                }
            }
            var list = _registerBll.GetRegisters(request);
            dgvRegisterList.DataSource = list;
        }

        private void dgvRegisterList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (this.dgvRegisterList.Columns[e.ColumnIndex].Name == "Operation")
                {
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;//设置重绘入单元格的字体样式
                    sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.EllipsisCharacter;

                    //e.PaintContent

                    e.PaintBackground(e.CellBounds, false);//重绘边框

                    //设置要写入字体的大小
                    System.Drawing.Font myFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    //myFon

                    SizeF sizeDel = e.Graphics.MeasureString("删除", myFont);

                    SizeF sizeMod = e.Graphics.MeasureString("修改", myFont);
                    SizeF sizeLook = e.Graphics.MeasureString("查看", myFont);

                    float fDel = sizeDel.Width / (sizeDel.Width + sizeMod.Width + sizeLook.Width); //
                    float fMod = sizeMod.Width / (sizeDel.Width + sizeMod.Width + sizeLook.Width);
                    float fLook = sizeLook.Width / (sizeDel.Width + sizeMod.Width + sizeLook.Width);

                    //设置每个“按钮的边界”
                    RectangleF rectDel = new RectangleF(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width * fDel, e.CellBounds.Height);

                    RectangleF rectMod = new RectangleF(rectDel.Right, e.CellBounds.Top, e.CellBounds.Width * fMod, e.CellBounds.Height);

                    RectangleF rectLook = new RectangleF(rectMod.Right, e.CellBounds.Top, e.CellBounds.Width * fLook, e.CellBounds.Height);

                    e.Graphics.DrawString("删除", myFont, Brushes.Black, rectDel, sf); //绘制“按钮”
                    e.Graphics.DrawString("修改", myFont, Brushes.Black, rectMod, sf);
                    e.Graphics.DrawString("查看", myFont, Brushes.Black, rectLook, sf);

                    e.Handled = true;
                }
            }
        }

        private bool IsAsc = true;
        private void dgvRegisterList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dgvRegisterList.Columns[e.ColumnIndex];
            var list = this.dgvRegisterList.DataSource as List<RegisterModel>;
            if (list != null)
            {
                if (IsAsc)
                {
                    switch (col.Name)
                    {
                        case "ApplyDate":
                            list = list.OrderByDescending(x => x.ApplyDate).ToList();
                            dgvRegisterList.Columns[e.ColumnIndex].HeaderText = "申请时间" + "▼";
                            IsAsc = !IsAsc;
                            break;
                        default:
                            break;
                    }
                    dgvRegisterList.DataSource = list;
                }
                else
                {
                    switch (col.Name)
                    {
                        case "ApplyDate":
                            list = list.OrderBy(x => x.ApplyDate).ToList();
                            dgvRegisterList.Columns[e.ColumnIndex].HeaderText = "申请时间" + "▲";
                            IsAsc = !IsAsc;
                            break;
                        default:
                            break;
                    }
                    dgvRegisterList.DataSource = list;
                }
            }
            else
            {
                return;
            }
        }

        private void dgvRegisterList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = this.dgvRegisterList.DataSource as List<RegisterModel>;
                var patientRegister = list[e.RowIndex];
                lblHospital.Text = patientRegister.Name;
                lblApplyDept.Text = patientRegister.ApplyDept;
                lblApplyDockerName.Text = patientRegister.CardNo;
                lblArea.Text = patientRegister.Area;
                lblBed.Text = patientRegister.Bed;
                lblImageNumber.Text = patientRegister.ImageNumber;
                lblCheckDept.Text = patientRegister.CheckDeptName;
                lblCheckType.Text = patientRegister.CheckType;
                lblEqu.Text = patientRegister.Equipment;
                lblPosition.Text = patientRegister.Position;
                txtZs.Text = patientRegister.Symptom;
                txtZd.Text = patientRegister.Diagnosis;
                txtYsyq.Text = patientRegister.DockerAsk;
                lblZs.Text = patientRegister.Symptom;
                lblZd.Text = patientRegister.Diagnosis;
                lblYsyq.Text = patientRegister.DockerAsk;
            }
        }

        private void tsmSystemSet_Click(object sender, EventArgs e)
        {
            SystemSettingForm form = new SystemSettingForm();
            form.Owner = this;
            form.ShowDialog();
        }

        private void tsmDeptSet_Click(object sender, EventArgs e)
        {
            DeptSettingForm deptSetting = new DeptSettingForm();
            //deptSetting.MdiParent = this;
            deptSetting.ShowDialog();
        }

        public override void SetStyle()
        {
            if (string.IsNullOrEmpty(AppConfSetting.HospitalName))
            {
                SystemSettingForm form = new SystemSettingForm();
                form.Owner = this;
                var result = form.ShowDialog();
            }
            base.SetStyle();
        }

        private void tsmConfigSet_Click(object sender, EventArgs e)
        {
            ConfigurationSettingForm form = new ConfigurationSettingForm();
            form.Owner = this;
            form.ShowDialog();
        }

        private void 检查部位设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PositionMethodSettingForm form = new PositionMethodSettingForm();
            form.Owner = this;
            form.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tssTime.Text = "系统时间:" + DateTime.Now.ToString();
            tssUser.Text = $"当前登录:[{CurrentUser.Name}]";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = this.ShowQuestion("确定要关闭系统吗?");
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.UserName == "admin")
            {
                UserSettingForm form = new UserSettingForm();
                form.ShowDialog();
            }
            else
            {
                this.ShowInfo("请联系管理员");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindData();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 关于ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutUsForm form = new AboutUsForm();
            form.ShowDialog();
        }

        private void 打开影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvRegisterList.Rows[dgvRegisterList.CurrentRow.Index];
            UdpUnit.SendMsg("addStudy#1.2.840.113619.2.472.3.2831157775.102.1603175316.637"); //+ row.Cells["ImageNumber"].Value);
        }

        private void 清除影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UdpUnit.SendMsg("clearImage");
        }

        private void 追加检查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvRegisterList.Rows[dgvRegisterList.CurrentRow.Index];
            UdpUnit.SendMsg("addStudy#1517952");// + row.Cells["ImageNumber"].Value);
        }

        private void tsbUserChange_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm(true);
            form.ShowDialog();
        }

        private void dgvRegisterList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //修改登记
            if (e.RowIndex>=0)
            {
                var list = this.dgvRegisterList.DataSource as List<RegisterModel>;
                var patientRegister = list[e.RowIndex];
                RegisterForm form = new RegisterForm(patientRegister);
                form.Rest += BindData;
                form.ShowDialog();
            }
        }
        bool rdbcheck1 = false;
        bool rdbcheck2 = false;
        bool rdbcheck3 = false;

        private void rdbWeek_Click(object sender, EventArgs e)
        {
            //三个日期按钮可以不选中.
            var radio = sender as RadioButton;
            if (rdbcheck1 && radio.Name== "rdbWeek")
            {
                radio.Checked = !rdbcheck1;
                rdbcheck1 = !rdbcheck1;
            }
            else
            {
                radio.Checked = !rdbcheck1;
                rdbcheck1 = !rdbcheck1;
            }
            if (rdbcheck2 && radio.Name == "rdbMonth")
            {
                radio.Checked = !rdbcheck2;
                rdbcheck2 = !rdbcheck2;
            }
            else
            {
                radio.Checked = !rdbcheck2;
                rdbcheck2 = !rdbcheck2;
            }
            if (rdbcheck3 && radio.Name == "rdbSelf")
            {
                radio.Checked = !rdbcheck3;
                rdbcheck3 = !rdbcheck3;
            }
            else
            {
                radio.Checked = !rdbcheck3;
                rdbcheck3 = !rdbcheck3;
            }
        }
    }
}