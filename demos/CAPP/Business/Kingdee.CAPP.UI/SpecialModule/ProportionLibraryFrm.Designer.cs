namespace Kingdee.CAPP.UI.SpecialModule
{
    partial class ProportionLibraryFrm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvMaterialQuota = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMeterWeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMeterWeight = new System.Windows.Forms.TextBox();
            this.btnModify = new System.Windows.Forms.Button();
            this.pnBody.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialQuota)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnModify);
            this.pnBody.Controls.Add(this.txtMeterWeight);
            this.pnBody.Controls.Add(this.label1);
            this.pnBody.Controls.Add(this.panel2);
            this.pnBody.Controls.Add(this.panel1);
            this.pnBody.Size = new System.Drawing.Size(449, 309);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 34);
            this.panel1.TabIndex = 0;
            // 
            // comboType
            // 
            this.comboType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboType.FormattingEnabled = true;
            this.comboType.Items.AddRange(new object[] {
            "业务类型",
            "对应物料"});
            this.comboType.Location = new System.Drawing.Point(12, 6);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(121, 20);
            this.comboType.TabIndex = 0;
            this.comboType.SelectedIndexChanged += new System.EventHandler(this.comboType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvMaterialQuota);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(449, 234);
            this.panel2.TabIndex = 1;
            // 
            // dgvMaterialQuota
            // 
            this.dgvMaterialQuota.AllowUserToAddRows = false;
            this.dgvMaterialQuota.AllowUserToDeleteRows = false;
            this.dgvMaterialQuota.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterialQuota.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMaterialQuota.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMaterialQuota.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialQuota.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.colCode,
            this.colName,
            this.colMeterWeight,
            this.colCategoryName});
            this.dgvMaterialQuota.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterialQuota.Location = new System.Drawing.Point(3, 3);
            this.dgvMaterialQuota.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvMaterialQuota.MultiSelect = false;
            this.dgvMaterialQuota.Name = "dgvMaterialQuota";
            this.dgvMaterialQuota.ReadOnly = true;
            this.dgvMaterialQuota.RowTemplate.Height = 23;
            this.dgvMaterialQuota.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaterialQuota.Size = new System.Drawing.Size(443, 228);
            this.dgvMaterialQuota.TabIndex = 2;
            this.dgvMaterialQuota.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterialQuota_CellClick);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "Code";
            this.colCode.HeaderText = "编码";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colMeterWeight
            // 
            this.colMeterWeight.DataPropertyName = "MeterWeight";
            this.colMeterWeight.HeaderText = "米重量";
            this.colMeterWeight.Name = "colMeterWeight";
            this.colMeterWeight.ReadOnly = true;
            // 
            // colCategoryName
            // 
            this.colCategoryName.DataPropertyName = "CategoryName";
            this.colCategoryName.HeaderText = "业务类型";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.ReadOnly = true;
            this.colCategoryName.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "米重量：";
            // 
            // txtMeterWeight
            // 
            this.txtMeterWeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMeterWeight.Location = new System.Drawing.Point(71, 275);
            this.txtMeterWeight.Multiline = true;
            this.txtMeterWeight.Name = "txtMeterWeight";
            this.txtMeterWeight.Size = new System.Drawing.Size(147, 21);
            this.txtMeterWeight.TabIndex = 3;
            this.txtMeterWeight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMeterWeight_KeyDown);
            this.txtMeterWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMeterWeight_KeyPress);
            // 
            // btnModify
            // 
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Location = new System.Drawing.Point(224, 274);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(55, 23);
            this.btnModify.TabIndex = 4;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // ProportionLibraryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 331);
            this.Name = "ProportionLibraryFrm";
            this.Text = "米重量库";
            this.Load += new System.EventHandler(this.ProportionLibraryFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialQuota)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvMaterialQuota;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.TextBox txtMeterWeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMeterWeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
    }
}