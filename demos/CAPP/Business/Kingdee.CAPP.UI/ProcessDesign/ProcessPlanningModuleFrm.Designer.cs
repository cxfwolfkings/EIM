namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class ProcessPlanningModuleFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPlanningModuleFrm));
            this.AddModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.AddNodeCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddPlanningModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeletePlanningModule = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteCardModule = new System.Windows.Forms.ToolStripMenuItem();
            this.AddFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tvProcessPlanningModule = new System.Windows.Forms.TreeView();
            this.tcProcessPlanningModuleManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.AddNodeCms.SuspendLayout();
            this.tcProcessPlanningModuleManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddModuleToolStripMenuItem
            // 
            this.AddModuleToolStripMenuItem.Name = "AddModuleToolStripMenuItem";
            this.AddModuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddModuleToolStripMenuItem.Text = "新增卡片模版";
            this.AddModuleToolStripMenuItem.Click += new System.EventHandler(this.AddModuleToolStripMenuItem_Click);
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
            // AddNodeCms
            // 
            this.AddNodeCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddPlanningModuleToolStripMenuItem,
            this.tsmnuDeletePlanningModule,
            this.AddModuleToolStripMenuItem,
            this.tsmnuDeleteCardModule,
            this.AddFolderToolStripMenuItem,
            this.tsmnuDeleteFolder});
            this.AddNodeCms.Name = "AddNodeCms";
            this.AddNodeCms.Size = new System.Drawing.Size(153, 158);
            // 
            // AddPlanningModuleToolStripMenuItem
            // 
            this.AddPlanningModuleToolStripMenuItem.Name = "AddPlanningModuleToolStripMenuItem";
            this.AddPlanningModuleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddPlanningModuleToolStripMenuItem.Text = "新增规程模版";
            this.AddPlanningModuleToolStripMenuItem.Click += new System.EventHandler(this.AddPlanningModuleToolStripMenuItem_Click);
            // 
            // tsmnuDeletePlanningModule
            // 
            this.tsmnuDeletePlanningModule.Name = "tsmnuDeletePlanningModule";
            this.tsmnuDeletePlanningModule.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeletePlanningModule.Text = "删除规程模版";
            this.tsmnuDeletePlanningModule.Click += new System.EventHandler(this.tsmnuDeletePlanningModule_Click);
            // 
            // tsmnuDeleteCardModule
            // 
            this.tsmnuDeleteCardModule.Name = "tsmnuDeleteCardModule";
            this.tsmnuDeleteCardModule.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteCardModule.Text = "删除卡片模板";
            this.tsmnuDeleteCardModule.Click += new System.EventHandler(this.tsmnuDeleteCardModule_Click);
            // 
            // AddFolderToolStripMenuItem
            // 
            this.AddFolderToolStripMenuItem.Name = "AddFolderToolStripMenuItem";
            this.AddFolderToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AddFolderToolStripMenuItem.Text = "新增文件夹";
            this.AddFolderToolStripMenuItem.Click += new System.EventHandler(this.AddFolderToolStripMenuItem_Click);
            // 
            // tsmnuDeleteFolder
            // 
            this.tsmnuDeleteFolder.Name = "tsmnuDeleteFolder";
            this.tsmnuDeleteFolder.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteFolder.Text = "删除文件夹";
            this.tsmnuDeleteFolder.Click += new System.EventHandler(this.tsmnuDeleteFolder_Click);
            // 
            // tvProcessPlanningModule
            // 
            this.tvProcessPlanningModule.AllowDrop = true;
            this.tvProcessPlanningModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProcessPlanningModule.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvProcessPlanningModule.ImageIndex = 0;
            this.tvProcessPlanningModule.ImageList = this.imgList;
            this.tvProcessPlanningModule.Location = new System.Drawing.Point(0, 0);
            this.tvProcessPlanningModule.Name = "tvProcessPlanningModule";
            this.tvProcessPlanningModule.SelectedImageIndex = 0;
            this.tvProcessPlanningModule.Size = new System.Drawing.Size(287, 222);
            this.tvProcessPlanningModule.TabIndex = 0;
            this.tvProcessPlanningModule.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterCollapse);
            this.tvProcessPlanningModule.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterExpand);
            this.tvProcessPlanningModule.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvProcessPlanningModule_ItemDrag);
            this.tvProcessPlanningModule.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProcessPlanningModule_NodeMouseDoubleClick);
            this.tvProcessPlanningModule.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvProcessPlanningModule_DragDrop);
            this.tvProcessPlanningModule.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvProcessPlanningModule_DragEnter);
            this.tvProcessPlanningModule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessPlanningModule_MouseDown);
            // 
            // tcProcessPlanningModuleManager
            // 
            this.tcProcessPlanningModuleManager.Controls.Add(this.tabPage1);
            this.tcProcessPlanningModuleManager.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcProcessPlanningModuleManager.Location = new System.Drawing.Point(0, 0);
            this.tcProcessPlanningModuleManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProcessPlanningModuleManager.Name = "tcProcessPlanningModuleManager";
            this.tcProcessPlanningModuleManager.SelectedIndex = 0;
            this.tcProcessPlanningModuleManager.Size = new System.Drawing.Size(283, 216);
            this.tcProcessPlanningModuleManager.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tabPage1.Controls.Add(this.tvProcessPlanningModule);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(275, 187);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工艺规程模板";
            // 
            // ProcessPlanningModuleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(314, 508);
            this.Controls.Add(this.tcProcessPlanningModuleManager);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessPlanningModuleFrm";
            this.TabText = "工艺规程模板管理";
            this.Text = "工艺规程模板管理";
            this.Load += new System.EventHandler(this.ProcessPlanningModule_Load);
            this.Resize += new System.EventHandler(this.ProcessPlanningModule_Resize);
            this.AddNodeCms.ResumeLayout(false);
            this.tcProcessPlanningModuleManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem AddModuleToolStripMenuItem;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip AddNodeCms;
        private System.Windows.Forms.ToolStripMenuItem AddFolderToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.TreeView tvProcessPlanningModule;
        private System.Windows.Forms.ToolStripMenuItem AddPlanningModuleToolStripMenuItem;
        private Controls.FlatTabControl tcProcessPlanningModuleManager;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteCardModule;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeletePlanningModule;
    }
}