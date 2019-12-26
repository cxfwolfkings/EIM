namespace Kingdee.CAPP.UI
{
    partial class ProductStructureNavigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductStructureNavigate));
            this.tvMaterial = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tvPBOM = new System.Windows.Forms.TreeView();
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcStructrueView = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tbMaterial = new System.Windows.Forms.TabPage();
            this.tbPBom = new System.Windows.Forms.TabPage();
            this.tcStructrueView.SuspendLayout();
            this.tbMaterial.SuspendLayout();
            this.tbPBom.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMaterial
            // 
            this.tvMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMaterial.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMaterial.ImageIndex = 0;
            this.tvMaterial.ImageList = this.imgList;
            this.tvMaterial.Location = new System.Drawing.Point(3, 3);
            this.tvMaterial.Name = "tvMaterial";
            this.tvMaterial.SelectedImageIndex = 0;
            this.tvMaterial.Size = new System.Drawing.Size(229, 427);
            this.tvMaterial.TabIndex = 1;
            this.tvMaterial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMaterial_MouseDown);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "product");
            this.imgList.Images.SetKeyName(2, "materialG");
            // 
            // tvPBOM
            // 
            this.tvPBOM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvPBOM.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvPBOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPBOM.ImageIndex = 0;
            this.tvPBOM.ImageList = this.imgList;
            this.tvPBOM.Location = new System.Drawing.Point(3, 3);
            this.tvPBOM.Name = "tvPBOM";
            this.tvPBOM.SelectedImageIndex = 0;
            this.tvPBOM.Size = new System.Drawing.Size(186, 60);
            this.tvPBOM.TabIndex = 0;
            this.tvPBOM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvPBOM_MouseDown);
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tcStructrueView
            // 
            this.tcStructrueView.Controls.Add(this.tbMaterial);
            this.tcStructrueView.Controls.Add(this.tbPBom);
            this.tcStructrueView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcStructrueView.Location = new System.Drawing.Point(0, 0);
            this.tcStructrueView.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcStructrueView.Name = "tcStructrueView";
            this.tcStructrueView.SelectedIndex = 0;
            this.tcStructrueView.Size = new System.Drawing.Size(243, 462);
            this.tcStructrueView.TabIndex = 3;
            this.tcStructrueView.SelectedIndexChanged += new System.EventHandler(this.tcStructureView_SelectedIndexChanged);
            // 
            // tbMaterial
            // 
            this.tbMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbMaterial.Controls.Add(this.tvMaterial);
            this.tbMaterial.Location = new System.Drawing.Point(4, 25);
            this.tbMaterial.Name = "tbMaterial";
            this.tbMaterial.Padding = new System.Windows.Forms.Padding(3);
            this.tbMaterial.Size = new System.Drawing.Size(235, 433);
            this.tbMaterial.TabIndex = 0;
            this.tbMaterial.Text = "设计BOM";
            // 
            // tbPBom
            // 
            this.tbPBom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbPBom.Controls.Add(this.tvPBOM);
            this.tbPBom.Location = new System.Drawing.Point(4, 25);
            this.tbPBom.Name = "tbPBom";
            this.tbPBom.Padding = new System.Windows.Forms.Padding(3);
            this.tbPBom.Size = new System.Drawing.Size(235, 433);
            this.tbPBom.TabIndex = 1;
            this.tbPBom.Text = "PBOM";
            // 
            // ProductStructureNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 462);
            this.Controls.Add(this.tcStructrueView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductStructureNavigate";
            this.TabText = "产品结构视图";
            this.Text = "产品结构视图";
            this.Load += new System.EventHandler(this.ProductNavigate_Load);
            this.tcStructrueView.ResumeLayout(false);
            this.tbMaterial.ResumeLayout(false);
            this.tbPBom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvPBOM;
        private System.Windows.Forms.ImageList imgList;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.TreeView tvMaterial;
        private Controls.FlatTabControl tcStructrueView;
        private System.Windows.Forms.TabPage tbMaterial;
        private System.Windows.Forms.TabPage tbPBom;
    }
}