namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    partial class CardDetailPropertyFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardDetailPropertyFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.ListBox();
            this.btnAddColumn = new System.Windows.Forms.Button();
            this.btnDeleteColumn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.comboAdvanceProperty = new System.Windows.Forms.ComboBox();
            this.lblAdvanceProperty = new System.Windows.Forms.Label();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMinimunSize = new System.Windows.Forms.Button();
            this.btnMaximumSize = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pgrdColumns = new System.Windows.Forms.PropertyGrid();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "明细列列表";
            // 
            // lbColumns
            // 
            this.lbColumns.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbColumns.FormattingEnabled = true;
            this.lbColumns.ItemHeight = 17;
            this.lbColumns.Location = new System.Drawing.Point(15, 50);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.Size = new System.Drawing.Size(192, 242);
            this.lbColumns.TabIndex = 1;
            this.lbColumns.SelectedValueChanged += new System.EventHandler(this.lbColumns_SelectedValueChanged);
            // 
            // btnAddColumn
            // 
            this.btnAddColumn.FlatAppearance.BorderSize = 0;
            this.btnAddColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddColumn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAddColumn.Location = new System.Drawing.Point(51, 294);
            this.btnAddColumn.Name = "btnAddColumn";
            this.btnAddColumn.Size = new System.Drawing.Size(75, 23);
            this.btnAddColumn.TabIndex = 4;
            this.btnAddColumn.Text = "增加列";
            this.btnAddColumn.UseVisualStyleBackColor = true;
            this.btnAddColumn.Click += new System.EventHandler(this.btnAddColumn_Click);
            // 
            // btnDeleteColumn
            // 
            this.btnDeleteColumn.FlatAppearance.BorderSize = 0;
            this.btnDeleteColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteColumn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDeleteColumn.Location = new System.Drawing.Point(132, 294);
            this.btnDeleteColumn.Name = "btnDeleteColumn";
            this.btnDeleteColumn.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteColumn.TabIndex = 5;
            this.btnDeleteColumn.Text = "删除列";
            this.btnDeleteColumn.UseVisualStyleBackColor = true;
            this.btnDeleteColumn.Click += new System.EventHandler(this.btnDeleteColumn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "列属性设置";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(413, 323);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(494, 323);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.BackgroundImage")));
            this.btnMoveDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveDown.FlatAppearance.BorderSize = 0;
            this.btnMoveDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveDown.Location = new System.Drawing.Point(214, 77);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(24, 19);
            this.btnMoveDown.TabIndex = 3;
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.BackgroundImage")));
            this.btnMoveUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMoveUp.FlatAppearance.BorderSize = 0;
            this.btnMoveUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoveUp.Location = new System.Drawing.Point(214, 50);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(24, 19);
            this.btnMoveUp.TabIndex = 2;
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // comboAdvanceProperty
            // 
            this.comboAdvanceProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAdvanceProperty.FormattingEnabled = true;
            this.comboAdvanceProperty.Location = new System.Drawing.Point(390, 50);
            this.comboAdvanceProperty.Name = "comboAdvanceProperty";
            this.comboAdvanceProperty.Size = new System.Drawing.Size(87, 24);
            this.comboAdvanceProperty.TabIndex = 30;
            this.comboAdvanceProperty.Visible = false;
            this.comboAdvanceProperty.SelectedIndexChanged += new System.EventHandler(this.comboAdvanceProperty_SelectedIndexChanged);
            // 
            // lblAdvanceProperty
            // 
            this.lblAdvanceProperty.AutoSize = true;
            this.lblAdvanceProperty.Location = new System.Drawing.Point(332, 55);
            this.lblAdvanceProperty.Name = "lblAdvanceProperty";
            this.lblAdvanceProperty.Size = new System.Drawing.Size(52, 16);
            this.lblAdvanceProperty.TabIndex = 31;
            this.lblAdvanceProperty.Text = "高级属性";
            this.lblAdvanceProperty.Visible = false;
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pnTitle.Controls.Add(this.label3);
            this.pnTitle.Controls.Add(this.lblTitle);
            this.pnTitle.Controls.Add(this.pictureBox1);
            this.pnTitle.Controls.Add(this.btnMinimunSize);
            this.pnTitle.Controls.Add(this.btnMaximumSize);
            this.pnTitle.Controls.Add(this.btnClose);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(586, 24);
            this.pnTitle.TabIndex = 32;
            this.pnTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(22, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "编辑明细列属性";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
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
            this.btnMinimunSize.BackgroundImage = global::Kingdee.CAPP.Common.ResourceNotice.minimum_d;
            this.btnMinimunSize.FlatAppearance.BorderSize = 0;
            this.btnMinimunSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimunSize.Location = new System.Drawing.Point(534, 0);
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
            this.btnMaximumSize.FlatAppearance.BorderSize = 0;
            this.btnMaximumSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximumSize.Location = new System.Drawing.Point(534, 0);
            this.btnMaximumSize.Name = "btnMaximumSize";
            this.btnMaximumSize.Size = new System.Drawing.Size(23, 23);
            this.btnMaximumSize.TabIndex = 1;
            this.btnMaximumSize.UseVisualStyleBackColor = true;
            this.btnMaximumSize.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackgroundImage = global::Kingdee.CAPP.Common.ResourceNotice.close_d;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(557, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
            // 
            // pgrdColumns
            // 
            this.pgrdColumns.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pgrdColumns.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pgrdColumns.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pgrdColumns.Location = new System.Drawing.Point(244, 50);
            this.pgrdColumns.Name = "pgrdColumns";
            this.pgrdColumns.Size = new System.Drawing.Size(325, 267);
            this.pgrdColumns.TabIndex = 6;
            this.pgrdColumns.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgrdColumns_PropertyValueChanged);
            // 
            // CardDetailPropertyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(586, 354);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.comboAdvanceProperty);
            this.Controls.Add(this.lblAdvanceProperty);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteColumn);
            this.Controls.Add(this.btnAddColumn);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.lbColumns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pgrdColumns);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CardDetailPropertyFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑明细列属性";
            this.Load += new System.EventHandler(this.CardDetailPropertyFrm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form_MouseDown);
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbColumns;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnAddColumn;
        private System.Windows.Forms.Button btnDeleteColumn;
        private System.Windows.Forms.PropertyGrid pgrdColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboAdvanceProperty;
        private System.Windows.Forms.Label lblAdvanceProperty;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnMinimunSize;
        private System.Windows.Forms.Button btnMaximumSize;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label3;
    }
}