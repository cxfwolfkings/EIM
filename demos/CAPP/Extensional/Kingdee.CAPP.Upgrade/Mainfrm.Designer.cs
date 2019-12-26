namespace Kingdee.CAPP.UpgradeServer
{
    partial class Mainfrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainfrm));
            this.label1 = new System.Windows.Forms.Label();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.tbxPath = new System.Windows.Forms.TextBox();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.fbdPath = new System.Windows.Forms.FolderBrowserDialog();
            this.tbxSupplyDownloadServerPath = new System.Windows.Forms.TextBox();
            this.lblSupplyDownloadServerPath = new System.Windows.Forms.Label();
            this.btnBrowserSupplyDownloadServerPath = new System.Windows.Forms.Button();
            this.btnCopyToSupplyDownloadServerPath = new System.Windows.Forms.Button();
            this.fdbDownloadForServerPath = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "请选择要更新的文件所在的目录：";
            // 
            // btnBrowser
            // 
            this.btnBrowser.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnBrowser.Location = new System.Drawing.Point(415, 9);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(26, 23);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "...";
            this.btnBrowser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // tbxPath
            // 
            this.tbxPath.Enabled = false;
            this.tbxPath.Location = new System.Drawing.Point(9, 33);
            this.tbxPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbxPath.Name = "tbxPath";
            this.tbxPath.Size = new System.Drawing.Size(433, 20);
            this.tbxPath.TabIndex = 3;
            // 
            // btnBuild
            // 
            this.btnBuild.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBuild.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuild.Location = new System.Drawing.Point(8, 59);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(181, 26);
            this.btnBuild.TabIndex = 4;
            this.btnBuild.Text = "创建文件更新列表文件(&C)";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(358, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭(&Q)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbxSupplyDownloadServerPath
            // 
            this.tbxSupplyDownloadServerPath.Location = new System.Drawing.Point(8, 126);
            this.tbxSupplyDownloadServerPath.Name = "tbxSupplyDownloadServerPath";
            this.tbxSupplyDownloadServerPath.Size = new System.Drawing.Size(433, 20);
            this.tbxSupplyDownloadServerPath.TabIndex = 6;
            this.tbxSupplyDownloadServerPath.Visible = false;
            // 
            // lblSupplyDownloadServerPath
            // 
            this.lblSupplyDownloadServerPath.AutoSize = true;
            this.lblSupplyDownloadServerPath.Location = new System.Drawing.Point(8, 109);
            this.lblSupplyDownloadServerPath.Name = "lblSupplyDownloadServerPath";
            this.lblSupplyDownloadServerPath.Size = new System.Drawing.Size(199, 13);
            this.lblSupplyDownloadServerPath.TabIndex = 7;
            this.lblSupplyDownloadServerPath.Text = "请选择供客户端下载的服务器路径：";
            this.lblSupplyDownloadServerPath.Visible = false;
            // 
            // btnBrowserSupplyDownloadServerPath
            // 
            this.btnBrowserSupplyDownloadServerPath.Location = new System.Drawing.Point(414, 103);
            this.btnBrowserSupplyDownloadServerPath.Name = "btnBrowserSupplyDownloadServerPath";
            this.btnBrowserSupplyDownloadServerPath.Size = new System.Drawing.Size(27, 22);
            this.btnBrowserSupplyDownloadServerPath.TabIndex = 8;
            this.btnBrowserSupplyDownloadServerPath.Text = "...";
            this.btnBrowserSupplyDownloadServerPath.UseVisualStyleBackColor = true;
            this.btnBrowserSupplyDownloadServerPath.Visible = false;
            this.btnBrowserSupplyDownloadServerPath.Click += new System.EventHandler(this.btnBrowserSupplyDownloadServerPath_Click);
            // 
            // btnCopyToSupplyDownloadServerPath
            // 
            this.btnCopyToSupplyDownloadServerPath.Location = new System.Drawing.Point(7, 152);
            this.btnCopyToSupplyDownloadServerPath.Name = "btnCopyToSupplyDownloadServerPath";
            this.btnCopyToSupplyDownloadServerPath.Size = new System.Drawing.Size(263, 23);
            this.btnCopyToSupplyDownloadServerPath.TabIndex = 9;
            this.btnCopyToSupplyDownloadServerPath.Text = "拷贝文件到供客户端下载的服务器目录下(&D)";
            this.btnCopyToSupplyDownloadServerPath.UseVisualStyleBackColor = true;
            this.btnCopyToSupplyDownloadServerPath.Visible = false;
            this.btnCopyToSupplyDownloadServerPath.Click += new System.EventHandler(this.btnCopyToSupplyDownloadServerPath_Click);
            // 
            // Mainfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 189);
            this.Controls.Add(this.btnCopyToSupplyDownloadServerPath);
            this.Controls.Add(this.btnBrowserSupplyDownloadServerPath);
            this.Controls.Add(this.lblSupplyDownloadServerPath);
            this.Controls.Add(this.tbxSupplyDownloadServerPath);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.tbxPath);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mainfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CAPP更新文件列表生成器";
            this.Load += new System.EventHandler(this.Mainfrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox tbxPath;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.FolderBrowserDialog fbdPath;
        private System.Windows.Forms.TextBox tbxSupplyDownloadServerPath;
        private System.Windows.Forms.Label lblSupplyDownloadServerPath;
        private System.Windows.Forms.Button btnBrowserSupplyDownloadServerPath;
        private System.Windows.Forms.Button btnCopyToSupplyDownloadServerPath;
        private System.Windows.Forms.FolderBrowserDialog fdbDownloadForServerPath;
    }
}

