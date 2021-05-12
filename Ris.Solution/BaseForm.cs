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

        /// <summary>
        /// 窗体大小更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_SizeChanged(object sender, EventArgs e)
        {
            asc.controlAutoSize(this);
        }

        protected virtual void RestTextBox()
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Text = null;
                }
                if (item.Controls.Count>0)
                {
                    RestTextBox(item);
                }
            }
        }
        private void RestTextBox(Control control )
        {
            foreach (Control item in control.Controls)
            {
                if (item is TextBox)
                {
                    TextBox txt = item as TextBox;
                    txt.Text = null;
                }
                if (item.Controls.Count > 0)
                {
                    RestTextBox(item);
                }
            }
        }

        public static UserModel CurrentUser { get; set; }
    }
}
