using Ris.Bll;
using Ris.IBll;
using Ris.Models.Enums;
using Ris.Models.TypeConfig;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Ris.Ui.Setting
{
    public partial class ConfigurationSettingForm : BaseForm
    {
        ITypeConfigBll _typeConfigBll;
        public ConfigurationSettingForm()
        {
            InitializeComponent();
            _typeConfigBll = new TypeConfigBll();
        }

        private void ConfigurationSettingForm_Load(object sender, EventArgs e)
        {
            cmbType.DisplayMember = "DataName";
            cmbType.ValueMember = "DataCode";
            var types= _typeConfigBll.GetTypeConfigs(new RequestTypeConfigModel { IsParent=1});
            types.Insert(0, new TypeConfigModel { DataCode = "-1", DataName = "全部" });
            cmbType.DataSource = types; // EnumUnit.EnumToList<TypeConfigEnum>();
            dataGridView1.ClearSelection();
            SetStyle();
            BindData();
            //this.comboBox1.Text = AppConfSetting.GlobalFontSize.ToString();
            ////this.txtHospitalCode.Text = AppConfSetting.HospitalCode;
            ////this.txtHospitalName.Text = AppConfSetting.HospitalName;
            //hospitalTypeConfigModel = _typeConfigBll.GetTypeConfig(TypeConfigEnum.HospitalInfo);
            //this.txtHospitalCode.Text = hospitalTypeConfigModel.DataCode;
            //this.txtHospitalName.Text = hospitalTypeConfigModel.DataName;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        private bool Verification()
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                this.ShowInfo("类型代码为空.");
                return false;
            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowInfo("类型名称为空.");
                return false;
            }
            if (cmbType.Text== "全部")
            {
                this.ShowInfo("请选择类型");
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindData()
        {
            dataGridView1.AutoGenerateColumns = false;
            var request = new RequestTypeConfigModel { IsParent = 0 };
            if (cmbType.Text != "全部")
            {
                request.DataType =(TypeConfigEnum) Enum.Parse(typeof(TypeConfigEnum), cmbType.SelectedValue.ToString());
            }
            var typeConfigs = _typeConfigBll.GetTypeConfigs(request);
            dataGridView1.DataSource = typeConfigs;
            dataGridView1.ClearSelection();
            txtCode.Text = null;
            txtName.Text = null;
            txtRemark.Text = null;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow!=null)
            {
                var id =Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["ID"].Value);
                if (!_typeConfigBll.DelTypeConfig(id))
                {
                    this.ShowInfo("设置无效失败.");
                    return;
                }
                BindData();
            }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var request = new RequestTypeConfigModel { IsParent = 0 };
            if (cmbType.Text != "全部")
            {
                request.DataType = (TypeConfigEnum)Enum.Parse(typeof(TypeConfigEnum), cmbType.SelectedValue.ToString());
            }
            var typeConfigs = _typeConfigBll.GetTypeConfigs(request);
            dataGridView1.DataSource = typeConfigs;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var type = (TypeConfigEnum)Enum.Parse(typeof(TypeConfigEnum), cmbType.SelectedValue.ToString());
            TypeConfigModel configModel = new TypeConfigModel
            {
                DataCode = txtCode.Text,
                DataName = txtName.Text,
                Status = 1,
                Remarks = txtRemark.Text,
                DataType =type,
                IsParent=0,
            };
            if (Verification())
            {
                if (_typeConfigBll.AddTypeConfig(configModel))
                {
                    BindData();
                }
                else
                {
                    this.ShowInfo("添加失败.");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            TypeConfigModel configModel = new TypeConfigModel
            {
                DataCode = txtCode.Text,
                DataName = txtName.Text,
                Remarks = txtRemark.Text,
                DataType =(TypeConfigEnum) cmbType.SelectedValue
            };
            _typeConfigBll.UpdateTypeConfig(configModel);
            btnAdd.Enabled = true;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = dataGridView1.DataSource as List<TypeConfigModel>;
                var configModel = list[e.RowIndex];
                txtCode.Text = configModel.DataCode;
                txtName.Text = configModel.DataName;
                txtRemark.Text = configModel.Remarks;
                btnAdd.Enabled = false;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewRow CurrentRow = dataGridView1.Rows[e.RowIndex];
            CurrentRow.HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "0")
                {
                    dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = dataGridView1.DataSource as List<TypeConfigModel>;
                var configModel = list[e.RowIndex];
                btnDel.Text = configModel.Status == 1 ? "禁用" : "启用";
            }
        }
    }
}