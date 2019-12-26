namespace Kingdee.CAPP.UI.ProcessDesign
{
    partial class SpecialSymbolFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecialSymbolFrm));
            this.dgvSymbol = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.innerPanel = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnModify = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.customScrollbar = new Kingdee.CAPP.Controls.CustomScrollbar();
            this.pnBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbol)).BeginInit();
            this.panel1.SuspendLayout();
            this.innerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Controls.Add(this.panel1);
            this.pnBody.Controls.Add(this.customScrollbar);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.btnSave);
            this.pnBody.Controls.Add(this.btnModify);
            this.pnBody.Size = new System.Drawing.Size(421, 329);
            // 
            // dgvSymbol
            // 
            this.dgvSymbol.AllowUserToAddRows = false;
            this.dgvSymbol.AllowUserToDeleteRows = false;
            this.dgvSymbol.AllowUserToResizeColumns = false;
            this.dgvSymbol.AllowUserToResizeRows = false;
            this.dgvSymbol.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSymbol.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSymbol.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSymbol.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSymbol.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSymbol.Location = new System.Drawing.Point(0, 0);
            this.dgvSymbol.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgvSymbol.Name = "dgvSymbol";
            this.dgvSymbol.RowHeadersVisible = false;
            this.dgvSymbol.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvSymbol.Size = new System.Drawing.Size(302, 302);
            this.dgvSymbol.TabIndex = 0;
            this.dgvSymbol.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvSymbol_CellBeginEdit);
            this.dgvSymbol.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSymbol_CellDoubleClick);
            this.dgvSymbol.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSymbol_CellEndEdit);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.innerPanel);
            this.panel1.Location = new System.Drawing.Point(11, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 307);
            this.panel1.TabIndex = 1;
            // 
            // innerPanel
            // 
            this.innerPanel.AutoScroll = true;
            this.innerPanel.Controls.Add(this.dgvSymbol);
            this.innerPanel.Location = new System.Drawing.Point(0, 0);
            this.innerPanel.Name = "innerPanel";
            this.innerPanel.Size = new System.Drawing.Size(321, 303);
            this.innerPanel.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(339, 9);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(70, 25);
            this.btnConfirm.TabIndex = 1;
            this.btnConfirm.Text = "确定(&A)";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(339, 42);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnModify
            // 
            this.btnModify.FlatAppearance.BorderSize = 0;
            this.btnModify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModify.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModify.Location = new System.Drawing.Point(339, 252);
            this.btnModify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(70, 25);
            this.btnModify.TabIndex = 3;
            this.btnModify.Text = "编辑(&E)";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // btnSave
            // 
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(339, 290);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 25);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存(&S)";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // customScrollbar
            // 
            this.customScrollbar.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(236)))));
            this.customScrollbar.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.DownArrowImage")));
            this.customScrollbar.LargeChange = 10;
            this.customScrollbar.Location = new System.Drawing.Point(316, 10);
            this.customScrollbar.Maximum = 100;
            this.customScrollbar.Minimum = 0;
            this.customScrollbar.MinimumSize = new System.Drawing.Size(17, 90);
            this.customScrollbar.Name = "customScrollbar";
            this.customScrollbar.Size = new System.Drawing.Size(17, 307);
            this.customScrollbar.SmallChange = 1;
            this.customScrollbar.TabIndex = 27;
            this.customScrollbar.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbBottomImage")));
            this.customScrollbar.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbBottomSpanImage")));
            this.customScrollbar.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbMiddleImage")));
            this.customScrollbar.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbTopImage")));
            this.customScrollbar.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.ThumbTopSpanImage")));
            this.customScrollbar.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("customScrollbar.UpArrowImage")));
            this.customScrollbar.Value = 0;
            this.customScrollbar.Scroll += new System.EventHandler(this.customScrollbar_Scroll);
            // 
            // SpecialSymbolFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 353);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "SpecialSymbolFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "特殊符号";
            this.Load += new System.EventHandler(this.SpecialSymbolFrm_Load);
            this.pnBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSymbol)).EndInit();
            this.panel1.ResumeLayout(false);
            this.innerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSymbol;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel innerPanel;
        private Controls.CustomScrollbar customScrollbar;

    }
}