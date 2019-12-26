namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    partial class ProcessLine
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
            this.contextMenuScript1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.AddNewProcessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvProcessLine = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.contextMenuScript1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuScript1
            // 
            this.contextMenuScript1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddNewProcessToolStripMenuItem});
            this.contextMenuScript1.Name = "contextMenuScript1";
            this.contextMenuScript1.Size = new System.Drawing.Size(159, 48);
            this.contextMenuScript1.Click += new System.EventHandler(this.RefProcessToSketchToolStripMenuItem_Click);
            // 
            // AddNewProcessToolStripMenuItem
            // 
            this.AddNewProcessToolStripMenuItem.Name = "AddNewProcessToolStripMenuItem";
            this.AddNewProcessToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.AddNewProcessToolStripMenuItem.Text = "关联草图到工序";
            // 
            // tvProcessLine
            // 
            this.tvProcessLine.Location = new System.Drawing.Point(0, 22);
            this.tvProcessLine.Name = "tvProcessLine";
            this.tvProcessLine.Size = new System.Drawing.Size(410, 869);
            this.tvProcessLine.TabIndex = 0;
            this.tvProcessLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessLine_MouseDown);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "   ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(165, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "   ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "：已经和草图关联的工序";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "：还未和草图关联的工序";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(0, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 21);
            this.panel1.TabIndex = 3;
            // 
            // ProcessLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 812);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tvProcessLine);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ProcessLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "工艺路线";
            this.contextMenuScript1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuScript1;
        private System.Windows.Forms.ToolStripMenuItem AddNewProcessToolStripMenuItem;
        private System.Windows.Forms.TreeView tvProcessLine;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;

    }
}