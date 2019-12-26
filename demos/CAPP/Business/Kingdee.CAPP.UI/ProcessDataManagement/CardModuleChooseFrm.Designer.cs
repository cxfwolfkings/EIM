namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    partial class CardModuleChooseFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardModuleChooseFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.tcNewCard = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tpCardModule = new System.Windows.Forms.TabPage();
            this.dgvCardModule = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpTypical = new System.Windows.Forms.TabPage();
            this.tvTypicalProcess = new System.Windows.Forms.TreeView();
            this.tpPlanning = new System.Windows.Forms.TabPage();
            this.tvProcessPlanningModule = new System.Windows.Forms.TreeView();
            this.tpCardData = new System.Windows.Forms.TabPage();
            this.dgvCard = new System.Windows.Forms.DataGridView();
            this.colCardId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpCreateDate = new System.Windows.Forms.DateTimePicker();
            this.comboCreator = new System.Windows.Forms.ComboBox();
            this.comboFileStatus = new System.Windows.Forms.ComboBox();
            this.comboFileType = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMaterial = new System.Windows.Forms.TextBox();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tcProcessCardManager = new Kingdee.CAPP.Controls.FlatTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvProcessCard = new System.Windows.Forms.TreeView();
            this.pnBody.SuspendLayout();
            this.tcNewCard.SuspendLayout();
            this.tpCardModule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardModule)).BeginInit();
            this.tpTypical.SuspendLayout();
            this.tpPlanning.SuspendLayout();
            this.tpCardData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCard)).BeginInit();
            this.panel1.SuspendLayout();
            this.tcProcessCardManager.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnBody
            // 
            this.pnBody.Controls.Add(this.tcProcessCardManager);
            this.pnBody.Controls.Add(this.tcNewCard);
            this.pnBody.Controls.Add(this.btnConfirm);
            this.pnBody.Controls.Add(this.btnCancel);
            this.pnBody.Size = new System.Drawing.Size(724, 418);
            // 
            // btnConfirm
            // 
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConfirm.Location = new System.Drawing.Point(556, 383);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
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
            this.btnCancel.Location = new System.Drawing.Point(637, 383);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "folder");
            this.imgList.Images.SetKeyName(1, "card");
            this.imgList.Images.SetKeyName(2, "planning");
            this.imgList.Images.SetKeyName(3, "folder_o");
            // 
            // tcNewCard
            // 
            this.tcNewCard.Controls.Add(this.tpCardModule);
            this.tcNewCard.Controls.Add(this.tpTypical);
            this.tcNewCard.Controls.Add(this.tpPlanning);
            this.tcNewCard.Controls.Add(this.tpCardData);
            this.tcNewCard.Location = new System.Drawing.Point(189, 6);
            this.tcNewCard.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcNewCard.Name = "tcNewCard";
            this.tcNewCard.SelectedIndex = 0;
            this.tcNewCard.Size = new System.Drawing.Size(523, 371);
            this.tcNewCard.TabIndex = 4;
            this.tcNewCard.SelectedIndexChanged += new System.EventHandler(this.tcNewCard_SelectedIndexChanged);
            // 
            // tpCardModule
            // 
            this.tpCardModule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpCardModule.Controls.Add(this.dgvCardModule);
            this.tpCardModule.Location = new System.Drawing.Point(4, 25);
            this.tpCardModule.Name = "tpCardModule";
            this.tpCardModule.Padding = new System.Windows.Forms.Padding(3);
            this.tpCardModule.Size = new System.Drawing.Size(515, 342);
            this.tpCardModule.TabIndex = 0;
            this.tpCardModule.Text = "卡片模版";
            // 
            // dgvCardModule
            // 
            this.dgvCardModule.AllowUserToAddRows = false;
            this.dgvCardModule.AllowUserToDeleteRows = false;
            this.dgvCardModule.AllowUserToResizeRows = false;
            this.dgvCardModule.BackgroundColor = System.Drawing.Color.White;
            this.dgvCardModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCardModule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCardModule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCardModule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCardModule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.name,
            this.Column3});
            this.dgvCardModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCardModule.Location = new System.Drawing.Point(3, 3);
            this.dgvCardModule.Name = "dgvCardModule";
            this.dgvCardModule.ReadOnly = true;
            this.dgvCardModule.RowHeadersVisible = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvCardModule.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCardModule.RowTemplate.Height = 23;
            this.dgvCardModule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCardModule.Size = new System.Drawing.Size(509, 336);
            this.dgvCardModule.TabIndex = 0;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "工艺卡片模板标识";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "工艺卡片模板名称";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "typename";
            this.Column3.HeaderText = "类别";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // tpTypical
            // 
            this.tpTypical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpTypical.Controls.Add(this.tvTypicalProcess);
            this.tpTypical.Location = new System.Drawing.Point(4, 25);
            this.tpTypical.Name = "tpTypical";
            this.tpTypical.Padding = new System.Windows.Forms.Padding(3);
            this.tpTypical.Size = new System.Drawing.Size(515, 342);
            this.tpTypical.TabIndex = 1;
            this.tpTypical.Text = "典型工艺";
            // 
            // tvTypicalProcess
            // 
            this.tvTypicalProcess.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvTypicalProcess.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvTypicalProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTypicalProcess.ImageIndex = 0;
            this.tvTypicalProcess.ImageList = this.imgList;
            this.tvTypicalProcess.Location = new System.Drawing.Point(3, 3);
            this.tvTypicalProcess.Name = "tvTypicalProcess";
            this.tvTypicalProcess.SelectedImageIndex = 0;
            this.tvTypicalProcess.Size = new System.Drawing.Size(186, 65);
            this.tvTypicalProcess.TabIndex = 1;
            this.tvTypicalProcess.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterCollapse);
            this.tvTypicalProcess.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterExpand);
            this.tvTypicalProcess.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvTypicalProcess_MouseDown);
            // 
            // tpPlanning
            // 
            this.tpPlanning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpPlanning.Controls.Add(this.tvProcessPlanningModule);
            this.tpPlanning.Location = new System.Drawing.Point(4, 25);
            this.tpPlanning.Name = "tpPlanning";
            this.tpPlanning.Size = new System.Drawing.Size(515, 342);
            this.tpPlanning.TabIndex = 2;
            this.tpPlanning.Text = "规程模版";
            // 
            // tvProcessPlanningModule
            // 
            this.tvProcessPlanningModule.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvProcessPlanningModule.Cursor = System.Windows.Forms.Cursors.Default;
            this.tvProcessPlanningModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvProcessPlanningModule.ImageIndex = 0;
            this.tvProcessPlanningModule.ImageList = this.imgList;
            this.tvProcessPlanningModule.Location = new System.Drawing.Point(0, 0);
            this.tvProcessPlanningModule.Name = "tvProcessPlanningModule";
            this.tvProcessPlanningModule.SelectedImageIndex = 0;
            this.tvProcessPlanningModule.Size = new System.Drawing.Size(192, 71);
            this.tvProcessPlanningModule.TabIndex = 1;
            this.tvProcessPlanningModule.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterCollapse);
            this.tvProcessPlanningModule.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessPlanningModule_AfterExpand);
            this.tvProcessPlanningModule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessPlanningModule_MouseDown);
            // 
            // tpCardData
            // 
            this.tpCardData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tpCardData.Controls.Add(this.dgvCard);
            this.tpCardData.Controls.Add(this.panel1);
            this.tpCardData.Location = new System.Drawing.Point(4, 25);
            this.tpCardData.Name = "tpCardData";
            this.tpCardData.Size = new System.Drawing.Size(515, 342);
            this.tpCardData.TabIndex = 3;
            this.tpCardData.Text = "已入库卡片";
            // 
            // dgvCard
            // 
            this.dgvCard.AllowUserToAddRows = false;
            this.dgvCard.AllowUserToDeleteRows = false;
            this.dgvCard.AllowUserToResizeColumns = false;
            this.dgvCard.AllowUserToResizeRows = false;
            this.dgvCard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCard.BackgroundColor = System.Drawing.Color.White;
            this.dgvCard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCard.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCardId,
            this.colName});
            this.dgvCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCard.Location = new System.Drawing.Point(0, 100);
            this.dgvCard.Name = "dgvCard";
            this.dgvCard.ReadOnly = true;
            this.dgvCard.RowHeadersVisible = false;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvCard.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCard.RowTemplate.Height = 23;
            this.dgvCard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCard.Size = new System.Drawing.Size(515, 242);
            this.dgvCard.TabIndex = 1;
            // 
            // colCardId
            // 
            this.colCardId.DataPropertyName = "id";
            this.colCardId.HeaderText = "工艺卡片标识";
            this.colCardId.Name = "colCardId";
            this.colCardId.ReadOnly = true;
            this.colCardId.Visible = false;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "name";
            this.colName.HeaderText = "工艺卡片名称";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpCreateDate);
            this.panel1.Controls.Add(this.comboCreator);
            this.panel1.Controls.Add(this.comboFileStatus);
            this.panel1.Controls.Add(this.comboFileType);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtMaterial);
            this.panel1.Controls.Add(this.txtProduct);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 100);
            this.panel1.TabIndex = 9;
            // 
            // dtpCreateDate
            // 
            this.dtpCreateDate.CustomFormat = "yyyy-MM-dd";
            this.dtpCreateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreateDate.Location = new System.Drawing.Point(262, 63);
            this.dtpCreateDate.Name = "dtpCreateDate";
            this.dtpCreateDate.ShowUpDown = true;
            this.dtpCreateDate.Size = new System.Drawing.Size(150, 22);
            this.dtpCreateDate.TabIndex = 12;
            // 
            // comboCreator
            // 
            this.comboCreator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboCreator.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboCreator.FormattingEnabled = true;
            this.comboCreator.Location = new System.Drawing.Point(95, 63);
            this.comboCreator.Name = "comboCreator";
            this.comboCreator.Size = new System.Drawing.Size(101, 24);
            this.comboCreator.TabIndex = 11;
            // 
            // comboFileStatus
            // 
            this.comboFileStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboFileStatus.FormattingEnabled = true;
            this.comboFileStatus.Items.AddRange(new object[] {
            "提交",
            "归档",
            "发布"});
            this.comboFileStatus.Location = new System.Drawing.Point(262, 33);
            this.comboFileStatus.Name = "comboFileStatus";
            this.comboFileStatus.Size = new System.Drawing.Size(150, 24);
            this.comboFileStatus.TabIndex = 10;
            // 
            // comboFileType
            // 
            this.comboFileType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboFileType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboFileType.FormattingEnabled = true;
            this.comboFileType.Items.AddRange(new object[] {
            "工艺卡片",
            "工艺规程"});
            this.comboFileType.Location = new System.Drawing.Point(95, 33);
            this.comboFileType.Name = "comboFileType";
            this.comboFileType.Size = new System.Drawing.Size(101, 24);
            this.comboFileType.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(444, 63);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "工艺文件类型:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(202, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "编制日期:";
            // 
            // txtMaterial
            // 
            this.txtMaterial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMaterial.Location = new System.Drawing.Point(262, 6);
            this.txtMaterial.Multiline = true;
            this.txtMaterial.Name = "txtMaterial";
            this.txtMaterial.Size = new System.Drawing.Size(149, 21);
            this.txtMaterial.TabIndex = 2;
            // 
            // txtProduct
            // 
            this.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtProduct.Location = new System.Drawing.Point(95, 6);
            this.txtProduct.Multiline = true;
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(100, 21);
            this.txtProduct.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 7;
            this.label5.Text = "编制人员:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "产品:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(202, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "文件状态:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "零部件:";
            // 
            // tcProcessCardManager
            // 
            this.tcProcessCardManager.Controls.Add(this.tabPage1);
            this.tcProcessCardManager.Location = new System.Drawing.Point(8, 8);
            this.tcProcessCardManager.myBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tcProcessCardManager.Name = "tcProcessCardManager";
            this.tcProcessCardManager.SelectedIndex = 0;
            this.tcProcessCardManager.Size = new System.Drawing.Size(175, 365);
            this.tcProcessCardManager.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(225)))), ((int)(((byte)(231)))));
            this.tabPage1.Controls.Add(this.tvProcessCard);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(167, 336);
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
            this.tvProcessCard.Size = new System.Drawing.Size(161, 330);
            this.tvProcessCard.TabIndex = 0;
            this.tvProcessCard.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterCollapse);
            this.tvProcessCard.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvProcessCard_AfterExpand);
            this.tvProcessCard.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvProcessCard_DrawNode);
            this.tvProcessCard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvProcessCard_MouseDown);
            // 
            // CardModuleChooseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 440);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "CardModuleChooseFrm";
            this.Text = "工艺卡片模板选择";
            this.Load += new System.EventHandler(this.CardModuleChooseFrm_Load);
            this.pnBody.ResumeLayout(false);
            this.tcNewCard.ResumeLayout(false);
            this.tpCardModule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCardModule)).EndInit();
            this.tpTypical.ResumeLayout(false);
            this.tpPlanning.ResumeLayout(false);
            this.tpCardData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCard)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tcProcessCardManager.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCardModule;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private Controls.FlatTabControl tcNewCard;
        private System.Windows.Forms.TabPage tpCardModule;
        private System.Windows.Forms.TabPage tpTypical;
        private System.Windows.Forms.TabPage tpPlanning;
        private System.Windows.Forms.TabPage tpCardData;
        private System.Windows.Forms.DataGridView dgvCard;
        private System.Windows.Forms.TreeView tvProcessPlanningModule;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TreeView tvTypicalProcess;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCardId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private Controls.FlatTabControl tcProcessCardManager;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView tvProcessCard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpCreateDate;
        private System.Windows.Forms.ComboBox comboCreator;
        private System.Windows.Forms.ComboBox comboFileStatus;
        private System.Windows.Forms.ComboBox comboFileType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtMaterial;
    }
}