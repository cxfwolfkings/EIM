namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class AddTypicalProcessCardFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddTypicalProcessCardFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCardList = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CardModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.lblCardName = new System.Windows.Forms.Label();
            this.btnSerach = new System.Windows.Forms.Button();
            this.txtProcessCardName = new System.Windows.Forms.TextBox();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Size = new System.Drawing.Size(439, 280);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCardList);
            this.groupBox1.Controls.Add(this.btnConfirm);
            this.groupBox1.Controls.Add(this.lblCardName);
            this.groupBox1.Controls.Add(this.btnSerach);
            this.groupBox1.Controls.Add(this.txtProcessCardName);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(412, 262);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // dgvCardList
            // 
            this.dgvCardList.BackgroundColor = System.Drawing.Color.White;
            this.dgvCardList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCardList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvCardList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCardList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.CardModuleName});
            this.dgvCardList.Location = new System.Drawing.Point(9, 48);
            this.dgvCardList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvCardList.Name = "dgvCardList";
            this.dgvCardList.RowTemplate.Height = 23;
            this.dgvCardList.Size = new System.Drawing.Size(390, 188);
            this.dgvCardList.TabIndex = 5;
            this.dgvCardList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCardList_CellContentClick);
            // 
            // Select
            // 
            this.Select.FalseValue = "0";
            this.Select.Frozen = true;
            this.Select.HeaderText = "序号";
            this.Select.Name = "Select";
            this.Select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Select.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Select.TrueValue = "1";
            this.Select.Width = 40;
            // 
            // CardModuleName
            // 
            this.CardModuleName.DataPropertyName = "Name";
            this.CardModuleName.Frozen = true;
            this.CardModuleName.HeaderText = "工艺卡片名称";
            this.CardModuleName.Name = "CardModuleName";
            this.CardModuleName.Width = 250;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(326, 17);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(59, 23);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确认";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblCardName
            // 
            this.lblCardName.AutoSize = true;
            this.lblCardName.Location = new System.Drawing.Point(6, 20);
            this.lblCardName.Name = "lblCardName";
            this.lblCardName.Size = new System.Drawing.Size(59, 17);
            this.lblCardName.TabIndex = 0;
            this.lblCardName.Text = "卡片名称:";
            // 
            // btnSerach
            // 
            this.btnSerach.FlatAppearance.BorderSize = 0;
            this.btnSerach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSerach.Location = new System.Drawing.Point(264, 17);
            this.btnSerach.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSerach.Name = "btnSerach";
            this.btnSerach.Size = new System.Drawing.Size(56, 23);
            this.btnSerach.TabIndex = 2;
            this.btnSerach.Text = "查询";
            this.btnSerach.UseVisualStyleBackColor = true;
            this.btnSerach.Click += new System.EventHandler(this.btnSerach_Click);
            // 
            // txtProcessCardName
            // 
            this.txtProcessCardName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcessCardName.Location = new System.Drawing.Point(71, 17);
            this.txtProcessCardName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProcessCardName.MaxLength = 50;
            this.txtProcessCardName.Multiline = true;
            this.txtProcessCardName.Name = "txtProcessCardName";
            this.txtProcessCardName.Size = new System.Drawing.Size(187, 20);
            this.txtProcessCardName.TabIndex = 1;
            this.txtProcessCardName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // AddTypicalProcessCardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 302);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "AddTypicalProcessCardFrm";
            this.Text = "增加工艺卡片";
            this.Load += new System.EventHandler(this.AddTypicalProcessCardFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Label lblCardName;
        private System.Windows.Forms.Button btnSerach;
        private System.Windows.Forms.TextBox txtProcessCardName;
        private System.Windows.Forms.DataGridView dgvCardList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardModuleName;
    }
}