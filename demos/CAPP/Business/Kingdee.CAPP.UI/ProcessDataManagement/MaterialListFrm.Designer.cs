namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class MaterialListFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialListFrm));
            this.dgvMaterial = new System.Windows.Forms.DataGridView();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVercode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIntproductMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsVirtualDesign = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaperCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjectIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDisginStateIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTechnicsStateIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategoryTypeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBaseId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboField = new System.Windows.Forms.ComboBox();
            this.txtFieldValue = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
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
            this.colDrawnumber,
            this.colSpec,
            this.colVercode,
            this.colIntproductMode,
            this.colCreateDate,
            this.colCount,
            this.colProductName,
            this.colCategoryName,
            this.colIsVirtualDesign,
            this.colTypeName,
            this.colPaperCount,
            this.colMemberSpec,
            this.colObjectIconPath,
            this.colDisginStateIconPath,
            this.colTechnicsStateIconPath,
            this.colCategoryTypeId,
            this.colBaseId});
            this.dgvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterial.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMaterial.Location = new System.Drawing.Point(0, 38);
            this.dgvMaterial.Name = "dgvMaterial";
            this.dgvMaterial.RowHeadersVisible = false;
            this.dgvMaterial.RowTemplate.Height = 23;
            this.dgvMaterial.Size = new System.Drawing.Size(610, 389);
            this.dgvMaterial.TabIndex = 0;
            this.dgvMaterial.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterial_CellValueChanged);
            this.dgvMaterial.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvMaterial_CurrentCellDirtyStateChanged);
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
            this.colDImage.Width = 40;
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
            // colDrawnumber
            // 
            this.colDrawnumber.DataPropertyName = "drawnumber";
            this.colDrawnumber.HeaderText = "图号";
            this.colDrawnumber.Name = "colDrawnumber";
            this.colDrawnumber.ReadOnly = true;
            // 
            // colSpec
            // 
            this.colSpec.DataPropertyName = "spec";
            this.colSpec.HeaderText = "规格";
            this.colSpec.Name = "colSpec";
            this.colSpec.ReadOnly = true;
            // 
            // colVercode
            // 
            this.colVercode.DataPropertyName = "vercode";
            this.colVercode.HeaderText = "版本号";
            this.colVercode.Name = "colVercode";
            this.colVercode.ReadOnly = true;
            // 
            // colIntproductMode
            // 
            this.colIntproductMode.DataPropertyName = "intproductmode";
            this.colIntproductMode.HeaderText = "生产方式";
            this.colIntproductMode.Name = "colIntproductMode";
            this.colIntproductMode.ReadOnly = true;
            // 
            // colCreateDate
            // 
            this.colCreateDate.DataPropertyName = "createdate";
            this.colCreateDate.HeaderText = "创建时间";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.ReadOnly = true;
            // 
            // colCount
            // 
            this.colCount.DataPropertyName = "count";
            this.colCount.HeaderText = "组件数量";
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            // 
            // colProductName
            // 
            this.colProductName.DataPropertyName = "productname";
            this.colProductName.HeaderText = "归属产品";
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colCategoryName
            // 
            this.colCategoryName.DataPropertyName = "categoryname";
            this.colCategoryName.HeaderText = "业务类型";
            this.colCategoryName.Name = "colCategoryName";
            this.colCategoryName.ReadOnly = true;
            // 
            // colIsVirtualDesign
            // 
            this.colIsVirtualDesign.DataPropertyName = "isvirtualdesign";
            this.colIsVirtualDesign.HeaderText = "设计虚件";
            this.colIsVirtualDesign.Name = "colIsVirtualDesign";
            this.colIsVirtualDesign.ReadOnly = true;
            // 
            // colTypeName
            // 
            this.colTypeName.DataPropertyName = "typename";
            this.colTypeName.HeaderText = "通用类别";
            this.colTypeName.Name = "colTypeName";
            this.colTypeName.ReadOnly = true;
            // 
            // colPaperCount
            // 
            this.colPaperCount.DataPropertyName = "papercount";
            this.colPaperCount.HeaderText = "图纸张数";
            this.colPaperCount.Name = "colPaperCount";
            this.colPaperCount.ReadOnly = true;
            // 
            // colMemberSpec
            // 
            this.colMemberSpec.DataPropertyName = "memberspec";
            this.colMemberSpec.HeaderText = "成员规格";
            this.colMemberSpec.Name = "colMemberSpec";
            this.colMemberSpec.ReadOnly = true;
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
            // colTechnicsStateIconPath
            // 
            this.colTechnicsStateIconPath.DataPropertyName = "technicsstateiconpath";
            this.colTechnicsStateIconPath.HeaderText = "TechnicsStateIconPath";
            this.colTechnicsStateIconPath.Name = "colTechnicsStateIconPath";
            this.colTechnicsStateIconPath.ReadOnly = true;
            this.colTechnicsStateIconPath.Visible = false;
            // 
            // colCategoryTypeId
            // 
            this.colCategoryTypeId.DataPropertyName = "categoryid_typeid";
            this.colCategoryTypeId.HeaderText = "";
            this.colCategoryTypeId.Name = "colCategoryTypeId";
            this.colCategoryTypeId.Visible = false;
            // 
            // colBaseId
            // 
            this.colBaseId.DataPropertyName = "baseid";
            this.colBaseId.HeaderText = "";
            this.colBaseId.Name = "colBaseId";
            this.colBaseId.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboField);
            this.panel1.Controls.Add(this.txtFieldValue);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnConfirm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 38);
            this.panel1.TabIndex = 1;
            // 
            // comboField
            // 
            this.comboField.BackColor = System.Drawing.SystemColors.Control;
            this.comboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboField.FormattingEnabled = true;
            this.comboField.Location = new System.Drawing.Point(12, 7);
            this.comboField.Name = "comboField";
            this.comboField.Size = new System.Drawing.Size(90, 25);
            this.comboField.TabIndex = 2;
            // 
            // txtFieldValue
            // 
            this.txtFieldValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFieldValue.Location = new System.Drawing.Point(108, 8);
            this.txtFieldValue.Multiline = true;
            this.txtFieldValue.Name = "txtFieldValue";
            this.txtFieldValue.Size = new System.Drawing.Size(100, 22);
            this.txtFieldValue.TabIndex = 1;
            this.txtFieldValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFieldValue_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(214, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(49, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(269, 8);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(49, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // MaterialListFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 427);
            this.Controls.Add(this.dgvMaterial);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MaterialListFrm";
            this.TabText = "物料列表";
            this.Text = "物料列表";
            this.Load += new System.EventHandler(this.MaterialListFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterial)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMaterial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox comboField;
        private System.Windows.Forms.TextBox txtFieldValue;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewImageColumn colDImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVercode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntproductMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsVirtualDesign;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaperCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectIconPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDisginStateIconPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTechnicsStateIconPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategoryTypeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBaseId;
        private System.Windows.Forms.Button btnSearch;
    }
}