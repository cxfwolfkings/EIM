using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.UI.ProcessDesign;
using Kingdee.CAPP.UI.About;
using System.Reflection;
using Kingdee.CAPP.IPlugIn;
using Kingdee.CAPP.UI.Plug_In;
using System.Xml.Linq;
using System.IO;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.UI.ProcessDataManagement;

namespace Kingdee.CAPP.UI
{
    public partial class MainFrm : Form, ICommand
    {
        /// <summary>
        /// launch file path
        /// </summary>
        public static string FilePath
        {
            get;
            set;
        }

        #region 变量声明

        ModuleManagerFrm navigate;

        /// <summary>
        /// 静态属性公布当前窗体，便于其他窗体调用该窗体公用方法
        /// </summary>
        public static MainFrm mainFrm { get; set; }

        private bool _closedWithoutNotice;
        /// <summary>
        /// 关闭时不提示
        /// </summary>
        public bool ClosedWithoutNotice
        {
            get
            {
                return _closedWithoutNotice;
            }
            set
            {
                _closedWithoutNotice = value && dockPaneMain.DocumentsCount > 1;
            }
        }

        private DataGridView dgv;

        #endregion

        #region 窗体控件事件

        public MainFrm()
        {
            InitializeComponent();

            navigate = new ModuleManagerFrm();
            mainFrm = this;

            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        private void MainFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ///增加菜单栏
                AddToolItem();

                AddMenuItem item = new AddMenuItem();
                item.AddMenu(menuStrip1, this, dockPaneMain);
                SetToolButtonEnable(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Text = LoginFrm.UserName + " " + Text;

            navigate.Show(dockPaneMain, DockState.DockLeft);

            //初次打开时不打开属性窗体
            //var property = new PropertiesNavigate();
            //property.Show(this.dockPaneMain, DockState.DockRight);

            //tsbtnSave.Enabled = CardModuleFrm.cardModuleFrm != null;
            //btnSave.Enabled = tsbtnSave.Enabled;

            if (!string.IsNullOrEmpty(FilePath))
            {
                if (FilePath.Contains("-") && !FilePath.Contains("\\"))
                {
                    if (FilePath.Contains("@"))
                    {
                        ProcessCardFrm frm = new ProcessCardFrm();
                        frm.Name = string.Format("ProcessCardFrm-{0}", Guid.NewGuid().ToString());
                        string name = FilePath.Substring(FilePath.IndexOf("@") + 1);
                        string cardid = FilePath.Substring(0, FilePath.IndexOf("@"));
                        if (frm != null)
                        {

                            #region 设置新增卡片Tab的TabText及Name

                            frm.TabText = name;

                            #endregion

                            frm.Show(this.dockPaneMain);
                            frm.OpenCard(null, cardid, false, false);
                        }
                    }
                    else if(FilePath.EndsWith("Folder"))
                    {
                        using (CardModuleChooseFrm chooseFrm = new CardModuleChooseFrm())
                        {
                            if (chooseFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                
                            }
                        }
                    }
                }
                else if (FilePath.Contains("\\"))
                {
                    OpenModule();
                }
            }

            menuStrip1.Renderer = new Controls.CustomMenuRender();
            tsToolbar.Renderer = new Controls.CustomMenuRender();
            //ToolTip tip= new ToolTip();
            //tip.SetToolTip(btnNew, btnNew.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnOpen, btnOpen.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnSave, btnSave.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnSaveAll, btnSaveAll.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnData, btnData.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnMeger, btnMeger.Tag.ToString());
            //tip = new ToolTip();
            //tip.SetToolTip(btnCancelMeger, btnCancelMeger.Tag.ToString());         
        }

        /// <summary>
        /// 动态添加Toolbar按钮
        /// </summary>
        void AddToolItem()
        {
            var tools = from p in XElement.Load(Application.StartupPath + "\\Plug-In\\CappExstention.xml").Elements("ToolBars").Elements("Tool") select p;
            string path = Application.StartupPath;
            foreach (var t in tools)
            {
                if (t.Attribute("Icon") != null)
                {
                    try
                    {
                        if (t.Attribute("Name").Value == "Split")//加分隔符
                        {
                            ToolStripSeparator sep = new ToolStripSeparator();
                            sep.BackColor = Color.Red;
                            sep.ForeColor = Color.Red;
                            tsToolbar.Items.Add(sep);
                        }
                        else
                        {                            
                            ToolStripItem item = new ToolStripMenuItem()
                            {
                                Text = t.Attribute("Name").Value,
                                Tag = t.Attribute("FormName").Value,
                                Image = Image.FromFile(path + "\\" + t.Attribute("Icon").Value),
                                ToolTipText = t.Attribute("Tooltip").Value
                            };
                            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
                            tsToolbar.Items.Add(item);
                        }
                    }
                    catch (FileNotFoundException fex)
                    {
                        MessageBox.Show("Canot find file!" + fex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 新建卡片模板
        /// </summary>
        private void ProcessCardModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AddCardModule();
            if(navigate.IsDisposed)
                navigate = new ModuleManagerFrm();
            navigate.Show(this.dockPaneMain, DockState.DockLeft);
        }

        private void bkwLoadTreeView_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        /// <summary>
        /// 取消合并
        /// </summary>
        void UnMerge()
        {
            if (DelegateForm.propertyForm != null)
            {
                DelegateForm.propertyForm.CellOperator(-1);
            }
        }
        
        /// <summary>
        /// 合并
        /// </summary>
        void Merge()
        {
            if (DelegateForm.propertyForm != null)
            {
                DelegateForm.propertyForm.CellOperator(1);
            }
        }

        /// <summary>
        /// 打开管理中心
        /// </summary>
        private void tsbShowModuleManager_Click(object sender, EventArgs e)
        {
            navigate.Show(this.dockPaneMain, DockState.DockLeft);
        }

        /// <summary>
        /// 产品升级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpgrade_Click(object sender, EventArgs e)
        {
            AboutAb upgrade = new AboutAb();
            upgrade.Show();
        }

        /// <summary>
        /// add menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ts_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
                string assemblyName = toolStripMenuItem.Tag.ToString();



                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> typeList = new List<Type>();
                foreach (Type t in assembly.GetTypes())
                {
                    if (t.IsClass && t.GetInterface("IplugIn") != null)
                    {
                        typeList.Add(t);
                    }
                }
                if (typeList.Count <= 0) return;

                foreach (Type t in typeList)
                {
                    IplugIn plugin = (IplugIn)Activator.CreateInstance(t);
                    plugin.FormShow(this, this.dockPaneMain);
                }
            }
            catch
            {
                throw;
            }
        }

        private void tsmnuShowModuleManager_Click(object sender, EventArgs e)
        {
            navigate.Show(this.dockPaneMain, DockState.DockLeft);
        }

        /// <summary>
        /// 保存
        /// </summary>
        void Save()
        {
            if (PropertiesNavigate.CurrentForm != null)
            {
                string formName = PropertiesNavigate.CurrentForm.GetType().Name;

                if (formName == typeof(CardModuleFrm).Name)
                {
                    //CardModuleFrm.cardModuleFrm.Name = PropertiesNavigate.CurrentForm.Name;
                    CardModuleFrm.cardModuleFrm = (CardModuleFrm)PropertiesNavigate.CurrentForm;
                    CardModuleFrm.cardModuleFrm.SaveTemplate(false, dgv);
                }
                else if (formName == typeof(ProcessCardFrm).Name)
                {
                    //ProcessCardFrm.processCardFrm.Name = PropertiesNavigate.CurrentForm.Name;
                    ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                    ProcessCardFrm.processCardFrm.SaveCard();
                }
            }
        }

        delegate void AddCardModuleToTreeEventHandler(BusinessType btype, ProcessCardModule cardModule);

        /// <summary>
        /// 入库
        /// </summary>
        void SaveData()
        {
            if (PropertiesNavigate.CurrentForm != null)
            {
                string formName = PropertiesNavigate.CurrentForm.GetType().Name;

                if (formName == typeof(CardModuleFrm).Name)
                {
                    //CardModuleFrm.cardModuleFrm.Name = PropertiesNavigate.CurrentForm.Name;
                    CardModuleFrm.cardModuleFrm = (CardModuleFrm)PropertiesNavigate.CurrentForm;
                    ProcessCardModule cardModule
                       = CardModuleFrm.cardModuleFrm.SaveTemplate(true, dgv);

                    if (cardModule != null)
                    {
                        //display cardmodule tree

                        AddCardModuleToTreeEventHandler addcardmouleHandler
                            = new AddCardModuleToTreeEventHandler(navigate.AddModuleOrFolder);

                        addcardmouleHandler(BusinessType.Card, cardModule);

                    }
                    else
                    {
                        //CloseModule(PropertiesNavigate.CurrentForm);
                    }
                }
                else if (formName == typeof(ProcessCardFrm).Name)
                {
                    ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                    //ProcessCardFrm.processCardFrm.Name = PropertiesNavigate.CurrentForm.Name;

                    DockContent content = CheckFormIsOpened(typeof(PropertiesNavigate).Name);

                    if (content == null)
                    {                        
                        var property = new PropertiesNavigate();
                        property.Show(this.dockPaneMain, DockState.DockRight);
                        property.Activate();
                    }

                    ProcessCard card =
                        ProcessCardFrm.processCardFrm.SaveCardIntoDatabase();

                    if (card != null)
                    {
                        if (ProcessCardFrm.processCardFrm.Name.EndsWith("NAVG") ||
                        ProcessCardFrm.processCardFrm.Name.EndsWith("NAVP"))
                        {
                            //添加节点
                            string baseid = MaterialStructureNavigate.materialNavigateFrm.AddNodeInMaterial(card, ProcessCardFrm.processCardFrm.ProcessFolderId);
                            if (string.IsNullOrEmpty(baseid))
                            {
                                return;
                            }
                            MaterialCardRelation materialCardRelation = new MaterialCardRelation();
                            materialCardRelation.MaterialCardRelationId = Guid.NewGuid();
                            materialCardRelation.MaterialId = new Guid(baseid);
                            materialCardRelation.ProcessCardId = card.ID;
                            if (ProcessCardFrm.processCardFrm.Name.EndsWith("NAVG"))
                                materialCardRelation.Type = 1;
                            else
                                materialCardRelation.Type = 2;

                            Kingdee.CAPP.BLL.MaterialCardRelationBLL.AddMaterialCardRelationData(materialCardRelation);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(ProcessCardFrm.processCardFrm.ProcessFolderId))
                            {
                                DataTable dt = Kingdee.CAPP.BLL.SqlServerControllerBLL.GetUserInfo(LoginFrm.UserName);
                                if (dt == null || dt.Rows.Count == 0)
                                    return;
                                ProcessVersion version = new ProcessVersion();
                                version.FolderId = ProcessCardFrm.processCardFrm.ProcessFolderId;
                                version.BaseId = card.ID.ToString();
                                version.Name = card.Name;
                                version.State = 2;
                                version.IsShow = 2;
                                version.Creator = dt.Rows[0]["UserId"].ToString();
                                version.Code = ProcessCardFrm.processCardFrm.TabText;
                                Kingdee.CAPP.BLL.ProcessCardBLL.InsertProcessVersion(version, null);
                                if (ProcessCardEditFrm.processCardEditFrm != null)
                                {
                                    ProcessCardEditFrm.processCardEditFrm.LoadCardData();
                                }
                            }
                        }
                        catch
                        { }
                    }
                }
            }
        }

        /// <summary>
        /// 打开模板
        /// </summary>
        void OpenModule()
        {
            CardModuleFrm frm = new CardModuleFrm();
            frm.Name = string.Format("CardModuleFrm-{0}", Guid.NewGuid().ToString());

            if (frm != null)
            {
                string path = string.Empty;
                if (!string.IsNullOrEmpty(FilePath))
                {
                    path = FilePath;
                    FilePath = null;
                }
                else
                {
                    OpenFileDialog of = new OpenFileDialog();
                    of.Filter = "CAP files (*.cap)|*.cap";
                    if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        path = of.FileName;
                    }
                }

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                string moduleName = Path.GetFileNameWithoutExtension(path);
                int tag = 1;
                int index = 1;
                List<int> listIndex = new List<int>();
                try
                {
                    foreach (DockContent form in this.MdiChildren)
                    {
                        if (form.Name.StartsWith("CardModuleFrm") &&
                            form.TabText.StartsWith(moduleName))
                        {
                            int start = form.TabText.IndexOf("-") + 1;
                            int length = form.TabText.IndexOf("@") - start - 1;
                            if (start > 0)
                            {
                                tag = int.Parse(form.TabText.Substring(start, length));
                            }
                            else
                                tag = 1;
                            listIndex.Add(tag);
                        }
                    }
                    listIndex.Sort();
                    foreach (var i in listIndex)
                    {
                        if (i > 1 && index == 1)
                        {
                            index = 1;
                            break;
                        }

                        if (index > 1)
                        {
                            if (i - listIndex[index - 2] > 1)
                            {
                                index = listIndex[index - 2] + 1;
                                break;
                            }
                        }
                        index++;
                    }
                    frm.TabText = string.Format("{0}-{1}", moduleName, index);

                    frm.Show(this.dockPaneMain);
                    frm.GetTemplate(null, path);
                    frm.Activate();
                    //tsbtnSave.Enabled = true;
                    //btnSave.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("模版文件打开失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 工艺规程模板
        /// </summary>
        private void ProcessPlanningModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(ProcessPlanningModuleFrm).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new ProcessPlanningModuleFrm();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 键盘事件：快捷键设置
        /// </summary>
        private void MainFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.O)//打开
            {
                OpenModule();
            }
            else if (e.Control && e.KeyCode == Keys.S)//保存
            {
                if (CardModuleFrm.cardModuleFrm != null)
                {
                    CardModuleFrm.cardModuleFrm.SaveTemplate(false, dgv);
                }
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)//保存所有
            {

            }
            else if (e.Control && e.KeyCode == Keys.E)//入库
            {
                if (CardModuleFrm.cardModuleFrm != null)
                {
                    ProcessCardModule cardmodule = CardModuleFrm.cardModuleFrm.SaveTemplate(true, dgv);
                    if (cardmodule == null)
                        CloseModule(PropertiesNavigate.CurrentForm);
                }
            }
        }

        /// <summary>
        /// 工艺卡片新建
        /// </summary>
        private void ProcessCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //CardModuleChooseFrm chooseFrm = new CardModuleChooseFrm();
            //if (chooseFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    //TO DO
            //    ProcessCardFrm frm = new ProcessCardFrm();
            //    frm.ModuleId = chooseFrm.ModuleId;
            //    frm.ModuleName = chooseFrm.ModuleName;
            //    frm.Show(this.dockPaneMain);
            //}
        }

        /// <summary>
        /// 新增工艺规程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProcessProcedureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProcessProcedureFrm processProcedure = new NewProcessProcedureFrm(dockPaneMain);
            processProcedure.Show();
        }

        /// <summary>
        /// 查看工艺规程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewProcessProcedureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(ProcessPlanningTreeFrm).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new ProcessPlanningTreeFrm();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 典型工艺
        /// </summary>
        private void TypicalProcessManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(TypicalProcessFrm).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new TypicalProcessFrm();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 当前Dock切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dockPaneMain_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (this.dockPaneMain.ActiveDocument is BaseForm)
            {
                BaseForm form = (BaseForm)this.dockPaneMain.ActiveDocument;

                if (form == null)
                {
                    return;
                }

                if (PropertiesNavigate.CurrentForm == null)
                {
                    PropertiesNavigate.CurrentForm = form;
                }
                else
                {
                    if (PropertiesNavigate.CurrentForm.GetType() != form.GetType())
                    {
                        PropertiesNavigate.CurrentForm = form;
                    }
                }

                foreach (DockContent content in dockPaneMain.Contents)
                {
                    if (form.Name == "MaterialListFrm" && content.Name == "MaterialChooseNavigate")
                    {
                        content.Activate();
                    }
                    else if (form.Name == "MaterialPropertyFrm" && content.Name == "MaterialStructureNavigate")
                    {
                        content.Activate();
                    }

                    if (form.GetType().Name != typeof(ProcessCardFrm).Name &&
                    form.GetType().Name != typeof(CardModuleFrm).Name)
                    {
                        if (DelegateForm.propertyForm != null)
                        {
                            DelegateForm.propertyForm.SetPropertyGrid(null, false, true);
                        }
                        SetToolButtonEnable(0);

                        if (form.GetType().Name == typeof(ProcessPlanningDetailFrm).Name ||
                            form.GetType().Name == typeof(ProcessPlanningModuleDetailFrm).Name)
                        {
                            tsToolbar.Items[3].Enabled = false;
                        }
                        else
                        {
                            tsToolbar.Items[3].Enabled = true;
                        }
                    }
                    else
                    {
                        int type = form.GetType().Name == typeof(ProcessCardFrm).Name ? 2 : 1;
                        SetToolButtonEnable(type);
                    }
                }

                if (form.Name != PropertiesNavigate.CurrentForm.Name)
                {
                    PropertiesNavigate.CurrentForm = form;

                    if (PropertiesNavigate.CurrentForm != null && DelegateForm.propertyForm != null)
                    {
                        if (PropertiesNavigate.CurrentForm.Controls.Count > 2 &&
                            PropertiesNavigate.CurrentForm.Controls[2].Controls.Count > 0)
                        {
                            dgv = (DataGridView)PropertiesNavigate.CurrentForm.Controls[2].Controls[0];
                            DelegateForm.propertyForm.SetPropertyGrid(dgv.CurrentCell, false, false);
                        }
                        else if (PropertiesNavigate.CurrentForm.Controls.Count > 0 &&
                            PropertiesNavigate.CurrentForm.Controls[0].Controls.Count > 0 &&
                            PropertiesNavigate.CurrentForm.Controls[0].Controls[0] is DataGridView)
                        {
                            dgv = (DataGridView)PropertiesNavigate.CurrentForm.Controls[0].Controls[0];
                            Kingdee.CAPP.Common.DataGridViewCustomerCellStyle style = new CAPP.Common.DataGridViewCustomerCellStyle();
                            DelegateForm.propertyForm.SetPropertyGrid(style, false, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 导入数据到明细框
        /// </summary>
        private void tsmnuImportData_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 明细框数据导出
        /// </summary>
        private void tsmnuExportData_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 将卡片输出为图片文件
        /// </summary>
        private void tsmnuExportToImage_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 工艺卡片新建
        /// </summary>
        private void tsmnuNewCard_Click(object sender, EventArgs e)
        {
            using (CardModuleChooseFrm chooseFrm = new CardModuleChooseFrm())
            {
                if (chooseFrm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ProcessCardFrm frm = new ProcessCardFrm();
                    frm.Name = string.Format("ProcessCardFrm-{0}", Guid.NewGuid().ToString());
                    frm.ProcessFolderId = chooseFrm.ProcessFolderId;

                    if (!string.IsNullOrEmpty(chooseFrm.ProcessCardId))
                    {
                        frm.TabText = chooseFrm.ProcessCardName;                        
                        frm.Show(this.dockPaneMain);
                        bool result = frm.OpenCard(null, chooseFrm.ProcessCardId, true, true);
                        if (!result)
                        {
                            CloseModule(frm);
                        }
                        return;
                    }

                    #region 设置新增卡片Tab的TabText及Name

                    int tag = 1;
                    int index = 1;
                    List<int> listIndex = new List<int>();
                    foreach (DockContent form in this.MdiChildren)
                    {
                        if (form.Name.StartsWith("ProcessCardFrm") &&
                            form.TabText.StartsWith(chooseFrm.ModuleName))
                        {
                            tag = int.Parse(form.TabText.Substring(form.TabText.IndexOf("-") + 1));
                            listIndex.Add(tag);
                        }
                    }
                    listIndex.Sort();
                    foreach (var i in listIndex)
                    {
                        if (i > 1 && index == 1)
                        {
                            index = 1;
                            break;
                        }

                        if (index > 1)
                        {
                            if (i - listIndex[index - 2] > 1)
                            {
                                index = listIndex[index - 2] + 1;
                                break;
                            }
                        }
                        index++;
                    }
                    frm.TabText = string.Format("{0}-{1}", chooseFrm.ModuleName, index);
                    

                    #endregion

                    frm.ModuleId = chooseFrm.ModuleId;
                    frm.ModuleName = chooseFrm.ModuleName;
                    frm.Show(this.dockPaneMain);
                }
            }
        }

        /// <summary>
        /// 打开工艺卡片
        /// </summary>
        private void tsmnuOpenCard_Click(object sender, EventArgs e)
        {
            ProcessCardFrm frm = new ProcessCardFrm();
            frm.Name = string.Format("ProcessCardFrm-{0}", Guid.NewGuid().ToString());

            if (frm != null)
            {
                string path = string.Empty;
                if (!string.IsNullOrEmpty(FilePath))
                {
                    path = FilePath;
                    FilePath = null;
                }
                else
                {
                    OpenFileDialog of = new OpenFileDialog();
                    of.Filter = "CARD files (*.card)|*.card";
                    if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        path = of.FileName;
                    }
                }

                if (string.IsNullOrEmpty(path))
                {
                    return;
                }

                #region 设置新增卡片Tab的TabText及Name

                string cardName = Path.GetFileNameWithoutExtension(path);
                int tag = 1;
                int index = 1;
                List<int> listIndex = new List<int>();
                foreach (DockContent form in this.MdiChildren)
                {
                    if (form.Name.StartsWith("ProcessCardFrm") &&
                        form.TabText.StartsWith(cardName))
                    {
                        int start = form.TabText.IndexOf("-") + 1;
                        tag = int.Parse(form.TabText.Substring(start));
                        listIndex.Add(tag);
                    }
                }
                listIndex.Sort();
                foreach (var i in listIndex)
                {
                    if (i > 1 && index == 1)
                    {
                        index = 1;
                        break;
                    }

                    if (index > 1)
                    {
                        if (i - listIndex[index - 2] > 1)
                        {
                            index = listIndex[index - 2] + 1;
                            break;
                        }
                    }
                    index++;
                }
                frm.TabText = string.Format("{0}-{1}", cardName, index);

                #endregion

                frm.Show(this.dockPaneMain);
                frm.OpenCard(path, null, false, true);
            }

        }

        /// <summary>
        /// 打开属性窗体
        /// </summary>
        private void tsmnuShowProperty_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(PropertiesNavigate).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockRight);
            }
            else
            {
                var property = new PropertiesNavigate();
                property.Show(this.dockPaneMain, DockState.DockRight);
                property.Activate();
            }
        }

        /// <summary>
        /// 物料
        /// </summary>
        private void tsmnuDesignView_Click(object sender, EventArgs e)
        {
            //var property = new DesignNavigate();
            //property.Show(this.dockPaneMain, DockState.DockLeft);
        }

        /// <summary>
        /// 产品
        /// </summary>
        private void tsmnuStructureView_Click(object sender, EventArgs e)
        {
            //var property = new StructureNavigate();
            //property.Show(this.dockPaneMain, DockState.DockLeft);
        }

        /// <summary>
        /// 物料选择
        /// </summary>
        private void tsmnuMaterial_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(MaterialChooseNavigate).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new MaterialChooseNavigate();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 产品选择
        /// </summary>
        private void tsmnuProduct_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(ProductChooseNavigate).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new ProductChooseNavigate();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 查找工艺文件
        /// </summary>
        private void tsmnuSearch_Click(object sender, EventArgs e)
        {
            using (ProcessSearchFrm frm = new ProcessSearchFrm())
            {
                frm.ShowDialog();
            }
        }

        /// <summary>
        /// 米重量库
        /// </summary>
        private void MaterialQuotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kingdee.CAPP.UI.SpecialModule.ProportionLibraryFrm materialQuotaToolFrm = new SpecialModule.ProportionLibraryFrm();
            materialQuotaToolFrm.ShowDialog();
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 最小化
        /// </summary>
        private void btnMinimunSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 最大化
        /// </summary>
        private void btnMaximumSize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_hover;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_hover;
            }
        }

        /// <summary>
        /// 标题Panel鼠标点击事件，用于拖动窗体
        /// </summary>
        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        /// <summary>
        /// 最小化按钮鼠标悬停事件
        /// </summary>
        private void btnMinimunSize_MouseHover(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.min_hover;
        }

        /// <summary>
        /// 最小化按钮鼠标离开事件
        /// </summary>
        private void btnMinimunSize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.minimum_d;
        }

        /// <summary>
        /// 最大化按钮鼠标悬停事件
        /// </summary>
        private void btnMaximumSize_MouseHover(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_hover;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_hover;
            }
        }

        /// <summary>
        /// 最大化按钮鼠标离开事件
        /// </summary>
        private void btnMaximumSize_MouseLeave(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_d;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_d;
            }
        }

        /// <summary>
        /// 关闭按钮鼠标悬停事件
        /// </summary>
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_hover;
        }

        /// <summary>
        /// 关闭按钮鼠标离开事件
        /// </summary>
        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_d;
        }
                
        private void pnTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// 窗体尺寸重置事件
        /// </summary>
        private void MainFrm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_d;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_d;
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        private void tsmnuExitLogin_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /// <summary>
        /// 增加事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsToolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem item = e.ClickedItem as ToolStripItem;
            if (item.Tag == null)
                return;
            string formName = item.Tag.ToString();
            //BaseForm form = Assembly.GetExecutingAssembly().CreateInstance(formName) as BaseForm;
            ICommand command = null;
            //if (form.Name == this.Name)
            //{
                command = this as ICommand;
                command.Excute(item.Text);
            //}

        }

        /// <summary>
        /// 新建模板
        /// </summary>
        private void tsmnuNewModule_Click(object sender, EventArgs e)
        {
            AddCardModule();
        }

        /// <summary>
        /// 打开卡片模板
        /// </summary>
        private void tsmnuOpenCardModule_Click(object sender, EventArgs e)
        {
            OpenModule();
        }

        /// <summary>
        /// 保存到本地
        /// </summary>
        private void tsmnuSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// 入库
        /// </summary>
        private void tsmnuSaveData_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// 内容页移除时事件
        /// </summary>
        private void dockPaneMain_ContentRemoved(object sender, DockContentEventArgs e)
        {
            
        }

        /// <summary>
        /// 浏览卡片
        /// </summary>
        private void tsmnuEditProcessCard_Click(object sender, EventArgs e)
        {
            DockContent content = CheckFormIsOpened(typeof(ProcessCardEditFrm).Name);

            if (content != null)
            {
                content.Show(this.dockPaneMain, DockState.DockLeft);
            }
            else
            {
                var property = new ProcessCardEditFrm();
                property.Show(this.dockPaneMain, DockState.DockLeft);
                property.Activate();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void tsmnuPrint_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打印预览
        /// </summary>
        private void tsmnuPrintView_Click(object sender, EventArgs e)
        {

        }
        
        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：新建卡片模板
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// </summary>
        public void AddCardModule()
        {
            using (CardPageSettingFrm setForm = new CardPageSettingFrm())
            {
                if (setForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    CardModuleFrm frm = new CardModuleFrm();

                    #region 设置新增模板Tab的TabText及Name

                    int tag = 1;
                    int index = 1;
                    List<int> listIndex = new List<int>();
                    foreach (DockContent form in this.MdiChildren)
                    {
                        if (form.Name.StartsWith("CardModuleFrm") &&
                            form.TabText.StartsWith(setForm.CardModuleName))
                        {
                            int start = form.TabText.IndexOf("-") + 1;
                            int length = form.TabText.IndexOf("@") - start - 1;
                            tag = int.Parse(form.TabText.Substring(start, length));
                            listIndex.Add(tag);
                        }
                    }
                    listIndex.Sort();
                    foreach (var i in listIndex)
                    {
                        if (i > 1 && index == 1)
                        {
                            index = 1;
                            break;
                        }

                        if (index > 1)
                        {
                            if (i - listIndex[index - 2] > 1)
                            {
                                index = listIndex[index - 2] + 1;
                                break;
                            }
                        }
                        index++;
                    }

                    frm.TabText = string.Format("{0}-{1}", setForm.CardModuleName, index);
                    frm.Name = string.Format("CardModuleFrm-{0}", Guid.NewGuid().ToString());

                    #endregion

                    frm.CardModuleName = setForm.CardModuleName;
                    frm.PageWidth = setForm.PageWidth;
                    frm.PageHeight = setForm.PageHeight;
                    frm.PaddingLeft = setForm.PaddingLeft;
                    frm.PaddingTop = setForm.PaddingTop;
                    frm.PaddingRight = setForm.PaddingRight;
                    frm.PaddingBottom = setForm.PaddingBottom;
                    frm.OffsetLeft = setForm.OffsetLeft;
                    frm.OffsetTop = setForm.OffsetTop;
                    frm.CardBreadth = setForm.CardBreadth;
                    frm.CardType = setForm.CardType;
                    frm.Show(this.dockPaneMain);
                }
                //tsbtnSave.Enabled = true;
                //btnSave.Enabled = true;
            }
        }

        /// <summary>
        /// 方法说明：打开对应的卡片模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-16
        /// </summary>
        /// <param name="form">要打开的内容窗体</param>
        public void OpenModule(DockContent form)
        {
            form.Show(this.dockPaneMain);
            form.Activate();
            //tsbtnSave.Enabled = true;
            //btnSave.Enabled = true;
        }

        /// <summary>
        /// 方法说明：关闭对应的卡片模板
        /// 作    者：jason.tang
        /// 完成时间：2013-07-23
        /// </summary>
        /// <param name="form">要关闭的内容窗体</param>
        public void CloseModule(DockContent form)
        {
            form.Close();
        }

        /// <summary>
        /// 方法说明：打开对应的工艺规程模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-21
        /// </summary>
        /// <param name="form">要打开的内容窗体</param>
        public void OpenPlanningModule(DockContent form)
        {
            form.Show(this.dockPaneMain);
            form.Activate();
        }

        /// <summary>
        /// 方法说明：打开对应的导航树
        /// 作    者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        /// <param name="form">要打开的内容窗体</param>
        /// <param name="state">导航栏位置</param>
        public void OpenNavigate(DockContent form, DockState state)
        {
            form.Show(this.dockPaneMain, state);
            form.Select();
        }

        /// <summary>
        /// 方法说明：根据窗体名检查该窗体是否已经打开
        /// 作者：jason.tang
        /// 完成时间：2013-07-22
        /// </summary>
        /// <param name="formName">窗体名</param>
        /// <returns></returns>
        public DockContent CheckFormIsOpened(string formName)
        {
            DockContent dockContent = null;

            foreach (DockContent content in dockPaneMain.Contents)
            {
                if (content.Name.EndsWith(formName))
                {
                    dockContent = content;
                    break;
                }
            }

            return dockContent;
        }

        /// <summary>
        /// 方法说明：根据窗体名检查该窗体是否已经打开
        /// 作者：jason.tang
        /// 完成时间：2013-07-22
        /// </summary>
        /// <param name="formName">窗体名</param>
        /// <returns></returns>
        public DockContent CheckContentIsOpened(string tagName)
        {
            DockContent dockContent = null;

            //foreach (DockContent content in dockPaneMain.Contents)
            //{
            //    if (content.Name.EndsWith(tagName))
            //    {
            //        dockContent = content;
            //        break;
            //    }
            //}

            foreach (Form form in Application.OpenForms)
            {
                if (form.Name.EndsWith(tagName) && form is DockContent)
                {
                    dockContent = (DockContent)form;
                }
            }

            return dockContent;
        }   

        /// <summary>
        /// 特殊符号
        /// </summary>
        private void Symbol()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(CardModuleFrm).Name)
            {
                using (SpecialSymbolFrm form = new SpecialSymbolFrm())
                {
                    CardModuleFrm.cardModuleFrm = (CardModuleFrm)PropertiesNavigate.CurrentForm;
                    form.SymbolAddEvent += new ProcessDesign.SpecialSymbolFrm.DelegateForm(CardModuleFrm.cardModuleFrm.GetSymbol);
                    form.ShowDialog();
                }
            }
            else if (formName == typeof(ProcessCardFrm).Name)
            {
                using (SpecialSymbolFrm form = new SpecialSymbolFrm())
                {
                    ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                    form.SymbolAddEvent += new ProcessDesign.SpecialSymbolFrm.DelegateForm(ProcessCardFrm.processCardFrm.GetSymbol);
                    form.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 粗糙度
        /// </summary>
        private void Roughness()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                using (RoughnessMarkFrm form = new RoughnessMarkFrm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                        ProcessCardFrm.processCardFrm.PasteSymbol(form.RoughImage, false);
                    }
                }
            }
        }

        /// <summary>
        /// 焊接符号
        /// </summary>
        private void Welding()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                using (WeldingSymbolFrm form = new WeldingSymbolFrm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                        ProcessCardFrm.processCardFrm.PasteSymbol(form.WeldingSymbolImage, false);
                    }
                }
            }           
        }

        /// <summary>
        /// 形位公差
        /// </summary>
        private void ShapeTolerance()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                using (ShapeToleranceFrm form = new ShapeToleranceFrm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                        ProcessCardFrm.processCardFrm.PasteSymbol(form.ShapeImage, false);
                    }
                }
            }            
        }

        /// <summary>
        /// 公差标注
        /// </summary>
        private void ToleranceMark()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                using (ToleranceMarkFrm form = new ToleranceMarkFrm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                        ProcessCardFrm.processCardFrm.PasteSymbol(form.ToleranceImage, false);
                    }
                }
            }
        }

        /// <summary>
        /// 上下标
        /// </summary>
        private void Subscrip()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                using (SuperscriptAndSubscriptFrm form = new SuperscriptAndSubscriptFrm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                        ProcessCardFrm.processCardFrm.PasteSymbol(form.ScriptImage, false);
                    }
                }
            }
        }

        /// <summary>
        /// 插入图片文件
        /// </summary>
        private void AddImage()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(CardModuleFrm).Name)
            {
                CardModuleFrm.cardModuleFrm = (CardModuleFrm)PropertiesNavigate.CurrentForm;
                CardModuleFrm.cardModuleFrm.ImportImage();
            }
        }

        /// <summary>
        /// 插入OLE对象
        /// </summary>
        private void AddOleObject()
        {
            if (PropertiesNavigate.CurrentForm == null)
                return;

            string formName = PropertiesNavigate.CurrentForm.GetType().Name;
            if (formName == typeof(ProcessCardFrm).Name)
            {
                ProcessCardFrm.processCardFrm = (ProcessCardFrm)PropertiesNavigate.CurrentForm;
                ProcessCardFrm.processCardFrm.SetOleObject();
            }
        }

        /// <summary>
        /// 方法说明：设置工具栏按钮是否可用
        /// 作      者：jason.tang
        /// 完成时间：2013-07-31
        /// </summary>
        /// <param name="formType">0-非模版和卡片 1-模版 2-卡片</param>
        public void SetToolButtonEnable(int formType)
        {
            tsToolbar.Items[5].Enabled = formType == 1;
            tsToolbar.Items[6].Enabled = formType == 1;
            tsToolbar.Items[8].Enabled = formType == 1 || formType == 2;
            tsToolbar.Items[9].Enabled = formType == 2;
            tsToolbar.Items[10].Enabled = formType == 2; ;
            tsToolbar.Items[11].Enabled = formType == 2; ;
            tsToolbar.Items[12].Enabled = formType == 2; ;
            tsToolbar.Items[13].Enabled = formType == 2; ;
            tsToolbar.Items[15].Enabled = formType == 1; ;
        }

        bool fullscreen = false;
        /// <summary>
        /// 全屏显示
        /// </summary>
        public void FullScreen(DockContent dockContent)
        {
            fullscreen = !fullscreen;//循环。点一次全屏，再点还原。
            if (fullscreen)
            {
                //SystemInformation.WorkingArea 获取整个屏幕工作区域的矩形，不含任务栏
                //Screen.PrimaryScreen.Bounds 获取整个屏幕的矩形，含任务栏
                dockContent.DockHandler.FloatAt(SystemInformation.WorkingArea);
                
            }
            else
            {
                dockContent.DockState = DockState.Document;//还原
            }
        }        

        /// <summary>
        /// 关闭其他
        /// </summary>
        public void CloseOther(string formName)
        {
            List<DockContent> listContents = new List<DockContent>();
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name != formName && form is DockContent)
                {
                    listContents.Add(((DockContent)form));
                }
            }

            foreach (DockContent content in listContents)
            {
                if (content.DockState == DockState.Document)
                {
                    content.Close();
                }
            }
        }

        /// <summary>
        /// 关闭所有
        /// </summary>
        public void CloseAll()
        {
            List<DockContent> listContents = new List<DockContent>();
            foreach (Form form in Application.OpenForms)
            {
                if (form is DockContent)
                {
                    listContents.Add(((DockContent)form));
                }
            }

            foreach (DockContent content in listContents)
            {
                if (content.DockState == DockState.Document)
                {
                    content.Close();
                }
            }
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// 方法说明：执行Toolbar按钮事件
        /// </summary>
        /// <param name="commandName"></param>
        public void Excute(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "new":
                    AddCardModule();
                    break;
                case "open":
                    OpenModule();
                    break;
                case "save":
                    Save();
                    break;
                case "data":
                    SaveData();
                    break;
                case "meger":
                    Merge();
                    break;
                case "unmeger":
                    UnMerge();
                    break;
                case "symbol":
                    Symbol();
                    break;
                case "roughness":
                    Roughness();
                    break;
                case "welding":
                    Welding();
                    break;
                case "shapetolerance":
                    ShapeTolerance();
                    break;
                case "tolerance":
                    ToleranceMark();
                    break;
                case "subscrip":
                    Subscrip();
                    break;
                case "image":
                    AddImage();
                    break;
                case "oleobject":
                    AddOleObject();
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
