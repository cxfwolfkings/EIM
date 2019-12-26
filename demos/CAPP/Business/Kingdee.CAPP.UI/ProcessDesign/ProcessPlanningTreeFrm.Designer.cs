namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class ProcessPlanningTreeFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPlanningTreeFrm));
            this.tvProcessProcedure = new System.Windows.Forms.TreeView();
            this.imgProcessProcedureList = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuDeleteProcessPlanning = new System.Windows.Forms.ToolStripMenuItem();
            this.NewAddProcessCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteProcessCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tcProcessProcedure = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tpProcessProcedure = new System.Windows.Forms.TabPage();
            this.contextMenuStrip1.SuspendLayout();
            this.tcProcessProcedure.SuspendLayout();
            this.tpProcessProcedure.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvProcessProcedure
            // 
            this.tvProcessProcedure.AllowDrop = true;
            this.tvProcessProcedure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProcessProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProcessProcedure.ImageIndex = 0;
            this.tvProcessProcedure.ImageList = this.imgProcessProcedureList;
            this.tvProcessProcedure.Location = new System.Drawing.Point(3, 3);
            this.tvProcessProcedure.Name = "tvProcessProcedure";
            this.tvProcessProcedure.SelectedImageIndex = 0;
            this.tvProcessProcedure.Size = new System.Drawing.Size(305, 514);
            this.tvProcessProcedure.TabIndex = 0;
            this.tvProcessProcedure.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessProcedure_AfterCollapse);
            this.tvProcessProcedure.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessProcedure_AfterExpand);
            this.tvProcessProcedure.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.tvProcessProcedure_ItemDrag);
            this.tvProcessProcedure.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvProcessProcedure_NodeMouseDoubleClick);
            this.tvProcessProcedure.DragDrop += new System.Windows.Forms.DragEventHandler(this.tvProcessProcedure_DragDrop);
            this.tvProcessProcedure.DragEnter += new System.Windows.Forms.DragEventHandler(this.tvProcessProcedure_DragEnter);
            this.tvProcessProcedure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessProcedure_MouseDown);
            // 
            // imgProcessProcedureList
            // 
            this.imgProcessProcedureList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgProcessProcedureList.ImageStream")));
            this.imgProcessProcedureList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgProcessProcedureList.Images.SetKeyName(0, "planning");
            this.imgProcessProcedureList.Images.SetKeyName(1, "card");
            this.imgProcessProcedureList.Images.SetKeyName(2, "folder");
            this.imgProcessProcedureList.Images.SetKeyName(3, "folder_o");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuDeleteProcessPlanning,
            this.NewAddProcessCardToolStripMenuItem,
            this.tsmnuDeleteProcessCard});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 92);
            // 
            // tsmnuDeleteProcessPlanning
            // 
            this.tsmnuDeleteProcessPlanning.Name = "tsmnuDeleteProcessPlanning";
            this.tsmnuDeleteProcessPlanning.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteProcessPlanning.Text = "删除工艺规程";
            this.tsmnuDeleteProcessPlanning.Click += new System.EventHandler(this.tsmnuDeleteProcessPlanning_Click);
            // 
            // NewAddProcessCardToolStripMenuItem
            // 
            this.NewAddProcessCardToolStripMenuItem.Name = "NewAddProcessCardToolStripMenuItem";
            this.NewAddProcessCardToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.NewAddProcessCardToolStripMenuItem.Text = "新增工艺卡片";
            this.NewAddProcessCardToolStripMenuItem.Click += new System.EventHandler(this.NewAddProcessCardToolStripMenuItem_Click);
            // 
            // tsmnuDeleteProcessCard
            // 
            this.tsmnuDeleteProcessCard.Name = "tsmnuDeleteProcessCard";
            this.tsmnuDeleteProcessCard.Size = new System.Drawing.Size(152, 22);
            this.tsmnuDeleteProcessCard.Text = "删除工艺卡片";
            this.tsmnuDeleteProcessCard.Click += new System.EventHandler(this.tsmnuDeleteProcessCard_Click);
            // 
            // tcProcessProcedure
            // 
            this.tcProcessProcedure.Controls.Add(this.tpProcessProcedure);
            this.tcProcessProcedure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProcessProcedure.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcProcessProcedure.Location = new System.Drawing.Point(0, 0);
            this.tcProcessProcedure.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProcessProcedure.Name = "tcProcessProcedure";
            this.tcProcessProcedure.SelectedIndex = 0;
            this.tcProcessProcedure.Size = new System.Drawing.Size(319, 549);
            this.tcProcessProcedure.TabIndex = 1;
            // 
            // tpProcessProcedure
            // 
            this.tpProcessProcedure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpProcessProcedure.Controls.Add(this.tvProcessProcedure);
            this.tpProcessProcedure.Location = new System.Drawing.Point(4, 25);
            this.tpProcessProcedure.Name = "tpProcessProcedure";
            this.tpProcessProcedure.Padding = new System.Windows.Forms.Padding(3);
            this.tpProcessProcedure.Size = new System.Drawing.Size(311, 520);
            this.tpProcessProcedure.TabIndex = 0;
            this.tpProcessProcedure.Text = "工艺规程";
            // 
            // ProcessPlanningTreeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 549);
            this.Controls.Add(this.tcProcessProcedure);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessPlanningTreeFrm";
            this.TabText = "工艺规程";
            this.Text = "工艺规程管理";
            this.Load += new System.EventHandler(this.ProcessProcedureTreeFrm_Load);
            this.Resize += new System.EventHandler(this.ProcessProcedureTreeFrm_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tcProcessProcedure.ResumeLayout(false);
            this.tpProcessProcedure.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvProcessProcedure;
        private System.Windows.Forms.ImageList imgProcessProcedureList;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem NewAddProcessCardToolStripMenuItem;
        private Controls.FlatTabControl tcProcessProcedure;
        private System.Windows.Forms.TabPage tpProcessProcedure;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteProcessPlanning;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteProcessCard;
    }
}