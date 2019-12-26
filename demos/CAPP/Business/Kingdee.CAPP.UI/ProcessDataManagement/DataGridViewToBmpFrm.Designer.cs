namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class DataGridViewToBmpFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridViewToBmpFrm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPageRange = new System.Windows.Forms.TextBox();
            this.rbtnPageRange = new System.Windows.Forms.RadioButton();
            this.rbtnCurrPage = new System.Windows.Forms.RadioButton();
            this.rbtnAllPage = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnFilePath = new System.Windows.Forms.Button();
            this.cmbImageType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.groupBox2);
            this.pnBody.Size = new System.Drawing.Size(435, 298);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPageRange);
            this.groupBox1.Controls.Add(this.rbtnPageRange);
            this.groupBox1.Controls.Add(this.rbtnCurrPage);
            this.groupBox1.Controls.Add(this.rbtnAllPage);
            this.groupBox1.Location = new System.Drawing.Point(14, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(408, 135);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "页面范围(共{0}页)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "页面范围可输入多个，以逗号分隔，如：1,3,6-8";
            // 
            // txtPageRange
            // 
            this.txtPageRange.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPageRange.Enabled = false;
            this.txtPageRange.Location = new System.Drawing.Point(96, 80);
            this.txtPageRange.Multiline = true;
            this.txtPageRange.Name = "txtPageRange";
            this.txtPageRange.Size = new System.Drawing.Size(256, 22);
            this.txtPageRange.TabIndex = 3;
            this.txtPageRange.Tag = "页面范围";
            this.txtPageRange.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // rbtnPageRange
            // 
            this.rbtnPageRange.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rbtnPageRange.FlatAppearance.BorderSize = 0;
            this.rbtnPageRange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnPageRange.Location = new System.Drawing.Point(19, 81);
            this.rbtnPageRange.Name = "rbtnPageRange";
            this.rbtnPageRange.Size = new System.Drawing.Size(73, 21);
            this.rbtnPageRange.TabIndex = 2;
            this.rbtnPageRange.Text = "页面范围";
            this.rbtnPageRange.UseVisualStyleBackColor = true;
            this.rbtnPageRange.CheckedChanged += new System.EventHandler(this.rbtnPageRange_CheckedChanged);
            // 
            // rbtnCurrPage
            // 
            this.rbtnCurrPage.Checked = true;
            this.rbtnCurrPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rbtnCurrPage.FlatAppearance.BorderSize = 0;
            this.rbtnCurrPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnCurrPage.Location = new System.Drawing.Point(19, 52);
            this.rbtnCurrPage.Name = "rbtnCurrPage";
            this.rbtnCurrPage.Size = new System.Drawing.Size(61, 21);
            this.rbtnCurrPage.TabIndex = 1;
            this.rbtnCurrPage.TabStop = true;
            this.rbtnCurrPage.Text = "当前页";
            this.rbtnCurrPage.UseVisualStyleBackColor = true;
            // 
            // rbtnAllPage
            // 
            this.rbtnAllPage.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rbtnAllPage.FlatAppearance.BorderSize = 0;
            this.rbtnAllPage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnAllPage.Location = new System.Drawing.Point(19, 23);
            this.rbtnAllPage.Name = "rbtnAllPage";
            this.rbtnAllPage.Size = new System.Drawing.Size(61, 21);
            this.rbtnAllPage.TabIndex = 0;
            this.rbtnAllPage.Text = "全部页";
            this.rbtnAllPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnFilePath);
            this.groupBox2.Controls.Add(this.cmbImageType);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtFileName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtFilePath);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 142);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 117);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "文件信息";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(208, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "(每页一个文件，根据文件名自动生成)";
            // 
            // btnFilePath
            // 
            this.btnFilePath.FlatAppearance.BorderSize = 0;
            this.btnFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFilePath.Location = new System.Drawing.Point(358, 20);
            this.btnFilePath.Name = "btnFilePath";
            this.btnFilePath.Size = new System.Drawing.Size(27, 24);
            this.btnFilePath.TabIndex = 6;
            this.btnFilePath.Text = "...";
            this.btnFilePath.UseVisualStyleBackColor = true;
            this.btnFilePath.Click += new System.EventHandler(this.btnFilePath_Click);
            // 
            // cmbImageType
            // 
            this.cmbImageType.BackColor = System.Drawing.SystemColors.Control;
            this.cmbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbImageType.FormattingEnabled = true;
            this.cmbImageType.Location = new System.Drawing.Point(67, 80);
            this.cmbImageType.Name = "cmbImageType";
            this.cmbImageType.Size = new System.Drawing.Size(122, 25);
            this.cmbImageType.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "图像类型:";
            // 
            // txtFileName
            // 
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileName.Location = new System.Drawing.Point(67, 50);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(122, 22);
            this.txtFileName.TabIndex = 3;
            this.txtFileName.Tag = "文件名";
            this.txtFileName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "文件名:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFilePath.Location = new System.Drawing.Point(67, 20);
            this.txtFilePath.Multiline = true;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(285, 22);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.Tag = "文件路径";
            this.txtFilePath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "文件路径:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(298, 265);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(57, 23);
            this.btnConfirm.TabIndex = 2;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(361, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DataGridViewToBmpFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 322);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "DataGridViewToBmpFrm";
            this.Text = "导出卡片为图像文件";
            this.Load += new System.EventHandler(this.DataGridViewToBmpFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPageRange;
        private System.Windows.Forms.RadioButton rbtnPageRange;
        private System.Windows.Forms.RadioButton rbtnCurrPage;
        private System.Windows.Forms.RadioButton rbtnAllPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnFilePath;
        private System.Windows.Forms.ComboBox cmbImageType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}