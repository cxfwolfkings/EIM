using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    public partial class ProcessCardEditFrm : DockContent
    {
        public static ProcessCardEditFrm processCardEditFrm = null;

        public ProcessCardEditFrm()
        {
            InitializeComponent();

            processCardEditFrm = this;
        }
      
        private void ProcessCardEditFrm_Load(object sender, EventArgs e)
        {
            LoadCardData();
        }
        
        public void LoadCardData()
        {
            //List<Model.ProcessCard> listProcessCard = Kingdee.CAPP.BLL.ProcessCardBLL.GetDefaultProcessCardList();
            //dgvCard.DataSource = listProcessCard;
            
            
            try
            {
                tvProcessCard.Nodes.Clear();

                CardManager cardManager =
                    CardManagerBLL.GetCardManagerListById(-1).FirstOrDefault<CardManager>();

                //if (cardManager == null) return;
                //如果数据库内没有记录，则默认增加一条root记录
                if (cardManager == null)
                {
                    cardManager = new CardManager();
                    cardManager.Name = "工艺卡片";
                    cardManager.BusinessId = Guid.NewGuid();
                    cardManager.CurrentNode = CardManagerBLL.AddBusiness(cardManager.Name, -1, -1, cardManager.BusinessId);
                }

                TreeNode root = new TreeNode();
                root.Text = cardManager.Name;
                root.Tag = " ";
                root.ImageKey = "folder_o";
                root.Expand();

                ShowChildNode(root);

                tvProcessCard.Nodes.Add(root);
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
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

                List<ProcessVersion> versionList = ProcessCardBLL.GetProcessCardByFolderId(f.FolderId, 1);
                if (versionList != null && versionList.Count > 0)
                {
                    versionList.ForEach((v) =>
                        {
                            TreeNode nd = new TreeNode();
                            nd.Text = v.Name;
                            nd.Tag = v.BaseId;
                            nd.Name = v.Name;
                            nd.ImageKey = "card";

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
        /// 方法说明：得到勾选的卡片ID
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <returns>卡片ID集合</returns>
        //private List<string> GetSelectedCardId()
        //{
        //    //List<string> listCardId = new List<string>();
        //    //foreach (DataGridViewRow row in dgvCard.Rows)
        //    //{
        //    //    if ((bool)row.Cells[0].EditedFormattedValue == true)
        //    //    {
        //    //        listCardId.Add(row.Cells["colCardId"].Value.ToString());
        //    //    }
        //    //}
        //    //return listCardId;
        //}

        /// <summary>
        /// 方法说明：得到勾选的卡片ID和名称
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <returns>卡片ID和名称字典</returns>
        private Dictionary<string, string> GetSelectedCardIds()
        {
            Dictionary<string, string> dicCardId = new Dictionary<string, string>();
            //foreach (DataGridViewRow row in dgvCard.Rows)
            //{
            //    if ((bool)row.Cells[0].EditedFormattedValue == true)
            //    {
            //        dicCardId.Add(row.Cells["colCardId"].Value.ToString(), row.Cells["colName"].Value.ToString());
            //    }
            //}
            return dicCardId;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> cardIds = GetSelectedCardIds();
            if (cardIds == null || cardIds.Count == 0)
            {
                MessageBox.Show("请选择一个卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cardIds.Count > 1)
            {
                MessageBox.Show("请选择单个卡片打开", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string cardName = string.Empty;
            string cardId = string.Empty;
            foreach (string key in cardIds.Keys)
            {
                cardId = key;
                cardName = cardIds[key];
            }

            //打开卡片
            FormCollection collection = Application.OpenForms;
            bool isOpened = false;
            foreach (Form form in collection)
            {
                if (form.Name.EndsWith(cardId))
                {
                    isOpened = true;
                    ((ProcessCardFrm)form).TabText = cardName;
                    ((ProcessCardFrm)form).OpenCard(null, cardId, false, true);
                    form.Select();
                }
            }

            if (!isOpened)
            {
                ProcessCardFrm frm = new ProcessCardFrm();
                frm.TabText = cardName;
                frm.Name = string.Format("ProcessCardFrm-{0}", cardId);
                MainFrm.mainFrm.OpenModule(frm);
                bool result = frm.OpenCard(null, cardId, false, true);
                if (!result)
                {
                    MainFrm.mainFrm.CloseModule(frm);
                }
            }

            this.Close();
        }

        private void tsmnuChangeToTypical_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dicCardIds = new Dictionary<string, string>();
            dicCardIds.Add(tvProcessCard.SelectedNode.Tag.ToString(), tvProcessCard.SelectedNode.Name);

            using (TypicalCategoryFrm form = new TypicalCategoryFrm())
            {
                form.ProcessCardIds = dicCardIds;
                //form.ProcessCardName = "";
                form.ShowDialog();
            }
        }

        private void bwLoadTree_DoWork(object sender, DoWorkEventArgs e)
        {
            LoadCardData();
        }

        private Point p;
        private void tvProcessCard_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessCard.GetNodeAt(p);
            if (selectedNode == null) return;
            tvProcessCard.SelectedNode = selectedNode;
            selectedNode.SelectedImageKey = selectedNode.ImageKey;

            if (selectedNode.SelectedImageKey == "folder" || selectedNode.SelectedImageKey == "folder_o")
            {
                tsmnuDeleteCard.Visible = false;
                tsmnuChangeToTypical.Visible = false;
            }
            else if (selectedNode.SelectedImageKey == "card")
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
                            ((ProcessCardFrm)form).OpenCard(null, selectedNode.Tag.ToString(), false, true);
                            form.Select();
                        }
                    }

                    if (!isOpened)
                    {
                        ProcessCardFrm frm = new ProcessCardFrm();
                        frm.TabText = selectedNode.Text;
                        frm.Name = string.Format("ProcessCardFrm-{0}", selectedNode.Tag.ToString());
                        MainFrm.mainFrm.OpenModule(frm);
                        bool result = frm.OpenCard(null, selectedNode.Tag.ToString(), false, true);
                        if (!result)
                        {
                            MainFrm.mainFrm.CloseModule(frm);
                        }
                    }
                }

                tsmnuDeleteCard.Visible = true;
                tsmnuChangeToTypical.Visible = true;
            }
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

        private void tsmnuDeleteCard_Click(object sender, EventArgs e)
        {
            if (tvProcessCard.SelectedNode == null || tvProcessCard.SelectedNode.Tag == null)
                return;

            bool result = Kingdee.CAPP.BLL.ProcessCardBLL.DeleteProcessVersion(tvProcessCard.SelectedNode.Tag.ToString(), tvProcessCard.SelectedNode.Parent.Tag.ToString());
            if (result)
            {
                result = Kingdee.CAPP.BLL.ProcessCardBLL.DeleteProcessCard(tvProcessCard.SelectedNode.Tag.ToString());
                MessageBox.Show("删除成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tvProcessCard.Nodes.Remove(tvProcessCard.SelectedNode);
            }
        }
    }
}
