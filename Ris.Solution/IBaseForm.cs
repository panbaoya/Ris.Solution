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
        AutoSizeFormClass asc = new AutoSizeFormClass();
        public virtual void SetStyle()
        {
            this.Font = new Font("宋体", AppConfSetting.GlobalFontSize);
            this.Location = new Point { X =this.Location.X- AppConfSetting.GlobalFontSize * (AppConfSetting.GlobalFontSize), Y = this.Location.Y - AppConfSetting.GlobalFontSize * (AppConfSetting.GlobalFontSize-9) };
            this.Text += "-" + AppConfSetting.HospitalName;
            this.Size = new Size { Height = this.Size.Height + 1, Width = this.Width + 1 };
            asc.controllInitializeSize(this);
            this.SizeChanged += BaseForm_SizeChanged;
        }

        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        public static UserModel CurrentUser { get; set; }
    }
}
