using Ris.Bll;
using Ris.IBll;
using Ris.Models.PositionMethod;
using Ris.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ris.Ui.Setting
{
    public partial class PositionMethodSettingForm : Form
    {
        PositionMethodModel _updateModel; 
        IPositionMethodBll _positionMethodBll;
        public PositionMethodSettingForm()
        {
            InitializeComponent();
            _positionMethodBll = new PositionMethodBll();
        }

        private void PositionMethodSettingForm_Load(object sender, EventArgs e)
        {
            BindData();
        }

        void BindData()
        {
            tvPosition.Nodes.Clear();
            var list = _positionMethodBll.GetPositionMethods();
            var parents = list.Where(x => x.IsPosition == 1);
            if (parents!=null && parents.Count()>0)
            {
                foreach (var item in parents)
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = item.Name;
                    tn.Text = item.Name;
                    tn.Tag = item.ID.ToString();
                    tn.ForeColor = item.Status == 0 ? Color.Red : Color.Black;
                    BindNote(tn, list, item.ID);
                    tvPosition.Nodes.Add(tn);
                }
            }
            tvPosition.ExpandAll();
        }

        public void BindNote(TreeNode treeNode, List<PositionMethodModel> list,int id)
        {
            var childs = list.Where(x => x.ParentID == id);
            if (childs!=null && childs.Count()>0)
            {
                foreach (var item in childs)
                {
                    TreeNode tn = new TreeNode();
                    tn.Name = item.Name;
                    tn.Text = item.Name;
                    tn.Tag = item.ID.ToString();
                    tn.ForeColor=item.Status == 0 ?  Color.Red :  Color.Black;
                    BindNote(tn, list, item.ID);
                    treeNode.Nodes.Add(tn);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                PositionMethodModel model = new PositionMethodModel()
                {
                    Name = textBox1.Text,
                    ParentID = 0,
                    Status = 1,
                    IsPosition = 1
                };
                _positionMethodBll.AddPositionMethod(model);
            }
            else
            {
                if (tvPosition.SelectedNode!=null && tvPosition.SelectedNode.Level==1)
                {
                    PositionMethodModel model = new PositionMethodModel()
                    {
                        Name = textBox1.Text,
                        ParentID = Convert.ToInt32(tvPosition.SelectedNode.Tag),
                        Status = 1,
                        IsPosition = 0
                    };
                    _positionMethodBll.AddPositionMethod(model);
                }
                else
                {
                    this.ShowInfo("添加方法请选择部位.");
                    return;
                }
            }
            BindData();
        }

        private void 作废ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tvPosition.SelectedNode!=null)
            {
                var node = tvPosition.SelectedNode.Parent;
                PositionMethodModel model = new PositionMethodModel()
                {
                    ID = Convert.ToInt32(tvPosition.SelectedNode.Tag),
                    IsPosition = node != null ? Convert.ToInt32(node.Parent.Tag) : 1,
                    Status = 0,
                };
                _positionMethodBll.UpdatePositionMethod(model);
            }
            BindData();
        }

        private void tvPosition_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tvPosition.SelectedNode = tvPosition.GetNodeAt(e.X, e.Y);
            }
        }

        private void tvPosition_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode node = this.tvPosition.GetNodeAt(e.X, e.Y);
            if (e.X > node.Bounds.Left || e.X < node.Bounds.Right)
            {
                _updateModel = new PositionMethodModel()
                {
                    Name = node.Name,
                    IsPosition = node.Level == 0 ? 1 : 0,
                    ID = Convert.ToInt32(node.Tag)
                };
                textBox1.Text = _updateModel.Name;
                if (_updateModel.IsPosition==1)
                {
                    radioButton1.Checked=true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                _positionMethodBll.UpdatePositionMethod(_updateModel);
            }
        }
    }
}
