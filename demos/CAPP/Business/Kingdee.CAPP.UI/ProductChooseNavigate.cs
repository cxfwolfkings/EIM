using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.UI.ProcessDataManagement;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：产品选择导航树界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-07
    /// </summary>
    public partial class ProductChooseNavigate : DockContent
    {
        #region 窗体控件事件

        public ProductChooseNavigate()
        {
            InitializeComponent();
        }

        private void ProductChooseNavigate_Load(object sender, EventArgs e)
        {
            LoadProductData();
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadProductData();
        }

        private void tvProduct_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProduct.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                GetProduct(selectedNode.Name);
        }

        private void tvProduct_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        private void tvProduct_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：加载产品树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        private void LoadProductData()
        {
            if (tvProduct.Nodes.Count > 0)
            {
                return;
            }

            //查找根节点(产品文件夹)下的子节点
            List<Model.ProductModule> productModuleList =
                   ProductModuleBLL.GetProductModuleList(string.Empty);

            if (productModuleList.Count <= 0) return;

            TreeNode root = new TreeNode();
            root.Text = "工艺文件夹";//productModuleList[0].FolderName;
            root.ImageKey = "folder";
            root.Expand();

            LoadProductChildNode(root);

            tvProduct.Nodes.Add(root);
        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        /// <param name="parentNode"></param>
        private void LoadProductChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            //查找根节点(产品文件夹)下的子节点
            List<Model.ProductModule> productModuleList =
                   ProductModuleBLL.GetProductModuleList(parentNode.Name);

            if (productModuleList.Count <= 0) return;

            productModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.FolderName;
                node.Tag = o.FolderId;
                node.Name = o.FolderId;
                node.ImageKey = parentNode.ImageKey;

                List<Model.ProductModule> childProductModuleList =
                   ProductModuleBLL.GetProductModuleList(node.Name);
                if (childProductModuleList.Count > 0)
                {
                    LoadProductChildNode(node);
                }

                parentNode.Nodes.Add(node);
            });

        }

        /// <summary>
        /// 方法说明：根据节点主键值获取产品文件夹数据
        /// 作    者：Jason.tang
        /// 完成时间：2013-03-17
        /// </summary>
        /// <param name="name">节点名</param>
        private void GetProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            //文件夹ID
            string folderid = name;
            
            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name == "ProductListFrm")
                {
                    isOpened = true;
                    ((ProductListFrm)form).RefreshProductData(name);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                ProductListFrm frm = new ProductListFrm();
                frm.FolderId = name;
                MainFrm.mainFrm.OpenModule(frm);
            }
        }

        #endregion
    }
}
