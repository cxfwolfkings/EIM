namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProcessSearchFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessSearchFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pnSearchConditions = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.comboStatus = new System.Windows.Forms.ComboBox();
            this.comboCardModule = new System.Windows.Forms.ComboBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.txtStaff = new System.Windows.Forms.TextBox();
            this.txtComponent = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.dgvProcessCard = new System.Windows.Forms.DataGridView();
            this.colCardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImageNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProcess = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPageNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnBody.SuspendLayout();
            this.pnSearchConditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessCard)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.pnSearchConditions);
            this.pnBody.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnBody.Size = new System.Drawing.Size(745, 113);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(219, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "零部件:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "卡片模版:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "编制人员:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "编制日期:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(219, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 17);
            this.label6.TabIndex = 5;
            this.label6.Text = "文件状态:";
            // 
            // pnSearchConditions
            // 
            this.pnSearchConditions.Controls.Add(this.btnExit);
            this.pnSearchConditions.Controls.Add(this.btnSearch);
            this.pnSearchConditions.Controls.Add(this.comboStatus);
            this.pnSearchConditions.Controls.Add(this.comboCardModule);
            this.pnSearchConditions.Controls.Add(this.dtpEnd);
            this.pnSearchConditions.Controls.Add(this.label7);
            this.pnSearchConditions.Controls.Add(this.dtpStart);
            this.pnSearchConditions.Controls.Add(this.txtStaff);
            this.pnSearchConditions.Controls.Add(this.txtComponent);
            this.pnSearchConditions.Controls.Add(this.txtProduct);
            this.pnSearchConditions.Controls.Add(this.label4);
            this.pnSearchConditions.Controls.Add(this.label6);
            this.pnSearchConditions.Controls.Add(this.label1);
            this.pnSearchConditions.Controls.Add(this.label5);
            this.pnSearchConditions.Controls.Add(this.label2);
            this.pnSearchConditions.Controls.Add(this.label3);
            this.pnSearchConditions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnSearchConditions.Location = new System.Drawing.Point(0, 0);
            this.pnSearchConditions.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnSearchConditions.Name = "pnSearchConditions";
            this.pnSearchConditions.Size = new System.Drawing.Size(745, 113);
            this.pnSearchConditions.TabIndex = 6;
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Location = new System.Drawing.Point(480, 75);
            this.btnExit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(69, 23);
            this.btnExit.TabIndex = 15;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(405, 74);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(69, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // comboStatus
            // 
            this.comboStatus.BackColor = System.Drawing.SystemColors.Control;
            this.comboStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboStatus.FormattingEnabled = true;
            this.comboStatus.Location = new System.Drawing.Point(280, 74);
            this.comboStatus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboStatus.Name = "comboStatus";
            this.comboStatus.Size = new System.Drawing.Size(116, 25);
            this.comboStatus.TabIndex = 7;
            // 
            // comboCardModule
            // 
            this.comboCardModule.BackColor = System.Drawing.SystemColors.Control;
            this.comboCardModule.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCardModule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboCardModule.FormattingEnabled = true;
            this.comboCardModule.Location = new System.Drawing.Point(97, 75);
            this.comboCardModule.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboCardModule.Name = "comboCardModule";
            this.comboCardModule.Size = new System.Drawing.Size(116, 25);
            this.comboCardModule.TabIndex = 6;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(427, 41);
            this.dtpEnd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(116, 23);
            this.dtpEnd.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(402, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(20, 17);
            this.label7.TabIndex = 10;
            this.label7.Text = "到";
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(280, 39);
            this.dtpStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(116, 23);
            this.dtpStart.TabIndex = 4;
            // 
            // txtStaff
            // 
            this.txtStaff.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStaff.Location = new System.Drawing.Point(97, 41);
            this.txtStaff.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStaff.Multiline = true;
            this.txtStaff.Name = "txtStaff";
            this.txtStaff.Size = new System.Drawing.Size(116, 22);
            this.txtStaff.TabIndex = 3;
            this.txtStaff.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtComponent
            // 
            this.txtComponent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComponent.Location = new System.Drawing.Point(280, 7);
            this.txtComponent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComponent.Multiline = true;
            this.txtComponent.Name = "txtComponent";
            this.txtComponent.Size = new System.Drawing.Size(116, 22);
            this.txtComponent.TabIndex = 2;
            this.txtComponent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtProduct
            // 
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProduct.Location = new System.Drawing.Point(97, 7);
            this.txtProduct.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProduct.Multiline = true;
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(116, 22);
            this.txtProduct.TabIndex = 1;
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // dgvProcessCard
            // 
            this.dgvProcessCard.AllowUserToAddRows = false;
            this.dgvProcessCard.AllowUserToDeleteRows = false;
            this.dgvProcessCard.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvProcessCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProcessCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardName,
            this.colImageNumber,
            this.colProcess,
            this.colStatus,
            this.colDate,
            this.colPageNumber,
            this.colProductNumber});
            this.dgvProcessCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProcessCard.Location = new System.Drawing.Point(0, 135);
            this.dgvProcessCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvProcessCard.Name = "dgvProcessCard";
            this.dgvProcessCard.ReadOnly = true;
            this.dgvProcessCard.RowTemplate.Height = 23;
            this.dgvProcessCard.Size = new System.Drawing.Size(745, 221);
            this.dgvProcessCard.TabIndex = 7;
            // 
            // colCardName
            // 
            this.colCardName.DataPropertyName = "name";
            this.colCardName.HeaderText = "卡片名称";
            this.colCardName.Name = "colCardName";
            this.colCardName.ReadOnly = true;
            // 
            // colImageNumber
            // 
            this.colImageNumber.DataPropertyName = "drawnumber";
            this.colImageNumber.HeaderText = "图号";
            this.colImageNumber.Name = "colImageNumber";
            this.colImageNumber.ReadOnly = true;
            // 
            // colProcess
            // 
            this.colProcess.DataPropertyName = "processname";
            this.colProcess.HeaderText = "工序";
            this.colProcess.Name = "colProcess";
            this.colProcess.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "status";
            this.colStatus.HeaderText = "状态";
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // colDate
            // 
            this.colDate.DataPropertyName = "createtime";
            this.colDate.HeaderText = "编制日期";
            this.colDate.Name = "colDate";
            this.colDate.ReadOnly = true;
            // 
            // colPageNumber
            // 
            this.colPageNumber.DataPropertyName = "pagenumber";
            this.colPageNumber.HeaderText = "页码";
            this.colPageNumber.Name = "colPageNumber";
            this.colPageNumber.ReadOnly = true;
            // 
            // colProductNumber
            // 
            this.colProductNumber.DataPropertyName = "prodrawnumber";
            this.colProductNumber.HeaderText = "产品图号";
            this.colProductNumber.Name = "colProductNumber";
            this.colProductNumber.ReadOnly = true;
            // 
            // ProcessSearchFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 356);
            this.Controls.Add(this.dgvProcessCard);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ProcessSearchFrm";
            this.Text = "查询工艺文件";
            this.Load += new System.EventHandler(this.ProcessSearchFrm_Load);
            this.Controls.SetChildIndex(this.pnBody, 0);
            this.Controls.SetChildIndex(this.dgvProcessCard, 0);
            this.pnBody.ResumeLayout(false);
            this.pnSearchConditions.ResumeLayout(false);
            this.pnSearchConditions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProcessCard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnSearchConditions;
        private System.Windows.Forms.ComboBox comboStatus;
        private System.Windows.Forms.ComboBox comboCardModule;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.TextBox txtStaff;
        private System.Windows.Forms.TextBox txtComponent;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.DataGridView dgvProcessCard;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImageNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPageNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductNumber;
    }
}