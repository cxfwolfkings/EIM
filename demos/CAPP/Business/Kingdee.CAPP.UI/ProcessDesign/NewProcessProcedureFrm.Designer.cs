namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class NewProcessProcedureFrm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProcessProcedureFrm));
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbNewProcessPlanning = new System.Windows.Forms.GroupBox();
            this.lblProcessPlanningName = new System.Windows.Forms.Label();
            this.txtProcessPlanningName = new System.Windows.Forms.TextBox();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cklProcessCardModuleNames = new System.Windows.Forms.CheckedListBox();
            this.btmChoose = new System.Windows.Forms.Button();
            this.tcProcessCardManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvProcessCard = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.pnBody.SuspendLayout();
            this.gbNewProcessPlanning.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tcProcessCardManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.tcProcessCardManager);
            this.pnBody.Controls.Add(this.btmChoose);
            this.pnBody.Controls.Add(this.BtnConfirm);
            this.pnBody.Controls.Add(this.gbNewProcessPlanning);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.groupBox1);
            this.pnBody.Size = new System.Drawing.Size(557, 245);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
            this.dataGridViewTextBoxColumn1.HeaderText = "工艺规程模板标识";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn2.HeaderText = "工艺规程模板名称";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // gbNewProcessPlanning
            // 
            this.gbNewProcessPlanning.Controls.Add(this.lblProcessPlanningName);
            this.gbNewProcessPlanning.Controls.Add(this.txtProcessPlanningName);
            this.gbNewProcessPlanning.Location = new System.Drawing.Point(189, 1);
            this.gbNewProcessPlanning.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbNewProcessPlanning.Name = "gbNewProcessPlanning";
            this.gbNewProcessPlanning.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gbNewProcessPlanning.Size = new System.Drawing.Size(354, 54);
            this.gbNewProcessPlanning.TabIndex = 0;
            this.gbNewProcessPlanning.TabStop = false;
            // 
            // lblProcessPlanningName
            // 
            this.lblProcessPlanningName.AutoSize = true;
            this.lblProcessPlanningName.Location = new System.Drawing.Point(10, 20);
            this.lblProcessPlanningName.Name = "lblProcessPlanningName";
            this.lblProcessPlanningName.Size = new System.Drawing.Size(92, 17);
            this.lblProcessPlanningName.TabIndex = 1;
            this.lblProcessPlanningName.Text = "工艺规程名称：";
            // 
            // txtProcessPlanningName
            // 
            this.txtProcessPlanningName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProcessPlanningName.Location = new System.Drawing.Point(108, 20);
            this.txtProcessPlanningName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtProcessPlanningName.Multiline = true;
            this.txtProcessPlanningName.Name = "txtProcessPlanningName";
            this.txtProcessPlanningName.Size = new System.Drawing.Size(240, 20);
            this.txtProcessPlanningName.TabIndex = 0;
            this.txtProcessPlanningName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProcessPlanningName_KeyDown);
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.FlatAppearance.BorderSize = 0;
            this.BtnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConfirm.Location = new System.Drawing.Point(364, 209);
            this.BtnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(87, 23);
            this.BtnConfirm.TabIndex = 2;
            this.BtnConfirm.Text = "确定";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(457, 209);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cklProcessCardModuleNames);
            this.groupBox1.Location = new System.Drawing.Point(189, 63);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(354, 138);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "卡片模板";
            // 
            // cklProcessCardModuleNames
            // 
            this.cklProcessCardModuleNames.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cklProcessCardModuleNames.FormattingEnabled = true;
            this.cklProcessCardModuleNames.Location = new System.Drawing.Point(6, 18);
            this.cklProcessCardModuleNames.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cklProcessCardModuleNames.Name = "cklProcessCardModuleNames";
            this.cklProcessCardModuleNames.Size = new System.Drawing.Size(342, 108);
            this.cklProcessCardModuleNames.TabIndex = 5;
            // 
            // btmChoose
            // 
            this.btmChoose.FlatAppearance.BorderSize = 0;
            this.btmChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btmChoose.Location = new System.Drawing.Point(283, 209);
            this.btmChoose.Name = "btmChoose";
            this.btmChoose.Size = new System.Drawing.Size(75, 23);
            this.btmChoose.TabIndex = 7;
            this.btmChoose.Text = "选择模板";
            this.btmChoose.UseVisualStyleBackColor = true;
            this.btmChoose.Click += new System.EventHandler(this.btmChoose_Click);
            // 
            // tcProcessCardManager
            // 
            this.tcProcessCardManager.Controls.Add(this.tabPage1);
            this.tcProcessCardManager.Location = new System.Drawing.Point(9, 6);
            this.tcProcessCardManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProcessCardManager.Name = "tcProcessCardManager";
            this.tcProcessCardManager.SelectedIndex = 0;
            this.tcProcessCardManager.Size = new System.Drawing.Size(175, 225);
            this.tcProcessCardManager.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tabPage1.Controls.Add(this.tvProcessCard);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(167, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工艺文件夹";
            // 
            // tvProcessCard
            // 
            this.tvProcessCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProcessCard.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvProcessCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProcessCard.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tvProcessCard.HideSelection = false;
            this.tvProcessCard.ImageIndex = 0;
            this.tvProcessCard.ImageList = this.imgList;
            this.tvProcessCard.Location = new System.Drawing.Point(3, 3);
            this.tvProcessCard.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvProcessCard.Name = "tvProcessCard";
            this.tvProcessCard.SelectedImageIndex = 0;
            this.tvProcessCard.Size = new System.Drawing.Size(161, 190);
            this.tvProcessCard.TabIndex = 0;
            this.tvProcessCard.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterCollapse);
            this.tvProcessCard.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterExpand);
            this.tvProcessCard.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvProcessCard_DrawNode);
            this.tvProcessCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessCard_MouseDown);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "folder_o");
            // 
            // NewProcessProcedureFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 267);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "NewProcessProcedureFrm";
            this.Text = "选择工艺规程模板";
            this.Load += new System.EventHandler(this.NewProcessProcedureFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.gbNewProcessPlanning.ResumeLayout(false);
            this.gbNewProcessPlanning.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tcProcessCardManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.GroupBox gbNewProcessPlanning;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Label lblProcessPlanningName;
        private System.Windows.Forms.TextBox txtProcessPlanningName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox cklProcessCardModuleNames;
        private System.Windows.Forms.Button btmChoose;
        private Controls.FlatTabControl tcProcessCardManager;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvProcessCard;
        private System.Windows.Forms.ImageList imgList;
    }
}