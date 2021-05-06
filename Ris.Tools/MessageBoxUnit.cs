using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Tools
{
    public static class MessageBoxUnit
    {
        /// <summary>
        /// show消息提示框
        /// </summary>
        /// <param name="box"></param>
        /// <param name="errorMsg">错误消息</param>
        public static void ShowInfo(this Form form,string errorMsg)
        {
            MessageBox.Show(form, errorMsg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        }
        public static DialogResult ShowQuestion(this Form form, string errorMsg)
        {
            return MessageBox.Show(form, errorMsg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
    }
}
