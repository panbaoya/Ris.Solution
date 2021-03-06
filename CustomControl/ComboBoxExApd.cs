using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomControl
{
    /// <summary>
    /// 支持拼音提示、ID提示、模糊查询和文本追加下拉框
    /// </summary>
    public class ComboBoxExApd : ComboBox
    {
        /// <summary>
        /// 支持拼音提示、ID提示、模糊查询和文本追加下拉框
        /// </summary>
        public ComboBoxExApd()
        {
            this.MaxDropDownItems = 20;
            this.IntegralHeight = false;
            SelectedIndexChanged += ComboBoxExApd_SelectedIndexChanged;
            KeyDown += ComboBoxExApd_KeyDown;
        }


        private List<string> _TextSource;
        /// <summary>
        /// 数据源，格式为Text，value，字母
        /// </summary>
        public List<string> TextSource
        {
            get { return _TextSource; }
            set
            {
                _TextSource = value;
                if (value != null)
                {
                    SetDefaultData();
                }
            }
        }


        //上下键操作按下回车时要使当前项生效
        private void ComboBoxExApd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DroppedDown = false;
                ComboBoxExApd_SelectedIndexChanged(sender, null);
            }
        }

        /// <summary>
        /// 追加文本
        /// </summary>
        public string StrAppend = "";
        private List<string> selecteds= new List<string>();

        //下标改变时需要删除已有部位，增加新部位，并判断是鼠标操作还是键盘上下键操作
        private void ComboBoxExApd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndex != -1 && !DroppedDown)
            {
                string strPart1 = this.Items[this.SelectedIndex].ToString();
                if (selecteds.Contains(strPart1))
                {
                    selecteds.Remove(strPart1);
                }
                else
                {
                    selecteds.Add(strPart1);
                }
                if (selecteds.Count>0)
                {
                    StrAppend = string.Join(",", selecteds) + " ";
                }
            }
            Task.Run(() =>
            {
                Thread.Sleep(10);
                this.Invoke(new Action(() =>
                {
                    this.Text = StrAppend;
                    this.SelectionStart = this.Text.Length;
                }));
            });
        }


        /// <summary>
        /// 重写基类文本事件
        /// </summary>
        protected override void OnTextUpdate(EventArgs e)
        {
            if (_TextSource != null && _TextSource.Count > 0)
            {
                this.Text = this.Text.Replace("，", ",");
                StrAppend = this.Text;
                string input = this.Text.Trim().ToUpper();
                this.Items.Clear();
                if (string.IsNullOrEmpty(input))
                {
                    this.Items.AddRange(DefaultList.ToArray());
                }
                else
                {
                    var newList = _TextSource.Where(T => T.Contains(input)).ToList();
                    if (newList.Count == 0)
                    {
                        this.Items.AddRange(DefaultList.ToArray());
                    }
                    else
                    {
                        for (int i = 0; i < newList.Count; i++)
                        {
                            this.Items.Add(newList[i].Split(',')[0]);
                        }
                    }
                }
                this.Select(this.Text.Length, 0);
                this.DroppedDown = true;

                //保持鼠标指针形状  
                Cursor.Current = Cursors.IBeam;
                Cursor.Current = Cursors.Default;
            }
            base.OnTextUpdate(e);
        }


        //默认项集合
        List<string> DefaultList = new List<string>();

        /// <summary>
        /// 设置控件默认项
        /// </summary>
        private void SetDefaultData()
        {
            StrAppend = "";
            this.Text = "";
            DefaultList.Clear();
            this.Items.Clear();
            for (int i = 0; i < _TextSource.Count; i++)
            {
                DefaultList.Add(_TextSource[i].Split(',')[0]);
            }
            this.Items.AddRange(DefaultList.ToArray());
        }


        #region -----基类事件-----


        protected override void OnGotFocus(EventArgs e)
        {
            if (Cursor.Current == Cursors.IBeam)
            {
                this.DroppedDown = true;
            }
            Cursor = Cursors.Default;
            base.OnGotFocus(e);
        }


        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Items.Count != DefaultList.Count)
            {
                this.Items.Clear();
                this.Items.AddRange(DefaultList.ToArray());
            }

            this.DroppedDown = false;
            base.OnLostFocus(e);
        }

        #endregion

    }
}
