using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;


namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class ProcessPlanningModuleFrm : BaseForm
    {
        private Point p;

        public ProcessPlanningModuleFrm()
        {
            InitializeComponent();
        }

        #region 窗体控件事件

        /// <summary>
        /// tcProcessPlanningModuleManager width and height
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessPlanningModule_Resize(object sender, EventArgs e)
        {

            tcProcessPlanningModuleManager.Height = tvProcessPlanningModule.Height = Height;
            tcProcessPlanningModuleManager.Width = tvProcessPlanningModule.Width = Width;
        }

        private void ProcessPlanningModule_Load(object sender, EventArgs e)
        {
            //bwLoadTree.RunWorkerAsync();
            LoadTreeData();

            tvProcessPlanningModule.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();
            AddNodeCms.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
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
            AddModuleOrFolder(BusinessType.Card);
        }
        /// <summary>
        /// new add folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModuleOrFolder(BusinessType.Folder);
        }
        /// <summary>
        /// add process planning module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPlanningModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddModuleOrFolder(BusinessType.Planning);
        }

        /// <summary>
        /// selected node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvProcessPlanningModule_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessPlanningModule.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            ShowChildNode(selectedNode);

            /// under folder can new process planning module
            if (selectedNode.SelectedImageKey == "folder" || selectedNode.SelectedImageKey == "folder_o")
            {
                ///new process card module menu
                AddFolderToolStripMenuItem.Visible = true;
                AddPlanningModuleToolStripMenuItem.Visible = true;
                AddModuleToolStripMenuItem.Visible = false;
                tsmnuDeletePlanningModule.Visible = false;
                tsmnuDeleteCardModule.Visible = false;
                tsmnuDeleteFolder.Visible = true;
                selectedNode.ContextMenuStrip = AddNodeCms;
            }
            ///under process planning can new process card module
            else if (selectedNode.SelectedImageKey == "planning")
            {
                ///new folder menu
                AddFolderToolStripMenuItem.Visible = false;
                ///new process planning module menu
                AddPlanningModuleToolStripMenuItem.Visible = false;
                AddModuleToolStripMenuItem.Visible = true;
                tsmnuDeletePlanningModule.Visible = true;
                tsmnuDeleteCardModule.Visible = false;
                tsmnuDeleteFolder.Visible = false;

                selectedNode.ContextMenuStrip = AddNodeCms;
            }
            else if(selectedNode.SelectedImageKey == "card")
            {
                AddFolderToolStripMenuItem.Visible = false;
                AddPlanningModuleToolStripMenuItem.Visible = false;
                AddModuleToolStripMenuItem.Visible = false;
                tsmnuDeletePlanningModule.Visible = false;
                tsmnuDeleteFolder.Visible = false;
                tsmnuDeleteCardModule.Visible = true;

                selectedNode.ContextMenuStrip = AddNodeCms;
            }

        }

        /// <summary>
        /// 事件说明：双击打开对应节点的工艺规程模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-21
        /// </summary>
        private void tvProcessPlanningModule_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                if (e.Node.ImageKey != "planning")
                {
                    return;
                }

                WeifenLuo.WinFormsUI.Docking.DockContent content = MainFrm.mainFrm.CheckContentIsOpened(e.Node.Tag.ToString().Substring(0, e.Node.Tag.ToString().IndexOf("@")));
                if (content != null)
                {
                    MainFrm.mainFrm.OpenModule(content);
                    return;
                }

                List<ProcessCardModule> listProcessCardModule =
                    ProcessPlanningModuleBLL.GetProcesCardModuleList(int.Parse(e.Node.Tag.ToString().Substring(e.Node.Tag.ToString().IndexOf("@") + 1)));

                if (listProcessCardModule.Count > 0)
                {
                    ProcessPlanningModuleDetailFrm form = new ProcessPlanningModuleDetailFrm();
                    form.Name = e.Node.Tag.ToString().Substring(0, e.Node.Tag.ToString().IndexOf("@"));
                    form.FormText = string.Format("{0}-{1}", e.Node.Text, e.Node.Tag.ToString().Substring(e.Node.Tag.ToString().IndexOf("@")));
                    form.ProcessPlanningModules = listProcessCardModule;
                    MainFrm.mainFrm.OpenPlanningModule(form);
                }
            }
        }

        private void tvProcessPlanningModule_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        private void tvProcessPlanningModule_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        /// <summary>
        /// 删除模版
        /// </summary>
        private void tsmnuDeleteCardModule_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessPlanningModule.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        private void tsmnuDeleteFolder_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessPlanningModule.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该文件夹下包含规程模版，不能删除文件夹", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            RemoveTreeNode(selectedNode);
        }
                
        /// <summary>
        /// 删除规程模版
        /// </summary>
        private void tsmnuDeletePlanningModule_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessPlanningModule.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该规程模版下包含卡片模版，不能删除", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

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
            //if (tvProcessPlanningModule.InvokeRequired)
            //{
            //    LoadTreeEventHandler loadtreeEventHandler = new LoadTreeEventHandler(() =>
            //    {
            try
            {
                List<Model.ProcessPlanningModule> processPlanningModuleList =
                    ProcessPlanningModuleBLL.GetProcesPlanningModuleList(0);

                //if (processPlanningModuleList.Count <= 0) return;
                //如果数据库内没有工艺规程模版记录，则默认增加一条root记录
                if (processPlanningModuleList.Count <= 0)
                {
                    processPlanningModuleList = new List<ProcessPlanningModule>();
                    ProcessPlanningModule planningModule = new ProcessPlanningModule();
                    planningModule.BusinessId = Guid.NewGuid();
                    planningModule.Name = "工艺规程模板";
                    planningModule.BType = 0;
                    planningModule.ParentNode = 0;
                    planningModule.Sort = 1;

                    int currentNode = ProcessPlanningModuleBLL.AddProcessPlanningModule(planningModule);

                    planningModule.CurrentNode = currentNode;
                    processPlanningModuleList.Add(planningModule);
                }

                Model.ProcessPlanningModule processPlanningModule = processPlanningModuleList[0];

                TreeNode root = new TreeNode();
                root.Text = processPlanningModule.Name;
                root.Tag = processPlanningModule.BusinessId + "@" + processPlanningModule.CurrentNode.ToString();
                root.ImageKey = "folder";
                root.Name = processPlanningModule.Sort.ToString(); //processPlanningModule.CurrentNode.ToString();
                root.Expand();

                ShowChildNode(root);

                tvProcessPlanningModule.Nodes.Add(root);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //});
            //tvProcessPlanningModule.BeginInvoke(loadtreeEventHandler, new object[] { });
            //}
        }

        /// <summary>
        /// get data from dic or database
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public List<Model.ProcessPlanningModule> GetTreeData(string nodeName)
        {
            List<Model.ProcessPlanningModule> processPlanningModuleList = processPlanningModuleList =
                    ProcessPlanningModuleBLL.GetProcesPlanningModuleList(int.Parse(nodeName));

            if (!processPlanningModuleDic.ContainsKey(nodeName))
            {
                processPlanningModuleDic.Add(nodeName, processPlanningModuleList);
            }
            else
            {
                processPlanningModuleDic[nodeName] = processPlanningModuleList;
            }
            //else
            //{
            //    processPlanningModuleList = processPlanningModuleDic[nodeName];

            //    var items = from planningModule in processPlanningModuleList
            //                orderby planningModule.Sort ascending
            //                select planningModule;

            //    processPlanningModuleList = new List<ProcessPlanningModule>();
            //    foreach (var item in items)
            //    {
            //        processPlanningModuleList.Add(item);
            //    }
            //}            

            return processPlanningModuleList;
        }

        /// <summary>
        /// Cache tree node
        /// </summary>
        static Dictionary<string, List<Model.ProcessPlanningModule>> processPlanningModuleDic
                = new Dictionary<string, List<Model.ProcessPlanningModule>>();

        /// <summary>
        /// Display all child nodes of one node
        /// </summary>
        /// <param name="parentNode"></param>
        private void ShowChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            int splitIndex = parentNode.Tag.ToString().IndexOf("@");
            string nodeName = string.Empty;

            if (splitIndex > 0)
            {
                nodeName = parentNode.Tag.ToString().Substring(splitIndex + 1);
            }

            List<Model.ProcessPlanningModule> processPlanningModuleList
                = GetTreeData(nodeName); //GetTreeData(parentNode.Name);

            processPlanningModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.Name;
                node.Tag = o.BusinessId + "@" + o.CurrentNode.ToString();                
                //node.Name = o.CurrentNode.ToString();
                node.Name = o.Sort.ToString();
                
                switch (o.BType)
                {
                    case BusinessType.Root:
                        node.ImageKey = "folder";
                        break;
                    case BusinessType.Card:
                        node.ImageKey = "card";
                        break;
                    case BusinessType.Planning:
                        node.ImageKey = "planning";
                        break;
                    case BusinessType.Folder:
                        node.ImageKey = "folder";
                        break;
                    default:
                        break;
                }

                List<Model.ProcessPlanningModule> nodeProcessPlanningModuleList
                    = GetTreeData(o.CurrentNode.ToString()); //GetTreeData(node.Name);

                if (nodeProcessPlanningModuleList.Count > 0)
                {
                    node.Nodes.Add(new TreeNode());
                }

                parentNode.Nodes.Add(node);
            });
        }

        /// <summary>
        /// New module or folder common method
        /// </summary>
        void AddModuleOrFolder(BusinessType type)
        {
            TreeNode node = tvProcessPlanningModule.SelectedNode;
            if (node == null) return;

            ProcessPlanningModule businessModule = new ProcessPlanningModule();
            businessModule.BType = type;
            businessModule.ParentNode = int.Parse(node.Tag.ToString().Substring(node.Tag.ToString().IndexOf("@") + 1));

            Guid businessId = Guid.NewGuid();
            businessModule.BusinessId = businessId;

            ///批量名称
            Dictionary<string, ProcessPlanningModule> dic = new Dictionary<string, ProcessPlanningModule>();
            DialogResult result;

            if (type == BusinessType.Folder)
            {
                ProcessPlanningModuleFolderFrm processPlanningModuleFolderFrm
                   = new ProcessPlanningModuleFolderFrm(businessModule);

                result = processPlanningModuleFolderFrm.ShowDialog();


                if (result == DialogResult.OK)
                {
                    ///当前节点修改缓存且显示在界面上
                    dic = processPlanningModuleFolderFrm.NewNodeNameDic;
                }
            }
            else if (type == BusinessType.Card)
            {
                AddProcessCardModuleFrm processCardModuleFrm
                    = new AddProcessCardModuleFrm(businessModule);



                result = processCardModuleFrm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ///当前节点修改缓存且显示在界面上
                    dic = processCardModuleFrm.ProcessPlanningModuleDic;
                }

            }
            else
            {
                AddProcessPlanningModuleFrm addProcessPlanningModuleFrm
                    = new AddProcessPlanningModuleFrm(businessModule);


                result = addProcessPlanningModuleFrm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ///当前节点修改缓存且显示在界面上
                    dic = addProcessPlanningModuleFrm.NewNodeNameDic;
                }
            }

            List<ProcessPlanningModule> processPlanningModuleList = null;
            if (!processPlanningModuleDic.ContainsKey(node.Tag.ToString().Substring(node.Tag.ToString().IndexOf("@") + 1)))
            {
                processPlanningModuleList = new List<ProcessPlanningModule>();
                processPlanningModuleList.Add(businessModule);
                processPlanningModuleDic.Add(node.Name, processPlanningModuleList);
            }
            else
            {
                /// 更新字典表内的值
                processPlanningModuleList = processPlanningModuleDic[node.Tag.ToString().Substring(node.Tag.ToString().IndexOf("@") + 1)];


                foreach (var kv in dic)
                {
                    processPlanningModuleList.Add(new ProcessPlanningModule()
                    {
                        BType = type,
                        BusinessId = businessId,
                        CurrentNode = kv.Value.CurrentNode,
                        Name = kv.Key
                    });


                    TreeNode newNode = new TreeNode();
                    newNode.Text = kv.Key;
                    newNode.Tag = kv.Value.BusinessId + "@" + kv.Value.CurrentNode.ToString();
                    newNode.Name = kv.Value.Sort.ToString(); //kv.Value.CurrentNode.ToString();
                    switch (type)
                    {
                        case BusinessType.Root:
                            newNode.ImageKey = "folder";
                            break;
                        case BusinessType.Card:
                            newNode.ImageKey = "card";
                            break;
                        case BusinessType.Planning:
                            newNode.ImageKey = "planning";
                            break;
                        case BusinessType.Folder:
                            newNode.ImageKey = "folder";
                            break;
                        default:
                            break;
                    }

                    node.Nodes.Add(newNode);
                    /// set current node selected image 
                    newNode.SelectedImageKey = newNode.ImageKey;
                    /// set current node is selected
                    tvProcessPlanningModule.SelectedNode = newNode;

                    node.Expand();

                    ///每个新增加的节点也要加入缓存
                    processPlanningModuleDic.Add(newNode.Tag.ToString().Substring(newNode.Tag.ToString().IndexOf("@") + 1),
                        new List<Model.ProcessPlanningModule>());
                }

                ///更新当前节点含有的子节点
                processPlanningModuleDic[node.Tag.ToString().Substring(node.Tag.ToString().IndexOf("@") + 1)] = processPlanningModuleList;
            }
        }

        /// <summary>
        /// 方法说明：根据BusinessId删除模版或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-24
        /// </summary>
        /// <param name="Id">业务ID</param>
        private void RemoveTreeNode(TreeNode currentNode)
        {
            if (currentNode == null) return;

            int splitIndex = currentNode.Tag.ToString().IndexOf("@");

            Guid businessId = new Guid(currentNode.Tag.ToString().Substring(0, splitIndex));
            bool result = ProcessPlanningModuleBLL.DeleteBusiness(businessId);
            if (result)
            {
                //Remove oneself Cache
                processPlanningModuleDic.Remove(currentNode.Tag.ToString().Substring(splitIndex + 1));

                List<ProcessPlanningModule> cardList = processPlanningModuleDic[currentNode.Parent.Tag.ToString().Substring(currentNode.Parent.Tag.ToString().IndexOf("@") + 1)];
                var afterCardList = cardList.Where<ProcessPlanningModule>(
                                              x => x.CurrentNode.ToString() != currentNode.Tag.ToString().Substring(splitIndex + 1))
                                            .ToList<ProcessPlanningModule>();

                processPlanningModuleDic[currentNode.Parent.Tag.ToString().Substring(currentNode.Parent.Tag.ToString().IndexOf("@") + 1)] = afterCardList;
                              

                if (!string.IsNullOrEmpty(currentNode.Parent.ImageKey))
                {
                    tvProcessPlanningModule.SelectedNode = currentNode.Parent;
                    tvProcessPlanningModule.SelectedImageKey = currentNode.Parent.ImageKey;
                }
                else
                {
                    tvProcessPlanningModule.SelectedNode = tvProcessPlanningModule.Nodes[0];
                    ///  如果选中的节点为空,默认为卡片
                    tvProcessPlanningModule.SelectedImageKey = tvProcessPlanningModule.Nodes[0].ImageKey;
                }

                //Remove treeview
                tvProcessPlanningModule.Nodes.Remove(currentNode);
                tvProcessPlanningModule.SelectedNode.Expand();
            }
        }

        #endregion

        private void tvProcessPlanningModule_DragDrop(object sender, DragEventArgs e)
        {
            //获得拖放中的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            TreeNode parentNode = moveNode.Parent;

            ///如果该节点不是卡片模版时，则不允许进行拖动
            if (moveNode.SelectedImageKey != "card")
            {
                return;
            }

            //根据鼠标坐标确定要移动到的目标节点
            Point pt;
            TreeNode targeNode;
            pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            targeNode = this.tvProcessPlanningModule.GetNodeAt(pt);
            TreeNode targeParent = targeNode.Parent;

            //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端
            TreeNode NewMoveNode = (TreeNode)moveNode.Clone();

            int moveIndex = moveNode.Tag.ToString().IndexOf("@");
            string moveId = string.Empty;
            if (moveIndex > 0)
            {
                moveId = moveNode.Tag.ToString().Substring(0, moveIndex);
            }

            int targeIndex = targeNode.Tag.ToString().IndexOf("@");
            string tagId = string.Empty;
            if (targeIndex > 0)
            {
                tagId = targeNode.Tag.ToString().Substring(0, targeIndex);
            }

            int parentIndex = parentNode.Tag.ToString().IndexOf("@");
            string parentName = string.Empty;
            if (parentIndex > 0)
            {
                parentName = parentNode.Tag.ToString().Substring(parentIndex + 1);
            }

            if (targeNode.Parent == moveNode.Parent)
            {
                tvProcessPlanningModule.BeginUpdate();
                ProcessPlanningModuleBLL.ChangeTwoCardSortOrder(moveId, moveNode.Name, tagId, targeNode.Name, parentName);
                parentNode.Nodes.Clear();
                ShowCardModuleByParentCode(parentName, parentNode);
                tvProcessPlanningModule.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();
                tvProcessPlanningModule.EndUpdate();

                tvProcessPlanningModule.Focus();
                tvProcessPlanningModule.SelectedNode = targeParent;
                if (tvProcessPlanningModule.SelectedNode != null)
                    tvProcessPlanningModule.SelectedNode.EnsureVisible();

                return;
            }

            object busiessId = moveId;
            string prevParentNode = parentName;
            TreeNodeCollection targeNodes = null;
            if (targeNode.ImageKey == "card")
            {
                targeNodes = targeNode.Parent.Nodes;
            }
            else if (targeNode.ImageKey == "planning")
            {
                targeNodes = targeNode.Nodes;
            }

            foreach (TreeNode nod in targeNodes)
            {
                if (nod.Tag.ToString().StartsWith(moveId))
                {
                    MessageBox.Show("目标节点下已包含相同的模版，无法移动", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            if (targeNode.Nodes.Count == 0)
            {
                if (targeNode.ImageKey == "card")
                {
                    targeNode.Parent.Nodes.Insert(targeNode.Index, NewMoveNode);
                }
                else if (targeNode.ImageKey == "planning")
                {
                    targeNode.Nodes.Insert(targeNode.Index, NewMoveNode);
                    targeNode.Expand();
                }
            }
            else
            {
                targeNode.Nodes.Insert(targeNode.Nodes.Count, NewMoveNode);
            }

            //更新数据库内节点的从属关系
            UpdatePlanningModule(targeNode.Parent.Tag.ToString().Substring(targeNode.Parent.Tag.ToString().IndexOf("@") + 1), prevParentNode, busiessId);

            tvProcessPlanningModule.SelectedNode = NewMoveNode;
            //展开目标节点,便于显示拖放效果
            targeNode.Expand();
            //移除拖放的节点
            moveNode.Remove();

            tvProcessPlanningModule.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();

            tvProcessPlanningModule.Focus();
            tvProcessPlanningModule.SelectedNode = targeParent;
            if (tvProcessPlanningModule.SelectedNode != null)
                tvProcessPlanningModule.SelectedNode.EnsureVisible();
        }

        private void tvProcessPlanningModule_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tvProcessPlanningModule_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
                if (tvProcessPlanningModule.SelectedNode != null)
                    tvProcessPlanningModule.SelectedNode.EnsureVisible();
            }
        }

        /// <summary>
        /// 根据父节点，显示卡片模板
        /// </summary>
        /// <param name="parentCode"></param>
        public void ShowCardModuleByParentCode(string parentCode, TreeNode node)
        {
            List<ProcessPlanningModule> planningCardModules
                = GetTreeData(parentCode);

            foreach (var pc in planningCardModules)
            {
                TreeNode cardModuleNode = new TreeNode();
                cardModuleNode.Text = pc.Name;
                cardModuleNode.Tag = pc.BusinessId + "@" + pc.CurrentNode.ToString();
                cardModuleNode.ImageKey = "card";
                cardModuleNode.Name = pc.Sort.ToString(); //pc.CurrentNode.ToString();
                cardModuleNode.Collapse();

                node.Nodes.Add(cardModuleNode);
            }
        }

        private void UpdatePlanningModule(string parentNode, string prevParentNode, object businessId)
        {
            ProcessPlanningModuleBLL.UpdatePlanningModuleData(parentNode, prevParentNode, businessId);
        }
    }
}
