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
using Kingdee.CAPP.Model;
using Kingdee.CAPP.UI.SpecialModule;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：物料导航树界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public partial class MaterialStructureNavigate : DockContent
    {
        #region 变量和属性声明

        private Point p;

        /// <summary>
        /// 分类ID+类型ID集合
        /// </summary>
        public List<string> CategoryTypeIds
        {
            get;
            set;
        }

        private List<MaterialModule> materialModuleList;

        /// <summary>
        /// 静态属性公布当前窗体，便于其他窗体调用该窗体公用方法
        /// </summary>
        public static MaterialStructureNavigate materialNavigateFrm { get; set; }

        #endregion

        #region 窗体控件事件

        public MaterialStructureNavigate()
        {
            InitializeComponent();

            materialNavigateFrm = this;
        }

        private void MaterialNavigate_Load(object sender, EventArgs e)
        {
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                LoadMaterialData();
            }
            else
            {
                LoadPbomData();
            }

            cmsDesignBom.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
            contextMenuStrip.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                LoadMaterialData();
            }
            else
            {
                LoadPbomData();
            }
        }

        /// <summary>
        /// 物料树节点鼠标点击事件
        /// </summary>
        private void tvMaterial_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvMaterialDesign.GetNodeAt(p);
            if (selectedNode == null) return;
            tvMaterialDesign.SelectedNode = selectedNode;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
            if (selectedNode.ImageKey == "card")
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    //打开卡片
                    FormCollection collection = Application.OpenForms;
                    bool isOpened = false;
                    foreach (Form form in collection)
                    {
                        if (form.Name.EndsWith(selectedNode.Tag.ToString()))
                        {
                            isOpened = true;
                            ((ProcessCardFrm)form).TabText = selectedNode.Text;
                            ((ProcessCardFrm)form).ModuleObject = selectedNode.Parent.Tag;
                            ((ProcessCardFrm)form).OpenCard(null, selectedNode.Tag.ToString(), false, false);
                            form.Select();
                        }
                    }

                    if (!isOpened)
                    {
                        ProcessCardFrm frm = new ProcessCardFrm();
                        frm.TabText = selectedNode.Text;
                        frm.Name = string.Format("ProcessCardFrm-{0}", selectedNode.Tag.ToString());
                        frm.ModuleObject = selectedNode.Parent.Tag;
                        MainFrm.mainFrm.OpenModule(frm);
                        bool result = frm.OpenCard(null, selectedNode.Tag.ToString(), false, false);
                        if (!result)
                        {
                            MainFrm.mainFrm.CloseModule(frm);
                        }
                    }
                }
                tsmnuNewCard.Visible = false;
                tsmnuAddProcessPlanning.Visible = false;
                tsmnuMaterialQuota.Visible = false;
                tsmnuChangeToTypical.Visible = true;
                tsmnuDeleteCard.Visible = true;
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    GetMaterial(selectedNode.Tag);
                }
                tsmnuNewCard.Visible = true;//selectedNode.Parent != null;
                tsmnuAddProcessPlanning.Visible = true; // selectedNode.Parent != null;

                tsmnuMaterialQuota.Visible = true;
                if(selectedNode.SelectedImageKey == "materialG" ||
                    selectedNode.SelectedImageKey == "materialS" ||
                    selectedNode.SelectedImageKey == "materialC")
                {
                    if (selectedNode.Nodes.Count > 0 && selectedNode.Nodes[0].ImageKey == "card")
                    {
                        tsmnuChangeToTypical.Visible = true;
                    }
                    else
                    {
                        tsmnuChangeToTypical.Visible = false;
                    }
                }
                else 
                    tsmnuChangeToTypical.Visible = false;
                tsmnuDeleteCard.Visible = false;
            }
        }

        /// <summary>
        /// 产品树节点鼠标点击事件
        /// </summary>
        private void tvProduct_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvMaterialPBom.GetNodeAt(p);
            if (selectedNode == null) return;            
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
            tvMaterialPBom.SelectedNode = selectedNode;
            //if(e.Button == System.Windows.Forms.MouseButtons.Left)
            //    GetPbom(selectedNode.Tag);

            if (selectedNode.ImageKey == "card")
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    //打开卡片
                    FormCollection collection = Application.OpenForms;
                    bool isOpened = false;

                    string pbomId = selectedNode.Parent.Parent.Tag.ToString();//改为自身
                    if (selectedNode.Parent.Tag.GetType() == typeof(MaterialModule))
                    {
                        pbomId = ((MaterialModule)selectedNode.Parent.Tag).pbomid;
                    }
                    foreach (Form form in collection)
                    {
                        if (form.Name.EndsWith(selectedNode.Tag.ToString()))
                        {
                            isOpened = true;
                            ((ProcessCardFrm)form).ModuleObject = selectedNode.Parent.Tag;
                            ((ProcessCardFrm)form).PBomID = pbomId;
                            ((ProcessCardFrm)form).TabText = selectedNode.Text;
                            ((ProcessCardFrm)form).OpenCard(null, selectedNode.Tag.ToString(), false, false);
                            form.Select();
                        }
                    }

                    if (!isOpened)
                    {
                        ProcessCardFrm frm = new ProcessCardFrm();
                        frm.ModuleObject = selectedNode.Parent.Tag;
                        frm.PBomID = pbomId;
                        frm.TabText = selectedNode.Text;
                        frm.Name = string.Format("ProcessCardFrm-{0}", selectedNode.Tag.ToString());
                        MainFrm.mainFrm.OpenModule(frm);
                        bool result = frm.OpenCard(null, selectedNode.Tag.ToString(), false, false);
                        if (!result)
                        {
                            MainFrm.mainFrm.CloseModule(frm);
                        }
                    }
                }
                tsmnuNewCard.Visible = false;
                tsmnuAddProcessPlanning.Visible = false;
                tsmnuMaterialQuota.Visible = false;
                tsmnuChangeToTypical.Visible = false;
                tsmnuDeleteCard.Visible = true;
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    GetMaterial(selectedNode.Tag);
                }
                //PBOM时根节点物料挂卡片有问题
                tsmnuNewCard.Visible = true; //selectedNode.Parent != null;
                tsmnuAddProcessPlanning.Visible = true; // selectedNode.Parent != null;
                tsmnuMaterialQuota.Visible = false;
                //if (selectedNode.SelectedImageKey == "materialG" ||
                //    selectedNode.SelectedImageKey == "materialS" ||
                //    selectedNode.SelectedImageKey == "materialC")
                //{
                //    if (selectedNode.Nodes.Count > 0 && selectedNode.Nodes[0].ImageKey == "card")
                //    {
                //        tsmnuChangeToTypical.Visible = true;
                //    }
                //    else
                //    {
                //        tsmnuChangeToTypical.Visible = false;
                //    }
                //}
                //else
                    tsmnuChangeToTypical.Visible = false;
                tsmnuDeleteCard.Visible = false;
            }
        }

        /// <summary>
        /// TabControl Page切换点击事件
        /// </summary>
        private void tcDesignView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                LoadMaterialData();
            }
            else
            {
                LoadPbomData();
            }
        }

        /// <summary>
        /// 新建卡片
        /// </summary>
        private void tsmnuNewCard_Click(object sender, EventArgs e)
        {
            NewCard();
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        private void tsmnuNewFolder_Click(object sender, EventArgs e)
        {
            NewFolder();
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        private void tsmnuDeleteFolder_Click(object sender, EventArgs e)
        {
            DeleteFolder();
        }

        /// <summary>
        /// 材料定额
        /// </summary>
        private void tsmnuMaterialQuota_Click(object sender, EventArgs e)
        {
            object obj = tvMaterialDesign.SelectedNode.Tag;
            if (obj == null)
                return;
            using (MaterialQuotaToolFrm form = new MaterialQuotaToolFrm())
            {
                MaterialModule materia = (MaterialModule)obj;
                form.MaterialVerId = materia.materialverid;
                form.ShowDialog();
            }
        }

        private void tvMaterialPBom_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        private void tvMaterialPBom_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        /// <summary>
        /// 转为典型
        /// </summary>
        private void tsmnuChangeToTypical_Click(object sender, EventArgs e)
        {
            if(tvMaterialDesign.SelectedNode == null || tvMaterialDesign.SelectedNode.Tag == null)
                return;

            TreeNode node = tvMaterialDesign.SelectedNode;
            string processCardId = string.Empty;
            string processCardName = string.Empty;
            Dictionary<string, string> dicCardIds = new Dictionary<string, string>();

            if (node.SelectedImageKey == "card")
            {                
                processCardId = node.Tag.ToString();
                processCardName = node.Text;

            }
            else if(node.Nodes.Count > 0)
            {
                foreach (TreeNode nd in node.Nodes)
                {
                    dicCardIds.Add(nd.Tag.ToString(), nd.Text);
                }
            }

            using (TypicalCategoryFrm form = new TypicalCategoryFrm())
            {
                form.ProcessCardId = processCardId;
                form.ProcessCardName = processCardName;
                form.ProcessCardIds = dicCardIds;
                form.ShowDialog();
            }


        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：加载物料树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        private void LoadMaterialData()
        {
            if (tvMaterialDesign.Nodes.Count > 0)
            {
                return;
            }
          
            if (CategoryTypeIds.Count > 0)
            {
                LoadMaterialChildNode(CategoryTypeIds[0], CategoryTypeIds[1]);
            }           

        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="parentNode"></param>
        private void LoadMaterialChildNode(string categoryid, string code)
        {            
            //if (parentNode == null) return;

            //parentNode.Nodes.Clear();

            //查找根节点(物料)下的子节点
            materialModuleList = MaterialModuleBLL.GetMaterialModuleListByCategoryId(categoryid, code);

            if (materialModuleList.Count <= 0) return;

            materialModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = string.Format("({0})", o.name);
                node.Tag = o;
                node.Name = o.baseid;
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
                node.ExpandAll();

                //根据物料类型查找物料分类
                List<Model.MaterialVersionModule> versionModuleList
                    = MaterialModuleBLL.GetChildMaterialByVersionId(o.materialverid);

                if (versionModuleList.Count > 0)
                {
                    versionModuleList.ForEach((v) =>
                    {
                        TreeNode nod = new TreeNode();
                        MaterialModule mod = new MaterialModule();
                        mod.code = v.Code;
                        mod.name = v.Name;
                        mod.materialverid = v.MaterialVerId;
                        mod.baseid = v.BaseId;
                        mod.productname = v.ProductId;

                        nod.Text = string.Format("{0}({1})", v.Name, v.Code);
                        //nod.Tag = v;
                        nod.Tag = mod;
                        nod.Name = v.MaterialVerId;
                        nod.ImageKey = "materialC";

                        //TreeNode nod = new TreeNode();
                        //nod.Text = string.Format("{0}({1})", v.Code, v.Name);
                        //nod.Tag = v.MaterialVerId;
                        //nod.Name = v.BaseId;
                        //nod.ImageKey = "materialC";

                        ShowMaterialChildNode(nod, v.MaterialVerId);

                        List<Model.ProcessCard> materialCardRelations =
                            MaterialCardRelationBLL.GetProcessCardByMaterialId(v.BaseId, 1);

                        if (materialCardRelations.Count > 0)
                        {
                            materialCardRelations.ForEach((m) =>
                            {
                                TreeNode nd = new TreeNode();
                                nd.Text = m.Name;
                                nd.Tag = m.ID;
                                nd.Name = m.ID.ToString();
                                nd.ImageKey = "card";

                                nod.Nodes.Add(nd);
                                nod.ImageKey = "materialCard";                                
                            });
                        }

                        node.Nodes.Add(nod);
                        if (nod.ImageKey == "materialCard")
                        {
                            node.ImageKey = nod.ImageKey;
                        }
                    });
                }

                //parentNode.Nodes.Add(node);
                tvMaterialDesign.Nodes.Add(node);
            });

        }

        /// <summary>
        /// 方法说明：获取下层物料
        /// 作    者：jason.tang
        /// 完成时间：2013-08-30
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="parentVerId">子节点VerID</param>
        private void ShowMaterialChildNode(TreeNode parentNode, string parentVerId)
        {
             //根据物料类型查找物料分类
                List<Model.MaterialVersionModule> versionModuleList
                    = MaterialModuleBLL.GetChildMaterialByVersionId(parentVerId);

                if (versionModuleList.Count > 0)
                {
                    versionModuleList.ForEach((v) =>
                    {
                        TreeNode nod = new TreeNode();
                        MaterialModule mod = new MaterialModule();
                        mod.code = v.Code;
                        mod.name = v.Name;
                        mod.materialverid = v.MaterialVerId;
                        mod.baseid = v.BaseId;
                        mod.productname = v.ProductId;

                        nod.Text = string.Format("{0}({1})", v.Name, v.Code);
                        //nod.Tag = v;
                        nod.Tag = mod;
                        nod.Name = v.MaterialVerId;
                        nod.ImageKey = "materialC";

                        ShowMaterialChildNode(nod, v.MaterialVerId);

                        List<Model.ProcessCard> materialCardRelations =
                            MaterialCardRelationBLL.GetProcessCardByMaterialId(v.BaseId, 1);

                        if (materialCardRelations.Count > 0)
                        {
                            materialCardRelations.ForEach((m) =>
                            {
                                TreeNode nd = new TreeNode();
                                nd.Text = m.Name;
                                nd.Tag = m.ID;
                                nd.Name = m.ID.ToString();
                                nd.ImageKey = "card";

                                nod.Nodes.Add(nd);
                                nod.ImageKey = "materialCard";
                            });
                        }

                        parentNode.Nodes.Add(nod);
                        if (nod.ImageKey == "materialCard")
                        {
                            parentNode.ImageKey = nod.ImageKey;
                        }
                    });
                }
        }

        /// <summary>
        /// 方法说明：加载产品树数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        private void LoadPbomData()
        {
            if (tvMaterialPBom.Nodes.Count > 0)
            {
                return;
            }

            if (CategoryTypeIds.Count > 0)
            {
                LoadPbomChildNode(CategoryTypeIds[0], CategoryTypeIds[1]);
            }           
        }

        /// <summary>
        /// 方法说明：根据根节点加载子节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="parentNode"></param>
        private void LoadPbomChildNode(string categoryid, string code)
        {
            //查找根节点(物料)下的子节点
            materialModuleList = MaterialModuleBLL.GetMaterialModuleListByCategoryId(CategoryTypeIds[0], CategoryTypeIds[1]);

            if (materialModuleList.Count <= 0) return;


            List<string> listBaseIds = new List<string>();
            foreach (MaterialModule material in materialModuleList)
            {
                listBaseIds.Add(string.Format("'{0}'", material.baseid));
            }
            string baseIds = string.Join(",", listBaseIds.ToArray());
            List<PBomModule> pbomModuleList = MaterialModuleBLL.GetPBomModuleListByBaseId(baseIds);

            if (pbomModuleList.Count <= 0)
                return;

            pbomModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.FolderName;
                node.Tag = o.VerId;
                node.Name = o.VerId;
                node.ImageKey = "materialG";

                node.ExpandAll();

                //根据物料类型查找物料分类
                List<Model.MaterialVersionModule> versionModuleList
                    = MaterialModuleBLL.GetChildPbomMaterialByPbomId(o.VerId);

                if (versionModuleList.Count > 0)
                {
                    versionModuleList.ForEach((v) =>
                    {
                        TreeNode nod = new TreeNode();
                        MaterialModule mod = new MaterialModule();
                        mod.code = v.Code;
                        mod.name = v.Name;
                        mod.materialverid = v.MaterialVerId;
                        mod.baseid = v.BaseId;
                        mod.productname = v.ProductId;
                        mod.pbomid = v.ChildId;

                        nod.Text = string.Format("{0}({1})", v.Name, v.Code);
                        //nod.Tag = v;
                        nod.Tag = mod;
                        nod.Name = v.ChildId;
                        nod.ImageKey = "materialC";

                        ShowPbomChildNode(nod, v.ChildId);

                        List<Model.ProcessCard> materialCardRelations =
                            MaterialCardRelationBLL.GetProcessCardByMaterialId(v.BaseId, 2);

                        if (materialCardRelations.Count > 0)
                        {
                            materialCardRelations.ForEach((m) =>
                            {
                                TreeNode nd = new TreeNode();
                                nd.Text = m.Name;
                                nd.Tag = m.ID;
                                nd.Name = m.ID.ToString();
                                nd.ImageKey = "card";

                                nod.Nodes.Add(nd);
                                nod.ImageKey = "materialCard";
                            });
                        }

                        node.Nodes.Add(nod);
                        if (nod.ImageKey == "materialCard")
                        {
                            node.ImageKey = nod.ImageKey;
                        }
                    });
                }

                ////if (versionModuleList.Count > 0)
                ////{
                ////    versionModuleList.ForEach((v) =>
                ////    {
                ////        TreeNode nod = new TreeNode();
                ////        MaterialModule mod = new MaterialModule();
                ////        mod.code = v.Code;
                ////        mod.name = v.Name;
                ////        mod.materialverid = v.MaterialVerId;
                ////        mod.baseid = v.BaseId;
                ////        mod.productname = v.ProductId;

                ////        nod.Text = string.Format("{0}({1})", v.Code, v.Name);
                ////        //nod.Tag = v;
                ////        nod.Tag = mod;
                ////        nod.Name = v.BaseId;
                ////        nod.ImageKey = "materialC";

                ////        List<Model.ProcessCard> materialCardRelations =
                ////            MaterialCardRelationBLL.GetProcessCardByMaterialId(v.BaseId, 2);

                ////        if (materialCardRelations.Count > 0)
                ////        {
                ////            materialCardRelations.ForEach((m) =>
                ////            {
                ////                TreeNode nd = new TreeNode();
                ////                nd.Text = m.Name;
                ////                nd.Tag = m.ID;
                ////                nd.Name = m.ID.ToString();
                ////                nd.ImageKey = "card";

                ////                nod.Nodes.Add(nd);
                ////            });
                ////        }

                ////        node.Nodes.Add(nod);
                ////    });
                ////}

                tvMaterialPBom.Nodes.Add(node);
            });

        }

        /// <summary>
        /// 方法说明：获取下层PBOM
        /// 作    者：jason.tang
        /// 完成时间：2013-08-30
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childId">子节点ID</param>
        private void ShowPbomChildNode(TreeNode parentNode, string childId)
        {
            //根据物料类型查找物料分类
                List<Model.MaterialVersionModule> versionModuleList
                    = MaterialModuleBLL.GetChildPbomMaterialByPbomId(childId);

                if (versionModuleList.Count > 0)
                {
                    versionModuleList.ForEach((v) =>
                    {
                        TreeNode nod = new TreeNode();
                        MaterialModule mod = new MaterialModule();
                        mod.code = v.Code;
                        mod.name = v.Name;
                        mod.materialverid = v.MaterialVerId;
                        mod.baseid = v.BaseId;
                        mod.productname = v.ProductId;
                        mod.pbomid = v.ChildId;

                        nod.Text = string.Format("{0}({1})", v.Name, v.Code);
                        //nod.Tag = v;
                        nod.Tag = mod;
                        nod.Name = v.ChildId;
                        nod.ImageKey = "materialC";

                        ShowPbomChildNode(nod, v.ChildId);

                        List<Model.ProcessCard> materialCardRelations =
                            MaterialCardRelationBLL.GetProcessCardByMaterialId(v.BaseId, 2);

                        if (materialCardRelations.Count > 0)
                        {
                            materialCardRelations.ForEach((m) =>
                            {
                                TreeNode nd = new TreeNode();
                                nd.Text = m.Name;
                                nd.Tag = m.ID;
                                nd.Name = m.ID.ToString();
                                nd.ImageKey = "card";

                                nod.Nodes.Add(nd);
                                nod.ImageKey = "materialCard";                                
                            });
                        }

                        parentNode.Nodes.Add(nod);
                        if (nod.ImageKey == "materialCard")
                        {
                            parentNode.ImageKey = nod.ImageKey;
                        }
                    });
                }
        }


        /// <summary>
        /// 方法说明：根据节点主键值加载对应的物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="tag">主键值</param>
        private void GetMaterial(object tag)
        {
            //查找根节点(物料)下的子节点
            if (tag == null)
                return;

            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name == "MaterialPropertyFrm")
                {
                    isOpened = true;
                    ((MaterialPropertyFrm)form).RefreshObject(tag);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                MaterialPropertyFrm frm = new MaterialPropertyFrm();
                frm.MaterialProperty = tag;
                MainFrm.mainFrm.OpenModule(frm);
            }
        }

        /// <summary>
        /// 方法说明：根据节点主键值加载对应的PBOM数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="tag">主键值</param>
        private void GetPbom(object tag)
        {
            if (tag == null)
                return;
            //文件夹ID
            string folderid = tag.ToString();           

            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name == "ProductListFrm")
                {
                    isOpened = true;
                    ((ProductListFrm)form).RefreshProductData(folderid);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                ProductListFrm frm = new ProductListFrm();
                frm.FolderId = folderid;
                MainFrm.mainFrm.OpenModule(frm);
            }
        }

        /// <summary>
        /// 方法说明：新建卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        private void NewCard()
        {
            using (CardModuleChooseFrm chooseFrm = new CardModuleChooseFrm())
            {
                if (chooseFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProcessCardFrm frm = new ProcessCardFrm();
                    frm.ProcessFolderId = chooseFrm.ProcessFolderId;

                    if (tcDesignView.SelectedTab == tbMaterial)
                        frm.Name = string.Format("ProcessCardFrm-{0}-NAVG", Guid.NewGuid().ToString());
                    else
                        frm.Name = string.Format("ProcessCardFrm-{0}-NAVP", Guid.NewGuid().ToString());

                    if (!string.IsNullOrEmpty(chooseFrm.ProcessCardId))
                    {
                        //frm.TabText = chooseFrm.ProcessCardName;
                        //MainFrm.mainFrm.OpenModule(frm);
                        //bool result = frm.OpenCard(null, chooseFrm.ProcessCardId, true);
                        //if (!result)
                        //{
                        //    MainFrm.mainFrm.CloseModule(frm);
                        //}
                        //添加节点
                        ProcessCardBLL bll = new ProcessCardBLL();
                        ProcessCard card = bll.GetProcessCard(new Guid(chooseFrm.ProcessCardId));
                        string baseid = AddNodeInMaterial(card, frm.ProcessFolderId);
                        if (string.IsNullOrEmpty(baseid))
                        {
                            MessageBox.Show("该物料下已包含相同的卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        MaterialCardRelation materialCardRelation = new MaterialCardRelation();
                        materialCardRelation.MaterialCardRelationId = Guid.NewGuid();
                        materialCardRelation.MaterialId = new Guid(baseid);
                        materialCardRelation.ProcessCardId = card.ID;
                        if (tcDesignView.SelectedTab == tbMaterial)
                            materialCardRelation.Type = 1;
                        else
                            materialCardRelation.Type = 2;
                        Kingdee.CAPP.BLL.MaterialCardRelationBLL.AddMaterialCardRelationData(materialCardRelation);

                        return;
                    }

                    #region 设置新增卡片Tab的TabText及Name

                    int tag = 1;
                    int index = 1;
                    List<int> listIndex = new List<int>();
                    foreach (DockContent form in this.MdiChildren)
                    {
                        if (form.Name.StartsWith("ProcessCardFrm") &&
                            form.TabText.StartsWith(chooseFrm.ModuleName))
                        {
                            tag = int.Parse(form.TabText.Substring(form.TabText.IndexOf("-") + 1));
                            listIndex.Add(tag);
                        }
                    }
                    listIndex.Sort();
                    foreach (var i in listIndex)
                    {
                        if (i > 1 && index == 1)
                        {
                            index = 1;
                            break;
                        }

                        if (index > 1)
                        {
                            if (i - listIndex[index - 2] > 1)
                            {
                                index = listIndex[index - 2] + 1;
                                break;
                            }
                        }
                        index++;
                    }
                    frm.TabText = string.Format("{0}-{1}", chooseFrm.ModuleName, index);


                    #endregion

                    frm.ModuleId = chooseFrm.ModuleId;
                    frm.ModuleName = chooseFrm.ModuleName;
                    if (tcDesignView.SelectedTab == tbMaterial)
                    {
                        frm.ModuleObject = tvMaterialDesign.SelectedNode.Tag;
                        frm.PBomID = string.Empty;
                    }
                    else
                    {
                        frm.ModuleObject = tvMaterialPBom.SelectedNode.Tag;
                        frm.PBomID = tvMaterialPBom.SelectedNode.Name;
                    }
                    MainFrm.mainFrm.OpenModule(frm);
                }
            }
        }

        /// <summary>
        /// 方法说明：新建文件夹
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        private void NewFolder()
        {
            TreeNode node = tvMaterialPBom.SelectedNode;

            if (node != null)
            {
                TreeNode tn = new TreeNode();
                tn.ImageKey = "folder";

                ProductModule pm = new ProductModule();
                if (node.Tag != null)
                {
                    pm.ParentFolderId = node.Tag.ToString();
                }
                CommonInputFrm frm = new CommonInputFrm();
                frm.CommonObject = pm;
                
                //从属性类获取对应的Attributes
                AttributeCollection attributes =
                   TypeDescriptor.GetProperties(pm)["FolderCode"].Attributes;

                // 从属性集合获取分类属性
                CategoryAttribute myAttribute =
                   (CategoryAttribute)attributes[typeof(CategoryAttribute)];

                frm.FormTitle = myAttribute.Category;
                //MainFrm.mainFrm.OpenModule(frm);
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    pm = (ProductModule)frm.CommonObject;
                    Guid gid = ProductModuleBLL.InsertPBomFolder(pm);
                    tn.Text = pm.FolderName;
                    tn.Name = gid.ToString();
                    tn.Tag = gid.ToString();
                    node.Nodes.Add(tn);
                }
            }
        }

        /// <summary>
        /// 方法说明：删除文件夹
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        private void DeleteFolder()
        {
            TreeNode node = tvMaterialPBom.SelectedNode;

            if (node != null && node.Tag != null)
            {
                bool result = ProductModuleBLL.DeletePBomFolder(node.Tag.ToString());
                if (result)
                {
                    tvMaterialPBom.Nodes.Remove(node);
                    tvMaterialPBom.Refresh();
                }
            }
        }

        /// <summary>
        /// 方法说明：新增卡片节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        public string AddNodeInMaterial(ProcessCard card, string folderId)
        {
            string baseid = string.Empty;
            TreeNode selectNode = null;
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                selectNode = tvMaterialDesign.SelectedNode;
            }
            else
            {
                selectNode = tvMaterialPBom.SelectedNode;
            }

            if (selectNode == null)
            {
                return null;
            }

            TreeNode[] treeNodes = selectNode.Nodes.Find(card.ID.ToString(), true);
            if (treeNodes != null && treeNodes.Length > 0)
            {
                //MessageBox.Show("该物料下已包含相同的卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            TreeNode node = new TreeNode();
            node.Name = card.Name;
            node.Text = card.Name;
            //int count = selectNode.Nodes.Count + 1;            
            node.Tag = card.ID;
            node.ImageKey = "card";
            selectNode.Nodes.Add(node);
            if (selectNode.Tag != null)
            {
                baseid = ((MaterialModule)selectNode.Tag).baseid;
            }

            DataTable dt = Kingdee.CAPP.BLL.SqlServerControllerBLL.GetUserInfo(LoginFrm.UserName);
            if (dt != null || dt.Rows.Count > 0)
            {                
                ProcessVersion version = new ProcessVersion();
                version.FolderId = folderId; //ProcessCardFrm.processCardFrm.ProcessFolderId;
                version.BaseId = card.ID.ToString();
                version.Name = card.Name;
                version.State = 2;
                version.IsShow = 2;
                version.Creator = dt.Rows[0]["UserId"].ToString();
                version.Code = card.Name;
                Kingdee.CAPP.BLL.ProcessCardBLL.InsertProcessVersion(version, selectNode.Tag);
                if (ProcessCardEditFrm.processCardEditFrm != null)
                {
                    ProcessCardEditFrm.processCardEditFrm.LoadCardData();
                }
            }

            return baseid;
        }

        #endregion

        private void tsmnuDeleteCard_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = null;
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                selectedNode = tvMaterialDesign.GetNodeAt(p);
            }
            else
            {
                selectedNode = tvMaterialPBom.GetNodeAt(p);
            }

            if (selectedNode == null || selectedNode.Tag == null) return;

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 方法说明：根据MaterialId和ProcessCardId删除物料下的卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-08-10
        /// </summary>
        /// <param name="Id">业务ID</param>
        private void RemoveTreeNode(TreeNode currentNode)
        {
            if (currentNode == null || currentNode.Parent.Tag == null) return;

            string processCardId =currentNode.Tag.ToString();

            MaterialModule materialModule = (MaterialModule)currentNode.Parent.Tag;
            string materialId = materialModule.baseid;

            TreeView treeView = new TreeView();
            int type = 1;
            if (tcDesignView.SelectedTab == tbMaterial)
            {
                treeView = tvMaterialDesign;
            }
            else
            {
                treeView = tvMaterialPBom;
                type = 2;
            }

            bool result = MaterialCardRelationBLL.DeleteMaterialCard(materialId, processCardId, type);
            if (result)
            {
                if (!string.IsNullOrEmpty(currentNode.Parent.ImageKey))
                {
                    //tvMaterialDesign.SelectedNode = currentNode.Parent;
                    //tvMaterialDesign.SelectedImageKey = currentNode.Parent.ImageKey;
                    treeView.SelectedNode = currentNode.Parent;
                    treeView.SelectedImageKey = currentNode.Parent.ImageKey;
                }
                else
                {
                    //tvMaterialDesign.SelectedNode = tvMaterialDesign.Nodes[0];
                    /////  如果选中的节点为空,默认为卡片
                    //tvMaterialDesign.SelectedImageKey = tvMaterialDesign.Nodes[0].ImageKey;
                    treeView.SelectedNode = tvMaterialDesign.Nodes[0];
                    treeView.SelectedImageKey = tvMaterialDesign.Nodes[0].ImageKey;
                }

                //Remove treeview
                //tvMaterialDesign.Nodes.Remove(currentNode);
                //tvMaterialDesign.SelectedNode.Expand();
                treeView.Nodes.Remove(currentNode);
                treeView.SelectedNode.Expand();
            }
        }

        private void tsmnuAddProcessPlanning_Click(object sender, EventArgs e)
        {
            using (ProcessPlanningChooseFrm form = new ProcessPlanningChooseFrm())
            {
                if (tcDesignView.SelectedTab == tbMaterial)
                    form.ViewType = 1;
                else
                    form.ViewType = 2;
                form.ShowDialog();
            }
        }
    }
}
