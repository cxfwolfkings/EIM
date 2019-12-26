namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class SelectProcessPlanningModuleFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectProcessPlanningModuleFrm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProcessProcedureName = new System.Windows.Forms.TextBox();
            this.lblProcessProcedure = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgvProcessProcedure = new System.Windows.Forms.DataGridView();
            this.cbxProcessPlanning = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvcbProcessProcedure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbProcessProcedureModule = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessProcedure)).BeginInit();
            this.gbProcessProcedureModule.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.gbProcessProcedureModule);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Size = new System.Drawing.Size(637, 421);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(562, 387);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(61, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProcessProcedureName);
            this.groupBox1.Controls.Add(this.lblProcessProcedure);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(13, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(610, 58);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // txtProcessProcedureName
            // 
            this.txtProcessProcedureName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcessProcedureName.Location = new System.Drawing.Point(131, 19);
            this.txtProcessProcedureName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProcessProcedureName.Multiline = true;
            this.txtProcessProcedureName.Name = "txtProcessProcedureName";
            this.txtProcessProcedureName.Size = new System.Drawing.Size(244, 20);
            this.txtProcessProcedureName.TabIndex = 5;
            this.txtProcessProcedureName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessProcedureName_KeyDown);
            // 
            // lblProcessProcedure
            // 
            this.lblProcessProcedure.AutoSize = true;
            this.lblProcessProcedure.Location = new System.Drawing.Point(9, 22);
            this.lblProcessProcedure.Name = "lblProcessProcedure";
            this.lblProcessProcedure.Size = new System.Drawing.Size(116, 17);
            this.lblProcessProcedure.TabIndex = 4;
            this.lblProcessProcedure.Text = "工艺规程模板名称：";
            // 
            // btnQuery
            // 
            this.btnQuery.FlatAppearance.BorderSize = 0;
            this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuery.Location = new System.Drawing.Point(381, 19);
            this.btnQuery.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(61, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(495, 387);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(61, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgvProcessProcedure
            // 
            this.dgvProcessProcedure.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProcessProcedure.BackgroundColor = System.Drawing.Color.White;
            this.dgvProcessProcedure.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProcessProcedure.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProcessProcedure.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProcessProcedure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessProcedure.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cbxProcessPlanning,
            this.dgvcbProcessProcedure,
            this.name});
            this.dgvProcessProcedure.Location = new System.Drawing.Point(3, 24);
            this.dgvProcessProcedure.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvProcessProcedure.Name = "dgvProcessProcedure";
            this.dgvProcessProcedure.RowHeadersVisible = false;
            this.dgvProcessProcedure.RowTemplate.Height = 23;
            this.dgvProcessProcedure.Size = new System.Drawing.Size(603, 281);
            this.dgvProcessProcedure.TabIndex = 0;
            this.dgvProcessProcedure.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProcessProcedure_CellContentClick);
            this.dgvProcessProcedure.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvProcessProcedure_CurrentCellDirtyStateChanged);
            // 
            // cbxProcessPlanning
            // 
            this.cbxProcessPlanning.FalseValue = "0";
            this.cbxProcessPlanning.FillWeight = 22.33503F;
            this.cbxProcessPlanning.HeaderText = "序号";
            this.cbxProcessPlanning.MinimumWidth = 20;
            this.cbxProcessPlanning.Name = "cbxProcessPlanning";
            this.cbxProcessPlanning.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxProcessPlanning.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cbxProcessPlanning.TrueValue = "1";
            // 
            // dgvcbProcessProcedure
            // 
            this.dgvcbProcessProcedure.DataPropertyName = "ProcessPlanningModuleId";
            this.dgvcbProcessProcedure.HeaderText = "工艺规程模板标识";
            this.dgvcbProcessProcedure.Name = "dgvcbProcessProcedure";
            this.dgvcbProcessProcedure.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvcbProcessProcedure.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvcbProcessProcedure.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.FillWeight = 197.665F;
            this.name.HeaderText = "工艺规程模板名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // gbProcessProcedureModule
            // 
            this.gbProcessProcedureModule.Controls.Add(this.dgvProcessProcedure);
            this.gbProcessProcedureModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbProcessProcedureModule.Location = new System.Drawing.Point(13, 67);
            this.gbProcessProcedureModule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbProcessProcedureModule.Name = "gbProcessProcedureModule";
            this.gbProcessProcedureModule.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbProcessProcedureModule.Size = new System.Drawing.Size(610, 312);
            this.gbProcessProcedureModule.TabIndex = 6;
            this.gbProcessProcedureModule.TabStop = false;
            this.gbProcessProcedureModule.Text = "选择工艺规程模板";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ProcessPlanningModuleId";
            this.dataGridViewTextBoxColumn1.HeaderText = "工艺规程模板标识";
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
            this.dataGridViewTextBoxColumn2.HeaderText = "工艺规程模板名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 390;
            // 
            // SelectProcessPlanningModuleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 443);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectProcessPlanningModuleFrm";
            this.Text = "选择工艺规程模板";
            this.Load += new System.EventHandler(this.SelectProcessPlanningModuleFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessProcedure)).EndInit();
            this.gbProcessProcedureModule.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProcessProcedureName;
        private System.Windows.Forms.Label lblProcessProcedure;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.DataGridView dgvProcessProcedure;
        private System.Windows.Forms.GroupBox gbProcessProcedureModule;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn cbxProcessPlanning;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvcbProcessProcedure;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
    }
}