namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class WeldingSymbolFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeldingSymbolFrm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pbWeldingSymbol = new System.Windows.Forms.PictureBox();
            this.picTemp = new System.Windows.Forms.PictureBox();
            this.pnBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeldingSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.pbWeldingSymbol);
            this.pnBody.Controls.Add(this.label1);
            this.pnBody.Controls.Add(this.label2);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.label3);
            this.pnBody.Controls.Add(this.label4);
            this.pnBody.Size = new System.Drawing.Size(190, 79);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "焊";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "接";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "符";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(11, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "号";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(102, 15);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(103, 44);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbWeldingSymbol
            // 
            this.pbWeldingSymbol.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWeldingSymbol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWeldingSymbol.Image = global::Kingdee.CAPP.UI.Properties.Resources.weld13;
            this.pbWeldingSymbol.Location = new System.Drawing.Point(36, 15);
            this.pbWeldingSymbol.Name = "pbWeldingSymbol";
            this.pbWeldingSymbol.Size = new System.Drawing.Size(56, 56);
            this.pbWeldingSymbol.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbWeldingSymbol.TabIndex = 0;
            this.pbWeldingSymbol.TabStop = false;
            this.pbWeldingSymbol.Click += new System.EventHandler(this.pbWeldingSymbol_Click);
            // 
            // picTemp
            // 
            this.picTemp.BackColor = System.Drawing.Color.White;
            this.picTemp.Location = new System.Drawing.Point(37, 107);
            this.picTemp.Name = "picTemp";
            this.picTemp.Size = new System.Drawing.Size(100, 50);
            this.picTemp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTemp.TabIndex = 7;
            this.picTemp.TabStop = false;
            // 
            // WeldingSymbolFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 103);
            this.Controls.Add(this.picTemp);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "WeldingSymbolFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "焊接符号";
            this.Load += new System.EventHandler(this.WeldingSymbolFrm_Load);
            this.Controls.SetChildIndex(this.picTemp, 0);
            this.Controls.SetChildIndex(this.pnBody, 0);
            this.pnBody.ResumeLayout(false);
            this.pnBody.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWeldingSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWeldingSymbol;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox picTemp;

    }
}