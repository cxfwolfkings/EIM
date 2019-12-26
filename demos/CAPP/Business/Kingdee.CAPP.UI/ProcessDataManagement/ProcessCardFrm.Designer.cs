namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class ProcessCardFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessCardFrm));
            this.pnCard = new System.Windows.Forms.Panel();
            this.dgvCard = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuImportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuExportDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuSaveToBmp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuOpenCard = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuOleObject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuCellProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuAllLine = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuNewLine = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPaster = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuCut = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuTextBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuSymbol = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuRoughnessMark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuWeldingSymbol = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuShapeTolerance = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuToleranceMark = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuSuperscriptAndSubscript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuTextCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuTextPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuTextCut = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.cmnuTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmnuFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuCloseCurrent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuCloseOther = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmnuCloseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmnuInsertRow = new System.Windows.Forms.ToolStripMenuItem();
            this.pnCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCard)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.contextMenuTextBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.cmnuTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnCard
            // 
            this.pnCard.BackColor = System.Drawing.Color.White;
            this.pnCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnCard.Controls.Add(this.dgvCard);
            this.pnCard.Location = new System.Drawing.Point(9, 9);
            this.pnCard.Margin = new System.Windows.Forms.Padding(0);
            this.pnCard.Name = "pnCard";
            this.pnCard.Size = new System.Drawing.Size(629, 454);
            this.pnCard.TabIndex = 5;
            // 
            // dgvCard
            // 
            this.dgvCard.AllowUserToAddRows = false;
            this.dgvCard.AllowUserToDeleteRows = false;
            this.dgvCard.AllowUserToResizeColumns = false;
            this.dgvCard.AllowUserToResizeRows = false;
            this.dgvCard.BackgroundColor = System.Drawing.Color.White;
            this.dgvCard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCard.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCard.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCard.Location = new System.Drawing.Point(0, 0);
            this.dgvCard.Margin = new System.Windows.Forms.Padding(0);
            this.dgvCard.Name = "dgvCard";
            this.dgvCard.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCard.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCard.RowTemplate.Height = 23;
            this.dgvCard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvCard.Size = new System.Drawing.Size(627, 452);
            this.dgvCard.TabIndex = 3;
            this.dgvCard.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvCard_CellBeginEdit);
            this.dgvCard.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCard_CellClick);
            this.dgvCard.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellDoubleClick);
            this.dgvCard.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_CellMouseDown);
            this.dgvCard.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvCard_CellPainting);
            this.dgvCard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DataGridView_KeyDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuImportDetail,
            this.tsmnuExportDetail,
            this.tsmnuSaveToBmp,
            this.toolStripSeparator1,
            this.tsmnuSave,
            this.tsmnuOpenCard,
            this.toolStripSeparator2,
            this.tsmnuOleObject,
            this.tsmnuCellProperty,
            this.tsmnuAllLine,
            this.tsmnuNewLine,
            this.toolStripSeparator4,
            this.tsmnuCopy,
            this.tsPaster,
            this.tsmnuCut,
            this.tsmnuInsertRow});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(173, 330);
            // 
            // tsmnuImportDetail
            // 
            this.tsmnuImportDetail.Name = "tsmnuImportDetail";
            this.tsmnuImportDetail.Size = new System.Drawing.Size(172, 22);
            this.tsmnuImportDetail.Text = "导入数据到明细框";
            this.tsmnuImportDetail.Visible = false;
            this.tsmnuImportDetail.Click += new System.EventHandler(this.tsmnuImportDetail_Click);
            // 
            // tsmnuExportDetail
            // 
            this.tsmnuExportDetail.Name = "tsmnuExportDetail";
            this.tsmnuExportDetail.Size = new System.Drawing.Size(172, 22);
            this.tsmnuExportDetail.Text = "导出明细列表数据";
            this.tsmnuExportDetail.Visible = false;
            this.tsmnuExportDetail.Click += new System.EventHandler(this.tsmnuExportDetail_Click);
            // 
            // tsmnuSaveToBmp
            // 
            this.tsmnuSaveToBmp.Name = "tsmnuSaveToBmp";
            this.tsmnuSaveToBmp.Size = new System.Drawing.Size(172, 22);
            this.tsmnuSaveToBmp.Text = "导出卡片为BMP图像";
            this.tsmnuSaveToBmp.Click += new System.EventHandler(this.tsmnuSaveToBmp_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmnuSave
            // 
            this.tsmnuSave.Name = "tsmnuSave";
            this.tsmnuSave.Size = new System.Drawing.Size(172, 22);
            this.tsmnuSave.Text = "保存卡片文件(&S)";
            this.tsmnuSave.Click += new System.EventHandler(this.tsmnuSave_Click);
            // 
            // tsmnuOpenCard
            // 
            this.tsmnuOpenCard.Name = "tsmnuOpenCard";
            this.tsmnuOpenCard.Size = new System.Drawing.Size(172, 22);
            this.tsmnuOpenCard.Text = "打开卡片文件(&O)";
            this.tsmnuOpenCard.Click += new System.EventHandler(this.tsmnuOpenCard_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmnuOleObject
            // 
            this.tsmnuOleObject.Name = "tsmnuOleObject";
            this.tsmnuOleObject.Size = new System.Drawing.Size(172, 22);
            this.tsmnuOleObject.Text = "OLE对象";
            this.tsmnuOleObject.Click += new System.EventHandler(this.tsmnuOleObject_Click);
            // 
            // tsmnuCellProperty
            // 
            this.tsmnuCellProperty.Name = "tsmnuCellProperty";
            this.tsmnuCellProperty.Size = new System.Drawing.Size(172, 22);
            this.tsmnuCellProperty.Text = "单元格属性";
            this.tsmnuCellProperty.Visible = false;
            this.tsmnuCellProperty.Click += new System.EventHandler(this.tsmnuCellProperty_Click);
            // 
            // tsmnuAllLine
            // 
            this.tsmnuAllLine.Name = "tsmnuAllLine";
            this.tsmnuAllLine.Size = new System.Drawing.Size(172, 22);
            this.tsmnuAllLine.Text = "整行显示";
            this.tsmnuAllLine.Visible = false;
            this.tsmnuAllLine.Click += new System.EventHandler(this.tsmnuAllLine_Click);
            // 
            // tsmnuNewLine
            // 
            this.tsmnuNewLine.Name = "tsmnuNewLine";
            this.tsmnuNewLine.Size = new System.Drawing.Size(172, 22);
            this.tsmnuNewLine.Text = "换行显示";
            this.tsmnuNewLine.Visible = false;
            this.tsmnuNewLine.Click += new System.EventHandler(this.tsmnuNewLine_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(169, 6);
            // 
            // tsmnuCopy
            // 
            this.tsmnuCopy.Name = "tsmnuCopy";
            this.tsmnuCopy.Size = new System.Drawing.Size(172, 22);
            this.tsmnuCopy.Text = "复制(&C)";
            this.tsmnuCopy.Click += new System.EventHandler(this.tsmnuCopy_Click);
            // 
            // tsPaster
            // 
            this.tsPaster.Name = "tsPaster";
            this.tsPaster.Size = new System.Drawing.Size(172, 22);
            this.tsPaster.Text = "粘贴(&V)";
            this.tsPaster.Click += new System.EventHandler(this.tsPaster_Click);
            // 
            // tsmnuCut
            // 
            this.tsmnuCut.Name = "tsmnuCut";
            this.tsmnuCut.Size = new System.Drawing.Size(172, 22);
            this.tsmnuCut.Text = "剪切(&X)";
            this.tsmnuCut.Click += new System.EventHandler(this.tsmnuCut_Click);
            // 
            // contextMenuTextBox
            // 
            this.contextMenuTextBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuSymbol,
            this.toolStripSeparator3,
            this.toolStripMenuItem1,
            this.toolStripSeparator5,
            this.tsmnuTextCopy,
            this.tsmnuTextPaste,
            this.tsmnuTextCut});
            this.contextMenuTextBox.Name = "contextMenuTextBox";
            this.contextMenuTextBox.Size = new System.Drawing.Size(119, 126);
            // 
            // tsmnuSymbol
            // 
            this.tsmnuSymbol.Name = "tsmnuSymbol";
            this.tsmnuSymbol.Size = new System.Drawing.Size(118, 22);
            this.tsmnuSymbol.Text = "特殊符号";
            this.tsmnuSymbol.Click += new System.EventHandler(this.tsmnuSymbol_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuRoughnessMark,
            this.tsmnuWeldingSymbol,
            this.tsmnuShapeTolerance,
            this.tsmnuToleranceMark,
            this.tsmnuSuperscriptAndSubscript});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.toolStripMenuItem1.Text = "内容插入";
            // 
            // tsmnuRoughnessMark
            // 
            this.tsmnuRoughnessMark.Name = "tsmnuRoughnessMark";
            this.tsmnuRoughnessMark.Size = new System.Drawing.Size(152, 22);
            this.tsmnuRoughnessMark.Text = "粗糙度标注";
            this.tsmnuRoughnessMark.Click += new System.EventHandler(this.tsmnuRoughnessMark_Click);
            // 
            // tsmnuWeldingSymbol
            // 
            this.tsmnuWeldingSymbol.Name = "tsmnuWeldingSymbol";
            this.tsmnuWeldingSymbol.Size = new System.Drawing.Size(152, 22);
            this.tsmnuWeldingSymbol.Text = "焊接符号";
            this.tsmnuWeldingSymbol.Click += new System.EventHandler(this.tsmnuWeldingSymbol_Click);
            // 
            // tsmnuShapeTolerance
            // 
            this.tsmnuShapeTolerance.Name = "tsmnuShapeTolerance";
            this.tsmnuShapeTolerance.Size = new System.Drawing.Size(152, 22);
            this.tsmnuShapeTolerance.Text = "形位公差";
            this.tsmnuShapeTolerance.Click += new System.EventHandler(this.tsmnuShapeTolerance_Click);
            // 
            // tsmnuToleranceMark
            // 
            this.tsmnuToleranceMark.Name = "tsmnuToleranceMark";
            this.tsmnuToleranceMark.Size = new System.Drawing.Size(152, 22);
            this.tsmnuToleranceMark.Text = "公差标注";
            this.tsmnuToleranceMark.Click += new System.EventHandler(this.tsmnuToleranceMark_Click);
            // 
            // tsmnuSuperscriptAndSubscript
            // 
            this.tsmnuSuperscriptAndSubscript.Name = "tsmnuSuperscriptAndSubscript";
            this.tsmnuSuperscriptAndSubscript.Size = new System.Drawing.Size(152, 22);
            this.tsmnuSuperscriptAndSubscript.Text = "上标下标";
            this.tsmnuSuperscriptAndSubscript.Click += new System.EventHandler(this.tsmnuSuperscriptAndSubscript_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(115, 6);
            // 
            // tsmnuTextCopy
            // 
            this.tsmnuTextCopy.Name = "tsmnuTextCopy";
            this.tsmnuTextCopy.Size = new System.Drawing.Size(118, 22);
            this.tsmnuTextCopy.Text = "复制";
            this.tsmnuTextCopy.Click += new System.EventHandler(this.tsmnuTextCopy_Click);
            // 
            // tsmnuTextPaste
            // 
            this.tsmnuTextPaste.Name = "tsmnuTextPaste";
            this.tsmnuTextPaste.Size = new System.Drawing.Size(118, 22);
            this.tsmnuTextPaste.Text = "粘贴";
            this.tsmnuTextPaste.Click += new System.EventHandler(this.tsmnuTextPaste_Click);
            // 
            // tsmnuTextCut
            // 
            this.tsmnuTextCut.Name = "tsmnuTextCut";
            this.tsmnuTextCut.Size = new System.Drawing.Size(118, 22);
            this.tsmnuTextCut.Text = "剪切";
            this.tsmnuTextCut.Click += new System.EventHandler(this.tsmnuTextCut_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            // 
            // cmnuTitle
            // 
            this.cmnuTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmnuFullScreen,
            this.tsmnuCloseCurrent,
            this.tsmnuCloseOther,
            this.tsmnuCloseAll});
            this.cmnuTitle.Name = "cmnuTitle";
            this.cmnuTitle.Size = new System.Drawing.Size(131, 92);
            // 
            // tsmnuFullScreen
            // 
            this.tsmnuFullScreen.Name = "tsmnuFullScreen";
            this.tsmnuFullScreen.Size = new System.Drawing.Size(130, 22);
            this.tsmnuFullScreen.Text = "全屏显示";
            this.tsmnuFullScreen.Click += new System.EventHandler(this.tsmnuFullScreen_Click);
            // 
            // tsmnuCloseCurrent
            // 
            this.tsmnuCloseCurrent.Name = "tsmnuCloseCurrent";
            this.tsmnuCloseCurrent.Size = new System.Drawing.Size(130, 22);
            this.tsmnuCloseCurrent.Text = "关闭当前页";
            this.tsmnuCloseCurrent.Click += new System.EventHandler(this.tsmnuCloseCurrent_Click);
            // 
            // tsmnuCloseOther
            // 
            this.tsmnuCloseOther.Name = "tsmnuCloseOther";
            this.tsmnuCloseOther.Size = new System.Drawing.Size(130, 22);
            this.tsmnuCloseOther.Text = "关闭其他页";
            this.tsmnuCloseOther.Click += new System.EventHandler(this.tsmnuCloseOther_Click);
            // 
            // tsmnuCloseAll
            // 
            this.tsmnuCloseAll.Name = "tsmnuCloseAll";
            this.tsmnuCloseAll.Size = new System.Drawing.Size(130, 22);
            this.tsmnuCloseAll.Text = "关闭所有页";
            this.tsmnuCloseAll.Click += new System.EventHandler(this.tsmnuCloseAll_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(115, 6);
            // 
            // tsmnuInsertRow
            // 
            this.tsmnuInsertRow.Name = "tsmnuInsertRow";
            this.tsmnuInsertRow.Size = new System.Drawing.Size(172, 22);
            this.tsmnuInsertRow.Text = "插入整行";
            this.tsmnuInsertRow.Click += new System.EventHandler(this.tsmnuInsertRow_Click);
            // 
            // ProcessCardFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(646, 475);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.Controls.Add(this.pnCard);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ProcessCardFrm";
            this.TabText = "工艺卡片";
            this.Text = "工艺卡片";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProcessCardFrm_FormClosed);
            this.Load += new System.EventHandler(this.ProcessCardFrm_Load);
            this.Resize += new System.EventHandler(this.ProcessCardFrm_Resize);
            this.pnCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCard)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuTextBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.cmnuTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnCard;
        private System.Windows.Forms.DataGridView dgvCard;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmnuSaveToBmp;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuExportDetail;
        private System.Windows.Forms.ToolStripMenuItem tsmnuImportDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmnuSave;
        private System.Windows.Forms.ToolStripMenuItem tsmnuOpenCard;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCopy;
        private System.Windows.Forms.ToolStripMenuItem tsPaster;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCut;
        private System.Windows.Forms.ContextMenuStrip contextMenuTextBox;
        private System.Windows.Forms.ToolStripMenuItem tsmnuTextCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmnuTextPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmnuTextCut;
        private System.Windows.Forms.ToolStripMenuItem tsmnuSymbol;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmnuRoughnessMark;
        private System.Windows.Forms.ToolStripMenuItem tsmnuWeldingSymbol;
        private System.Windows.Forms.ToolStripMenuItem tsmnuShapeTolerance;
        private System.Windows.Forms.ToolStripMenuItem tsmnuToleranceMark;
        private System.Windows.Forms.ToolStripMenuItem tsmnuSuperscriptAndSubscript;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCellProperty;
        private System.Windows.Forms.ToolStripMenuItem tsmnuAllLine;
        private System.Windows.Forms.ToolStripMenuItem tsmnuNewLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmnuOleObject;
        private System.Windows.Forms.ContextMenuStrip cmnuTitle;
        private System.Windows.Forms.ToolStripMenuItem tsmnuFullScreen;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCloseCurrent;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCloseOther;
        private System.Windows.Forms.ToolStripMenuItem tsmnuCloseAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmnuInsertRow;
    }
}