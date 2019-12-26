namespace Kingdee.CAPP.UI
{
    partial class BaseSkinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSkinForm));
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMinimunSize = new System.Windows.Forms.Button();
            this.btnMaximumSize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnBody = new System.Windows.Forms.Panel();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pnTitle.Controls.Add(this.lblTitle);
            this.pnTitle.Controls.Add(this.pictureBox1);
            this.pnTitle.Controls.Add(this.btnMinimunSize);
            this.pnTitle.Controls.Add(this.btnMaximumSize);
            this.pnTitle.Controls.Add(this.btnClose);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(364, 24);
            this.pnTitle.TabIndex = 12;
            this.pnTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTitle.Location = new System.Drawing.Point(22, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 17);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(13, 13);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnMinimunSize
            // 
            this.btnMinimunSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimunSize.BackgroundImage = global::Kingdee.CAPP.UI.Properties.Resources.minimum_d;
            this.btnMinimunSize.FlatAppearance.BorderSize = 0;
            this.btnMinimunSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimunSize.Location = new System.Drawing.Point(312, 0);
            this.btnMinimunSize.Name = "btnMinimunSize";
            this.btnMinimunSize.Size = new System.Drawing.Size(23, 23);
            this.btnMinimunSize.TabIndex = 2;
            this.btnMinimunSize.UseVisualStyleBackColor = true;
            this.btnMinimunSize.Click += new System.EventHandler(this.btnMinimunSize_Click);
            this.btnMinimunSize.MouseLeave += new System.EventHandler(this.btnMinimunSize_MouseLeave);
            this.btnMinimunSize.MouseHover += new System.EventHandler(this.btnMinimunSize_MouseHover);
            // 
            // btnMaximumSize
            // 
            this.btnMaximumSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximumSize.BackgroundImage = global::Kingdee.CAPP.UI.Properties.Resources.max_d;
            this.btnMaximumSize.FlatAppearance.BorderSize = 0;
            this.btnMaximumSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximumSize.Location = new System.Drawing.Point(312, 0);
            this.btnMaximumSize.Name = "btnMaximumSize";
            this.btnMaximumSize.Size = new System.Drawing.Size(23, 23);
            this.btnMaximumSize.TabIndex = 1;
            this.btnMaximumSize.UseVisualStyleBackColor = true;
            this.btnMaximumSize.Visible = false;
            this.btnMaximumSize.Click += new System.EventHandler(this.btnMaximumSize_Click);
            this.btnMaximumSize.MouseLeave += new System.EventHandler(this.btnMaximumSize_MouseLeave);
            this.btnMaximumSize.MouseHover += new System.EventHandler(this.btnMaximumSize_MouseHover);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::Kingdee.CAPP.UI.Properties.Resources.close_d;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(335, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // pnBody
            // 
            this.pnBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnBody.Location = new System.Drawing.Point(0, 24);
            this.pnBody.Name = "pnBody";
            this.pnBody.Size = new System.Drawing.Size(364, 186);
            this.pnBody.TabIndex = 13;
            // 
            // BaseSkinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(364, 210);
            this.Controls.Add(this.pnBody);
            this.Controls.Add(this.pnTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BaseSkinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BaseSkinForm";
            this.Load += new System.EventHandler(this.BaseSkinForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnMinimunSize;
        private System.Windows.Forms.Button btnMaximumSize;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTitle;
        protected System.Windows.Forms.Panel pnBody;
    }
}