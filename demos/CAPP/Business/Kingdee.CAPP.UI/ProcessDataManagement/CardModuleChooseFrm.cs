using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 窗体说明：工艺卡片模板选择界面
    /// 作    者：jason.tang
    /// 完成时间：2013-02-05
    /// </summary>
    public partial class CardModuleChooseFrm : BaseSkinForm
    {
        #region 属性和变量声明

        /// <summary>
        /// 卡片模板ID
        /// </summary>
        private string _moduleId;
        public string ModuleId
        {
            get
            {
                return _moduleId;
            }
            set
            {
                _moduleId = value;
            }
        }

        /// <summary>
        /// 卡片模板名称
        /// </summary>
        private string _moduleName;
        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                _moduleName = value;
            }
        }

        /// <summary>
        /// 模版或者卡片
        /// </summary>
        public bool ModuleOrCard { get; set; }

        /// <summary>
        /// 卡片ID
        /// </summary>
        public string ProcessCardId { get; set; }

        /// <summary>
        /// 卡片名称
        /// </summary>
        public string ProcessCardName { get; set; }

        /// <summary>
        /// 工艺文件夹
        /// </summary>
        public string ProcessFolderId { get; set; }

        private List<Model.ProcessCard> listProcessCard;

        #endregion

        #region 构造函数

        public CardModuleChooseFrm()
        {
            InitializeComponent();

            dgvCardModule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        #endregion

        #region 窗体控件事件

        /// <summary>
        /// 确定
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (tvProcessCard.SelectedNode == null || tvProcessCard.SelectedNode.Parent == null)
            {
                MessageBox.Show("请选择工艺文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ProcessFolderId = tvProcessCard.SelectedNode.Tag.ToString();

            if (tcNewCard.SelectedTab == tpCardModule)
            {
                if (dgvCardModule.CurrentRow != null)
                {
                    _moduleId = dgvCardModule.CurrentRow.Cells["id"].Value.ToString();
                    _moduleName = dgvCardModule.CurrentRow.Cells["name"].Value.ToString();

                    ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
                    Kingdee.CAPP.Model.CardsXML cardsmodule = new Kingdee.CAPP.Model.CardsXML();
                    try
                    {
                        cardsmodule = pcmDll.GetCardModule(new Guid(_moduleId));
                    }
                    catch
                    {
                        MessageBox.Show("模版读取失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    Kingdee.CAPP.Model.Card cardmodule = cardsmodule.Cards.FirstOrDefault<Kingdee.CAPP.Model.Card>();
                    if (cardmodule.Rows == null || cardmodule.Rows.Length == 0)
                    {
                        MessageBox.Show("模版为空，请选择另一个模版", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            else if (tcNewCard.SelectedTab == tpTypical)
            {
                if (tvTypicalProcess.SelectedNode != null && tvTypicalProcess.SelectedNode.SelectedImageKey == "card")
                {
                    ProcessCardId = tvTypicalProcess.SelectedNode.Tag.ToString();
                    ProcessCardName = tvTypicalProcess.SelectedNode.Text;
                }
                else
                {
                    MessageBox.Show("请选择典型工艺卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (tcNewCard.SelectedTab == tpPlanning)
            {
                if (tvProcessPlanningModule.SelectedNode != null && tvProcessPlanningModule.SelectedNode.SelectedImageKey == "card")
                {
                    _moduleId = tvProcessPlanningModule.SelectedNode.Tag.ToString();
                    _moduleName = tvProcessPlanningModule.SelectedNode.Text;
                }
                else
                {
                    MessageBox.Show("请选择规程模版", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (tcNewCard.SelectedTab == tpCardData)
            {
                if (dgvCard.CurrentRow != null)
                {
                    ProcessCardId = dgvCard.CurrentRow.Cells["colCardId"].Value.ToString();
                    ProcessCardName = dgvCard.CurrentRow.Cells["colName"].Value.ToString();
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        
        private void CardModuleChooseFrm_Load(object sender, EventArgs e)
        {
            List<Model.ProcessCardModule> listCardModule = ProcessCardModuleBLL.GetDefaultProcessCardList();
            if (listCardModule != null && listCardModule.Count > 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("id");
                dt.Columns.Add("name");
                dt.Columns.Add("typename");
                foreach (Model.ProcessCardModule cardmodule in listCardModule)
                {
                    DataRow dr = dt.NewRow();
                    dr["id"] = cardmodule.Id;
                    dr["name"] = cardmodule.Name;
                    dr["typename"] = cardmodule.TypeName;
                    dt.Rows.Add(dr.ItemArray);
                }
                dgvCardModule.DataSource = dt;
            }

            LoadCardFileData();

            if (tvProcessCard.Nodes.Count > 0 && tvProcessCard.Nodes[0].Nodes.Count > 0)
            {
                tvProcessCard.SelectedNode = tvProcessCard.Nodes[0].Nodes[0];
            }
        }

        private void tcNewCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcNewCard.SelectedTab == tpTypical && tvTypicalProcess.Nodes.Count == 0)
            {
                LoadTypicalTreeData();
            }
            else if (tcNewCard.SelectedTab == tpPlanning && tvProcessPlanningModule.Nodes.Count == 0)
            {
                LoadPlanningTreeData();
            }
            else if (tcNewCard.SelectedTab == tpCardData && dgvCard.Rows.Count == 0)
            {
                LoadCardData();
            }
        }        

        private void tvProcessPlanningModule_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessPlanningModule.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                ShowPlanningChildNode(selectedNode);
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

        private void tvTypicalProcess_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvTypicalProcess.GetNodeAt(p);
            if (selectedNode == null) return;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                ShowTypicalChildNode(selectedNode);
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

        /// <summary>
        /// 事件说明：文本框为了设置高度，设置多行，并限制不能回车
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 加载规程模版卡片
        /// </summary>
        private void LoadPlanningTreeData()
        {
            try
            {
                List<Model.ProcessPlanningModule> processPlanningModuleList =
                    ProcessPlanningModuleBLL.GetProcesPlanningModuleList(0);

                //if (processPlanningModuleList.Count <= 0) return;
                //如果数据库内没有工艺规程模版记录，则默认增加一条root记录
                if (processPlanningModuleList.Count <= 0)
                {
                    processPlanningModuleList = new List<Model.ProcessPlanningModule>();
                    Model.ProcessPlanningModule planningModule = new Model.ProcessPlanningModule();
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
                root.Tag = processPlanningModule.BusinessId;
                root.ImageKey = "folder";
                root.Name = processPlanningModule.CurrentNode.ToString();
                root.Expand();

                ShowPlanningChildNode(root);

                tvProcessPlanningModule.Nodes.Add(root);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 显示规程所有子节点
        /// </summary>
        /// <param name="parentNode"></param>
        private void ShowPlanningChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            List<Model.ProcessPlanningModule> processPlanningModuleList
                = GetPlaningTreeData(parentNode.Name);

            processPlanningModuleList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.Name;
                node.Tag = o.BusinessId;
                node.Name = o.CurrentNode.ToString();
                switch (o.BType)
                {
                    case Model.BusinessType.Root:
                        node.ImageKey = "folder";
                        break;
                    case Model.BusinessType.Card:
                        node.ImageKey = "card";
                        break;
                    case Model.BusinessType.Planning:
                        node.ImageKey = "planning";
                        break;
                    case Model.BusinessType.Folder:
                        node.ImageKey = "folder";
                        break;
                    default:
                        break;
                }

                List<Model.ProcessPlanningModule> nodeProcessPlanningModuleList
                    = GetPlaningTreeData(node.Name);

                if (nodeProcessPlanningModuleList.Count > 0)
                {
                    node.Nodes.Add(new TreeNode());
                }

                parentNode.Nodes.Add(node);
            });
        }

        /// <summary>
        /// 获取规程模版
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private List<Model.ProcessPlanningModule> GetPlaningTreeData(string nodeName)
        {
            List<Model.ProcessPlanningModule> processPlanningModuleList =
                    ProcessPlanningModuleBLL.GetProcesPlanningModuleList(int.Parse(nodeName));

            return processPlanningModuleList;
        }

        /// <summary>
        /// 方法说明：加载典型工艺数据到树
        /// </summary>
        private void LoadTypicalTreeData()
        {
            try
            {
                List<Model.TypicalProcess> typicalProcessList =
                    TypicalProcessBLL.GetTypicalProcesList(0);

                //如果数据库内没有工艺规程模版记录，则默认增加一条root记录
                if (typicalProcessList.Count <= 0)
                {
                    typicalProcessList = new List<Model.TypicalProcess>();
                    Model.TypicalProcess typicalProcess = new Model.TypicalProcess();
                    typicalProcess.BusinessId = Guid.NewGuid();
                    typicalProcess.Name = "典型工艺";
                    typicalProcess.BType = 0;
                    typicalProcess.ParentNode = 0;
                    typicalProcess.Sort = 1;

                    int currentNode = TypicalProcessBLL.AddTypicalProcess(typicalProcess);

                    typicalProcess.CurrentNode = currentNode;
                    typicalProcessList.Add(typicalProcess);
                }

                Model.TypicalProcess typicalProcessModule = typicalProcessList[0];

                TreeNode root = new TreeNode();
                root.Text = typicalProcessModule.Name;
                root.Tag = typicalProcessModule.BusinessId;
                root.ImageKey = "folder";
                root.Name = typicalProcessModule.CurrentNode.ToString();
                root.Expand();

                ShowTypicalChildNode(root);

                tvTypicalProcess.Nodes.Add(root);
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
        /// 方法说明：显示子节点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        private void ShowTypicalChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            List<Model.TypicalProcess> typicalProcessList
                = GetTypicalTreeData(parentNode.Name);

            typicalProcessList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.Name;
                node.Tag = o.BusinessId;
                node.Name = o.CurrentNode.ToString();
                switch (o.BType)
                {
                    case Model.BusinessType.Root:
                        node.ImageKey = "folder";
                        break;
                    case Model.BusinessType.Card:
                        node.ImageKey = "card";
                        break;
                    case Model.BusinessType.Planning:
                        node.ImageKey = "planning";
                        break;
                    case Model.BusinessType.Folder:
                        node.ImageKey = "folder";
                        break;
                    default:
                        break;
                }

                List<Model.TypicalProcess> nodeTypicalProcessList
                    = GetTypicalTreeData(node.Name);

                if (nodeTypicalProcessList.Count > 0)
                {
                    node.Nodes.Add(new TreeNode());
                }

                parentNode.Nodes.Add(node);
            });
        }

        /// <summary>
        /// 方法说明：获取树节点数据
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        private List<Model.TypicalProcess> GetTypicalTreeData(string nodeName)
        {
            List<Model.TypicalProcess> typicalProcessList = TypicalProcessBLL.GetTypicalProcesList(int.Parse(nodeName));

            return typicalProcessList;
        }

        /// <summary>
        /// 获取所有已入库卡片
        /// </summary>
        private void LoadCardData()
        {
            listProcessCard = ProcessCardBLL.GetProcessVersion("");
            dgvCard.AutoGenerateColumns = false;
            dgvCard.DataSource = listProcessCard;

            DataTable dtUser = ProcessCardBLL.GetUsers();
            comboCreator.DisplayMember = "UserName";
            comboCreator.ValueMember = "UserId";
            comboCreator.DataSource = dtUser;
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

        /// <summary>
        /// 方法说明：获取查询条件
        /// 作   者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <returns></returns>
        private string GetConditions()
        {
            string condtion = string.Empty;

            List<string> listConditions = new List<string>();
            //零部件
            if (!string.IsNullOrEmpty(txtMaterial.Text))
            {
                //根据零部件名称得到挂在零部件下的卡片
                List<ProcessCard> listProcessCards = MaterialCardRelationBLL.GetProcessCardByMaterialName(txtMaterial.Text);

                List<string> listCardIds = new List<string>();
                if (listProcessCards != null && listProcessCards.Count > 0)
                {
                    foreach (ProcessCard card in listProcessCards)
                    {
                        listCardIds.Add(string.Format("'{0}'", card.ID.ToString()));
                    }

                    listConditions.Add(string.Format("BaseId in ({0})", string.Join(",", listCardIds.ToArray())));
                }
                
            }
            //文件类型
            if (comboFileType.SelectedItem != null)
            {
                if (comboFileType.SelectedIndex == 0)
                {
                    listConditions.Add("Type=1");
                }
                else
                {
                    listConditions.Add("Type=2");
                }
            }
            //文件状态
            if (comboFileStatus.SelectedItem != null)
            {
                if (comboFileStatus.SelectedIndex == 0)
                {
                    listConditions.Add("IsShow=2");
                }
                else if (comboFileStatus.SelectedIndex == 1)
                {
                    listConditions.Add("IsShow=1");
                }
                else
                {
                    listConditions.Add("IsShow=0");
                }

            }
            //编制人员
            if (comboCreator.SelectedValue != null)
            {
                listConditions.Add(string.Format("v.Creator='{0}'", comboCreator.SelectedValue.ToString()));
            }

            if (dtpCreateDate.Value != null)
            {
                listConditions.Add(string.Format("CONVERT(varchar(100), CONVERT(datetime,v.CreateDate), 23)= '{0}'", dtpCreateDate.Value.ToString("yyyy-MM-dd")));
            }

            if (listConditions.Count > 0)
                condtion = string.Join(" And ", listConditions.ToArray());

            return condtion;
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string condition = GetConditions();
            if (!string.IsNullOrEmpty(condition))
            {
                listProcessCard = ProcessCardBLL.GetProcessVersion(condition);
                dgvCard.DataSource = listProcessCard;
            }
        }
        
    }
}
