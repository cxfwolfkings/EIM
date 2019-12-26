namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    partial class SourceFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SourceFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.tvSource = new System.Windows.Forms.TreeView();
            this.btnNonSource = new System.Windows.Forms.Button();
            this.btnOtherSource = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnParameter = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnUndo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetOtherSource = new System.Windows.Forms.Button();
            this.txtOtherSource = new System.Windows.Forms.TextBox();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMinimunSize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "来源:";
            // 
            // txtSource
            // 
            this.txtSource.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSource.Location = new System.Drawing.Point(52, 22);
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ReadOnly = true;
            this.txtSource.Size = new System.Drawing.Size(354, 20);
            this.txtSource.TabIndex = 1;
            this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSource_KeyDown);
            // 
            // tvSource
            // 
            this.tvSource.Location = new System.Drawing.Point(15, 52);
            this.tvSource.Name = "tvSource";
            this.tvSource.Size = new System.Drawing.Size(472, 261);
            this.tvSource.TabIndex = 2;
            this.tvSource.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvSource_NodeMouseClick);
            // 
            // btnNonSource
            // 
            this.btnNonSource.FlatAppearance.BorderSize = 0;
            this.btnNonSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNonSource.Location = new System.Drawing.Point(15, 319);
            this.btnNonSource.Name = "btnNonSource";
            this.btnNonSource.Size = new System.Drawing.Size(75, 24);
            this.btnNonSource.TabIndex = 3;
            this.btnNonSource.Text = "没有来源";
            this.btnNonSource.UseVisualStyleBackColor = true;
            this.btnNonSource.Click += new System.EventHandler(this.btnNonSource_Click);
            // 
            // btnOtherSource
            // 
            this.btnOtherSource.FlatAppearance.BorderSize = 0;
            this.btnOtherSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOtherSource.Location = new System.Drawing.Point(96, 319);
            this.btnOtherSource.Name = "btnOtherSource";
            this.btnOtherSource.Size = new System.Drawing.Size(75, 24);
            this.btnOtherSource.TabIndex = 4;
            this.btnOtherSource.Text = "其他来源...";
            this.btnOtherSource.UseVisualStyleBackColor = true;
            this.btnOtherSource.Click += new System.EventHandler(this.btnOtherSource_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(331, 319);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 24);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(412, 319);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnParameter
            // 
            this.btnParameter.FlatAppearance.BorderSize = 0;
            this.btnParameter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnParameter.Location = new System.Drawing.Point(412, 18);
            this.btnParameter.Name = "btnParameter";
            this.btnParameter.Size = new System.Drawing.Size(75, 24);
            this.btnParameter.TabIndex = 7;
            this.btnParameter.Text = "参数";
            this.btnParameter.UseVisualStyleBackColor = true;
            this.btnParameter.Click += new System.EventHandler(this.btnParameter_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSource);
            this.groupBox1.Controls.Add(this.btnParameter);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.tvSource);
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.btnNonSource);
            this.groupBox1.Controls.Add(this.btnOtherSource);
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 350);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnUndo);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnSetOtherSource);
            this.groupBox2.Controls.Add(this.txtOtherSource);
            this.groupBox2.Location = new System.Drawing.Point(14, 400);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 83);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他来源";
            // 
            // btnUndo
            // 
            this.btnUndo.FlatAppearance.BorderSize = 0;
            this.btnUndo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUndo.Location = new System.Drawing.Point(412, 18);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 24);
            this.btnUndo.TabIndex = 3;
            this.btnUndo.Text = "撤销";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(443, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "注：如果想设为数据表Table对应的Column1字段，可以这样设置Table\\Column1";
            // 
            // btnSetOtherSource
            // 
            this.btnSetOtherSource.FlatAppearance.BorderSize = 0;
            this.btnSetOtherSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetOtherSource.Location = new System.Drawing.Point(331, 18);
            this.btnSetOtherSource.Name = "btnSetOtherSource";
            this.btnSetOtherSource.Size = new System.Drawing.Size(75, 24);
            this.btnSetOtherSource.TabIndex = 1;
            this.btnSetOtherSource.Text = "设定";
            this.btnSetOtherSource.UseVisualStyleBackColor = true;
            this.btnSetOtherSource.Click += new System.EventHandler(this.btnSetOtherSource_Click);
            // 
            // txtOtherSource
            // 
            this.txtOtherSource.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtOtherSource.Location = new System.Drawing.Point(15, 22);
            this.txtOtherSource.Multiline = true;
            this.txtOtherSource.Name = "txtOtherSource";
            this.txtOtherSource.Size = new System.Drawing.Size(310, 20);
            this.txtOtherSource.TabIndex = 0;
            this.txtOtherSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSource_KeyDown);
            // 
            // pnTitle
            // 
            this.pnTitle.Controls.Add(this.label3);
            this.pnTitle.Controls.Add(this.pictureBox1);
            this.pnTitle.Controls.Add(this.btnMinimunSize);
            this.pnTitle.Controls.Add(this.btnClose);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(525, 28);
            this.pnTitle.TabIndex = 27;
            this.pnTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(26, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "来源";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(13, 13);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // btnMinimunSize
            // 
            this.btnMinimunSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimunSize.BackColor = System.Drawing.SystemColors.Control;
            this.btnMinimunSize.BackgroundImage = global::Kingdee.CAPP.Common.ResourceNotice.minimumsize;
            this.btnMinimunSize.FlatAppearance.BorderSize = 0;
            this.btnMinimunSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimunSize.Location = new System.Drawing.Point(468, 0);
            this.btnMinimunSize.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnMinimunSize.Name = "btnMinimunSize";
            this.btnMinimunSize.Size = new System.Drawing.Size(23, 23);
            this.btnMinimunSize.TabIndex = 2;
            this.btnMinimunSize.UseVisualStyleBackColor = false;
            this.btnMinimunSize.Click += new System.EventHandler(this.btnMinimunSize_Click);
            this.btnMinimunSize.MouseLeave += new System.EventHandler(this.btnMinimunSize_MouseLeave);
            this.btnMinimunSize.MouseHover += new System.EventHandler(this.btnMinimunSize_MouseHover);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::Kingdee.CAPP.Common.ResourceNotice.close;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(491, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // SourceFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(525, 394);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SourceFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "来源";
            this.Load += new System.EventHandler(this.SourceFrm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TreeView tvSource;
        private System.Windows.Forms.Button btnNonSource;
        private System.Windows.Forms.Button btnOtherSource;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnParameter;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetOtherSource;
        private System.Windows.Forms.TextBox txtOtherSource;
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnMinimunSize;
        private System.Windows.Forms.Button btnClose;
    }
}