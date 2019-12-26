namespace Kingdee.CAPP.UI
{
    partial class ModuleManagerFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleManagerFrm));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.AddNodeCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvProcessCard = new System.Windows.Forms.TreeView();
            this.tcModuleManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.AddNodeCms.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tcModuleManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "content");
            this.imgList.Images.SetKeyName(2, "card");
            this.imgList.Images.SetKeyName(3, "detail");
            this.imgList.Images.SetKeyName(4, "folder_o");
            // 
            // AddNodeCms
            // 
            this.AddNodeCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddModuleToolStripMenuItem,
            this.AddFolderToolStripMenuItem,
            this.tsmnuDeleteModule,
            this.tsmnuDeleteFolder});
            this.AddNodeCms.Name = "AddNodeCms";
            this.AddNodeCms.Size = new System.Drawing.Size(153, 114);
            // 
            // AddModuleToolStripMenuItem
            // 
            this.AddModuleToolStripMenuItem.Name = "AddModuleToolStripMenuItem";
            this.AddModuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddModuleToolStripMenuItem.Text = "新增模版";
            this.AddModuleToolStripMenuItem.Click += new System.EventHandler(this.AddModuleToolStripMenuItem_Click);
            // 
            // AddFolderToolStripMenuItem
            // 
            this.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            this.AddFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddFolderToolStripMenuItem.Text = "新增文件夹";
            this.AddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // tsmnuDeleteModule
            // 
            this.tsmnuDeleteModule.Name = "tsmnuDeleteModule";
            this.tsmnuDeleteModule.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteModule.Text = "删除模版";
            this.tsmnuDeleteModule.Click += new System.EventHandler(this.tsmnuDeleteModule_Click);
            // 
            // tsmnuDeleteFolder
            // 
            this.tsmnuDeleteFolder.Name = "tsmnuDeleteFolder";
            this.tsmnuDeleteFolder.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteFolder.Text = "删除文件夹";
            this.tsmnuDeleteFolder.Click += new System.EventHandler(this.tsmnuDeleteFolder_Click);
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tabPage1.Controls.Add(this.tvProcessCard);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(230, 626);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工艺模板";
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
            this.tvProcessCard.Size = new System.Drawing.Size(224, 620);
            this.tvProcessCard.TabIndex = 0;
            this.tvProcessCard.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterCollapse);
            this.tvProcessCard.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterExpand);
            this.tvProcessCard.DoubleClick += new System.EventHandler(this.tvProcessCard_DoubleClick);
            this.tvProcessCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessCard_MouseDown);
            // 
            // tcModuleManager
            // 
            this.tcModuleManager.Controls.Add(this.tabPage1);
            this.tcModuleManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcModuleManager.Location = new System.Drawing.Point(0, 0);
            this.tcModuleManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcModuleManager.Name = "tcModuleManager";
            this.tcModuleManager.SelectedIndex = 0;
            this.tcModuleManager.Size = new System.Drawing.Size(238, 655);
            this.tcModuleManager.TabIndex = 1;
            // 
            // ModuleManagerFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(238, 655);
            this.Controls.Add(this.tcModuleManager);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ModuleManagerFrm";
            this.TabText = "模板管理中心";
            this.Text = "模板管理中心";
            this.Load += new System.EventHandler(this.ModuleManagerFrm_Load);
            this.Resize += new System.EventHandler(this.ModuleManagerFrm_Resize);
            this.AddNodeCms.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tcModuleManager.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip AddNodeCms;
        private System.Windows.Forms.ToolStripMenuItem AddModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddFolderToolStripMenuItem;
        private System.Windows.Forms.ImageList imgList;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvProcessCard;
        private Controls.FlatTabControl tcModuleManager;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteModule;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteFolder;
    }
}