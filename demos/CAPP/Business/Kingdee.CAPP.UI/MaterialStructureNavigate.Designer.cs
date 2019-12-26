namespace Kingdee.CAPP.UI
{
    partial class MaterialStructureNavigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialStructureNavigate));
            this.tvMaterialDesign = new System.Windows.Forms.TreeView();
            this.cmsDesignBom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuNewCard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuAddProcessPlanning = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuMaterialQuota = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuChangeToTypical = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteCard = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tvMaterialPBom = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuDeleteFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcDesignView = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tbMaterial = new System.Windows.Forms.TabPage();
            this.tbProduct = new System.Windows.Forms.TabPage();
            this.cmsDesignBom.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.tcDesignView.SuspendLayout();
            this.tbMaterial.SuspendLayout();
            this.tbProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMaterialDesign
            // 
            this.tvMaterialDesign.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMaterialDesign.ContextMenuStrip = this.cmsDesignBom;
            this.tvMaterialDesign.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvMaterialDesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMaterialDesign.ImageIndex = 1;
            this.tvMaterialDesign.ImageList = this.imgList;
            this.tvMaterialDesign.Location = new System.Drawing.Point(3, 3);
            this.tvMaterialDesign.Name = "tvMaterialDesign";
            this.tvMaterialDesign.SelectedImageIndex = 0;
            this.tvMaterialDesign.Size = new System.Drawing.Size(277, 427);
            this.tvMaterialDesign.TabIndex = 0;
            this.tvMaterialDesign.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMaterial_MouseDown);
            // 
            // cmsDesignBom
            // 
            this.cmsDesignBom.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuNewCard,
            this.tsmnuAddProcessPlanning,
            this.tsmnuMaterialQuota,
            this.tsmnuChangeToTypical,
            this.tsmnuDeleteCard});
            this.cmsDesignBom.Name = "cmsDesignBom";
            this.cmsDesignBom.Size = new System.Drawing.Size(143, 114);
            // 
            // tsmnuNewCard
            // 
            this.tsmnuNewCard.Name = "tsmnuNewCard";
            this.tsmnuNewCard.Size = new System.Drawing.Size(142, 22);
            this.tsmnuNewCard.Text = "新建卡片";
            this.tsmnuNewCard.Click += new System.EventHandler(this.tsmnuNewCard_Click);
            // 
            // tsmnuAddProcessPlanning
            // 
            this.tsmnuAddProcessPlanning.Name = "tsmnuAddProcessPlanning";
            this.tsmnuAddProcessPlanning.Size = new System.Drawing.Size(142, 22);
            this.tsmnuAddProcessPlanning.Text = "新建工艺规程";
            this.tsmnuAddProcessPlanning.Click += new System.EventHandler(this.tsmnuAddProcessPlanning_Click);
            // 
            // tsmnuMaterialQuota
            // 
            this.tsmnuMaterialQuota.Name = "tsmnuMaterialQuota";
            this.tsmnuMaterialQuota.Size = new System.Drawing.Size(142, 22);
            this.tsmnuMaterialQuota.Text = "材料定额";
            this.tsmnuMaterialQuota.Click += new System.EventHandler(this.tsmnuMaterialQuota_Click);
            // 
            // tsmnuChangeToTypical
            // 
            this.tsmnuChangeToTypical.Name = "tsmnuChangeToTypical";
            this.tsmnuChangeToTypical.Size = new System.Drawing.Size(142, 22);
            this.tsmnuChangeToTypical.Text = "转为典型";
            this.tsmnuChangeToTypical.Click += new System.EventHandler(this.tsmnuChangeToTypical_Click);
            // 
            // tsmnuDeleteCard
            // 
            this.tsmnuDeleteCard.Name = "tsmnuDeleteCard";
            this.tsmnuDeleteCard.Size = new System.Drawing.Size(142, 22);
            this.tsmnuDeleteCard.Text = "删除卡片";
            this.tsmnuDeleteCard.Click += new System.EventHandler(this.tsmnuDeleteCard_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "library");
            this.imgList.Images.SetKeyName(1, "materialG");
            this.imgList.Images.SetKeyName(2, "materialS");
            this.imgList.Images.SetKeyName(3, "materialC");
            this.imgList.Images.SetKeyName(4, "class");
            this.imgList.Images.SetKeyName(5, "folder");
            this.imgList.Images.SetKeyName(6, "card");
            this.imgList.Images.SetKeyName(7, "folder_o");
            this.imgList.Images.SetKeyName(8, "materialCard");
            // 
            // tvMaterialPBom
            // 
            this.tvMaterialPBom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMaterialPBom.ContextMenuStrip = this.cmsDesignBom;
            this.tvMaterialPBom.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvMaterialPBom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMaterialPBom.ImageIndex = 5;
            this.tvMaterialPBom.ImageList = this.imgList;
            this.tvMaterialPBom.Location = new System.Drawing.Point(3, 3);
            this.tvMaterialPBom.Name = "tvMaterialPBom";
            this.tvMaterialPBom.SelectedImageIndex = 5;
            this.tvMaterialPBom.Size = new System.Drawing.Size(277, 427);
            this.tvMaterialPBom.TabIndex = 1;
            this.tvMaterialPBom.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvMaterialPBom_AfterCollapse);
            this.tvMaterialPBom.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvMaterialPBom_AfterExpand);
            this.tvMaterialPBom.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProduct_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuNewFolder,
            this.tsmnuDeleteFolder});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(131, 48);
            // 
            // tsmnuNewFolder
            // 
            this.tsmnuNewFolder.Name = "tsmnuNewFolder";
            this.tsmnuNewFolder.Size = new System.Drawing.Size(130, 22);
            this.tsmnuNewFolder.Text = "新建文件夹";
            this.tsmnuNewFolder.Click += new System.EventHandler(this.tsmnuNewFolder_Click);
            // 
            // tsmnuDeleteFolder
            // 
            this.tsmnuDeleteFolder.Name = "tsmnuDeleteFolder";
            this.tsmnuDeleteFolder.Size = new System.Drawing.Size(130, 22);
            this.tsmnuDeleteFolder.Text = "删除文件夹";
            this.tsmnuDeleteFolder.Click += new System.EventHandler(this.tsmnuDeleteFolder_Click);
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tcDesignView
            // 
            this.tcDesignView.Controls.Add(this.tbMaterial);
            this.tcDesignView.Controls.Add(this.tbProduct);
            this.tcDesignView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDesignView.Location = new System.Drawing.Point(0, 0);
            this.tcDesignView.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcDesignView.Name = "tcDesignView";
            this.tcDesignView.SelectedIndex = 0;
            this.tcDesignView.Size = new System.Drawing.Size(291, 462);
            this.tcDesignView.TabIndex = 4;
            this.tcDesignView.SelectedIndexChanged += new System.EventHandler(this.tcDesignView_SelectedIndexChanged);
            // 
            // tbMaterial
            // 
            this.tbMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbMaterial.Controls.Add(this.tvMaterialDesign);
            this.tbMaterial.Location = new System.Drawing.Point(4, 25);
            this.tbMaterial.Name = "tbMaterial";
            this.tbMaterial.Padding = new System.Windows.Forms.Padding(3);
            this.tbMaterial.Size = new System.Drawing.Size(283, 433);
            this.tbMaterial.TabIndex = 0;
            this.tbMaterial.Text = "EBOM";
            // 
            // tbProduct
            // 
            this.tbProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbProduct.Controls.Add(this.tvMaterialPBom);
            this.tbProduct.Location = new System.Drawing.Point(4, 25);
            this.tbProduct.Name = "tbProduct";
            this.tbProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tbProduct.Size = new System.Drawing.Size(283, 433);
            this.tbProduct.TabIndex = 1;
            this.tbProduct.Text = "PBOM";
            // 
            // MaterialStructureNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 462);
            this.Controls.Add(this.tcDesignView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialStructureNavigate";
            this.TabText = "物料结构视图";
            this.Text = "物料结构视图";
            this.Load += new System.EventHandler(this.MaterialNavigate_Load);
            this.cmsDesignBom.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.tcDesignView.ResumeLayout(false);
            this.tbMaterial.ResumeLayout(false);
            this.tbProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMaterialDesign;
        private System.Windows.Forms.ImageList imgList;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.TreeView tvMaterialPBom;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmnuNewFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteFolder;
        private System.Windows.Forms.ContextMenuStrip cmsDesignBom;
        private System.Windows.Forms.ToolStripMenuItem tsmnuNewCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuMaterialQuota;
        private Controls.FlatTabControl tcDesignView;
        private System.Windows.Forms.TabPage tbMaterial;
        private System.Windows.Forms.TabPage tbProduct;
        private System.Windows.Forms.ToolStripMenuItem tsmnuChangeToTypical;
        private System.Windows.Forms.ToolStripMenuItem tsmnuDeleteCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuAddProcessPlanning;
    }
}