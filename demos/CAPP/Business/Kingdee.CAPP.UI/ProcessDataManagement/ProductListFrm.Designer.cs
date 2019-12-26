namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProductListFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductListFrm));
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCurrentVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdatePerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjectIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisginStateIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNewPbom = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvMaterial
            // 
            this.dgvMaterial.AllowUserToAddRows = false;
            this.dgvMaterial.AllowUserToDeleteRows = false;
            this.dgvMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMaterial.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterial.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterial.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colImage,
            this.colDImage,
            this.colCode,
            this.colName,
            this.colGroupId,
            this.colCurrentVer,
            this.colCreator,
            this.colCreateDate,
            this.colUpdatePerson,
            this.colUpdateDate,
            this.colStatus,
            this.colRemark,
            this.colObjectIconPath,
            this.colDisginStateIconPath});
            this.dgvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMaterial.Location = new System.Drawing.Point(0, 31);
            this.dgvMaterial.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.RowHeadersVisible = false;
            this.dgvMaterial.RowTemplate.Height = 23;
            this.dgvMaterial.Size = new System.Drawing.Size(927, 764);
            this.dgvMaterial.TabIndex = 1;
            this.dgvMaterial.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMaterial_DataBindingComplete);
            // 
            // colSelect
            // 
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            this.colSelect.Width = 25;
            // 
            // colImage
            // 
            this.colImage.HeaderText = "";
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            this.colImage.Width = 25;
            // 
            // colDImage
            // 
            this.colDImage.HeaderText = "";
            this.colDImage.Name = "colDImage";
            this.colDImage.ReadOnly = true;
            this.colDImage.Width = 25;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "code";
            this.colCode.HeaderText = "物料编码";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "物料名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colGroupId
            // 
            this.colGroupId.DataPropertyName = "groupid";
            this.colGroupId.HeaderText = "单号";
            this.colGroupId.Name = "colGroupId";
            this.colGroupId.ReadOnly = true;
            // 
            // colCurrentVer
            // 
            this.colCurrentVer.DataPropertyName = "currentver";
            this.colCurrentVer.HeaderText = "版本";
            this.colCurrentVer.Name = "colCurrentVer";
            this.colCurrentVer.ReadOnly = true;
            // 
            // colCreator
            // 
            this.colCreator.HeaderText = "创建人";
            this.colCreator.Name = "colCreator";
            this.colCreator.ReadOnly = true;
            // 
            // colCreateDate
            // 
            this.colCreateDate.DataPropertyName = "createdate";
            this.colCreateDate.HeaderText = "创建日期";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.ReadOnly = true;
            // 
            // colUpdatePerson
            // 
            this.colUpdatePerson.DataPropertyName = "updateperson";
            this.colUpdatePerson.HeaderText = "修改人";
            this.colUpdatePerson.Name = "colUpdatePerson";
            this.colUpdatePerson.ReadOnly = true;
            // 
            // colUpdateDate
            // 
            this.colUpdateDate.DataPropertyName = "updatedate";
            this.colUpdateDate.HeaderText = "修改日期";
            this.colUpdateDate.Name = "colUpdateDate";
            this.colUpdateDate.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colRemark
            // 
            this.colRemark.DataPropertyName = "remark";
            this.colRemark.HeaderText = "备注";
            this.colRemark.Name = "colRemark";
            this.colRemark.ReadOnly = true;
            // 
            // colObjectIconPath
            // 
            this.colObjectIconPath.DataPropertyName = "objecticonpath";
            this.colObjectIconPath.HeaderText = "ObjectIconPath";
            this.colObjectIconPath.Name = "colObjectIconPath";
            this.colObjectIconPath.ReadOnly = true;
            this.colObjectIconPath.Visible = false;
            // 
            // colDisginStateIconPath
            // 
            this.colDisginStateIconPath.DataPropertyName = "disginstateiconpath";
            this.colDisginStateIconPath.HeaderText = "DesignStateIconPath";
            this.colDisginStateIconPath.Name = "colDisginStateIconPath";
            this.colDisginStateIconPath.ReadOnly = true;
            this.colDisginStateIconPath.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnNewPbom);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 31);
            this.panel1.TabIndex = 2;
            // 
            // btnNewPbom
            // 
            this.btnNewPbom.FlatAppearance.BorderSize = 0;
            this.btnNewPbom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewPbom.Location = new System.Drawing.Point(105, 4);
            this.btnNewPbom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNewPbom.Name = "btnNewPbom";
            this.btnNewPbom.Size = new System.Drawing.Size(87, 23);
            this.btnNewPbom.TabIndex = 1;
            this.btnNewPbom.Text = "新建PBOM";
            this.btnNewPbom.UseVisualStyleBackColor = true;
            this.btnNewPbom.Visible = false;
            this.btnNewPbom.Click += new System.EventHandler(this.btnNewPbom_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(12, 4);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(87, 23);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            // 
            // ProductListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 795);
            this.Controls.Add(this.dgvMaterial);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProductListFrm";
            this.TabText = "产品物料列表";
            this.Text = "产品物料列表";
            this.Load += new System.EventHandler(this.ProductListFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewImageColumn colDImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCurrentVer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreator;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdatePerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUpdateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRemark;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectIconPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisginStateIconPath;
        private System.Windows.Forms.Button btnNewPbom;
    }
}