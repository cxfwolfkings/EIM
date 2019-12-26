namespace Kingdee.CAPP.UI
{
    partial class ProductChooseNavigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductChooseNavigate));
            this.tvProduct = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.bwLoadTree = new System.ComponentModel.BackgroundWorker();
            this.tcProductChoose = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tbProduct = new System.Windows.Forms.TabPage();
            this.tcProductChoose.SuspendLayout();
            this.tbProduct.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvProduct
            // 
            this.tvProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProduct.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProduct.ImageIndex = 0;
            this.tvProduct.ImageList = this.imgList;
            this.tvProduct.Location = new System.Drawing.Point(3, 3);
            this.tvProduct.Name = "tvProduct";
            this.tvProduct.SelectedImageIndex = 0;
            this.tvProduct.Size = new System.Drawing.Size(274, 560);
            this.tvProduct.TabIndex = 1;
            this.tvProduct.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProduct_AfterCollapse);
            this.tvProduct.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProduct_AfterExpand);
            this.tvProduct.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProduct_MouseDown);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "folder_o");
            // 
            // bwLoadTree
            // 
            this.bwLoadTree.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwLoadTree_DoWork);
            // 
            // tcProductChoose
            // 
            this.tcProductChoose.Controls.Add(this.tbProduct);
            this.tcProductChoose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcProductChoose.Location = new System.Drawing.Point(0, 0);
            this.tcProductChoose.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProductChoose.Name = "tcProductChoose";
            this.tcProductChoose.SelectedIndex = 0;
            this.tcProductChoose.Size = new System.Drawing.Size(288, 595);
            this.tcProductChoose.TabIndex = 5;
            // 
            // tbProduct
            // 
            this.tbProduct.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tbProduct.Controls.Add(this.tvProduct);
            this.tbProduct.Location = new System.Drawing.Point(4, 25);
            this.tbProduct.Name = "tbProduct";
            this.tbProduct.Padding = new System.Windows.Forms.Padding(3);
            this.tbProduct.Size = new System.Drawing.Size(280, 566);
            this.tbProduct.TabIndex = 0;
            this.tbProduct.Text = "产品";
            // 
            // ProductChooseNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(288, 595);
            this.Controls.Add(this.tcProductChoose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductChooseNavigate";
            this.TabText = "产品选择";
            this.Text = "产品选择";
            this.Load += new System.EventHandler(this.ProductChooseNavigate_Load);
            this.tcProductChoose.ResumeLayout(false);
            this.tbProduct.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvProduct;
        private System.ComponentModel.BackgroundWorker bwLoadTree;
        private System.Windows.Forms.ImageList imgList;
        private Controls.FlatTabControl tcProductChoose;
        private System.Windows.Forms.TabPage tbProduct;
    }
}