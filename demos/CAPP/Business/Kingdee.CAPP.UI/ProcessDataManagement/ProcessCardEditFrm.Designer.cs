namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProcessCardEditFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessCardEditFrm));
            this.tcProcessCardManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvProcessCard = new System.Windows.Forms.TreeView();
            this.AddNodeCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuDeleteCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuChangeToTypical = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcProcessCardManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.AddNodeCms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcProcessCardManager
            // 
            this.tcProcessCardManager.Controls.Add(this.tabPage1);
            this.tcProcessCardManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProcessCardManager.Location = new System.Drawing.Point(0, 0);
            this.tcProcessCardManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProcessCardManager.Name = "tcProcessCardManager";
            this.tcProcessCardManager.SelectedIndex = 0;
            this.tcProcessCardManager.Size = new System.Drawing.Size(324, 528);
            this.tcProcessCardManager.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tabPage1.Controls.Add(this.tvProcessCard);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(316, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工艺卡片";
            // 
            // tvProcessCard
            // 
            this.tvProcessCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProcessCard.ContextMenuStrip = this.AddNodeCms;
            this.tvProcessCard.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvProcessCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProcessCard.ImageIndex = 0;
            this.tvProcessCard.ImageList = this.imgList;
            this.tvProcessCard.Location = new System.Drawing.Point(3, 3);
            this.tvProcessCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvProcessCard.Name = "tvProcessCard";
            this.tvProcessCard.SelectedImageIndex = 0;
            this.tvProcessCard.Size = new System.Drawing.Size(310, 493);
            this.tvProcessCard.TabIndex = 0;
            this.tvProcessCard.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterCollapse);
            this.tvProcessCard.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterExpand);
            this.tvProcessCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessCard_MouseDown);
            // 
            // AddNodeCms
            // 
            this.AddNodeCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuDeleteCard,
            this.tsmnuDeleteFolder,
            this.tsmnuChangeToTypical});
            this.AddNodeCms.Name = "AddNodeCms";
            this.AddNodeCms.Size = new System.Drawing.Size(153, 92);
            // 
            // tsmnuDeleteCard
            // 
            this.tsmnuDeleteCard.Name = "tsmnuDeleteCard";
            this.tsmnuDeleteCard.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteCard.Text = "删除卡片";
            this.tsmnuDeleteCard.Click += new System.EventHandler(this.tsmnuDeleteCard_Click);
            // 
            // tsmnuDeleteFolder
            // 
            this.tsmnuDeleteFolder.Name = "tsmnuDeleteFolder";
            this.tsmnuDeleteFolder.Size = new System.Drawing.Size(134, 22);
            this.tsmnuDeleteFolder.Text = "删除文件夹";
            this.tsmnuDeleteFolder.Visible = false;
            // 
            // tsmnuChangeToTypical
            // 
            this.tsmnuChangeToTypical.Name = "tsmnuChangeToTypical";
            this.tsmnuChangeToTypical.Size = new System.Drawing.Size(134, 22);
            this.tsmnuChangeToTypical.Text = "转为典型";
            this.tsmnuChangeToTypical.Click += new System.EventHandler(this.tsmnuChangeToTypical_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "card");
            this.imgList.Images.SetKeyName(2, "folder_o");
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // ProcessCardEditFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 528);
            this.Controls.Add(this.tcProcessCardManager);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessCardEditFrm";
            this.TabText = "工艺卡片管理";
            this.Text = "工艺卡片管理";
            this.Load += new System.EventHandler(this.ProcessCardEditFrm_Load);
            this.tcProcessCardManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.AddNodeCms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.FlatTabControl tcProcessCardManager;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvProcessCard;
        private System.Windows.Forms.ImageList imgList;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.ContextMenuStrip AddNodeCms;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmnuChangeToTypical;

    }
}