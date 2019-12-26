using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    public partial class SourceFromDataFrm : BaseSkinForm
    {
        #region 属性声明

        /// <summary>
        /// 来源属性
        /// </summary>
        private string _source;
        public string FieldSource
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

        public SourceFromDataFrm()
        {
            InitializeComponent();
        }

        private void SourceFromDataFrm_Load(object sender, EventArgs e)
        {
            GetTableColumn();
        }

        private void GetTableColumn()
        {
            //List<string> listTableName = new List<string>();
            //listTableName.Add("MAT_MaterialVersion");
            //listTableName.Add("MAT_Folder");

            tvSource.Nodes.Clear();
            //禁止重绘
            tvSource.BeginUpdate();

            //foreach (string name in listTableName)
            //{
            //    DataTable dt = SqlServerControllerBLL.GetTableColumnByName(name);
            
                List<Kingdee.CAPP.Model.BaseResource> listBaseResource = Kingdee.CAPP.BLL.SqlServerControllerBLL.GetBaseResource();
                if (listBaseResource == null || listBaseResource.Count == 0)
                    return;

                List<Kingdee.CAPP.Model.BaseResource> listParent = listBaseResource.FindAll(r => string.IsNullOrEmpty(r.ParentField));

                listParent.ForEach((p) =>
                {
                    TreeNode node = new TreeNode();
                    node.Name = p.FieldCode;
                    node.Text = p.FieldName;
                    node.Tag = p.FieldCode;

                    GetChildNode(node, p.ResourceId);
                    
                    tvSource.Nodes.Add(node);
                });


            tvSource.EndUpdate();
        }

        private void GetChildNode(TreeNode parentNode, string parentField)
        {
            List<Kingdee.CAPP.Model.BaseResourceField> listChild = Kingdee.CAPP.BLL.SqlServerControllerBLL.GetResourceField(parentField);

            if (listChild == null || listChild.Count == 0)
                return;

            listChild.ForEach((c) =>
            {
                TreeNode node = new TreeNode();
                node.Name = c.FieldCode;
                node.Text = c.FieldName;
                node.Tag = c.FieldCode;
                                
                parentNode.Nodes.Add(node);
            });
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tvSource.SelectedNode == null)
            {
                MessageBox.Show("请选择一个资源", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _source = tvSource.SelectedNode.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
