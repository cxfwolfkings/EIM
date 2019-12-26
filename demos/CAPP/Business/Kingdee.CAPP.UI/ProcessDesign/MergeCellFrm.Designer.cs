namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class MergeCellFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeCellFrm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numRows = new System.Windows.Forms.NumericUpDown();
            this.numColumns = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckSameHeight = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckSameWidth = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.colColumnNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRows = new System.Windows.Forms.DataGridView();
            this.colRowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotalWidth = new System.Windows.Forms.Label();
            this.lblTotalHeight = new System.Windows.Forms.Label();
            this.pnBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.groupBox2);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Controls.Add(this.label3);
            this.pnBody.Controls.Add(this.ckSameWidth);
            this.pnBody.Controls.Add(this.ckSameHeight);
            this.pnBody.Controls.Add(this.label4);
            this.pnBody.Size = new System.Drawing.Size(362, 378);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(268, 340);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(182, 340);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(80, 25);
            this.btnConfirm.TabIndex = 8;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "行数:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(189, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "列数:";
            // 
            // numRows
            // 
            this.numRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numRows.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numRows.Location = new System.Drawing.Point(51, 18);
            this.numRows.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numRows.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.Name = "numRows";
            this.numRows.Size = new System.Drawing.Size(80, 19);
            this.numRows.TabIndex = 1;
            this.numRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRows.ValueChanged += new System.EventHandler(this.numRows_ValueChanged);
            this.numRows.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numRows_KeyUp);
            // 
            // numColumns
            // 
            this.numColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numColumns.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.numColumns.Location = new System.Drawing.Point(225, 18);
            this.numColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numColumns.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.numColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.Name = "numColumns";
            this.numColumns.Size = new System.Drawing.Size(80, 19);
            this.numColumns.TabIndex = 2;
            this.numColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numColumns.ValueChanged += new System.EventHandler(this.numColumns_ValueChanged);
            this.numColumns.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numColumns_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numColumns);
            this.groupBox1.Controls.Add(this.numRows);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(16, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(332, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "行参数:";
            // 
            // ckSameHeight
            // 
            this.ckSameHeight.AutoSize = true;
            this.ckSameHeight.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ckSameHeight.FlatAppearance.BorderSize = 0;
            this.ckSameHeight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckSameHeight.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckSameHeight.Location = new System.Drawing.Point(75, 52);
            this.ckSameHeight.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckSameHeight.Name = "ckSameHeight";
            this.ckSameHeight.Size = new System.Drawing.Size(72, 21);
            this.ckSameHeight.TabIndex = 3;
            this.ckSameHeight.Text = "每行等高";
            this.ckSameHeight.UseVisualStyleBackColor = true;
            this.ckSameHeight.CheckedChanged += new System.EventHandler(this.ckSameHeight_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(191, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "列参数:";
            // 
            // ckSameWidth
            // 
            this.ckSameWidth.AutoSize = true;
            this.ckSameWidth.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.ckSameWidth.FlatAppearance.BorderSize = 0;
            this.ckSameWidth.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckSameWidth.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckSameWidth.Location = new System.Drawing.Point(245, 53);
            this.ckSameWidth.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ckSameWidth.Name = "ckSameWidth";
            this.ckSameWidth.Size = new System.Drawing.Size(72, 21);
            this.ckSameWidth.TabIndex = 4;
            this.ckSameWidth.Text = "每列等宽";
            this.ckSameWidth.UseVisualStyleBackColor = true;
            this.ckSameWidth.CheckedChanged += new System.EventHandler(this.ckSameWidth_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvColumns);
            this.groupBox2.Controls.Add(this.dgvRows);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(16, 81);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.groupBox2.Size = new System.Drawing.Size(332, 251);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "总高度：{0}px   总宽度：{1}px";
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.BackgroundColor = System.Drawing.Color.White;
            this.dgvColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvColumns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColumnNumber,
            this.colWidth});
            this.dgvColumns.Location = new System.Drawing.Point(169, 29);
            this.dgvColumns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowHeadersVisible = false;
            this.dgvColumns.RowTemplate.Height = 23;
            this.dgvColumns.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvColumns.Size = new System.Drawing.Size(156, 212);
            this.dgvColumns.TabIndex = 7;
            // 
            // colColumnNumber
            // 
            this.colColumnNumber.HeaderText = "列项";
            this.colColumnNumber.Name = "colColumnNumber";
            this.colColumnNumber.Width = 60;
            // 
            // colWidth
            // 
            this.colWidth.HeaderText = "宽度";
            this.colWidth.Name = "colWidth";
            // 
            // dgvRows
            // 
            this.dgvRows.AllowUserToAddRows = false;
            this.dgvRows.BackgroundColor = System.Drawing.Color.White;
            this.dgvRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRows.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRowNumber,
            this.colHeight});
            this.dgvRows.Location = new System.Drawing.Point(9, 29);
            this.dgvRows.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvRows.Name = "dgvRows";
            this.dgvRows.RowHeadersVisible = false;
            this.dgvRows.RowTemplate.Height = 23;
            this.dgvRows.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRows.Size = new System.Drawing.Size(156, 212);
            this.dgvRows.TabIndex = 6;
            // 
            // colRowNumber
            // 
            this.colRowNumber.HeaderText = "行项";
            this.colRowNumber.Name = "colRowNumber";
            this.colRowNumber.Width = 60;
            // 
            // colHeight
            // 
            this.colHeight.HeaderText = "高度";
            this.colHeight.Name = "colHeight";
            // 
            // lblTotalWidth
            // 
            this.lblTotalWidth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalWidth.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalWidth.Location = new System.Drawing.Point(274, 515);
            this.lblTotalWidth.Name = "lblTotalWidth";
            this.lblTotalWidth.Size = new System.Drawing.Size(50, 24);
            this.lblTotalWidth.TabIndex = 17;
            // 
            // lblTotalHeight
            // 
            this.lblTotalHeight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalHeight.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHeight.Location = new System.Drawing.Point(201, 515);
            this.lblTotalHeight.Name = "lblTotalHeight";
            this.lblTotalHeight.Size = new System.Drawing.Size(50, 24);
            this.lblTotalHeight.TabIndex = 15;
            // 
            // MergeCellFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 400);
            this.Controls.Add(this.lblTotalWidth);
            this.Controls.Add(this.lblTotalHeight);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MergeCellFrm";
            this.Text = "单元格行与列";
            this.Load += new System.EventHandler(this.MergeCellFrm_Load);
            this.Controls.SetChildIndex(this.lblTotalHeight, 0);
            this.Controls.SetChildIndex(this.lblTotalWidth, 0);
            this.Controls.SetChildIndex(this.pnBody, 0);
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numColumns)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRows;
        private System.Windows.Forms.NumericUpDown numColumns;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckSameHeight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox ckSameWidth;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.DataGridView dgvRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumnNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWidth;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRowNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHeight;
        private System.Windows.Forms.Label lblTotalWidth;
        private System.Windows.Forms.Label lblTotalHeight;
    }
}