namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class ProcessPlanningModuleDetailFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPlanningModuleDetailFrm));
            this.pnlPlanningModule = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlPlanningModule
            // 
            this.pnlPlanningModule.Location = new System.Drawing.Point(12, 12);
            this.pnlPlanningModule.Name = "pnlPlanningModule";
            this.pnlPlanningModule.Size = new System.Drawing.Size(571, 304);
            this.pnlPlanningModule.TabIndex = 0;
            // 
            // ProcessPlanningModuleDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(595, 328);
            this.Controls.Add(this.pnlPlanningModule);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProcessPlanningModuleDetailFrm";
            this.TabText = "工艺规程模板";
            this.Text = "工艺规程模板";
            this.Load += new System.EventHandler(this.ProcessPlanningModuleDetailFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlanningModule;


    }
}