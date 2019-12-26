namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class SuperscriptAndSubscriptFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SuperscriptAndSubscriptFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBase1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSup1 = new System.Windows.Forms.TextBox();
            this.txtSub1 = new System.Windows.Forms.TextBox();
            this.cbSplit = new System.Windows.Forms.CheckBox();
            this.lklblSplit1 = new System.Windows.Forms.LinkLabel();
            this.btnTwo = new System.Windows.Forms.Button();
            this.lklblSplit2 = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBase2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSup2 = new System.Windows.Forms.TextBox();
            this.txtSub2 = new System.Windows.Forms.TextBox();
            this.lklblSplit3 = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBase3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSup3 = new System.Windows.Forms.TextBox();
            this.txtSub3 = new System.Windows.Forms.TextBox();
            this.btnThree = new System.Windows.Forms.Button();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbTemp = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Controls.Add(this.pbTemp);
            this.pnBody.Controls.Add(this.label10);
            this.pnBody.Controls.Add(this.btnTwo);
            this.pnBody.Controls.Add(this.btnThree);
            this.pnBody.Controls.Add(this.btnExit);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Size = new System.Drawing.Size(185, 169);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(10, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "基准:";
            // 
            // txtBase1
            // 
            this.txtBase1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBase1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBase1.Location = new System.Drawing.Point(49, 36);
            this.txtBase1.Multiline = true;
            this.txtBase1.Name = "txtBase1";
            this.txtBase1.Size = new System.Drawing.Size(48, 20);
            this.txtBase1.TabIndex = 1;
            this.txtBase1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(69, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "上标:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(69, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "下标:";
            // 
            // txtSup1
            // 
            this.txtSup1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSup1.Location = new System.Drawing.Point(103, 21);
            this.txtSup1.Multiline = true;
            this.txtSup1.Name = "txtSup1";
            this.txtSup1.Size = new System.Drawing.Size(67, 20);
            this.txtSup1.TabIndex = 2;
            this.txtSup1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtSub1
            // 
            this.txtSub1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSub1.Location = new System.Drawing.Point(103, 52);
            this.txtSub1.Multiline = true;
            this.txtSub1.Name = "txtSub1";
            this.txtSub1.Size = new System.Drawing.Size(67, 20);
            this.txtSub1.TabIndex = 3;
            this.txtSub1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // cbSplit
            // 
            this.cbSplit.AutoSize = true;
            this.cbSplit.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbSplit.FlatAppearance.BorderSize = 0;
            this.cbSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSplit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSplit.Location = new System.Drawing.Point(13, 60);
            this.cbSplit.Name = "cbSplit";
            this.cbSplit.Size = new System.Drawing.Size(60, 21);
            this.cbSplit.TabIndex = 4;
            this.cbSplit.Text = "分位线";
            this.cbSplit.UseVisualStyleBackColor = true;
            this.cbSplit.CheckedChanged += new System.EventHandler(this.cbSplit_CheckedChanged);
            // 
            // lklblSplit1
            // 
            this.lklblSplit1.AutoSize = true;
            this.lklblSplit1.Location = new System.Drawing.Point(102, 34);
            this.lklblSplit1.Name = "lklblSplit1";
            this.lklblSplit1.Size = new System.Drawing.Size(71, 16);
            this.lklblSplit1.TabIndex = 7;
            this.lklblSplit1.TabStop = true;
            this.lklblSplit1.Text = "                     ";
            this.lklblSplit1.Visible = false;
            // 
            // btnTwo
            // 
            this.btnTwo.FlatAppearance.BorderSize = 0;
            this.btnTwo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTwo.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTwo.Location = new System.Drawing.Point(138, 106);
            this.btnTwo.Name = "btnTwo";
            this.btnTwo.Size = new System.Drawing.Size(33, 23);
            this.btnTwo.TabIndex = 8;
            this.btnTwo.Text = ">>";
            this.btnTwo.UseVisualStyleBackColor = true;
            this.btnTwo.Click += new System.EventHandler(this.btnTwo_Click);
            // 
            // lklblSplit2
            // 
            this.lklblSplit2.AutoSize = true;
            this.lklblSplit2.Location = new System.Drawing.Point(275, 34);
            this.lklblSplit2.Name = "lklblSplit2";
            this.lklblSplit2.Size = new System.Drawing.Size(71, 16);
            this.lklblSplit2.TabIndex = 7;
            this.lklblSplit2.TabStop = true;
            this.lklblSplit2.Text = "                     ";
            this.lklblSplit2.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(183, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "基准:";
            // 
            // txtBase2
            // 
            this.txtBase2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBase2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBase2.Location = new System.Drawing.Point(222, 36);
            this.txtBase2.Multiline = true;
            this.txtBase2.Name = "txtBase2";
            this.txtBase2.Size = new System.Drawing.Size(48, 20);
            this.txtBase2.TabIndex = 5;
            this.txtBase2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(242, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "上标:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(242, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "下标:";
            // 
            // txtSup2
            // 
            this.txtSup2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSup2.Location = new System.Drawing.Point(276, 21);
            this.txtSup2.Multiline = true;
            this.txtSup2.Name = "txtSup2";
            this.txtSup2.Size = new System.Drawing.Size(67, 20);
            this.txtSup2.TabIndex = 6;
            this.txtSup2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtSub2
            // 
            this.txtSub2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSub2.Location = new System.Drawing.Point(276, 52);
            this.txtSub2.Multiline = true;
            this.txtSub2.Name = "txtSub2";
            this.txtSub2.Size = new System.Drawing.Size(67, 20);
            this.txtSub2.TabIndex = 7;
            this.txtSub2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // lklblSplit3
            // 
            this.lklblSplit3.AutoSize = true;
            this.lklblSplit3.Location = new System.Drawing.Point(451, 34);
            this.lklblSplit3.Name = "lklblSplit3";
            this.lklblSplit3.Size = new System.Drawing.Size(71, 16);
            this.lklblSplit3.TabIndex = 7;
            this.lklblSplit3.TabStop = true;
            this.lklblSplit3.Text = "                     ";
            this.lklblSplit3.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(359, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "基准:";
            // 
            // txtBase3
            // 
            this.txtBase3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBase3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBase3.Location = new System.Drawing.Point(398, 36);
            this.txtBase3.Multiline = true;
            this.txtBase3.Name = "txtBase3";
            this.txtBase3.Size = new System.Drawing.Size(48, 20);
            this.txtBase3.TabIndex = 8;
            this.txtBase3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(418, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 17);
            this.label8.TabIndex = 2;
            this.label8.Text = "上标:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(418, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "下标:";
            // 
            // txtSup3
            // 
            this.txtSup3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSup3.Location = new System.Drawing.Point(452, 21);
            this.txtSup3.Multiline = true;
            this.txtSup3.Name = "txtSup3";
            this.txtSup3.Size = new System.Drawing.Size(67, 20);
            this.txtSup3.TabIndex = 9;
            this.txtSup3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // txtSub3
            // 
            this.txtSub3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSub3.Location = new System.Drawing.Point(452, 52);
            this.txtSub3.Multiline = true;
            this.txtSub3.Name = "txtSub3";
            this.txtSub3.Size = new System.Drawing.Size(67, 20);
            this.txtSub3.TabIndex = 10;
            this.txtSub3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // btnThree
            // 
            this.btnThree.FlatAppearance.BorderSize = 0;
            this.btnThree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThree.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnThree.Location = new System.Drawing.Point(311, 106);
            this.btnThree.Name = "btnThree";
            this.btnThree.Size = new System.Drawing.Size(33, 23);
            this.btnThree.TabIndex = 8;
            this.btnThree.Text = ">>";
            this.btnThree.UseVisualStyleBackColor = true;
            this.btnThree.Click += new System.EventHandler(this.btnThree_Click);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(12, 106);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(55, 23);
            this.btnConfirm.TabIndex = 11;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnExit
            // 
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExit.Location = new System.Drawing.Point(73, 106);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(55, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSup1);
            this.groupBox1.Controls.Add(this.lklblSplit1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbSplit);
            this.groupBox1.Controls.Add(this.txtBase1);
            this.groupBox1.Controls.Add(this.txtSub3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSub2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtSub1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSup3);
            this.groupBox1.Controls.Add(this.txtBase2);
            this.groupBox1.Controls.Add(this.txtSup2);
            this.groupBox1.Controls.Add(this.txtBase3);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lklblSplit2);
            this.groupBox1.Controls.Add(this.lklblSplit3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 100);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // pbTemp
            // 
            this.pbTemp.BackColor = System.Drawing.Color.White;
            this.pbTemp.Location = new System.Drawing.Point(9, 238);
            this.pbTemp.Name = "pbTemp";
            this.pbTemp.Size = new System.Drawing.Size(0, 0);
            this.pbTemp.TabIndex = 14;
            this.pbTemp.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(5, 141);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 17);
            this.label10.TabIndex = 15;
            this.label10.Text = "预览:";
            // 
            // SuperscriptAndSubscriptFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 193);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SuperscriptAndSubscriptFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "上标下标";
            this.Load += new System.EventHandler(this.SuperscriptAndSubscriptFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBase1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSup1;
        private System.Windows.Forms.TextBox txtSub1;
        private System.Windows.Forms.CheckBox cbSplit;
        private System.Windows.Forms.LinkLabel lklblSplit1;
        private System.Windows.Forms.Button btnTwo;
        private System.Windows.Forms.LinkLabel lklblSplit2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBase2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSup2;
        private System.Windows.Forms.TextBox txtSub2;
        private System.Windows.Forms.LinkLabel lklblSplit3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBase3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSup3;
        private System.Windows.Forms.TextBox txtSub3;
        private System.Windows.Forms.Button btnThree;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbTemp;
        private System.Windows.Forms.Label label10;
    }
}