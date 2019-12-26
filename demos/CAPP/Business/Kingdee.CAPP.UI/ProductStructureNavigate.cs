using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：产品导航树界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public partial class ProductStructureNavigate : DockContent
    {
        public ProductStructureNavigate()
        {
            InitializeComponent();
        }        

        private void ProductNavigate_Load(object sender, EventArgs e)
        {
            if (tcStructrueView.SelectedTab == tbMaterial)
            {
                LoadMaterialAndProductData();
            }
            else if (tcStructrueView.SelectedTab == tbPBom)
            {
                LoadPBomData();
            }
        }
        
        private void tvPBOM_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvPBOM.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tcStructrueView.SelectedTab == tbMaterial)
            {
                LoadMaterialAndProductData();
            }
            else if (tcStructrueView.SelectedTab == tbPBom)
            {
                LoadPBomData();
            }
        }

        /// <summary>
        /// 方法说明：加载产品树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        private void LoadPBomData()
        {
            if (tvPBOM.Nodes.Count > 0)
            {
                return;
            }

            //查找根节点(物料版本)下的子节点
            List<Model.MaterialVersionModule> versionModuleList =
                   MaterialModuleBLL.GetMaterialVersionModuleList();

            if (versionModuleList.Count <= 0) return;


            versionModuleList.ForEach((o) =>
            {
                TreeNode root = new TreeNode();
                root.Text = o.Name;
                root.ImageKey = "materialG";
                root.Tag = o.MaterialVerId;
                root.Name = o.BaseId;
                root.Expand();

                LoadPBomChildNode(root);

                tvPBOM.Nodes.Add(root);
            });
        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="parentNode"></param>
        private void LoadPBomChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            //根据BaseId查找PBOM
            List<Model.ProductModule> productModuleList =
                   ProductModuleBLL.GetProductModuleList(parentNode.Name);

            if (productModuleList.Count <= 0) return;

            //productModuleList.ForEach((o) =>
            //{
            //    TreeNode node = new TreeNode();
            //    node.Text = o.FolderName;
            //    node.Tag = o.FolderId;
            //    node.Name = o.FolderId;
            //    node.ImageKey = parentNode.ImageKey;

            //    List<Model.MaterialVersionModule> childProductModuleList =
            //       MaterialModuleBLL.GetProductModuleList(node.Name);
            //    if (childProductModuleList.Count > 0)
            //    {
            //        LoadPBomChildNode(node);
            //    }

            //    parentNode.Nodes.Add(node);
            //});

        }

        /// <summary>
        /// 方法说明：加载物料/产品树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        private void LoadMaterialAndProductData()
        {
            if (tvMaterial.Nodes.Count > 0)
            {
                return;
            }
            //查找根节点(物料版本)下的子节点
            List<Model.MaterialVersionModule> versionModuleList =
                   MaterialModuleBLL.GetMaterialVersionModuleList();

            if (versionModuleList.Count <= 0) return;

            versionModuleList.ForEach((o) =>
            {
                TreeNode root = new TreeNode();
                root.Text = o.Name;
                root.ImageKey = "product";
                root.Tag = o.MaterialVerId;
                root.Name = o.MaterialVerId;
                root.Expand();

                LoadMaterialChildNode(root);

                tvMaterial.Nodes.Add(root);
            });
        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="parentNode">父节点</param>
        private void LoadMaterialChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            //查找根节点(产品文件夹)下的子节点
            List<Model.MaterialRelationModule> relationModuleList =
                   MaterialModuleBLL.GetMaterialVersionModuleListByVersionId(parentNode.Name);

            if (relationModuleList.Count <= 0) return;

            relationModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.name;
                node.Tag = o.ChildVerId;
                node.Name = o.ChildVerId;
                node.ImageKey = "materialG";

                List<Model.MaterialRelationModule> childRelationModuleList =
                   MaterialModuleBLL.GetMaterialVersionModuleListByVersionId(node.Name);
                if (childRelationModuleList.Count > 0)
                {
                    LoadPBomChildNode(node);
                }

                parentNode.Nodes.Add(node);
            });
        }
        
        private void tcStructureView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcStructrueView.SelectedTab == tbMaterial)
            {
                LoadMaterialAndProductData();
            }
            else if (tcStructrueView.SelectedTab == tbPBom)
            {
                LoadPBomData();
            }
        }

        private void tvMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvMaterial.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
        }
    }
}
