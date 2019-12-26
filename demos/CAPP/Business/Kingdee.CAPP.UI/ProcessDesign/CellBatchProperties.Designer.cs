namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class CellBatchProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CellBatchProperties));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnShowFontDialog = new System.Windows.Forms.Button();
            this.txtFont = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboStyle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboType = new System.Windows.Forms.ComboBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDown = new System.Windows.Forms.TextBox();
            this.txtRight = new System.Windows.Forms.TextBox();
            this.txtLeft = new System.Windows.Forms.TextBox();
            this.txtUp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdbtnBottom = new System.Windows.Forms.RadioButton();
            this.rdbtnMiddle = new System.Windows.Forms.RadioButton();
            this.rdbtnTop = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbtnRight = new System.Windows.Forms.RadioButton();
            this.rdbtnCenter = new System.Windows.Forms.RadioButton();
            this.rdbtnLeft = new System.Windows.Forms.RadioButton();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.groupBox3);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.groupBox2);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.groupBox4);
            this.pnBody.Size = new System.Drawing.Size(287, 396);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnShowFontDialog);
            this.groupBox1.Controls.Add(this.txtFont);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.comboStyle);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comboType);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(12, 1);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(265, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // btnShowFontDialog
            // 
            this.btnShowFontDialog.FlatAppearance.BorderSize = 0;
            this.btnShowFontDialog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowFontDialog.Location = new System.Drawing.Point(218, 83);
            this.btnShowFontDialog.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnShowFontDialog.Name = "btnShowFontDialog";
            this.btnShowFontDialog.Size = new System.Drawing.Size(25, 24);
            this.btnShowFontDialog.TabIndex = 18;
            this.btnShowFontDialog.Text = "...";
            this.btnShowFontDialog.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnShowFontDialog.UseVisualStyleBackColor = true;
            this.btnShowFontDialog.Click += new System.EventHandler(this.btnShowFontDialog_Click);
            // 
            // txtFont
            // 
            this.txtFont.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFont.Enabled = false;
            this.txtFont.Location = new System.Drawing.Point(45, 85);
            this.txtFont.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFont.Multiline = true;
            this.txtFont.Name = "txtFont";
            this.txtFont.Size = new System.Drawing.Size(167, 22);
            this.txtFont.TabIndex = 16;
            this.txtFont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "字体:";
            // 
            // comboStyle
            // 
            this.comboStyle.BackColor = System.Drawing.SystemColors.Control;
            this.comboStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboStyle.FormattingEnabled = true;
            this.comboStyle.Location = new System.Drawing.Point(45, 51);
            this.comboStyle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboStyle.Name = "comboStyle";
            this.comboStyle.Size = new System.Drawing.Size(198, 25);
            this.comboStyle.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "样式:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型:";
            // 
            // comboType
            // 
            this.comboType.BackColor = System.Drawing.SystemColors.Control;
            this.comboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboType.FormattingEnabled = true;
            this.comboType.Location = new System.Drawing.Point(45, 17);
            this.comboType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboType.Name = "comboType";
            this.comboType.Size = new System.Drawing.Size(198, 25);
            this.comboType.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtDown);
            this.groupBox4.Controls.Add(this.txtRight);
            this.groupBox4.Controls.Add(this.txtLeft);
            this.groupBox4.Controls.Add(this.txtUp);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Location = new System.Drawing.Point(12, 243);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(265, 113);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "边距";
            // 
            // txtDown
            // 
            this.txtDown.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDown.Location = new System.Drawing.Point(115, 80);
            this.txtDown.Multiline = true;
            this.txtDown.Name = "txtDown";
            this.txtDown.Size = new System.Drawing.Size(45, 22);
            this.txtDown.TabIndex = 24;
            this.txtDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtRight
            // 
            this.txtRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRight.Location = new System.Drawing.Point(184, 45);
            this.txtRight.Multiline = true;
            this.txtRight.Name = "txtRight";
            this.txtRight.Size = new System.Drawing.Size(45, 22);
            this.txtRight.TabIndex = 23;
            this.txtRight.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtLeft
            // 
            this.txtLeft.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLeft.Location = new System.Drawing.Point(45, 45);
            this.txtLeft.Multiline = true;
            this.txtLeft.Name = "txtLeft";
            this.txtLeft.Size = new System.Drawing.Size(45, 22);
            this.txtLeft.TabIndex = 22;
            this.txtLeft.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtUp
            // 
            this.txtUp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUp.Location = new System.Drawing.Point(115, 15);
            this.txtUp.Multiline = true;
            this.txtUp.Name = "txtUp";
            this.txtUp.Size = new System.Drawing.Size(45, 22);
            this.txtUp.TabIndex = 21;
            this.txtUp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(87, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "上:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 17);
            this.label9.TabIndex = 19;
            this.label9.Text = "左:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(156, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 17);
            this.label10.TabIndex = 20;
            this.label10.Text = "右:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(87, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 17);
            this.label11.TabIndex = 21;
            this.label11.Text = "下:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(5, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 17);
            this.label12.TabIndex = 22;
            this.label12.Text = "(单位:0.1mm)";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdbtnBottom);
            this.groupBox3.Controls.Add(this.rdbtnMiddle);
            this.groupBox3.Controls.Add(this.rdbtnTop);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(12, 185);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(265, 52);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "垂直位置";
            // 
            // rdbtnBottom
            // 
            this.rdbtnBottom.AutoSize = true;
            this.rdbtnBottom.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnBottom.FlatAppearance.BorderSize = 0;
            this.rdbtnBottom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnBottom.Location = new System.Drawing.Point(206, 22);
            this.rdbtnBottom.Name = "rdbtnBottom";
            this.rdbtnBottom.Size = new System.Drawing.Size(37, 21);
            this.rdbtnBottom.TabIndex = 19;
            this.rdbtnBottom.TabStop = true;
            this.rdbtnBottom.Text = "底";
            this.rdbtnBottom.UseVisualStyleBackColor = true;
            // 
            // rdbtnMiddle
            // 
            this.rdbtnMiddle.AutoSize = true;
            this.rdbtnMiddle.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnMiddle.FlatAppearance.BorderSize = 0;
            this.rdbtnMiddle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnMiddle.Location = new System.Drawing.Point(113, 22);
            this.rdbtnMiddle.Name = "rdbtnMiddle";
            this.rdbtnMiddle.Size = new System.Drawing.Size(37, 21);
            this.rdbtnMiddle.TabIndex = 18;
            this.rdbtnMiddle.TabStop = true;
            this.rdbtnMiddle.Text = "中";
            this.rdbtnMiddle.UseVisualStyleBackColor = true;
            // 
            // rdbtnTop
            // 
            this.rdbtnTop.AutoSize = true;
            this.rdbtnTop.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnTop.FlatAppearance.BorderSize = 0;
            this.rdbtnTop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnTop.Location = new System.Drawing.Point(20, 22);
            this.rdbtnTop.Name = "rdbtnTop";
            this.rdbtnTop.Size = new System.Drawing.Size(37, 21);
            this.rdbtnTop.TabIndex = 17;
            this.rdbtnTop.TabStop = true;
            this.rdbtnTop.Text = "顶";
            this.rdbtnTop.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbtnRight);
            this.groupBox2.Controls.Add(this.rdbtnCenter);
            this.groupBox2.Controls.Add(this.rdbtnLeft);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(12, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 52);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "水平位置";
            // 
            // rdbtnRight
            // 
            this.rdbtnRight.AutoSize = true;
            this.rdbtnRight.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnRight.FlatAppearance.BorderSize = 0;
            this.rdbtnRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnRight.Location = new System.Drawing.Point(206, 22);
            this.rdbtnRight.Name = "rdbtnRight";
            this.rdbtnRight.Size = new System.Drawing.Size(37, 21);
            this.rdbtnRight.TabIndex = 15;
            this.rdbtnRight.TabStop = true;
            this.rdbtnRight.Text = "右";
            this.rdbtnRight.UseVisualStyleBackColor = true;
            // 
            // rdbtnCenter
            // 
            this.rdbtnCenter.AutoSize = true;
            this.rdbtnCenter.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnCenter.FlatAppearance.BorderSize = 0;
            this.rdbtnCenter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnCenter.Location = new System.Drawing.Point(113, 22);
            this.rdbtnCenter.Name = "rdbtnCenter";
            this.rdbtnCenter.Size = new System.Drawing.Size(37, 21);
            this.rdbtnCenter.TabIndex = 14;
            this.rdbtnCenter.TabStop = true;
            this.rdbtnCenter.Text = "中";
            this.rdbtnCenter.UseVisualStyleBackColor = true;
            // 
            // rdbtnLeft
            // 
            this.rdbtnLeft.AutoSize = true;
            this.rdbtnLeft.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.rdbtnLeft.FlatAppearance.BorderSize = 0;
            this.rdbtnLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rdbtnLeft.Location = new System.Drawing.Point(20, 22);
            this.rdbtnLeft.Name = "rdbtnLeft";
            this.rdbtnLeft.Size = new System.Drawing.Size(37, 21);
            this.rdbtnLeft.TabIndex = 13;
            this.rdbtnLeft.TabStop = true;
            this.rdbtnLeft.Text = "左";
            this.rdbtnLeft.UseVisualStyleBackColor = true;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(121, 365);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 24);
            this.btnConfirm.TabIndex = 24;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(202, 365);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // CellBatchProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 420);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CellBatchProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量设置单元格属性";
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboType;
        private System.Windows.Forms.Button btnShowFontDialog;
        private System.Windows.Forms.TextBox txtFont;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboStyle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDown;
        private System.Windows.Forms.TextBox txtRight;
        private System.Windows.Forms.TextBox txtLeft;
        private System.Windows.Forms.TextBox txtUp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbtnBottom;
        private System.Windows.Forms.RadioButton rdbtnMiddle;
        private System.Windows.Forms.RadioButton rdbtnTop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdbtnRight;
        private System.Windows.Forms.RadioButton rdbtnCenter;
        private System.Windows.Forms.RadioButton rdbtnLeft;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}