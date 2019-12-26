namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    partial class NewProcessLine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProcessLine));
            this.cbxRoutingCategory = new System.Windows.Forms.ComboBox();
            this.tbxRoutingCode = new System.Windows.Forms.TextBox();
            this.lblRoutingCode = new System.Windows.Forms.Label();
            this.lblRoutingName = new System.Windows.Forms.Label();
            this.tbxRoutingName = new System.Windows.Forms.TextBox();
            this.lblRoutingCategory = new System.Windows.Forms.Label();
            this.btnConfrim = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbxRoutingCategory
            // 
            this.cbxRoutingCategory.FormattingEnabled = true;
            this.cbxRoutingCategory.Location = new System.Drawing.Point(111, 89);
            this.cbxRoutingCategory.Name = "cbxRoutingCategory";
            this.cbxRoutingCategory.Size = new System.Drawing.Size(256, 21);
            this.cbxRoutingCategory.TabIndex = 16;
            // 
            // tbxRoutingCode
            // 
            this.tbxRoutingCode.Location = new System.Drawing.Point(111, 58);
            this.tbxRoutingCode.Name = "tbxRoutingCode";
            this.tbxRoutingCode.Size = new System.Drawing.Size(256, 20);
            this.tbxRoutingCode.TabIndex = 15;
            // 
            // lblRoutingCode
            // 
            this.lblRoutingCode.AutoSize = true;
            this.lblRoutingCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRoutingCode.Location = new System.Drawing.Point(33, 58);
            this.lblRoutingCode.Name = "lblRoutingCode";
            this.lblRoutingCode.Size = new System.Drawing.Size(72, 13);
            this.lblRoutingCode.TabIndex = 14;
            this.lblRoutingCode.Text = "路线代码：";
            // 
            // lblRoutingName
            // 
            this.lblRoutingName.AutoSize = true;
            this.lblRoutingName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRoutingName.Location = new System.Drawing.Point(33, 26);
            this.lblRoutingName.Name = "lblRoutingName";
            this.lblRoutingName.Size = new System.Drawing.Size(72, 13);
            this.lblRoutingName.TabIndex = 13;
            this.lblRoutingName.Text = "路线名称：";
            // 
            // tbxRoutingName
            // 
            this.tbxRoutingName.Location = new System.Drawing.Point(111, 23);
            this.tbxRoutingName.Name = "tbxRoutingName";
            this.tbxRoutingName.Size = new System.Drawing.Size(256, 20);
            this.tbxRoutingName.TabIndex = 12;
            // 
            // lblRoutingCategory
            // 
            this.lblRoutingCategory.AutoSize = true;
            this.lblRoutingCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRoutingCategory.Location = new System.Drawing.Point(35, 89);
            this.lblRoutingCategory.Name = "lblRoutingCategory";
            this.lblRoutingCategory.Size = new System.Drawing.Size(72, 13);
            this.lblRoutingCategory.TabIndex = 11;
            this.lblRoutingCategory.Text = "路线类别：";
            // 
            // btnConfrim
            // 
            this.btnConfrim.Location = new System.Drawing.Point(92, 127);
            this.btnConfrim.Name = "btnConfrim";
            this.btnConfrim.Size = new System.Drawing.Size(85, 23);
            this.btnConfrim.TabIndex = 10;
            this.btnConfrim.Text = "确认";
            this.btnConfrim.UseVisualStyleBackColor = true;
            this.btnConfrim.Click += new System.EventHandler(this.btnConfrim_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(244, 126);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // NewProcessLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 166);
            this.Controls.Add(this.cbxRoutingCategory);
            this.Controls.Add(this.tbxRoutingCode);
            this.Controls.Add(this.lblRoutingCode);
            this.Controls.Add(this.lblRoutingName);
            this.Controls.Add(this.tbxRoutingName);
            this.Controls.Add(this.lblRoutingCategory);
            this.Controls.Add(this.btnConfrim);
            this.Controls.Add(this.btnClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(420, 200);
            this.MinimumSize = new System.Drawing.Size(420, 200);
            this.Name = "NewProcessLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建工艺路线";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxRoutingCategory;
        private System.Windows.Forms.TextBox tbxRoutingCode;
        private System.Windows.Forms.Label lblRoutingCode;
        private System.Windows.Forms.Label lblRoutingName;
        private System.Windows.Forms.TextBox tbxRoutingName;
        private System.Windows.Forms.Label lblRoutingCategory;
        private System.Windows.Forms.Button btnConfrim;
        private System.Windows.Forms.Button btnClose;

    }
}