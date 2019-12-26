namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class AddProcessCardModuleFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProcessCardModuleFrm));
            this.txtProcessCardModuleName = new System.Windows.Forms.TextBox();
            this.btnSerach = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.dgvModuleCardList = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CardModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblBusinessName = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModuleCardList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Size = new System.Drawing.Size(446, 267);
            // 
            // txtProcessCardModuleName
            // 
            this.txtProcessCardModuleName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcessCardModuleName.Location = new System.Drawing.Point(80, 26);
            this.txtProcessCardModuleName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProcessCardModuleName.Multiline = true;
            this.txtProcessCardModuleName.Name = "txtProcessCardModuleName";
            this.txtProcessCardModuleName.Size = new System.Drawing.Size(187, 20);
            this.txtProcessCardModuleName.TabIndex = 1;
            this.txtProcessCardModuleName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessCardModuleName_KeyDown);
            // 
            // btnSerach
            // 
            this.btnSerach.FlatAppearance.BorderSize = 0;
            this.btnSerach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerach.Location = new System.Drawing.Point(273, 25);
            this.btnSerach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Size = new System.Drawing.Size(56, 23);
            this.btnSerach.TabIndex = 2;
            this.btnSerach.Text = "查询";
            this.btnSerach.UseVisualStyleBackColor = true;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.dgvModuleCardList);
            this.groupBox1.Controls.Add(this.lblBusinessName);
            this.groupBox1.Controls.Add(this.btnSerach);
            this.groupBox1.Controls.Add(this.txtProcessCardModuleName);
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(422, 251);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(335, 25);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(59, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // dgvModuleCardList
            // 
            this.dgvModuleCardList.AllowUserToAddRows = false;
            this.dgvModuleCardList.AllowUserToDeleteRows = false;
            this.dgvModuleCardList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvModuleCardList.BackgroundColor = System.Drawing.Color.White;
            this.dgvModuleCardList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvModuleCardList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvModuleCardList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModuleCardList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.CardModuleName,
            this.colID});
            this.dgvModuleCardList.Location = new System.Drawing.Point(17, 56);
            this.dgvModuleCardList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvModuleCardList.Name = "dgvModuleCardList";
            this.dgvModuleCardList.RowTemplate.Height = 23;
            this.dgvModuleCardList.Size = new System.Drawing.Size(390, 188);
            this.dgvModuleCardList.TabIndex = 4;
            this.dgvModuleCardList.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvModuleCardList_CellBeginEdit);
            this.dgvModuleCardList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvModuleCardList_CellContentClick);
            // 
            // Select
            // 
            this.Select.FalseValue = "0";
            this.Select.FillWeight = 50.76142F;
            this.Select.HeaderText = "序号";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.TrueValue = "1";
            // 
            // CardModuleName
            // 
            this.CardModuleName.DataPropertyName = "Name";
            this.CardModuleName.FillWeight = 149.2386F;
            this.CardModuleName.HeaderText = "工艺卡片模版名称";
            this.CardModuleName.Name = "CardModuleName";
            // 
            // colID
            // 
            this.colID.DataPropertyName = "Id";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.Visible = false;
            // 
            // lblBusinessName
            // 
            this.lblBusinessName.AutoSize = true;
            this.lblBusinessName.Location = new System.Drawing.Point(15, 27);
            this.lblBusinessName.Name = "lblBusinessName";
            this.lblBusinessName.Size = new System.Drawing.Size(59, 17);
            this.lblBusinessName.TabIndex = 0;
            this.lblBusinessName.Text = "模板名称:";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "模板名称";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 250;
            // 
            // AddProcessCardModuleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 289);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AddProcessCardModuleFrm";
            this.Text = "选择工艺卡片模板";
            this.Load += new System.EventHandler(this.AddProcessCardModuleFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModuleCardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtProcessCardModuleName;
        private System.Windows.Forms.Button btnSerach;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBusinessName;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView dgvModuleCardList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
    }
}