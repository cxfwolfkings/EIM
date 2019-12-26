namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class RoughnessMarkFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoughnessMarkFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSymbolValue = new System.Windows.Forms.ComboBox();
            this.cmbValue = new System.Windows.Forms.ComboBox();
            this.pnSymbol = new System.Windows.Forms.Panel();
            this.btnSymbol3 = new System.Windows.Forms.Button();
            this.btnSymbol2 = new System.Windows.Forms.Button();
            this.btnSymbol1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSymbolValue = new System.Windows.Forms.Label();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.btnShowSymbol = new System.Windows.Forms.Button();
            this.pbTemp = new System.Windows.Forms.PictureBox();
            this.pnBody.SuspendLayout();
            this.pnSymbol.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.pnSymbol);
            this.pnBody.Controls.Add(this.label1);
            this.pnBody.Controls.Add(this.label2);
            this.pnBody.Controls.Add(this.lblSymbolValue);
            this.pnBody.Controls.Add(this.cmbSymbolValue);
            this.pnBody.Controls.Add(this.pbPreview);
            this.pnBody.Controls.Add(this.cmbValue);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.btnShowSymbol);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.label3);
            this.pnBody.Size = new System.Drawing.Size(311, 135);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(26, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "基本符号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(95, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "值:";
            // 
            // cmbSymbolValue
            // 
            this.cmbSymbolValue.BackColor = System.Drawing.SystemColors.Control;
            this.cmbSymbolValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSymbolValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSymbolValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSymbolValue.FormattingEnabled = true;
            this.cmbSymbolValue.Location = new System.Drawing.Point(120, 2);
            this.cmbSymbolValue.Name = "cmbSymbolValue";
            this.cmbSymbolValue.Size = new System.Drawing.Size(99, 25);
            this.cmbSymbolValue.TabIndex = 2;
            this.cmbSymbolValue.SelectedValueChanged += new System.EventHandler(this.cmbSymbolValue_SelectedValueChanged);
            // 
            // cmbValue
            // 
            this.cmbValue.BackColor = System.Drawing.SystemColors.Control;
            this.cmbValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbValue.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbValue.FormattingEnabled = true;
            this.cmbValue.Location = new System.Drawing.Point(120, 32);
            this.cmbValue.Name = "cmbValue";
            this.cmbValue.Size = new System.Drawing.Size(99, 25);
            this.cmbValue.TabIndex = 3;
            this.cmbValue.TextChanged += new System.EventHandler(this.cmbValue_TextChanged);
            this.cmbValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbValue_KeyPress);
            // 
            // pnSymbol
            // 
            this.pnSymbol.Controls.Add(this.btnSymbol3);
            this.pnSymbol.Controls.Add(this.btnSymbol2);
            this.pnSymbol.Controls.Add(this.btnSymbol1);
            this.pnSymbol.Location = new System.Drawing.Point(6, 25);
            this.pnSymbol.Name = "pnSymbol";
            this.pnSymbol.Size = new System.Drawing.Size(34, 98);
            this.pnSymbol.TabIndex = 5;
            this.pnSymbol.Visible = false;
            // 
            // btnSymbol3
            // 
            this.btnSymbol3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSymbol3.FlatAppearance.BorderSize = 0;
            this.btnSymbol3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSymbol3.Image = global::Kingdee.CAPP.UI.Properties.Resources.mark3;
            this.btnSymbol3.Location = new System.Drawing.Point(0, 64);
            this.btnSymbol3.Name = "btnSymbol3";
            this.btnSymbol3.Size = new System.Drawing.Size(34, 34);
            this.btnSymbol3.TabIndex = 2;
            this.btnSymbol3.UseVisualStyleBackColor = true;
            this.btnSymbol3.Click += new System.EventHandler(this.btnSymbol3_Click);
            // 
            // btnSymbol2
            // 
            this.btnSymbol2.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSymbol2.FlatAppearance.BorderSize = 0;
            this.btnSymbol2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSymbol2.Image = global::Kingdee.CAPP.UI.Properties.Resources.mark2;
            this.btnSymbol2.Location = new System.Drawing.Point(0, 32);
            this.btnSymbol2.Name = "btnSymbol2";
            this.btnSymbol2.Size = new System.Drawing.Size(34, 32);
            this.btnSymbol2.TabIndex = 1;
            this.btnSymbol2.UseVisualStyleBackColor = true;
            this.btnSymbol2.Click += new System.EventHandler(this.btnSymbol2_Click);
            // 
            // btnSymbol1
            // 
            this.btnSymbol1.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSymbol1.FlatAppearance.BorderSize = 0;
            this.btnSymbol1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSymbol1.Image = global::Kingdee.CAPP.UI.Properties.Resources.mark1;
            this.btnSymbol1.Location = new System.Drawing.Point(0, 0);
            this.btnSymbol1.Name = "btnSymbol1";
            this.btnSymbol1.Size = new System.Drawing.Size(34, 32);
            this.btnSymbol1.TabIndex = 0;
            this.btnSymbol1.UseVisualStyleBackColor = true;
            this.btnSymbol1.Click += new System.EventHandler(this.btnSymbol1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(117, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "预览";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(225, 2);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 7;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(225, 32);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblSymbolValue
            // 
            this.lblSymbolValue.AutoSize = true;
            this.lblSymbolValue.BackColor = System.Drawing.Color.Transparent;
            this.lblSymbolValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSymbolValue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSymbolValue.Location = new System.Drawing.Point(122, 88);
            this.lblSymbolValue.Name = "lblSymbolValue";
            this.lblSymbolValue.Size = new System.Drawing.Size(23, 12);
            this.lblSymbolValue.TabIndex = 10;
            this.lblSymbolValue.Text = "asdf";
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.Transparent;
            this.pbPreview.Image = global::Kingdee.CAPP.UI.Properties.Resources.mark1;
            this.pbPreview.Location = new System.Drawing.Point(141, 98);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(15, 17);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.TabIndex = 9;
            this.pbPreview.TabStop = false;
            // 
            // btnShowSymbol
            // 
            this.btnShowSymbol.FlatAppearance.BorderSize = 0;
            this.btnShowSymbol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowSymbol.Image = global::Kingdee.CAPP.UI.Properties.Resources.mark1;
            this.btnShowSymbol.Location = new System.Drawing.Point(39, 25);
            this.btnShowSymbol.Name = "btnShowSymbol";
            this.btnShowSymbol.Size = new System.Drawing.Size(50, 50);
            this.btnShowSymbol.TabIndex = 4;
            this.btnShowSymbol.UseVisualStyleBackColor = true;
            this.btnShowSymbol.Click += new System.EventHandler(this.btnShowSymbol_Click);
            // 
            // pbTemp
            // 
            this.pbTemp.BackColor = System.Drawing.Color.White;
            this.pbTemp.Location = new System.Drawing.Point(-246, -124);
            this.pbTemp.Name = "pbTemp";
            this.pbTemp.Size = new System.Drawing.Size(71, 20);
            this.pbTemp.TabIndex = 11;
            this.pbTemp.TabStop = false;
            // 
            // RoughnessMarkFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(311, 159);
            this.Controls.Add(this.pbTemp);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "RoughnessMarkFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "粗糙度标注";
            this.Load += new System.EventHandler(this.RoughnessMarkFrm_Load);
            this.Click += new System.EventHandler(this.RoughnessMarkFrm_Click);
            this.Controls.SetChildIndex(this.pbTemp, 0);
            this.Controls.SetChildIndex(this.pnBody, 0);
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            this.pnSymbol.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTemp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSymbolValue;
        private System.Windows.Forms.ComboBox cmbValue;
        private System.Windows.Forms.Button btnShowSymbol;
        private System.Windows.Forms.Button btnSymbol1;
        private System.Windows.Forms.Panel pnSymbol;
        private System.Windows.Forms.Button btnSymbol3;
        private System.Windows.Forms.Button btnSymbol2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Label lblSymbolValue;
        private System.Windows.Forms.PictureBox pbTemp;
    }
}