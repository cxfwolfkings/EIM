namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class MediaFrm
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
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Size = new System.Drawing.Size(420, 303);
            // 
            // MediaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 327);
            this.Name = "MediaFrm";
            this.Text = "三维动画预览";
            this.Load += new System.EventHandler(this.MediaFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

    }
}