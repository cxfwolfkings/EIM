using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.UI.Resource;
/*******************************
 * Created By franco
 * Description: New Process Procedure
 *******************************/

namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class NewProcessProcedureFrm : BaseSkinForm
    {
        #region 属性声明

        /// <summary>
        /// 工艺文件夹
        /// </summary>
        public string ProcessFolderId { get; set; }

        #endregion

        #region 窗体控件事件

        /// <summary>
        /// Add process card to process planning tree
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (tvProcessCard.SelectedNode == null || tvProcessCard.SelectedNode.Parent == null)
            {
                MessageBox.Show("请选择工艺文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int count = cklProcessCardModuleNames.Items.Count;

            ///这节代码是为了--方便后面扩展
            List<object> objList = new List<object>();
            for (int i = 0; i < count; i++)
            {
                if (cklProcessCardModuleNames.GetItemChecked(i))
                {
                    if (!objList.Contains(cklProcessCardModuleNames.Items[i]))
                    {
                        objList.Add(cklProcessCardModuleNames.Items[i]);
                    }
                }
            }

            if (string.IsNullOrEmpty(txtProcessPlanningName.Text.Trim()))
            {
                MessageBox.Show(GlobalResource.HAVENOTFILLPROCESSPLANNINGNAME);
                txtProcessPlanningName.Focus();
                return;
            }

            if (objList.Count <= 0)
            {
                MessageBox.Show(GlobalResource.HAVENOTSELECTEDCARDMODULE);
                return;
            }
            else
            {

                ProcessPlanning processPlanning = new ProcessPlanning();
                processPlanning.ProcessPlanningId = Guid.NewGuid();
                processPlanning.Name = txtProcessPlanningName.Text.Trim();

                /// 保存工艺规程和卡片
                SaveProcessPlanning(ProcessCardModules, processPlanning);

                ProcessPlanningTreeFrm processProcedureTree = ProcessPlanningTreeFrm.GetInstance();
                processProcedureTree.ShowProcessPlanningTreeFromNew(ProcessCard, processPlanning, tvProcessCard.SelectedNode.Tag.ToString());


                if (processProcedureTree != null && !processProcedureTree.IsDisposed)
                {
                    processProcedureTree.Show(LefttDockPanel, DockState.DockLeft);
                }

                ///关闭当前窗口
                Close();
            }
        }

        /// <summary>
        /// 保存工艺规程和卡片
        /// </summary>
        /// <param name="?"></param>
        /// <param name="processPlanning"></param>
        public void SaveProcessPlanning(
            List<ProcessCardModule> processCardModules,
            ProcessPlanning processPlanning)
        {

            ProcessCardModuleBLL processCardModuleBLL = new ProcessCardModuleBLL();
            try
            {
                /// 保存工艺规程
                ProcessPlanningBLL.AddProcesPlanning(processPlanning);

                //保存文件夹
                ProcessVersion version = new ProcessVersion();
                version.FolderId = tvProcessCard.SelectedNode.Tag.ToString();
                version.BaseId = processPlanning.ProcessPlanningId.ToString();
                version.Name = processPlanning.Name;
                version.CategoryId = "A9FE1F2B-730A-4DA7-8323-557C664B9734";
                Kingdee.CAPP.BLL.ProcessCardBLL.InsertProcessVersion(version, null);
                
                /// 保存工艺卡片
                ProcessCardBLL processCardBLL = new ProcessCardBLL();
                ProcessCard = new List<Model.ProcessCard>();

                int i = 1;
                foreach (var pcm in processCardModules)
                {
                    ProcessCard processCard = new ProcessCard();
                    //processCard.ID = Guid.NewGuid();
                    processCard.Name = string.Format(processPlanning.Name + "-{0}", i); //Guid.NewGuid().ToString();
                    processCard.CardModuleId = pcm.Id;
                    processCard.Card = processCardModuleBLL.GetCardModule(pcm.Id);
                    processCard.CreateTime = DateTime.Now;
                    processCard.IsCheckOut = false;
                    processCard.IsDelete = 0;
                    processCard.UpdateTime = DateTime.Now;                    

                    Guid id = processCardBLL.InsertProcessCard(processCard);
                    processCard.ID = id;
                    ProcessCard.Add(processCard);

                    ///保存工艺规程和卡片映射
                    PlanningCardRelationBLL.AddProcesPlanningData(
                        new PlanningCardRelation()
                        {
                            ProcessPlanningId = processPlanning.ProcessPlanningId,
                            PlanningCardRelationId = Guid.NewGuid(),
                            ProcessCardId = id,
                            CardSort = 0
                        });
                    i++;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtProcessPlanningName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }        

        private void btmChoose_Click(object sender, EventArgs e)
        {
            SelectProcessPlanningModuleFrm selectProcessPlanningModuleFrm
                    = new SelectProcessPlanningModuleFrm(this);

            selectProcessPlanningModuleFrm.Show();
        }

        private void tvProcessCard_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessCard.GetNodeAt(p);
            if (selectedNode == null) return;
            tvProcessCard.SelectedNode = selectedNode;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
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

        private void tvProcessCard_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.Node.Bounds);
            if (e.State == TreeNodeStates.Selected)//选中的失去焦点的节点
            {
                Brush brush = new SolidBrush(Color.FromArgb(51, 153, 255));
                e.Graphics.FillRectangle(brush, new Rectangle(e.Node.Bounds.Left - 1, e.Node.Bounds.Top, e.Node.Bounds.Width + 4, e.Node.Bounds.Height));
                e.Graphics.DrawString(e.Node.Text, tvProcessCard.Font, Brushes.White, e.Bounds);//白字           
            }
            else
            {
                e.DrawDefault = true;
            }
        }

        #endregion
        
        #region 方法

        public DockPanel LefttDockPanel
        {
            get;
            set;
        }
        public NewProcessProcedureFrm(DockPanel dockPaneMain)
        {
            InitializeComponent();

            LefttDockPanel = dockPaneMain;
            ProcessCardModules = new List<ProcessCardModule>();
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
        /// 卡片列表
        /// </summary>
        private List<ProcessCard> ProcessCard
        {
            get;
            set;
        }

        private void lkSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectProcessPlanningModuleFrm selectProcessPlanningModuleFrm
                    = new SelectProcessPlanningModuleFrm(this);

            selectProcessPlanningModuleFrm.Show();
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProcessProcedureFrm_Load(object sender, EventArgs e)
        {
            //cklProcessCardModuleNames.Enabled = false;
            LoadCardFileData();

            if (tvProcessCard.Nodes.Count > 0 && tvProcessCard.Nodes[0].Nodes.Count > 0)
            {
                tvProcessCard.SelectedNode = tvProcessCard.Nodes[0].Nodes[0];
            }
        }

        /// <summary>
        /// 从SelectProcessPlanningModuleFrm 设置过来的
        /// </summary>
        /// <param name="processCardModules"></param>
        public void SetCklProcessCardModuleName(List<ProcessCardModule> processCardModules)
        {
            ProcessCardModules = processCardModules;
            cklProcessCardModuleNames.Items.Clear();
            foreach (var pcm in ProcessCardModules)
            {
                if (!cklProcessCardModuleNames.Items.Contains(pcm.Name))
                {
                    cklProcessCardModuleNames.Items.Add(pcm.Name, true);
                }
            }

        }

        private void LoadCardFileData()
        {
            try
            {
                TreeNode root = new TreeNode();
                root.Text = "工艺文件夹";
                root.Tag = " ";
                root.ImageKey = "folder_o";
                root.Expand();

                ShowChildNode(root);

                tvProcessCard.Nodes.Add(root);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

                List<ProcessFolder> childFolderList = ProcessCardBLL.GetProcessFolderList(node.Tag.ToString());
                if (childFolderList != null && childFolderList.Count > 0)
                {
                    ShowChildNode(node);
                }

                parentNode.Nodes.Add(node);
            });
        }

        #endregion

    }
}
