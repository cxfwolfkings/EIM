namespace Kingdee.CAPP.UI
{
    partial class PropertiesNavigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesNavigate));
            this.pgrdCellProperty = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // pgrdCellProperty
            // 
            this.pgrdCellProperty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pgrdCellProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgrdCellProperty.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgrdCellProperty.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pgrdCellProperty.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.pgrdCellProperty.Location = new System.Drawing.Point(0, 0);
            this.pgrdCellProperty.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pgrdCellProperty.Name = "pgrdCellProperty";
            this.pgrdCellProperty.Size = new System.Drawing.Size(185, 526);
            this.pgrdCellProperty.TabIndex = 3;
            this.pgrdCellProperty.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgrdCellProperty_PropertyValueChanged);
            // 
            // PropertiesNavigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(185, 526);
            this.Controls.Add(this.pgrdCellProperty);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PropertiesNavigate";
            this.TabText = "单元格属性";
            this.Text = "单元格属性";
            this.Load += new System.EventHandler(this.PropertiesNavigate_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgrdCellProperty;
    }
}