namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class SelectProcessCardModuleFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectProcessCardModuleFrm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProcessCardName = new System.Windows.Forms.TextBox();
            this.lblProcessCard = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnQuery = new System.Windows.Forms.Button();
            this.gbProcessCardModule = new System.Windows.Forms.GroupBox();
            this.dgvProcessCardModule = new System.Windows.Forms.DataGridView();
            this.cbxProcessPlanning = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcbProcessCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbProcessCardModule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessCardModule)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.gbProcessCardModule);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Size = new System.Drawing.Size(547, 309);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(452, 13);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(51, 21);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.txtProcessCardName);
            this.groupBox1.Controls.Add(this.lblProcessCard);
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 41);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            // 
            // txtProcessCardName
            // 
            this.txtProcessCardName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcessCardName.Location = new System.Drawing.Point(121, 14);
            this.txtProcessCardName.MaxLength = 50;
            this.txtProcessCardName.Multiline = true;
            this.txtProcessCardName.Name = "txtProcessCardName";
            this.txtProcessCardName.Size = new System.Drawing.Size(210, 18);
            this.txtProcessCardName.TabIndex = 5;
            this.txtProcessCardName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessCardName_KeyDown);
            // 
            // lblProcessCard
            // 
            this.lblProcessCard.AutoSize = true;
            this.lblProcessCard.Location = new System.Drawing.Point(4, 16);
            this.lblProcessCard.Name = "lblProcessCard";
            this.lblProcessCard.Size = new System.Drawing.Size(116, 17);
            this.lblProcessCard.TabIndex = 4;
            this.lblProcessCard.Text = "工艺卡片模板名称：";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(395, 14);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(51, 21);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnQuery
            // 
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Location = new System.Drawing.Point(337, 14);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(52, 21);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // gbProcessCardModule
            // 
            this.gbProcessCardModule.Controls.Add(this.dgvProcessCardModule);
            this.gbProcessCardModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbProcessCardModule.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProcessCardModule.Location = new System.Drawing.Point(12, 46);
            this.gbProcessCardModule.Name = "gbProcessCardModule";
            this.gbProcessCardModule.Size = new System.Drawing.Size(523, 252);
            this.gbProcessCardModule.TabIndex = 8;
            this.gbProcessCardModule.TabStop = false;
            this.gbProcessCardModule.Text = "选择工艺卡片模板";
            // 
            // dgvProcessCardModule
            // 
            this.dgvProcessCardModule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProcessCardModule.BackgroundColor = System.Drawing.Color.White;
            this.dgvProcessCardModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProcessCardModule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProcessCardModule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProcessCardModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessCardModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbxProcessPlanning,
            this.dgvcbProcessCard,
            this.name});
            this.dgvProcessCardModule.Location = new System.Drawing.Point(1, 17);
            this.dgvProcessCardModule.Name = "dgvProcessCardModule";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProcessCardModule.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvProcessCardModule.RowTemplate.Height = 23;
            this.dgvProcessCardModule.Size = new System.Drawing.Size(517, 232);
            this.dgvProcessCardModule.TabIndex = 0;
            this.dgvProcessCardModule.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcessCardModule_CellContentClick);
            // 
            // cbxProcessPlanning
            // 
            this.cbxProcessPlanning.FalseValue = "0";
            this.cbxProcessPlanning.FillWeight = 33.50254F;
            this.cbxProcessPlanning.HeaderText = "序号";
            this.cbxProcessPlanning.MinimumWidth = 30;
            this.cbxProcessPlanning.Name = "cbxProcessPlanning";
            this.cbxProcessPlanning.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxProcessPlanning.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cbxProcessPlanning.TrueValue = "1";
            // 
            // dgvcbProcessCard
            // 
            this.dgvcbProcessCard.DataPropertyName = "Id";
            this.dgvcbProcessCard.HeaderText = "工艺卡片模板标识";
            this.dgvcbProcessCard.Name = "dgvcbProcessCard";
            this.dgvcbProcessCard.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvcbProcessCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvcbProcessCard.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.FillWeight = 186.4975F;
            this.name.HeaderText = "工艺卡片模板名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProcessPlanningModuleId";
            this.dataGridViewTextBoxColumn1.HeaderText = "工艺卡片模板标识";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn2.FillWeight = 120F;
            this.dataGridViewTextBoxColumn2.HeaderText = "工艺卡片模板名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 390;
            // 
            // SelectProcessCardModuleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 331);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectProcessCardModuleFrm";
            this.Text = "选择工艺卡片模板";
            this.Load += new System.EventHandler(this.SelectProcessCardModuleFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbProcessCardModule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessCardModule)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProcessCardName;
        private System.Windows.Forms.Label lblProcessCard;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.GroupBox gbProcessCardModule;
        private System.Windows.Forms.DataGridView dgvProcessCardModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbxProcessPlanning;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcbProcessCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
    }
}