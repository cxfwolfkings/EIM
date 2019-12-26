namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class ProcessPlanningDetailFrm
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
            this.pnlPlanningCard = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlPlanningCard
            // 
            this.pnlPlanningCard.Location = new System.Drawing.Point(12, 12);
            this.pnlPlanningCard.Name = "pnlPlanningCard";
            this.pnlPlanningCard.Size = new System.Drawing.Size(571, 304);
            this.pnlPlanningCard.TabIndex = 1;
            // 
            // ProcessPlanningDetailFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(596, 330);
            this.Controls.Add(this.pnlPlanningCard);
            this.Name = "ProcessPlanningDetailFrm";
            this.TabText = "工艺规程卡片";
            this.Text = "工艺规程卡片";
            this.Load += new System.EventHandler(this.ProcessPlanningDetailFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlanningCard;
    }
}