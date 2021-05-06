using Ris.Bll;
using Ris.Dal;
using Ris.IBll;
using Ris.Models.Deptment;
using Ris.Models.Enums;
using Ris.Models.Register;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui
{
    public partial class RegisterForm : Form,IBaseForm
    {
        ///业务类
        private readonly IRegisterBll _registerBll;
        private readonly ITypeConfigBll _typeConfigBll;
        private readonly IDeptmentBll _deptmentBll;
        private readonly IPositionMethodBll _positionMethodBll;
        public RegisterForm()
        {
            InitializeComponent();
            _registerBll = new RegisterBll();
            _typeConfigBll = new TypeConfigBll();
            _deptmentBll = new DeptmentBll();
            _positionMethodBll = new PositionMethodBll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            //绑定模型
            RegisterModel _registerModel= new RegisterModel
            {
                    CardNo = txtCardNo.Text,
                    Name=cmbName.Text,
                    IDCard=txtIDCard.Text,
                    PinYin1=txtPinyin1.Text,
                    Gender=Convert.ToInt32(cmbGender.SelectedValue),
                    Address=txtAddress.Text,
                    BirthDay=txtBirthDay.Text,
                    CurrentAge=txtAge.Text,
                    Phone=txtPhone.Text,
                    PostCode=txtPostCode.Text,
                    Email=txtEmail.Text,
                    ImageNumber=txtImageNumber.Text,
                    PatientType=cmbPatientType.Text,
                    CheckNo=txtCheckNo.Text,
                    BillHospital=txtBillHospital.Text,
                    ApplyDept=cmbApplyDept.SelectedValue.ToString(),
                    ApplyDeptName= cmbApplyDept.Text,
                    ApplyDocterName=txtApplyDocter.Text,
                    Area=txtArea.Text,
                    Bed=txtBed.Text,
                    ApplyDate=DateTime.Now,
                    CheckDept =cmbCheckDept.SelectedValue.ToString(),
                    CheckDeptName=cmbCheckDept.Text,
                    CheckType=cmbCheckType.Text,
                    Equipment=cmbEquipment.Text,
                    Position=cmbPosition.Text,
                    Method=cmbMethod.Text,
                    Symptom=txtSymptom.Text,
                    Diagnosis=txtDiagnosis.Text,
                    DockerAsk=txtRemarks.Text
            };
            if (_registerBll.Register(_registerModel, out string errorMsg))
            {
                MessageBox.Show("添加成功.");
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show(errorMsg);
            }
            btnRegister.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtIDCard.LostFocus += txtIDCard_LostFocus;
            SetFont();
            Init();
        }

        private void txtIDCard_LostFocus(object sender, EventArgs e)
        {
            txtBirthDay.Text =  BirthDayUnit.GetBirthday(txtIDCard.Text);
            txtAge.Text = BirthDayUnit.GetAge(txtIDCard.Text);
            cmbGender.Text = BirthDayUnit.GetGender(txtIDCard.Text);
        }

        private void Init()
        {
            this.Font = new Font("宋体", AppConfSetting.GlobalFontSize);
            var typeConfigs = _typeConfigBll.GetTypeConfigs(new Models.TypeConfig.RequestTypeConfigModel { IsParent=0});
            //性别
            var Genders = typeConfigs.Where(x => x.DataType == TypeConfigEnum.Gender).ToList();
            cmbGender.DisplayMember = "DataName";
            cmbGender.ValueMember = "DataCode";
            cmbGender.DataSource = Genders;
            //cmbGender.SelectedIndex = 0;
            //就诊类型
            var patientTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.PatientType).ToList();
            cmbPatientType.DisplayMember = "DataName";
            cmbPatientType.ValueMember = "DataCode";
            cmbPatientType.DataSource = patientTypes;
            //cmbPatientType.SelectedIndex = 0;
            //科室列表
            var depts = _deptmentBll.GetDeptments(1);
            cmbApplyDept.DisplayMember = "DeptName";
            cmbApplyDept.ValueMember = "DeptCode";
            cmbApplyDept.DataSource = depts;
            cmbCheckDept.DisplayMember = "DeptName";
            cmbCheckDept.ValueMember = "DeptCode";
            cmbCheckDept.DataSource =new List<DeptmentModel> (depts);
            //检查类型
            var checkTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.CheckType).ToList();
            cmbCheckType.DisplayMember = "DataName";
            cmbCheckType.ValueMember = "DataCode";
            cmbCheckType.DataSource = checkTypes;
            //检查设备
            var checkEqus = typeConfigs.Where(x => x.DataType == TypeConfigEnum.CheckEqu).ToList();
            cmbEquipment.DisplayMember = "DataName";
            cmbEquipment.ValueMember = "DataCode";
            cmbEquipment.DataSource = checkEqus;
            //检查部位
            var positions = _positionMethodBll.GetPositions();
            cmbPosition.TextSource = positions.Select(x=>x.Name).ToList();

        }

        private void cmbName_TextChanged(object sender, EventArgs e)
        {
            txtPinyin1.Text = PinYinConvert.GetPY(cmbName.Text);
            //if (cmbName.SelectedValue != null)
            //{
            //    var selectPatient = patients.Where(x => x.PatientID == cmbName.SelectedValue.ToString()).FirstOrDefault();
            //    txtCardNo.Text = selectPatient.CardNo;
            //    cmbGender.SelectedValue = selectPatient.Gender;
            //    _patientModel.Patient = selectPatient;
            //}
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch.PerformClick();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbName.Text))
            {
                RequestRegisterModel request = new RequestRegisterModel { Name = cmbName.Text };
                var retisters = _registerBll.Getpatients(request);
                if (retisters != null)
                {
                    cmbName.DisplayMember = "Name";
                    cmbName.ValueMember = "PatientID";
                    cmbName.DataSource = retisters;
                    cmbName.SelectionStart = cmbName.Text.Length;
                    cmbName.SelectionLength = 0;
                    cmbName.SelectedIndex = -1;
                    Cursor = Cursors.Default;
                    cmbName.DroppedDown = true;
                    cmbName.Text = request.Name;
                    txtPinyin1.Text = PinYinConvert.GetPY(cmbName.Text);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            this.Dispose();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || txtPostCode.Text.Length >= 6)
            {
                e.Handled = true;
            }
        }

        public void SetFont()
        {
            this.Font = new Font("宋体", AppConfSetting.GlobalFontSize);
            this.Text += "-" + AppConfSetting.HospitalName;
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //检查方法
            var methods = _positionMethodBll.GetMethods(Convert.ToInt32(cmbPosition.SelectedValue));
            cmbMethod.DisplayMember = "Name";
            cmbMethod.ValueMember = "ID";
            cmbMethod.DataSource = methods;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            txtImageNumber.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
