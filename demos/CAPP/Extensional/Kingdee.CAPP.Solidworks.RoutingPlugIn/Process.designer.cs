namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    partial class Process
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
            this.lblProcessName = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblComponentList = new System.Windows.Forms.Label();
            this.cbxComponentList = new System.Windows.Forms.ComboBox();
            this.btnCorrelationComponentToRouting = new System.Windows.Forms.Button();
            this.tcNewProcess = new System.Windows.Forms.TabControl();
            this.tbAddNewComponentToAssembly = new System.Windows.Forms.TabPage();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.tbxSelectedComponent = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddPartToAssembly = new System.Windows.Forms.Button();
            this.tbxProcessName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxProcessCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tpCorrelationToRouting = new System.Windows.Forms.TabPage();
            this.cbxProcessName = new System.Windows.Forms.ComboBox();
            this.tcNewProcess.SuspendLayout();
            this.tbAddNewComponentToAssembly.SuspendLayout();
            this.tpCorrelationToRouting.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblProcessName
            // 
            this.lblProcessName.AutoSize = true;
            this.lblProcessName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProcessName.Location = new System.Drawing.Point(16, 41);
            this.lblProcessName.Name = "lblProcessName";
            this.lblProcessName.Size = new System.Drawing.Size(98, 13);
            this.lblProcessName.TabIndex = 21;
            this.lblProcessName.Text = "选择工序名称：";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(235, 122);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 23);
            this.btnClose.TabIndex = 17;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblComponentList
            // 
            this.lblComponentList.AutoSize = true;
            this.lblComponentList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblComponentList.Location = new System.Drawing.Point(42, 76);
            this.lblComponentList.Name = "lblComponentList";
            this.lblComponentList.Size = new System.Drawing.Size(72, 13);
            this.lblComponentList.TabIndex = 24;
            this.lblComponentList.Text = "选择组件：";
            // 
            // cbxComponentList
            // 
            this.cbxComponentList.FormattingEnabled = true;
            this.cbxComponentList.Location = new System.Drawing.Point(118, 73);
            this.cbxComponentList.Name = "cbxComponentList";
            this.cbxComponentList.Size = new System.Drawing.Size(240, 21);
            this.cbxComponentList.TabIndex = 25;
            // 
            // btnCorrelationComponentToRouting
            // 
            this.btnCorrelationComponentToRouting.Location = new System.Drawing.Point(61, 121);
            this.btnCorrelationComponentToRouting.Name = "btnCorrelationComponentToRouting";
            this.btnCorrelationComponentToRouting.Size = new System.Drawing.Size(131, 23);
            this.btnCorrelationComponentToRouting.TabIndex = 26;
            this.btnCorrelationComponentToRouting.Text = "关联组件到工艺路线";
            this.btnCorrelationComponentToRouting.UseVisualStyleBackColor = true;
            this.btnCorrelationComponentToRouting.Click += new System.EventHandler(this.btnCorrelationComponentToRouting_Click);
            // 
            // tcNewProcess
            // 
            this.tcNewProcess.Controls.Add(this.tbAddNewComponentToAssembly);
            this.tcNewProcess.Controls.Add(this.tpCorrelationToRouting);
            this.tcNewProcess.Location = new System.Drawing.Point(0, 1);
            this.tcNewProcess.Name = "tcNewProcess";
            this.tcNewProcess.SelectedIndex = 0;
            this.tcNewProcess.Size = new System.Drawing.Size(425, 230);
            this.tcNewProcess.TabIndex = 28;
            // 
            // tbAddNewComponentToAssembly
            // 
            this.tbAddNewComponentToAssembly.Controls.Add(this.btnSelectFile);
            this.tbAddNewComponentToAssembly.Controls.Add(this.tbxSelectedComponent);
            this.tbAddNewComponentToAssembly.Controls.Add(this.button1);
            this.tbAddNewComponentToAssembly.Controls.Add(this.btnAddPartToAssembly);
            this.tbAddNewComponentToAssembly.Controls.Add(this.tbxProcessName);
            this.tbAddNewComponentToAssembly.Controls.Add(this.label1);
            this.tbAddNewComponentToAssembly.Controls.Add(this.label2);
            this.tbAddNewComponentToAssembly.Controls.Add(this.tbxProcessCode);
            this.tbAddNewComponentToAssembly.Controls.Add(this.label3);
            this.tbAddNewComponentToAssembly.Location = new System.Drawing.Point(4, 22);
            this.tbAddNewComponentToAssembly.Name = "tbAddNewComponentToAssembly";
            this.tbAddNewComponentToAssembly.Padding = new System.Windows.Forms.Padding(3);
            this.tbAddNewComponentToAssembly.Size = new System.Drawing.Size(417, 204);
            this.tbAddNewComponentToAssembly.TabIndex = 1;
            this.tbAddNewComponentToAssembly.Text = "增加新零件到装配";
            this.tbAddNewComponentToAssembly.UseVisualStyleBackColor = true;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(104, 76);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(75, 23);
            this.btnSelectFile.TabIndex = 38;
            this.btnSelectFile.Text = "浏览";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // tbxSelectedComponent
            // 
            this.tbxSelectedComponent.BackColor = System.Drawing.SystemColors.Info;
            this.tbxSelectedComponent.Location = new System.Drawing.Point(103, 105);
            this.tbxSelectedComponent.Name = "tbxSelectedComponent";
            this.tbxSelectedComponent.ReadOnly = true;
            this.tbxSelectedComponent.Size = new System.Drawing.Size(258, 20);
            this.tbxSelectedComponent.TabIndex = 37;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(217, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 23);
            this.button1.TabIndex = 28;
            this.button1.Text = "关闭";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddPartToAssembly
            // 
            this.btnAddPartToAssembly.Location = new System.Drawing.Point(71, 148);
            this.btnAddPartToAssembly.Name = "btnAddPartToAssembly";
            this.btnAddPartToAssembly.Size = new System.Drawing.Size(116, 23);
            this.btnAddPartToAssembly.TabIndex = 29;
            this.btnAddPartToAssembly.Text = "增加新零件到装配";
            this.btnAddPartToAssembly.UseVisualStyleBackColor = true;
            this.btnAddPartToAssembly.Click += new System.EventHandler(this.btnAddPartToAssembly_Click);
            // 
            // tbxProcessName
            // 
            this.tbxProcessName.Location = new System.Drawing.Point(103, 14);
            this.tbxProcessName.Name = "tbxProcessName";
            this.tbxProcessName.Size = new System.Drawing.Size(258, 20);
            this.tbxProcessName.TabIndex = 30;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(25, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "选择文件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(26, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "工序名称：";
            // 
            // tbxProcessCode
            // 
            this.tbxProcessCode.Location = new System.Drawing.Point(103, 49);
            this.tbxProcessCode.Name = "tbxProcessCode";
            this.tbxProcessCode.Size = new System.Drawing.Size(259, 20);
            this.tbxProcessCode.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(24, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "工序代码：";
            // 
            // tpCorrelationToRouting
            // 
            this.tpCorrelationToRouting.Controls.Add(this.cbxProcessName);
            this.tpCorrelationToRouting.Controls.Add(this.cbxComponentList);
            this.tpCorrelationToRouting.Controls.Add(this.btnClose);
            this.tpCorrelationToRouting.Controls.Add(this.btnCorrelationComponentToRouting);
            this.tpCorrelationToRouting.Controls.Add(this.lblComponentList);
            this.tpCorrelationToRouting.Controls.Add(this.lblProcessName);
            this.tpCorrelationToRouting.Location = new System.Drawing.Point(4, 22);
            this.tpCorrelationToRouting.Name = "tpCorrelationToRouting";
            this.tpCorrelationToRouting.Padding = new System.Windows.Forms.Padding(3);
            this.tpCorrelationToRouting.Size = new System.Drawing.Size(417, 204);
            this.tpCorrelationToRouting.TabIndex = 0;
            this.tpCorrelationToRouting.Text = "关联组件到工艺路线";
            this.tpCorrelationToRouting.UseVisualStyleBackColor = true;
            // 
            // cbxProcessName
            // 
            this.cbxProcessName.FormattingEnabled = true;
            this.cbxProcessName.Location = new System.Drawing.Point(118, 37);
            this.cbxProcessName.Name = "cbxProcessName";
            this.cbxProcessName.Size = new System.Drawing.Size(240, 21);
            this.cbxProcessName.TabIndex = 27;
            // 
            // Process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(404, 212);
            this.Controls.Add(this.tcNewProcess);
            this.Name = "Process";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建工序";
            this.tcNewProcess.ResumeLayout(false);
            this.tbAddNewComponentToAssembly.ResumeLayout(false);
            this.tbAddNewComponentToAssembly.PerformLayout();
            this.tpCorrelationToRouting.ResumeLayout(false);
            this.tpCorrelationToRouting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblProcessName;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblComponentList;
        private System.Windows.Forms.ComboBox cbxComponentList;
        private System.Windows.Forms.Button btnCorrelationComponentToRouting;
        private System.Windows.Forms.TabControl tcNewProcess;
        private System.Windows.Forms.TabPage tpCorrelationToRouting;
        private System.Windows.Forms.TabPage tbAddNewComponentToAssembly;
        private System.Windows.Forms.TextBox tbxSelectedComponent;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAddPartToAssembly;
        private System.Windows.Forms.TextBox tbxProcessName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxProcessCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ComboBox cbxProcessName;
    }
}