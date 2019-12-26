using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    /// <summary>
    /// 类型说明：来源窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-01-04
    /// </summary>
    public partial class SourceFrm : Form
    {
        public SourceFrm()
        {
            InitializeComponent();
        }

        #region 属性声明

        /// <summary>
        /// 来源属性
        /// </summary>
        private string _source;
        public string ParameterSource
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        #endregion

        #region 界面控件事件

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //_source = txtSource.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 参数
        /// </summary>
        private void btnParameter_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 没有来源
        /// </summary>
        private void btnNonSource_Click(object sender, EventArgs e)
        {
            txtSource.Text = null;
            _source = null;
        }

        /// <summary>
        /// 其他来源
        /// </summary>
        private void btnOtherSource_Click(object sender, EventArgs e)
        {
            //groupBox1.Enabled = false;
            //this.Height = 478;
            txtSource.Text = "Other";
            _source = "Other";
        }
        

        private void SourceFrm_Load(object sender, EventArgs e)
        {
            BindTreeView();
        }

        /// <summary>
        /// 点击TreeView节点
        /// </summary>
        private void tvSource_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Nodes.Count == 0)
            {
                txtSource.Text = e.Node.Parent.Text + "\\" + e.Node.Text;            
                _source = e.Node.Parent.Tag.ToString() + "\\" + e.Node.Tag.ToString();
            }
        }

        #endregion


        #region 方法

        /// <summary>
        /// 方法说明：绑定TreeView控件
        /// 作    者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        protected void BindTreeView()
        {
            //利用xmldoc对象读取xml文件
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Application.StartupPath + "\\Resources\\source.xml");
            tvSource.Nodes.Clear();
            //禁止重绘
            tvSource.BeginUpdate();

            //读取根节点
            XmlElement xmlele = xmldoc.DocumentElement;
            TreeNode tn = new TreeNode();
            tn.Text = "来源参数"; //xmlele.Attributes["name"].Value;            
            //获取根节点下的所有节点
            XmlNodeList xmlnl = xmlele.ChildNodes;
            //递归遍历节点 
            TreeNode tn_nodes = null;
            foreach (XmlNode xmlnode in xmlnl)
            {
                if (xmlnode.HasChildNodes)
                {
                    tn_nodes = new TreeNode();
                    tn_nodes.Text = xmlnode.Attributes["name"].Value;
                    tn_nodes.Tag = xmlnode.Attributes["id"].Value;
                    GetNodes(xmlnode, tn_nodes);
                    tn.Nodes.Add(tn_nodes);
                }
            }
            tvSource.Nodes.Add(tn);

            tvSource.EndUpdate();
        }

        /// <summary>
        /// 方法说明：递归遍历节点
        /// 作    者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        /// <param name="xmlnode">当前xml文件中的节点</param>
        /// <param name="tn">treeview中当前节点</param>
        public void GetNodes(XmlNode xmlnd, TreeNode tn)
        {
            //获取根节点下的所有节点
            XmlNodeList xmlnl = xmlnd.ChildNodes;
            TreeNode tn_nodes = null;
            foreach (XmlNode xmlnode in xmlnl)
            {
                if (xmlnode.ChildNodes.Count > 1)
                {
                    tn_nodes = new TreeNode();
                    tn_nodes.Text = xmlnode.Attributes["name"].Value;
                    tn_nodes.Tag = xmlnode.Attributes["id"].Value;
                    XmlElement xml = (XmlElement)xmlnode;
                    GetNodes(xmlnode, tn_nodes);
                    tn.Nodes.Add(tn_nodes);
                }
                else
                {
                    tn_nodes = new TreeNode();
                    tn_nodes.Text = xmlnode.InnerText;
                    tn_nodes.Tag = xmlnode.Attributes["id"].Value;
                    tn.Nodes.Add(tn_nodes);
                }
            }
        }

        #endregion

        private void btnSetOtherSource_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOtherSource.Text))
            {
                MessageBox.Show("来源不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtOtherSource.Focus();
                return;
            }

            txtSource.Text = txtOtherSource.Text;
            groupBox1.Enabled = true;
            this.Height = 385;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            this.Height = 385;
        }

        private void txtSource_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimunSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMinimunSize_MouseHover(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = ResourceNotice.min_hover;
        }

        private void btnMinimunSize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = ResourceNotice.minimumsize;
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = ResourceNotice.close_hover;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = ResourceNotice.close;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }
    }
}
