using Ris.Bll;
using Ris.Dal;
using Ris.IBll;
using Ris.Models.Deptment;
using Ris.Models.Enums;
using Ris.Models.InterFaceModel;
using Ris.Models.PositionMethod;
using Ris.Models.Register;
using Ris.Models.TypeConfig;
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
using System.Xml;

namespace Ris.Ui.Register
{
    public partial class RegisterForm : BaseForm
    {
        ///业务类
        private readonly IRegisterBll _registerBll;
        private readonly ITypeConfigBll _typeConfigBll;
        private readonly IDeptmentBll _deptmentBll;
        private readonly IPositionMethodBll _positionMethodBll; 
        public RegisterForm()
        {
            InitializeComponent();
            txtIDCard.LostFocus += txtIDCard_LostFocus;
            _registerBll = new RegisterBll();
            _typeConfigBll = new TypeConfigBll();
            _deptmentBll = new DeptmentBll();
            _positionMethodBll = new PositionMethodBll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetStyle();
            Init();
            tabPage2.Parent = null;
        }

        private void txtIDCard_LostFocus(object sender, EventArgs e)
        {
            txtBirthDay.Text =  BirthDayUnit.GetBirthday(txtIDCard.Text);
            txtAge.Text = BirthDayUnit.GetAge(txtIDCard.Text);
            cmbGender.Text = BirthDayUnit.GetGender(txtIDCard.Text);
        }

        private void Init()
        {
            var typeConfigs = _typeConfigBll.GetTypeConfigs(new Models.TypeConfig.RequestTypeConfigModel { IsParent=0});
            //性别
            var Genders = typeConfigs.Where(x => x.DataType == TypeConfigEnum.Gender).ToList();
            cmbGender.DisplayMember = "DataName";
            cmbGender.ValueMember = "DataCode";
            cmbGender.DataSource = Genders;
            //cmbGender.SelectedIndex = 0;
            //就诊类型
            var visitTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.VisitType).ToList();
            cmbVisitType.DisplayMember = "DataName";
            cmbVisitType.ValueMember = "DataCode";
            cmbVisitType.DataSource = visitTypes;
            //cmbPatientType.SelectedIndex = 0;
            //就诊类型
            var patientTypes = typeConfigs.Where(x => x.DataType == TypeConfigEnum.PatientType).ToList();
            cmbPatientType.DisplayMember = "DataName";
            cmbPatientType.ValueMember = "DataCode";
            cmbPatientType.DataSource = patientTypes;
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

            txtBillHospital.Text = AppConfSetting.HospitalName;

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            txtPinyin1.Text = PinYinConvert.GetPY(txtName.Text);
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
                //btnSearch.PerformClick();
            }
        }

        /// <summary>
        /// 姓名查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtName.Text))
            //{
            //    RequestRegisterModel request = new RequestRegisterModel { Name = txtName.Text };
            //    var retisters = _registerBll.Getpatients(request);
            //    if (retisters != null)
            //    {
            //        txtName.DisplayMember = "Name";
            //        txtName.ValueMember = "PatientID";
            //        txtName.DataSource = retisters;
            //        txtName.SelectionStart = txtName.Text.Length;
            //        txtName.SelectionLength = 0;
            //        txtName.SelectedIndex = -1;
            //        Cursor = Cursors.Default;
            //        txtName.DroppedDown = true;
            //        txtName.Text = request.Name;
            //        txtPinyin1.Text = PinYinConvert.GetPY(txtName.Text);
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            patient = null;
            dgvApplys.DataSource = null;
            dgvApplyProjects.DataSource = null;
            dgvApplyBills.DataSource = null;
            tabPage2.Parent = null;
            RestTextBox();
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) || txtPostCode.Text.Length >= 6)
            {
                e.Handled = true;
            }
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            //检查方法
            var methods = _positionMethodBll.GetMethods(Convert.ToInt32(cmbPosition.SelectedValue));
            cmbMethod.DisplayMember = "Name";
            cmbMethod.ValueMember = "ID";
            cmbMethod.DataSource = methods;
        }

        private void txtCardNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Enter)
            {
                var xml = @"<ApplyBillInfo>
        <PatientInfos>
            <PatientInfo>
                <RegCode>970312</RegCode>
                <BedNo/>
                <DoctorName null=""yes""/>
                <PatientType>10</PatientType>
                <PatientTypeName>普通病人</PatientTypeName>
                <OutPatientNo>8658580</OutPatientNo>
                <WardCode>0</WardCode>
                <DateOfBirth>19980808</DateOfBirth>
                <RelationshipType/>
                <EncounterType>O</EncounterType>
                <EncounterTypeName>门诊</EncounterTypeName>
                <Age>22岁</Age>
                <LocationName>内科</LocationName>
                <RelationshipAdd/>
                <RelationshipTel/>
                <MIcardNumber>88888888</MIcardNumber>
                <HomeTel/>
                <DoctorCode/>
                <MedicalCardNumber null=""yes""/>
                <LocationCode>265</LocationCode>
                <RelationshipName>测试123321</RelationshipName>
                <HomeAddress/>
                <GenderCode>M</GenderCode>
                <GenderName>男</GenderName>
                <WorkAddress/>
                <WardName null=""yes"" />
                <PatientName>测试123321</PatientName>
                <WorkTel/>
                <Wbbh>8658580</Wbbh>
                <InPatientNo/>
                <SSNumber/>
                <ApplyBills>
                    <ApplyBill>
                        <Result/>
                        <RequestStatus>0 </RequestStatus>
                        <Memo/>
                        <MedicalHistory>主诉:1  现病史:1&#xd;
既往史:1  体格检查:1</MedicalHistory>
                        <LocationCode>265</LocationCode>
                        <ReqDoctorName>0000</ReqDoctorName>
                        <RequestDate>20210413111021</RequestDate>
                        <UrgentStatus>0</UrgentStatus>
                        <ReqLocationCode>265</ReqLocationCode>
                        <LocationName>内科</LocationName>
                        <RequestId>385168</RequestId>
                        <ReqLocationName null=""yes"" />
                        <ReqDoctorCode>0000</ReqDoctorCode>
                        <RequestType/>
                        <ClinicDiagnosis>健康查体</ClinicDiagnosis>
                        <AllergyHistory/>
                        <ApplyItems>
                            <ApplyItem>
                                <UnitPrice>350.0000</UnitPrice>
                                <CheckPointName/>
                                <CheckPointCode/>
                                <Memo/>
                                <MethodName>0</MethodName>
                                <ItemName>电子胃镜检查</ItemName>
                                <MethodCode/>
                                <Quantity>1</Quantity>
                                <ItemCode>NKJJC1344</ItemCode>
                            </ApplyItem>
                        </ApplyItems>
                    </ApplyBill>
                    <ApplyBill>
                        <Result/>
                        <RequestStatus>0 </RequestStatus>
                        <Memo/>
                        <MedicalHistory>主诉:1  现病史:1既往史:1  体格检查:1</MedicalHistory>
                        <LocationCode>265</LocationCode>
                        <ReqDoctorName>0000</ReqDoctorName>
                        <RequestDate>20210413111038</RequestDate>
                        <UrgentStatus>0</UrgentStatus>
                        <ReqLocationCode>265</ReqLocationCode>
                        <LocationName>内科</LocationName>
                        <RequestId>385169</RequestId>
                        <ReqLocationName null=""yes"" />
                        <ReqDoctorCode>0000</ReqDoctorCode>
                        <RequestType/>
                        <ClinicDiagnosis>健康查体</ClinicDiagnosis>
                        <AllergyHistory/>
                        <ApplyItems>
                            <ApplyItem>
                                <UnitPrice>400.0000</UnitPrice>
                                <CheckPointName/>
                                <CheckPointCode/>
                                <Memo/>
                                <MethodName>0</MethodName>
                                <ItemName>电子肠镜检查</ItemName>
                                <MethodCode/>
                                <Quantity>1</Quantity>
                                <ItemCode>NKJJC1345</ItemCode>
                            </ApplyItem>
                        </ApplyItems>
                    </ApplyBill>
                    <ApplyBill>
                        <Result/>
                        <RequestStatus>0 </RequestStatus>
                        <Memo/>
                        <MedicalHistory>主诉:1  现病史:1既往史:1  体格检查:1</MedicalHistory>
                        <LocationCode>265</LocationCode>
                        <ReqDoctorName>0000</ReqDoctorName>
                        <RequestDate>20210413110330</RequestDate>
                        <UrgentStatus/>
                        <ReqLocationCode>265</ReqLocationCode>
                        <LocationName>内科</LocationName>
                        <RequestId>385162</RequestId>
                        <ReqLocationName null=""yes"" />
                        <ReqDoctorCode>0000</ReqDoctorCode>
                        <RequestType/>
                        <ClinicDiagnosis>健康查体</ClinicDiagnosis>
                        <AllergyHistory/>
                        <ApplyItems>
                            <ApplyItem>
                                <UnitPrice>350.0000</UnitPrice>
                                <CheckPointName>部位2</CheckPointName>
                                <CheckPointCode>222</CheckPointCode>
                                <Memo/>
                                <MethodName/>
                                <ItemName>电子胃镜检查</ItemName>
                                <MethodCode/>
                                <Quantity>1</Quantity>
                                <ItemCode>NKJJC1344</ItemCode>
                            </ApplyItem>
                            <ApplyItem>
                                <UnitPrice>400.0000</UnitPrice>
                                <CheckPointName>部位</CheckPointName>
                                <CheckPointCode>111</CheckPointCode>
                                <Memo/>
                                <ItemName>电子肠镜检查</ItemName>
                                <MethodName>方法</MethodName>
                                <MethodCode>222</MethodCode>
                                <Quantity>1</Quantity>
                                <ItemCode>NKJJC1345</ItemCode>
                            </ApplyItem>
                        </ApplyItems>
                    </ApplyBill>
                </ApplyBills>
            </PatientInfo>
        </PatientInfos>
    </ApplyBillInfo>
";
                if (!string.IsNullOrEmpty(txtCardNo.Text))
                {
                    var applyBill = XmlUnit.XmlDeserialize<ApplyBillInfo>(xml);
                    if (applyBill!=null && applyBill.PatientInfos.Count>0&& applyBill.PatientInfos[0].ApplyBills.Count>0)
                    {
                        patient = applyBill.PatientInfos[0];
                        patient.ApplyBills[0].ApplyItems.Select(x => x.CheckPointCode);
                        //his接口数据,异步添加配置
                        var configTask=_typeConfigBll.AddTypeConfigByHisAsync(patient);
                        var deptTask=_deptmentBll.AddDeptmentByHisAsync(patient);

                        dgvApplyBills.DataSource = patient.ApplyBills;
                        tabPage2.Parent = tabControl1;
                        tabControl1.SelectedTab = tabPage2;
                        //同步数据,最后加载,怕线程没有执行完.
                        //configTask.Wait();
                        //deptTask.Wait();                        
                    }
                    else
                    {
                        this.ShowInfo("没有患者信息.");
                    }
                }
            }
        }

        public delegate void RestDataBind();
        //事件只能内部去调用.
        public event RestDataBind Rest;

        private void btnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            //绑定模型
            RegisterModel _registerModel = new RegisterModel
            {
                CardNo = txtCardNo.Text,
                Name = txtName.Text,
                IDCard = txtIDCard.Text,
                PinYin = txtPinyin1.Text,
                Gender = cmbGender.SelectedValue.ToString(),
                GenderName = cmbGender.Text,
                Address = txtAddress.Text,
                BirthDay = txtBirthDay.Text,
                CurrentAge = txtAge.Text,
                Phone = txtPhone.Text,
                PostCode = txtPostCode.Text,
                Email = txtEmail.Text,
                ImageNumber = txtImageNumber.Text,
                PatientType = cmbPatientType.Text,
                VisitType = cmbVisitType.Text,
                CheckNo = txtCheckNo.Text,
                BillHospital = txtBillHospital.Text,
                ApplyDept = cmbApplyDept.SelectedValue.ToString(),
                ApplyDeptName = cmbApplyDept.Text,
                ApplyDocter=txtApplyDocterCode.Text,
                ApplyDocterName = txtApplyDocter.Text,
                Area = txtArea.Text,
                Bed = txtBed.Text,
                ApplyDate = dtpApplyDate.Value,
                CheckDept = cmbCheckDept.SelectedValue.ToString(),
                CheckDeptName = cmbCheckDept.Text,
                CheckType = cmbCheckType.Text,
                Equipment = cmbEquipment.Text,
                Position = cmbPosition.Text,
                Method = cmbMethod.Text,
                Symptom = txtSymptom.Text,
                Diagnosis = txtDiagnosis.Text,
                DockerAsk = txtRemarks.Text,
                Projects = dgvApplys.DataSource as List<ApplyItem>,
            };
            if (_registerBll.Register(_registerModel, out string errorMsg))
            {
                tabControl1.SelectedTab = tabPage1;
                tabPage2.Parent = null;
                dgvApplyBills.DataSource = null;
                dgvApplyProjects.DataSource = null;
                dgvApplys.DataSource = null;
                patient = new PatientInfo();
                Rest();
                RestTextBox();
                MessageBox.Show("添加成功.");

                //this.Close();
                //this.DialogResult = DialogResult.OK;

            }
            else
            {
                MessageBox.Show(errorMsg);
            }
            btnRegister.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtImageNumber.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            txtImageNumber.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtImageNumber.Text = null;
            txtImageNumber.Enabled = true;
        }

        private PatientInfo patient;
        private void dgvApplyBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex>0)
            {
                if (true)//按照申请单登记
                {
                    var bills = dgvApplyBills.DataSource as List<ApplyBill>;
                    var bill = bills[e.RowIndex];
                    if (bill.RequestStatus.Trim() != "0")
                    {
                        this.ShowInfo("该申请单不符合登记状态.");
                        return;
                    }
                    //本身UI赋值
                    txtName.Text = patient.PatientName;
                    txtAddress.Text = patient.HomeAddress;
                    txtPhone.Text = patient.HomeTel;
                    txtCardNo.Text = patient.EncounterType == "O" ? patient.OutPatientNo : patient.InPatientNo;
                    txtDiagnosis.Text = !string.IsNullOrEmpty(patient.DiagnosisCode) ? patient.DiagnosisCode + "-" + patient.DiagnosisName : "";
                    txtCheckNo.Text = patient.RegCode;
                    txtSymptom.Text = bill.MedicalHistory;
                    txtRemarks.Text = bill.Memo;
                    Init();
                    if (!string.IsNullOrEmpty(patient.SSNumber))
                    {
                        txtIDCard.Text = patient.SSNumber;
                    }
                    else
                    {
                        txtAge.Text = patient.Age;
                        cmbGender.Text = patient.GenderName;
                        cmbGender.SelectedValue = patient.GenderCode;
                    }
                    txtApplyDocter.Text = bill.ReqDoctorName;
                    txtApplyDocterCode.Text = bill.ReqDoctorCode;
                    cmbPatientType.Text = patient.PatientTypeName;
                    cmbPatientType.SelectedValue = patient.PatientType;
                    cmbApplyDept.Text = patient.LocationName;
                    cmbApplyDept.SelectedValue = patient.LocationCode;
                    cmbVisitType.Text = patient.EncounterTypeName;
                    cmbVisitType.SelectedValue = patient.EncounterType;
                    dtpApplyDate.Value = DateTime.ParseExact(bill.RequestDate, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    tabControl1.SelectedTab = tabPage1;
                    dgvApplys.DataSource = dgvApplyProjects.DataSource;
                }
                else
                {
                    return;
                }
            }
        }

        private void dgvApplyBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = dgvApplyBills.DataSource as List<ApplyBill>;
                var apply = list[e.RowIndex];
                dgvApplyProjects.DataSource = apply.ApplyItems;
                var posiModels = apply.ApplyItems.Where(x=>!string.IsNullOrEmpty(x.CheckPointCode)).Select(x => new PositionMethodModel{Code=x.CheckPointCode, Name=x.CheckPointName,IsPosition=1,Status=1 }).ToList();
                var methods = apply.ApplyItems.Where(x => !string.IsNullOrEmpty(x.MethodCode)).Select(x => new PositionMethodModel { Code = x.CheckPointCode, Name = x.CheckPointName, IsPosition = 0, Status = 1 }).ToList();
                posiModels.AddRange(methods);
                var positionTask = _positionMethodBll.AddPositionMethodByHisAsync(posiModels);
            }
        }
    }
}
