namespace Kingdee.CAPP.UI
{
    partial class MaterialChooseNavigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialChooseNavigate));
            this.tvMaterial = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcMaterialChoose = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tbMaterial = new System.Windows.Forms.TabPage();
            this.tcMaterialChoose.SuspendLayout();
            this.tbMaterial.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvMaterial
            // 
            this.tvMaterial.BackColor = System.Drawing.SystemColors.Window;
            this.tvMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvMaterial.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvMaterial.ImageIndex = 0;
            this.tvMaterial.ImageList = this.imgList;
            this.tvMaterial.Location = new System.Drawing.Point(3, 3);
            this.tvMaterial.Name = "tvMaterial";
            this.tvMaterial.SelectedImageIndex = 0;
            this.tvMaterial.Size = new System.Drawing.Size(253, 483);
            this.tvMaterial.TabIndex = 0;
            this.tvMaterial.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvMaterial_AfterCollapse);
            this.tvMaterial.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvMaterial_AfterExpand);
            this.tvMaterial.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvMaterial_MouseDown);
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
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tcMaterialChoose
            // 
            this.tcMaterialChoose.Controls.Add(this.tbMaterial);
            this.tcMaterialChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMaterialChoose.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcMaterialChoose.Location = new System.Drawing.Point(0, 0);
            this.tcMaterialChoose.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcMaterialChoose.Name = "tcMaterialChoose";
            this.tcMaterialChoose.SelectedIndex = 0;
            this.tcMaterialChoose.Size = new System.Drawing.Size(267, 518);
            this.tcMaterialChoose.TabIndex = 5;
            // 
            // tbMaterial
            // 
            this.tbMaterial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbMaterial.Controls.Add(this.tvMaterial);
            this.tbMaterial.Location = new System.Drawing.Point(4, 25);
            this.tbMaterial.Name = "tbMaterial";
            this.tbMaterial.Padding = new System.Windows.Forms.Padding(3);
            this.tbMaterial.Size = new System.Drawing.Size(259, 489);
            this.tbMaterial.TabIndex = 0;
            this.tbMaterial.Text = "物料";
            // 
            // MaterialChooseNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(267, 518);
            this.Controls.Add(this.tcMaterialChoose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialChooseNavigate";
            this.TabText = "物料选择";
            this.Text = "物料选择";
            this.Load += new System.EventHandler(this.MaterialChooseNavigate_Load);
            this.tcMaterialChoose.ResumeLayout(false);
            this.tbMaterial.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvMaterial;
        private System.Windows.Forms.ImageList imgList;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private Controls.FlatTabControl tcMaterialChoose;
        private System.Windows.Forms.TabPage tbMaterial;
    }
}