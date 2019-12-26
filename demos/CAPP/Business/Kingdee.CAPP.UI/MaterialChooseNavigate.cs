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
using Kingdee.CAPP.UI.ProcessDataManagement;
using System.Runtime.InteropServices;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：物料选择导航树界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-07
    /// </summary>
    public partial class MaterialChooseNavigate : DockContent
    {
        #region 界面控件事件

        public MaterialChooseNavigate()
        {
            InitializeComponent();
        }

        private void MaterialChooseNavigate_Load(object sender, EventArgs e)
        {
            LoadMaterialData();
            //tvMaterial.Height = TotalNodesHeight(tvMaterial.Nodes);
            //SetScorllBarValue();
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadMaterialData();
        }

        private void tvMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvMaterial.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            //物料分类ID+类别ID
            string typeId = selectedNode.Tag == null ? "" : selectedNode.Tag.ToString();
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
                GetMaterialList(typeId, selectedNode.Name);           
        }

        private void tvMaterial_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：加载物料树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        private void LoadMaterialData()
        {
            if (tvMaterial.Nodes.Count > 0)
            {
                return;
            }

            TreeNode root = new TreeNode();
            root.Text = "企业物料库";
            root.ImageKey = "library";
            root.Expand();

            LoadMaterialChildNode(root);

            tvMaterial.Nodes.Add(root);
        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        /// <param name="parentNode"></param>
        private void LoadMaterialChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            //查找根节点(物料)下的子节点
            List<Model.MaterialModule> materialModuleList =
                   MaterialModuleBLL.GetMaterialModuleList();

            if (materialModuleList.Count <= 0) return;

            materialModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.TypeName;
                node.Tag = o.TypeId;
                node.Name = null;
                switch (o.TypeId)
                {
                    case "1":
                        node.ImageKey = "materialG";
                        break;
                    case "2":
                        node.ImageKey = "materialS";
                        break;
                    case "3":
                        node.ImageKey = "materialC";
                        break;
                    case "4":
                        node.ImageKey = "materialC";
                        break;
                    default:
                        break;
                }

                //根据物料类型查找物料分类
                List<Model.BusinessCategoryModule> businessCategoryModuleList
                    = MaterialModuleBLL.GetCategoryModuleListByType(o.TypeId);

                if (businessCategoryModuleList.Count > 0)
                {
                    businessCategoryModuleList.ForEach((c) =>
                    {
                        TreeNode nd = new TreeNode();
                        nd.Text = c.CategoryName;
                        nd.Tag = o.TypeId;
                        nd.Name = c.CategoryId;
                        nd.ImageKey = "class";

                        ////根据物料父分类ID查找是否包含子分类
                        //List<Model.BusinessCategoryModule> childCategoryModuleList
                        //    = MaterialModuleBLL.GetCategoryModuleListByParentId(nd.Name);

                        //if (childCategoryModuleList.Count > 0)
                        //{
                        //    childCategoryModuleList.ForEach((s) =>
                        //    {
                        //        TreeNode nod = new TreeNode();
                        //        nod.Text = s.CategoryName;
                        //        nod.Tag = o.TypeId;
                        //        nod.Name = s.CategoryId;
                        //        nod.ImageKey = "class";

                        //        nd.Nodes.Add(nod);
                        //    }
                        //    );
                        //}
                        GetChildCategoryModuleList(nd);

                        node.Nodes.Add(nd);
                    });
                }

                parentNode.Nodes.Add(node);
            });

        }

        private void GetChildCategoryModuleList(TreeNode nd)
        {
            //根据物料父分类ID查找是否包含子分类
            List<Model.BusinessCategoryModule> childCategoryModuleList
                = MaterialModuleBLL.GetCategoryModuleListByParentId(nd.Name);

            if (childCategoryModuleList.Count > 0)
            {
                childCategoryModuleList.ForEach((s) =>
                {
                    TreeNode nod = new TreeNode();
                    nod.Text = s.CategoryName;
                    nod.Tag = nd.Tag;
                    nod.Name = s.CategoryId;
                    nod.ImageKey = "class";

                    GetChildCategoryModuleList(nod);

                    nd.Nodes.Add(nod);
                }
                );
            }
        }

        /// <summary>
        /// 方法说明：根据分类ID+类型ID获取物料
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        /// <param name="typeId">分类Id</param>
        /// <param name="categoryId">业务类型ID</param>
        private void GetMaterialList(string typeId, string categoryId)
        {
            //根据物料分类ID获取物料列表
            DataTable dt = MaterialModuleBLL.GetMaterialModuleDataByCategoryId(typeId, categoryId, null);

            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name == "MaterialListFrm")
                {
                    isOpened = true;
                    ((MaterialListFrm)form).CategoryTypeId = categoryId;
                    ((MaterialListFrm)form).TypeId = typeId;
                    ((MaterialListFrm)form).RefreshData(dt);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                MaterialListFrm frm = new MaterialListFrm();
                frm.MaterialData = dt;
                frm.CategoryTypeId = categoryId;
                frm.TypeId = typeId;
                MainFrm.mainFrm.OpenModule(frm);
            }
        }
               
        private int intWidth = 0;
        /// <summary>
        /// 方法说明：获取所有节点展开后的高度总和
        /// 作      者：jason.tang
        /// 完成时间：2013-07-15
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <returns></returns>
        //private int TotalNodesHeight(TreeNodeCollection nodes)
        //{
            //int intHeight = 0;
            //foreach (TreeNode node in nodes)
            //{
            //    intHeight += node.Bounds.Height;
            //    if (node.Bounds.X + node.Bounds.Width > intWidth)
            //        intWidth = node.Bounds.X + node.Bounds.Width;

            //    if (node.Nodes.Count > 0)
            //    {
            //        intHeight += TotalNodesHeight(node.Nodes);
            //    }
            //}
            //return intHeight;
        //}

        /// <summary>
        /// 方法说明：设置滚动条的值
        /// 作      者：jason.tang
        /// 完成时间：2013-07-15
        /// </summary>
        //private void SetScorllBarValue()
        //{
            //if (tvMaterial.Height > innerPanel.Height)
            //{
            //    Point pt = new Point(this.innerPanel.AutoScrollPosition.X, this.innerPanel.AutoScrollPosition.Y);
            //    this.VerticalScrollbar.Minimum = 0;
            //    this.VerticalScrollbar.Maximum = this.innerPanel.DisplayRectangle.Height;
            //    this.VerticalScrollbar.LargeChange = VerticalScrollbar.Maximum / VerticalScrollbar.Height + this.innerPanel.Height;
            //    this.VerticalScrollbar.SmallChange = 15;
            //    this.VerticalScrollbar.Value = Math.Abs(this.innerPanel.AutoScrollPosition.Y);

            //    VerticalScrollbar.Visible = true;
            //    outterPanel.Width = innerPanel.Width - VerticalScrollbar.Width - 16;
            //}
            //else
            //{
            //    VerticalScrollbar.Visible = false;
            //    outterPanel.Width += VerticalScrollbar.Width;
            //}
        //}

        #endregion

        //private void customScrollbar1_Scroll(object sender, EventArgs e)
        //{
        //    innerPanel.AutoScrollPosition = new Point(0, VerticalScrollbar.Value);
        //    VerticalScrollbar.Invalidate();
        //    Application.DoEvents();
        //}
        private void tvMaterial_AfterExpand(object sender, TreeViewEventArgs e)
        {
            //tvMaterial.Height = TotalNodesHeight(tvMaterial.Nodes);
            //tvMaterial.Width = intWidth;
            //SetScorllBarValue();
        }

        private void tvMaterial_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            //tvMaterial.Height = TotalNodesHeight(tvMaterial.Nodes);
            //tvMaterial.Width = intWidth;
            //SetScorllBarValue();
        }

        //private void MaterialChooseNavigate_Resize(object sender, EventArgs e)
        //{
        //    outterPanel.Width = this.Width - VerticalScrollbar.Width - 16;
        //    innerPanel.Width = this.Width;
        //    outterPanel.Height = tbMaterial.Height - 16;
        //    innerPanel.Height = tbMaterial.Height - 16;
        //}
                
    }
}
