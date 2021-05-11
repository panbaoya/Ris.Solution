using Ris.Bll;
using Ris.Dal;
using Ris.IBll;
using Ris.Models.Deptment;
using Ris.Models.Enums;
using Ris.Models.InterFaceModel;
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
                //var response = HttpUnit.PostJson("", "");
                //RegisterModel _registerModel = new RegisterModel
                //{
                //    CardNo = txtCardNo.Text,
                //    Name = cmbName.Text,
                //    IDCard = txtIDCard.Text,
                //    PinYin1 = txtPinyin1.Text,
                //    Gender = Convert.ToInt32(cmbGender.SelectedValue),
                //    Address = txtAddress.Text,
                //    BirthDay = txtBirthDay.Text,
                //    CurrentAge = txtAge.Text,
                //    Phone = txtPhone.Text,
                //    PostCode = txtPostCode.Text,
                //    Email = txtEmail.Text,
                //    ImageNumber = txtImageNumber.Text,
                //    PatientType = cmbPatientType.Text,
                //    CheckNo = txtCheckNo.Text,
                //    BillHospital = txtBillHospital.Text,
                //    ApplyDept = cmbApplyDept.SelectedValue.ToString(),
                //    ApplyDeptName = cmbApplyDept.Text,
                //    ApplyDocterName = txtApplyDocter.Text,
                //    Area = txtArea.Text,
                //    Bed = txtBed.Text,
                //    ApplyDate = DateTime.Now,
                //    CheckDept = cmbCheckDept.SelectedValue.ToString(),
                //    CheckDeptName = cmbCheckDept.Text,
                //    CheckType = cmbCheckType.Text,
                //    Equipment = cmbEquipment.Text,
                //    Position = cmbPosition.Text,
                //    Method = cmbMethod.Text,
                //    Symptom = txtSymptom.Text,
                //    Diagnosis = txtDiagnosis.Text,
                //    DockerAsk = txtRemarks.Text
                //};
                //var ssss=_registerModel.ToJson();



                //var response = "{\"PatientID\":null,\"Name\":\"潘保亚\",\"CardNo\":\"123\",\"PinYin\":null,\"PinYin1\":\"PanBaoYa\",\"Gender\":1,\"IDCard\":\"410928199106044232\",\"Address\":\"家庭住址\",\"Phone\":\"199216152093\",\"PostCode\":\"123412\",\"CurrentAge\":\"30岁\",\"BirthDay\":\"1991-06-04\",\"Email\":\"123@156.com\",\"ImageNumber\":\"20210506165038\",\"PatientType\":\"自费\",\"BillHospital\":\"开单医院\",\"ApplyDept\":\"ks002\",\"ApplyDeptName\":\"超声科\",\"ApplyDocter\":null,\"ApplyDocterName\":\"申请医师\",\"CheckType\":\"CT\",\"Bed\":\"2床\",\"ApplyDate\":\"2021-05-06T16: 52:14.1880235+08:00\",\"CheckNo\":\"123\",\"CheckDept\":\"ks002\",\"CheckDeptName\":\"超声科\",\"Area\":\"1区\",\"Diagnosis\":\"临床诊断\",\"Equipment\":\"AS\",\"Position\":\"腹部,头部,胸部\",\"Method\":\"肝Ca\",\"Symptom\":\"病例描述\",\"DockerAsk\":\"医师要求\"}";
                //var model = response.JsonToObj<RegisterModel>();
                //txtCardNo.Text = model.CardNo;
                //cmbName.Text = model.Name;
                //txtIDCard.Text = model.IDCard;
                //txtPinyin1.Text = model.PinYin1;
                //cmbGender.SelectedValue = model.Gender.ToString();
                //txtAddress.Text = model.Address;
                //txtBirthDay.Text = model.BirthDay;
                //txtAge.Text = model.CurrentAge;
                //txtPhone.Text = model.Phone;
                //txtPostCode.Text = model.PostCode;
                //txtEmail.Text = model.Email;
                //txtImageNumber.Text = model.ImageNumber;
                //cmbPatientType.Text = model.PatientType;
                //txtCheckNo.Text = model.CheckNo;
                //txtBillHospital.Text = model.BillHospital;
                //cmbApplyDept.SelectedValue = model.ApplyDept;
                //cmbApplyDept.Text = model.ApplyDeptName;
                //txtApplyDocter.Text = model.ApplyDocterName;
                //txtArea.Text = model.Area;
                //txtBed.Text = model.Bed;
                //dtpApplyDate.Value = model.ApplyDate.Value;
                //cmbCheckDept.SelectedValue = model.CheckDept;
                //cmbCheckDept.Text = model.CheckDeptName;
                //cmbCheckType.Text = model.CheckType;
                //cmbEquipment.Text = model.Equipment;
                //cmbPosition.Text = model.Position;
                //cmbMethod.Text = model.Method;
                //txtSymptom.Text = model.Symptom;
                //txtDiagnosis.Text = model.Diagnosis;
                //txtRemarks.Text = model.DockerAsk;



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
                                <CheckPointName/>
                                <CheckPointCode/>
                                <Memo/>
                                <MethodName/>
                                <ItemName>电子胃镜检查</ItemName>
                                <MethodCode/>
                                <Quantity>1</Quantity>
                                <ItemCode>NKJJC1344</ItemCode>
                            </ApplyItem>
                            <ApplyItem>
                                <UnitPrice>400.0000</UnitPrice>
                                <CheckPointName/>
                                <CheckPointCode/>
                                <Memo/>
                                <MethodName/>
                                <ItemName>电子肠镜检查</ItemName>
                                <MethodCode/>
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
                        var patient = applyBill.PatientInfos[0];
                        //his接口数据,异步添加配置
                        var configTask=_typeConfigBll.AddTypeConfigByHisAsync(patient);
                        var deptTask=_deptmentBll.AddDeptmentByHisAsync(patient);

                        //本身UI赋值
                        cmbName.Text = patient.PatientName;
                        txtAddress.Text = patient.HomeAddress;
                        txtPhone.Text = patient.HomeTel;
                        txtCardNo.Text = patient.EncounterType == "O" ? patient.OutPatientNo : patient.InPatientNo;
                        txtDiagnosis.Text =!string.IsNullOrEmpty(patient.DiagnosisCode)? patient.DiagnosisCode + "-" + patient.DiagnosisName:"";
                        txtCheckNo.Text = patient.RegCode;
                        txtSymptom.Text = patient.ApplyBills[0].MedicalHistory;
                        txtRemarks.Text = patient.ApplyBills[0].Memo;

                        dgvApplyBills.DataSource = patient.ApplyBills;
                        tabPage2.Parent = tabControl1;
                        //同步数据,最后加载,怕线程没有执行完.
                        configTask.Wait();
                        deptTask.Wait();
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
                        cmbPatientType.Text = patient.PatientTypeName;
                        cmbPatientType.SelectedValue = patient.PatientType;
                        cmbApplyDept.Text = patient.LocationName;
                        cmbApplyDept.SelectedValue = patient.LocationCode;
                        cmbVisitType.Text = patient.EncounterTypeName;
                        cmbVisitType.SelectedValue = patient.EncounterType;
                    }
                    else
                    {
                        this.ShowInfo("没有患者信息.");
                    }
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
            //绑定模型
            RegisterModel _registerModel = new RegisterModel
            {
                CardNo = txtCardNo.Text,
                Name = cmbName.Text,
                IDCard = txtIDCard.Text,
                PinYin1 = txtPinyin1.Text,
                Gender = Convert.ToInt32(cmbGender.SelectedValue),
                Address = txtAddress.Text,
                BirthDay = txtBirthDay.Text,
                CurrentAge = txtAge.Text,
                Phone = txtPhone.Text,
                PostCode = txtPostCode.Text,
                Email = txtEmail.Text,
                ImageNumber = txtImageNumber.Text,
                PatientType = cmbVisitType.Text,
                CheckNo = txtCheckNo.Text,
                BillHospital = txtBillHospital.Text,
                ApplyDept = cmbApplyDept.SelectedValue.ToString(),
                ApplyDeptName = cmbApplyDept.Text,
                ApplyDocterName = txtApplyDocter.Text,
                Area = txtArea.Text,
                Bed = txtBed.Text,
                ApplyDate = DateTime.Now,
                CheckDept = cmbCheckDept.SelectedValue.ToString(),
                CheckDeptName = cmbCheckDept.Text,
                CheckType = cmbCheckType.Text,
                Equipment = cmbEquipment.Text,
                Position = cmbPosition.Text,
                Method = cmbMethod.Text,
                Symptom = txtSymptom.Text,
                Diagnosis = txtDiagnosis.Text,
                DockerAsk = txtRemarks.Text
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

        private void button1_Click(object sender, EventArgs e)
        {
            txtImageNumber.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtImageNumber.Text = null;
            txtImageNumber.Enabled = true;
        }

        private void dgvApplyBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvApplyBills_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var list = dgvApplyBills.DataSource as List<ApplyBill>;
                var apply = list[e.RowIndex];
                dgvApplyProjects.DataSource = apply.ApplyItems;
            }
        }
    }
}
