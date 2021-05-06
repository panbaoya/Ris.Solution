using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace CustomControl
{

    /// <summary>
    /// 功能描述:自定义多选下拉框
    /// 作　　者:huangzh
    /// 创建日期:2016-01-04 11:57:13
    /// 任务编号:
    /// </summary>
    public class CheckComboBox : ComboBox
    {
        PopupForm frmhost;
        TreeView lst = new TreeView();


        public CheckComboBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;//只有设置这个属性为OwnerDrawFixed才可能让重画起作用

            lst.Width = ListWidth == 0 ? this.Width : ListWidth;
            //lst.Height = ListHeight == 0 ? 300 : ListHeight;
            lst.CheckBoxes = true;
            lst.ShowLines = false;
            lst.ShowPlusMinus = false;
            lst.ShowRootLines = false;
            lst.KeyUp += new KeyEventHandler(lst_KeyUp);
            lst.AfterCheck += new TreeViewEventHandler(lst_AfterCheck);
            this.DropDownHeight = 1;
            frmhost = new PopupForm(lst);
            //frmhost.Closed += new ToolStripDropDownClosedEventHandler(frmhost_Closed);
            //frmhost.Opened += new EventHandler(frmhost_Opened);
        }

        void lst_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        void lst_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                if (Text != "")
                {
                    Text += ",";
                }
                Text += e.Node.Tag.ToString();
            }
            else
            {
                string strValue = e.Node.Tag.ToString().Trim();
                if (Text != "")
                {
                    List<string> strs = Text.Split(',').ToList();
                    strs.Remove("");
                    if (strs.Contains(strValue))
                    {
                        strs.Remove(strValue);
                        Text = string.Join(",", strs);
                    }
                }
            }
        }

        void frmhost_Opened(object sender, EventArgs e)
        {

        }

        void frmhost_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {

        }

        #region Property
        /// <summary>
        /// 选中项
        /// </summary>
        [Description("选定项的值"), Category("Data")]
        public List<TreeNode> SelectedItems
        {
            get
            {
                List<TreeNode> lsttn = new List<TreeNode>();
                foreach (TreeNode tn in lst.Nodes)
                {
                    if (tn.Checked)
                    {
                        lsttn.Add(tn);
                    }
                }
                return lsttn;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [Description("数据源"), Category("Data")]
        public new object DataSource
        {
            get;
            set;
        }
        /// <summary>
        /// 显示字段
        /// </summary>
        [Description("显示字段"), Category("Data")]
        public string DisplayFiled
        {
            get;
            set;
        }
        /// <summary>
        /// 值字段
        /// </summary>
        [Description("值字段"), Category("Data")]
        public string ValueFiled
        {
            get;
            set;
        }
        /// <summary>
        /// 列表高度
        /// </summary>
        [Description("列表高度"), Category("Data")]
        public int ListHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 列表宽度
        /// </summary>
        [Description("列表宽度"), Category("Data")]
        public int ListWidth
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 功能描述:绑定数据
        /// 作　　者:huangzh
        /// 创建日期:2016-01-04 10:38:51
        /// 任务编号:
        /// </summary>
        public void DataBind()
        {
            this.BeginUpdate();
            if (DataSource != null)
            {
                if (DataSource is IDataReader)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(DataSource as IDataReader);

                    DataBindToDataTable(dataTable);
                }
                else if (DataSource is DataView || DataSource is DataSet || DataSource is DataTable)
                {
                    DataTable dataTable = null;

                    if (DataSource is DataView)
                    {
                        dataTable = ((DataView)DataSource).ToTable();
                    }
                    else if (DataSource is DataSet)
                    {
                        dataTable = ((DataSet)DataSource).Tables[0];
                    }
                    else
                    {
                        dataTable = ((DataTable)DataSource);
                    }

                    DataBindToDataTable(dataTable);
                }
                else if (DataSource is IEnumerable)
                {
                    DataBindToEnumerable((IEnumerable)DataSource);
                }
                else
                {
                    throw new Exception("DataSource doesn't support data type: " + DataSource.GetType().ToString());
                }
            }
            else
            {
                lst.Nodes.Clear();
            }

            this.EndUpdate();
        }

        /// <summary>
        /// 功能描述:绑定Table数据
        /// 作　　者:huangzh
        /// 创建日期:2016-01-04 10:47:27
        /// 任务编号:
        /// </summary>
        /// <param name="dt">dt</param>
        private void DataBindToDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                if (!string.IsNullOrEmpty(DisplayFiled) && !string.IsNullOrEmpty(ValueFiled))
                {
                    tn.Text = dr[DisplayFiled].ToString();
                    tn.Tag = dr[ValueFiled].ToString();
                }
                else if (string.IsNullOrEmpty(ValueFiled))
                {
                    tn.Text = dr[DisplayFiled].ToString();
                    tn.Tag = dr[DisplayFiled].ToString();
                }
                else if (string.IsNullOrEmpty(DisplayFiled))
                {
                    tn.Text = dr[ValueFiled].ToString();
                    tn.Tag = dr[ValueFiled].ToString();
                }
                else
                {
                    throw new Exception("ValueFiled和DisplayFiled至少保证有一项有值");
                }
                tn.ToolTipText = tn.Text;

                tn.Checked = false;
                lst.Nodes.Add(tn);
            }
        }

        /// <summary>
        /// 绑定到可枚举类型
        /// </summary>
        /// <param name="enumerable">可枚举类型</param>
        private void DataBindToEnumerable(IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object currentObject = enumerator.Current;
                lst.Nodes.Add(CreateListItem(currentObject));
            }
        }


        /// <summary>
        /// 功能描述:获取一个CheckBox
        /// 作　　者:huangzh
        /// 创建日期:2016-01-04 10:53:12
        /// 任务编号:
        /// </summary>
        /// <param name="obj">obj</param>
        /// <returns>返回值</returns>
        private TreeNode CreateListItem(Object obj)
        {
            TreeNode item = new TreeNode();

            if (obj is string)
            {
                item.Text = obj.ToString();
                item.Tag = obj.ToString();
            }
            else
            {
                if (DisplayFiled != "")
                {
                    item.Text = GetPropertyValue(obj, DisplayFiled);
                }
                else
                {
                    item.Text = obj.ToString();
                }

                if (ValueFiled != "")
                {
                    item.Tag = GetPropertyValue(obj, ValueFiled);
                }
                else
                {
                    item.Tag = obj.ToString();
                }
            }
            item.ToolTipText = item.Text;
            return item;
        }

        /// <summary>
        /// 取得属性值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        private string GetPropertyValue(object obj, string propertyName)
        {
            object result = null;

            result = ObjectUtil.GetPropertyValue(obj, propertyName);
            return result == null ? String.Empty : result.ToString();
        }

        #region override

        /// <summary>
        /// 功能描述:OnKeyUp
        /// 作　　者:huangzh
        /// 创建日期:2016-01-04 11:58:33
        /// 任务编号:
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool Pressed = (e.Control && ((e.KeyData & Keys.A) == Keys.A));
            if (Pressed)
            {
                this.Text = "";
                for (int i = 0; i < lst.Nodes.Count; i++)
                {
                    lst.Nodes[i].Checked = true;
                }
            }
        }

        /// <summary>
        /// 功能描述:OnDropDown
        /// 作　　者:huangzh
        /// 创建日期:2016-01-04 11:58:53
        /// 任务编号:
        /// </summary>
        /// <param name="e">e</param>
        protected override void OnDropDown(EventArgs e)
        {
            this.DroppedDown = false;
            string strValue = this.Text;
            if (!string.IsNullOrEmpty(strValue))
            {
                List<string> lstvalues = strValue.Split(',').ToList<string>();
                foreach (TreeNode tn in lst.Nodes)
                {
                    if (tn.Checked && !lstvalues.Contains(tn.Tag.ToString()) && !string.IsNullOrEmpty(tn.Tag.ToString().Trim()))
                    {
                        tn.Checked = false;
                    }
                    else if (!tn.Checked && lstvalues.Contains(tn.Tag.ToString()) && !string.IsNullOrEmpty(tn.Tag.ToString().Trim()))
                    {
                        tn.Checked = true;
                    }
                }
            }
            else
            {
                foreach (TreeNode tn in lst.Nodes)
                {
                    tn.Checked = false;
                }
            }
            frmhost.Show(this);
        }
        #endregion
    }


    /// <summary>
    /// 对象帮助类
    /// </summary>
    public class ObjectUtil
    {
        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <param name="obj">可能是DataRowView或一个对象</param>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue(object obj, string propertyName)
        {
            object result = null;

            try
            {
                if (obj is DataRow)
                {
                    result = (obj as DataRow)[propertyName];
                }
                else if (obj is DataRowView)
                {
                    result = (obj as DataRowView)[propertyName];
                }
                else if (obj is JObject)
                {
                    result = (obj as JObject).Value<JValue>(propertyName).Value; //.getValue(propertyName);
                }
                else
                {
                    result = GetPropertyValueFormObject(obj, propertyName);
                }
            }
            catch (Exception)
            {
                // 找不到此属性
            }

            return result;
        }

        /// <summary>
        /// 获取对象的属性值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyName">属性名（"Color"、"BodyStyle"或者"Info.UserName"）</param>
        /// <returns>属性值</returns>
        private static object GetPropertyValueFormObject(object obj, string propertyName)
        {
            object rowObj = obj;
            object result = null;

            if (propertyName.IndexOf(".") > 0)
            {
                string[] properties = propertyName.Split('.');
                object tmpObj = rowObj;

                for (int i = 0; i < properties.Length; i++)
                {
                    PropertyInfo property = tmpObj.GetType().GetProperty(properties[i], BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (property != null)
                    {
                        tmpObj = property.GetValue(tmpObj, null);
                    }
                }

                result = tmpObj;
            }
            else
            {
                PropertyInfo property = rowObj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (property != null)
                {
                    result = property.GetValue(rowObj, null);
                }
            }

            return result;
        }
    }
}