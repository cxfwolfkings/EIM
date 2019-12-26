using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.UI.Resource;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.UI.ProcessDataManagement;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
/*******************************
 * Created By franco
 * Description: Process Procedure
 *******************************/

namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class ProcessPlanningTreeFrm : BaseForm
    {
        public ProcessPlanningTreeFrm()
        {
            InitializeComponent();
        }

        private static ProcessPlanningTreeFrm instance;
        private static object _lock = new object();
        public static ProcessPlanningTreeFrm GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                lock (_lock)
                {
                    if (instance == null || instance.IsDisposed)
                    {
                        instance = new ProcessPlanningTreeFrm();
                    }
                }
            }
            return instance;
        }


        /// <summary>
        /// 设置tvProcessProcedure 和 tcProcessProcedure,高和宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessProcedureTreeFrm_Resize(object sender, EventArgs e)
        {
            tcProcessProcedure.Width = tvProcessProcedure.Width = Width;
            tcProcessProcedure.Height = tvProcessProcedure.Height = Height;
        }

        /// <summary>
        /// 卡片模板列表
        /// </summary>
        public List<ProcessCardModule> ProcessCardModules
        {
            get;
            set;
        }

        /// <summary>
        /// init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessProcedureTreeFrm_Load(object sender, EventArgs e)
        {
            ShowProcessPlanningTreeFromView();

            //tvProcessProcedure.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();

            if (tvProcessProcedure.Nodes.Count > 0)
            {
                tvProcessProcedure.SelectedNode = tvProcessProcedure.Nodes[0];
                if (tvProcessProcedure.SelectedNode != null)
                    tvProcessProcedure.SelectedNode.EnsureVisible();
            }

            contextMenuStrip1.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender(); 
        }

        /// <summary>
        /// 新增时调用， 显示工艺规程和卡片模版树
        /// </summary>
        /// <param name="processCard"></param>
        public void ShowProcessPlanningTreeFromNew(
            List<ProcessCard> processCard,
            ProcessPlanning ProcessPlanning,
            string folderid)
        {
            TreeNode planningNode = new TreeNode();
            planningNode.Text = ProcessPlanning.Name;
            planningNode.Tag = ProcessPlanning.ProcessPlanningId;
            planningNode.ImageKey = "planning";
            planningNode.Name = ProcessPlanning.Sort.ToString(); //ProcessPlanning.ProcessPlanningId.ToString();
            planningNode.Expand();


            TreeNode[] nodes = tvProcessProcedure.Nodes.Find(folderid, false);

            if (nodes == null || nodes.Length == 0)
                return;

            //tvProcessProcedure.Nodes.Add(planningNode);
            nodes[0].Nodes.Add(planningNode);

            int i = 0;
            processCard.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = string.Format(ProcessPlanning.Name + "-{0}", planningNode.Nodes.Count + 1);
                node.Tag = o.ID;
                node.ImageKey = "card";
                node.Name = o.CardSort.ToString(); //o.ID.ToString();
                planningNode.Nodes.Add(node);

                i++;
            });
        }

        /// <summary>
        /// 查看时调用， 显示工艺规程和卡片模版树
        /// </summary>
        /// <param name="processCardModules"></param>
        public void ShowProcessPlanningTreeFromView()
        {
            TreeNode root = new TreeNode();
            root.Text = "工艺规程";
            root.Tag = " ";
            root.ImageKey = "folder_o";
            root.Expand();

            ShowChildNode(root);

            tvProcessProcedure.Nodes.Add(root);            
        }

        private void ShowChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            List<ProcessFolder> folderList = ProcessCardBLL.GetProcessFolderList(parentNode.Tag.ToString());

            if (folderList == null || folderList.Count == 0) return;

            folderList.ForEach((f) =>
            {
                TreeNode node = new TreeNode();
                node.Text = f.FolderName;
                node.Tag = f.FolderId;
                node.Name = f.ParentFolder;
                node.ImageKey = "folder";

                List<ProcessVersion> versionList = ProcessCardBLL.GetProcessCardByFolderId(f.FolderId, 2);
                if (versionList != null && versionList.Count > 0)
                {
                    versionList.ForEach((v) =>
                    {
                        TreeNode nd = new TreeNode();
                        nd.Text = v.Name;
                        nd.Tag = v.BaseId;
                        nd.Name = v.Name;
                        nd.ImageKey = "planning";

                        ShowProcessCardByProcessPlanningId(new Guid(v.BaseId), nd);

                        node.Nodes.Add(nd);
                    }
                    );
                }

                List<ProcessFolder> childFolderList = ProcessCardBLL.GetProcessFolderList(node.Tag.ToString());
                if (childFolderList != null && childFolderList.Count > 0)
                {
                    ShowChildNode(node);
                }

                parentNode.Nodes.Add(node);
            });
        }
        
        /// <summary>
        /// Show process card module by process planning Id
        /// 根据工艺规程Id，显示卡片
        /// </summary>
        /// <param name="processPlanningId"></param>
        public void ShowProcessCardByProcessPlanningId(Guid processPlanningId, TreeNode node)
        {
            List<ProcessCard> processCardModules
                = PlanningCardRelationBLL.GetProcessCardListByProcessPlanningId(processPlanningId);

            foreach (var pc in processCardModules)
            {
                TreeNode processCardNode = new TreeNode();
                processCardNode.Text = pc.Name;
                processCardNode.Tag = string.Format("{0}@{1}", pc.ID, pc.CardModuleId);
                processCardNode.ImageKey = "card";
                processCardNode.Name = pc.CardSort.ToString();
                processCardNode.Collapse();

                node.Nodes.Add(processCardNode);
            }
        }

        private Point p;
        private void tvProcessProcedure_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessProcedure.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            if (selectedNode.SelectedImageKey == "planning")
            {
                selectedNode.ContextMenuStrip = contextMenuStrip1;
                tsmnuDeleteProcessPlanning.Visible = true;
                tsmnuDeleteProcessCard.Visible = false;
                NewAddProcessCardToolStripMenuItem.Visible = true;
            }
            else if (selectedNode.SelectedImageKey == "card")
            {
                selectedNode.ContextMenuStrip = contextMenuStrip1;
                tsmnuDeleteProcessPlanning.Visible = false;
                tsmnuDeleteProcessCard.Visible = true;
                NewAddProcessCardToolStripMenuItem.Visible = false;
            }
        }

        /// <summary>
        /// 新增工艺卡片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewAddProcessCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectProcessCardModuleFrm selectProcessPlanningModuleFrm
                = new SelectProcessCardModuleFrm(this);

            selectProcessPlanningModuleFrm.ShowDialog();

            //using (CardModuleChooseFrm chooseFrm = new CardModuleChooseFrm())
            //{
            //    if (chooseFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //    {
            //        ProcessCardFrm frm = new ProcessCardFrm();
            //        frm.Name = string.Format("ProcessCardFrm-{0}", Guid.NewGuid().ToString());

            //        if (!string.IsNullOrEmpty(chooseFrm.ProcessCardId))
            //        {
            //            frm.TabText = chooseFrm.ProcessCardName;
            //            MainFrm.mainFrm.OpenModule(frm);
            //            bool result = frm.OpenCard(null, chooseFrm.ProcessCardId, true);
            //            if (!result)
            //            {
            //                MainFrm.mainFrm.CloseModule(frm);
            //            }
            //            return;
            //        }

            //        #region 设置新增卡片Tab的TabText及Name

            //        int tag = 1;
            //        int index = 1;
            //        List<int> listIndex = new List<int>();
            //        foreach (DockContent form in this.MdiChildren)
            //        {
            //            if (form.Name.StartsWith("ProcessCardFrm") &&
            //                form.TabText.StartsWith(chooseFrm.ModuleName))
            //            {
            //                tag = int.Parse(form.TabText.Substring(form.TabText.IndexOf("-") + 1));
            //                listIndex.Add(tag);
            //            }
            //        }
            //        listIndex.Sort();
            //        foreach (var i in listIndex)
            //        {
            //            if (i > 1 && index == 1)
            //            {
            //                index = 1;
            //                break;
            //            }

            //            if (index > 1)
            //            {
            //                if (i - listIndex[index - 2] > 1)
            //                {
            //                    index = listIndex[index - 2] + 1;
            //                    break;
            //                }
            //            }
            //            index++;
            //        }
            //        frm.TabText = string.Format("{0}-{1}", chooseFrm.ModuleName, index);


            //        #endregion

            //        frm.ModuleId = chooseFrm.ModuleId;
            //        frm.ModuleName = chooseFrm.ModuleName;
            //        MainFrm.mainFrm.OpenModule(frm);
            //    }
            //}
        }

        /// <summary>
        /// set current node new add process card 
        /// </summary>
        /// <param name="processCardModuleList"></param>
        public void SetCurrentNodeNewAddProcessCard(List<ProcessCardModule> processCardModuleList)
        {
            TreeNode selectedNode = tvProcessProcedure.GetNodeAt(p);
            Guid processPlanningId = (Guid)selectedNode.Tag;

            ProcessCardModuleBLL processCardModuleBLL = new ProcessCardModuleBLL();
            ProcessCardBLL processCardBLL = new ProcessCardBLL();
            try
            {
                int i = selectedNode.Nodes.Count + 1;
                /// 增加卡片
                foreach (var pcm in processCardModuleList)
                {
                    string name = string.Format(pcm.Name + "-{0}", i);

                    ProcessCard processCard = new ProcessCard();
                    processCard.ID = Guid.NewGuid();
                    processCard.Name = name; //Guid.NewGuid().ToString();
                    processCard.CardModuleId = pcm.Id;
                    processCard.Card = processCardModuleBLL.GetCardModule(pcm.Id);
                    processCard.CreateTime = DateTime.Now;
                    processCard.IsCheckOut = false;
                    processCard.IsDelete = 0;
                    processCard.UpdateTime = DateTime.Now;

                    Guid id = processCardBLL.InsertProcessCard(processCard);

                    ///保存工艺规程和卡片映射
                    PlanningCardRelationBLL.AddProcesPlanningData(
                        new PlanningCardRelation()
                        {
                            ProcessPlanningId = processPlanningId,
                            PlanningCardRelationId = Guid.NewGuid(),
                            ProcessCardId = id,
                            CardSort = 0
                        });

                    TreeNode newNode = new TreeNode();
                    newNode.Tag = string.Format("{0}@{1}", id, pcm.Id);
                    newNode.Name = i.ToString(); // id.ToString();
                    newNode.Text = name;
                    newNode.ImageKey = "card";
                    newNode.Collapse();

                    selectedNode.Nodes.Add(newNode);

                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tvProcessProcedure.Refresh();

            //tvProcessProcedure.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();
        }

        private void tvProcessProcedure_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
                if (tvProcessProcedure.SelectedNode != null)
                    tvProcessProcedure.SelectedNode.EnsureVisible();
            }
        }

        private void tvProcessProcedure_DragDrop(object sender, DragEventArgs e)
        {
            //获得拖放中的节点
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            TreeNode parentNode = moveNode.Parent;

            ///如果该节点时规程，则不允许进行拖动
            if (moveNode.SelectedImageKey == "planning")
            {
                return;
            }

            //根据鼠标坐标确定要移动到的目标节点
            Point pt;
            TreeNode targeNode;
            pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            targeNode = this.tvProcessProcedure.GetNodeAt(pt);
            TreeNode targeParent = targeNode.Parent;

            //如果目标节点无子节点则添加为同级节点,反之添加到下级节点的未端
            TreeNode NewMoveNode = (TreeNode)moveNode.Clone();

            if (targeNode.Parent == moveNode.Parent)
            {
                tvProcessProcedure.BeginUpdate();
                PlanningCardRelationBLL.ChangeTwoCardSortOrder(moveNode.Tag, moveNode.Name, targeNode.Tag, targeNode.Name, parentNode.Tag.ToString());
                parentNode.Nodes.Clear();
                ShowProcessCardByProcessPlanningId(new Guid(parentNode.Tag.ToString()), parentNode);
                //tvProcessProcedure.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();
                tvProcessProcedure.EndUpdate();

                tvProcessProcedure.Focus();
                tvProcessProcedure.SelectedNode = targeParent;
                if (tvProcessProcedure.SelectedNode != null)
                    tvProcessProcedure.SelectedNode.EnsureVisible();

                return;
            }

            object planningid = null;
            object prevPlanningid = moveNode.Parent.Tag;

            if (targeNode.Nodes.Count == 0)
            {
                if (targeNode.ImageKey == "card")
                {
                    targeNode.Parent.Nodes.Insert(targeNode.Index, NewMoveNode);
                    planningid = targeNode.Parent.Tag;
                }
                else if (targeNode.ImageKey == "planning")
                {
                    targeNode.Nodes.Insert(targeNode.Index, NewMoveNode);
                    planningid = targeNode.Tag;
                    targeNode.Expand();
                }
            }
            else
            {
                targeNode.Nodes.Insert(targeNode.Nodes.Count, NewMoveNode);
                planningid = targeNode.Tag;
            }

            int splitIndex = NewMoveNode.Tag.ToString().IndexOf("@");
            string cardid = NewMoveNode.Tag.ToString().Substring(0, splitIndex);

            //更新数据库内节点的从属关系
            //UpdateCardRelation(planningid, prevPlanningid, NewMoveNode.Tag);
            UpdateCardRelation(planningid, prevPlanningid, cardid);

            tvProcessProcedure.SelectedNode = NewMoveNode;
            //展开目标节点,便于显示拖放效果
            targeNode.Expand();
            //移除拖放的节点
            moveNode.Remove();

           // tvProcessProcedure.TreeViewNodeSorter = new Kingdee.CAPP.UI.Common.NodeSorter();

            tvProcessProcedure.Focus();
            tvProcessProcedure.SelectedNode = targeParent;
            if (tvProcessProcedure.SelectedNode != null)
                tvProcessProcedure.SelectedNode.EnsureVisible();
        }

        private void UpdateCardRelation(object planningid, object prevPlanningid, object cardid)
        {
            PlanningCardRelationBLL.UpdateProcessPlanningData(planningid, prevPlanningid, cardid);
        }

        private void tvProcessProcedure_DragEnter(object sender, DragEventArgs e)
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

        /// <summary>
        /// 打开卡片
        /// </summary>
        private void tvProcessProcedure_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            if (node.ImageKey != "planning")
            {
                return;
            }

            WeifenLuo.WinFormsUI.Docking.DockContent content = MainFrm.mainFrm.CheckContentIsOpened(e.Node.Tag.ToString());
            if (content != null)
            {
                MainFrm.mainFrm.OpenModule(content);
                return;
            }

            Guid planningId = new Guid(e.Node.Tag.ToString());
            List<ProcessCard> listProcessCard
                = PlanningCardRelationBLL.GetProcessCardListByProcessPlanningId(planningId);
            
            if (listProcessCard.Count > 0)
            {
                ProcessPlanningDetailFrm form = new ProcessPlanningDetailFrm();
                form.FormText = string.Format("{0}-{1}", e.Node.Text, e.Node.Tag.ToString());
                form.ProcessPlanningCards = listProcessCard;
                MainFrm.mainFrm.OpenPlanningModule(form);
            }
        }

        private void tvProcessProcedure_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        private void tvProcessProcedure_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        /// <summary>
        /// 删除工艺规程下的卡片
        /// </summary>
        private void tsmnuDeleteProcessCard_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessProcedure.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 删除工艺规程
        /// </summary>
        private void tsmnuDeleteProcessPlanning_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessProcedure.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该规程下包含卡片，不能删除", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            RemoveTreeNode(selectedNode);
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

            bool result = false;
            if (currentNode.ImageKey == "planning")
            {
                Guid planningId = new Guid(currentNode.Tag.ToString());
                result = ProcessPlanningBLL.DeletePlanningById(planningId);
                ProcessCardBLL.DeleteProcessVersion(planningId.ToString(), currentNode.Parent.Tag.ToString());
            }
            else
            {
                int splitIndex = currentNode.Tag.ToString().IndexOf("@");
                Guid cardid = new Guid(currentNode.Tag.ToString().Substring(0, splitIndex));
                Guid planningId = new Guid(currentNode.Parent.Tag.ToString());
                result = PlanningCardRelationBLL.DeleteRelationByCardId(cardid, planningId);
            }

            if (result)
            {
                if (currentNode.Parent != null && !string.IsNullOrEmpty(currentNode.Parent.ImageKey))
                {
                    tvProcessProcedure.SelectedNode = currentNode.Parent;
                    tvProcessProcedure.SelectedImageKey = currentNode.Parent.ImageKey;
                }
                else
                {
                    tvProcessProcedure.SelectedNode = tvProcessProcedure.Nodes[0];
                    ///  如果选中的节点为空,默认为卡片
                    tvProcessProcedure.SelectedImageKey = tvProcessProcedure.Nodes[0].ImageKey;
                }

                //Remove treeview
                tvProcessProcedure.Nodes.Remove(currentNode);
                tvProcessProcedure.SelectedNode.Expand();
            }
        }

    }
}
