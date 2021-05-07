using Ris.Models.User;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui
{
    /// <summary>
    /// 窗体接口
    /// </summary>
    public interface IBaseForm
    {
        void SetStyle();
    }

    /// <summary>
    /// 窗体接口
    /// </summary>
    public class BaseForm:Form, IBaseForm
    {
        public virtual void SetStyle()
        {
            this.Font = new Font("宋体", AppConfSetting.GlobalFontSize);
            this.Text += "-" + AppConfSetting.HospitalName;
        }

        public static UserModel CurrentUser { get; set; }
    }
}
