using Ris.Bll;
using Ris.IBll;
using Ris.Models.Deptment;
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
    public partial class DeptSettingForm : BaseForm
    {
        //业务类
        private readonly IDeptmentBll _deptmentBll;

        public DeptSettingForm()
        {
            InitializeComponent();
            _deptmentBll = new DeptmentBll();
        }

        private void DeptSettingForm_Load(object sender, EventArgs e)
        {
            SetStyle();
            txtStatus.DataSource = new List<object>
            {
                new { Code = 0, Name = "禁用" },
                new { Code = 1, Name = "启用" },
            };
            txtStatus.DisplayMember = "Name";
            txtStatus.ValueMember = "Code";
            dataGridView1.ClearSelection();
            BindData();
        }

        /// <summary>
        /// 绑定科室列表
        /// </summary>
        void BindData()
        {
            //科室列表
            var depts = _deptmentBll.GetDeptments();
            dataGridView1.DataSource = depts;
            txtDeptCode.Text = null;
            txtDeptName.Text = null;
            txtHisCode.Text = null;
            txtStatus.SelectedIndex = 1;
        }

        /// <summary>
        /// 添加科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            DeptmentModel deptModel = new DeptmentModel
            {
                DeptCode=txtDeptCode.Text,
                DeptName=txtDeptName.Text,
                HisDeptCode=txtHisCode.Text,
                Status=Convert.ToInt32(txtStatus.SelectedValue),
            };
            var result = _deptmentBll.AddDeptment(deptModel, out string errorMsg);
            if (!result)
            {
                this.ShowInfo(errorMsg);
            }
            BindData();
        }

        /// <summary>
        /// 更新科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Enabled = false;
            if (string.IsNullOrEmpty(txtDeptCode.Text))
            {
                this.ShowInfo("请选择要修改的科室.");
            }
            DeptmentModel deptModel = new DeptmentModel
            {
                DeptCode = txtDeptCode.Text,
                DeptName = txtDeptName.Text,
                HisDeptCode = txtHisCode.Text,
                Status = Convert.ToInt32(txtStatus.SelectedValue),
            };
            var result = _deptmentBll.UpdateDeptment(deptModel, out string errorMsg);
            if (!result)
            {
                this.ShowInfo(errorMsg);
            }
            btnAdd.Enabled = true;
            btnUpdate.Enabled = true;
            BindData();
        }

        /// <summary>
        /// 双击选择科室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = dataGridView1.DataSource as List<DeptmentModel>;
                var deptModel = list[e.RowIndex];
                txtDeptCode.Text = deptModel.DeptCode;
                txtDeptName.Text = deptModel.DeptName;
                txtStatus.SelectedValue = deptModel.Status;
                txtHisCode.Text = deptModel.HisDeptCode;
                btnAdd.Enabled = false;
            }
        }

        /// <summary>
        /// 显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow CurrentRow = dataGridView1.Rows[e.RowIndex];
            CurrentRow.HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString() != "1")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
    }
}
