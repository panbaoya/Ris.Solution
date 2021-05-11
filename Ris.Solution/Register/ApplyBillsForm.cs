using Ris.Models.InterFaceModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui.Register
{
    public partial class ApplyBillsForm : BaseForm
    {
        public PatientInfo _patient;
        public ApplyBillsForm()
        {
            InitializeComponent();
        }

        public ApplyBillsForm(PatientInfo patient)
        {
            InitializeComponent();
            _patient = patient;
        }
    }
}
