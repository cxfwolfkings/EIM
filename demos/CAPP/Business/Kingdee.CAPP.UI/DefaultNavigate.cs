using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.UI.SystemSetting;
using System.Threading;
using Kingdee.CAPP.UI.ProcessDesign;
/*******************************
 * Created By franco
 * Description: module manager
 *******************************/

namespace Kingdee.CAPP.UI
{
    public partial class ModuleManagerFrm : DockContent
    {
        #region 构造函数

        public ModuleManagerFrm()
        {
            InitializeComponent();

            AddNodeCms.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
        }

        #endregion

        #region 窗体控件事件

        private void ModuleManagerFrm_Resize(object sender, EventArgs e)
        {
            tcModuleManager.Height = tvProcessCard.Height = Height;
            tcModuleManager.Width = tvProcessCard.Width = Width;
        }

        private void ModuleManagerFrm_Load(object sender, EventArgs e)
        {           
            bwLoadTree.RunWorkerAsync();
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadTreeData();
        }

        /// <summary>
        /// new add module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // New Card Module
            MainFrm mainFrm = (MainFrm)this.ParentForm;
           mainFrm.AddCardModule();
        }
        
        /// <summary>
        /// new add folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModuleOrFolder(BusinessType.Folder, null);
        }

        private Point p;
        private void tvProcessCard_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessCard.GetNodeAt(p);
            if (selectedNode == null) return;
            tvProcessCard.SelectedNode = selectedNode;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            ShowChildNode(selectedNode);

            if (selectedNode.SelectedImageKey == "folder" || selectedNode.SelectedImageKey == "folder_o")
            {
                tsmnuDeleteFolder.Visible = selectedNode.Parent != null;
                tsmnuDeleteModule.Visible = false;
                AddFolderToolStripMenuItem.Visible = true;
                AddModuleToolStripMenuItem.Visible = true;
            }
            else
            {
                tsmnuDeleteFolder.Visible = false;
                AddFolderToolStripMenuItem.Visible = false;
                AddModuleToolStripMenuItem.Visible = false;
                tsmnuDeleteModule.Visible = true;
            }
        }
        
        /// <summary>
        /// Open Card module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvProcessCard_DoubleClick(object sender, EventArgs e)
        {
            TreeNode node = this.tvProcessCard.GetNodeAt(p);
            if (node.ImageKey != "card")
            {
                return;
            }

            string moduleName = node.Text;
            DockContent content = MainFrm.mainFrm.CheckContentIsOpened(node.Tag.ToString());
            if (content != null)
            {
                MainFrm.mainFrm.OpenModule(content);
                return;
            }

            CardModuleFrm frm = new CardModuleFrm();
            frm.TabText = moduleName;
            frm.Name = string.Format("CardModuleFrm-{0}", node.Tag.ToString());
            frm.ModuleId = node.Tag.ToString();

            MainFrm.mainFrm.OpenModule(frm);
        }

        private void tvProcessCard_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        private void tvProcessCard_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        /// <summary>
        /// 删除文件夹节点
        /// </summary>
        private void tsmnuDeleteFolder_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessCard.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该文件夹下包含模版，不能删除文件夹", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }                       

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 删除模版
        /// </summary>
        private void tsmnuDeleteModule_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessCard.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            RemoveTreeNode(selectedNode);
        }

        #endregion

        #region 方法

        delegate void LoadTreeEventHandler();

        /// <summary>
        /// Get card manager list
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public void LoadTreeData()
        {                
            if (tvProcessCard.InvokeRequired)
            {
                LoadTreeEventHandler loadtreeEventHandler = new LoadTreeEventHandler(() =>
                {
                    try
                    {
                        CardManager cardManager =
                            CardManagerBLL.GetCardManagerListById(0).FirstOrDefault<CardManager>();

                        //if (cardManager == null) return;
                        //如果数据库内没有记录，则默认增加一条root记录
                        if (cardManager == null)
                        {
                            cardManager = new CardManager();
                            cardManager.Name = "工艺模版";
                            cardManager.BusinessId = Guid.NewGuid();
                            cardManager.CurrentNode = CardManagerBLL.AddBusiness(cardManager.Name, 0, 0, cardManager.BusinessId);
                        }

                        TreeNode root = new TreeNode();
                        root.Text = cardManager.Name;
                        root.Tag = cardManager.BusinessId;
                        root.ImageKey = "folder_o";
                        root.Name = cardManager.CurrentNode.ToString();
                        root.Expand();

                        ShowChildNode(root);

                        tvProcessCard.Nodes.Add(root);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                });
                tvProcessCard.BeginInvoke(loadtreeEventHandler, new object[] { });
            }
        }

        /// <summary>
        /// get data from dic or database
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public List<CardManager> GetTreeData(string nodeName)
        {
            List<CardManager> cardManagerList = null;
            if (!cardManagerDic.ContainsKey(nodeName))
            {
                cardManagerList =
                    CardManagerBLL.GetCardManagerListById(int.Parse(nodeName));
                cardManagerDic.Add(nodeName, cardManagerList);
            }
            else
            {
                cardManagerList = cardManagerDic[nodeName];
            }
            return cardManagerList;
        }

        /// <summary>
        /// Cache tree node
        /// </summary>
        static Dictionary<string, List<CardManager>> cardManagerDic
                = new Dictionary<string, List<CardManager>>();

        /// <summary>
        /// Display all child nodes of one node
        /// </summary>
        /// <param name="parentNode"></param>
        private void ShowChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            List<CardManager> cardManagerList = GetTreeData(parentNode.Name);

            cardManagerList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.Name;
                node.Tag = o.BusinessId;
                node.Name = o.CurrentNode.ToString();
                switch (o.BType)
                {
                    case BusinessType.Root:
                        node.ImageKey = "folder_o";
                        break;
                    case BusinessType.Card:
                        node.ImageKey = "card";
                        break;
                    case BusinessType.FixTip:
                        node.ImageKey = "detail";
                        break;
                    case BusinessType.Detail:
                        node.ImageKey = "content";
                        break;
                    case BusinessType.Folder:
                        node.ImageKey = "folder";
                        break;
                    default:
                        break;
                }

                List<CardManager> nodeCardManagerList = GetTreeData(node.Name); ;

                if (nodeCardManagerList.Count > 0)
                {
                    node.Nodes.Add(new TreeNode());
                }

                parentNode.Nodes.Add(node);
            });
        }

        /// <summary>
        /// New add module or folder common method
        /// </summary>
        public void AddModuleOrFolder(BusinessType type, ProcessCardModule cardModule)
        {
            TreeNode node = tvProcessCard.SelectedNode;

            ///如果整个Treeview都没有选中的话
            if (node == null)
            {
                node = tvProcessCard.Nodes[0];
            }
             
            /// 避免直接新建时选中卡片或其他的节点
            while (node.ImageKey != "folder" && node.ImageKey != "folder_o")
            {
                node = node.Parent;
            }

            Guid businessId = Guid.NewGuid();

            CardManager businessModule = new CardManager();
            businessModule.BType = type;
            businessModule.ParentNode = int.Parse(node.Name);

            if(cardModule != null)
                businessId = cardModule.Id;
            businessModule.BusinessId = businessId;

            ///当前节点修改缓存且显示在界面上
            string newNodeName = string.Empty;
            AddNodeFrm nodeFrm = null;
            int currentNode = 0;
                        
            if (type != BusinessType.Card)
            {
                nodeFrm = new AddNodeFrm(businessModule);
                nodeFrm.ShowDialog();

                if (nodeFrm.DialogResult != DialogResult.OK)
                {
                    return;
                }
                newNodeName = nodeFrm.NewNodeName;
                currentNode = nodeFrm.CurrentNode;
            }
            else
            {
                newNodeName = cardModule.Name;
                currentNode = CardManagerBLL.AddBusiness(
                          cardModule.Name,
                          (int)type,
                          businessModule.ParentNode, cardModule.Id);
            }

            businessModule.Name = newNodeName;

            List<CardManager> cardManagerList = null;

            if (!cardManagerDic.ContainsKey(node.Name))
            {
                cardManagerList = new List<CardManager>();
                cardManagerList.Add(businessModule);
                cardManagerDic.Add(node.Name, cardManagerList);
            }
            else
            {
                cardManagerList = cardManagerDic[node.Name];

                cardManagerList.Add(new CardManager()
                {
                    BType = type,
                    CurrentNode = currentNode,
                    BusinessId = businessId,
                    Name = newNodeName
                });
                cardManagerDic[node.Name] = cardManagerList;
            }

            TreeNode newNode = new TreeNode();
            newNode.Text = newNodeName;
            newNode.Name = currentNode.ToString();
            newNode.Tag = businessId;
            switch (type)
            {
                case BusinessType.Root:
                    newNode.ImageKey = "folder_o";
                    break;
                case BusinessType.Card:
                    newNode.ImageKey = "card";
                    break;
                case BusinessType.FixTip:
                    newNode.ImageKey = "detail";
                    break;
                case BusinessType.Detail:
                    newNode.ImageKey = "content";
                    break;
                case BusinessType.Folder:
                    newNode.ImageKey = "folder";
                    break;
                default:
                    break;
            }

            node.Nodes.Add(newNode);

            ///新增加的节点也要加入缓存
            cardManagerDic.Add(newNode.Name,
                new List<CardManager>());
        }

        /// <summary>
        /// 方法说明：新建卡片时增加TreeView节点
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <param name="card"></param>
        public void AddCardNode(ProcessCard card)
        {
            TreeNode node = tvProcessCard.SelectedNode;

            ///如果整个Treeview都没有选中的话
            if (node == null)
            {
                node = tvProcessCard.Nodes[0];
            }

            /// 避免直接新建时选中卡片或其他的节点
            while (node.ImageKey != "card")
            {
                node = node.Parent;
            }

            CardManager businessModule = new CardManager();
            businessModule.BType = BusinessType.Card;
            businessModule.ParentNode = int.Parse(node.Name);

            ///当前节点修改缓存且显示在界面上
            string newNodeName = string.Empty;
            int currentNode = 0;

            string businessId = Guid.NewGuid().ToString();           
            businessModule.Name = newNodeName;
            
            TreeNode newNode = new TreeNode();
            newNode.Text = newNodeName;
            newNode.Name = currentNode.ToString();
            newNode.Tag = businessId;
           
                    newNode.ImageKey = "content";


            node.Nodes.Add(newNode);

            ///新增加的节点也要加入缓存
            cardManagerDic.Add(newNode.Name,
                new List<CardManager>());
        }


        /// <summary>
        /// 方法说明：根据BusinessId删除模版或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-22
        /// </summary>
        /// <param name="Id">业务ID</param>
        private void RemoveTreeNode(TreeNode currentNode)
        {
            //TreeNode lastNode = currentNode.PrevNode;
            //if (lastNode == null)
            //{
            //    lastNode = currentNode.Parent;

            //    if (lastNode == null)
            //    {
            //        return;
            //    }
            //    lastNode.Expand();

            //}
            

            if (currentNode == null) return;
                
            Guid businessId = new Guid(currentNode.Tag.ToString());
            bool result = CardManagerBLL.DeleteBusiness(businessId);
            if (result)
            {
                //Remove oneself Cache
                cardManagerDic.Remove(currentNode.Name.ToString());

                List<CardManager> cardList = cardManagerDic[currentNode.Parent.Name];
                var afterCardList = cardList.Where<CardManager>(
                                              x => x.CurrentNode.ToString() != currentNode.Name)
                                            .ToList<CardManager>();

                cardManagerDic[currentNode.Parent.Name] = afterCardList;

                if (!string.IsNullOrEmpty(currentNode.Parent.ImageKey))
                {
                    tvProcessCard.SelectedNode = currentNode.Parent;
                    tvProcessCard.SelectedImageKey = currentNode.Parent.ImageKey;
                }
                else
                {
                    tvProcessCard.SelectedNode = tvProcessCard.Nodes[0];
                    ///  如果选中的节点为空,默认为卡片
                    tvProcessCard.SelectedImageKey = tvProcessCard.Nodes[0].ImageKey;
                }

                //Remove treeview
                tvProcessCard.Nodes.Remove(currentNode);
                tvProcessCard.SelectedNode.Expand();
            }
        }

        #endregion
        
    }
}
        