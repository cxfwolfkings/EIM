namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProcessPlanningChooseFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPlanningChooseFrm));
            this.tvProcessProcedure = new System.Windows.Forms.TreeView();
            this.imgProcessProcedureList = new System.Windows.Forms.ImageList(this.components);
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.tvProcessProcedure);
            this.pnBody.Size = new System.Drawing.Size(336, 304);
            // 
            // tvProcessProcedure
            // 
            this.tvProcessProcedure.ImageIndex = 0;
            this.tvProcessProcedure.ImageList = this.imgProcessProcedureList;
            this.tvProcessProcedure.Location = new System.Drawing.Point(10, 0);
            this.tvProcessProcedure.Name = "tvProcessProcedure";
            this.tvProcessProcedure.SelectedImageIndex = 0;
            this.tvProcessProcedure.Size = new System.Drawing.Size(316, 264);
            this.tvProcessProcedure.TabIndex = 0;
            this.tvProcessProcedure.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessProcedure_MouseDown);
            // 
            // imgProcessProcedureList
            // 
            this.imgProcessProcedureList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgProcessProcedureList.ImageStream")));
            this.imgProcessProcedureList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgProcessProcedureList.Images.SetKeyName(0, "planning");
            this.imgProcessProcedureList.Images.SetKeyName(1, "card");
            this.imgProcessProcedureList.Images.SetKeyName(2, "folder");
            this.imgProcessProcedureList.Images.SetKeyName(3, "folder_o");
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(210, 270);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(55, 23);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(271, 270);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(55, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ProcessPlanningChooseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 328);
            this.Name = "ProcessPlanningChooseFrm";
            this.Text = "工艺规程选择";
            this.Load += new System.EventHandler(this.ProcessPlanningChooseFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TreeView tvProcessProcedure;
        private System.Windows.Forms.ImageList imgProcessProcedureList;
    }
}