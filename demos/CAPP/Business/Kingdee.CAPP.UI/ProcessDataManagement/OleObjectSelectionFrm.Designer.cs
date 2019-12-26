namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class OleObjectSelectionFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OleObjectSelectionFrm));
            this.lbObjectType = new System.Windows.Forms.ListBox();
            this.cbDisplayAsIco = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbOle = new System.Windows.Forms.PictureBox();
            this.cbDisplayAsIcon = new System.Windows.Forms.CheckBox();
            this.cbLinkToFile = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pbLink = new System.Windows.Forms.PictureBox();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcOleObject = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tpCreate = new System.Windows.Forms.TabPage();
            this.tpCreateFromFile = new System.Windows.Forms.TabPage();
            this.pnBody.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbOle)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLink)).BeginInit();
            this.tcOleObject.SuspendLayout();
            this.tpCreate.SuspendLayout();
            this.tpCreateFromFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.tcOleObject);
            this.pnBody.Size = new System.Drawing.Size(505, 358);
            // 
            // lbObjectType
            // 
            this.lbObjectType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.lbObjectType.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbObjectType.FormattingEnabled = true;
            this.lbObjectType.ItemHeight = 17;
            this.lbObjectType.Location = new System.Drawing.Point(6, 25);
            this.lbObjectType.Name = "lbObjectType";
            this.lbObjectType.Size = new System.Drawing.Size(348, 153);
            this.lbObjectType.TabIndex = 4;
            this.lbObjectType.SelectedIndexChanged += new System.EventHandler(this.lbObjectType_SelectedIndexChanged);
            // 
            // cbDisplayAsIco
            // 
            this.cbDisplayAsIco.AutoSize = true;
            this.cbDisplayAsIco.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbDisplayAsIco.FlatAppearance.BorderSize = 0;
            this.cbDisplayAsIco.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDisplayAsIco.Location = new System.Drawing.Point(360, 161);
            this.cbDisplayAsIco.Name = "cbDisplayAsIco";
            this.cbDisplayAsIco.Size = new System.Drawing.Size(108, 21);
            this.cbDisplayAsIco.TabIndex = 3;
            this.cbDisplayAsIco.Text = "显示为一个图标";
            this.cbDisplayAsIco.UseVisualStyleBackColor = true;
            this.cbDisplayAsIco.CheckedChanged += new System.EventHandler(this.cbDisplayAsIco_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "对象类型:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbOle);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(6, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 87);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "结果";
            // 
            // pbOle
            // 
            this.pbOle.Image = global::Kingdee.CAPP.UI.Properties.Resources.ole;
            this.pbOle.Location = new System.Drawing.Point(11, 33);
            this.pbOle.Name = "pbOle";
            this.pbOle.Size = new System.Drawing.Size(43, 35);
            this.pbOle.TabIndex = 0;
            this.pbOle.TabStop = false;
            // 
            // cbDisplayAsIcon
            // 
            this.cbDisplayAsIcon.AutoSize = true;
            this.cbDisplayAsIcon.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbDisplayAsIcon.FlatAppearance.BorderSize = 0;
            this.cbDisplayAsIcon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbDisplayAsIcon.Location = new System.Drawing.Point(360, 156);
            this.cbDisplayAsIcon.Name = "cbDisplayAsIcon";
            this.cbDisplayAsIcon.Size = new System.Drawing.Size(108, 21);
            this.cbDisplayAsIcon.TabIndex = 5;
            this.cbDisplayAsIcon.Text = "显示为一个图标";
            this.cbDisplayAsIcon.UseVisualStyleBackColor = true;
            this.cbDisplayAsIcon.CheckedChanged += new System.EventHandler(this.cbDisplayAsIcon_CheckedChanged);
            // 
            // cbLinkToFile
            // 
            this.cbLinkToFile.AutoSize = true;
            this.cbLinkToFile.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbLinkToFile.FlatAppearance.BorderSize = 0;
            this.cbLinkToFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbLinkToFile.Location = new System.Drawing.Point(360, 127);
            this.cbLinkToFile.Name = "cbLinkToFile";
            this.cbLinkToFile.Size = new System.Drawing.Size(84, 21);
            this.cbLinkToFile.TabIndex = 4;
            this.cbLinkToFile.Text = "链接到文件";
            this.cbLinkToFile.UseVisualStyleBackColor = true;
            this.cbLinkToFile.CheckedChanged += new System.EventHandler(this.cbLinkToFile_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pbLink);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(6, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(348, 87);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "结果";
            // 
            // pbLink
            // 
            this.pbLink.Image = global::Kingdee.CAPP.UI.Properties.Resources.ole;
            this.pbLink.Location = new System.Drawing.Point(12, 34);
            this.pbLink.Name = "pbLink";
            this.pbLink.Size = new System.Drawing.Size(51, 35);
            this.pbLink.TabIndex = 1;
            this.pbLink.TabStop = false;
            // 
            // btnBrowser
            // 
            this.btnBrowser.FlatAppearance.BorderSize = 0;
            this.btnBrowser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowser.Location = new System.Drawing.Point(360, 25);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(75, 24);
            this.btnBrowser.TabIndex = 2;
            this.btnBrowser.Text = "浏览...";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileName.Location = new System.Drawing.Point(9, 26);
            this.txtFileName.Multiline = true;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(345, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "文件名:";
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Location = new System.Drawing.Point(333, 321);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 24);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(414, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tcOleObject
            // 
            this.tcOleObject.Controls.Add(this.tpCreate);
            this.tcOleObject.Controls.Add(this.tpCreateFromFile);
            this.tcOleObject.Location = new System.Drawing.Point(12, 1);
            this.tcOleObject.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcOleObject.Name = "tcOleObject";
            this.tcOleObject.SelectedIndex = 0;
            this.tcOleObject.Size = new System.Drawing.Size(481, 318);
            this.tcOleObject.TabIndex = 3;
            // 
            // tpCreate
            // 
            this.tpCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpCreate.Controls.Add(this.lbObjectType);
            this.tpCreate.Controls.Add(this.groupBox1);
            this.tpCreate.Controls.Add(this.cbDisplayAsIco);
            this.tpCreate.Controls.Add(this.label1);
            this.tpCreate.Location = new System.Drawing.Point(4, 25);
            this.tpCreate.Name = "tpCreate";
            this.tpCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreate.Size = new System.Drawing.Size(473, 289);
            this.tpCreate.TabIndex = 0;
            this.tpCreate.Text = "创建OLE对象";
            // 
            // tpCreateFromFile
            // 
            this.tpCreateFromFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpCreateFromFile.Controls.Add(this.cbDisplayAsIcon);
            this.tpCreateFromFile.Controls.Add(this.groupBox2);
            this.tpCreateFromFile.Controls.Add(this.cbLinkToFile);
            this.tpCreateFromFile.Controls.Add(this.label2);
            this.tpCreateFromFile.Controls.Add(this.txtFileName);
            this.tpCreateFromFile.Controls.Add(this.btnBrowser);
            this.tpCreateFromFile.Location = new System.Drawing.Point(4, 25);
            this.tpCreateFromFile.Name = "tpCreateFromFile";
            this.tpCreateFromFile.Padding = new System.Windows.Forms.Padding(3);
            this.tpCreateFromFile.Size = new System.Drawing.Size(473, 289);
            this.tpCreateFromFile.TabIndex = 1;
            this.tpCreateFromFile.Text = "从文件创建OLE对象";
            // 
            // OleObjectSelectionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 380);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "OleObjectSelectionFrm";
            this.Text = "OLE对象";
            this.Load += new System.EventHandler(this.OleObjectSelectionFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbOle)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLink)).EndInit();
            this.tcOleObject.ResumeLayout(false);
            this.tpCreate.ResumeLayout(false);
            this.tpCreate.PerformLayout();
            this.tpCreateFromFile.ResumeLayout(false);
            this.tpCreateFromFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDisplayAsIco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ListBox lbObjectType;
        private System.Windows.Forms.CheckBox cbDisplayAsIcon;
        private System.Windows.Forms.CheckBox cbLinkToFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbOle;
        private System.Windows.Forms.PictureBox pbLink;
        private Controls.FlatTabControl tcOleObject;
        private System.Windows.Forms.TabPage tpCreate;
        private System.Windows.Forms.TabPage tpCreateFromFile;
    }
}