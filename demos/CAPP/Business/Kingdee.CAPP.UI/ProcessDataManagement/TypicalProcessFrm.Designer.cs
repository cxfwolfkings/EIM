namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class TypicalProcessFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TypicalProcessFrm));
            this.tvTypicalProcess = new System.Windows.Forms.TreeView();
            this.AddNodeCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddTypicalProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteTypicalProcess = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTypicalCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuExportCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuImportCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteProcessCard = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcTypicalProcessManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tbProcessPlaningModule = new System.Windows.Forms.TabPage();
            this.AddNodeCms.SuspendLayout();
            this.tcTypicalProcessManager.SuspendLayout();
            this.tbProcessPlaningModule.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvTypicalProcess
            // 
            this.tvTypicalProcess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvTypicalProcess.ContextMenuStrip = this.AddNodeCms;
            this.tvTypicalProcess.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvTypicalProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTypicalProcess.ImageIndex = 0;
            this.tvTypicalProcess.ImageList = this.imgList;
            this.tvTypicalProcess.Location = new System.Drawing.Point(3, 3);
            this.tvTypicalProcess.Name = "tvTypicalProcess";
            this.tvTypicalProcess.SelectedImageIndex = 0;
            this.tvTypicalProcess.Size = new System.Drawing.Size(263, 505);
            this.tvTypicalProcess.TabIndex = 0;
            this.tvTypicalProcess.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvTypicalProcess_AfterCollapse);
            this.tvTypicalProcess.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvTypicalProcess_AfterExpand);
            this.tvTypicalProcess.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvTypicalProcess_MouseDown);
            // 
            // AddNodeCms
            // 
            this.AddNodeCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTypicalProcessToolStripMenuItem,
            this.tsmnuDeleteTypicalProcess,
            this.AddTypicalCardToolStripMenuItem,
            this.AddFolderToolStripMenuItem,
            this.tsmnuDeleteFolder,
            this.tsmnuExportCard,
            this.tsmnuImportCard,
            this.tsmnuDeleteProcessCard});
            this.AddNodeCms.Name = "AddNodeCms";
            this.AddNodeCms.Size = new System.Drawing.Size(171, 180);
            // 
            // AddTypicalProcessToolStripMenuItem
            // 
            this.AddTypicalProcessToolStripMenuItem.Name = "AddTypicalProcessToolStripMenuItem";
            this.AddTypicalProcessToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.AddTypicalProcessToolStripMenuItem.Text = "新增典型工艺类型";
            this.AddTypicalProcessToolStripMenuItem.Click += new System.EventHandler(this.AddTypicalProcessToolStripMenuItem_Click);
            // 
            // tsmnuDeleteTypicalProcess
            // 
            this.tsmnuDeleteTypicalProcess.Name = "tsmnuDeleteTypicalProcess";
            this.tsmnuDeleteTypicalProcess.Size = new System.Drawing.Size(170, 22);
            this.tsmnuDeleteTypicalProcess.Text = "删除典型工艺类型";
            this.tsmnuDeleteTypicalProcess.Click += new System.EventHandler(this.tsmnuDeleteTypicalProcess_Click);
            // 
            // AddTypicalCardToolStripMenuItem
            // 
            this.AddTypicalCardToolStripMenuItem.Name = "AddTypicalCardToolStripMenuItem";
            this.AddTypicalCardToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.AddTypicalCardToolStripMenuItem.Text = "新增典型工艺卡片";
            this.AddTypicalCardToolStripMenuItem.Visible = false;
            this.AddTypicalCardToolStripMenuItem.Click += new System.EventHandler(this.AddTypicalCardToolStripMenuItem_Click);
            // 
            // AddFolderToolStripMenuItem
            // 
            this.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            this.AddFolderToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.AddFolderToolStripMenuItem.Text = "新增文件夹";
            this.AddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // tsmnuDeleteFolder
            // 
            this.tsmnuDeleteFolder.Name = "tsmnuDeleteFolder";
            this.tsmnuDeleteFolder.Size = new System.Drawing.Size(170, 22);
            this.tsmnuDeleteFolder.Text = "删除文件夹";
            this.tsmnuDeleteFolder.Click += new System.EventHandler(this.tsmnuDeleteFolder_Click);
            // 
            // tsmnuExportCard
            // 
            this.tsmnuExportCard.Name = "tsmnuExportCard";
            this.tsmnuExportCard.Size = new System.Drawing.Size(170, 22);
            this.tsmnuExportCard.Text = "导出典型工艺卡片";
            this.tsmnuExportCard.Click += new System.EventHandler(this.tsmnuExportCard_Click);
            // 
            // tsmnuImportCard
            // 
            this.tsmnuImportCard.Name = "tsmnuImportCard";
            this.tsmnuImportCard.Size = new System.Drawing.Size(170, 22);
            this.tsmnuImportCard.Text = "导入典型工艺卡片";
            this.tsmnuImportCard.Click += new System.EventHandler(this.tsmnuImportCard_Click);
            // 
            // tsmnuDeleteProcessCard
            // 
            this.tsmnuDeleteProcessCard.Name = "tsmnuDeleteProcessCard";
            this.tsmnuDeleteProcessCard.Size = new System.Drawing.Size(170, 22);
            this.tsmnuDeleteProcessCard.Text = "删除工艺卡片";
            this.tsmnuDeleteProcessCard.Click += new System.EventHandler(this.tsmnuDeleteProcessCard_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "card");
            this.imgList.Images.SetKeyName(2, "planning");
            this.imgList.Images.SetKeyName(3, "folder_o");
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tcTypicalProcessManager
            // 
            this.tcTypicalProcessManager.Controls.Add(this.tbProcessPlaningModule);
            this.tcTypicalProcessManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTypicalProcessManager.Location = new System.Drawing.Point(0, 0);
            this.tcTypicalProcessManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcTypicalProcessManager.Name = "tcTypicalProcessManager";
            this.tcTypicalProcessManager.SelectedIndex = 0;
            this.tcTypicalProcessManager.Size = new System.Drawing.Size(277, 540);
            this.tcTypicalProcessManager.TabIndex = 3;
            // 
            // tbProcessPlaningModule
            // 
            this.tbProcessPlaningModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbProcessPlaningModule.Controls.Add(this.tvTypicalProcess);
            this.tbProcessPlaningModule.Location = new System.Drawing.Point(4, 25);
            this.tbProcessPlaningModule.Name = "tbProcessPlaningModule";
            this.tbProcessPlaningModule.Padding = new System.Windows.Forms.Padding(3);
            this.tbProcessPlaningModule.Size = new System.Drawing.Size(269, 511);
            this.tbProcessPlaningModule.TabIndex = 0;
            this.tbProcessPlaningModule.Text = "典型工艺分类";
            // 
            // TypicalProcessFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 540);
            this.Controls.Add(this.tcTypicalProcessManager);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TypicalProcessFrm";
            this.TabText = "典型工艺";
            this.Text = "典型工艺";
            this.Load += new System.EventHandler(this.TypicalProcessFrm_Load);
            this.Resize += new System.EventHandler(this.TypicalProcessFrm_Resize);
            this.AddNodeCms.ResumeLayout(false);
            this.tcTypicalProcessManager.ResumeLayout(false);
            this.tbProcessPlaningModule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvTypicalProcess;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip AddNodeCms;
        private System.Windows.Forms.ToolStripMenuItem AddTypicalProcessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddTypicalCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddFolderToolStripMenuItem;
        private Controls.FlatTabControl tcTypicalProcessManager;
        private System.Windows.Forms.TabPage tbProcessPlaningModule;
        private System.Windows.Forms.ToolStripMenuItem tsmnuExportCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuImportCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteTypicalProcess;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteProcessCard;
    }
}