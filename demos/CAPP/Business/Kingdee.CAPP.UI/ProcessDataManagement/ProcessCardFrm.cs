using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.Common;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using System.Diagnostics;
using Microsoft.Win32;
using System.Reflection;
using Kingdee.CAPP.Common.DataGridViewHelp;
using AxWMPLib;
using System.Net;
using System.Configuration;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 窗体说明：新建卡片界面
    /// 作    者：jason.tang
    /// 完成时间：2013-02-05
    /// </summary>
    public partial class ProcessCardFrm : BaseForm
    {
        #region 变量声明

        /// <summary>
        /// 当前明细列表
        /// </summary>
        private DataGridView dgvTemp;

        /// <summary>
        /// 当前卡片
        /// </summary>
        private DataGridView CurrentDataGrid;

        /// <summary>
        /// 界面卡片和
        /// </summary>
        private int pageCount = 1;

        /// <summary>
        /// 当前编辑的Grid内置文本框
        /// </summary>
        private RichTextBox currTextBox;

        /// <summary>
        /// 当前拷贝的单元格集合
        /// </summary>
        private Dictionary<Point, RichTextBox> dicCopyRichs;
        /// <summary>
        /// 选中的最小坐标单元格
        /// </summary>
        private Point minValue;

        private double zoomNum = 15;

        private int cardWidth = 0;
        private int cardHeight = 0;
        private int cardBreadth = 0;

        private PictureBox currOlePicture = new PictureBox();

        /// <summary>
        /// 卡片panel
        /// </summary>
        private List<Panel> listPanels = null;

        /// <summary>
        /// 卡片路径
        /// </summary>
        private string cardPath = string.Empty;

        /// <summary>
        /// 当前右击的单元格
        /// </summary>
        private DataGridViewCell CurrentClickCell;

        /// <summary>
        /// 没有缩放前的字体
        /// </summary>
        private Font FontBeforeZoom;

        /// <summary>
        /// 工序
        /// </summary>
        private DataTable dtOper = null;

        /// <summary>
        /// 定时保存
        /// </summary>
        private System.Threading.Timer tm = null;

        #endregion

        #region 属性声明

        /// <summary>
        /// 卡片模板ID
        /// </summary>
        public string ModuleId
        {
            get;
            set;
        }

        /// <summary>
        /// 卡片模板名称
        /// </summary>
        public string ModuleName
        {
            get;
            set;
        }

        /// <summary>
        /// 物料/产品对象
        /// </summary>
        public object ModuleObject
        {
            get;
            set;
        }

        /// <summary>
        /// PBOM ID
        /// </summary>
        public string PBomID { get; set; }

        /// <summary>
        /// 工艺文件夹
        /// </summary>
        public string ProcessFolderId { get; set; }

        /// <summary>
        /// 静态属性公布当前窗体，便于其他窗体调用该窗体公用方法
        /// </summary>
        public static ProcessCardFrm processCardFrm { get; set; }

        #endregion

        //AxWindowsMediaPlayer player;
        #region 窗体控件事件

        public ProcessCardFrm()
        {
            InitializeComponent();

            //双缓存
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);

            ProcessCardFrm.processCardFrm = this;

            //ControlOperator operat = new ControlOperator(player);

            //operat.Size = true;  //是否能改变控件大小 
            //operat.Move = true;  //是否能移动控件 
            //operat.Max = false;   //是否能移动大于窗体的位置 
            //operat.Min = false;   //是否能移动到窗体的最前面

            //player = new AxWindowsMediaPlayer();
            //player.Visible = false;
            //dgvCard.Controls.Add(player);
        }

        private void ProcessCardFrm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ModuleId))
            {
                if (!string.IsNullOrEmpty(PBomID) && dtOper == null)
                {
                    dtOper = MaterialModuleBLL.GetOperByPBomId(PBomID);
                }

                GetCardModuleById(ModuleId, dgvCard);                
            }
            this.MouseWheel += new MouseEventHandler(Form_MouseWheel);

            contextMenuStrip.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
            contextMenuTextBox.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
            cmnuTitle.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();

            this.TabPageContextMenuStrip = cmnuTitle;
            //player.MouseDownEvent += new _WMPOCXEvents_MouseDownEventHandler(player_MouseDownEvent);
            //player.MouseMoveEvent += new _WMPOCXEvents_MouseMoveEventHandler(player_MouseMoveEvent);
            //player.MouseUpEvent += new _WMPOCXEvents_MouseUpEventHandler(player_MouseUpEvent);    
                        
            //worker.RunWorkerAsync();
            //tm = new System.Threading.Timer(new System.Threading.TimerCallback(AutoSaveCard), null, 2000, 5000);
        }

        void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                int delta = e.Delta / 120;
                if (delta > 0)
                {
                    zoomNum += 15;
                }
                else
                {
                    zoomNum -= 15;
                    if (zoomNum < 0)
                    {
                        return;
                    }
                }
                foreach (DataGridViewRow row in dgvCard.Rows)
                {
                    foreach (DataGridViewTextBoxCellEx cell in row.Cells)
                    {
                        float diff = e.Delta / 120;
                        if (cell.Style.Font.Size + diff < 0)
                        {
                            break;
                        }
                        try
                        {
                            cell.Style.Font = new Font(cell.Style.Font.FontFamily, cell.Style.Font.Size + diff, cell.Style.Font.Style);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                int height = 0;
                int oldHeight = 0;
                foreach (Control control in this.Controls)
                {
                    Panel panel = (Panel)control;
                    DataGridView dgv = new DataGridView();
                    foreach (Control ctrl in panel.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            dgv = (DataGridView)ctrl;
                        }
                    }
                    panel.Location = new Point(panel.Location.X, panel.Location.Y + height);
                    oldHeight = panel.Height;
                    SetPageZoom(zoomNum, delta, panel, dgv);

                    height += panel.Height - oldHeight;
                }
            }
        }

        /// <summary>
        /// 窗体尺寸重置事件
        /// </summary>
        private void ProcessCardFrm_Resize(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    int remainWidth = this.Width - control.Width;
                    int remainHeight = this.Height - control.Height;
                    if (this.Controls.Count > 1)
                    {
                        remainHeight = 0;
                    }
                    if (remainWidth > 0)
                    {
                        control.Location = new Point(remainWidth / 2, control.Location.Y);
                    }
                    if (remainHeight > 0)
                    {
                        control.Location = new Point(control.Location.X, remainHeight / 2);
                    }
                }
            }
        }

        /// <summary>
        /// CellBeginEdit事件处理器
        /// </summary>
        private void dgvCard_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑
            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //只有标题填写框才可以编辑
            if (cell.CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "1"))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 单元格点击事件处理器
        /// </summary>
        private void dgvCard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dgvTemp = null;
            if (sender == null)
                return;

            MainFrm.mainFrm.SetToolButtonEnable(0);

            DataGridView datagridview = (DataGridView)sender;
            CurrentDataGrid = datagridview;
            ClearPictrueBoxBorder(false);
            currOlePicture = null;
            foreach (Control control in datagridview.Controls)
            {
                if (control is DataGridView)
                {
                    ((DataGridView)control).ClearSelection();
                }
            }

            if (DelegateForm.propertyForm == null)
                return;

            DataGridViewCustomerCellStyle style = new DataGridViewCustomerCellStyle();// (DataGridViewCustomerCellStyle)dgvCard.CurrentCell.Style;
            DataGridViewCellStyle cellStyle = datagridview.CurrentCell.Style;
            style.Font = cellStyle.Font;
            style.ForeColor = cellStyle.ForeColor;
            style.BackColor = cellStyle.BackColor;
            style.Alignment = cellStyle.Alignment;
            style.Padding = cellStyle.Padding;
            style.WrapMode = cellStyle.WrapMode;
            style.CellType = 1;
            if (dgvCard.Tag != null)
            {
                style.CardName = dgvCard.Tag.ToString();
            }

            //if (currTextBox != null)
            //{
            //    currTextBox.BackColor = style.BackColor;
            //    currTextBox.Font = style.Font;
            //    currTextBox.ForeColor = style.ForeColor;
            //    if (style.Alignment.ToString().EndsWith("Right"))
            //    {
            //        currTextBox.SelectionAlignment = HorizontalAlignment.Right;
            //    }
            //    else if (style.Alignment.ToString().EndsWith("Left"))
            //    {
            //        currTextBox.SelectionAlignment = HorizontalAlignment.Left;
            //    }
            //    else
            //    {
            //        currTextBox.SelectionAlignment = HorizontalAlignment.Center;
            //    }
            //}

            DelegateForm.propertyForm.SetPropertyGrid(style, false, true);
        }

        /// <summary>
        /// 方法说明：重写基类的方法，有属性界面委托Invoke
        /// 作    者：jason.tang
        /// 完成时间：2013-04-03
        /// </summary>
        /// <param name="obj"></param>
        public override void SetPropertyEvent(object obj)
        {
            if (obj == null || CurrentClickCell == null)
            {
                return;
            }

            if (obj.GetType() != typeof(DataGridViewCustomerCellStyle))
            {
                return;
            }

            DataGridViewCustomerCellStyle style = (DataGridViewCustomerCellStyle)obj;
            DataGridViewCellStyle cellStyle = new DataGridViewCellStyle();
            cellStyle.Font = style.Font;
            cellStyle.ForeColor = style.ForeColor;
            cellStyle.BackColor = style.BackColor;
            style.CellType = 1;

            if (CurrentClickCell.GetType() == typeof(DataGridViewRichTextBoxCell))
            {
                if (style.RichAlignment == HorizontalAlignment.Left)
                {
                    cellStyle.Alignment = DataGridViewContentAlignment.TopLeft;
                }
                else if (style.RichAlignment == HorizontalAlignment.Right)
                {
                    cellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
                else if (style.RichAlignment == HorizontalAlignment.Center)
                {
                    cellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                }
            }
            else
                cellStyle.Alignment = style.Alignment;

            cellStyle.Padding = style.Padding;
            cellStyle.WrapMode = style.WrapMode;

            if (currTextBox != null)
            {
                currTextBox.BackColor = style.BackColor;
                currTextBox.Font = style.Font;
                currTextBox.ForeColor = style.ForeColor;
                //if (style.Alignment.ToString().EndsWith("Right"))
                //{
                //    currTextBox.SelectionAlignment = HorizontalAlignment.Right;
                //}
                //else if (style.Alignment.ToString().EndsWith("Left"))
                //{
                //    currTextBox.SelectionAlignment = HorizontalAlignment.Left;
                //}
                //else if (style.Alignment.ToString().EndsWith("Center"))
                //{
                //    currTextBox.SelectionAlignment = HorizontalAlignment.Center;
                //}
                currTextBox.SelectionAlignment = style.RichAlignment;
            }

            if (!string.IsNullOrEmpty(style.CardName))
            {
                dgvCard.Tag = style.CardName;
            }

            CurrentClickCell.Style = cellStyle;

            if (CurrentClickCell.GetType() == typeof(DataGridViewRichTextBoxCell))
            {
                style.CellType = 2;
                DataGridViewRichTextBoxCell richTextBoxCell = (DataGridViewRichTextBoxCell)CurrentClickCell;
                richTextBoxCell.Style = cellStyle;
            }
            else if (CurrentClickCell.GetType() == typeof(DataGridViewTextBoxCellEx))
            {

                DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)CurrentClickCell;
                List<DataGridViewCustomerCellStyle> listCellStyle = new List<DataGridViewCustomerCellStyle>();
                listCellStyle.Add(style);
                cellEx.CustStyle = listCellStyle;
            }
            
        }

        /// <summary>
        /// 重写ProcessCmdKey方法,DataGridView单元格回车换行
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgvTemp != null && dgvTemp.CurrentCell != null &&
                (keyData == Keys.Enter || keyData == Keys.Return))
            {
                //当前行在为明细列表的最后一行
                if (dgvTemp.CurrentCell.RowIndex == dgvTemp.RowCount - 1)
                {
                    int index = int.Parse(dgvTemp.Name.Substring(dgvTemp.Name.IndexOf("@") + 1)) + 1;
                    //如果当前卡片为最后一页则新增卡片
                    if (dgvTemp.Name.Contains("@") &&
                        dgvTemp.Name.Substring(dgvTemp.Name.IndexOf("@") + 1) == pageCount.ToString())
                    {
                        AddNewCard();

                        if (listPanels != null && listPanels.Count > 0)
                        {
                            int rang = 0;
                            foreach (Panel pn in listPanels)
                            {
                                if (pn.Name != "pnCard" && rang - 1 >= 0)
                                {
                                    pn.Location = new Point(listPanels[rang - 1].Location.X, listPanels[rang - 1].Location.Y + pnCard.Height + 10);
                                }
                                rang++;
                            }
                        }
                    }
                    else
                    {
                        //否则跳到下一卡片的明细列表.
                        SetNextFocus(index);
                    }
                }
                else
                {
                    dgvTemp.EditMode = DataGridViewEditMode.EditOnEnter;

                    int returnNumber = 1;
                    foreach (DetailGridViewTextBoxColumn column in dgvTemp.Columns)
                    {
                        //间隔行数不可编辑
                        if (!string.IsNullOrEmpty(column.SpaceRows))
                        {
                            int spaceRows = int.Parse(column.SpaceRows) + 1;
                            bool isContainer = false;
                            for (int i = 0; i < dgvTemp.Rows.Count; i += spaceRows)
                            {
                                if (dgvTemp.CurrentCell.RowIndex == i)
                                {
                                    isContainer = true;
                                    break;
                                }
                            }
                            //如果在间隔开始行，则发生间隔行次回车
                            if (isContainer)
                            {
                                returnNumber = spaceRows;
                            }
                        }
                    }

                    for (int i = 0; i < returnNumber; i++)
                    {
                        SendKeys.Send("+~");
                    }

                    dgvTemp.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                }

                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 导出卡片为图片
        /// </summary>
        private void tsmnuSaveToBmp_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(dgvCard.Width, dgvCard.Height);
            //dgvCard.DrawToBitmap(bmp, new Rectangle(0, 0, this.dgvCard.Width, this.dgvCard.Height));
            //bmp.Save(@"D:\\test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

            //Bitmap bit = new Bitmap(dgvTemp.Width, dgvTemp.Height);//实例化一个和窗体一样大的bitmap
            //Graphics g = Graphics.FromImage(bit);
            //g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;//质量设为最高
            //g.CopyFromScreen(dgvTemp.Location.X, dgvTemp.Location.Y, 0, 0, new Size(dgvTemp.Width, dgvTemp.Height));//保存整个窗体为图片
            ////g.CopyFromScreen(panel游戏区 .PointToScreen(Point.Empty), Point.Empty, panel游戏区.Size);//只保存某个控件（这里是panel游戏区）
            //bit.Save("weiboTemp.png");//默认保存格式为PNG，保存成jpg格式质量不是很好
            //this.pnCard.DrawToBitmap(bmp, new Rectangle(0, 0, this.pnCard.Width, this.pnCard.Height));
            //bmp.Save("panel.png", System.Drawing.Imaging.ImageFormat.Png);
            DataGridViewToBmpFrm form = new DataGridViewToBmpFrm();
            form.TotalPage = pageCount.ToString();
            DialogResult result = form.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string path = form.FilePath;
                string name = form.FileName;
                string imageType = form.ImageType;
                string range = form.PageRange;

                //所有页
                if (range == "All")
                {
                    int index = 1;
                    foreach (Control control in this.Controls)
                    {
                        if (control is Panel)
                        {
                            foreach (Control ctrl in control.Controls)
                            {
                                if (ctrl is DataGridView)
                                {
                                    ConvertDataGridViewToBmp((DataGridView)ctrl, string.Format(path + name + "_{0}.{1}", index.ToString(), imageType.ToLower()));
                                }
                                index++;
                            }
                        }
                    }
                }//当前页
                else if (range == "Current")
                {
                    DataGridView dgvCurrent = (DataGridView)dgvTemp.Parent;
                    ConvertDataGridViewToBmp(dgvCurrent, string.Format(path + name + "_1.{0}", imageType.ToLower()));
                }
                else//页码范围
                {
                    string[] strRanges = range.Split(new char[] { ',' });
                    int index = 1;
                    foreach (Control control in this.Controls)
                    {
                        if (control is Panel)
                        {
                            foreach (Control ctrl in control.Controls)
                            {
                                string ctrName = ctrl.Name.Substring(ctrl.Name.IndexOf("@") + 1);
                                if (ctrName == "dgvCard")
                                {
                                    ctrName = "1";
                                }
                                if (ctrl is DataGridView &&
                                    strRanges.Contains<string>(ctrName))
                                {
                                    ConvertDataGridViewToBmp((DataGridView)ctrl, string.Format(path + name + "_{0}.{1}", index.ToString(), imageType.ToLower()));
                                    index++;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 特殊符号
        /// </summary>
        private void tsmnuSymbol_Click(object sender, EventArgs e)
        {
            ProcessDesign.SpecialSymbolFrm form = new ProcessDesign.SpecialSymbolFrm();
            //form.TopMost = true;
            form.SymbolAddEvent += new ProcessDesign.SpecialSymbolFrm.DelegateForm(GetSymbol);
            form.Show();
        }

        /// <summary>
        /// 导出明细框数据
        /// </summary>
        private void tsmnuExportDetail_Click(object sender, EventArgs e)
        {
            ExportDataToTxt();
        }

        /// <summary>
        /// 从文本文件导入明细框数据
        /// </summary>
        private void tsmnuImportDetail_Click(object sender, EventArgs e)
        {
            ImportFromTxt();
        }

        /// <summary>
        /// 保存卡片
        /// </summary>
        private void tsmnuSave_Click(object sender, EventArgs e)
        {
            SaveCard();
        }

        /// <summary>
        /// 打开卡片
        /// </summary>
        private void tsmnuOpenCard_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "CARD files (*.card)|*.card";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenCard(dialog.FileName, null, false, true);
            }
        }

        /// <summary>
        /// OLE对象
        /// </summary>
        private void tsmnuOleObject_Click(object sender, EventArgs e)
        {
            SetOleObject();
        }

        void pOle_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                currOlePicture = (PictureBox)sender;
                ClearPictrueBoxBorder(true);
            }
        }

        void pOle_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Process p;
            if (pb.Name.StartsWith("pCad"))
            {
                string path = string.Empty;
                List<string> listDatas = new List<string>();
                //获取CAD的安装路径
                RegistryKey machine = Registry.LocalMachine;
                RegistryKey autocad = machine.OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD", false);
                string[] cadSubKeys = autocad.GetSubKeyNames();
                if (cadSubKeys.Length > 0)
                {
                    RegistryKey verSubKeys = autocad.OpenSubKey(cadSubKeys[0], false);
                    string[] verdata = verSubKeys.GetSubKeyNames();
                    if (verdata.Length > 0)
                    {
                        RegistryKey ss = verSubKeys.OpenSubKey(verdata[0]);
                        string[] names = ss.GetValueNames();
                        path = ss.GetValue("AcadLocation").ToString();
                    }
                }
                string watcherPath = string.IsNullOrEmpty(pb.ImageLocation) ? Application.StartupPath + @"\temp" : pb.ImageLocation.Substring(0, pb.ImageLocation.LastIndexOf("\\"));
                if (!Directory.Exists(Application.StartupPath + "\\temp"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\temp");
                }
                fileSystemWatcher.Path = watcherPath;
                if (string.IsNullOrEmpty(pb.ImageLocation))
                {
                    pb.ImageLocation = watcherPath + @"\Drawing1.dwg";
                }

                string filePath = pb.ImageLocation.Replace("_cut.png", ".dwg");
                string imgFilePath = filePath.Replace(".dwg", "_cut.dwg").Replace(".dwg", ".png");

                string isExist = "false";
                if (File.Exists(imgFilePath))
                {
                    isExist = "true";
                }

                filePath = string.Format("\"{0}\"", filePath);
                p = Process.Start(path + @"\acad.exe", string.Format(filePath + " {0} {1} {2}", pb.Width, pb.Height, isExist));
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, this.Handle);
                ShowWindowAsync(p.MainWindowHandle, 3);

                if (File.Exists(imgFilePath.Replace(".png", ".dwg")))
                {
                    UploadFun(imgFilePath.Replace(".png", ".dwg"));
                }
            }
            else if (pb.Name.StartsWith("pBmp"))
            {
                if (pb.Name.Contains("@"))
                {
                    string filePath = pb.Name.Substring(pb.Name.IndexOf("@") + 1);
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            pb.ImageLocation = filePath;
                        }
                        else
                        {
                            string path = Application.StartupPath + "\\temp\\" + new FileInfo(filePath).Name;
                            if (!Directory.Exists(Application.StartupPath + "\\temp"))
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\temp");
                            }
                            if (DownloadFtp(path) == 0)
                            {
                                pb.Name = pb.Name.Substring(0, pb.Name.IndexOf("@")) + "@" + path;
                                pb.ImageLocation = path;
                            }
                        }
                    }
                    try
                    {
                        List<string> oldpath = GetRegistData();

                        p = Process.Start("mspaint", "\"" + pb.ImageLocation + "\"");
                        p.WaitForInputIdle();
                        SetParent(p.MainWindowHandle, this.Handle);
                        bool isshow = ShowWindowAsync(p.MainWindowHandle, 3);

                        p.WaitForExit();

                        List<string> newpath = GetRegistData();

                        HashSet<string> h1 = new HashSet<string>(oldpath);
                        HashSet<string> h2 = new HashSet<string>(newpath);
                        List<string> listResult = new List<string>();
                        h2.ExceptWith(h1);
                        if (h2.Count > 0)
                        {
                            foreach (string s in h2)
                            {
                                pb.ImageLocation = s;
                                //pb.Image = Image.FromFile(s);
                            }
                        }
                        else
                        {
                            if (File.Exists(oldpath[0]))
                            {
                                pb.ImageLocation = oldpath[0];
                                //pb.Image = Image.FromFile(oldpath[0]);
                            }
                        }

                        pb.Name = pb.Name.Substring(0, pb.Name.IndexOf("@")) + "@" + pb.ImageLocation;
                        UploadFun(pb.ImageLocation);

                        if (File.Exists(filePath) && pb.ImageLocation != filePath)
                        {
                            File.Delete(filePath);
                        }
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        List<string> oldpath = GetRegistData();

                        if (!Directory.Exists(Application.StartupPath + "\\temp"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "\\temp");
                        }

                        var img = Image.FromFile(Application.StartupPath + "\\temp\\temp.bmp");
                        var thumbnail = img.GetThumbnailImage(pb.Width, pb.Height, null, IntPtr.Zero);
                        string imgName = Guid.NewGuid().ToString();
                        string thumbPath = Application.StartupPath + string.Format("\\temp\\{0}.bmp", imgName);
                        thumbnail.Save(thumbPath);

                        p = Process.Start("mspaint", "\"" + thumbPath + "\"");
                        p.WaitForInputIdle();
                        SetParent(p.MainWindowHandle, this.Handle);
                        bool isshow = ShowWindowAsync(p.MainWindowHandle, 3);

                        p.WaitForExit();

                        List<string> newpath = GetRegistData();

                        HashSet<string> h1 = new HashSet<string>(oldpath);
                        HashSet<string> h2 = new HashSet<string>(newpath);
                        List<string> listResult = new List<string>();
                        h2.ExceptWith(h1);
                        if (h2.Count > 0)
                        {
                            foreach (string s in h2)
                            {
                                pb.ImageLocation = s;
                                //pb.Image = Image.FromFile(s);
                            }
                        }
                        else
                        {
                            if (newpath.Count > 0 && oldpath.Count > 0)
                            {
                                if (newpath[0] != oldpath[0] && File.Exists(newpath[0]))
                                {
                                    pb.ImageLocation = newpath[0];
                                }
                                else if (newpath[0] == oldpath[0])
                                {
                                    pb.ImageLocation = thumbPath;
                                }

                                //pb.Image = Image.FromFile(oldpath[0]);
                            }

                        }

                        pb.Name = pb.Name + "@" + pb.ImageLocation;
                        UploadFun(pb.ImageLocation);

                        if (File.Exists(thumbPath) && pb.ImageLocation != thumbPath)
                        {
                            File.Delete(thumbPath);
                        }
                    }
                    catch
                    { }
                }
                
            }
            else if (pb.Name.StartsWith("AVI"))
            {
                //string videoPath = @"C:\Users\franco.zhan\Desktop\aa.avi";
                //player.Size = new Size(pb.Width,pb.Height);
                //player.Location = pb.Location;
                //player.Visible = true;
                //player.newMedia(videoPath);
                //player.URL = videoPath;
                //player.uiMode = "none";
                //player.BringToFront();
                using (MediaFrm form = new MediaFrm())
                {
                    if (pb.Name.Contains("@"))
                    {
                        string filePath = pb.Name.Substring(pb.Name.IndexOf("@") + 1);
                        if (File.Exists(filePath))
                        {
                            form.VideoPath = filePath;
                        }
                        else
                        {
                            string path = Application.StartupPath + "\\temp\\" + new FileInfo(filePath).Name;
                            if (!Directory.Exists(Application.StartupPath + "\\temp"))
                            {
                                Directory.CreateDirectory(Application.StartupPath + "\\temp");
                            }
                            DownloadFtp(path);
                            pb.Name = pb.Name.Substring(0, pb.Name.IndexOf("@")) + "@" + path;
                            form.VideoPath = path;
                        }
                        form.ShowDialog();
                    }
                    else
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.InitialDirectory = "c:\\";
                        openFileDialog.Filter = "AVI(*.avi)|*.avi";
                        openFileDialog.RestoreDirectory = true;
                        openFileDialog.FilterIndex = 1;
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            form.VideoPath = openFileDialog.FileName;
                            pb.Name = pb.Name + "@" + openFileDialog.FileName;
                            UploadFun(openFileDialog.FileName);
                            form.ShowDialog();
                        }
                    }
                }
            }
      
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void tsmnuCopy_Click(object sender, EventArgs e)
        {
            DataGridViewCopy(dgvTemp);
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void tsPaster_Click(object sender, EventArgs e)
        {
            DataGridViewPaste();
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void tsmnuCut_Click(object sender, EventArgs e)
        {
            DataGridViewCut(dgvTemp);
        }

        private void tsmnuTextCut_Click(object sender, EventArgs e)
        {
            TextCut();
        }

        private void tsmnuTextPaste_Click(object sender, EventArgs e)
        {
            TextPaste();
        }

        private void tsmnuTextCopy_Click(object sender, EventArgs e)
        {
            TextCopy();
        }

        /// <summary>
        /// 粗糙度标注
        /// </summary>
        private void tsmnuRoughnessMark_Click(object sender, EventArgs e)
        {
            RoughnessMarkFrm form = new RoughnessMarkFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK && currTextBox != null)
            {
                Clipboard.SetDataObject(form.RoughImage, false);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 方法说明：粘贴符号(粗糙度、焊接符号等)
        /// 作      者：jason.tang
        /// 完成时间：2013-07-26
        /// </summary>
        public void PasteSymbol(object obj, bool copy)
        {
            if (currTextBox != null)
            {
                Clipboard.SetDataObject(obj, copy);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 焊接符号
        /// </summary>
        private void tsmnuWeldingSymbol_Click(object sender, EventArgs e)
        {
            WeldingSymbolFrm form = new WeldingSymbolFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Clipboard.SetDataObject(form.WeldingSymbolImage, false);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 形位公差
        /// </summary>
        private void tsmnuShapeTolerance_Click(object sender, EventArgs e)
        {
            ShapeToleranceFrm form = new ShapeToleranceFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Clipboard.SetDataObject(form.ShapeImage, false);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 公差标注
        /// </summary>
        private void tsmnuToleranceMark_Click(object sender, EventArgs e)
        {
            ToleranceMarkFrm form = new ToleranceMarkFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Clipboard.SetDataObject(form.ToleranceImage, false);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 上标下标
        /// </summary>
        private void tsmnuSuperscriptAndSubscript_Click(object sender, EventArgs e)
        {
            SuperscriptAndSubscriptFrm form = new SuperscriptAndSubscriptFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Clipboard.SetDataObject(form.ScriptImage, false);
                currTextBox.Paste();
            }
        }

        /// <summary>
        /// 文件变动监听
        /// </summary>
        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (currOlePicture != null && currOlePicture.Name.StartsWith("pCad") && currOlePicture.ImageLocation != null)
            {
                currOlePicture.ImageLocation = currOlePicture.ImageLocation.Replace(".dwg", "_cut.dwg").Replace(".dwg", ".png"); ;
            }
        }

        /// <summary>
        /// 如果单元格属性为OLE对象，这绘制一张OLE图片
        /// </summary>
        private void dgvCard_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                //DataGridView dgv = (DataGridView)sender;
                //Image oleImage = Kingdee.CAPP.UI.Properties.Resources.oleobj;
                //DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                //if (cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "1") ||
                //    cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "2"))
                //{
                //    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                //    Rectangle rect = new Rectangle(e.CellBounds.X, e.CellBounds.Y, oleImage.Width, oleImage.Height);
                //    e.Graphics.DrawImage(oleImage, rect);
                //    e.Handled = true;
                //}
            }
            catch
            {
            }
        }

        /// <summary>
        /// 单元格属性
        /// </summary>
        private void tsmnuCellProperty_Click(object sender, EventArgs e)
        {
            //Kingdee.CAPP.UI.ProcessDesign.CellPropertiesFrm form = new Kingdee.CAPP.UI.ProcessDesign.CellPropertiesFrm();
            //form.CellProperties = GetCellProperties();
            //if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    RefreshCell(form.CellProperties);
            //}
        }

        /// <summary>
        /// 整行显示
        /// </summary>
        private void tsmnuAllLine_Click(object sender, EventArgs e)
        {
            CurrentClickCell.Style.WrapMode = DataGridViewTriState.False;
            try
            {
                Graphics g = this.CreateGraphics();
                Font font = CurrentClickCell.Style.Font;
                object value = CurrentClickCell.Value;
                if (font == null && currTextBox != null)
                {
                    font = currTextBox.Font;
                    value = currTextBox.Text;
                }
                FontBeforeZoom = font;
                float width = g.MeasureString(value.ToString(), font).Width;
                float fontSize = font.Size;
                while (CurrentClickCell.OwningColumn.Width - width < 0)
                {
                    fontSize -= 0.5f;
                    font = new Font(font.Name, fontSize, font.Style);
                    width = g.MeasureString(value.ToString(), font).Width;
                }
                CurrentClickCell.Style.Font = new Font(font.Name, fontSize, font.Style);
            }
            catch
            { }
        }

        /// <summary>
        /// 换行显示
        /// </summary>
        private void tsmnuNewLine_Click(object sender, EventArgs e)
        {
            CurrentClickCell.Style.WrapMode = DataGridViewTriState.True;
            CurrentClickCell.Style.Font = FontBeforeZoom;
        }

        /// <summary>
        /// DataGridView单元格双击，如果来源是从数据选取，则打开选择界面
        /// </summary>
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender == null)
                return;
            string source = string.Empty;

            DataGridView datagridview = (DataGridView)sender;
            //if (datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].GetType() == typeof(DataGridViewTextBoxCellEx))
            //{
            //    DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //    source = cell.CellSource;
            //}
            //else
            //{
            //    DetailGridViewTextBoxColumn column = (DetailGridViewTextBoxColumn)datagridview.Columns[e.ColumnIndex];
            //    source = column.Source;
            //}

            if (datagridview.Columns[e.ColumnIndex].GetType() == typeof(DetailGridViewTextBoxColumn))
            {                
                DetailGridViewTextBoxColumn column = (DetailGridViewTextBoxColumn)datagridview.Columns[e.ColumnIndex];
                source = column.Source;
                
                if (string.IsNullOrEmpty(source))// && source == "Other")
                {
                    bool isContainer = false;
                    //间隔行数不可编辑
                    if (!string.IsNullOrEmpty(column.SpaceRows))
                    {
                        int spaceRows = int.Parse(column.SpaceRows) + 1;
                        string pageNumber = datagridview.Name.Substring(datagridview.Name.IndexOf("@") + 1);
                        int remainder = datagridview.Rows.Count % spaceRows;
                        int startIndex = 0;
                        if (pageNumber != "1" && remainder > 0)
                        {
                            startIndex = spaceRows - (int.Parse(pageNumber) - 1);
                        }
                        
                        for (int i = startIndex; i < dgvTemp.Rows.Count; i += spaceRows)
                        {
                            if (e.RowIndex == i)
                            {
                                isContainer = true;
                                break;
                            }
                        }

                        //如果是空白行或序号行则也可以编辑
                        foreach (DataGridViewCell cell in dgvTemp.Rows[e.RowIndex].Cells)
                        {
                            if (cell.Style.Tag != null && (cell.Style.Tag.ToString() == "1" || cell.Style.Tag.ToString() == "2"))
                            {
                                isContainer = true;
                                break;
                            }
                            else
                                isContainer = false;
                        }
                    }

                    if (!isContainer)
                        return;

                    SourceFromDataFrm form = new SourceFromDataFrm();
                    if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!string.IsNullOrEmpty(form.FieldSource))// && ModuleObject != null)
                        {
                            //string value = string.Empty;
                            //foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
                            //{
                            //    if (pi.Name.ToLower() == form.FieldSource.ToLower())
                            //    {
                            //        value = pi.GetValue(ModuleObject, new object[] { }).ToString();
                            //        break;
                            //    }
                            //}

                            CurrentClickCell.Value += form.FieldSource;
                            if (currTextBox != null)
                                currTextBox.Text += form.FieldSource;
                        }
                    }
                }
            }            
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView datagridview = (DataGridView)sender;
            if (e.KeyCode == Keys.Delete && currOlePicture != null && !string.IsNullOrEmpty(currOlePicture.Name))
            {
                Control[] controls = datagridview.Controls.Find(currOlePicture.Name, false);
                if (controls != null && controls.Length > 0)
                {
                    datagridview.Controls.RemoveByKey(currOlePicture.Name);
                }
            }
        }

        Point srcPoint;
        bool isState = false;
        private void player_MouseDownEvent(object sender, AxWMPLib._WMPOCXEvents_MouseDownEvent e)
        {
            if (e.nButton == 1)
            {
                srcPoint = new Point(e.fX, e.fY);
                ((Control)sender).BringToFront();
            }
        }

        private void player_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            ///判断为左键 
            if (e.nButton == 1)
            {
                ((Control)sender).Location = new Point(((Control)sender).Location.X + e.fX - srcPoint.X,
                    ((Control)sender).Location.Y + e.fY - srcPoint.Y);

            }
        }

        private void player_MouseUpEvent(object sender, AxWMPLib._WMPOCXEvents_MouseUpEvent e)
        {
            isState = false;
        }

        /// <summary>
        /// 全屏显示
        /// </summary>
        private void tsmnuFullScreen_Click(object sender, EventArgs e)
        {
            MainFrm.mainFrm.FullScreen(this);
            tsmnuFullScreen.Text = this.DockState == WeifenLuo.WinFormsUI.Docking.DockState.Document ? "全屏显示" : "退出全屏";
        }

        /// <summary>
        /// 关闭当前
        /// </summary>
        private void tsmnuCloseCurrent_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 关闭其他
        /// </summary>
        private void tsmnuCloseOther_Click(object sender, EventArgs e)
        {
            MainFrm.mainFrm.CloseOther(this.Name);
        }

        /// <summary>
        /// 关闭所有
        /// </summary>
        private void tsmnuCloseAll_Click(object sender, EventArgs e)
        {
            MainFrm.mainFrm.CloseAll();
        }

        /// <summary>
        /// 窗体关闭时，释放Timer
        /// </summary>
        private void ProcessCardFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(tm != null)
                tm.Dispose();
        }

        /// <summary>
        /// 插入整行
        /// </summary>
        private void tsmnuInsertRow_Click(object sender, EventArgs e)
        {
            try
            {
                int currRowIndex = CurrentClickCell.RowIndex;
                if (dgvTemp != null && currRowIndex > 0)
                {
                    int rowHeight = dgvTemp.Rows[currRowIndex].Height;
                    dgvTemp.Rows.Insert(currRowIndex, 1);
                    dgvTemp.Rows.RemoveAt(dgvTemp.Rows.Count - 1);
                    dgvTemp.Rows[currRowIndex].Height = rowHeight;
                    dgvTemp.Rows[currRowIndex].Cells[0].Style.Tag = 1;                    
                }

                if (listDetails.Count > 0)
                {
                    foreach (DataGridView dgv in listDetails)
                    {                        
                       SetSerialRows(dgv);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region 方法

        //引用gdi32.dll API函数
        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(
        IntPtr hdcDest,    //handle to destination DC   
        int nXDest,        //x-coord of destination upper-left corner   
        int nYDest,        //y-coord of destination upper-left corner   
        int nWidth,        //width of destination rectangle   
        int nHeight,       //height of destination rectangle   
        IntPtr hdcSrc,     //handle to source DC   
        int nXSrc,         //x-coordinate of source upper-left corner   
        int nYSrc,         //y-coordinate of source upper-left corner   
        System.Int32 dwRop //raster operation code   
        );

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 方法说明：将DataGridView转换为Bmp图片
        /// 作    者：jason.tang
        /// 完成时间：2013-02-19
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="sFilePath"></param>
        private void ConvertDataGridViewToBmp(DataGridView dg, string sFilePath)
        {
            System.Threading.Thread.Sleep(100);
            dg.ClearSelection();
            if (dg.HasChildren)
            {
                foreach (Control control in dg.Controls)
                {
                    if (control is DataGridView)
                    {
                        ((DataGridView)control).ClearSelection();
                    }
                }
            }
            dg.Focus();
            dg.Refresh();
            dg.Select();

            Graphics g = dg.CreateGraphics();
            Bitmap ibitMap = new Bitmap(dg.ClientSize.Width, dg.ClientSize.Height, g);
            Graphics iBitMap_gr = Graphics.FromImage(ibitMap);
            IntPtr iBitMap_hdc = iBitMap_gr.GetHdc();
            IntPtr me_hdc = g.GetHdc();

            BitBlt(iBitMap_hdc, 0, 0, dg.ClientSize.Width, dg.ClientSize.Height, me_hdc, 0, 0, 13369376);
            g.ReleaseHdc(me_hdc);
            iBitMap_gr.ReleaseHdc(iBitMap_hdc);

            ibitMap.Save(sFilePath, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        /// <summary>
        /// 方法说明：根据模板ID得到模板
        /// 作    者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="moduleId">模板ID</param>
        private void GetCardModuleById(string moduleid, DataGridView datagridview)
        {
            ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
            CardsXML cardsmodule = new CardsXML();

            try
            {
                cardsmodule = pcmDll.GetCardModule(new Guid(moduleid));
            }
            catch
            {
                return;
            }

            Card cardmodule = cardsmodule.Cards.FirstOrDefault<Card>();

            int Width = Convert.ToInt32(cardmodule.Width);
            int Height = Convert.ToInt32(cardmodule.Height);
            int breadth = int.Parse(cardmodule.CardRange.Replace("A", ""));

            cardWidth = Width;
            cardHeight = Height;
            cardBreadth = breadth;

            string materialName = string.Empty;
            if (ModuleObject != null)
            {
                materialName = ((MaterialModule)ModuleObject).name;
            }
            dgvCard.Tag = string.IsNullOrEmpty(materialName) ? cardmodule.Name : string.Format("{0}-{1}", materialName, cardmodule.Name);

            if (datagridview.Name == "dgvCard")
            {
                ResizeControls(Width, Height, breadth);
            }

            List<DataGridViewRow> listRow = new List<DataGridViewRow>();

            int rows = cardmodule.Rows.Length;
            int columns = cardmodule.Rows[0].Cells.Length;

            List<int> listWidth = new List<int>();
            List<int> listHeight = new List<int>();

            foreach (Row row in cardmodule.Rows)
            {
                listHeight.Add(Convert.ToInt32(row.Height));
            }

            foreach (Cell cell in cardmodule.Rows[0].Cells)
            {
                listWidth.Add(Convert.ToInt32(cell.Width));
            }

            InitDataGridView(listHeight, listWidth, datagridview);

            DataGridViewTextBoxCellEx cellEx;
            List<DataGridViewCustomerCellStyle> listCellStyle;
            DataGridViewCustomerCellStyle cellStyle;
            List<int> listPadding;

            try
            {
                //工艺路线
                DataTable dtRouting = null;
                if (!string.IsNullOrEmpty(PBomID))
                {
                    dtRouting = MaterialModuleBLL.GetRoutingByPBomId(PBomID);
                }

                foreach (Row row in cardmodule.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cellEx = ((DataGridViewTextBoxCellEx)datagridview.Rows[cell.PointX].Cells[cell.PointY]);
                        cellEx.Style.Alignment = (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cell.Alignment);
                        cellEx.Style.BackColor = int.Parse(cell.BackGround) == 0 ? Color.White : Color.FromArgb(int.Parse(cell.BackGround));
                        cellEx.BottomBorderColor = Color.FromArgb(int.Parse(cell.BottomBorderColor));
                        cellEx.BottomBorderWidth = Convert.ToInt32(cell.BottomBorderWidth);
                        cellEx.CellEditType = (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), cell.CellType);
                        cellEx.ColumnSpan = cell.ColSpan;
                        if (!string.IsNullOrEmpty(cell.CellSource))
                        {                         

                            string sourceName = cell.CellSource.Substring(cell.CellSource.IndexOf("\\") + 1);
                            string typeName = cell.CellSource.Substring(0, cell.CellSource.IndexOf("\\"));

                            if (ModuleObject != null && typeName.ToLower() != "routing")
                            {
                                foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
                                {
                                    if (pi.Name.ToLower() == sourceName.ToLower())
                                    {
                                        cellEx.Value = pi.GetValue(ModuleObject, new object[] { });
                                        break;
                                    }
                                }
                            }
                            else if (dtRouting != null && typeName.ToLower() == "routing")
                            {
                                foreach (DataRow dr in dtRouting.Rows)
                                {
                                    foreach (DataColumn col in dtRouting.Columns)
                                    {
                                        if (col.ColumnName.ToLower() == sourceName.ToLower())
                                        {
                                            cellEx.Value = dr[col].ToString();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            cellEx.Value = cell.Content;
                        }
                        string[] spanCell = cell.SpanCell.Split(new char[] { ',' });
                        cellEx.SpanCell = new Point(int.Parse(spanCell[0]), int.Parse(spanCell[1]));

                        #region 处理CellStyle

                        listCellStyle = new List<DataGridViewCustomerCellStyle>();
                        listPadding = new List<int>();
                        if (cellEx.CustStyle == null)
                        {
                            cellStyle = new DataGridViewCustomerCellStyle();
                            cellStyle.Alignment = (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cell.Alignment);
                            cellStyle.BackColor = int.Parse(cell.BackGround) == 0 ? Color.White : Color.FromArgb(int.Parse(cell.BackGround));
                            cellStyle.Font = new Font(cell.FontFamily, float.Parse(cell.ZoomFontSize), (FontStyle)Enum.Parse(typeof(FontStyle), cell.FontStyle));
                            cellStyle.ForeColor = Color.FromArgb(int.Parse(cell.ForeColor));
                            cellStyle.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());

                            string[] padding = cell.Padding.Split(new char[] { ',' });
                            foreach (string pad in padding)
                            {
                                listPadding.Add(int.Parse(pad));
                            }
                            cellStyle.Padding = new System.Windows.Forms.Padding(listPadding[0], listPadding[1], listPadding[2], listPadding[3]);
                            listCellStyle.Add(cellStyle);
                            cellEx.CustStyle = listCellStyle;
                        }

                        #endregion

                        //cell.ContentType;
                        //cell.DataSrc;
                        //cell.DetailCells;

                        cellEx.Style.Font = new Font(cell.FontFamily, float.Parse(cell.FontSize), (FontStyle)Enum.Parse(typeof(FontStyle), cell.FontStyle));
                        cellEx.Style.ForeColor = Color.FromArgb(int.Parse(cell.ForeColor));
                        cellEx.LeftBorderColor = Color.FromArgb(int.Parse(cell.LeftBorderColor));
                        cellEx.LeftBorderWidth = Convert.ToInt32(cell.LeftBorderWidth);
                        //cell.Name;
                        cellEx.RightBorderColor = Color.FromArgb(int.Parse(cell.RightBorderColor));
                        cellEx.RightBorderWidth = Convert.ToInt32(cell.RightBorderWidth);
                        cellEx.RowSpan = cell.RowSpan;
                        cellEx.TopBorderColor = Color.FromArgb(int.Parse(cell.TopBorderColor));
                        cellEx.TopBorderWidth = Convert.ToInt32(cell.TopBorderWidth);

                        cellEx.LeftTopRightBottom = cell.LeftTopRightBottom;
                        cellEx.LeftBottomRightTop = cell.LeftBottomRightTop;
                        cellEx.CellContent = string.IsNullOrEmpty(cell.ContentType) ? (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0") :
                            (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), cell.ContentType);

                        cellEx.Style.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());
                        cellEx.CellSource = cell.CellSource;
                        cellEx.CellTag = cell.CellTag;

                        string parentName = datagridview.Parent.Name;
                        //明细框单元格
                        if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                        {
                            Rectangle rect = datagridview.GetCellDisplayRectangle(cellEx.ColumnIndex, cellEx.RowIndex, false);
                            int top = rect.Y - 1;
                            int left = rect.X - 1;

                            List<DetailGridViewTextBoxColumn> dicColumns = new List<DetailGridViewTextBoxColumn>();
                            object objDetailProperty = cell.DetailCells;
                            //明细框
                            AddDetailGridView(top, left, objDetailProperty, cellEx, datagridview, dicColumns, false);
                            cellEx.DetailProperty = dicColumns;
                        }
                        else if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "3"))  //页码
                        {
                            cellEx.Value = 1;
                        }
                        else if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "4"))  //页数
                        {
                            cellEx.Value = 1;
                        }
                    }
                }

                //读取图片
                if (cardmodule.ImageObjects != null && cardmodule.ImageObjects.Length > 0)
                {
                    foreach (ImageObject image in cardmodule.ImageObjects)
                    {
                        LoadImage(image, datagridview);
                    }
                }
            }
            catch
            {
                datagridview.Rows.Clear();
                MessageBox.Show("读取模板文件失败，无法新建卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //dgvCard.Refresh();
        }

        /// <summary>
        /// 方法说明：重置控件尺寸
        /// 作   者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="_breadth">纸张</param>
        private void ResizeControls(int width, int height, int _breadth)
        {
            int diff = 2;
            double percent = 0;
            switch (_breadth)
            {
                case 0:
                    diff = diff * 0;
                    percent = 0.0625;
                    break;
                case 1:
                    diff = diff * 1;
                    percent = 0.0833;
                    break;
                case 2:
                    diff = diff * 2;
                    percent = 0.125;
                    break;
                case 3:
                    diff = diff * 3;
                    percent = 0.1667;
                    break;
                case 4:
                    diff = diff * 4;
                    percent = 0.25;
                    break;
                case 5:
                    diff = diff * 5;
                    percent = 0.3333;
                    break;
                default:
                    diff = 0;
                    break;
            }


            int widPixel = MillimetersToPixelsWidth(width, true);
            int heightPixel = MillimetersToPixelsWidth(height, false);


            pnCard.Width = (int)Math.Round(widPixel * percent);
            pnCard.Height = (int)Math.Round(heightPixel * percent);
            dgvCard.Height = (int)Math.Round(heightPixel * percent);

        }

        /// <summary>
        /// 方法说明：根据毫米数得到像素
        /// 作    者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="length">长度(mm)</param>
        /// <param name="widthOrHeight">宽或者高</param>
        /// <returns></returns>
        private int MillimetersToPixelsWidth(int length, bool widthOrHeight)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(pnCard.Handle);
            int dpiX = (int)Math.Round(g.DpiX);
            int dpiY = (int)Math.Round(g.DpiY);

            int pixel = 0;
            if (widthOrHeight)
            {
                pixel = (int)Math.Round((length / 8.128) * dpiX);
            }
            else
            {
                pixel = (int)Math.Round((length / 8.128) * dpiY);
            }
            return pixel;

        }

        /// <summary>
        /// 方法说明：初始化单元格
        /// 作    者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="listRows">行数</param>
        /// <param name="listColumns">列数</param>
        private void InitDataGridView(List<int> listRows, List<int> listColumns, DataGridView datagridview)
        {
            int index = 0;

            //增加列
            DataGridViewTextBoxColumnEx column = new DataGridViewTextBoxColumnEx();

            List<int> listColWidth = new List<int>();
            List<int> listRowHeight = new List<int>();

            foreach (int i in listColumns)
            {
                column = new DataGridViewTextBoxColumnEx();
                column.Width = i;
                if (index == listColumns.Count - 1)
                {
                    column.Width = i - 3;
                }
                datagridview.Columns.Insert(index, column);
                listColWidth.Add(column.Width);
                index++;
            }

            index = 0;
            //增加行
            foreach (int i in listRows)
            {
                datagridview.Rows.Insert(index, 1);
                index++;
            }

            //行高设定
            index = 0;
            foreach (DataGridViewRow row in datagridview.Rows)
            {
                if (index < listRows.Count)
                {
                    row.Height = listRows[index];
                }
                if (index == datagridview.Rows.Count - 1)
                {
                    row.Height = row.Height - 3;
                }
                listRowHeight.Add(row.Height);
                index++;
            }
        }

        List<DataGridView> listDetails = new List<DataGridView>();

        /// <summary>
        /// 方法说明：增加明细框Grid
        /// 作者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="top">上边距</param>
        /// <param name="left">下边距</param>
        /// <param name="objColumns">明细列集合</param>
        /// <param name="cellEx">单元格</param>
        /// <param name="isBrowser">是否浏览卡片</param>
        private void AddDetailGridView(int top, int left, object objColumns, DataGridViewTextBoxCellEx cellEx, DataGridView datagridview, List<DetailGridViewTextBoxColumn> _dicColumns, bool isBrowser)
        {
            DataGridView dgv = new DataGridView();
            dgv.Name = string.Format("dgv{0}&{1}&{2}@{3}", cellEx.RowIndex, cellEx.ColumnIndex,
                Guid.NewGuid().ToString(), pageCount);
            dgv.BackgroundColor = Color.White;

            #region 明细框位置设定

            dgv.Top = top;
            dgv.Left = left;

            //dgv.Name = string.Format("dgv{0}-{1}", cellEx.RowIndex.ToString(), cellEx.ColumnIndex.ToString());                       

            List<DetailGridViewTextBoxColumn> listColumns = new List<DetailGridViewTextBoxColumn>();
            DetailCell[] detailCells = new DetailCell[] { };
            if (objColumns != null)
            {
                DetailGridViewTextBoxColumn textBoxColumn;                
                string type = objColumns.GetType().ToString();
                if (objColumns.GetType() == detailCells.GetType())
                {
                    detailCells = objColumns as DetailCell[];
                    foreach (DetailCell cell in detailCells)
                    {

                        textBoxColumn = new DetailGridViewTextBoxColumn();
                        textBoxColumn.Width = cell.ColumnWidth;
                        textBoxColumn.Content = cell.Content;
                        textBoxColumn.DetailSplitLine = cell.DetailLine;
                        textBoxColumn.HeaderText = cell.HeaderText;
                        textBoxColumn.Lans = cell.Lans;
                        textBoxColumn.Length = cell.Length;
                        textBoxColumn.ColumnName = cell.Name;
                        textBoxColumn.PerProcessRow = cell.PerProcessRow;
                        textBoxColumn.SplitLineInProcess = cell.ProcessDetailLine;
                        textBoxColumn.Rows = cell.Rows.ToString();
                        textBoxColumn.SerialNumber = cell.SerialNumber;
                        textBoxColumn.Source = cell.Source;
                        textBoxColumn.SpaceRows = cell.SpaceRows.ToString();
                        textBoxColumn.Tag = cell.Tag;
                        textBoxColumn.Type = string.IsNullOrEmpty(cell.Type) ? (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), "0") :
                            (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), cell.Type);
                        textBoxColumn.Visible = cell.AdvanceProperty != ((ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "3")).ToString();
                        textBoxColumn.AdvanceProperty = !string.IsNullOrEmpty(cell.AdvanceProperty) ? (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), cell.AdvanceProperty) :
                            (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "0");
                        textBoxColumn.SerialStep = cell.SerialStep;
                        textBoxColumn.ColumnValue = cell.ColumnValue;
                                                
                        listColumns.Add(textBoxColumn);

                        if (_dicColumns != null && !_dicColumns.Contains(textBoxColumn))
                        {
                            _dicColumns.Add(textBoxColumn);
                        }
                    }
                }
            }

            dgv.Width = datagridview.Columns[cellEx.ColumnIndex].Width;
            dgv.Height = datagridview.Rows[cellEx.RowIndex].Height;

            #endregion

            #region 明细框：处理合并的单元格

            int colspan = cellEx.ColumnSpan;
            int rowspan = cellEx.RowSpan;

            if (colspan > 1)
            {
                for (int i = 1; i < colspan; i++)
                {
                    dgv.Width += datagridview.Columns[cellEx.ColumnIndex + i].Width;
                }
            }
            if (rowspan > 1)
            {
                for (int i = 1; i < rowspan; i++)
                {
                    dgv.Height += datagridview.Rows[cellEx.RowIndex + i].Height;
                }
            }

            #endregion

            #region 明细框列表属性配置

            dgv.RowHeadersVisible = false;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ShowCellToolTips = false;
            //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgv.ReadOnly = true;
            dgv.ScrollBars = ScrollBars.None;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dgv.EditMode = DataGridViewEditMode.EditOnEnter;
            if (listColumns.Count > 0)
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                dgv.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                int totalWidth = 0;
                int totalRows = 1;
                string tag = string.Empty;
                foreach (DetailGridViewTextBoxColumn column in listColumns)
                {
                    dgv.Columns.Add(column);
                    totalWidth += column.Width;
                }

                if (totalWidth < dgv.Width)
                {
                    dgv.Columns[dgv.Columns.Count - 1].Width += dgv.Width - totalWidth;
                }

                if (listColumns != null && listColumns.Count > 0)
                {
                    DetailGridViewTextBoxColumn columns = (DetailGridViewTextBoxColumn)listColumns[0];
                    totalRows = int.Parse(columns.Rows);
                    for (int i = 0; i < totalRows; i++)
                    {
                        dgv.Rows.Insert(i, 1);
                    }
                }

                int rowHeight = dgv.Height - 23;
                //总宽度不足，补充到最后一列
                for (int i = 0; i < totalRows; i++)
                {
                    dgv.Rows[i].Height = rowHeight / totalRows;
                    if (i == totalRows - 1)
                    {
                        dgv.Rows[i].Height = rowHeight / totalRows + rowHeight % totalRows;
                    }
                }
            }

            #region 明细单元格样式设定

            if (detailCells.Length > 0 && dgv.Rows.Count > 0)
            {
                int colIndex = 0;
                foreach (DetailCell cell in detailCells)
                {
                    int rowIndex = 0;
                    if (cell.CustomerCellStyles != null && cell.CustomerCellStyles.Length > 0)
                    {
                        foreach (DataGridViewRow dr in dgv.Rows)
                        {
                            CustomerCellStyle cellStyle = cell.CustomerCellStyles[rowIndex];
                            if (dr.Cells[colIndex].GetType() == typeof(DataGridViewRichTextBoxCell))
                            {
                                dr.Cells[colIndex].Style.Tag = cellStyle.EmptyRow;

                                DataGridViewRichTextBoxCell richCell = (DataGridViewRichTextBoxCell)dr.Cells[colIndex];
                                if (!string.IsNullOrEmpty(cellStyle.FontFamily) &&
                                    !string.IsNullOrEmpty(cellStyle.FontSize) &&
                                    !string.IsNullOrEmpty(cellStyle.FontStyle))
                                {
                                    Font font = new Font(cellStyle.FontFamily, float.Parse(cellStyle.FontSize), (FontStyle)Enum.Parse(typeof(FontStyle), cellStyle.FontStyle));
                                    richCell.Style.Font = font;
                                }
                                richCell.Style.ForeColor = Color.FromArgb(int.Parse(cellStyle.ForeColor));
                                richCell.Style.Alignment = cellStyle.Alignment == null ? DataGridViewContentAlignment.NotSet : (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cellStyle.Alignment);

                                richCell.Style.BackColor = cellStyle.BackColor == null || int.Parse(cellStyle.BackColor) == 0 ? Color.Empty : Color.FromArgb(int.Parse(cellStyle.BackColor));
                                richCell.Style.ForeColor = cellStyle.ForeColor == null ? Color.Black : Color.FromArgb(int.Parse(cellStyle.ForeColor));


                                richCell.Style.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cellStyle.WrapMode.ToString());
                                List<int> listPadding = new List<int>();
                                if (cellStyle.Padding == null)
                                {
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                }
                                else
                                {
                                    string[] padding = cellStyle.Padding.Split(new char[] { ',' });
                                    foreach (string pad in padding)
                                    {
                                        listPadding.Add(int.Parse(pad));
                                    }
                                }
                                richCell.Style.Padding = new System.Windows.Forms.Padding(listPadding[0], listPadding[1], listPadding[2], listPadding[3]);
                            }
                            rowIndex++;
                        }

                    }
                    colIndex++;
                }
            }

            #endregion

            //int colRange = 0;
            //DataTable dtTemp = null;
            //foreach (DetailGridViewTextBoxColumn column in listColumns)
            //{
            //    string columnValue = ((DetailGridViewTextBoxColumn)column).ColumnValue;
            //    int spaceRows = string.IsNullOrEmpty(column.SpaceRows) ? 0 : int.Parse(column.SpaceRows);

            //    if (!string.IsNullOrEmpty(columnValue) && string.IsNullOrEmpty(column.Source))
            //    {
            //        string[] strValues = columnValue.Split(new char[] { ',' });
            //        int rowRange = 0;
                    
            //        foreach (DataGridViewRow row in dgv.Rows)
            //        {
            //            row.Cells[colRange].Value = strValues[rowRange];
            //            rowRange++;
            //        }
            //    }//来源不为空，则自动带出来源数据
            //    else if (!string.IsNullOrEmpty(column.Source))
            //    {
            //        string sourceName = column.Source.Substring(column.Source.IndexOf("\\") + 1);
            //        string typeName = column.Source.Substring(0, column.Source.IndexOf("\\"));

            //        //if (ModuleObject != null && typeName.ToLower() != "process")
            //        //{
            //        //    foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
            //        //    {
                            
            //        //        if (typeName.ToLower() == "product")
            //        //        {
            //        //            if (pi.Name.ToLower() == sourceName.ToLower())
            //        //            {
            //        //                columnValue = pi.GetValue(ModuleObject, new object[] { }).ToString();
            //        //                break;
            //        //            }
            //        //        }
            //        //    }
            //        //    //int rowRange = 0;
            //        //    //foreach (DataGridViewRow row in dgv.Rows)
            //        //    //{
            //        //    //    row.Cells[colRange].Value = columnValue;
            //        //    //    rowRange++;
            //        //    //}
            //        //    dgv.Rows[0].Cells[colRange].Value = columnValue;
            //        //}
            //        //else 
            //        if (dtOper != null)
            //        {
            //            if (typeName.ToLower() == "process")
            //            {
            //                int rowRange = 0;
            //                dtTemp = dtOper.Clone();
            //                foreach (DataRow row in dtOper.Rows)
            //                {
            //                    if (dtOper.Columns.Contains(sourceName) && rowRange <= dgv.Rows.Count - 1)
            //                    {
            //                        dgv.Rows[rowRange].Cells[colRange].Value = row[sourceName].ToString();
            //                    }
            //                    else
            //                    {
            //                        dtTemp.Rows.Add(row.ItemArray);
            //                    }
            //                    rowRange++;                                
            //                }                          
            //            }
            //        }
            //    }
            //    colRange++;
            //}

            dgv.CellClick += dgv_CellClick;
            dgv.CellDoubleClick += DataGridView_CellDoubleClick;
            dgv.CellMouseDown += DataGridView_CellMouseDown;
            dgv.EditingControlShowing += dgv_EditingControlShowing;
            dgv.KeyDown += dgv_KeyDown;
            dgv.CellBeginEdit += dgv_CellBeginEdit;

            dgvTemp = dgv;
            SetSerialNumberColumn(dgv, isBrowser);
            datagridview.Controls.Add(dgv);
            listDetails.Add(dgv);

            //if (dtTemp != null && dtTemp.Rows.Count > 0)
            //{
            //    dtOper = new DataTable();
            //    dtOper = dtTemp.Clone();
            //    foreach (DataRow row in dtTemp.Rows)
            //    {
            //        dtOper.Rows.Add(row.ItemArray);
            //    }  
            //    //当前行在为明细列表的最后一行
            //    int index = int.Parse(dgv.Name.Substring(dgv.Name.IndexOf("@") + 1)) + 1;
            //    //如果当前卡片为最后一页则新增卡片
            //    if (dgv.Name.Contains("@") &&
            //        dgv.Name.Substring(dgv.Name.IndexOf("@") + 1) == pageCount.ToString())
            //    {
            //        AddNewCard();
            //    }
            //    else
            //    {
            //        //否则跳到下一卡片的明细列表.
            //        SetNextFocus(index);
            //    }
            //}

            #endregion
                       
        }

        /// <summary>
        /// 右击选中单元格
        /// </summary>
        void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender == null)
                return;

            if (e.RowIndex < 0)
                return;

            MainFrm.mainFrm.SetToolButtonEnable(2);

            DataGridView datagridview = (DataGridView)sender;
            if (datagridview.SelectedCells.Count > 1)
            {
                foreach (DataGridViewCell cell in datagridview.SelectedCells)
                {
                    cell.Selected = true;
                }
            }
            else
                datagridview.ClearSelection();

            if (datagridview.Name.StartsWith("dgvCard"))
            {
                CurrentDataGrid = datagridview;
                tsmnuExportDetail.Enabled = false;
                tsmnuImportDetail.Enabled = false;
                tsmnuOleObject.Enabled = true;
                tsmnuCopy.Visible = false;
                tsmnuCut.Visible = false;
                tsPaster.Visible = false;
            }
            else
            {
                tsmnuExportDetail.Enabled = true;
                tsmnuImportDetail.Enabled = true;
                tsmnuOleObject.Enabled = false;
                tsmnuCopy.Visible = true;
                tsmnuCut.Visible = true;
                tsPaster.Visible = true;
            }

            CurrentClickCell = datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (e.RowIndex >= 0 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {

                    if (datagridview.HasChildren)
                    {
                        foreach (Control control in datagridview.Controls)
                        {
                            if (control is DataGridView)
                            {
                                ((DataGridView)control).ClearSelection();
                            }
                        }
                    }
                    if (datagridview.Parent != null && datagridview.Parent is DataGridView)
                    {
                        ((DataGridView)datagridview.Parent).ClearSelection();
                    }

                    datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;

                }
                catch
                { }
            }
        }

        /// <summary>
        /// 设置明细框内的单元格是否可以编辑
        /// </summary>
        void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑
            DetailGridViewTextBoxColumn column = (DetailGridViewTextBoxColumn)dgv.Columns[e.ColumnIndex];
            //如何明细列有数据源，则不可以编辑
            if (!string.IsNullOrEmpty(column.Source))// || column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
            {
                e.Cancel = true;
            }
            //间隔行数不可编辑
            if (!string.IsNullOrEmpty(column.SpaceRows))
            {
                int spaceRows = int.Parse(column.SpaceRows) + 1;
                string pageNumber = dgv.Name.Substring(dgv.Name.IndexOf("@") + 1);
                int remainder = dgv.Rows.Count % spaceRows;
                int startIndex = 0;
                if (pageNumber != "1" && remainder > 0)
                {
                    startIndex = spaceRows - (int.Parse(pageNumber) - 1);
                }

                bool isContainer = false;
                for (int i = startIndex; i < dgvTemp.Rows.Count; i += spaceRows)
                {
                    if (e.RowIndex == i)
                    {
                        isContainer = true;
                        break;
                    }                    
                }

                int styleTag = 0;
                //如果是空白行或序号行则也可以编辑
                foreach (DataGridViewCell cell in dgvTemp.Rows[e.RowIndex].Cells)
                {
                    if (cell.Style.Tag != null && (cell.Style.Tag.ToString() == "1" || cell.Style.Tag.ToString() == "2"))
                    {
                        isContainer = true;
                        styleTag = int.Parse(cell.Style.Tag.ToString());
                        break;
                    }
                    else
                        isContainer = false;
                }

                if (string.IsNullOrEmpty(column.Source))
                {
                    e.Cancel = !isContainer;
                }
                else if (styleTag == 1)
                {
                    e.Cancel = false;
                }
            }
        }

        /// <summary>
        /// 复制、粘贴、剪切
        /// </summary>
        void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender == null)
                return;

            if (e.Control && e.KeyCode == Keys.C)//复制
            {
                DataGridViewCopy((DataGridView)sender);
            }
            else if (e.Control && e.KeyCode == Keys.X)//剪切
            {
                DataGridViewCut((DataGridView)sender);
            }
            else if (e.Control && e.KeyCode == Keys.V)//粘贴
            {
                if(string.IsNullOrEmpty(Clipboard.GetText()))
                    Clipboard.Clear();
                DataGridViewPaste();
            }
        }

        /// <summary>
        /// 得到当前正在编辑的内置TextBox
        /// </summary>
        void dgv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvTemp.CurrentCell != null && e.Control is RichTextBox)
            {
                currTextBox = (RichTextBox)e.Control;
                currTextBox.ShortcutsEnabled = false;
                currTextBox.ContextMenuStrip = contextMenuTextBox;
                currTextBox.SelectionStart = currTextBox.Text.Length;
                if (CurrentClickCell != null)
                {
                    currTextBox.Multiline = CurrentClickCell.Style.WrapMode == DataGridViewTriState.True ? true : false;

                    //if (CurrentClickCell.GetType() == typeof(DataGridViewRichTextBoxCell))
                    //{
                    //    currTextBox.BackColor = CurrentClickCell.Style.BackColor;
                    //    currTextBox.Font = CurrentClickCell.Style.Font;
                    //    currTextBox.ForeColor = CurrentClickCell.Style.ForeColor;
                    //    if (CurrentClickCell.Style.Alignment.ToString().EndsWith("Right"))
                    //    {
                    //        currTextBox.SelectionAlignment = HorizontalAlignment.Right;
                    //    }
                    //    else if (CurrentClickCell.Style.Alignment.ToString().EndsWith("Left"))
                    //    {
                    //        currTextBox.SelectionAlignment = HorizontalAlignment.Left;
                    //    }
                    //    else if (CurrentClickCell.Style.Alignment.ToString().EndsWith("Center"))
                    //    {
                    //        currTextBox.SelectionAlignment = HorizontalAlignment.Center;
                    //    }                        
                    //}
                }
                SendKeys.Send("{Delete}");
            }
        }

        /// <summary>
        /// 单元格单击事件
        /// </summary>
        void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender != null)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.HasChildren)
                    {
                        foreach (Control ctrl in control.Controls)
                        {
                            if (ctrl is DataGridView &&
                                ctrl.Name.StartsWith("dgvCard"))
                            {
                                ((DataGridView)ctrl).ClearSelection();
                            }
                        }
                    }
                }
                currOlePicture = null;
                ClearPictrueBoxBorder(false);
                dgvTemp = (DataGridView)sender;

                DataGridViewCustomerCellStyle style = new DataGridViewCustomerCellStyle();// (DataGridViewCustomerCellStyle)dgvCard.CurrentCell.Style;
                style.CellType = 1;
                if (dgvTemp.CurrentCell.EditType == typeof(DataGridViewRichTextBoxEditingControl))
                {
                    currTextBox = (RichTextBox)dgvTemp.EditingControl;
                }

                if (dgvTemp.CurrentCell != null && dgvTemp.CurrentCell.GetType() == typeof(DataGridViewRichTextBoxCell))
                {
                    DataGridViewRichTextBoxCell richtextCell = (DataGridViewRichTextBoxCell)dgvTemp.CurrentCell;
                    style.Font = richtextCell.Style.Font;
                    style.ForeColor = richtextCell.Style.ForeColor;
                    style.BackColor = richtextCell.Style.BackColor;
                    style.Alignment = richtextCell.Style.Alignment;
                    style.Padding = richtextCell.Style.Padding;
                    style.WrapMode = richtextCell.Style.WrapMode;
                    style.CellType = 2;

                    if (richtextCell.Style.Alignment == DataGridViewContentAlignment.NotSet ||
                        richtextCell.Style.Alignment == DataGridViewContentAlignment.TopLeft)
                    {
                        style.RichAlignment = HorizontalAlignment.Left;
                    }
                    else if (richtextCell.Style.Alignment == DataGridViewContentAlignment.TopRight)
                    {
                        style.RichAlignment = HorizontalAlignment.Right;
                    }
                    else if (richtextCell.Style.Alignment == DataGridViewContentAlignment.TopCenter)
                    {
                        style.RichAlignment = HorizontalAlignment.Center;
                    }

                    richtextCell.Selected = true;
                }

                if (dgvCard.Tag != null)
                {
                    style.CardName = dgvCard.Tag.ToString();
                }

                if (DelegateForm.propertyForm == null)
                    return;               

                DelegateForm.propertyForm.SetPropertyGrid(style, false, true);
            }
        }

        /// <summary>
        /// 方法说明：装载图片文件
        /// 作    者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="image">图片对象</param>
        private void LoadImage(ImageObject image, DataGridView datagridview)
        {
            PictureBox pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Name = Guid.NewGuid().ToString();
            pic.Width = image.Width;
            pic.Height = image.Height;
            
            if (image.ImagePath.EndsWith("AVI"))
            {
                pic.Image = Properties.Resources.avi;
                pic.ImageLocation = null;
                pic.Name = string.Format("AVI{0}@{1}", pic.Name, image.ImagePath);
            }
            else
            {
                if (!File.Exists(image.ImagePath))
                {
                    if (image.ImagePath.Contains("_cut"))
                    {
                        pic.Image = Properties.Resources.CAD;
                        pic.Name = string.Format("pCad{0}@{1}", pic.Name, image.ImagePath);
                    }
                    else
                    {
                        pic.Image = Properties.Resources.img;
                        pic.Name = string.Format("pBmp{0}@{1}", pic.Name, image.ImagePath);
                    }
                    //pic.Image = Properties.Resources.none_img;
                    pic.ImageLocation = image.ImagePath;
                }
                else
                {
                    if (image.ImagePath.Contains("_cut"))
                    {
                        pic.Name = string.Format("pCad{0}@{1}", pic.Name, image.ImagePath);
                    }
                    else
                    {
                        pic.Name = string.Format("pBmp{0}@{1}", pic.Name, image.ImagePath);
                    }
                    pic.Image = Image.FromFile(image.ImagePath);
                    pic.ImageLocation = image.ImagePath;
                }
            }

            datagridview.Controls.Add(pic);
            pic.Location = new Point(image.LocationX, image.LocationY);

            pic.DoubleClick += pOle_DoubleClick;
            pic.Click += pOle_Click;
            dgvTemp.SendToBack();
        }

        /// <summary>
        /// 方法说明：回车时增加一个新的卡片(带上原卡片标题)
        /// 作    者：jason.tang
        /// 完成时间：2013-02-18
        /// </summary>
        private void AddNewCard()
        {
            Panel pn = new Panel();
            pn.BorderStyle = BorderStyle.FixedSingle;
            pn.Width = pnCard.Width;
            pn.Height = pnCard.Height;
            pn.AutoScroll = false;

            pn.Name = string.Format("pnCard{0}@{1}", Guid.NewGuid().ToString(), pageCount + 1);

            DataGridView dgvNew = new DataGridView();
            dgvNew.Name = string.Format("dgvCard{0}@{1}", Guid.NewGuid().ToString(), pageCount + 1);
            dgvNew.AllowUserToAddRows = false;
            dgvNew.AllowUserToDeleteRows = false;
            dgvNew.AllowUserToResizeColumns = false;
            dgvNew.AllowUserToResizeRows = false;
            dgvNew.ColumnHeadersVisible = false;
            dgvNew.RowHeadersVisible = false;
            dgvNew.BorderStyle = BorderStyle.Fixed3D;
            dgvNew.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dgvNew.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvNew.Dock = DockStyle.Fill;
            dgvNew.ScrollBars = ScrollBars.None;
            dgvNew.CellBeginEdit += dgvCard_CellBeginEdit;
            dgvNew.CellPainting += dgvCard_CellPainting;
            dgvNew.CellMouseDown += DataGridView_CellMouseDown;
            dgvNew.CellClick += dgvCard_CellClick;
            dgvNew.CellDoubleClick += DataGridView_CellDoubleClick;
            dgvNew.KeyDown += DataGridView_KeyDown;
            pn.Controls.Add(dgvNew);
            this.Controls.Add(pn);
            pageCount++;
            GetCardModuleById(ModuleId, dgvNew);
            Point p = SetPageNumber();

            //pn.Location = new Point(p.X, p.Y + pnCard.Height + 10);

        }

        /// <summary>
        /// 方法说明：设置下一个明细框获取焦点
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        /// <param name="index">下一个明细框索引</param>
        private void SetNextFocus(int index)
        {
            //遍历窗体控件
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in control.Controls)//遍历窗体的DataGridView
                    {
                        if (ctrl is DataGridView)
                        {
                            foreach (Control ctr in ctrl.Controls)//遍历DataGridView内的控件
                            {
                                if (ctr is DataGridView &&
                                    ctr.Name.EndsWith(index.ToString()))
                                {
                                    ctr.Focus();
                                    dgvTemp = (DataGridView)ctr;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 方法说明：设置页码、页数
        /// 作    者：jason.tang
        /// 完成时间：2013-02-18
        /// </summary>
        private Point SetPageNumber()
        {
            int range = 0;
            Point p = new Point();
            listPanels = new List<Panel>();
            //遍历窗体控件，得到卡片的总数
            foreach (Control control in this.Controls)
            {
                if (control is Panel && control.Name.StartsWith("pnCard"))
                {
                    range++;
                    listPanels.Add((Panel)control);
                    if (range < this.Controls.Count)
                    {
                        p = new Point(control.Location.X, control.Location.Y);
                    }
                }
            }

            foreach (Panel pn in listPanels)
            {
                if (pn.HasChildren)
                {
                    string parentName = pn.Name;
                    //遍历Panel内的控件，设置页码与页数
                    foreach (Control control in pn.Controls)
                    {
                        if (control is DataGridView)
                        {
                            foreach (DataGridViewRow row in ((DataGridView)control).Rows)
                            {
                                foreach (DataGridViewTextBoxCellEx cell in row.Cells)
                                {
                                    if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "3"))  //页码
                                    {
                                        cell.Value = parentName.Contains("@") ? parentName.Substring(parentName.IndexOf("@") + 1) : "1";
                                    }
                                    else if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "4"))  //页数
                                    {
                                        cell.Value = pageCount;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return p;
        }

        /// <summary>
        /// 方法说明：获取特殊字符
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        /// <param name="obj">特殊字符对象</param>
        public void GetSymbol(object obj)
        {
            if (obj != null && currTextBox != null)
            {
                RichTextBox rt = new RichTextBox();
                rt.Text = obj.ToString();

                Clipboard.SetData(DataFormats.Rtf, rt.Rtf);
                currTextBox.Paste();
                Clipboard.Clear();
            }
        }

        /// <summary>
        /// 方法说明：导出明细框数据到文本文件
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        private void ExportDataToTxt()
        {
            if (dgvTemp == null)
            {
                MessageBox.Show("该卡片文件不存在明细，无法导出", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文档（*.txt）|*.txt";                //默认文件扩展名
            if (dialog.ShowDialog() == DialogResult.OK)               //用户点击保存
            {
                StreamWriter sw = File.CreateText(dialog.FileName);
                string strLine = "";
                //写入数据
                for (int i = 0; i < dgvTemp.Rows.Count; i++)
                {
                    strLine = "";
                    for (int j = 0; j < dgvTemp.Columns.Count; j++)
                    {
                        if (dgvTemp.Rows[i].Cells[j].Value == null)
                        {
                            if (j < dgvTemp.Columns.Count - 1)
                            {
                                strLine += "\t";
                            }
                        }
                        else
                        {
                            if (j == dgvTemp.Columns.Count - 1)
                            {
                                strLine += dgvTemp.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                                strLine += dgvTemp.Rows[i].Cells[j].Value.ToString() + "\t";
                        }
                    }
                    sw.WriteLine(strLine);
                }
                sw.Flush();    //之前写入的是缓冲区，现在更新到文件中去
                MessageBox.Show("数据保存到" + dialog.FileName, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 方法说明：从文本文件导入明细框数据
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        private void ImportFromTxt()
        {
            if (dgvTemp == null)
            {
                MessageBox.Show("该卡片文件不存在明细，不允许导入", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "文本文档（*.txt）|*.txt";                //默认文件扩展名
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader sr = new StreamReader(dialog.FileName);
                string sLine = "";
                List<string> lineList = new List<string>();
                while (sLine != null)
                {
                    sLine = sr.ReadLine();
                    if (sLine != null && !sLine.Equals(""))
                        lineList.Add(sLine);
                }
                sr.Close();

                int index = 0;
                foreach (string str in lineList)
                {
                    int cIndex = 0;
                    string[] values = str.Split(new char[] { '\t' });
                    if (values.Length != dgvTemp.Columns.Count)
                    {
                        MessageBox.Show("文件格式不符", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                    foreach (string value in values)
                    {
                        dgvTemp.Rows[index].Cells[cIndex].Value = value;
                        cIndex++;
                    }
                    index++;
                }
            }
        }

        /// <summary>
        /// 方法说明：界面缩放
        /// 作    者：jason.tang
        /// 完成时间：2013-02-21
        /// </summary>
        /// <param name="_percent">百分比</param>
        /// <param name="delta">鼠标滚轮向上还是向下</param>
        /// <param name="panel">当前Panel</param>
        /// <param name="datagridview">当前Grid</param>
        private void SetPageZoom(double _percent, int delta, Panel panel, DataGridView datagridview)
        {
            int totalWidth = 0;
            int totalHeight = 0;
            int index = 0;

            try
            {
                int diff = _percent.ToString().EndsWith("5") ? 10 : 9;

                //调整行高
                foreach (DataGridViewRow row in datagridview.Rows)
                {
                    if (delta > 0)
                    {
                        row.Height = row.Height + 3;
                        totalHeight += 3;
                    }
                    else
                    {
                        row.Height = row.Height - 3;
                        totalHeight -= 3;
                    }
                    index++;
                }
                //调整列宽
                foreach (DataGridViewColumn column in datagridview.Columns)
                {
                    if (delta > 0)
                    {
                        column.Width = column.Width + diff;
                        totalWidth += diff;
                    }
                    else
                    {
                        column.Width = column.Width - diff;
                        totalWidth -= diff;
                    }
                }

                #region 明细框、图片缩放位置调整

                foreach (Control control in datagridview.Controls)
                {
                    if (control.GetType() == typeof(DataGridView) &&
                        control.Name.StartsWith("dgv"))  //明细框缩放
                    {
                        string row = control.Name.Substring(3, control.Name.IndexOf("&") - 3);
                        string col = control.Name.Substring(control.Name.IndexOf("&") + 1, control.Name.LastIndexOf("&") - control.Name.IndexOf("&") - 1);

                        int rowIndex = int.Parse(row);
                        int colIndex = int.Parse(col);


                        control.Height = datagridview.Rows[rowIndex].Height;
                        int rowspan = (datagridview.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).RowSpan;

                        if (rowspan > 1)
                        {
                            for (int i = 1; i < rowspan; i++)
                            {
                                control.Height += datagridview.Rows[rowIndex + i].Height;
                            }
                        }
                        int value = GetLeftValue(control.Height, (DataGridView)control, true);
                        ((DataGridView)control).Rows[((DataGridView)control).Rows.Count - 1].Height = value;

                        control.Width = datagridview.Columns[colIndex].Width;
                        int colspan = (datagridview.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).ColumnSpan;

                        if (colspan > 1)
                        {
                            for (int i = 1; i < colspan; i++)
                            {
                                control.Width += datagridview.Columns[colIndex + i].Width;
                            }
                        }

                        value = GetLeftValue(control.Width, (DataGridView)control, false);
                        ((DataGridView)control).Columns[((DataGridView)control).Columns.Count - 1].Width = value;

                        Rectangle rect = datagridview.GetCellDisplayRectangle(colIndex, rowIndex, false);
                        int top = rect.Y - 1;
                        int left = rect.X - 1;
                        control.Location = new Point(left, top);
                    }
                    else if (control.GetType() == typeof(PictureBox))   //图片缩放
                    {
                        int x = control.Location.X;
                        int y = control.Location.Y;

                        double height = Convert.ToDouble(control.Height);
                        double width = Convert.ToDouble(control.Width);
                        //图片宽高比例
                        double proportion = height / width;

                        if (delta > 0)
                        {
                            control.Height = control.Height + (int)Math.Round(2 * proportion);
                            control.Width = control.Width + 2;
                            control.Location = new Point(x + 3 * diff, y + 6);
                        }
                        else
                        {
                            control.Height = control.Height - (int)Math.Round(2 * proportion);
                            control.Width = control.Width - 2;
                            control.Location = new Point(x - 3 * diff, y - 6);
                        }
                    }
                }

                #endregion

                panel.Width += totalWidth;
                panel.Height += totalHeight;

                string tabText = this.TabText;
                if (this.TabText.Contains("@"))
                {
                    tabText = tabText.Substring(0, tabText.IndexOf("@") - 1);
                }
                this.TabText = string.Format(tabText + " @ {0}%", _percent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 方法说明：得到变动的值
        /// 作    者：jason.tang
        /// 完成时间：2013-02-21
        /// </summary>
        /// <returns>列宽或行高值</returns>
        /// <param name="total">总的宽度或高度</param>
        /// <param name="dgv">明细列Grid</param>
        /// <param name="rowOrColumn">行高或列宽</param>
        private int GetLeftValue(int total, DataGridView dgv, bool rowOrColumn)
        {
            int value = 0;

            if (rowOrColumn)
            {
                value = 0;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    value += row.Height;
                }
                if (value > 0)
                {
                    value = total - value + dgv.Rows[dgv.Rows.Count - 1].Height;
                }
            }
            else
            {
                value = 0;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    value += col.Width;
                }
                if (value > 0)
                {
                    value = total - value + dgv.Columns[dgv.Columns.Count - 1].Width;
                }
            }

            return value;
        }

        /// <summary>
        /// 方法说明：得到卡片内容
        /// 作    者：jason.tang
        /// 完成时间：2013-02-21
        /// </summary>
        private Card GetCard(Panel panel, DataGridView datagridview)
        {
            datagridview.ClearSelection();
            panel.Focus();

            ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
            Model.ProcessCardModule pcm = new Model.ProcessCardModule();

            //Model.CardModuleXML module = new Model.CardModuleXML();
            Card module = new Card();
            //模板的属性赋值
            module.CardRange = string.Format("A{0}", cardBreadth);
            //moude.CardDirection = "横向";
            //module.MarginLeft = _padleft.ToString();
            //module.MarginTop = _padtop.ToString();
            //module.MarginRight = _padright.ToString();
            //module.MarginBottom = _padbottom.ToString();
            ////moude.PrintScale = 0;
            //module.PrintAboveOffset = _offsettop;
            //module.PrintUnderOffset = _offsetleft;
            module.Width = cardWidth;
            module.Height = cardHeight;

            List<ImageObject> listImages = new List<ImageObject>();
            ImageObject image = new ImageObject();
            
            foreach (Control control in datagridview.Controls)
            {
                if (control.GetType() == typeof(PictureBox))
                {
                    //listPath.Add(((PictureBox)control).ImageLocation);
                    image = new ImageObject();
                    string name = ((PictureBox)control).Name;
                    if (name.Contains("@"))
                    {
                        image.ImagePath = name.Substring(name.IndexOf("@") + 1);
                    }
                    else
                        image.ImagePath = ((PictureBox)control).ImageLocation;
                    image.Width = ((PictureBox)control).Width;
                    image.Height = ((PictureBox)control).Height;
                    image.LocationX = ((PictureBox)control).Location.X;
                    image.LocationY = ((PictureBox)control).Location.Y;
                    listImages.Add(image);
                }
            }
            module.ImageObjects = listImages.ToArray();

            if (dgvCard.Tag != null)
            {
                module.Name = dgvCard.Tag.ToString();
            }
            //module.Name = this.TabText;
            //pcm.Name = CardModuleName;

            List<Model.Row> rowList = new List<Model.Row>();
            List<Model.Cell> cellList;
            List<Model.DetailCell> detailList;
            List<CustomerCellStyle> custCellStyleList;
            Model.Row rowProperty;
            Model.Cell cellProperty;
            Model.DetailCell detailProperty;

            int rowIndex = 0;
            int colIndex = 0;
            DataGridView dgvDetail = null;
            try
            {
                foreach (DataGridViewRow row in datagridview.Rows)
                {
                    rowProperty = new Model.Row();
                    rowProperty.Height = rowIndex == dgvCard.Rows.Count - 1 ? row.Height + 3 : row.Height;
                    cellList = new List<Model.Cell>();
                    colIndex = 0;
                    foreach (DataGridViewTextBoxCellEx cell in row.Cells)
                    {
                        cellProperty = new Model.Cell();
                        cellProperty.BottomBorderColor = cell.BottomBorderColor.ToArgb().ToString();
                        cellProperty.BottomBorderWidth = cell.BottomBorderWidth;
                        cellProperty.CellType = cell.CellEditType.ToString();
                        cellProperty.ColSpan = cell.ColumnSpan;
                        cellProperty.Content = cell.Value == null ? string.Empty : cell.Value.ToString();
                        cellProperty.SpanCell = string.Format("{0},{1}", cell.SpanCell.X, cell.SpanCell.Y);

                        #region 处理CellStyle

                        if (cell.CustStyle != null)
                        {
                            List<DataGridViewCustomerCellStyle> listCellStyle = (List<DataGridViewCustomerCellStyle>)cell.CustStyle;
                            if (listCellStyle.Count > 0)
                            {
                                cellProperty.Alignment = listCellStyle[0].Alignment.ToString();
                                cellProperty.BackGround = listCellStyle[0].BackColor.ToArgb().ToString();
                                cellProperty.FontFamily = listCellStyle[0].Font.FontFamily.ToString();
                                cellProperty.FontSize = listCellStyle[0].Font.Size.ToString();
                                cellProperty.FontStyle = listCellStyle[0].Font.Style.ToString();
                                cellProperty.ForeColor = listCellStyle[0].ForeColor.ToArgb().ToString();
                                cellProperty.WrapMode = listCellStyle[0].WrapMode == DataGridViewTriState.True ? true : false;
                                cellProperty.ZoomFontSize = listCellStyle[0].Font.Size.ToString();
                                cellProperty.Padding = string.Format("{0},{1},{2},{3}", listCellStyle[0].Padding.Left, listCellStyle[0].Padding.Top,
                                    listCellStyle[0].Padding.Right, listCellStyle[0].Padding.Bottom);
                            }
                        }

                        #endregion

                        cellProperty.Height = row.Height;
                        cellProperty.IsReadOnly = cell.ReadOnly;
                        cellProperty.LeftBorderColor = cell.LeftBorderColor.ToArgb().ToString();
                        cellProperty.LeftBorderWidth = cell.LeftBorderWidth;
                        //cellProperty.Name
                        cellProperty.PointX = cell.RowIndex;
                        cellProperty.PointY = cell.ColumnIndex;
                        cellProperty.RightBorderColor = cell.RightBorderColor.ToArgb().ToString();
                        cellProperty.RightBorderWidth = cell.RightBorderWidth;
                        cellProperty.RowSpan = cell.RowSpan;
                        cellProperty.TopBorderColor = cell.TopBorderColor.ToArgb().ToString();
                        cellProperty.TopBorderWidth = cell.TopBorderWidth;
                        cellProperty.Width = colIndex == dgvCard.Columns.Count - 1 ?
                            dgvCard.Columns[cell.ColumnIndex].Width + 3 : dgvCard.Columns[cell.ColumnIndex].Width;

                        cellProperty.LeftTopRightBottom = cell.LeftTopRightBottom;
                        cellProperty.LeftBottomRightTop = cell.LeftBottomRightTop;
                        cellProperty.ContentType = cell.CellContent.ToString();
                        cellProperty.CellTag = cell.CellTag;
                        cellProperty.CellSource = cell.CellSource;

                        detailList = new List<Model.DetailCell>();
                        if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                        {
                            foreach (DataGridView dgv in listDetails)
                            {
                                if (dgv.Name.StartsWith(string.Format("dgv{0}&{1}", cell.RowIndex, cell.ColumnIndex)) && 
                                    datagridview.Controls.Contains(dgv))
                                {
                                    dgvDetail = dgv;
                                    break;
                                }
                            }

                            if (cell.DetailProperty != null)
                            {
                                #region 明细框列属性

                                int range = 0;
                                List<DetailGridViewTextBoxColumn> details = (List<DetailGridViewTextBoxColumn>)cell.DetailProperty;
                                foreach (DetailGridViewTextBoxColumn column in details)
                                {                                    
                                    List<string> listValues = new List<string>();
                                    //foreach (Control control in datagridview.Controls)
                                    //{
                                    //    if (control is DataGridView)
                                    //    {
                                    //        string dataRowIndex = control.Name.Substring(3, control.Name.IndexOf("&") - 3);
                                    //        string dataColIndex = control.Name.Substring(control.Name.IndexOf("&") + 1, control.Name.LastIndexOf("&") - control.Name.IndexOf("&") - 1);

                                    //        if (cell.RowIndex.ToString() != dataRowIndex.ToString() ||
                                    //            cell.ColumnIndex.ToString() != dataColIndex.ToString())
                                    //        {
                                    //            continue;
                                    //        }
                                    //        foreach (DataGridViewRow dr in ((DataGridView)control).Rows)
                                    //        {
                                    //            string value = string.Empty;
                                    //            if (dr.Cells[range].Value != null)
                                    //            {
                                    //                //RichTextBox rt = new RichTextBox();
                                    //                //rt.Rtf = dr.Cells[range].Value.ToString();
                                    //                //value = rt.Text;
                                    //                value = dr.Cells[range].Value.ToString();
                                    //            }
                                    //            listValues.Add(value);
                                    //        }
                                    //    }
                                    //}
                                    custCellStyleList = new List<CustomerCellStyle>();

                                    if (dgvDetail != null)
                                    {
                                        foreach (DataGridViewRow dr in dgvDetail.Rows)
                                        {
                                            DataGridViewCell c = dr.Cells[column.Index];
                                            if (c.GetType() == typeof(DataGridViewRichTextBoxCell))
                                            {
                                                CustomerCellStyle style = new CustomerCellStyle();
                                                DataGridViewRichTextBoxCell richtextCell = (DataGridViewRichTextBoxCell)c;
                                                if (richtextCell.Style.Font != null)
                                                {
                                                    style.FontFamily = richtextCell.Style.Font.FontFamily.ToString();
                                                    style.FontSize = richtextCell.Style.Font.Size.ToString();
                                                    style.FontStyle = richtextCell.Style.Font.Style.ToString();
                                                }
                                                style.ForeColor = richtextCell.Style.ForeColor.ToArgb().ToString();
                                                style.BackColor = richtextCell.Style.BackColor.ToArgb().ToString();
                                                style.Alignment = richtextCell.Style.Alignment.ToString();
                                                style.Padding = string.Format("{0},{1},{2},{3}", richtextCell.Style.Padding.Left, richtextCell.Style.Padding.Top,
                                    richtextCell.Style.Padding.Right, richtextCell.Style.Padding.Bottom);
                                                style.WrapMode = richtextCell.Style.WrapMode == DataGridViewTriState.True ? true : false;
                                                style.CellType = 2;
                                                if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                                                {
                                                    if (c.Style.Tag == null)
                                                    {
                                                        style.EmptyRow = 0;
                                                    }
                                                    else
                                                    {
                                                        style.EmptyRow = int.Parse(c.Style.Tag.ToString());
                                                    }
                                                    //style.EmptyRow = c.Style.Tag == null || c.Style.Tag.ToString() == "0" ? 0 : 1;
                                                }

                                                if (richtextCell.Style.Alignment == DataGridViewContentAlignment.NotSet ||
                                                    richtextCell.Style.Alignment == DataGridViewContentAlignment.TopLeft)
                                                {
                                                    style.RichAlignment = HorizontalAlignment.Left.ToString();
                                                }
                                                else if (richtextCell.Style.Alignment == DataGridViewContentAlignment.TopRight)
                                                {
                                                    style.RichAlignment = HorizontalAlignment.Right.ToString();
                                                }
                                                else if (richtextCell.Style.Alignment == DataGridViewContentAlignment.TopCenter)
                                                {
                                                    style.RichAlignment = HorizontalAlignment.Center.ToString();
                                                }

                                                richtextCell.Selected = true;
                                                custCellStyleList.Add(style);
                                            }

                                            string value = string.Empty;
                                            if (dr.Cells[range].Value != null)
                                            {
                                                //RichTextBox rt = new RichTextBox();
                                                //rt.Rtf = dr.Cells[range].Value.ToString();
                                                //value = rt.Text;
                                                value = dr.Cells[range].Value.ToString();
                                            }
                                            listValues.Add(value);
                                        }
                                    }

                                    detailProperty = new Model.DetailCell();
                                    detailProperty.ColumnWidth = column.Width;
                                    detailProperty.Content = column.Content;
                                    detailProperty.DetailLine = column.DetailSplitLine;
                                    detailProperty.HeaderText = column.HeaderText;
                                    detailProperty.Lans = column.Lans;
                                    detailProperty.Length = column.Length;
                                    detailProperty.Name = column.ColumnName;
                                    detailProperty.PerProcessRow = column.PerProcessRow;
                                    detailProperty.ProcessDetailLine = column.SplitLineInProcess;
                                    detailProperty.Rows = int.Parse(column.Rows);
                                    detailProperty.SerialNumber = column.SerialNumber;
                                    detailProperty.Source = column.Source;
                                    detailProperty.SpaceRows = string.IsNullOrEmpty(column.SpaceRows) ? 0 : int.Parse(column.SpaceRows);
                                    detailProperty.Tag = column.Tag == null ? string.Empty : column.Tag.ToString();
                                    detailProperty.Type = column.Type.ToString();
                                    detailProperty.AdvanceProperty = column.AdvanceProperty.ToString();
                                    detailProperty.SerialStep = column.SerialStep;
                                    detailProperty.ColumnValue = string.Join(",", listValues.ToArray()); 
                                    
                                    
                                    
                                    detailProperty.CustomerCellStyles = custCellStyleList.ToArray();
                                    detailList.Add(detailProperty);

                                    range++;
                                }

                                #endregion                                
                            }
                        }
                        cellProperty.DetailCells = detailList.ToArray();
                        cellList.Add(cellProperty);
                        colIndex++;
                    }
                    rowProperty.Cells = cellList.ToArray();
                    rowList.Add(rowProperty);
                    rowIndex++;
                }

                module.Rows = rowList.ToArray();
            }
            catch
            {
                MessageBox.Show("卡片文件保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return module;
        }

        Font preFont = null;
        Color preColor = Color.Empty;
        /// <summary>
        /// 方法说明：打开卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-02-21
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="cardid">卡片ID</param>
        /// <param name="isNew">是否新建卡片</param>
        /// <param name="isBrowser">是否浏览已入库的卡片</param>
        public bool OpenCard(string path, string cardid, bool isNew, bool isBrowser)
        {
            ProcessCardBLL pcBll = new ProcessCardBLL();
            ProcessCard processCard = new ProcessCard();
            CardsXML cards = new CardsXML();
            DataGridView datagridview = new DataGridView();
            if (!string.IsNullOrEmpty(path))
            {
                processCard = Kingdee.CAPP.Common.Serialize.SerializeHelper.BinaryDeSerialize<ProcessCard>(path);
                cards = processCard.Card;
                ModuleId = processCard.CardModuleId.ToString();
                if (!isNew)
                    this.Tag = processCard.ID;
                cardPath = path;
            }
            else if (!string.IsNullOrEmpty(cardid))
            {
                Guid id = new Guid(cardid);
                processCard = pcBll.GetProcessCard(id);
                cards = processCard.Card;
                if (!isNew)
                    this.Tag = id;
            }

            if (!string.IsNullOrEmpty(PBomID) && dtOper == null)
            {
                dtOper = MaterialModuleBLL.GetOperByPBomId(PBomID);
            }

            if (cards == null)
                return false;

            if (!isNew)
            {
                dgvCard.Tag = processCard.Name;
            }
            else
            {                
                string materialName = string.Empty;
                string moduleName = processCard.Name;
                if (ModuleObject != null)
                {
                    materialName = ((MaterialModule)ModuleObject).name;
                }
                if (processCard.Name.Contains("-"))
                {
                    moduleName = processCard.Name.Substring(processCard.Name.IndexOf("-") + 1);
                }
                dgvCard.Tag = string.IsNullOrEmpty(materialName) ? moduleName : string.Format("{0}-{1}", materialName, moduleName);
            }

            int index = 0;
            Panel pn = new Panel();
            foreach (Card card in cards.Cards)
            {
                if (index == 0)
                {
                    datagridview = dgvCard;
                }
                else
                {
                    pn = new Panel();
                    pn.BorderStyle = BorderStyle.FixedSingle;
                    pn.Width = pnCard.Width;
                    pn.Height = pnCard.Height;
                    pn.AutoScroll = false;

                    pn.Name = string.Format("pnCard{0}@{1}", Guid.NewGuid().ToString(), pageCount + 1);

                    DataGridView dgvNew = new DataGridView();
                    dgvNew.Name = string.Format("dgvCard{0}@{1}", Guid.NewGuid().ToString(), pageCount + 1);
                    dgvNew.AllowUserToAddRows = false;
                    dgvNew.AllowUserToDeleteRows = false;
                    dgvNew.AllowUserToResizeColumns = false;
                    dgvNew.AllowUserToResizeRows = false;
                    dgvNew.ColumnHeadersVisible = false;
                    dgvNew.RowHeadersVisible = false;
                    dgvNew.BorderStyle = BorderStyle.Fixed3D;
                    dgvNew.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
                    dgvNew.DefaultCellStyle.SelectionForeColor = Color.Black;
                    dgvNew.Dock = DockStyle.Fill;
                    dgvNew.ScrollBars = ScrollBars.None;
                    dgvNew.CellBeginEdit += dgvCard_CellBeginEdit;
                    pn.Controls.Add(dgvNew);
                    this.Controls.Add(pn);
                    pageCount++;

                    datagridview = dgvNew;
                }
                index++;
                int Width = Convert.ToInt32(card.Width);
                int Height = Convert.ToInt32(card.Height);
                int breadth = int.Parse(card.CardRange.Replace("A", ""));

                cardWidth = Width;
                cardHeight = Height;
                cardBreadth = breadth;

                if (datagridview.Name == "dgvCard")
                {
                    ResizeControls(Width, Height, breadth);
                }

                List<DataGridViewRow> listRow = new List<DataGridViewRow>();

                int rows = card.Rows.Length;
                int columns = card.Rows[0].Cells.Length;

                List<int> listWidth = new List<int>();
                List<int> listHeight = new List<int>();

                foreach (Row row in card.Rows)
                {
                    listHeight.Add(Convert.ToInt32(row.Height));
                }

                foreach (Cell cell in card.Rows[0].Cells)
                {
                    listWidth.Add(Convert.ToInt32(cell.Width));
                }

                InitDataGridView(listHeight, listWidth, datagridview);

                DataGridViewTextBoxCellEx cellEx;
                List<DataGridViewCustomerCellStyle> listCellStyle;
                DataGridViewCustomerCellStyle cellStyle;
                List<int> listPadding;
                try
                {
                    //工艺路线
                    DataTable dtRouting = null;
                    if (!string.IsNullOrEmpty(PBomID))
                    {
                        dtRouting = MaterialModuleBLL.GetRoutingByPBomId(PBomID);
                    }

                    foreach (Row row in card.Rows)
                    {
                        foreach (Cell cell in row.Cells)
                        {
                            cellEx = ((DataGridViewTextBoxCellEx)datagridview.Rows[cell.PointX].Cells[cell.PointY]);
                            cellEx.CellTag = processCard.Name;
                            cellEx.Style.Alignment = cell.Alignment == null ? DataGridViewContentAlignment.NotSet : (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cell.Alignment);
                            cellEx.Style.BackColor = cell.BackGround == null || int.Parse(cell.BackGround) == 0 ? Color.Empty : Color.FromArgb(int.Parse(cell.BackGround));
                            cellEx.BottomBorderColor = cell.BottomBorderColor == null || int.Parse(cell.BottomBorderColor) == 0 ? Color.Empty : Color.FromArgb(int.Parse(cell.BottomBorderColor));
                            cellEx.BottomBorderWidth = Convert.ToInt32(cell.BottomBorderWidth);
                            cellEx.CellEditType = (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), cell.CellType);
                            cellEx.ColumnSpan = cell.ColSpan;
                            if (!string.IsNullOrEmpty(cell.CellSource) && !isBrowser)
                            {
                                string sourceName = cell.CellSource.Substring(cell.CellSource.IndexOf("\\") + 1);
                                string typeName = cell.CellSource.Substring(0, cell.CellSource.IndexOf("\\"));

                                if (ModuleObject != null && typeName.ToLower() != "routing")
                                {
                                    foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
                                    {
                                        if (pi.Name.ToLower() == sourceName.ToLower())
                                        {
                                            cellEx.Value = pi.GetValue(ModuleObject, new object[] { });
                                            break;
                                        }
                                    }
                                }
                                else if (dtRouting != null && typeName.ToLower() == "routing")
                                {
                                    foreach (DataRow dr in dtRouting.Rows)
                                    {
                                        foreach (DataColumn col in dtRouting.Columns)
                                        {
                                            if (col.ColumnName.ToLower() == sourceName.ToLower())
                                            {
                                                cellEx.Value = dr[col].ToString();
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                cellEx.Value = cell.Content;
                            }
                            string[] spanCell = cell.SpanCell.Split(new char[] { ',' });
                            cellEx.SpanCell = new Point(int.Parse(spanCell[0]), int.Parse(spanCell[1]));

                            #region 处理CellStyle

                            listCellStyle = new List<DataGridViewCustomerCellStyle>();
                            listPadding = new List<int>();
                            if (cellEx.CustStyle == null)
                            {
                                cellStyle = new DataGridViewCustomerCellStyle();
                                cellStyle.Alignment = cell.Alignment == null ? DataGridViewContentAlignment.NotSet : (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cell.Alignment);
                                cellStyle.BackColor = cell.BackGround == null || int.Parse(cell.BackGround) == 0 ? Color.Empty : Color.FromArgb(int.Parse(cell.BackGround));
                                if (cell.FontFamily == null || cell.ZoomFontSize == null || cell.FontStyle == null)
                                    cellStyle.Font = preFont;
                                else
                                    cellStyle.Font = new Font(cell.FontFamily, float.Parse(cell.ZoomFontSize), (FontStyle)Enum.Parse(typeof(FontStyle), cell.FontStyle));

                                cellStyle.ForeColor = cell.ForeColor == null ? preColor : Color.FromArgb(int.Parse(cell.ForeColor));

                                preFont = cellStyle.Font;
                                preColor = cellStyle.ForeColor;

                                cellStyle.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());
                                if (cell.Padding == null)
                                {
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                    listPadding.Add(0);
                                }
                                else
                                {
                                    string[] padding = cell.Padding.Split(new char[] { ',' });
                                    foreach (string pad in padding)
                                    {
                                        listPadding.Add(int.Parse(pad));
                                    }
                                }
                                cellStyle.Padding = new System.Windows.Forms.Padding(listPadding[0], listPadding[1], listPadding[2], listPadding[3]);
                                listCellStyle.Add(cellStyle);
                                cellEx.CustStyle = listCellStyle;
                            }

                            #endregion

                            //cell.ContentType;
                            //cell.DataSrc;
                            //cell.DetailCells;

                            if (cell.FontFamily == null || cell.FontSize == null || cell.FontStyle == null)
                                cellEx.Style.Font = preFont;
                            else
                                cellEx.Style.Font = new Font(cell.FontFamily, float.Parse(cell.FontSize), (FontStyle)Enum.Parse(typeof(FontStyle), cell.FontStyle));

                            cellEx.Style.ForeColor = cell.ForeColor == null ? preColor : Color.FromArgb(int.Parse(cell.ForeColor));

                            preFont = cellEx.Style.Font;
                            preColor = cellEx.Style.ForeColor;

                            cellEx.LeftBorderColor = Color.FromArgb(int.Parse(cell.LeftBorderColor));
                            cellEx.LeftBorderWidth = Convert.ToInt32(cell.LeftBorderWidth);
                            //cell.Name;
                            cellEx.RightBorderColor = Color.FromArgb(int.Parse(cell.RightBorderColor));
                            cellEx.RightBorderWidth = Convert.ToInt32(cell.RightBorderWidth);
                            cellEx.RowSpan = cell.RowSpan;
                            cellEx.TopBorderColor = Color.FromArgb(int.Parse(cell.TopBorderColor));
                            cellEx.TopBorderWidth = Convert.ToInt32(cell.TopBorderWidth);

                            cellEx.LeftTopRightBottom = cell.LeftTopRightBottom;
                            cellEx.LeftBottomRightTop = cell.LeftBottomRightTop;
                            cellEx.CellContent = string.IsNullOrEmpty(cell.ContentType) ? (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0") :
                                (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), cell.ContentType);

                            cellEx.Style.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());
                            cellEx.CellTag = cell.CellTag;
                            cellEx.CellSource = cell.CellSource;

                            string parentName = datagridview.Parent.Name;
                            //明细框单元格
                            if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                            {
                                Rectangle rect = datagridview.GetCellDisplayRectangle(cellEx.ColumnIndex, cellEx.RowIndex, false);
                                int top = rect.Y - 1;
                                int left = rect.X - 1;

                                List<DetailGridViewTextBoxColumn> dicColumns = new List<DetailGridViewTextBoxColumn>();
                                object objDetailProperty = cell.DetailCells;
                                //明细框
                                AddDetailGridView(top, left, objDetailProperty, cellEx, datagridview, dicColumns, isBrowser);
                                cellEx.DetailProperty = dicColumns;
                            }
                            else if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "3"))  //页码
                            {
                                cellEx.Value = 1;
                            }
                            else if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "4"))  //页数
                            {
                                cellEx.Value = 1;
                            }
                        }
                    }

                    Point p = SetPageNumber();
                    pn.Location = new Point(p.X, p.Y + pnCard.Height + 10);

                    //读取图片
                    if (card.ImageObjects != null && card.ImageObjects.Length > 0)
                    {
                        foreach (ImageObject image in card.ImageObjects)
                        {
                            LoadImage(image, datagridview);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("读取模板文件失败，无法新建卡片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 方法说明：获取注册表中当前图片列表
        /// 作    者：jason.tang
        /// 完成时间：2013-02-26
        /// </summary>
        /// <returns>图片路径列表</returns>
        private List<string> GetRegistData()
        {
            List<string> listDatas = new List<string>();
            RegistryKey hkml = Registry.CurrentUser;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey micro = software.OpenSubKey("MICROSOFT", true);
            RegistryKey windows = micro.OpenSubKey("WINDOWS", true);
            RegistryKey version = windows.OpenSubKey("CurrentVersion", true);
            RegistryKey applets = version.OpenSubKey("Applets", true);
            RegistryKey paint = applets.OpenSubKey("Paint", true);
            RegistryKey filelist = paint.OpenSubKey("Recent File List", true);
            string[] registData = filelist.GetValueNames();
            foreach (string str in registData)
            {
                listDatas.Add(filelist.GetValue(str).ToString());
            }
            return listDatas;
        }

        /// <summary>
        /// 方法说明：卡片内容粘贴
        /// 作    者：jason.tang
        /// 完成时间：2013-02-27
        /// </summary>
        private void DataGridViewPaste()
        {
            try
            {
                //取剪贴板里的内容，如果内容为空，则退出
                string pastTest = Clipboard.GetText();
                if (!string.IsNullOrEmpty(pastTest))
                {
                    //excel中是以 空格 和换行来 当做字段和行，所以用\n \r来分隔
                    string[] lines = pastTest.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    int rowIndex = dgvTemp.CurrentCell.RowIndex;
                    foreach (string line in lines)
                    {
                        string[] strs = line.Split(new char[] { '\t' });
                        int colIndex = dgvTemp.CurrentCell.ColumnIndex;

                        //如果粘贴的行超过当前的行数，则新建一张卡片
                        if (rowIndex >= dgvTemp.Rows.Count)
                        {
                            AddNewCard();

                            if (listPanels != null && listPanels.Count > 0)
                            {
                                int rang = 0;
                                foreach (Panel pn in listPanels)
                                {
                                    if (pn.Name != "pnCard" && rang - 1 >= 0)
                                    {
                                        pn.Location = new Point(listPanels[rang - 1].Location.X, listPanels[rang - 1].Location.Y + pnCard.Height + 10);
                                    }
                                    rang++;
                                }
                            }

                            rowIndex = 0;
                            foreach (string str in strs)
                            {
                                if (colIndex >= dgvTemp.Columns.Count)
                                {
                                    break;
                                }
                                if (dgvTemp.Columns[colIndex] is DetailGridViewTextBoxColumn &&
                                    !string.IsNullOrEmpty(((DetailGridViewTextBoxColumn)dgvTemp.Columns[colIndex]).Source))
                                {
                                    colIndex++;
                                    continue;
                                }
                                dgvTemp.Rows[rowIndex].Cells[colIndex].Value = str;
                                colIndex++;
                            }
                        }
                        else
                        {
                            foreach (string str in strs)
                            {
                                if (colIndex >= dgvTemp.Columns.Count)
                                {
                                    break;
                                }
                                if (dgvTemp.Columns[colIndex] is DetailGridViewTextBoxColumn &&
                                    !string.IsNullOrEmpty(((DetailGridViewTextBoxColumn)dgvTemp.Columns[colIndex]).Source))
                                {
                                    colIndex++;
                                    continue;
                                }
                                dgvTemp.Rows[rowIndex].Cells[colIndex].Value = str;
                                colIndex++;
                            }
                        }
                        rowIndex++;
                    }
                }
                else if (dicCopyRichs.Count > 0)
                {
                    if (dicCopyRichs.Count == 1)
                    {
                        if (dgvTemp.Columns[dgvTemp.CurrentCell.ColumnIndex] is DetailGridViewTextBoxColumn &&
                                    !string.IsNullOrEmpty(((DetailGridViewTextBoxColumn)dgvTemp.Columns[dgvTemp.CurrentCell.ColumnIndex]).Source))
                        {
                            return;
                        }
                        Point p = new Point(minValue.Y, minValue.X);
                        dgvTemp.CurrentCell.Value = dicCopyRichs[p].Rtf;
                    }
                    else
                    {
                        int currRowIndex = dgvTemp.CurrentCell.RowIndex;
                        int currColIndex = dgvTemp.CurrentCell.ColumnIndex;

                        int diffRowIndex = currRowIndex - minValue.Y;
                        int diffColIndex = currColIndex - minValue.X;

                        foreach (Point p in dicCopyRichs.Keys)
                        {
                            if (dgvTemp.Columns[p.Y + diffColIndex] is DetailGridViewTextBoxColumn &&
                                    !string.IsNullOrEmpty(((DetailGridViewTextBoxColumn)dgvTemp.Columns[p.Y + diffColIndex]).Source))
                            {
                                continue;
                            }
                            dgvTemp.Rows[p.X + diffRowIndex].Cells[p.Y + diffColIndex].Value = dicCopyRichs[p].Rtf;
                        }
                    }
                }
            }
            catch
            { }
        }

        /// <summary>
        /// 方法说明：DataGridView拷贝
        /// 作    者：jason.tang
        /// 完成时间：2013-02-27
        /// </summary>
        /// <param name="datagridview">DataGridView</param>
        private void DataGridViewCopy(DataGridView datagridview)
        {
            dicCopyRichs = new Dictionary<Point, RichTextBox>();
            int init = 0;
            minValue = new Point();
            foreach (DataGridViewCell cell in datagridview.SelectedCells)
            {
                RichTextBox richTemp = new RichTextBox();
                try
                {
                    richTemp.Rtf = cell.Value.ToString();
                }
                catch
                {
                    richTemp.Text = cell.Value != null ? cell.Value.ToString() : string.Empty;
                }
                dicCopyRichs.Add(new Point(cell.RowIndex, cell.ColumnIndex), richTemp);

                if (init <= 0)
                {
                    minValue = new Point(cell.ColumnIndex, cell.RowIndex);
                    init++;
                }
                else
                {
                    if (cell.RowIndex < minValue.Y || cell.ColumnIndex < minValue.X)
                    {
                        minValue = new Point(cell.ColumnIndex, cell.RowIndex);
                    }
                }
            }

            Clipboard.Clear();
        }

        /// <summary>
        /// 方法说明：DataGridView剪切
        /// 作    者：jason.tang
        /// 完成时间：2013-02-27
        /// </summary>
        /// <param name="datagridview">DataGridView</param>
        private void DataGridViewCut(DataGridView datagridview)
        {
            DataGridViewCopy(datagridview);
            foreach (DataGridViewCell cell in datagridview.SelectedCells)
            {
                cell.Value = null;
            }
        }

        /// <summary>
        /// 方法说明：单元格内容粘贴
        /// 作    者：jason.tang
        /// 完成时间：2013-02-28
        /// </summary>
        private void TextPaste()
        {
            //取剪贴板里的内容，如果内容为空，则退出
            object pastRtf = Clipboard.GetData(DataFormats.Rtf);
            if (pastRtf == null) return;
            currTextBox.Paste();
        }

        /// <summary>
        /// 方法说明：单元格内容拷贝
        /// 作    者：jason.tang
        /// 完成时间：2013-02-28
        /// </summary>
        private void TextCopy()
        {
            if (currTextBox != null && !string.IsNullOrEmpty(currTextBox.SelectedRtf))
            {
                Clipboard.SetData(DataFormats.Rtf, currTextBox.SelectedRtf);
            }
        }

        /// <summary>
        /// 方法说明：单元格内容剪切
        /// 作    者：jason.tang
        /// 完成时间：2013-02-28
        /// </summary>
        private void TextCut()
        {
            TextCopy();
            if (currTextBox != null && !string.IsNullOrEmpty(currTextBox.SelectedRtf))
            {
                currTextBox.SelectedRtf = string.Empty;
            }
        }

        /// <summary>
        /// 方法说明：卡片保存到本地
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        public void SaveCard()
        {
            SaveFileDialog of = new SaveFileDialog();
            of.Filter = "CARD files (*.card)|*.card";
            if (of.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            List<Card> listCards = new List<Card>();
            string cardName = string.Empty;
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            Card card = new Card();
                            card = GetCard((Panel)control, (DataGridView)ctrl);
                            if (card == null)
                            {
                                return;
                            }
                            listCards.Add(card);
                            cardName = card.Name;
                        }
                    }
                }
            }
            if (listCards.Count == 0)
            {
                return;
            }

            try
            {
                ProcessCard card = new ProcessCard();
                CardsXML cards = new CardsXML();

                string path = of.FileName;
                cards.Cards = listCards.ToArray();
                card.Card = cards;
                card.Name = cardName;
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    card.CardModuleId = new Guid(ModuleId);
                }
                if (this.Tag != null)
                {
                    card.ID = new Guid(this.Tag.ToString());
                }
                Kingdee.CAPP.Common.Serialize.SerializeHelper.BinarySerialize<ProcessCard>(card, path);
                MessageBox.Show("卡片文件保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("卡片文件保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 方法说明：卡片入库
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <returns>卡片实体</returns>
        public ProcessCard SaveCardIntoDatabase()
        {
            if (dgvCard.Tag == null)
            {
                MessageBox.Show("请设置卡片名称属性值", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            List<Card> listCards = new List<Card>();
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            Card card = new Card();
                            card = GetCard((Panel)control, (DataGridView)ctrl);
                            if (card == null)
                            {
                                return null;
                            }
                            listCards.Add(card);
                        }
                    }
                }
            }
            if (listCards.Count == 0)
            {
                return null;
            }

            try
            {
                ProcessCardBLL pcBll = new ProcessCardBLL();
                ProcessCard pc = new ProcessCard();
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    pc.CardModuleId = new Guid(ModuleId);
                }

                //pc.Name = listCards[0].Name;
                string cardid = string.Empty;
                if (this.Tag != null)
                {
                    cardid = this.Tag.ToString();
                }
                CardsXML cardXML = new CardsXML();
                cardXML.Cards = listCards.ToArray();
                pc.Card = cardXML;

                pc.Name = dgvCard.Tag.ToString();

                Guid guid = new Guid();
                bool isNewOrModify = true;
                if (!string.IsNullOrEmpty(cardid))
                {
                    try
                    {
                        Guid gid = new Guid(cardid);
                        Model.ProcessCard processCard = pcBll.GetProcessCard(gid);
                        isNewOrModify = false;
                    }
                    catch
                    {
                        isNewOrModify = true;
                    }
                }
                //已经存在的卡片则更新
                if (!isNewOrModify)
                {
                    pc.ID = new Guid(cardid);
                    bool result = pcBll.UpdateProcessCard(pc);
                    string notice = result ? "成功" : "失败";
                    MessageBox.Show(string.Format("卡片入库{0}", notice), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                }
                //卡片新建
                guid = pcBll.InsertProcessCard(pc);
                if (guid != null)
                {
                    /// 初始化业务Id
                    pc.ID = guid;

                    //同时修改本地文件
                    if (!string.IsNullOrEmpty(cardPath))
                    {
                        Kingdee.CAPP.Common.Serialize.SerializeHelper.BinarySerialize<ProcessCard>(pc, cardPath);
                    }

                    MessageBox.Show("卡片入库成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return pc;
                }
                else
                {
                    MessageBox.Show("卡片入库失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return null;
            }
            catch
            {
                MessageBox.Show("卡片文件入库失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }
        }

        public ProcessCard SaveCardIntoDatabaseWithName(string name)
        {
            dgvCard.Tag = name;

            List<Card> listCards = new List<Card>();
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            Card card = new Card();
                            card = GetCard((Panel)control, (DataGridView)ctrl);
                            if (card == null)
                            {
                                return null;
                            }
                            listCards.Add(card);
                        }
                    }
                }
            }
            if (listCards.Count == 0)
            {
                return null;
            }

            try
            {
                ProcessCardBLL pcBll = new ProcessCardBLL();
                ProcessCard pc = new ProcessCard();
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    pc.CardModuleId = new Guid(ModuleId);
                }

                //pc.Name = listCards[0].Name;
                string cardid = string.Empty;
                if (this.Tag != null)
                {
                    cardid = this.Tag.ToString();
                }
                CardsXML cardXML = new CardsXML();
                cardXML.Cards = listCards.ToArray();
                pc.Card = cardXML;

                pc.Name = dgvCard.Tag.ToString();

                Guid guid = new Guid();
                bool isNewOrModify = true;
                if (!string.IsNullOrEmpty(cardid))
                {
                    try
                    {
                        Guid gid = new Guid(cardid);
                        Model.ProcessCard processCard = pcBll.GetProcessCard(gid);
                        isNewOrModify = false;
                    }
                    catch
                    {
                        isNewOrModify = true;
                    }
                }
                //已经存在的卡片则更新
                if (!isNewOrModify)
                {
                    pc.ID = new Guid(cardid);
                    bool result = pcBll.UpdateProcessCard(pc);
                    if (result)
                        return pc;
                    return null;
                }
                //卡片新建
                guid = pcBll.InsertProcessCard(pc);
                if (guid != null)
                {
                    /// 初始化业务Id
                    pc.ID = guid;

                    //同时修改本地文件
                    if (!string.IsNullOrEmpty(cardPath))
                    {
                        Kingdee.CAPP.Common.Serialize.SerializeHelper.BinarySerialize<ProcessCard>(pc, cardPath);
                    }

                    return pc;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        //Dictionary<int, int> dicRealRowTag = new Dictionary<int, int>();
        Dictionary<string, int> dicSerialRows = new Dictionary<string,int>();

        /// <summary>
        /// 方法说明：设置自动序号列
        /// 作    者：jason.tang
        /// 完成时间：2013-03-12
        /// </summary>
        /// <param name="dgv">当前DataGrid</param>
        /// <param name="isBrowser">是否浏览卡片</param>
        private void SetSerialNumberColumn(DataGridView dgv, bool isBrowser)
        {
            int range = 0;
            
            DataTable dtTemp = null;            

            foreach (DetailGridViewTextBoxColumn column in dgv.Columns)
            {                
                //间隔行
                if (!string.IsNullOrEmpty(column.SpaceRows))
                {
                    #region 空白间隔行

                    //int spaceRows = int.Parse(column.SpaceRows) + 1;
                    //string pageNumber = dgv.Name.Substring(dgv.Name.IndexOf("@") + 1);
                    //int remainder = dgv.Rows.Count % spaceRows;
                    //int startIndex = 0;
                    //int serialStep = column.SerialStep > 0 ? column.SerialStep : 1;
                    //bool spaceEqualStep = spaceRows == column.SerialStep + 1;

                    //int mod = int.Parse(pageNumber) % spaceRows;
                    //if (mod != 1 && mod != 0)
                    //{
                    //    startIndex = Math.Abs(spaceRows - (int.Parse(pageNumber) % spaceRows - 1));
                    //}
                    //else if (mod == 0 && spaceRows != 1)
                    //{
                    //    startIndex = 1;
                    //}

                    //int realRow = 0;
                    //if (spaceEqualStep)
                    //    startIndex = 0;

                    //if (dicPageSerial.ContainsKey(int.Parse(pageNumber) - 1))
                    //{
                    //    List<int> listSerial = dicPageSerial[int.Parse(pageNumber) - 1];
                    //    if (listSerial[listSerial.Count - 1] == dgv.Rows.Count - spaceRows + 1)
                    //    {
                    //        startIndex = spaceRows - 1;
                    //    }
                    //}

                    ////if (!string.IsNullOrEmpty(PBomID) && dtOper == null)
                    ////{
                    ////    dtOper = MaterialModuleBLL.GetOperByPBomId(PBomID);
                    ////}

                    //range = 0;
                    ////新增空行集合
                    //List<int> listEmptyRowIndex = new List<int>();
                    ////序号行集合
                    //List<int> listSerialRowIndex = new List<int>();
                    //foreach (DataGridViewRow dr in dgv.Rows)
                    //{
                    //    if (dr.Cells[column.Index].Style.Tag != null &&
                    //        dr.Cells[column.Index].Style.Tag.ToString() == "1")
                    //    {
                    //        listEmptyRowIndex.Add(dr.Index);
                    //    }
                    //}

                    ////序号显示行总计
                    //int totalSerialRows = GetSerialRows(pageNumber);

                    //int serialRows = 0;
                    //for (int i = startIndex; i < dgv.Rows.Count; i += spaceRows)
                    //{
                    //    //自动序号列
                    //    if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                    //    {
                    //        realRow = ((int.Parse(pageNumber) - 1) * dgv.Rows.Count + i + 1);
                    //        int emptyRows = 0;
                    //        int consecutiveNumber = 1;
                    //        int empIndex = 0;
                    //        foreach (int index in listEmptyRowIndex)
                    //        {
                    //            if (listEmptyRowIndex.Count > empIndex + 1 &&
                    //                listEmptyRowIndex[empIndex + 1] == index + 1)
                    //            {
                    //                consecutiveNumber++;
                    //            }
                    //            empIndex++;
                    //        }
                    //        foreach (int index in listEmptyRowIndex)
                    //        {
                    //            if (i > index || (listEmptyRowIndex.Contains(i) &&
                    //                consecutiveNumber == listEmptyRowIndex.Count))
                    //            {
                    //                emptyRows++;
                    //            }
                    //        }
                    //        if (emptyRows > 0)
                    //        {
                    //            if (i + emptyRows < dgv.Rows.Count)
                    //            {
                    //                if (dicPageSerial.ContainsKey(int.Parse(pageNumber) - 1))
                    //                {
                    //                    dgv.Rows[i + emptyRows].Cells[column.Index].Value = ((totalSerialRows + serialRows) * spaceRows + 1) * serialStep;
                    //                }
                    //                else
                    //                {
                    //                    dgv.Rows[i + emptyRows].Cells[column.Index].Value = realRow * serialStep;
                    //                }
                    //                serialRows++;
                    //                listSerialRowIndex.Add(i + emptyRows);
                    //                //序号行
                    //                dgv.Rows[i + emptyRows].Cells[column.Index].Style.Tag = "2";
                    //            }
                    //            else if (i + emptyRows >= dgv.Rows.Count && i < dgv.Rows.Count)
                    //            {
                    //                dgv.Rows[i].Cells[column.Index].Value = ((totalSerialRows + serialRows) * spaceRows + 1) * serialStep;
                    //                dgv.Rows[i].Cells[column.Index].Style.Tag = "2";
                    //            }
                    //            else
                    //            {
                    //                dgv.Rows[i].Cells[column.Index].Value = "";
                    //                dgv.Rows[i].Cells[column.Index].Style.Tag = "0";
                    //            }
                    //        }
                    //        else
                    //        {
                    //            //根据步长得到序号
                    //            if (dicPageSerial.ContainsKey(int.Parse(pageNumber) - 1))
                    //            {
                    //                dgv.Rows[i].Cells[column.Index].Value = ((totalSerialRows + serialRows) * spaceRows + 1) * serialStep;
                    //            }
                    //            else
                    //            {
                    //                dgv.Rows[i].Cells[column.Index].Value = realRow * serialStep;
                    //            }
                    //            //dgv.Rows[i].Cells[column.Index].Value = (spaceEqualStep ? realRow : realRow - realRow / spaceRows) * serialStep; 
                    //            serialRows++;
                    //            listSerialRowIndex.Add(i);
                    //            dgv.Rows[i].Cells[column.Index].Style.Tag = "2";
                    //        }
                    //        i += emptyRows;
                    //    }
                    //    else
                    //    {
                    //        string columnValue = ((DetailGridViewTextBoxColumn)column).ColumnValue;

                    //        if (!string.IsNullOrEmpty(columnValue) && string.IsNullOrEmpty(column.Source))
                    //        {
                    //            string[] strValues = columnValue.Split(new char[] { ',' });
                    //            //int rowRange = 0;

                    //            //foreach (DataGridViewRow row in dgv.Rows)
                    //            //{
                    //            //    row.Cells[column.Index].Value = strValues[rowRange];
                    //            //    rowRange++;
                    //            //}
                    //            dgv.Rows[i].Cells[column.Index].Value = strValues[i];
                    //        }//来源不为空，则自动带出来源数据
                    //        else if (!string.IsNullOrEmpty(column.Source))
                    //        {
                    //            string sourceName = column.Source.Substring(column.Source.IndexOf("\\") + 1);
                    //            string typeName = column.Source.Substring(0, column.Source.IndexOf("\\"));

                    //            if (dtOper != null)
                    //            {
                    //                if (typeName.ToLower() == "process")
                    //                {
                    //                    //foreach (DataRow row in dtOper.Rows)
                    //                    //{
                    //                    //    if (dtOper.Columns.Contains(sourceName) && rowRange <= dgv.Rows.Count - 1)
                    //                    //    {
                    //                    //        dgv.Rows[i].Cells[column.Index].Value = row[sourceName].ToString();
                    //                    //    }
                    //                    //    else
                    //                    //    {
                    //                    //        //dtTemp.Rows.Add(row.ItemArray);
                    //                    //    }
                    //                    //}
                    //                    if (range < dtOper.Rows.Count)
                    //                    {
                    //                        dgv.Rows[i].Cells[column.Index].Value = dtOper.Rows[range][sourceName].ToString();
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    range++;
                    //}
                    //if (listEmptyRowIndex.Count > 0)
                    //{
                    //    if (dicPageSerial.ContainsKey(int.Parse(pageNumber)))
                    //    {
                    //        dicPageSerial[int.Parse(pageNumber)] = listSerialRowIndex;
                    //    }
                    //    else
                    //    {
                    //        dicPageSerial.Add(int.Parse(pageNumber), listSerialRowIndex);
                    //    }
                    //}

                    #endregion

                    #region Old Code
                    //*
                    int spaceRows = int.Parse(column.SpaceRows) + 1;
                    string pageNumber = dgv.Name.Substring(dgv.Name.IndexOf("@") + 1);
                    int remainder = dgv.Rows.Count % spaceRows;
                    int startIndex = 0;
                    int serialStep = column.SerialStep > 0 ? column.SerialStep : 1;
                    bool spaceEqualStep = spaceRows == column.SerialStep + 1;

                    int mod = int.Parse(pageNumber) % spaceRows;
                    if (mod != 1 && mod != 0)
                    {
                        startIndex = Math.Abs(spaceRows - (int.Parse(pageNumber) % spaceRows - 1));
                    }
                    else if (mod == 0 && spaceRows != 1)
                    {
                        startIndex = 1;
                    }

                    int realRow = 0;
                    if (spaceEqualStep)
                        startIndex = 0;

                    //if (!string.IsNullOrEmpty(PBomID) && dtOper == null)
                    //{
                    //    dtOper = MaterialModuleBLL.GetOperByPBomId(PBomID);
                    //}

                    range = 0;
                    //foreach (DataGridViewRow dr in dgv.Rows)
                    //{
                    //    realRow = ((int.Parse(pageNumber) - 1) * dgv.Rows.Count + dr.Index + 1);
                    //    if (!dicRealRowTag.ContainsKey(realRow))
                    //    {
                    //        if (dr.Cells[0].Style.Tag != null)
                    //        {
                    //            dicRealRowTag.Add(realRow, int.Parse(dr.Cells[0].Style.Tag.ToString()));
                    //        }
                    //        else
                    //            dicRealRowTag.Add(realRow, 0);
                    //    }
                    //}

                    int serialRows = 0;
                    bool isOpen = false;
                    //序号显示行总计
                    int totalSerialRows = GetSerialRows(pageNumber);
                    for (int i = startIndex; i < dgv.Rows.Count; i += spaceRows)
                    {
                        //realRow = ((int.Parse(pageNumber) - 1) * dgv.Rows.Count + i + 1);
                        //自动序号列
                        if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                        {
                            if (dgv.Rows[i].Cells[column.Index].Style.Tag == null)
                            {
                                //根据步长得到序号
                                if (totalSerialRows > 0)
                                {
                                    dgv.Rows[i].Cells[column.Index].Value = (totalSerialRows + serialRows + 1) * serialStep;
                                }
                                else
                                    dgv.Rows[i].Cells[column.Index].Value = (serialRows + 1) * serialStep;
                                //dgv.Rows[i].Cells[column.Index].Value = (spaceEqualStep ? realRow : realRow - realRow / spaceRows) * serialStep;                               
                                serialRows++;

                                //if (dicRealRowTag.ContainsKey(i))
                                //{
                                //    dicRealRowTag[i] = 2;
                                //}
                                dgv.Rows[i].Cells[column.Index].Style.Tag = 2;
                            }
                            else if (dgv.Rows[i].Cells[column.Index].Style.Tag != null)
                            {
                                isOpen = true;
                                break;
                            }
                            

                            if (dicSerialRows.ContainsKey(pageNumber))
                            {
                                dicSerialRows[pageNumber] = serialRows;
                            }
                            else
                                dicSerialRows.Add(pageNumber, serialRows);
                        }
                        else
                        {
                            string columnValue = ((DetailGridViewTextBoxColumn)column).ColumnValue;

                            if (!string.IsNullOrEmpty(columnValue) && (string.IsNullOrEmpty(column.Source) || isBrowser))
                            {
                                string[] strValues = columnValue.Split(new char[] { ',' });
                                //int rowRange = 0;

                                //foreach (DataGridViewRow row in dgv.Rows)
                                //{
                                //    row.Cells[column.Index].Value = strValues[rowRange];
                                //    rowRange++;
                                //}
                                dgv.Rows[i].Cells[column.Index].Value = strValues[i];
                            }//来源不为空，则自动带出来源数据
                            else if (!string.IsNullOrEmpty(column.Source))
                            {
                                string sourceName = column.Source.Substring(column.Source.IndexOf("\\") + 1);
                                string typeName = column.Source.Substring(0, column.Source.IndexOf("\\"));

                                if (dtOper != null)
                                {
                                    if (typeName.ToLower() == "process")
                                    {
                                        //foreach (DataRow row in dtOper.Rows)
                                        //{
                                        //    if (dtOper.Columns.Contains(sourceName) && rowRange <= dgv.Rows.Count - 1)
                                        //    {
                                        //        dgv.Rows[i].Cells[column.Index].Value = row[sourceName].ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        //dtTemp.Rows.Add(row.ItemArray);
                                        //    }
                                        //}
                                        if (range < dtOper.Rows.Count)
                                        {
                                            dgv.Rows[i].Cells[column.Index].Value = dtOper.Rows[range][sourceName].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        range++;                        
                    }

                    serialRows = 0;
                    if (isOpen)
                    {
                        for (int i = 0; i < dgv.Rows.Count; i++)
                        {
                            //realRow = ((int.Parse(pageNumber) - 1) * dgv.Rows.Count + i + 1);
                            //自动序号列
                            if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                            {
                                if (dgv.Rows[i].Cells[column.Index].Style.Tag != null)
                                {
                                    if (dgv.Rows[i].Cells[column.Index].Style.Tag.ToString() == "2")
                                    {
                                        //根据步长得到序号
                                        if (totalSerialRows > 0)
                                        {
                                            dgv.Rows[i].Cells[column.Index].Value = (totalSerialRows + serialRows + 1) * serialStep;
                                        }
                                        else
                                            dgv.Rows[i].Cells[column.Index].Value = (serialRows + 1) * serialStep;
                           
                                        serialRows++;

                                        //if (dicRealRowTag.ContainsKey(i))
                                        //{
                                        //    dicRealRowTag[i] = 2;
                                        //}
                                        dgv.Rows[i].Cells[column.Index].Style.Tag = 2;
                                    }
                                    else if (dgv.Rows[i].Cells[column.Index].Style.Tag.ToString() == "1")
                                    {
                                        dgv.Rows[i].Cells[column.Index].Style.Tag = 1;
                                    }
                                }

                                if (dicSerialRows.ContainsKey(pageNumber))
                                {
                                    dicSerialRows[pageNumber] = serialRows;
                                }
                                else
                                    dicSerialRows.Add(pageNumber, serialRows);
                            }
                            else
                            {
                                string columnValue = ((DetailGridViewTextBoxColumn)column).ColumnValue;

                                if (!string.IsNullOrEmpty(columnValue) && string.IsNullOrEmpty(column.Source))
                                {
                                    string[] strValues = columnValue.Split(new char[] { ',' });
                                    dgv.Rows[i].Cells[column.Index].Value = strValues[i];
                                }//来源不为空，则自动带出来源数据
                                else if (!string.IsNullOrEmpty(column.Source))
                                {
                                    string sourceName = column.Source.Substring(column.Source.IndexOf("\\") + 1);
                                    string typeName = column.Source.Substring(0, column.Source.IndexOf("\\"));

                                    if (dtOper != null)
                                    {
                                        if (typeName.ToLower() == "process")
                                        {
                                            if (range < dtOper.Rows.Count)
                                            {
                                                dgv.Rows[i].Cells[column.Index].Value = dtOper.Rows[range][sourceName].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                            range++;
                        }
                    }
                    //*/
                    #endregion
                }
            }

            if (dtOper != null && dtOper.Rows.Count > 0 && dtOper.Rows.Count > range - 1)
            {
                dtTemp = dtOper.Clone();
                int rowRange = 0;
                foreach (DataRow row in dtOper.Rows)
                {
                    if (rowRange > range - 1)
                    {
                        dtTemp.Rows.Add(row.ItemArray);
                    }
                    rowRange++;
                }

                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    dtOper = new DataTable();
                    dtOper = dtTemp.Clone();
                    foreach (DataRow row in dtTemp.Rows)
                    {
                        dtOper.Rows.Add(row.ItemArray);
                    }
                    //当前行在为明细列表的最后一行
                    int index = int.Parse(dgv.Name.Substring(dgv.Name.IndexOf("@") + 1)) + 1;
                    //如果当前卡片为最后一页则新增卡片
                    if (dgv.Name.Contains("@") &&
                        dgv.Name.Substring(dgv.Name.IndexOf("@") + 1) == pageCount.ToString())
                    {
                        AddNewCard();

                        if (listPanels != null && listPanels.Count > 0)
                        {
                            int rang = 0;
                            foreach (Panel pn in listPanels)
                            {
                                if (pn.Name != "pnCard" && rang - 1 >= 0)
                                {
                                    pn.Location = new Point(listPanels[rang - 1].Location.X, listPanels[rang - 1].Location.Y + pnCard.Height + 10);
                                }
                                rang++;
                            }
                        }
                    }
                    else
                    {
                        //否则跳到下一卡片的明细列表.
                        SetNextFocus(index);
                    }
                }
            }
        }

        private void SetSerialRows(DataGridView dgv)
        {
            foreach (DetailGridViewTextBoxColumn column in dgv.Columns)
            {

                string pageNumber = dgv.Name.Substring(dgv.Name.IndexOf("@") + 1);
                int serialRows = 0;
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    //自动序号列
                    if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                    {
                        if (dgv.Rows[i].Cells[column.Index].Style.Tag != null &&
                            dgv.Rows[i].Cells[column.Index].Style.Tag.ToString() == "2")
                        {
                            serialRows++;
                        }

                        if (dicSerialRows.ContainsKey(pageNumber))
                        {
                            dicSerialRows[pageNumber] = serialRows;
                        }
                        else
                            dicSerialRows.Add(pageNumber, serialRows);
                    }
                    else
                        continue;
                }
            }
        }

        /// <summary>
        /// 方法说明：获取所有显示序号的行
        /// 作    者：jason.tang
        /// 完成时间：2013-09-22
        /// </summary>
        /// <param name="pagenum">页码</param>
        /// <returns></returns>
        private int GetSerialRows(string pagenum)
        {
            int totalSerialRows = 0;
            int pageNumber = int.Parse(pagenum);
            for (int i = pageNumber; i > 0; i--)
            {
                if (i - 1 > 0 && dicSerialRows.ContainsKey(Convert.ToString(i - 1)))
                {
                    totalSerialRows += dicSerialRows[Convert.ToString(i - 1)];
                }
            }

            return totalSerialRows;
        }

        /// <summary>
        /// 方法说明：刷新当前单元格
        /// 作      者：jason.tang
        /// 完成时间：2013-04-01
        /// </summary>
        /// <param name="obj"></param>       
        private void RefreshCell(object obj)
        {
            if (obj == null)
            {
                return;
            }

            try
            {
                Dictionary<string, object> dicFormat = (Dictionary<string, object>)obj;
                //字体
                Font font = (Font)dicFormat["Font"];

                //样式
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = font;
                //排列
                string alignment = dicFormat["Alignment"].ToString();
                style.Alignment = (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), alignment);
                //边距
                List<int> listPadding = (List<int>)dicFormat["Padding"];
                style.Padding = new Padding(listPadding[0], listPadding[1], listPadding[2], listPadding[3]);

                CurrentClickCell.Style = style;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 方法说明：获取单元格属性
        /// 作   者：jason.tang
        /// 完成时间：2013-04-01
        /// </summary>
        /// <returns>属性集合</returns>
        private Dictionary<string, object> GetCellProperties()
        {
            Dictionary<string, object> dicProperties = new Dictionary<string, object>();
            dicProperties.Add("Font", CurrentClickCell.Style.Font);
            dicProperties.Add("Alignment", CurrentClickCell.Style.Alignment);

            List<int> listPadding = new List<int>();
            listPadding.Add(CurrentClickCell.Style.Padding.Left);
            listPadding.Add(CurrentClickCell.Style.Padding.Top);
            listPadding.Add(CurrentClickCell.Style.Padding.Right);
            listPadding.Add(CurrentClickCell.Style.Padding.Bottom);
            dicProperties.Add("Padding", listPadding);

            return dicProperties;
        }

        /// <summary>
        /// 设置Ole对象
        /// </summary>
        public void SetOleObject()
        {
            OleObjectSelectionFrm form = new OleObjectSelectionFrm();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string type = form.OleType;
                string filename = form.FileName;
                if (CurrentClickCell.GetType() != typeof(DataGridViewTextBoxCellEx) || CurrentDataGrid == null)
                {
                    return;
                }

                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)CurrentClickCell;
                int columnIndex = cell.ColumnIndex;
                int rowIndex = cell.RowIndex;

                if (cell.SpanCell.X != rowIndex && cell.SpanCell.X >= 0)
                {
                    rowIndex = cell.SpanCell.X;
                }
                if (cell.SpanCell.Y != columnIndex && cell.SpanCell.Y >= 0)
                {
                    columnIndex = cell.SpanCell.Y;
                }

                int width = CurrentDataGrid.Columns[columnIndex].Width - 1;
                int height = CurrentDataGrid.Rows[rowIndex].Height - 1;

                DataGridViewTextBoxCellEx realCell = (DataGridViewTextBoxCellEx)CurrentDataGrid.Rows[rowIndex].Cells[columnIndex];
                for (int i = 1; i < realCell.RowSpan; i++)
                {
                    height += CurrentDataGrid.Rows[rowIndex + i].Height;
                }

                for (int i = 1; i < realCell.ColumnSpan; i++)
                {
                    width += CurrentDataGrid.Columns[columnIndex + i].Width;
                }

                Rectangle rect = CurrentDataGrid.GetCellDisplayRectangle(columnIndex, rowIndex, false);
                int top = rect.Y;
                int left = rect.X;

                PictureBox pOle = new PictureBox();
                pOle.SizeMode = PictureBoxSizeMode.StretchImage;

                string gid = Guid.NewGuid().ToString();

                pOle.Width = width;
                pOle.Height = height;
                pOle.Left = left;
                pOle.Top = top;
                //pOle.Padding = new Padding(2);
                //pOle.BackColor = Color.Black;         

                switch (type.ToLower())
                {
                    case "avi":
                        pOle.Image = Properties.Resources.avi;
                        break;
                    case "bitmap":
                        pOle.Image = Properties.Resources.img;
                        break;
                    case "autocad":
                        pOle.Image = Properties.Resources.CAD;
                        break;
                    default:
                        pOle.Image = Properties.Resources.white;
                        break;
                }


                pOle.DoubleClick += pOle_DoubleClick;
                pOle.Click += pOle_Click;
                CurrentDataGrid.Controls.Add(pOle);
                //dgvTemp.SendToBack();                

                if (!string.IsNullOrEmpty(type))
                {
                    if (type.ToLower() == "bitmap")
                    {
                        pOle.Name = "pBmp" + gid;

                        //List<string> oldpath = GetRegistData();
                        //Process p = Process.Start("mspaint");//notepad");
                        //p.WaitForInputIdle();
                        //SetParent(p.MainWindowHandle, this.Handle);
                        //bool isshow = ShowWindowAsync(p.MainWindowHandle, 3);

                        //p.WaitForExit();
                        //List<string> newpath = GetRegistData();

                        //HashSet<string> h1 = new HashSet<string>(oldpath);
                        //HashSet<string> h2 = new HashSet<string>(newpath);
                        //List<string> listResult = new List<string>();
                        //h2.ExceptWith(h1);
                        //if (h2.Count > 0)
                        //{
                        //    foreach (string s in h2)
                        //    {
                        //        pOle.ImageLocation = s;
                        //        pOle.Image = Image.FromFile(s);
                        //    }
                        //}
                        //else
                        //{
                        //    pOle.ImageLocation = oldpath[0];
                        //    pOle.Image = Image.FromFile(oldpath[0]);
                        //}
                    }
                    else if (type.ToLower() == "autocad")
                    {
                        pOle.Name = "pCad" + gid;

                        if (!string.IsNullOrEmpty(filename))
                        {
                            pOle.ImageLocation = filename;
                        }

                    }
                    else if (type.ToLower() == "avi")
                    {
                        pOle.Name = "AVI" + gid;
                    }
                }
                else if(type.ToLower() == "autocad")
                {
                    pOle.ImageLocation = filename;
                    if (filename.EndsWith(".dwg"))
                    {
                        pOle.Name = "pCad" + gid;
                    }
                }

                currOlePicture = pOle;
                ClearPictrueBoxBorder(true);

                //OLD对象锁定
                //ControlOperator operat = new ControlOperator(pOle);

                //operat.Size = true;  //是否能改变控件大小 
                //operat.Move = true;  //是否能移动控件 
                //operat.Max = false;   //是否能移动大于窗体的位置 
                //operat.Min = false;   //是否能移动到窗体的最前面
            }
        }

        /// <summary>
        /// 方法说明：清除PictureBox边框
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// <param name="pic">是否PictureBox</param>
        private void ClearPictrueBoxBorder(bool pic)
        {
            if (CurrentDataGrid == null)
                return;
            if (pic && currOlePicture == null)
            {
                return;
            }
            foreach (Control control in CurrentDataGrid.Controls)
            {
                if (control is PictureBox)
                {
                    if (!pic || control.Name != currOlePicture.Name)
                    {
                        control.Padding = new Padding(0);
                        control.BackColor = Color.Transparent;
                    }
                    else
                    {
                        control.Padding = new Padding(2);
                        control.BackColor = Color.Black;
                    }
                }
                else if (control is DataGridView)
                {
                    ((DataGridView)control).ClearSelection();
                }
            }
        }

        /// <summary>
        /// 方法说明：Ftp文件上传
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private int UploadFun(string fileName)
        {
        //    /// <summary>
        ///// ftp方式上传 
        ///// </summary>
        //public static int UploadFtp(string filePath, string filename, string ftpServerIP, string ftpUserID, string ftpPassword)
        //{
            string uploadUrl = ConfigurationManager.AppSettings["FtpUrl"].ToString();
            string userId = ConfigurationManager.AppSettings["Uid"].ToString();
            string password = ConfigurationManager.AppSettings["Pass"].ToString();
            if (string.IsNullOrEmpty(fileName))
                return 0;
            FileInfo fileInf = new FileInfo(fileName);
            string uri = uploadUrl + "/" + fileInf.Name;
            FtpWebRequest reqFTP;
            // Create FtpWebRequest object from the Uri provided 
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uploadUrl + "/" + fileInf.Name));
            try
            {
                // Provide the WebPermission Credintials 
                reqFTP.Credentials = new NetworkCredential(userId, password);
 
                // By default KeepAlive is true, where the control connection is not closed 
                // after a command is executed. 
                reqFTP.KeepAlive = false;
 
                // Specify the command to be executed. 
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
 
                // Specify the data transfer type. 
                reqFTP.UseBinary = true;
 
                // Notify the server about the size of the uploaded file 
                reqFTP.ContentLength = fileInf.Length;
 
                // The buffer size is set to 2kb 
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
 
                // Opens a file stream (System.IO.FileStream) to read the file to be uploaded 
                //FileStream fs = fileInf.OpenRead(); 
                FileStream fs = fileInf.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
 
                // Stream to which the file to be upload is written 
                Stream strm = reqFTP.GetRequestStream();
 
                // Read from the file stream 2kb at a time 
                contentLen = fs.Read(buff, 0, buffLength);
 
                // Till Stream content ends 
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload Stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
 
                // Close the file stream and the Request Stream 
                strm.Close();
                fs.Close();
                return 0;
            }
            catch (Exception ex)
            {
                reqFTP.Abort();
                //  Logging.WriteError(ex.Message + ex.StackTrace);
                return -2;
            }
        }

        /// <summary>
        /// 方法说明：ftp方式下载 
        /// </summary>
        public static int DownloadFtp(string filePath)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string downloadUrl = ConfigurationManager.AppSettings["FtpUrl"].ToString();
                string userId = ConfigurationManager.AppSettings["Uid"].ToString();
                string password = ConfigurationManager.AppSettings["Pass"].ToString();

                FileStream outputStream = new FileStream(filePath, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(downloadUrl + "/" + fileInfo.Name));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.KeepAlive = false;
                reqFTP.Credentials = new NetworkCredential(userId, password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                return 0;
            }
            catch (Exception ex)
            {
                // Logging.WriteError(ex.Message + ex.StackTrace);
                // System.Windows.Forms.MessageBox.Show(ex.Message);
                return -2;
            }
        }
        
        /// <summary>
        /// 方法说明：自动保存卡片
        /// 作   者：jason.tang
        /// 完成时间：2013-09-12
        /// </summary>
        /// <param name="obj"></param>
        private void AutoSaveCard(object obj)
        {

            List<Card> listCards = new List<Card>();
            string cardName = string.Empty;
            foreach (Control control in this.Controls)
            {
                if (control is Panel)
                {
                    foreach (Control ctrl in control.Controls)
                    {
                        if (ctrl is DataGridView)
                        {
                            Card card = new Card();
                            card = GetCard((Panel)control, (DataGridView)ctrl);
                            if (card == null)
                            {
                                return;
                            }
                            listCards.Add(card);
                            cardName = card.Name;
                        }
                    }
                }
            }
            if (listCards.Count == 0)
            {
                return;
            }

            try
            {
                ProcessCard card = new ProcessCard();
                CardsXML cards = new CardsXML();
                string path = Application.StartupPath + "\\temp";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                path = string.Format(path + "\\{0}.card", cardName);
                cards.Cards = listCards.ToArray();
                card.Card = cards;
                card.Name = cardName;
                if (!string.IsNullOrEmpty(ModuleId))
                {
                    card.CardModuleId = new Guid(ModuleId);
                }
                if (this.Tag != null)
                {
                    card.ID = new Guid(this.Tag.ToString());
                }
                Kingdee.CAPP.Common.Serialize.SerializeHelper.BinarySerialize<ProcessCard>(card, path);
            }
            catch
            {

            }

        }
        
        #endregion
        
    }
}
