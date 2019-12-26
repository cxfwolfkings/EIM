namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProductPropertyFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductPropertyFrm));
            this.pgProduct = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgProduct
            // 
            this.pgProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProduct.Location = new System.Drawing.Point(0, 0);
            this.pgProduct.Name = "pgProduct";
            this.pgProduct.Size = new System.Drawing.Size(284, 575);
            this.pgProduct.TabIndex = 0;
            // 
            // ProductPropertyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 575);
            this.Controls.Add(this.pgProduct);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductPropertyFrm";
            this.TabText = "产品属性";
            this.Text = "产品属性";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgProduct;
    }
}