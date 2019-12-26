namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    partial class PartRelProcessLing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartRelProcessLing));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxProcessLine = new System.Windows.Forms.ComboBox();
            this.btnComfirm = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewProcessLine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "选择关联工艺路线：";
            // 
            // cbxProcessLine
            // 
            this.cbxProcessLine.FormattingEnabled = true;
            this.cbxProcessLine.Location = new System.Drawing.Point(97, 25);
            this.cbxProcessLine.Name = "cbxProcessLine";
            this.cbxProcessLine.Size = new System.Drawing.Size(176, 21);
            this.cbxProcessLine.TabIndex = 2;
            // 
            // btnComfirm
            // 
            this.btnComfirm.Location = new System.Drawing.Point(14, 71);
            this.btnComfirm.Name = "btnComfirm";
            this.btnComfirm.Size = new System.Drawing.Size(75, 23);
            this.btnComfirm.TabIndex = 3;
            this.btnComfirm.Text = "确定";
            this.btnComfirm.UseVisualStyleBackColor = true;
            this.btnComfirm.Click += new System.EventHandler(this.btnComfirm_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(105, 70);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewProcessLine
            // 
            this.btnNewProcessLine.Location = new System.Drawing.Point(191, 70);
            this.btnNewProcessLine.Name = "btnNewProcessLine";
            this.btnNewProcessLine.Size = new System.Drawing.Size(89, 23);
            this.btnNewProcessLine.TabIndex = 5;
            this.btnNewProcessLine.Text = "新建工艺路线";
            this.btnNewProcessLine.UseVisualStyleBackColor = true;
            this.btnNewProcessLine.Click += new System.EventHandler(this.btnNewProcessLine_Click);
            // 
            // PartRelProcessLing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 119);
            this.Controls.Add(this.btnNewProcessLine);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnComfirm);
            this.Controls.Add(this.cbxProcessLine);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PartRelProcessLing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "零件关联到工艺路线";
            this.Load += new System.EventHandler(this.PartRelProcessLing_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxProcessLine;
        private System.Windows.Forms.Button btnComfirm;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewProcessLine;
    }
}