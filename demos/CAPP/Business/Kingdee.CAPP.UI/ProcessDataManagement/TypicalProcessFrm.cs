using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;
using System.IO;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：典型工艺管理界面
    /// 作      者：jason.tang
    /// 完成时间：2013-05-23
    /// </summary>
    public partial class TypicalProcessFrm : BaseForm
    {
        public static TypicalProcessFrm typicalProcessForm = null;

        public TypicalProcessFrm()
        {
            InitializeComponent();

            typicalProcessForm = this;
        }

        #region 窗体控件事件

        private void TypicalProcessFrm_Resize(object sender, EventArgs e)
        {
            tcTypicalProcessManager.Width = tvTypicalProcess.Width = Width;
            tcTypicalProcessManager.Height = tvTypicalProcess.Height = Height;
        }


        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadTreeData();
        }

        private void TypicalProcessFrm_Load(object sender, EventArgs e)
        {
            LoadTreeData();

            AddNodeCms.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
        }

        private void AddTypicalProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypicalOrFolder(BusinessType.Planning);
        }

        private void AddTypicalCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypicalOrFolder(BusinessType.Card);
        }

        private void AddFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTypicalOrFolder(BusinessType.Folder);
        }

        private Point p;
        private void tvTypicalProcess_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvTypicalProcess.GetNodeAt(p);
            if (selectedNode == null) return;

            tvTypicalProcess.SelectedNode = selectedNode;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            ShowChildNode(selectedNode);

            /// under folder can new typical process  folder
            if (selectedNode.SelectedImageKey == "folder" || selectedNode.SelectedImageKey == "folder_o")
            {
                ///new process card module menu
                AddFolderToolStripMenuItem.Visible = true;
                //AddTypicalCardToolStripMenuItem.Visible = false;
                AddTypicalProcessToolStripMenuItem.Visible = true;
                tsmnuExportCard.Visible = false;
                tsmnuDeleteFolder.Visible = true;
                tsmnuDeleteProcessCard.Visible = false;
                tsmnuDeleteTypicalProcess.Visible = false;
                tsmnuImportCard.Visible = false;
            }
            ///under process type can new process card
            else if (selectedNode.SelectedImageKey == "planning")
            {
                ///new folder menu
                AddFolderToolStripMenuItem.Visible = false;
                //AddTypicalCardToolStripMenuItem.Visible = true;
                AddTypicalProcessToolStripMenuItem.Visible = false;
                tsmnuExportCard.Visible = false;
                tsmnuDeleteFolder.Visible = false;
                tsmnuDeleteProcessCard.Visible = false;
                tsmnuDeleteTypicalProcess.Visible = true;
                tsmnuImportCard.Visible = true;
            }
            else if(selectedNode.SelectedImageKey == "card")
            {
                AddFolderToolStripMenuItem.Visible = false;
                //AddTypicalCardToolStripMenuItem.Visible = false;
                AddTypicalProcessToolStripMenuItem.Visible = false;


                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    OpenCard(selectedNode);

                tsmnuExportCard.Visible = true;
                tsmnuDeleteFolder.Visible = false;
                tsmnuDeleteProcessCard.Visible = true;
                tsmnuDeleteTypicalProcess.Visible = false;
                tsmnuImportCard.Visible = false;
            }
        }

        private void tvTypicalProcess_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder")
            {
                e.Node.ImageKey = "folder_o";
                e.Node.SelectedImageKey = "folder_o";
            }
        }

        private void tvTypicalProcess_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.ImageKey == "folder_o")
            {
                e.Node.ImageKey = "folder";
                e.Node.SelectedImageKey = "folder";
            }
        }

        /// <summary>
        /// 导出典型工艺卡片到本地
        /// </summary>
        private void tsmnuExportCard_Click(object sender, EventArgs e)
        {
            if (PropertiesNavigate.CurrentForm != null)
            {
                string formName = PropertiesNavigate.CurrentForm.GetType().Name;

                if (formName == typeof(ProcessCardFrm).Name)
                {
                    ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                    ProcessCardFrm.processCardFrm.SaveCard();
                }
            }
        }

        private ProcessCard processCardInfo = null;
        /// <summary>
        /// 导入本地卡片到典型工艺
        /// </summary>
        private void tsmnuImportCard_Click(object sender, EventArgs e)
        {
            ProcessCardFrm frm = new ProcessCardFrm();
            frm.Name = string.Format("ProcessCardFrm-{0}", Guid.NewGuid().ToString());

            if (frm != null)
            {
                string path = string.Empty;
                
                    OpenFileDialog of = new OpenFileDialog();
                    of.Filter = "CARD files (*.card)|*.card";
                    if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        path = of.FileName;
                    }


                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                #region 设置新增卡片Tab的TabText及Name

                string cardName = Path.GetFileNameWithoutExtension(path);
                int tag = 1;
                int index = 1;
                List<int> listIndex = new List<int>();
                foreach (WeifenLuo.WinFormsUI.Docking.DockContent form in this.MdiChildren)
                {
                    if (form.Name.StartsWith("ProcessCardFrm") &&
                        form.TabText.StartsWith(cardName))
                    {
                        int start = form.TabText.IndexOf("-") + 1;
                        tag = int.Parse(form.TabText.Substring(start));
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
                frm.TabText = string.Format("{0}-{1}", cardName, index);

                #endregion

                MainFrm.mainFrm.OpenModule(frm);
                frm.OpenCard(path, null, false, true);                
                processCardInfo = frm.SaveCardIntoDatabaseWithName(cardName);

                if (processCardInfo != null)
                {
                    AddTypicalOrFolder(BusinessType.Card);
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：加载数据到树
        /// </summary>
        public void LoadTreeData()
        {
            try
            {
                List<TypicalProcess> typicalProcessList =
                    TypicalProcessBLL.GetTypicalProcesList(0);

                //如果数据库内没有工艺规程模版记录，则默认增加一条root记录
                if (typicalProcessList.Count <= 0)
                {
                    typicalProcessList = new List<TypicalProcess>();
                    TypicalProcess typicalProcess = new TypicalProcess();
                    typicalProcess.BusinessId = Guid.NewGuid();
                    typicalProcess.Name = "典型工艺";
                    typicalProcess.BType = 0;
                    typicalProcess.ParentNode = 0;
                    typicalProcess.Sort = 1;

                    int currentNode = TypicalProcessBLL.AddTypicalProcess(typicalProcess);

                    typicalProcess.CurrentNode = currentNode;
                    typicalProcessList.Add(typicalProcess);
                }

                TypicalProcess typicalProcessModule = typicalProcessList[0];

                TreeNode root = new TreeNode();
                root.Text = typicalProcessModule.Name;
                root.Tag = typicalProcessModule.BusinessId;
                root.ImageKey = "folder";
                root.Name = typicalProcessModule.CurrentNode.ToString();
                root.Expand();

                ShowChildNode(root);

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
        private void ShowChildNode(TreeNode parentNode)
        {
            if (parentNode == null) return;

            parentNode.Nodes.Clear();

            List<TypicalProcess> typicalProcessList
                = GetTreeData(parentNode.Name);

            typicalProcessList.ForEach((o) =>
            {
                TreeNode node = new TreeNode();
                node.Text = o.Name;
                if (o.BType == BusinessType.Card)
                    node.Tag = o.TypicalProcessId + "@" + o.BusinessId;
                else
                    node.Tag = o.TypicalProcessId;

                node.Name = o.CurrentNode.ToString();
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

                List<TypicalProcess> nodeTypicalProcessList
                    = GetTreeData(node.Name);

                if (nodeTypicalProcessList.Count > 0)
                {
                    node.Nodes.Add(new TreeNode());
                }

                parentNode.Nodes.Add(node);
            });
        }

        /// <summary>
        /// 缓存树节点
        /// </summary>
        static Dictionary<string, List<TypicalProcess>> typicalProcessDic
                = new Dictionary<string, List<TypicalProcess>>();

        /// <summary>
        /// 方法说明：获取树节点数据
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public List<TypicalProcess> GetTreeData(string nodeName)
        {
            List<TypicalProcess> typicalProcessList = null;
            if (!typicalProcessDic.ContainsKey(nodeName))
            {
                typicalProcessList =
                    TypicalProcessBLL.GetTypicalProcesList(int.Parse(nodeName));
                typicalProcessDic.Add(nodeName, typicalProcessList);
            }
            else
            {
                typicalProcessList = typicalProcessDic[nodeName];
            }
            return typicalProcessList;
        }

        /// <summary>
        /// 方法说明：增加典型工艺或文件夹
        /// </summary>
        /// <param name="type">类型</param>
        void AddTypicalOrFolder(BusinessType type)
        {
            TreeNode node = tvTypicalProcess.SelectedNode;
            if (node == null) return;

            TypicalProcess typicalProcess = new TypicalProcess();
            typicalProcess.BType = type;
            typicalProcess.ParentNode = int.Parse(node.Name);


            ///批量名称
            Dictionary<string, TypicalProcess> dic = new Dictionary<string, TypicalProcess>();
            DialogResult result;

            if (type == BusinessType.Folder)
            {
                TypicalProcessFolderFrm typicalProcessFolderFrm
                   = new TypicalProcessFolderFrm(typicalProcess);

                result = typicalProcessFolderFrm.ShowDialog();


                if (result == DialogResult.OK)
                {
                    ///当前节点修改缓存且显示在界面上
                    dic = typicalProcessFolderFrm.NewNodeNameDic;
                }
            }
            else if (type == BusinessType.Card)
            {
            //    AddTypicalProcessCardFrm processCardModuleFrm
            //        = new AddTypicalProcessCardFrm(typicalProcess);



            //    result = processCardModuleFrm.ShowDialog();

            //    if (result == DialogResult.OK)
            //    {
            //        ///当前节点修改缓存且显示在界面上
            //        dic = processCardModuleFrm.ProcessPlanningModuleDic;
            //    }

            //}
            //else if (type == BusinessType.Planning)
            //{
                Dictionary<string, TypicalProcess> dicProcessCard = new Dictionary<string, TypicalProcess>();
                List<TypicalProcess> list = new List<TypicalProcess>();
                
                TypicalProcess typical = new TypicalProcess();
                typical.Name = processCardInfo.Name;
                typical.BusinessId = processCardInfo.ID;
                typical.TypicalProcessId = Guid.NewGuid();
                typical.BType = BusinessType.Card;
                typical.ParentNode = typicalProcess.ParentNode;
                list.Add(typical);

                TypicalProcessBLL.AddTypicalProcess(list);
                dicProcessCard.Add(typical.Name, typical);
                dic = dicProcessCard;
            }
            else
            {
                AddTypicalProcessFrm addProcessPlanningModuleFrm
                    = new AddTypicalProcessFrm(typicalProcess);


                result = addProcessPlanningModuleFrm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ///当前节点修改缓存且显示在界面上
                    dic = addProcessPlanningModuleFrm.NewNodeNameDic;
                }
            }

            List<TypicalProcess> processPlanningModuleList = null;
            if (!typicalProcessDic.ContainsKey(node.Name))
            {
                processPlanningModuleList = new List<TypicalProcess>();
                processPlanningModuleList.Add(typicalProcess);
                typicalProcessDic.Add(node.Name, processPlanningModuleList);
            }
            else
            {
                /// 更新字典表内的值
                processPlanningModuleList = typicalProcessDic[node.Name];


                foreach (var kv in dic)
                {
                    processPlanningModuleList.Add(new TypicalProcess()
                    {
                        BType = type,
                        CurrentNode = kv.Value.CurrentNode,
                        Name = kv.Key
                    });


                    TreeNode newNode = new TreeNode();
                    newNode.Text = kv.Key;
                    newNode.Tag = kv.Value.BusinessId;
                    newNode.Name = kv.Value.CurrentNode.ToString();
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
                    tvTypicalProcess.SelectedNode = newNode;

                    node.Expand();

                    ///每个新增加的节点也要加入缓存
                    typicalProcessDic.Add(newNode.Name,
                        new List<TypicalProcess>());
                }

                ///更新当前节点含有的子节点
                typicalProcessDic[node.Name] = processPlanningModuleList;
            }
        }

        private void OpenCard(TreeNode selectedNode)
        {            
            //打开卡片
            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                int splitIndex = selectedNode.Tag.ToString().IndexOf("@");
                string typicalid = string.Empty;
                string cardid = string.Empty;

                if (splitIndex > 0)
                {
                    typicalid = selectedNode.Tag.ToString().Substring(0, splitIndex);
                    cardid = selectedNode.Tag.ToString().Substring(splitIndex + 1);
                }
                else
                    cardid = selectedNode.Tag.ToString();

                if (form.Name.EndsWith(cardid))
                {
                    isOpened = true;
                    ((ProcessCardFrm)form).TabText = selectedNode.Text;
                    ((ProcessCardFrm)form).OpenCard(null, cardid, false, true);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                ProcessCardFrm frm = new ProcessCardFrm();
                frm.TabText = selectedNode.Text;

                int splitIndex = selectedNode.Tag.ToString().IndexOf("@");
                string typicalid = string.Empty;
                string cardid = string.Empty;

                if (splitIndex > 0)
                {
                    typicalid = selectedNode.Tag.ToString().Substring(0, splitIndex);
                    cardid = selectedNode.Tag.ToString().Substring(splitIndex + 1);
                }
                else
                    cardid = selectedNode.Tag.ToString();

                frm.Name = string.Format("ProcessCardFrm-{0}", cardid);
                MainFrm.mainFrm.OpenModule(frm);
                bool result = frm.OpenCard(null, cardid, false, true);
                if (!result)
                {
                    MainFrm.mainFrm.CloseModule(frm);
                }
            }
        }

        public void ChangeToTypical()
        {
            typicalProcessDic.Clear();
            tvTypicalProcess.Nodes.Clear();

            LoadTreeData();
        }

        #endregion

        /// <summary>
        /// 删除卡片
        /// </summary>
        private void tsmnuDeleteProcessCard_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvTypicalProcess.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        private void tsmnuDeleteFolder_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvTypicalProcess.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该文件夹下包含典型工艺，不能删除", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 删除典型工艺
        /// </summary>
        private void tsmnuDeleteTypicalProcess_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvTypicalProcess.GetNodeAt(p);
            if (selectedNode == null || selectedNode.Tag == null) return;

            if (selectedNode.Nodes.Count > 0)
            {
                MessageBox.Show("该典型工艺下包含卡片，不能删除", "提示",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            RemoveTreeNode(selectedNode);
        }

        /// <summary>
        /// 方法说明：根据BusinessId删除典型或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="Id">业务ID</param>
        private void RemoveTreeNode(TreeNode currentNode)
        {
            if (currentNode == null) return;

            int splitIndex = currentNode.Tag.ToString().IndexOf("@");
            string typicalId;
            if(splitIndex >= 0)
                typicalId = currentNode.Tag.ToString().Substring(0, splitIndex);
            else
                typicalId = currentNode.Tag.ToString();

            bool result = TypicalProcessBLL.DeleteTypicalById(new Guid(typicalId));
            if (result)
            {
                //Remove oneself Cache
                typicalProcessDic.Remove(currentNode.Name.ToString());

                List<TypicalProcess> cardList = typicalProcessDic[currentNode.Parent.Name];
                var afterCardList = cardList.Where<TypicalProcess>(
                                              x => x.CurrentNode.ToString() != currentNode.Name)
                                            .ToList<TypicalProcess>();

                typicalProcessDic[currentNode.Parent.Name] = afterCardList;

                if (currentNode.Parent != null && !string.IsNullOrEmpty(currentNode.Parent.ImageKey))
                {
                    tvTypicalProcess.SelectedNode = currentNode.Parent;
                    tvTypicalProcess.SelectedImageKey = currentNode.Parent.ImageKey;
                }
                else
                {
                    tvTypicalProcess.SelectedNode = tvTypicalProcess.Nodes[0];
                    ///  如果选中的节点为空,默认为卡片
                    tvTypicalProcess.SelectedImageKey = tvTypicalProcess.Nodes[0].ImageKey;
                }

                //Remove treeview
                tvTypicalProcess.Nodes.Remove(currentNode);
                tvTypicalProcess.SelectedNode.Expand();
            }
        }
    }
}
