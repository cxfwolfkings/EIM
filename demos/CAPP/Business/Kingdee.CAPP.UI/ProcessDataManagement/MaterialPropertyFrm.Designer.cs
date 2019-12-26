namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class MaterialPropertyFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialPropertyFrm));
            this.pgMaterial = new System.Windows.Forms.PropertyGrid();
            this.tcMaterialProperty = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tpProperty = new System.Windows.Forms.TabPage();
            this.tpObject = new System.Windows.Forms.TabPage();
            this.dgvMaterialObject = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colDImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.colCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDrawnumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIntproductMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjectIconPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObjOption = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCheckOutState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCopyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcMaterialProperty.SuspendLayout();
            this.tpProperty.SuspendLayout();
            this.tpObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialObject)).BeginInit();
            this.SuspendLayout();
            // 
            // pgMaterial
            // 
            this.pgMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgMaterial.Location = new System.Drawing.Point(3, 3);
            this.pgMaterial.Name = "pgMaterial";
            this.pgMaterial.Size = new System.Drawing.Size(871, 477);
            this.pgMaterial.TabIndex = 0;
            // 
            // tcMaterialProperty
            // 
            this.tcMaterialProperty.Controls.Add(this.tpProperty);
            this.tcMaterialProperty.Controls.Add(this.tpObject);
            this.tcMaterialProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMaterialProperty.Location = new System.Drawing.Point(0, 0);
            this.tcMaterialProperty.myBackColor = System.Drawing.SystemColors.Control;
            this.tcMaterialProperty.Name = "tcMaterialProperty";
            this.tcMaterialProperty.SelectedIndex = 0;
            this.tcMaterialProperty.Size = new System.Drawing.Size(885, 512);
            this.tcMaterialProperty.TabIndex = 1;
            // 
            // tpProperty
            // 
            this.tpProperty.Controls.Add(this.pgMaterial);
            this.tpProperty.Location = new System.Drawing.Point(4, 25);
            this.tpProperty.Name = "tpProperty";
            this.tpProperty.Padding = new System.Windows.Forms.Padding(3);
            this.tpProperty.Size = new System.Drawing.Size(877, 483);
            this.tpProperty.TabIndex = 0;
            this.tpProperty.Text = "基本信息";
            this.tpProperty.UseVisualStyleBackColor = true;
            // 
            // tpObject
            // 
            this.tpObject.Controls.Add(this.dgvMaterialObject);
            this.tpObject.Location = new System.Drawing.Point(4, 25);
            this.tpObject.Name = "tpObject";
            this.tpObject.Padding = new System.Windows.Forms.Padding(3);
            this.tpObject.Size = new System.Drawing.Size(877, 483);
            this.tpObject.TabIndex = 1;
            this.tpObject.Text = "相关对象";
            this.tpObject.UseVisualStyleBackColor = true;
            // 
            // dgvMaterialObject
            // 
            this.dgvMaterialObject.AllowUserToAddRows = false;
            this.dgvMaterialObject.AllowUserToDeleteRows = false;
            this.dgvMaterialObject.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaterialObject.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMaterialObject.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvMaterialObject.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaterialObject.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSelect,
            this.colImage,
            this.colDImage,
            this.colCode,
            this.colName,
            this.colDrawnumber,
            this.colSpec,
            this.colIntproductMode,
            this.colCreateDate,
            this.colCount,
            this.colObjectIconPath,
            this.colObjOption,
            this.colState,
            this.colCheckOutState,
            this.colCopyId});
            this.dgvMaterialObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaterialObject.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvMaterialObject.Location = new System.Drawing.Point(3, 3);
            this.dgvMaterialObject.Name = "dgvMaterialObject";
            this.dgvMaterialObject.RowHeadersVisible = false;
            this.dgvMaterialObject.RowTemplate.Height = 23;
            this.dgvMaterialObject.Size = new System.Drawing.Size(871, 477);
            this.dgvMaterialObject.TabIndex = 1;
            this.dgvMaterialObject.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterialObject_CellContentClick);
            this.dgvMaterialObject.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMaterialObject_CellMouseEnter);
            this.dgvMaterialObject.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvMaterialObject_DataBindingComplete);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ObjectOption";
            this.dataGridViewTextBoxColumn1.HeaderText = "对象类型";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 87;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn2.HeaderText = "对象名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 87;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn3.HeaderText = "对象编码";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 87;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "VerCode";
            this.dataGridViewTextBoxColumn4.HeaderText = "版本号";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 87;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "CategoryName";
            this.dataGridViewTextBoxColumn5.HeaderText = "业务类型";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 87;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "RelationType";
            this.dataGridViewTextBoxColumn6.HeaderText = "关联类型";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 87;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "OriginalMode";
            this.dataGridViewTextBoxColumn7.HeaderText = "创建方式";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 87;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "objecticonpath";
            this.dataGridViewTextBoxColumn8.HeaderText = "ObjectIconPath";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "ObjOption";
            this.dataGridViewTextBoxColumn9.HeaderText = "ObjOption";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Visible = false;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "State";
            this.dataGridViewTextBoxColumn10.HeaderText = "State";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Visible = false;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "CheckOutState";
            this.dataGridViewTextBoxColumn11.HeaderText = "CheckOutState";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Visible = false;
            // 
            // colSelect
            // 
            this.colSelect.FillWeight = 20F;
            this.colSelect.HeaderText = "";
            this.colSelect.Name = "colSelect";
            // 
            // colImage
            // 
            this.colImage.FillWeight = 20F;
            this.colImage.HeaderText = "";
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            // 
            // colDImage
            // 
            this.colDImage.FillWeight = 20F;
            this.colDImage.HeaderText = "";
            this.colDImage.Name = "colDImage";
            this.colDImage.ReadOnly = true;
            // 
            // colCode
            // 
            this.colCode.DataPropertyName = "ObjectOption";
            this.colCode.FillWeight = 82.91032F;
            this.colCode.HeaderText = "对象类型";
            this.colCode.Name = "colCode";
            this.colCode.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.FillWeight = 82.91032F;
            this.colName.HeaderText = "对象名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDrawnumber
            // 
            this.colDrawnumber.DataPropertyName = "Code";
            this.colDrawnumber.FillWeight = 82.91032F;
            this.colDrawnumber.HeaderText = "对象编码";
            this.colDrawnumber.Name = "colDrawnumber";
            this.colDrawnumber.ReadOnly = true;
            // 
            // colSpec
            // 
            this.colSpec.DataPropertyName = "VerCode";
            this.colSpec.FillWeight = 82.91032F;
            this.colSpec.HeaderText = "版本号";
            this.colSpec.Name = "colSpec";
            this.colSpec.ReadOnly = true;
            // 
            // colIntproductMode
            // 
            this.colIntproductMode.DataPropertyName = "CategoryName";
            this.colIntproductMode.FillWeight = 82.91032F;
            this.colIntproductMode.HeaderText = "业务类型";
            this.colIntproductMode.Name = "colIntproductMode";
            this.colIntproductMode.ReadOnly = true;
            // 
            // colCreateDate
            // 
            this.colCreateDate.DataPropertyName = "RelationType";
            this.colCreateDate.FillWeight = 82.91032F;
            this.colCreateDate.HeaderText = "关联类型";
            this.colCreateDate.Name = "colCreateDate";
            this.colCreateDate.ReadOnly = true;
            // 
            // colCount
            // 
            this.colCount.DataPropertyName = "OriginalMode";
            this.colCount.FillWeight = 82.91032F;
            this.colCount.HeaderText = "创建方式";
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            // 
            // colObjectIconPath
            // 
            this.colObjectIconPath.DataPropertyName = "objecticonpath";
            this.colObjectIconPath.HeaderText = "ObjectIconPath";
            this.colObjectIconPath.Name = "colObjectIconPath";
            this.colObjectIconPath.ReadOnly = true;
            this.colObjectIconPath.Visible = false;
            // 
            // colObjOption
            // 
            this.colObjOption.DataPropertyName = "ObjOption";
            this.colObjOption.HeaderText = "ObjOption";
            this.colObjOption.Name = "colObjOption";
            this.colObjOption.Visible = false;
            // 
            // colState
            // 
            this.colState.DataPropertyName = "State";
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.Visible = false;
            // 
            // colCheckOutState
            // 
            this.colCheckOutState.DataPropertyName = "CheckOutState";
            this.colCheckOutState.HeaderText = "CheckOutState";
            this.colCheckOutState.Name = "colCheckOutState";
            this.colCheckOutState.Visible = false;
            // 
            // colCopyId
            // 
            this.colCopyId.DataPropertyName = "CopyId";
            this.colCopyId.HeaderText = "CopyId";
            this.colCopyId.Name = "colCopyId";
            this.colCopyId.Visible = false;
            // 
            // MaterialPropertyFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 512);
            this.Controls.Add(this.tcMaterialProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialPropertyFrm";
            this.TabText = "物料属性";
            this.Text = "物料属性";
            this.Load += new System.EventHandler(this.MaterialPropertyFrm_Load);
            this.tcMaterialProperty.ResumeLayout(false);
            this.tpProperty.ResumeLayout(false);
            this.tpObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaterialObject)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgMaterial;
        private Controls.FlatTabControl tcMaterialProperty;
        private System.Windows.Forms.TabPage tpProperty;
        private System.Windows.Forms.TabPage tpObject;
        private System.Windows.Forms.DataGridView dgvMaterialObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSelect;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewImageColumn colDImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDrawnumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIntproductMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjectIconPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObjOption;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCheckOutState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCopyId;
    }
}