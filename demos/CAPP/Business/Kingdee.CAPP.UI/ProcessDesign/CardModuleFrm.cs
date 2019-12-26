using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using Kingdee.CAPP.UI.Common;
using Kingdee.CAPP.Common;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;
using System.IO;

namespace Kingdee.CAPP.UI.ProcessDesign
{
    /// <summary>
    /// 类型说明：卡片模板窗体
    /// 作   者：jason.tang
    /// 完成时间：2012-12-17
    /// </summary>
    public partial class CardModuleFrm : BaseForm
    {
        #region 变量声明

        #region API
        /*
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);


        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);


        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(
        string lpszDriver, // driver name
        string lpszDevice, // device name
        string lpszOutput, // not used; should be NULL
        Int64 lpInitData // optional printer data
        );


        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(
        IntPtr hdc, // handle to DC
        int nIndex // index of capability
        );


        [DllImport("user32.dll")]
        internal static extern bool SetProcessDPIAware();


        const int DRIVERVERSION = 0;
        const int TECHNOLOGY = 2;
        const int HORZSIZE = 4;
        const int VERTSIZE = 6;
        const int HORZRES = 8;
        const int VERTRES = 10;
        const int BITSPIXEL = 12;
        const int PLANES = 14;
        const int NUMBRUSHES = 16;
        const int NUMPENS = 18;
        const int NUMMARKERS = 20;
        const int NUMFONTS = 22;
        const int NUMCOLORS = 24;
        const int PDEVICESIZE = 26;
        const int CURVECAPS = 28;
        const int LINECAPS = 30;
        const int POLYGONALCAPS = 32;
        const int TEXTCAPS = 34;
        const int CLIPCAPS = 36;
        const int RASTERCAPS = 38;
        const int ASPECTX = 40;
        const int ASPECTY = 42;
        const int ASPECTXY = 44;
        const int SHADEBLENDCAPS = 45;
        const int LOGPIXELSX = 88;
        const int LOGPIXELSY = 90;
        const int SIZEPALETTE = 104;
        const int NUMRESERVED = 106;
        const int COLORRES = 108;
        const int PHYSICALWIDTH = 110;
        const int PHYSICALHEIGHT = 111;
        const int PHYSICALOFFSETX = 112;
        const int PHYSICALOFFSETY = 113;
        const int SCALINGFACTORX = 114;
        const int SCALINGFACTORY = 115;
        const int VREFRESH = 116;
        const int DESKTOPVERTRES = 117;
        const int DESKTOPHORZRES = 118;
        const int BLTALIGNMENT = 119;
        */
        #endregion

        /// <summary>
        /// 选中的单元格左上角坐标
        /// </summary>
        private Point minValue = new Point(0, 0);
        /// <summary>
        /// 选中的单元格右下角坐标
        /// </summary>
        private Point maxValue = new Point(0, 0);
        /// <summary>
        /// 当前选中的明细框名字
        /// </summary>
        private string CurrentGridName;

        /// <summary>
        /// 当前鼠标所在的位置
        /// </summary>
        private DataGridView.HitTestInfo hti;
        /// <summary>
        /// 当前选择的图像
        /// </summary>
        private string currPicture;
        
        /// <summary>
        /// 行高
        /// </summary>
        private List<int> listRowHeight;

        /// <summary>
        /// 列宽
        /// </summary>
        private List<int> listColWidth;

        /// <summary>
        /// 列线条坐标
        /// </summary>
        private Dictionary<Point, Point> dicColumnLine;

        /// <summary>
        /// 行线条坐标
        /// </summary>
        private Dictionary<Point, Point> dicRowLine;

        /// <summary>
        /// 当前点击的列线条索引
        /// </summary>
        private int clickColumnDivider = 0;

        /// <summary>
        /// 当前点击的行线条索引
        /// </summary>
        private int clickRowDivider = 0;

        /// <summary>
        /// 当前Copy的单元格
        /// </summary>
        private Dictionary<Point, DataGridViewTextBoxCellEx> dicCopyCells;
        /// <summary>
        /// 百分比输入框
        /// </summary>
        private TextBox txtPercent;
        /// <summary>
        /// 百分比
        /// </summary>
        private string[] percentNumbers;
        /// <summary>
        /// 模板的值是否发生变化
        /// </summary>
        private bool valueChanged;
        /// <summary>
        /// 是否初始化
        /// </summary>
        private bool Initialize;

        /// <summary>
        /// 单元格高、宽是否变动
        /// </summary>
        private bool cellChanged;

        /// <summary>
        /// 变化前的字体
        /// </summary>
        private float oldSize;

        /// <summary>
        /// 暂存当前Cell
        /// </summary>
        private static DataGridViewTextBoxCellEx CurrentSelectCell;

        private string startupPath = string.Empty;

        /// <summary>
        /// 增加的行数
        /// </summary>
        private int addRows = 0;
        /// <summary>
        /// 增加的列数
        /// </summary>
        private int addColumns = 0;

        #endregion

        #region 属性声明

        /// <summary>
        /// 卡片模板名称
        /// </summary>
        private string _cardModuleName;
        public string CardModuleName
        {
            get
            {
                return _cardModuleName;
            }
            set
            {
                _cardModuleName = value;
            }
        }

        /// <summary>
        /// 纸张宽度
        /// </summary>
        private int _width;
        public int PageWidth
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        /// <summary>
        /// 纸张高度
        /// </summary>
        private int _height;
        public int PageHeight
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        /// <summary>
        /// 左边距
        /// </summary>
        private int _padleft;
        public int PaddingLeft
        {
            get
            {
                return _padleft;
            }
            set
            {
                _padleft = value;
            }
        }

        /// <summary>
        /// 上边距
        /// </summary>
        private int _padtop;
        public int PaddingTop
        {
            get
            {
                return _padtop;
            }
            set
            {
                _padtop = value;
            }
        }

        /// <summary>
        /// 右边距
        /// </summary>
        private int _padright;
        public int PaddingRight
        {
            get
            {
                return _padright;
            }
            set
            {
                _padright = value;
            }
        }

        /// <summary>
        /// 下边距
        /// </summary>
        private int _padbottom;
        public int PaddingBottom
        {
            get
            {
                return _padbottom;
            }
            set
            {
                _padbottom = value;
            }
        }

        /// <summary>
        /// 左偏移
        /// </summary>
        private int _offsetleft;
        public int OffsetLeft
        {
            get
            {
                return _offsetleft;
            }
            set
            {
                _offsetleft = value;
            }
        }

        /// <summary>
        /// 上偏移
        /// </summary>
        private int _offsettop;
        public int OffsetTop
        {
            get
            {
                return _offsettop;
            }
            set
            {
                _offsettop = value;
            }
        }

        /// <summary>
        /// 卡片幅面
        /// </summary>
        private int _breadth;
        public int CardBreadth
        {
            get
            {
                return _breadth;
            }
            set
            {
                _breadth = value;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private int _cardtype;
        public int CardType
        {
            get
            {
                return _cardtype;
            }
            set
            {
                _cardtype = value;
            }
        }

        /// <summary>
        /// 模板ID
        /// </summary>
        private string _moduleid;
        public string ModuleId
        {
            get
            {
                return _moduleid;
            }
            set
            {
                _moduleid = value;
            }
        }

        /// <summary>
        /// 静态属性公布当前窗体，便于其他窗体调用该窗体公用方法
        /// </summary>
        public static CardModuleFrm cardModuleFrm { get; set; }

        #endregion

        #region 窗体控件事件

        public CardModuleFrm()
        {
            InitializeComponent();


            //双缓存
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            this.UpdateStyles();

            //dgvCardModule.GetType().GetProperty("DoubleBuffered",
            //    BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvCardModule, true, null);

            //typeof(DataGridView).InvokeMember(
            //   "DoubleBuffered",
            //   BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            //   null,
            //   dgvCardModule,
            //   new object[] { true });

            //Type dgvType = dgvCardModule.GetType();
            //PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            //    BindingFlags.Instance | BindingFlags.NonPublic);
            //pi.SetValue(dgvCardModule, true, null);

            dgvCardModule.Cursor = Cursors.Cross;
            //dgvCardModule.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //dgvCardModule.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            SetMenuEnable(true);

            //SetProcessDPIAware(); //重要
            //IntPtr screenDC = GetDC(IntPtr.Zero);
            //int dpi_x = GetDeviceCaps(screenDC, LOGPIXELSX);
            //int dpi_y = GetDeviceCaps(screenDC, LOGPIXELSY);
            //ReleaseDC(IntPtr.Zero, screenDC);

            CardModuleFrm.cardModuleFrm = this;            
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        private void tsmnuMerge_Click(object sender, EventArgs e)
        {
            MergeCell();
        }

        /// <summary>
        /// 取消合并单元格
        /// </summary>
        private void tsmnuCancelMerge_Click(object sender, EventArgs e)
        {
            CancelMergeCell();
        }

        /// <summary>
        /// 插入整行
        /// </summary>
        private void tsmnuInsertRow_Click(object sender, EventArgs e)
        {
            int currIndex = dgvCardModule.CurrentRow.Index;
            int i = 1;

            if (currIndex - 1 < 0)
            {
                return;
            }

            while (dgvCardModule.Rows[currIndex - i].Height - 10 <= 0)
            {
                i++;
                if (currIndex - i < 0 || currIndex - i > dgvCardModule.Rows.Count)
                {
                    return;
                }
            }

            try
            {
                int maxCellNumber = 0;
                foreach (DataGridViewRow dr in dgvCardModule.Rows)
                {
                    foreach (DataGridViewCell cell in dr.Cells)
                    {
                        string cellTag = ((DataGridViewTextBoxCellEx)cell).CellTag;
                        int cellNumber = 0;
                        if (cellTag.StartsWith("Cell"))
                        {
                            bool result = int.TryParse(cellTag.Replace("Cell", ""), out cellNumber);
                            if (result && cellNumber > maxCellNumber)
                            {
                                maxCellNumber = cellNumber;
                            }
                        }
                    }
                }

                //检查上一行和当前行是否包含合并的单元格
                int preRowIndex = dgvCardModule.CurrentRow.Index - 1;
                List<DataGridViewTextBoxCellEx> listCurrSpanCell = CheckCrossSpanCell(currIndex, 0, 0, 0);  //当前行贯穿的合并单元格
                List<DataGridViewTextBoxCellEx> listPreSpanCell = CheckCrossSpanCell(preRowIndex, 0, 0, 0); //上一行贯穿的合并单元格
                List<DataGridViewTextBoxCellEx> listTopCell = CheckCrossSpanCell(0, 0, 1, currIndex);       //跟当前行在同一行的合并单元格
                List<DataGridViewTextBoxCellEx> listBelowCell = CheckCrossSpanCell(0, 0, 3, currIndex);     //当前行下面行所包含的合并单元格

                //插入行时，需要动态加入行的高度          
                dgvCardModule.Rows.Insert(currIndex, 1);
                listRowHeight.Insert(currIndex, 10);
                listRowHeight[currIndex - i] = dgvCardModule.Rows[currIndex - i].Height - 10;
                //ResizeDetailGrid(true, 1, currIndex);
                dgvCardModule.RowHeightChanged -= dgvCardModule_RowHeightChanged;
                dgvCardModule.Rows[currIndex - i].Height = dgvCardModule.Rows[currIndex - i].Height - 10;                
                dgvCardModule.Rows[currIndex].Height = 10;
                dgvCardModule.RowHeightChanged += dgvCardModule_RowHeightChanged;

                float pageSize = (float)_breadth;
                float size = _breadth < 4 ? 8f - _breadth * 1.5f : 4f - _breadth;
                Font defaultFont = this.Font;

                //DataGridViewTextBoxCellEx cell;                

                foreach (DataGridViewColumn col in dgvCardModule.Columns)
                {
                    dgvCardModule.Rows[currIndex].Cells[col.Index].Style.Font = new Font(defaultFont.FontFamily, defaultFont.Size - size, defaultFont.Style);
                    dgvCardModule.Rows[currIndex].Cells[col.Index].Style.WrapMode = DataGridViewTriState.True;
                                       

                    List<DataGridViewCustomerCellStyle> listCellStyle = new List<DataGridViewCustomerCellStyle>();
                    DataGridViewCustomerCellStyle cellStyle = new DataGridViewCustomerCellStyle();
                    cellStyle.Font = defaultFont;
                    cellStyle.WrapMode = DataGridViewTriState.True;
                    listCellStyle.Add(cellStyle);
                    ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[col.Index]).CustStyle = listCellStyle;
                    ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[col.Index]).CellTag = string.Format("Cell{0}", maxCellNumber + col.Index + 1);
                    ////增加行时，如果该列包含合并的单元格，则合并行数加1
                    //foreach (DataGridViewRow  row in dgvCardModule.Rows)
                    //{
                    //    cell = (DataGridViewTextBoxCellEx)row.Cells[col.Index];
                    //    if (cell.RowSpan > 1 && cell.RowIndex != dgvCardModule.CurrentCell.RowIndex &&
                    //        cell.RowIndex < dgvCardModule.CurrentCell.RowIndex &&
                    //        cell.RowSpan + cell.RowIndex + 1 > dgvCardModule.CurrentCell.RowIndex)
                    //    {
                    //        cell.RowSpan += 1;
                    //    }
                    //}
                }

                //设置上、底边框（适应单个单元格）
                foreach (DataGridViewColumn column in dgvCardModule.Columns)
                {
                    if (currIndex + 1 < dgvCardModule.Rows.Count)//当前行的底边为加1行的上边
                    {
                        DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[column.Index];
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex + 1].Cells[column.Index];

                        if (cell1.SpanCell.X < 0 || cell1.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell1.BottomBorderColor = cell2.TopBorderColor;
                            cell1.BottomBorderWidth = cell2.TopBorderWidth;
                            cell1.BottomBorderStyle = cell2.TopBorderStyle;
                        }
                    }

                    if (currIndex - 1 >= 0)//减1行的底边清空
                    {
                        DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[column.Index];
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex - 1].Cells[column.Index];

                        if (cell2.SpanCell.X < 0 || cell2.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell2.SpanCell.X].Cells[cell2.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell2.SpanCell.X].Cells[cell2.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell2.BottomBorderColor = Color.Empty;
                            cell2.BottomBorderWidth = 0;
                        }

                        if (cell1.SpanCell.X < 0 || cell1.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell1.LeftBorderColor = cell2.LeftBorderColor;
                            cell1.LeftBorderWidth = cell2.LeftBorderWidth;
                            cell1.LeftBorderStyle = cell2.LeftBorderStyle;

                            cell1.RightBorderColor = cell2.RightBorderColor;
                            cell1.RightBorderWidth = cell2.RightBorderWidth;
                            cell1.RightBorderStyle = cell2.RightBorderStyle;
                        }

                        if (column.Index > 0)
                        {
                            DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[column.Index - 1];

                            if (cell3.SpanCell.X < 0 || cell3.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell3.SpanCell.X].Cells[cell3.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell3.SpanCell.X].Cells[cell3.SpanCell.Y]).ColumnSpan <= 1))
                            {
                                cell3.RightBorderColor = cell2.LeftBorderColor;
                                cell3.RightBorderWidth = cell2.LeftBorderWidth;
                                cell3.RightBorderStyle = cell2.LeftBorderStyle;
                            }
                        }

                        if (column.Index + 1 < dgvCardModule.Columns.Count)
                        {
                            DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[currIndex].Cells[column.Index + 1];

                            if (cell4.SpanCell.X < 0 || cell4.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell4.SpanCell.X].Cells[cell4.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell4.SpanCell.X].Cells[cell4.SpanCell.Y]).ColumnSpan <= 1))
                            {
                                cell4.LeftBorderColor = cell2.RightBorderColor;
                                cell4.LeftBorderWidth = cell2.RightBorderWidth;
                                cell4.LeftBorderStyle = cell2.RightBorderStyle;
                            }
                        }
                    }
                }


                //当前行贯穿的合并单元格边框设置
                if (listCurrSpanCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listCurrSpanCell)
                    {
                        cell.RowSpan += 1;
                        SetNewCellBorders(currIndex, 0, cell);

                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cell.RowIndex, cell.ColumnIndex);
                            }
                        }
                    }
                }                

                //上一行贯穿的合并单元格的边框设置
                if (listPreSpanCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listPreSpanCell)
                    {
                        if (!listCurrSpanCell.Contains(cell))
                        {
                            cell.RowSpan += 1;
                            SetNewCellBorders(currIndex, 0, cell);

                            for (int r = 0; r < cell.RowSpan; r++)
                            {
                                for (int c = 0; c < cell.ColumnSpan; c++)
                                {
                                    (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                        new Point(cell.RowIndex, cell.ColumnIndex);
                                }
                            }
                        }
                    }
                }
                //跟当前行在同一行的合并单元格的边框设置
                if (listTopCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listTopCell)
                    {
                        SetNewCellBorders(currIndex, 0, cell);
                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cell.RowIndex, cell.ColumnIndex);
                            }
                        }
                    }
                }
                //当前行下面行所包含的合并单元格的行索引加1
                if (listBelowCell.Count > 0)
                {
                    int spanRowIndex = 0;
                    foreach (DataGridViewTextBoxCellEx cell in listBelowCell)
                    {
                        spanRowIndex = cell.RowIndex + 1;
                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(spanRowIndex, cell.ColumnIndex);
                            }
                        }
                    }
                }

                foreach (Control control in dgvCardModule.Controls)
                {
                    if (control.GetType() == typeof(DataGridView) &&
                        control.Name.StartsWith("dgv"))
                    {
                        string row = control.Name.Substring(3, control.Name.IndexOf("-") - 3);
                        string col = control.Name.Substring(control.Name.IndexOf("-") + 1);

                        int dgvRowIndex = int.Parse(row);

                        if (currIndex <= dgvRowIndex)
                        {
                            dgvRowIndex += 1;
                        }

                        control.Name = string.Format("dgv{0}-{1}", dgvRowIndex, col);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 方法说明：校验是否有贯穿合并的单元格
        /// 作    者：jason.tang
        /// 完成时间：2013-01-31
        /// </summary>
        /// <param name="rowIndex">当前行索引</param>
        /// <param name="colIndex">当前列索引</param>
        /// <param name="tat">标识[1-行2-列3-当前行下面的行4-当前列右边的列]</param>
        private List<DataGridViewTextBoxCellEx> CheckCrossSpanCell(int rowIndex, int colIndex, int tag, int index)
        {
            List<DataGridViewTextBoxCellEx> listSpanCells = new List<DataGridViewTextBoxCellEx>();
            //当前列是否贯穿合并的单元格
            if (colIndex > 0)
            {
                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)row.Cells[colIndex];
                    if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                        cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                    {
                        DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                        if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) && 
                            !listSpanCells.Contains(spanCell) && 
                            spanCell.ColumnIndex != colIndex)
                        {
                            listSpanCells.Add(spanCell);
                        }
                    }
                }
            }//当前行是否贯穿合并的单元格
            else if (rowIndex > 0)
            {
                foreach (DataGridViewColumn column in dgvCardModule.Columns)
                {
                    DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex].Cells[column.Index];
                    if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                        cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                    {
                        DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                        if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) && 
                            !listSpanCells.Contains(spanCell) && 
                            spanCell.RowIndex != rowIndex)
                        {
                            listSpanCells.Add(spanCell);
                        }
                    }
                }
            }
            if (index > 0)
            {
                if (tag == 1)
                {
                    foreach (DataGridViewColumn column in dgvCardModule.Columns)
                    {
                        DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[index].Cells[column.Index];
                        if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                            cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                        {
                            DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                            if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) &&
                                !listSpanCells.Contains(spanCell) && 
                            spanCell.RowIndex == index)
                            {
                                listSpanCells.Add(spanCell);
                            }
                        }
                    }
                }
                else if(tag == 2)
                {
                    foreach (DataGridViewRow row in dgvCardModule.Rows)
                    {
                        DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)row.Cells[index];
                        if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                            cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                        {
                            DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                            if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) &&
                                !listSpanCells.Contains(spanCell) && 
                            spanCell.ColumnIndex == index)
                            {
                                listSpanCells.Add(spanCell);
                            }
                        }
                    }
                }
                else if (tag == 3)
                {
                    for (int r = index; r < dgvCardModule.Rows.Count; r++)
                    {
                        foreach (DataGridViewColumn column in dgvCardModule.Columns)
                        {
                            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[r].Cells[column.Index];
                            if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                                cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                            {
                                DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                                if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) &&
                                    !listSpanCells.Contains(spanCell) &&
                                spanCell.RowIndex > index)
                                {
                                    listSpanCells.Add(spanCell);
                                }
                            }
                        }
                    }
                }
                else if(tag == 4)
                {
                    for (int c = index; c < dgvCardModule.Columns.Count; c++)
                    {
                        foreach (DataGridViewRow row in dgvCardModule.Rows)
                        {
                            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)row.Cells[c];
                            if (cell.ColumnSpan == 1 && cell.RowSpan == 1 &&
                                cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
                            {
                                DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                                if ((spanCell.ColumnSpan > 1 || spanCell.RowSpan > 1) &&
                                    !listSpanCells.Contains(spanCell) &&
                                spanCell.ColumnIndex > index)
                                {
                                    listSpanCells.Add(spanCell);
                                }
                            }
                        }
                    }
                }
            }

            return listSpanCells;
        }

        /// <summary>
        /// 方法说明：设置新增行列贯穿合并单元格时的边框
        /// 作者：jason.tang
        /// 完成时间：2013-02-01
        /// </summary>
        /// <param name="rowIndex">行索引</param>
        /// <param name="colIndex">列索引</param>
        /// <param name="cell">主合并单元格</param>
        private void SetNewCellBorders(int rowIndex, int colIndex, DataGridViewTextBoxCellEx cell)
        {
            if (rowIndex > 0)
            {
                if (rowIndex == cell.RowIndex - 1)
                {
                    //设置上、底边框
                    for (int i = 0; i < cell.ColumnSpan; i++)
                    {
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderColor = cell.TopBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderWidth = cell.TopBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderStyle = cell.TopBorderStyle;

                        (dgvCardModule.Rows[rowIndex - 1].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderColor = Color.Empty;
                        (dgvCardModule.Rows[rowIndex - 1].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderWidth = 0;
                    }
                }
                else
                {
                    //设置新增行的左边框和右边框                
                    if (cell.ColumnIndex > 0)
                    {
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex] as DataGridViewTextBoxCellEx).LeftBorderColor = cell.LeftBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex] as DataGridViewTextBoxCellEx).LeftBorderWidth = cell.LeftBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex] as DataGridViewTextBoxCellEx).LeftBorderStyle = cell.LeftBorderStyle;

                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex - 1] as DataGridViewTextBoxCellEx).RightBorderColor = cell.LeftBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex - 1] as DataGridViewTextBoxCellEx).RightBorderWidth = cell.LeftBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex - 1] as DataGridViewTextBoxCellEx).RightBorderStyle = cell.LeftBorderStyle;
                    }
                    if (cell.ColumnIndex + cell.ColumnSpan < dgvCardModule.Columns.Count)
                    {
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan - 1] as DataGridViewTextBoxCellEx).RightBorderColor = cell.RightBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan - 1] as DataGridViewTextBoxCellEx).RightBorderWidth = cell.RightBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan - 1] as DataGridViewTextBoxCellEx).RightBorderStyle = cell.RightBorderStyle;

                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan] as DataGridViewTextBoxCellEx).LeftBorderColor = cell.RightBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan] as DataGridViewTextBoxCellEx).LeftBorderWidth = cell.RightBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + cell.ColumnSpan] as DataGridViewTextBoxCellEx).LeftBorderStyle = cell.RightBorderStyle;
                    }

                    //设置底边框
                    for (int i = 0; i < cell.ColumnSpan; i++)
                    {
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderColor = cell.BottomBorderColor;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderWidth = cell.BottomBorderWidth;
                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).BottomBorderStyle = cell.BottomBorderStyle;

                        (dgvCardModule.Rows[rowIndex].Cells[cell.ColumnIndex + i] as DataGridViewTextBoxCellEx).SpanCell = new Point(cell.RowIndex, cell.ColumnIndex);
                    }
                }
            }
            else if (colIndex > 0)
            {
                if (colIndex == cell.ColumnIndex - 1)
                {
                    //设置左、右边框
                    for (int i = 0; i < cell.RowSpan; i++)
                    {
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderColor = cell.LeftBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderWidth = cell.LeftBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderStyle = cell.LeftBorderStyle;

                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex - 1] as DataGridViewTextBoxCellEx).RightBorderColor = Color.Empty;
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex - 1] as DataGridViewTextBoxCellEx).RightBorderWidth = 0;
                    }
                }
                else
                {
                    //设置新增行的上边框和下边框                
                    if (cell.RowIndex > 0)
                    {
                        (dgvCardModule.Rows[cell.RowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderColor = cell.TopBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderWidth = cell.TopBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderStyle = cell.TopBorderStyle;

                        (dgvCardModule.Rows[cell.RowIndex - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderColor = cell.TopBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderWidth = cell.TopBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderStyle = cell.TopBorderStyle;
                    }
                    if (cell.RowIndex + cell.RowSpan < dgvCardModule.Rows.Count)
                    {
                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderColor = cell.BottomBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderWidth = cell.BottomBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan - 1].Cells[colIndex] as DataGridViewTextBoxCellEx).BottomBorderStyle = cell.BottomBorderStyle;

                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderColor = cell.BottomBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderWidth = cell.BottomBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex + cell.RowSpan].Cells[colIndex] as DataGridViewTextBoxCellEx).TopBorderStyle = cell.BottomBorderStyle;
                    }

                    //设置底右框
                    for (int i = 0; i < cell.RowSpan; i++)
                    {
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderColor = cell.RightBorderColor;
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderWidth = cell.RightBorderWidth;
                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).RightBorderStyle = cell.RightBorderStyle;

                        (dgvCardModule.Rows[cell.RowIndex + i].Cells[colIndex] as DataGridViewTextBoxCellEx).SpanCell = new Point(cell.RowIndex, colIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 插入整列
        /// </summary>
        private void tsmnuInsertColumn_Click(object sender, EventArgs e)
        {
            int currIndex = dgvCardModule.CurrentCell.ColumnIndex;
            int i = 1;
            if (currIndex - i < 0)
            {
                return;
            }

            while (dgvCardModule.Columns[currIndex - i].Width - 10 <= 0)
            {
                i++;
                if (currIndex - i < 0 || currIndex - i > dgvCardModule.Columns.Count)
                {
                    return;
                }
            }

            try
            {
                int maxCellNumber = 0;
                foreach (DataGridViewRow dr in dgvCardModule.Rows)
                {
                    foreach (DataGridViewCell cell in dr.Cells)
                    {
                        string cellTag = ((DataGridViewTextBoxCellEx)cell).CellTag;
                        int cellNumber = 0;
                        if (cellTag.StartsWith("Cell"))
                        {
                            bool result = int.TryParse(cellTag.Replace("Cell", ""), out cellNumber);
                            if (result && cellNumber > maxCellNumber)
                            {
                                maxCellNumber = cellNumber;
                            }
                        }
                    }
                }

                //检查上一列和当前列是否包含合并的单元格
                int preColIndex = dgvCardModule.CurrentCell.ColumnIndex - 1;
                List<DataGridViewTextBoxCellEx> listCurrSpanCell = CheckCrossSpanCell(0, currIndex, 0, 0);    //当前列贯穿的合并单元格
                List<DataGridViewTextBoxCellEx> listPreSpanCell = CheckCrossSpanCell(0, preColIndex, 0, 0);   //上一列贯穿的合并单元格
                List<DataGridViewTextBoxCellEx> listTopCell = CheckCrossSpanCell(0, 0, 2, currIndex);         //跟当前列在同一行的合并单元格
                List<DataGridViewTextBoxCellEx> listRightCell = CheckCrossSpanCell(0, 0, 4, currIndex);       //当前列右边列所包含的合并单元格
                
                dgvCardModule.ColumnWidthChanged -= dgvCardModule_ColumnWidthChanged;
                //插入列时，需要动态加入列的宽度   
                DataGridViewTextBoxColumnEx column = new DataGridViewTextBoxColumnEx();
                column.Width = 10;
                dgvCardModule.Columns.Insert(dgvCardModule.CurrentCell.ColumnIndex, column);
                listColWidth.Insert(currIndex, 10);
                listColWidth[currIndex - i] = dgvCardModule.Columns[currIndex - i].Width - 10;

                //ResizeDetailGrid(false, 2, currIndex);
                dgvCardModule.Columns[currIndex - i].Width = dgvCardModule.Columns[currIndex - i].Width - 10;

                dgvCardModule.ColumnWidthChanged += dgvCardModule_ColumnWidthChanged;

                float pageSize = (float)_breadth;
                float size = _breadth < 4 ? 8f - _breadth * 1.5f : 4f - _breadth;
                Font defaultFont = this.Font;
                int rowIndex = 0;
                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    row.Cells[column.Index].Style.Font = new Font(defaultFont.FontFamily, defaultFont.Size - size, defaultFont.Style);
                    row.Cells[column.Index].Style.WrapMode = DataGridViewTriState.True;

                    List<DataGridViewCustomerCellStyle> listCellStyle = new List<DataGridViewCustomerCellStyle>();
                    DataGridViewCustomerCellStyle cellStyle = new DataGridViewCustomerCellStyle();
                    cellStyle.Font = defaultFont;
                    cellStyle.WrapMode = DataGridViewTriState.True;
                    listCellStyle.Add(cellStyle);
                    ((DataGridViewTextBoxCellEx)row.Cells[column.Index]).CustStyle = listCellStyle;

                    rowIndex = row.Index;
                    ((DataGridViewTextBoxCellEx)row.Cells[column.Index]).CellTag = string.Format("Cell{0}", maxCellNumber + rowIndex + 1);

                    
                    ////增加列时，如果该列包含合并的单元格，则合并列数加1
                    //foreach (DataGridViewTextBoxCellEx cell in row.Cells)
                    //{
                    //    if (cell.ColumnSpan > 1 && cell.ColumnIndex != dgvCardModule.CurrentCell.ColumnIndex &&
                    //        cell.ColumnIndex < dgvCardModule.CurrentCell.ColumnIndex &&
                    //        (cell.ColumnSpan + cell.ColumnIndex + 1 > dgvCardModule.CurrentCell.ColumnIndex))
                    //    {
                    //        cell.ColumnSpan += 1;
                    //    }
                    //}
                }

                //设置左、右边框（适应单个单元格）
                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    if (currIndex + 1 < dgvCardModule.Columns.Count)//当前列的右边为加1列左边的样式
                    {
                        DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index].Cells[currIndex];
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index].Cells[currIndex + 1];

                        if (cell1.SpanCell.X < 0 || cell1.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell1.RightBorderColor = cell2.LeftBorderColor;
                            cell1.RightBorderWidth = cell2.LeftBorderWidth;
                            cell1.RightBorderStyle = cell2.LeftBorderStyle;
                        }
                    }

                    if (currIndex - 1 >= 0)//减1列的右边去除
                    {
                        DataGridViewTextBoxCellEx cell1 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index].Cells[currIndex - 1];
                        DataGridViewTextBoxCellEx cell2 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index].Cells[currIndex];

                        if (cell1.SpanCell.X < 0 || cell1.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell1.SpanCell.X].Cells[cell1.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell1.RightBorderColor = Color.Empty;
                            cell1.RightBorderWidth = 0;
                        }

                        if (cell2.SpanCell.X < 0 || cell2.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell2.SpanCell.X].Cells[cell2.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell2.SpanCell.X].Cells[cell2.SpanCell.Y]).ColumnSpan <= 1))
                        {
                            cell2.TopBorderColor = cell1.TopBorderColor;
                            cell2.TopBorderWidth = cell1.TopBorderWidth;
                            cell2.TopBorderStyle = cell1.TopBorderStyle;

                            cell2.BottomBorderColor = cell1.BottomBorderColor;
                            cell2.BottomBorderWidth = cell1.BottomBorderWidth;
                            cell2.BottomBorderStyle = cell1.BottomBorderStyle;
                        }

                        if (row.Index > 0)
                        {
                            DataGridViewTextBoxCellEx cell3 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index - 1].Cells[currIndex];

                            if (cell3.SpanCell.X < 0 || cell3.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell3.SpanCell.X].Cells[cell3.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell3.SpanCell.X].Cells[cell3.SpanCell.Y]).ColumnSpan <= 1))
                            {
                                cell3.BottomBorderColor = cell1.TopBorderColor;
                                cell3.BottomBorderWidth = cell1.TopBorderWidth;
                                cell3.BottomBorderStyle = cell1.TopBorderStyle;
                            }
                        }

                        if (row.Index + 1 < dgvCardModule.Rows.Count)
                        {
                            DataGridViewTextBoxCellEx cell4 = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[row.Index + 1].Cells[column.Index];

                            if (cell4.SpanCell.X < 0 || cell4.SpanCell.Y < 0
                            || (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell4.SpanCell.X].Cells[cell4.SpanCell.Y]).RowSpan <= 1
                            && ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell4.SpanCell.X].Cells[cell4.SpanCell.Y]).ColumnSpan <= 1))
                            {
                                cell4.TopBorderColor = cell1.BottomBorderColor;
                                cell4.TopBorderWidth = cell1.BottomBorderWidth;
                                cell4.TopBorderStyle = cell1.BottomBorderStyle;
                            }
                        }
                    }
                }

                //设置当前贯穿的单元格边框
                if (listCurrSpanCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listCurrSpanCell)
                    {
                        cell.ColumnSpan += 1;
                        SetNewCellBorders(0, currIndex, cell);

                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cell.RowIndex, cell.ColumnIndex);
                            }
                        }
                    }
                }                

                //设置前一列贯穿的单元格边框
                if (listPreSpanCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listPreSpanCell)
                    {
                        if (!listCurrSpanCell.Contains(cell))
                        {
                            cell.ColumnSpan += 1;
                            SetNewCellBorders(0, currIndex, cell);

                            for (int r = 0; r < cell.RowSpan; r++)
                            {
                                for (int c = 0; c < cell.ColumnSpan; c++)
                                {
                                    (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                        new Point(cell.RowIndex, cell.ColumnIndex);
                                }
                            }
                        }
                    }
                }
                //设置当前列右边与之相邻的合并单元格边框
                if (listTopCell.Count > 0)
                {
                    foreach (DataGridViewTextBoxCellEx cell in listTopCell)
                    {
                        SetNewCellBorders(0, currIndex, cell);

                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cell.RowIndex, cell.ColumnIndex);
                            }
                        }
                    }
                }
                //设置当前列右边的所有合并单元格的列索引加1
                if (listRightCell.Count > 0)
                {
                    int spanColumnIndex = 0;
                    foreach (DataGridViewTextBoxCellEx cell in listRightCell)
                    {
                        spanColumnIndex = cell.ColumnIndex + 1;

                        for (int r = 0; r < cell.RowSpan; r++)
                        {
                            for (int c = 0; c < cell.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cell.RowIndex + r].Cells[cell.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cell.RowIndex, spanColumnIndex);
                            }
                        }
                    }
                }

                foreach (Control control in dgvCardModule.Controls)
                {
                    if (control.GetType() == typeof(DataGridView) &&
                        control.Name.StartsWith("dgv"))
                    {
                        string col = control.Name.Substring(control.Name.IndexOf("-") + 1);
                        string row = control.Name.Substring(3, control.Name.IndexOf("-") - 3);
                                
                        int dgvColIndex = int.Parse(col);

                        if (currIndex <= dgvColIndex)
                        {
                            dgvColIndex += 1;
                        }

                        control.Name = string.Format("dgv{0}-{1}", row, dgvColIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 删除整行
        /// </summary>
        private void tsmnuDeleteRow_Click(object sender, EventArgs e)
        {
            //行数为1或少于1返回
            if (dgvCardModule.Rows.Count <= 1)
            {
                return;
            }

            int height = dgvCardModule.Rows[CurrentSelectCell.RowIndex].Height;
            dgvCardModule.RowHeightChanged -= dgvCardModule_RowHeightChanged;
            
            if (CurrentSelectCell.RowIndex == 0)
            {
                dgvCardModule.Rows[CurrentSelectCell.RowIndex + 1].Height += height;
            }
            else
            {
                dgvCardModule.Rows[CurrentSelectCell.RowIndex - 1].Height += height;
            }

            dgvCardModule.Rows.RemoveAt(CurrentSelectCell.RowIndex);

            DataGridViewTextBoxCellEx cell;
            foreach (DataGridViewColumn col in dgvCardModule.Columns)
            {                
                //删除行时，如果该列包含合并的单元格，则合并行数减1
                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    cell = (DataGridViewTextBoxCellEx)row.Cells[col.Index];
                    if (cell.RowSpan > 1 && cell.RowIndex != dgvCardModule.CurrentCell.RowIndex &&
                        cell.RowIndex < dgvCardModule.CurrentCell.RowIndex &&
                        cell.RowSpan + cell.RowIndex > dgvCardModule.CurrentCell.RowIndex)
                    {
                        cell.RowSpan -= 1;
                    }
                }
            }

            List<DataGridViewTextBoxCellEx> listBelowCell = CheckCrossSpanCell(0, 0, 3, CurrentSelectCell.RowIndex);     //当前行下面行所包含的合并单元格
            //删除行时，需要还原合并单元格的行索引
            if (listBelowCell.Count > 0)
            {
                int spanRowIndex = 0;
                foreach (DataGridViewTextBoxCellEx cl in listBelowCell)
                {
                    spanRowIndex = cl.RowIndex - 1;
                    if (spanRowIndex >= 0)
                    {
                        for (int r = 0; r < cl.RowSpan; r++)
                        {
                            for (int c = 0; c < cl.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cl.RowIndex + r].Cells[cl.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(spanRowIndex, cl.ColumnIndex);
                            }
                        }
                    }
                }
            }

            dgvCardModule.RowHeightChanged += dgvCardModule_RowHeightChanged;
        }

        /// <summary>
        /// 删除整列
        /// </summary>
        private void tsmnuDeleteColumn_Click(object sender, EventArgs e)
        {
            //列数为1或少于1返回
            if (dgvCardModule.Columns.Count <= 1)
            {
                return;
            }

            int width = dgvCardModule.Columns[CurrentSelectCell.ColumnIndex].Width;
            dgvCardModule.ColumnWidthChanged -= dgvCardModule_ColumnWidthChanged;
            
            if (CurrentSelectCell.ColumnIndex == 0)
            {
                dgvCardModule.Columns[CurrentSelectCell.ColumnIndex + 1].Width += width;
            }
            else
            {
                dgvCardModule.Columns[CurrentSelectCell.ColumnIndex - 1].Width += width;
            }

            dgvCardModule.Columns.RemoveAt(CurrentSelectCell.ColumnIndex);
            
            foreach (DataGridViewRow row in dgvCardModule.Rows)
            {                
                //删除列时，如果该列包含合并的单元格，则合并列数减1
                foreach (DataGridViewTextBoxCellEx cell in row.Cells)
                {
                    if (cell.ColumnSpan > 1 && cell.ColumnIndex != dgvCardModule.CurrentCell.ColumnIndex &&
                        cell.ColumnIndex < dgvCardModule.CurrentCell.ColumnIndex &&
                        (cell.ColumnSpan + cell.ColumnIndex > dgvCardModule.CurrentCell.ColumnIndex))
                    {
                        cell.ColumnSpan -= 1;
                    }
                }
            }

            List<DataGridViewTextBoxCellEx> listRightCell = CheckCrossSpanCell(0, 0, 4, CurrentSelectCell.ColumnIndex);       //当前列右边列所包含的合并单元格
            //删除列时，需要还原合并单元格的列索引
            if (listRightCell.Count > 0)
            {
                int spanColumnIndex = 0;
                foreach (DataGridViewTextBoxCellEx cl in listRightCell)
                {                    
                    spanColumnIndex = cl.ColumnIndex - 1;
                    if (spanColumnIndex >= 0)
                    {
                        for (int r = 0; r < cl.RowSpan; r++)
                        {
                            for (int c = 0; c < cl.ColumnSpan; c++)
                            {
                                (dgvCardModule.Rows[cl.RowIndex + r].Cells[cl.ColumnIndex + c] as DataGridViewTextBoxCellEx).SpanCell =
                                    new Point(cl.RowIndex, spanColumnIndex);
                            }
                        }
                    }
                }
            }

            dgvCardModule.ColumnWidthChanged += dgvCardModule_ColumnWidthChanged;
        }       

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void tsmnuInit_Click(object sender, EventArgs e)
        {
            MergeCellFrm form = new MergeCellFrm(dgvCardModule.Width, dgvCardModule.Height);
            DialogResult result = form.ShowDialog();
            List<int> listHeight = form.ListHeight;
            List<int> listWidth = form.ListWidth;

            if (result == DialogResult.OK)
            {
                InitDataGridView(listHeight, listWidth, true);
                SetMenuEnable(false);
                if (listWidth.Count == 1 && listHeight.Count == 1)
                {
                    tsmnuInit.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 单元格单击事件
        /// </summary>
        private void dgvCardModule_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Name == "dgvCardModule")
            {
                if (dgv.Controls.Count > 0)
                {
                    try
                    {
                        if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                        {
                            if ((Control.ModifierKeys & Keys.Shift) == Keys.Shift)//按住Shift键点击单元格会选中该行
                            {
                                dgvCardModule.ClearSelection();
                                dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex].Selected = true;
                            }
                            else if ((Control.ModifierKeys & Keys.Alt) == Keys.Alt)  //按住Alt键点击单元格会选中该列
                            {
                                dgvCardModule.ClearSelection();
                                foreach (DataGridViewRow row in dgvCardModule.Rows)
                                {
                                    row.Cells[dgvCardModule.CurrentCell.ColumnIndex].Selected = true;
                                }
                            }
                            else
                            {
                                foreach (Control control in dgv.Controls)
                                {
                                    if (control.GetType() == typeof(DataGridView))
                                    {
                                        ((DataGridView)control).ClearSelection();
                                    }
                                }
                            }
                        }                        
                    }
                    catch
                    {
                    }
                }
            }
            else
            {
                if ((Control.ModifierKeys & Keys.Control) != Keys.Control)
                {
                    dgvCardModule.ClearSelection();
                    ClearPictrueBoxBorder(false);
                }
                CurrentGridName = dgv.Name;
                //得到明细框单元格的当前索引
                string[] index = CurrentGridName.Split(new char[2] { 'v', '-' });
                if (index.Length > 0)
                {
                    dgvCardModule.CurrentCell = dgvCardModule.Rows[int.Parse(index[1])].Cells[int.Parse(index[2])];
                }

                //选中所有行
                dgv.SelectAll();
            }

            if (!string.IsNullOrEmpty(currPicture))
            {
                ClearPictrueBoxBorder(false);
                currPicture = null;
            }

            //当选择多个单元格时，需要使用单元格批量属性设置
            tsmnuBatchSetProperty.Enabled = dgv.SelectedCells.Count > 1;
            tsmnuCellProperty.Enabled = dgv.SelectedCells.Count == 1;

            int colSpan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan;
            int rowSpan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan;

            tsslblMessage.Text = string.Format("当前行:[{0}]，当前列:[{1}]",
                dgvCardModule.CurrentCell.RowIndex + 1, dgvCardModule.CurrentCell.ColumnIndex + 1);

            //dgvCardModule.Invalidate();
        }

        /// <summary>
        /// 双击单元格打开单元格属性窗体
        /// </summary>
        private void dgvCardModule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //CellPropertiesFrm form = new CellPropertiesFrm();
            //form.InvokeEvent += new CellPropertiesFrm.DelegateForm(RefreshCell);
            //form.CellProperties = GetCellProperties();
            ////选中所有行
            //DataGridView dgv = (DataGridView)sender;
            //if (dgv.Name != "dgvCardModule")
            //{
            //    dgv.SelectAll();
            //}
            //form.ShowDialog();
        }
        
        /// <summary>
        /// 边框线属性
        /// </summary>
        private void tsmnuBorderProperty_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 边框属性
        /// </summary>
        private void tsmnuCellBorder_Click(object sender, EventArgs e)
        {
            //CellBorderFrm form = new CellBorderFrm();
            //Dictionary<string, object> BorderProperties = new Dictionary<string, object>();
            //BorderProperties.Add("BorderLeft", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellBorderLeft);
            //BorderProperties.Add("BorderTop", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellBorderTop);
            //BorderProperties.Add("BorderRight", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellBorderRight);
            //BorderProperties.Add("BorderBottom", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellBorderBottom);
            //BorderProperties.Add("BorderAll", false);
            //BorderProperties.Add("LeftBorderColor", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).LeftBorderColor);
            //BorderProperties.Add("TopBorderColor", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).TopBorderColor);
            //BorderProperties.Add("RightBorderColor", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RightBorderColor);
            //BorderProperties.Add("BottomBorderColor", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).BottomBorderColor);

            //BorderProperties.Add("LeftBorderWidth", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).LeftBorderWidth);
            //BorderProperties.Add("TopBorderWidth", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).TopBorderWidth);
            //BorderProperties.Add("RightBorderWidth", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RightBorderWidth);
            //BorderProperties.Add("BottomBorderWidth", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).BottomBorderWidth);

            //BorderProperties.Add("LeftBorderStyle", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).LeftBorderStyle);
            //BorderProperties.Add("TopBorderStyle", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).TopBorderStyle);
            //BorderProperties.Add("RightBorderStyle", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RightBorderStyle);
            //BorderProperties.Add("BottomBorderStyle", (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).BottomBorderStyle);
            
            //form.BorderProperties = BorderProperties;

            //if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    BorderProperties = form.BorderProperties;
            //    //SetCellBorder(BorderProperties);
            //}
        }

        //private void dgvCardModule_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    //if (e.ColumnIndex == 3 && e.RowIndex == 1)
        //    //{
        //    //    SolidBrush brush = new SolidBrush(Color.DarkGray);
        //    //    e.Graphics.FillRectangle(brush, e.CellBounds);
        //    //    brush.Dispose();
        //    //    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
        //    //    ControlPaint.DrawBorder(e.Graphics, e.CellBounds, Color.Red, 0, ButtonBorderStyle.None, Color.Red, 1, ButtonBorderStyle.Solid, Color.Red, 1, ButtonBorderStyle.Solid, Color.Red, 1, ButtonBorderStyle.Solid);
        //    //}            
        //}

        /// <summary>
        /// 特殊符号
        /// </summary>
        private void tsmnuSpecialSymbol_Click(object sender, EventArgs e)
        {
            SpecialSymbolFrm form = new SpecialSymbolFrm();
            form.SymbolAddEvent += new ProcessDesign.SpecialSymbolFrm.DelegateForm(GetSymbol);
            form.ShowDialog();
        }

        /// <summary>
        /// 窗体变化，整个表格区域位置跟着相应变化
        /// </summary>
        private void CardModuleFrm_Resize(object sender, EventArgs e)
        {
            CardModulePanelResize();
        }

        private void CardModulePanelResize()
        {
            int remainWidth = this.Width - pnModule.Width;
            int remainHeight = this.Height - pnModule.Height;
            if (remainWidth > 20)
            {
                pnModule.Location = new Point(remainWidth / 2, pnModule.Location.Y);
            }
            if (remainHeight > 20)
            {
                pnModule.Location = new Point(pnModule.Location.X, remainHeight / 2);
            }
        }

        /// <summary>
        /// Grid列宽尺寸变化
        /// </summary>
        private void dgvCardModule_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {            
            if (dgvCardModule.CurrentCell != null && cellChanged)
            {
                ResizeModuleGrid(false, 0, 0);
                valueChanged = true && !Initialize;
            }
        }

        /// <summary>
        /// Grid行高尺寸变化
        /// </summary>
        private void dgvCardModule_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            if (dgvCardModule.CurrentCell != null && cellChanged)
            {
                ResizeModuleGrid(true, 0, 0);
                valueChanged = true && !Initialize;
            }
        }

        private void CardModuleFrm_Load(object sender, EventArgs e)
        {
            txtPercent = new TextBox();
            ToolStripControlHost tch = new ToolStripControlHost(txtPercent);
            txtPercent.ReadOnly = true;
            txtPercent.Validating += new CancelEventHandler(txtPercent_Validating);
            txtPercent.KeyDown += new KeyEventHandler(txtPercent_KeyDown);
            this.statusMessage.Items.Insert(0, tch);

            startupPath = Application.StartupPath + "\\Resources";

            percentNumbers = Kingdee.CAPP.UI.Resource.GlobalResource.PercentNum.Split(new string[] { "," }, StringSplitOptions.None);

            ResizeInitGridView(_width, _height, -1);

            pnModule.MouseWheel += new MouseEventHandler(pnModule_MouseWheel);

            if (!string.IsNullOrEmpty(_moduleid))
            {
                GetTemplate(_moduleid, null);
            }

            #region 如果属性窗体没有打开则打开属性窗口
                        
            bool isExists = false;
            FormCollection collection = Application.OpenForms;
            foreach (Form form in collection)
            {
                if (form.Name == "PropertiesNavigate")
                {
                    isExists = true;
                    break;
                }

            }
            if (!isExists)
            {
                PropertiesNavigate property = new PropertiesNavigate();
                MainFrm.mainFrm.OpenNavigate(property, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
            }

            #endregion

            cmnuDataGridview.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();
            cmnuTitle.Renderer = new Kingdee.CAPP.Controls.CustomMenuRender();

            this.TabPageContextMenuStrip = cmnuTitle;
        }

        void txtPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetPercent();
            }
        }

        void txtPercent_Validating(object sender, CancelEventArgs e)
        {
            SetPercent();
        }

        /// <summary>
        /// 事件说明：根据Ctrl+鼠标滚轮缩放Panel
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        void pnModule_MouseWheel(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                int delta = e.Delta / 120;
                double percent = 0;
                
                bool isDigit = double.TryParse(txtPercent.Text.Replace("%", ""), out percent);
                if (!isDigit)
                {
                    return;
                }

                double oldPercent = percent;

                int index = 0;
                foreach (string str in percentNumbers)
                {
                    if (delta > 0)
                    {
                        if (double.Parse(str) > percent)
                        {
                            percent = double.Parse(str);
                            break;
                        }
                    }
                    else
                    {
                        if (double.Parse(str) < percent)
                        {
                            double clearPercent = 0;
                            for (int i = index; i < percentNumbers.Length - index; i++)
                            {
                                if (double.Parse(percentNumbers[i]) >= percent)
                                {
                                    break;
                                }
                                clearPercent = double.Parse(percentNumbers[i]);
                            }
                            percent = clearPercent;
                            break;
                        }
                    }                  

                    index++;
                }

                txtPercent.Text = string.Format("{0}%", percent);
                if ((oldPercent <= 10 && e.Delta < 0) || (oldPercent >= 400 && e.Delta > 0))
                {
                    //MessageBox.Show("百分比介于10到400之间", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    string text = percent <= 10 ? "10" : "400";                    
                    txtPercent.Text = string.Format("{0}%", text);
                    return;
                }                               

                foreach (DataGridViewRow row in dgvCardModule.Rows)
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
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                SetPageZoom(percent, delta);
            }
        }

        /// <summary>
        /// 获取当前鼠标所在位置的单元格
        /// </summary>
        private void dgvCardModule_MouseMove(object sender, MouseEventArgs e)
        {
            hti = dgvCardModule.HitTest(e.X, e.Y);
            tsslblSplit.Text = string.Format("Mouse Position: X{0},Y{1}", e.X, e.Y);
           
            if (e.Y > -1 && e.Y < 2)
            {
                dgvCardModule.Cursor = new Cursor(startupPath + @"\col.cur");
            }
            else if (e.X < 2 && e.X > -1)
            {
                dgvCardModule.Cursor = new Cursor(startupPath + @"\row.cur");
            }
            else
            {
                dgvCardModule.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        private void tsmnuInsertPicture_Click(object sender, EventArgs e)
        {
            ImportImage();
        }

        /// <summary>
        /// 图片点击事件
        /// </summary>
        void pic_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p != null)
            {
                currPicture = p.Name;
            }
            if (p.Padding.All == 0)
            {
                ClearPictrueBoxBorder(true);
            }
        }

        /// <summary>
        /// 窗体键盘事件
        /// </summary>
        private void dgvCardModule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && currPicture != null)
            {
                Control[] controls = dgvCardModule.Controls.Find(currPicture, false);
                if (controls != null && controls.Length > 0)
                {
                    dgvCardModule.Controls.RemoveByKey(currPicture);
                    valueChanged = true;
                }
            }
            else if (e.Control && e.KeyCode ==  Keys.C)
            {
                CopyCells();
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteCells();
                valueChanged = true;
            }
            else if (e.KeyCode == Keys.F4) //取消合并
            {
                CancelMergeCell();
            }
            else if (e.KeyCode == Keys.F3) //合并
            {
                MergeCell();
            }
        }

        /// <summary>
        /// 重写ProcessCmdKey方法,DataGridView单元格回车换行
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dgvCardModule.CurrentCell != null &&
                dgvCardModule.CurrentCell.IsInEditMode &&
                (keyData == Keys.Enter || keyData == Keys.Return))
            {
                SendKeys.Send("+~");
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// 单元格属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmnuCellProperty_Click(object sender, EventArgs e)
        {
            //CellPropertiesFrm form = new CellPropertiesFrm();
            //form.InvokeEvent += new CellPropertiesFrm.DelegateForm(RefreshCell);
            //form.CellProperties = GetCellProperties();
            //form.ShowDialog();
        }

        /// <summary>
        /// 批量设置单元格属性
        /// </summary>
        private void tsmnuBatchSetProperty_Click(object sender, EventArgs e)
        {
            CellBatchProperties form = new CellBatchProperties();
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (DataGridViewCell cell in dgvCardModule.SelectedCells)
                {
                    dgvCardModule.CurrentCell = cell;
                    RefreshCell(form.BatchCellProperties);
                }
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void tsmnuCopy_Click(object sender, EventArgs e)
        {
            CopyCells();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void tsmnuPaste_Click(object sender, EventArgs e)
        {
            PasteCells();
        }
        
        /// <summary>
        /// 鼠标单击得到当前鼠标位置所在的单元格列或行索引
        /// </summary>
        private void dgvCardModule_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                DataGridView datagridview = (DataGridView)sender;
                GetGridDividerLine(datagridview);

                if (dicColumnLine == null || dicRowLine == null)
                {
                    return;
                }

                #region 选择全行与全列

                int index = 0;
                if (e.X < 2 && e.X > -1)  //选中行
                {
                    foreach (Point pt in dicRowLine.Keys)
                    {
                        if (e.Y <= pt.Y)
                        {
                            break;
                        }
                        index++;
                    }

                    datagridview.ClearSelection();
                    datagridview.Rows[index].Selected = true;
                }
                else if (e.Y > -1 && e.Y < 2)  //选中列
                {
                    index = 0;
                    foreach (Point pt in dicColumnLine.Keys)
                    {
                        if (e.X <= pt.X)
                        {
                            break;
                        }
                        index++;
                    }

                    datagridview.ClearSelection();
                    foreach (DataGridViewRow row in datagridview.Rows)
                    {
                        row.Cells[index].Selected = true;
                    }
                }

                #endregion

                #region 得到当前点击的边框线所在的行或列索引

                listColWidth = new List<int>();
                listRowHeight = new List<int>();
                cellChanged = true;
                foreach (DataGridViewRow row in datagridview.Rows)
                {
                    listRowHeight.Add(row.Height);
                }

                foreach (DataGridViewColumn column in datagridview.Columns)
                {
                    listColWidth.Add(column.Width);
                }

                Point p = new Point(e.X, e.Y);
                clickColumnDivider = 1;
                clickRowDivider = 1;

                bool pointInRow = false;
                bool pointInCol = false;


                //遍历列边线是否包含当前鼠标点击的点
                foreach (Point pt in dicColumnLine.Keys)
                {
                    if (e.X < pt.X + 6 && e.X > pt.X - 6 &&
                        e.Y > pt.Y && e.Y < dicColumnLine[pt].Y)
                    {
                        pointInCol = true;
                        break;
                    }
                    clickColumnDivider++;
                }

                if (pointInCol)
                {
                    return;
                }
                //遍历行边线是否包含当前鼠标点击的点
                foreach (Point pt in dicRowLine.Keys)
                {
                    if (e.Y < pt.Y + 6 && e.Y > pt.Y - 6 &&
                        e.X > pt.X && e.X < dicRowLine[pt].X)
                    {
                        pointInRow = true;
                        break;
                    }

                    clickRowDivider++;
                }

                if (pointInRow)
                {
                    return;
                }

                #endregion                
            }
        }

        /// <summary>
        /// 鼠标点击弹起时，判断当前是否选中了多个单元格
        /// </summary>
        private void dgvCardModule_MouseUp(object sender, MouseEventArgs e)
        {
            if (dgvCardModule.SelectedCells.Count > 1)
            {
                List<DataGridViewCustomerCellStyle> listCellStyle = (List<DataGridViewCustomerCellStyle>)((DataGridViewTextBoxCellEx)dgvCardModule.SelectedCells[0]).CustStyle;
                if (listCellStyle != null && listCellStyle.Count > 0)
                {
                    oldSize = listCellStyle[0].Font.Size;
                }
                List<object> listObjects = new List<object>();
                string cellEditType = ((DataGridViewTextBoxCellEx)dgvCardModule.SelectedCells[0]).CellEditType.ToString();
                foreach (DataGridViewTextBoxCellEx cell in dgvCardModule.SelectedCells)
                {
                    if (cellEditType != cell.CellEditType.ToString())
                    {
                        DelegateForm.propertyForm.SetPropertyGrid(null, false, false);
                        MessageBox.Show("您选中的单元格类型不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                GetLeftTopCell();
                CurrentSelectCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[minValue.Y].Cells[minValue.X];
                DelegateForm.propertyForm.SetPropertyGrid(CurrentSelectCell, true, false);
            }
            else
            {
                if (dgvCardModule.CurrentCell == null)
                {
                    return;
                }
                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.CurrentCell;
                Point p = new Point(cell.SpanCell.X, cell.SpanCell.Y);

                if (p.X >= 0 && p.Y >= 0 && 
                    (p.X != cell.RowIndex ||
                    p.Y != cell.ColumnIndex) &&
                    (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y]).RowSpan > 1 ||
                    ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y]).ColumnSpan > 1))
                {
                    CurrentSelectCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y];
                }
                else
                {
                    CurrentSelectCell = cell;
                }

                if (CurrentSelectCell.CustStyle == null)
                {
                    return;
                }

                List<DataGridViewCustomerCellStyle> listCellStyle = (List<DataGridViewCustomerCellStyle>)CurrentSelectCell.CustStyle;
                if (listCellStyle.Count > 0)
                {
                    oldSize = listCellStyle[0].Font.Size;
                }
                int cellWidth = dgvCardModule.Columns[CurrentSelectCell.ColumnIndex].Width;

                //如果单元格合并，则需要将合并的单元格相加
                int colspan = CurrentSelectCell.ColumnSpan;
                if (colspan > 1)
                {
                    for (int i = 1; i < colspan; i++)
                    {
                        cellWidth += dgvCardModule.Columns[CurrentSelectCell.ColumnIndex + i].Width;
                    }
                }

                DelegateForm.propertyForm.SetPropertyGrid(CurrentSelectCell, false, false);
                CurrentSelectCell.CellWidth = cellWidth;
            }
            if (CurrentSelectCell != null)
            {
                //Rectangle rect = dgvCardModule.GetCellDisplayRectangle(CurrentSelectCell.ColumnIndex, CurrentSelectCell.RowIndex, false);
                ////Graphics g = dgvCardModule.CreateGraphics();

                //Pen p = new Pen(Color.FromArgb(106, 90, 205), 3);
                //int x = rect.X - 1;
                //int y = rect.Y - 1;
                //int width = rect.Width + 1;
                //int height = rect.Height + 1;

                //if (dgvCardModule.SelectedCells.Count > 1)
                //{
                //    height = 1;
                //    width = 1;
                //    foreach (DataGridViewCell cell in dgvCardModule.SelectedCells)
                //    {
                //        if (cell.ColumnIndex == CurrentSelectCell.ColumnIndex)
                //        {
                //            height += dgvCardModule.Rows[cell.RowIndex].Height;
                //        }
                //        if (cell.RowIndex == CurrentSelectCell.RowIndex)
                //        {
                //            width += dgvCardModule.Columns[cell.ColumnIndex].Width;
                //        }
                //    }
                //}

                //Bitmap map = new Bitmap(width, height);
                //Graphics g = Graphics.FromImage(map);                
                //g.DrawRectangle(p, x, y, width, height);
                //g.Dispose();
                //dgvCardModule.CreateGraphics().DrawImageUnscaled(map, new Point(x, y));
            }
        }

        /// <summary>
        /// 单元格编辑完成
        /// </summary>
        private void dgvCardModule_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCardModule.CurrentCell != null)
            {
                string oldValue = dgvCardModule.CurrentCell.Value == null ? string.Empty : dgvCardModule.CurrentCell.Value.ToString();
                if (((DataGridViewTextBoxCellEx)dgvCardModule.CurrentCell).Style.WrapMode == DataGridViewTriState.True)
                {
                    Font font = dgvCardModule.CurrentCell.Style.Font;
                    string value = splitStringByWidth(oldValue, font, new Point(-1,-1));
                    dgvCardModule.CurrentCell.Value = value;
                }
                valueChanged = true;
            }

        }

        /// <summary>
        /// 窗体关闭时校验是否发生变化
        /// </summary>
        private void CardModuleFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //如果是父窗体关闭则提示当前所有子窗体是否有变动？
            if (e.CloseReason == CloseReason.MdiFormClosing && !MainFrm.mainFrm.ClosedWithoutNotice)
            {
                DialogResult result = MessageBox.Show("多个模板内容已更改，是否逐个提示保存？\r\n选择“是”逐个保存，选择“否”直接忽略所有", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == System.Windows.Forms.DialogResult.Yes && valueChanged)
                {
                    SaveTemplate(false, null);
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                MainFrm.mainFrm.ClosedWithoutNotice = true;
                return;
                
            }
            if (valueChanged)
            {
                DialogResult result = MessageBox.Show("模板内容已更改，是否保存？\r\n选择“是”保存，选择“否”直接退出", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    SaveTemplate(false, null);
                }
                else if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
            //窗体关闭时清空属性框
            if (DelegateForm.propertyForm != null)
            {
                DelegateForm.propertyForm.SetPropertyGrid(null, false, false);
            }

        }

        private void dgvCardModule_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //控件重绘
            //dgvCardModule.Invalidate();                      
        }

        private void dgvCardModule_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //控件重绘
            //dgvCardModule.Invalidate();
        }

        private void dgvCardModule_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑
            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.SpanCell.X >= 0 && cell.SpanCell.Y >= 0)
            {
                DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgv.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                if (spanCell.RowSpan > 1 || spanCell.ColumnSpan > 1)
                {
                    if (spanCell.CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "0") ||
                        spanCell.CellContent != (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0"))
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            //只有固定文本框才可以编辑
            if (cell != null &&
                (cell.CellEditType != (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "0") ||
                cell.CellContent != (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0")))
            {
                e.Cancel = true;
            }
        }
        
        /// <summary>
        /// 如果单元格属性为OLE对象，则绘制一张OLE图片
        /// </summary>
        private void dgvCardModule_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                Image oleImage = Image.FromFile(startupPath + @"\oleimg.png"); //Kingdee.CAPP.UI.Properties.Resources.oleobj;
                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "1") ||
                    cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "2"))
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                    Rectangle rect = new Rectangle(e.CellBounds.X, e.CellBounds.Y, oleImage.Width, oleImage.Height);
                    e.Graphics.DrawImage(oleImage, rect);
                    e.Handled = true;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：初始化单元格
        /// 作    者：jason.tang
        /// 完成时间：2012-12-17
        /// </summary>
        /// <param name="listRows">行数</param>
        /// <param name="listColumns">列数</param>
        /// <param name="isInit">是否新建的初始化</param>
        private void InitDataGridView(List<int> listRows, List<int> listColumns, bool isInit)
        {
            int index = 0;
            Initialize = true;

            dgvCardModule.Rows.Clear();
            dgvCardModule.Columns.Clear();

            //增加列
            DataGridViewTextBoxColumnEx column = new DataGridViewTextBoxColumnEx();
            
            listColWidth = new List<int>();
            listRowHeight = new List<int>();

            foreach (int i in listColumns)
            {
                column = new DataGridViewTextBoxColumnEx();
                column.Width = i;
                if (index == listColumns.Count - 1)
                {
                    column.Width = i - 3;
                }
                dgvCardModule.Columns.Insert(index, column);
                listColWidth.Add(column.Width);
                index++;
            }

            index = 0;
            //增加行
            foreach (int i in listRows)
            {
                dgvCardModule.Rows.Insert(index, 1);
                index++;
            }

            //行高设定
            index = 0;
            foreach (DataGridViewRow row in dgvCardModule.Rows)
            {
                if (index < listRows.Count)
                {
                    row.Height = listRows[index];
                }
                if (index == dgvCardModule.Rows.Count - 1)
                {
                    row.Height = row.Height - 3;
                }
                listRowHeight.Add(row.Height);
                index++;
            }

            if (isInit)
            {
                float pageSize = (float)_breadth;
                float size = _breadth < 4 ? 8f - _breadth * 1.5f : 4f - _breadth;
                Font defaultFont = this.Font;
                int columnCount = dgvCardModule.Columns.Count;

                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    foreach (DataGridViewColumn col in dgvCardModule.Columns)
                    {
                        
                        row.Cells[col.Index].Style.Font = new Font(defaultFont.FontFamily, defaultFont.Size - size, defaultFont.Style);
                        row.Cells[col.Index].Style.WrapMode = DataGridViewTriState.True;
                        
                        List<DataGridViewCustomerCellStyle> listCellStyle = new List<DataGridViewCustomerCellStyle>();
                        DataGridViewCustomerCellStyle cellStyle = new DataGridViewCustomerCellStyle();
                        cellStyle.Font = defaultFont;
                        cellStyle.WrapMode = DataGridViewTriState.True;
                        listCellStyle.Add(cellStyle);
                        ((DataGridViewTextBoxCellEx)row.Cells[col.Index]).CustStyle = listCellStyle;
                        ((DataGridViewTextBoxCellEx)row.Cells[col.Index]).SpanCell = new Point(-1, -1);
                        int cellTag = columnCount * row.Index + col.Index + 1;
                        ((DataGridViewTextBoxCellEx)row.Cells[col.Index]).CellTag = string.Format("Cell{0}", cellTag);
                    }
                }
            }
            Initialize = false;
        }

        /// <summary>
        /// 设置右键菜单项是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void SetMenuEnable(bool enabled)
        {
            foreach (ToolStripItem item in cmnuDataGridview.Items)
            {
                if (item.Name == "tsmnuInit")
                {
                    item.Enabled = enabled;
                }
                else
                {
                    item.Enabled = !enabled;
                }
            }
        }

        /// <summary>
        /// 方法说明：设置单元格的属性
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        /// <param name="obj"></param>
        void SetCellProperty(object obj)
        {
            if (obj == null)
            {
                if (dgvCardModule.Controls.ContainsKey(CurrentGridName))
                {
                    ((DataGridView)dgvCardModule.Controls.Find(CurrentGridName, false)[0]).Rows.Clear();
                }
                return;
            }

            try
            {
                //单元格类型
                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)obj;
                //string cellType = cell.CellEditType.ToString();  
                int currRowIndex = CurrentSelectCell.RowIndex;
                int currColIndex = CurrentSelectCell.ColumnIndex;

                if (dgvCardModule.Rows.Count == 0 || dgvCardModule.Columns.Count == 0)
                {
                    return;
                }

                Rectangle rect = dgvCardModule.GetCellDisplayRectangle(currColIndex, currRowIndex, false);
                int top = rect.Y - 1;
                int left = rect.X - 1;

                SetCellBorder(cell);

                object objDetailProperty = cell.DetailProperty;
                //明细框
                if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                {
                    AddDetailGridView(top, left, objDetailProperty, null);
                }
                else if (CurrentGridName != null)
                {
                    string rowIndex = CurrentGridName.Substring(3, CurrentGridName.IndexOf('-') - 3);
                    string colIndex = CurrentGridName.Substring(CurrentGridName.IndexOf('-') + 1);
                    if (rowIndex == currRowIndex.ToString() &&
                        colIndex == currColIndex.ToString())
                    {
                        dgvCardModule.Controls.RemoveByKey(CurrentGridName);
                    }
                    ((DataGridViewTextBoxCellEx)obj).DetailProperty = null;
                }

                //OLE对象
                if (cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "2"))
                {
                    AddOleImage(top, left);
                }
                
                dgvCardModule.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 方法说明：重写基类的方法，有属性界面委托Invoke
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        /// <param name="obj"></param>
        public override void SetPropertyEvent(object obj)
        {
            if (obj == null)
            {
                return;
            }

            if (obj.GetType() != typeof(DataGridViewTextBoxCellEx))
                return;
            CurrentSelectCell = (DataGridViewTextBoxCellEx)obj;

            try
            {
                int merge = CurrentSelectCell.Merge;
                if (merge == 1)
                {
                    MergeCell();
                    CurrentSelectCell.Merge = 0;
                }
                else if (merge == -1 )
                {
                    if (CurrentSelectCell.RowSpan == 1 && CurrentSelectCell.ColumnSpan == 1)
                    {
                        return;
                    }
                    CancelMergeCell();
                    CurrentSelectCell.Merge = 0;
                }
                else
                {
                    float fontSize = 0;
                    List<DataGridViewCustomerCellStyle> listCellStyle = new List<DataGridViewCustomerCellStyle>();
                    if (CurrentSelectCell.CustStyle != null)
                    {
                        listCellStyle = (List<DataGridViewCustomerCellStyle>)CurrentSelectCell.CustStyle;
                    }
                    if (dgvCardModule.SelectedCells.Count > 1)
                    {                       
                        foreach (DataGridViewTextBoxCellEx cell in dgvCardModule.SelectedCells)
                        {
                            oldSize = cell.Style.Font.Size;                                                  

                            if (listCellStyle.Count > 0)
                            {
                                fontSize = listCellStyle[0].Font.Size - oldSize;

                                cell.CustStyle = listCellStyle;
                                if (CurrentSelectCell.Style.Font != null)
                                {
                                    if (fontSize != 0)
                                    {
                                        cell.Style.Font = new Font(listCellStyle[0].Font.FontFamily, CurrentSelectCell.Style.Font.Size + fontSize, listCellStyle[0].Font.Style);                                        
                                    }
                                    else
                                    {
                                        cell.Style.Font = new Font(listCellStyle[0].Font.FontFamily, CurrentSelectCell.Style.Font.Size, listCellStyle[0].Font.Style);                                        
                                    }
                                }
                                listCellStyle = (List<DataGridViewCustomerCellStyle>)CurrentSelectCell.CustStyle;
                                if (listCellStyle.Count > 0)
                                {
                                    cell.Style.Alignment = listCellStyle[0].Alignment;
                                    cell.Style.ForeColor = listCellStyle[0].ForeColor;
                                    cell.Style.Padding = listCellStyle[0].Padding;
                                    cell.Style.WrapMode = listCellStyle[0].WrapMode;
                                    cell.Style.BackColor = listCellStyle[0].BackColor;
                                }
                            }

                            cell.CellEditType = CurrentSelectCell.CellEditType;
                        }
                    }
                    else
                    {
                        if (listCellStyle.Count > 0)
                        {
                            fontSize = listCellStyle[0].Font.Size - oldSize;

                            if (CurrentSelectCell.Style.Font != null)
                            {
                                if (fontSize != 0)
                                {
                                    CurrentSelectCell.Style.Font = new Font(listCellStyle[0].Font.FontFamily, CurrentSelectCell.Style.Font.Size + fontSize, listCellStyle[0].Font.Style);
                                }
                                else
                                {
                                    CurrentSelectCell.Style.Font = new Font(listCellStyle[0].Font.FontFamily, CurrentSelectCell.Style.Font.Size, listCellStyle[0].Font.Style);
                                }
                            }

                            listCellStyle = (List<DataGridViewCustomerCellStyle>)CurrentSelectCell.CustStyle;
                            if (listCellStyle.Count > 0)
                            {
                                CurrentSelectCell.Style.Alignment = listCellStyle[0].Alignment;
                                CurrentSelectCell.Style.ForeColor = listCellStyle[0].ForeColor;
                                CurrentSelectCell.Style.Padding = listCellStyle[0].Padding;
                                CurrentSelectCell.Style.WrapMode = listCellStyle[0].WrapMode;
                                oldSize = listCellStyle[0].Font.Size;
                                CurrentSelectCell.Style.BackColor = listCellStyle[0].BackColor;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            SetCellProperty(obj);
        }

        /// <summary>
        /// 刷新当前单元格
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
                //是否多行
                bool isWrap = (bool)dicFormat["Wrap"];
                if (dicFormat.ContainsKey("Value"))
                {
                    string cellValue = dicFormat["Value"].ToString();
                    dgvCardModule.CurrentCell.Value = cellValue;
                }
                //样式
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.Font = font;
                style.WrapMode = DataGridViewTriState.False;
                if (isWrap)
                {
                    style.WrapMode = DataGridViewTriState.True;
                }
                //排列
                string alignment = dicFormat["Alignment"].ToString();
                style.Alignment = (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), alignment);
                //边距
                List<int> listPadding = (List<int>)dicFormat["Padding"];
                style.Padding = new Padding(listPadding[0], listPadding[1], listPadding[2], listPadding[3]);

                //单元格类型
                string cellType = dicFormat["CellType"].ToString();
                if (cellType != "-1")  //不改变类型时不赋值
                {
                    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellEditType = (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), cellType);
                }
                Rectangle rect = dgvCardModule.GetCellDisplayRectangle(dgvCardModule.CurrentCell.ColumnIndex, dgvCardModule.CurrentCell.RowIndex, false);
                int top = rect.Y - 1;
                int left = rect.X - 1;

                List<DataGridViewTextBoxColumn> listColumns = null;
                if (dicFormat.ContainsKey("Columns"))
                {
                    listColumns = (List<DataGridViewTextBoxColumn>)dicFormat["Columns"];
                }
                //明细框
                if (cellType == "2")
                {
                    AddDetailGridView(top, left, listColumns, null);
                }
                else if(CurrentGridName != null)
                {
                    string rowIndex = CurrentGridName.Substring(3, CurrentGridName.IndexOf('-') - 3);
                    string colIndex = CurrentGridName.Substring(CurrentGridName.IndexOf('-') + 1);
                    if (rowIndex == dgvCardModule.CurrentCell.RowIndex.ToString() &&
                        colIndex == dgvCardModule.CurrentCell.ColumnIndex.ToString())
                    {
                        dgvCardModule.Controls.RemoveByKey(CurrentGridName);
                    }
                }

                //对角线
                int leftTop = int.Parse(dicFormat["DiagonalLeftTop"].ToString());
                int leftBottom = int.Parse(dicFormat["DiagonalLeftBottom"].ToString());
                int currRowIndex = dgvCardModule.CurrentCell.RowIndex;
                int currColumnIndex = dgvCardModule.CurrentCell.ColumnIndex;

                (dgvCardModule.Rows[currRowIndex].Cells[currColumnIndex] as DataGridViewTextBoxCellEx).LeftTopRightBottom = leftTop;
                (dgvCardModule.Rows[currRowIndex].Cells[currColumnIndex] as DataGridViewTextBoxCellEx).LeftBottomRightTop = leftBottom;

                dgvCardModule.CurrentCell.Style = style;
                dgvCardModule.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 方法说明：增加明细框Grid
        /// 作者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        /// <param name="top">上边距</param>
        /// <param name="left">下边距</param>
        /// <param name="objColumns">明细列集合</param>
        private void AddDetailGridView(int top, int left, object objColumns, List<DetailGridViewTextBoxColumn> _dicColumns)
        {
            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.White;

            #region 明细框位置设定

            dgv.Top = top;
            dgv.Left = left;

            CheckIsInSpanCells(dgv);

            dgv.Name = string.Format("dgv{0}-{1}", CurrentSelectCell.RowIndex.ToString(), CurrentSelectCell.ColumnIndex.ToString());

            List<DetailGridViewTextBoxColumn> listColumns = new List<DetailGridViewTextBoxColumn>();
            if (objColumns != null)
            {
                List<DetailGridViewTextBoxColumn> dicColumns = objColumns as List<DetailGridViewTextBoxColumn>;
                DetailGridViewTextBoxColumn textBoxColumn;
                DetailCell[] detailCells = new DetailCell[] { };
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
                        textBoxColumn.SpaceRows = CurrentSelectCell.SpaceRows.ToString(); //cell.SpaceRows.ToString();
                        textBoxColumn.Tag = cell.Tag;
                        textBoxColumn.Type = string.IsNullOrEmpty(cell.Type) ? (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), "0") :
                            (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), cell.Type);
                        textBoxColumn.Visible = cell.AdvanceProperty != ((ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "3")).ToString();
                        textBoxColumn.AdvanceProperty = !string.IsNullOrEmpty(cell.AdvanceProperty) ? (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), cell.AdvanceProperty) :
                            (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "0");
                        textBoxColumn.SerialStep = CurrentSelectCell.SerialStep; //cell.SerialStep;
                        listColumns.Add(textBoxColumn);
                        if (_dicColumns != null && !_dicColumns.Contains(textBoxColumn))
                        {
                            _dicColumns.Add(textBoxColumn);
                        }
                    }
                }
                else if (dicColumns != null)
                {
                    foreach (DetailGridViewTextBoxColumn col in dicColumns)
                    {
                        col.SerialStep = CurrentSelectCell.SerialStep;
                        col.SpaceRows = CurrentSelectCell.SpaceRows.ToString();
                        listColumns.Add(col);
                    }
                }

            }

            if (dgvCardModule.Controls.ContainsKey(dgv.Name) && objColumns != null)
            {
                SetDataGridViewColumns(listColumns, dgv.Name);
                return;
            }

            CurrentGridName = dgv.Name;
            dgv.Width = dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex].Width;
            dgv.Height = dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex].Height;

            #endregion

            #region 明细框：处理合并的单元格

            int colspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan;
            int rowspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan;

            if (colspan > 1)
            {
                for (int i = 1; i < colspan; i++)
                {
                    dgv.Width += dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex + i].Width;
                }
            }
            if (rowspan > 1)
            {
                for (int i = 1; i < rowspan; i++)
                {
                    dgv.Height += dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex + i].Height;
                }
            }

            #endregion

            #region 明细框列表属性配置

            Control[] controls = dgvCardModule.Controls.Find(dgv.Name, false);
            if (controls.Length > 0)
            {
                dgv = (DataGridView)controls[0];
                dgv.Rows.Clear();
                dgv.Columns.Clear();
            }


            dgv.Columns.Add("Column1", "");
            dgv.Columns.Add("Column2", "");
            //dgv.ColumnHeadersVisible = false;
            dgv.Rows[0].Height = dgv.Height;
            dgv.Columns[0].Width = 25;
            dgv.Columns[1].Width = dgv.Width - 25;
            dgv.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            dgv.BorderStyle = BorderStyle.None;
            //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.ReadOnly = true;
            dgv.ScrollBars = ScrollBars.None;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv.CellDoubleClick += dgvCardModule_CellDoubleClick;
            dgv.CellClick += dgvCardModule_CellClick;
            dgv.MouseUp += dgvCardModule_MouseUp;
            dgv.MouseDown += dgvCardModule_MouseDown;            
            dgvCardModule.Controls.Add(dgv);
            if (listColumns.Count > 0)
            {
                dgv.ColumnWidthChanged += dgv_ColumnWidthChanged;
                dgv.RowHeightChanged += dgv_RowHeightChanged;
                SetDataGridViewColumns(listColumns, dgv.Name);
            }
            else
            {
                dgv.ColumnWidthChanged -= dgv_ColumnWidthChanged;
                dgv.RowHeightChanged -= dgv_RowHeightChanged;
            }

            #endregion
        }

        private void AddOleImage(int top, int left)
        {
            //PictureBox pic = new PictureBox();
            //pic.Left = left;
            //pic.Top = top;
            //pic.SizeMode = PictureBoxSizeMode.Normal;
            //pic.Image = Image.FromFile(@"C:\Users\jason.tang\Desktop\thum.png");
            //pic.Width = dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex].Width;
            //pic.Height = dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex].Height;

            //int colspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan;
            //int rowspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan;

            //if (colspan > 1)
            //{
            //    for (int i = 1; i < colspan; i++)
            //    {
            //        pic.Width += dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex + i].Width;
            //    }
            //}
            //if (rowspan > 1)
            //{
            //    for (int i = 1; i < rowspan; i++)
            //    {
            //        pic.Height += dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex + i].Height;
            //    }
            //}

            //dgvCardModule.Controls.Add(pic);
        }

        /// <summary>
        /// 明细框行高变动
        /// </summary>
        void dgv_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
        {
            ResizeDetail(true, sender);
        }

        /// <summary>
        /// 明细框列宽变动
        /// </summary>
        void dgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            ResizeDetail(false, sender);            
        }

        /// <summary>
        /// 方法说明：动态调整列宽和行高
        /// 作    者：jason.tang
        /// 完成时间：2013-03-27
        /// </summary>
        /// <param name="rowOrColumn">调整行或列</param>
        private void ResizeDetail(bool rowOrColumn, object sender)
        {
            int index = 0;
            DataGridView datagridview = (DataGridView)sender;

            #region 边线拖动，改变行高与列宽

            ////拖动行边线时，只改变边线上下两边的单元格高度
            //if (rowOrColumn)
            //{
            //    if (listRowHeight == null || listRowHeight.Count < datagridview.Rows.Count)
            //    {
            //        return;
            //    }

            //    //防止最后一行拉动
            //    if (clickRowDivider == datagridview.Rows.Count)
            //    {
            //        int lastRowHeight = datagridview.Rows[clickRowDivider - 1].Height;
            //        if (lastRowHeight != listRowHeight[clickRowDivider - 1])
            //        {
            //            datagridview.Rows[clickRowDivider - 1].Height = listRowHeight[clickRowDivider - 1];
            //            return;
            //        }
            //    }

            //    foreach (DataGridViewRow dgr in datagridview.Rows)
            //    {
            //        if (dgr.Index == clickRowDivider && index > 0)
            //        {
            //            if (listRowHeight[index] + listRowHeight[index - 1] - datagridview.Rows[index - 1].Height < 6)
            //            {
            //                datagridview.Rows[index - 1].Height = listRowHeight[index] + listRowHeight[index - 1] - 6;
            //                dgr.Height = 6;
            //            }
            //            else
            //            {
            //                dgr.Height = listRowHeight[index] + listRowHeight[index - 1] - datagridview.Rows[index - 1].Height;
            //            }
            //        }
            //        else if (dgr.Index != clickRowDivider - 1)
            //        {
            //            //if (index == datagridview.Rows.Count - 1 && preRowSpan)
            //            //{
            //            //    dgr.Height = listRowHeight[index] + listRowHeight[index + 1];
            //            //}
            //            //else
            //            //{
            //                dgr.Height = listRowHeight[index];
            //            //}
            //        }

            //        index++;
            //    }
            //}
            //else    //拖动列边线时,只改变边线左右两边的单元格宽度
            //{
            //    index = 0;
            //    if (listColWidth == null || listColWidth.Count < datagridview.Columns.Count)
            //    {
            //        return;
            //    }
            //    //防止最后一列拉动
            //    if (clickColumnDivider == datagridview.Columns.Count)
            //    {
            //        int lastColumnWidth = datagridview.Columns[clickColumnDivider - 1].Width;
            //        if (lastColumnWidth != listColWidth[clickColumnDivider - 1])
            //        {
            //            datagridview.Columns[clickColumnDivider - 1].Width = listColWidth[clickColumnDivider - 1];
            //            return;
            //        }
            //    }

            //    foreach (DataGridViewColumn column in datagridview.Columns)
            //    {
            //        if (column.Index == clickColumnDivider && index > 0)
            //        {
            //            if (listColWidth[index] + listColWidth[index - 1] - datagridview.Columns[index - 1].Width < 6)
            //            {
            //                datagridview.Columns[index - 1].Width = listColWidth[index] + listColWidth[index - 1] - 6;
            //                column.Width = 6;
            //            }
            //            else
            //            {
            //                column.Width = listColWidth[index] + listColWidth[index - 1] - datagridview.Columns[index - 1].Width;
            //            }
            //        }
            //        else if (column.Index != clickColumnDivider - 1)
            //        {
            //            //if (index == datagridview.Columns.Count - 1 && preColumnSpan)
            //            //{
            //            //    column.Width = listColWidth[index] + listColWidth[index + 1];
            //            //}
            //            //else
            //            //{
            //                column.Width = listColWidth[index];
            //            //}
            //        }

            //        index++;
            //    }
            //}

            #endregion

            GetGridDividerLine(datagridview);
            datagridview.Invalidate();
        }

        /// <summary>
        /// 方法说明：设定明细框列
        /// 作   者：jason.tang
        /// 完成时间：2013-01-05
        /// <param name="listColumns">明细列集合</param>
        /// <param name="gridName">明细Grid名称</param>
        private void SetDataGridViewColumns(List<DetailGridViewTextBoxColumn> listColumns, string gridName)
        {
            Control[] controls = dgvCardModule.Controls.Find(gridName, false);

            if (controls.Length == 0 || listColumns == null)
            {
                return;
            }

            DataGridView dgv = (DataGridView)controls[0];
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            int totalWidth = 0;
            int totalRows = 1;
            string tag = string.Empty;
            int displayIndex = 0;
            foreach (DetailGridViewTextBoxColumn column in listColumns)
            {
                //if (column.Tag != null && !string.IsNullOrEmpty(column.Tag.ToString()))
                //{
                //    tag = column.Tag.ToString();
                //}
                column.DisplayIndex = displayIndex;
                column.Visible = column.AdvanceProperty != ((ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "3"));
                dgv.Columns.Add(column);
                totalWidth += column.Width;
                displayIndex++;
            }

            if (totalWidth < dgv.Width)
            {
                dgv.Columns[dgv.Columns.Count - 1].Width += dgv.Width - totalWidth;
            }

            ////明细列扩展属性Tag
            //if (!string.IsNullOrEmpty(tag))
            //{
            //    string[] tags = tag.Split(new char[] { ',' });
            //    totalRows = int.Parse(tags[4]);
            //    for (int i = 1; i < totalRows; i++)
            //    {
            //        dgv.Rows.Add();
            //    }
            //}
            if (listColumns != null && listColumns.Count > 0)
            {
                DetailGridViewTextBoxColumn columns = (DetailGridViewTextBoxColumn)listColumns[0];
                totalRows = int.Parse(columns.Rows);
                for (int i = 1; i < totalRows; i++)
                {
                    dgv.Rows.Add();
                }
            }

            //总宽度不足，补充到最后一列
            int totalHeight = dgv.Height - 20;
            for (int i = 0; i < totalRows; i++)
            {
                dgv.Rows[i].Height = totalHeight / totalRows;
                if (i == totalRows - 1)
                {
                    dgv.Rows[i].Height = totalHeight / totalRows + totalHeight % totalRows;
                }
                //dgv.Rows[i].DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 228, 242);
            }
        }

        /// <summary>
        /// 方法说明：设置界面的百分比
        /// 作    者：jason.tang
        /// 完成时间：2013-01-14
        /// </summary>
        private void SetPercent()
        {
            double percent = 0;            
            bool isDigit = double.TryParse(txtPercent.Text.Replace("%", ""), out percent);
            string oldPercent = this.TabText.Substring(this.TabText.IndexOf("@") + 2, this.TabText.IndexOf("%") - this.TabText.IndexOf("@") - 2);
            int delta = double.Parse(oldPercent) > percent ? -1 : 1;
            if (!isDigit)
            {
                MessageBox.Show("请检查百分比是否为空且必须为数字", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (percent < 10 || percent > 400)
            {
                MessageBox.Show("百分比介于10到400之间", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!txtPercent.Text.EndsWith("%"))
            {
                txtPercent.Text = txtPercent.Text + "%";
            }
            SetPageZoom(percent, delta);
        }

        /// <summary>
        /// 方法说明：设定初始尺寸
        /// 作   者：jason.tang
        /// 完成时间：2012-12-18
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="_percent">百分比</param>
        private void ResizeInitGridView(int width, int height, double _percent)
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
                    diff = diff * 0;
                    percent = 0.0625;
                    break;
            }

            if (_percent > 0)
            {
                percent = _percent;
            }

            int widPixel = MillimetersToPixelsWidth(width, true);
            int heightPixel = MillimetersToPixelsWidth(height, false);

            //边距
            pnModule.Padding = new System.Windows.Forms.Padding(_padleft + diff, _padtop + diff, _padright + diff, _padbottom + diff);

            pnModule.Width = (int)Math.Round(widPixel * percent) + _padleft + _padright + diff * 2;
            pnModule.Height = (int)Math.Round(heightPixel * percent) + _padtop + _padbottom + diff * 2;
            
            txtPercent.Text = string.Format("{0}%", percent * 100);
            string tabText = this.TabText;
            if (this.TabText.Contains("@"))
            {
                tabText = tabText.Substring(0, tabText.IndexOf("@") - 1);
            }
            this.TabText = string.Format(tabText + " @ {0}%", percent * 100);
            tsslblPageSize.Text = string.Format("{0}毫米x{1}毫米(A{2})", width, height, _breadth);

            //dgvCardModule.Width = widPixel;
            //dgvCardModule.Height = heightPixel;
            
            //tsmnuInit_Click(tsmnuInit, null);
        }

        /// <summary>
        /// 方法说明：根据毫米数得到像素
        /// 作    者：jason.tang
        /// 完成时间：2013-01-14
        /// </summary>
        /// <param name="length">长度(mm)</param>
        /// <param name="widthOrHeight">宽或者高</param>
        /// <returns></returns>
        private int MillimetersToPixelsWidth(int length, bool widthOrHeight)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(pnModule.Handle);
            int dpiX = (int)Math.Round(g.DpiX);
            int dpiY = (int)Math.Round(g.DpiY);

            //int px = GetDeviceCaps(pnModule.Handle, dpiX);
            //int py = GetDeviceCaps(pnModule.Handle, dpiY);
            //pixel = (int)Math.Round(length * px / 25.4);

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
        /// 方法说明：获取单元格属性
        /// 作   者：jason.tang
        /// 完成时间：2012-12-19
        /// </summary>
        /// <returns>属性集合</returns>
        private Dictionary<string, object> GetCellProperties()
        {
            Dictionary<string, object> dicProperties = new Dictionary<string, object>();

            int cellWidth = dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex].Width;
            int cellHeight = dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex].Height;

            //如果单元格合并，则需要将合并的单元格相加
            int colspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan;
            int rowspan = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan;

            if (colspan > 1)
            {
                for (int i = 1; i < colspan; i++)
                {
                    cellWidth += dgvCardModule.Columns[dgvCardModule.CurrentCell.ColumnIndex + i].Width;
                }
            }
            if (rowspan > 1)
            {
                for (int i = 1; i < rowspan; i++)
                {
                    cellHeight += dgvCardModule.Rows[dgvCardModule.CurrentCell.RowIndex + i].Height;
                }
            }
            int cellType = (int)(dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).CellEditType;
            dicProperties.Add("Width", cellWidth);
            dicProperties.Add("Height", cellHeight);
            dicProperties.Add("Value", dgvCardModule.CurrentCell.Value);
            dicProperties.Add("Font", dgvCardModule.CurrentCell.Style.Font);
            dicProperties.Add("Wrap", dgvCardModule.CurrentCell.Style.WrapMode);
            dicProperties.Add("Alignment", dgvCardModule.CurrentCell.Style.Alignment);

            List<int> listPadding = new List<int>();
            listPadding.Add(dgvCardModule.CurrentCell.Style.Padding.Left);
            listPadding.Add(dgvCardModule.CurrentCell.Style.Padding.Top);
            listPadding.Add(dgvCardModule.CurrentCell.Style.Padding.Right);
            listPadding.Add(dgvCardModule.CurrentCell.Style.Padding.Bottom);
            dicProperties.Add("Padding", listPadding);

            //单元格类型
            dicProperties.Add("CellType", cellType);

            //明细框列属性
            if (cellType == 2)
            {
                if (dgvCardModule.Controls.ContainsKey(CurrentGridName))
                {
                    List<DataGridViewTextBoxColumn> listColumns = new List<DataGridViewTextBoxColumn>();
                    Control[] controls = dgvCardModule.Controls.Find(CurrentGridName, false);
                    if (controls.Length > 0)
                    {
                        DataGridView datagrid = (DataGridView)controls[0];
                        foreach (DataGridViewTextBoxColumn column in datagrid.Columns)
                        {
                            listColumns.Add(column);
                        }
                        dicProperties.Add("Columns", listColumns);
                    }
                }
            }

            return dicProperties;
        }

        /// <summary>
        /// 方法说明：设置单元格边框
        /// 作    者：jason.tang
        /// 完成时间：2012-12-27
        /// </summary>
        /// <param name="borderAll">是否全边框</param>
        /// <param name="borderLeft">左边框</param>
        /// <param name="borderTop">上边框</param>
        /// <param name="borderRight">右边框</param>
        /// <param name="borderBottom">下边框</param>
        private void SetCellBorder(DataGridViewTextBoxCellEx cell)
        {
            int rowIndex = cell.RowIndex;
            int colIndex = cell.ColumnIndex;

            bool isSpanCell = false;
            int rowSpan = 1;
            int colSpan = 1;

            Point p = cell.SpanCell;
            if (p.X >= 0 && p.Y >= 0)
            {
                DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y];
                if (cellEx.RowSpan > 1 || cellEx.ColumnSpan > 1)
                {
                    isSpanCell = true;
                    rowIndex = p.X;
                    colIndex = p.Y;
                    rowSpan = cellEx.RowSpan;
                    colSpan = cellEx.ColumnSpan;                    
                }
                else
                {
                    isSpanCell = false;
                }
            }

            if (isSpanCell)
            {

                //左
                if (colIndex - 1 >= 0)
                {
                    Color color = Color.Empty;
                    if (cell.LeftBorderWidth > 0)
                    {
                        color = cell.LeftBorderColor;
                    }
                    for (int i = 0; i < rowSpan; i++)
                    {
                        DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + i].Cells[colIndex - 1];
                        cellEx.RightBorderColor = color;
                        cellEx.RightBorderWidth = cell.LeftBorderWidth;
                        cellEx.RightBorderStyle = cell.LeftBorderStyle;
                    }
                }

                //上
                if (rowIndex - 1 >= 0)
                {
                    Color color = Color.Empty;
                    if (cell.TopBorderWidth > 0)
                    {
                        color = cell.TopBorderColor;
                    }
                    for (int i = 0; i < colSpan; i++)
                    {
                        DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex - 1].Cells[colIndex + i];
                        cellEx.BottomBorderColor = color;
                        cellEx.BottomBorderWidth = cell.TopBorderWidth;
                        cellEx.BottomBorderStyle = cell.TopBorderStyle;
                    }
                }

                //右
                if (colIndex + 1 < dgvCardModule.Columns.Count)
                {
                    Color color = Color.Empty;
                    if (cell.RightBorderWidth > 0)
                    {
                        color = cell.RightBorderColor;
                    }
                    for (int i = 0; i < rowSpan; i++)
                    {
                        if (colIndex + colSpan < dgvCardModule.Columns.Count)
                        {
                            DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + i].Cells[colIndex + colSpan];
                            cellEx.LeftBorderColor = color;
                            cellEx.LeftBorderWidth = cell.RightBorderWidth;
                            cellEx.LeftBorderStyle = cell.RightBorderStyle;
                            cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + i].Cells[colIndex + colSpan - 1];
                            cellEx.RightBorderColor = color;
                            cellEx.RightBorderWidth = cell.RightBorderWidth;
                            cellEx.RightBorderStyle = cell.RightBorderStyle;
                        }
                    }
                }
                //下
                if (rowIndex + 1 < dgvCardModule.Rows.Count)
                {
                    Color color = Color.Empty;
                    if (cell.BottomBorderWidth > 0)
                    {
                        color = cell.BottomBorderColor;
                    }
                    for (int i = 0; i < colSpan; i++)
                    {
                        if (rowIndex + rowSpan < dgvCardModule.Rows.Count)
                        {
                            DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + rowSpan].Cells[colIndex + i];
                            cellEx.TopBorderColor = color;
                            cellEx.TopBorderWidth = cell.BottomBorderWidth;
                            cellEx.TopBorderStyle = cell.BottomBorderStyle;
                            cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + rowSpan - 1].Cells[colIndex + i];
                            cellEx.BottomBorderColor = color;
                            cellEx.BottomBorderWidth = cell.BottomBorderWidth;
                            cellEx.BottomBorderStyle = cell.BottomBorderStyle;
                        }
                    }
                }                
            }
            else
            {
                //左
                if (colIndex - 1 >= 0)
                {
                    Color color = Color.Empty;
                    if (cell.LeftBorderWidth > 0)
                    {
                        color = cell.LeftBorderColor;
                    }

                    DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex].Cells[colIndex - 1];
                    cellEx.RightBorderColor = color;
                    cellEx.RightBorderWidth = cell.LeftBorderWidth;
                    cellEx.RightBorderStyle = cell.LeftBorderStyle;

                    if (cellEx.SpanCell.X >= 0 && cellEx.SpanCell.Y >= 0)
                    {
                        DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cellEx.SpanCell.X].Cells[cellEx.SpanCell.Y];
                        if (spanCell.ColumnSpan > 1)
                        {
                            spanCell.RightBorderColor = color;
                            spanCell.RightBorderWidth = cell.LeftBorderWidth;
                            spanCell.RightBorderStyle = cell.LeftBorderStyle;
                        }
                    }
                }

                //上
                if (rowIndex - 1 >= 0)
                {
                    Color color = Color.Empty;
                    if (cell.TopBorderWidth > 0)
                    {
                        color = cell.TopBorderColor;
                    }
                    DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex - 1].Cells[colIndex];
                    cellEx.BottomBorderColor = color;
                    cellEx.BottomBorderWidth = cell.TopBorderWidth;
                    cellEx.BottomBorderStyle = cell.TopBorderStyle;

                    if (cellEx.SpanCell.X >= 0 && cellEx.SpanCell.Y >= 0)
                    {                        
                        DataGridViewTextBoxCellEx spanCell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cellEx.SpanCell.X].Cells[cellEx.SpanCell.Y];
                        if (spanCell.RowSpan > 1)
                        {
                            spanCell.BottomBorderColor = color;
                            spanCell.BottomBorderWidth = cell.TopBorderWidth;
                            spanCell.BottomBorderStyle = cell.TopBorderStyle;
                        }
                    }
                }

                //右
                if (colIndex + 1 < dgvCardModule.Columns.Count)
                {
                    Color color = Color.Empty;
                    if (cell.RightBorderWidth > 0)
                    {
                        color = cell.RightBorderColor;
                    }
                    DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex].Cells[colIndex + 1];
                    cellEx.LeftBorderColor = color;
                    cellEx.LeftBorderWidth = cell.RightBorderWidth;
                    cellEx.LeftBorderStyle = cell.RightBorderStyle;                    
                }
                //else
                //{
                //    cell.RightBorderWidth = 0;
                //}

                //下
                if (rowIndex + 1 < dgvCardModule.Rows.Count)
                {
                    Color color = Color.Empty;
                    if (cell.BottomBorderWidth > 0)
                    {
                        color = cell.BottomBorderColor;
                    }
                    DataGridViewTextBoxCellEx cellEx = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + 1].Cells[colIndex];
                    cellEx.TopBorderColor = color;
                    cellEx.TopBorderWidth = cell.BottomBorderWidth;
                    cellEx.TopBorderStyle = cell.BottomBorderStyle;
                }
                //else
                //{
                //    cell.BottomBorderWidth = 0;
                //}
            }

            dgvCardModule.Invalidate();
        }

        /// <summary>
        /// 方法说明：动态调整列宽和行高时，相应调整明细框的尺寸
        /// 作    者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        /// <param name="rowOrColumn">调整行或列</param>
        /// <param name="addRowOrColumn">0-没有发生插入 1-插入行 2-插入列</param>
        /// <param name="currIndex">当前要插入的单元格索引</param>
        private void ResizeModuleGrid(bool rowOrColumn, int addRowOrColumn, int currIndex)
        {
            string row;
            string col;

            int rowIndex = 0;
            int colIndex = 0;
            int value = 0;
            int index = 0;
            cellChanged = false;

            #region 边线拖动，改变行高与列宽

            //拖动行边线时，只改变边线上下两边的单元格高度
            if (rowOrColumn)
            {
                if (listRowHeight == null || listRowHeight.Count < dgvCardModule.Rows.Count)
                {
                    return;
                }                

                //防止最后一行拉动
                if (clickRowDivider == dgvCardModule.Rows.Count)
                {
                    int lastRowHeight = dgvCardModule.Rows[clickRowDivider - 1].Height;
                    if (lastRowHeight != listRowHeight[clickRowDivider - 1])
                    {
                        dgvCardModule.Rows[clickRowDivider - 1].Height = listRowHeight[clickRowDivider - 1];
                        return;
                    }
                }
                
                foreach (DataGridViewRow dgr in dgvCardModule.Rows)
                {
                    if (dgr.Index == clickRowDivider && index > 0)
                    {
                        if (listRowHeight[index] + listRowHeight[index - 1] - dgvCardModule.Rows[index - 1].Height < 6)
                        {
                            dgvCardModule.Rows[index - 1].Height = listRowHeight[index] + listRowHeight[index - 1] - 6;
                            dgr.Height = 6;
                        }
                        else
                        {
                            dgr.Height = listRowHeight[index] + listRowHeight[index - 1] - dgvCardModule.Rows[index - 1].Height;
                        }
                    }
                    else if (dgr.Index != clickRowDivider - 1)
                    {
                        dgr.Height = listRowHeight[index];
                    }
                    
                    index++;
                }
            }
            else    //拖动列边线时,只改变边线左右两边的单元格宽度
            {
                index = 0;
                if (listColWidth == null || listColWidth.Count < dgvCardModule.Columns.Count)
                {
                    return;
                }
                //防止最后一列拉动
                if (clickColumnDivider == dgvCardModule.Columns.Count)
                {                    
                    int lastColumnWidth = dgvCardModule.Columns[clickColumnDivider - 1].Width;
                    if (lastColumnWidth != listColWidth[clickColumnDivider - 1])
                    {
                        dgvCardModule.Columns[clickColumnDivider - 1].Width = listColWidth[clickColumnDivider - 1];
                        return;
                    }
                }

                foreach (DataGridViewColumn column in dgvCardModule.Columns)
                {
                    if (column.Index == clickColumnDivider && index > 0)
                    {
                        if (listColWidth[index] + listColWidth[index - 1] - dgvCardModule.Columns[index - 1].Width < 6)
                        {
                            dgvCardModule.Columns[index - 1].Width = listColWidth[index] + listColWidth[index - 1] - 6;
                            column.Width = 6;
                        }
                        else
                        {
                            column.Width = listColWidth[index] + listColWidth[index - 1] - dgvCardModule.Columns[index - 1].Width;
                        }
                    }
                    else if (column.Index != clickColumnDivider - 1)
                    {
                        column.Width = listColWidth[index];
                    }

                    //列调整时，动态调整回车
                    if (column.Index == clickColumnDivider || column.Index == clickColumnDivider - 1)
                    {
                        foreach (DataGridViewRow dgr in dgvCardModule.Rows)
                        {
                            if (dgr.Cells[column.Index].Value != null)
                            {
                                string oldValue = dgr.Cells[column.Index].Value.ToString();
                                dgr.Cells[column.Index].Value = splitStringByWidth(oldValue, dgr.Cells[column.Index].Style.Font, new Point(dgr.Index, column.Index));
                            }
                        }
                    }
                    index++;
                }
            }

            #endregion

            #region 设置明细框的高与宽

            foreach (Control control in dgvCardModule.Controls)
            {
                if (control.GetType() == typeof(DataGridView) && 
                    control.Name.StartsWith("dgv"))
                {
                    row = control.Name.Substring(3, control.Name.IndexOf("-") - 3);
                    col = control.Name.Substring(control.Name.IndexOf("-") + 1);

                    rowIndex = int.Parse(row);
                    colIndex = int.Parse(col);

                    if (addRowOrColumn == 1 && 
                        currIndex <= rowIndex)
                    {
                        rowIndex +=1;
                    }
                    else if (addRowOrColumn == 2 &&
                        currIndex <= colIndex)
                    {
                        colIndex += 1;
                    }

                    control.Name = string.Format("dgv{0}-{1}", rowIndex, colIndex);

                    if (rowOrColumn)
                    {
                        control.Height = dgvCardModule.Rows[rowIndex].Height;
                        int rowspan = (dgvCardModule.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).RowSpan;

                        if (rowspan > 1)
                        {
                            for (int i = 1; i < rowspan; i++)
                            {
                                control.Height += dgvCardModule.Rows[rowIndex + i].Height;
                            }
                        }
                        value = GetLeftValue(control.Height, (DataGridView)control, rowOrColumn);
                        ((DataGridView)control).Rows[((DataGridView)control).Rows.Count - 1].Height = value;
                    }
                    else
                    {
                        control.Width = dgvCardModule.Columns[colIndex].Width;
                        int colspan = (dgvCardModule.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).ColumnSpan;

                        if (colspan > 1)
                        {
                            for (int i = 1; i < colspan; i++)
                            {
                                control.Width += dgvCardModule.Columns[colIndex + i].Width;
                            }
                        }
                        
                         value = GetLeftValue(control.Width, (DataGridView)control, rowOrColumn);
                        ((DataGridView)control).Columns[((DataGridView)control).Columns.Count - 1].Width = value;
                    }
                    Rectangle rect = dgvCardModule.GetCellDisplayRectangle(colIndex, rowIndex, false);
                    int top = rect.Y - 1;
                    int left = rect.X - 1;
                    control.Location = new Point(left, top);
                }
            }

            #endregion
            
            GetGridDividerLine(dgvCardModule);
            dgvCardModule.Invalidate();
        }

        #region 最后一行高度设置

        /*
        /// <summary>
        /// 方法说明：动态调整行高时，相应调整最后一行高度
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// </summary>
        private void SetLastRowHeight()
        {
            int totalHeight = dgvCardModule.Height - 3;
            int countHeight = 0;
            int staticHeight = 0;
            int index = 0;

            foreach (DataGridViewRow row in dgvCardModule.Rows)
            {
                countHeight += row.Height;
                if (hti.RowIndex - 1 >= index)
                {
                    staticHeight += row.Height;
                }
                index++;
            }
            //往上拉
            if (countHeight < totalHeight)
            {
                isFirstLoad = true;
                dgvCardModule.Rows[dgvCardModule.Rows.Count - 1].Height += totalHeight - countHeight;
                isFirstLoad = false;
            }//往下拉
            else
            {
                isFirstLoad = true;
                if (countHeight - totalHeight >= 0)
                {
                    for (int i = hti.RowIndex; i < dgvCardModule.Rows.Count; i++)
                    {
                        if (i < dgvCardModule.Rows.Count - 1)
                        {
                            dgvCardModule.Rows[i].Height = (totalHeight - staticHeight) / (dgvCardModule.Rows.Count - hti.RowIndex);
                        }
                        else if (hti.RowIndex == dgvCardModule.Rows.Count - 1)
                        {
                            dgvCardModule.Rows[hti.RowIndex].MinimumHeight = 3;
                            int remain = totalHeight - staticHeight;
                            if (remain > 3)
                            {
                                dgvCardModule.Rows[hti.RowIndex].Height = remain;
                            }
                            else
                            {
                                dgvCardModule.Rows[hti.RowIndex].Height = 3;
                            }
                        }
                        else if (dgvCardModule.Rows.Count - hti.RowIndex - 1 > 0)
                        {
                            int mod = (totalHeight - staticHeight) % (dgvCardModule.Rows.Count - hti.RowIndex);
                            dgvCardModule.Rows[i].Height = (totalHeight - staticHeight) / (dgvCardModule.Rows.Count - hti.RowIndex) + mod;

                        }
                    }
                }
                isFirstLoad = false;
            }
        }
        */
        
        #endregion

        /// <summary>
        /// 方法说明：插入图片文件
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// </summary>
        public void ImportImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "JPEG(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp|图片文件(*.jpg,*.bmp,*.gif)|*.jpg;*.bmp;*.gif";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                try
                {
                    PictureBox pic = new PictureBox();
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Name = Guid.NewGuid().ToString();
                    pic.Width = 25;
                    pic.Height = 25;

                    pic.Image = Image.FromFile(fileName);
                    pic.ImageLocation = fileName;

                    dgvCardModule.Controls.Add(pic);
                    if (dgvCardModule.CurrentCell != null)
                    {
                        Rectangle rect = dgvCardModule.GetCellDisplayRectangle(dgvCardModule.CurrentCell.ColumnIndex, dgvCardModule.CurrentCell.RowIndex, false);
                        pic.Location = new Point(rect.X, rect.Y);
                    }
                    pic.Click += new EventHandler(pic_Click);
                    currPicture = pic.Name;
                    ClearPictrueBoxBorder(true);

                    ControlOperator operat = new ControlOperator(pic);

                    operat.Size = true;  //是否能改变控件大小 
                    operat.Move = true;  //是否能移动控件 
                    operat.Max = false;   //是否能移动大于窗体的位置 
                    operat.Min = false;   //是否能移动到窗体的最前面
                }
                catch
                {
                    MessageBox.Show("载入图片文件失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }            
        }

        /// <summary>
        /// 方法说明：清除PictureBox边框
        /// 作    者：jason.tang
        /// 完成时间：2012-12-26
        /// <param name="pic">是否PictureBox</param>
        private void ClearPictrueBoxBorder(bool pic)
        {
            foreach (Control control in dgvCardModule.Controls)
            {
                if (control is PictureBox)
                {
                    if (!pic || control.Name != currPicture)
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
                else if(control is DataGridView)
                {
                    ((DataGridView)control).ClearSelection();
                }
            }
        }

        /// <summary>
        /// 方法说明：检查当前单元格是否包含在合并的单元格中
        /// 作    者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        private void CheckIsInSpanCells(DataGridView dgv)
        {
            //Point p = FindSpanCell();
            int rowIndex = CurrentSelectCell.RowIndex;
            int colIndex = CurrentSelectCell.ColumnIndex;

            int colspan = CurrentSelectCell.ColumnSpan;
            int rowspan = CurrentSelectCell.RowSpan;

            if (CurrentSelectCell.RowIndex < rowIndex + rowspan &&
                CurrentSelectCell.ColumnIndex < colIndex + colspan)
            {
                dgvCardModule.CurrentCell = dgvCardModule.Rows[rowIndex].Cells[colIndex];
                Rectangle rect = dgvCardModule.GetCellDisplayRectangle(colIndex, rowIndex, false);
                dgv.Top = rect.Y - 1;
                dgv.Left = rect.X - 1;
            }
        }

        /// <summary>
        /// 方法说明：找到合并单元格的起点单元格坐标
        /// 作    者：jason.tang
        /// 完成时间：2013-01-06
        /// </summary>
        /// <returns>单元格坐标</returns>
        private Point FindSpanCell()
        {
            Point p = new Point();

            int currRowIndex = CurrentSelectCell.RowIndex;
            int currColIndex = CurrentSelectCell.ColumnIndex;

            p = (dgvCardModule.Rows[currRowIndex].Cells[currColIndex] as DataGridViewTextBoxCellEx).SpanCell;            

            return p;
        }

        /// <summary>
        /// 方法说明：拉动列或行时得到变动的值
        /// 作    者：jason.tang
        /// 完成时间：2013-01-06
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
        /// 方法说明：获取Grid边线的线段坐标
        /// 作    者：jason.tang
        /// 完成时间：2013-01-07
        /// </summary>
        private void GetGridDividerLine(DataGridView datagridview)
        {            
            int index = 0;
            int value = 0;
            dicRowLine = new Dictionary<Point, Point>();
            dicColumnLine = new Dictionary<Point, Point>();

            foreach (DataGridViewRow row in datagridview.Rows)
            {
                value += row.Height;
                if (index < datagridview.Rows.Count - 1 && 
                    !dicRowLine.ContainsKey(new Point(0, value)))
                {
                    dicRowLine.Add(new Point(0, value), new Point(datagridview.Width, value));
                }
                index++;
            }
            index = 0;
            value = 0;
            foreach (DataGridViewColumn column in datagridview.Columns)
            {
                value += column.Width;
                if (index < datagridview.Columns.Count - 1 &&
                    !dicColumnLine.ContainsKey(new Point(value, 0)))
                {
                    dicColumnLine.Add(new Point(value, 0), new Point(value, datagridview.Height));
                }
                index++;
            }
        }

        /// <summary>
        /// 方法说明：拷贝单元格
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        private void CopyCells()
        {
            dicCopyCells = new Dictionary<Point, DataGridViewTextBoxCellEx>();
            if (dgvCardModule.SelectedCells.Count > 0)
            {
                foreach (DataGridViewTextBoxCellEx cell in dgvCardModule.SelectedCells)
                {
                    if (cell.SpanCell.X > 0 && cell.SpanCell.Y > 0)
                    {
                        DataGridViewTextBoxCellEx ex = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y];
                        if (ex.ColumnSpan > 1 || ex.RowSpan > 1)
                        {
                            if (ex.ColumnSpan > 1)
                            {
                                for (int i = 0; i < ex.ColumnSpan; i++)
                                {
                                    if (!dicCopyCells.ContainsKey(new Point(cell.SpanCell.Y + i, cell.SpanCell.X)))
                                    {
                                        dicCopyCells.Add(new Point(cell.SpanCell.Y + i, cell.SpanCell.X), (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X].Cells[cell.SpanCell.Y + i]);
                                    }
                                }
                            }
                            if (ex.RowSpan > 1)
                            {
                                for (int i = 0; i < ex.RowSpan; i++)
                                {
                                    if (!dicCopyCells.ContainsKey(new Point(cell.SpanCell.Y, cell.SpanCell.X + i)))
                                    {
                                        dicCopyCells.Add(new Point(cell.SpanCell.Y, cell.SpanCell.X + i), (DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.SpanCell.X + i].Cells[cell.SpanCell.Y]);
                                    }
                                }
                            }
                        }
                    }
                    if (!dicCopyCells.ContainsKey(new Point(cell.ColumnIndex, cell.RowIndex)))
                    {
                        dicCopyCells.Add(new Point(cell.ColumnIndex, cell.RowIndex), cell);
                    }
                }
            }
        }

        /// <summary>
        /// 方法说明：单元格粘贴
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        private void PasteCells()
        {
            if (dicCopyCells != null && dicCopyCells.Count > 0)
            {
                if (CurrentSelectCell == null)
                {
                    return;
                }

                int rowIndex = CurrentSelectCell.RowIndex;
                int colIndex = CurrentSelectCell.ColumnIndex;
                List<int> listRowIndex = new List<int>();
                List<int> listColIndex = new List<int>();
                foreach (Point p in dicCopyCells.Keys)
                {
                    if (!listRowIndex.Contains(p.Y))
                    {
                        listRowIndex.Add(p.Y);
                    }
                    if (!listColIndex.Contains(p.X))
                    {
                        listColIndex.Add(p.X);
                    }
                }
                //将行列索引分别排序
                listRowIndex.Sort();
                listColIndex.Sort();

                int rowCount = listRowIndex.Count - 1;
                int colCount = listColIndex.Count - 1;

                //如果需要粘贴的区域与复制的区域有重叠，撤销操作
                if ((listRowIndex.Contains(rowIndex) && listColIndex.Contains(colIndex)) ||
                    (listRowIndex.Contains(rowIndex + rowCount) && listColIndex.Contains(colIndex)) ||
                    (listRowIndex.Contains(rowIndex) && listColIndex.Contains(colIndex + colCount)) ||
                    (listRowIndex.Contains(rowIndex + rowCount) && listColIndex.Contains(colIndex + colCount)))
                {
                    return;
                }

                //得出当前要粘贴的单元格与复制单元格的左上角单元格坐标差值
                int rowDiff = rowIndex - listRowIndex[0];
                int colDiff = colIndex - listColIndex[0];

                int colRange;
                int rowRange = 0;
                //设置对应单元格的样式
                try
                {
                    foreach (int row in listRowIndex)
                    {
                        colRange = 0;
                        foreach (int col in listColIndex)
                        {
                            DataGridViewCellStyle style = new DataGridViewCellStyle(dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].Style);
                            ComboBoxSourceHelper.CellType type = (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType),
                                dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].CellEditType.ToString());
                            object value = dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].Value;
                            int rowSpan = dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].RowSpan;
                            int colSpan = dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].ColumnSpan;
                            Point p = dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].SpanCell;
                            dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange].Style = style;
                            (dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange] as DataGridViewTextBoxCellEx).CellEditType = type;
                            (dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange] as DataGridViewTextBoxCellEx).CustStyle = 
                                dicCopyCells[new Point(colIndex + colRange - colDiff, rowIndex + rowRange - rowDiff)].CustStyle;
                            ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange]).RowSpan = rowSpan;
                            ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange]).ColumnSpan = colSpan;
                            ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange]).SpanCell = p;
                            dgvCardModule.Rows[rowIndex + rowRange].Cells[colIndex + colRange].Value = value;
                            colRange++;
                        }
                        rowRange++;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("粘贴区域与拷贝不相符", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 方法说明：合并单元格
        /// 作    者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        private void MergeCell()
        {
            //GetLeftTopCell();
            bool isContainerSpan = false;       //是否已经包含合并的单元格
            if (minValue != null && maxValue != null)
            {
                //给单元的合并属性赋值
                foreach (DataGridViewCell cl in dgvCardModule.SelectedCells)
                {
                    (cl as DataGridViewTextBoxCellEx).SpanCell = new Point(minValue.Y, minValue.X);
                    if ((cl as DataGridViewTextBoxCellEx).RowSpan > 1 ||
                        (cl as DataGridViewTextBoxCellEx).ColumnSpan > 1)
                    {
                        isContainerSpan = true;
                    }
                    //如果包含合并的单元格，先撤销合并
                    (cl as DataGridViewTextBoxCellEx).RowSpan = 1;
                    (cl as DataGridViewTextBoxCellEx).ColumnSpan = 1;
                }

                if (isContainerSpan)
                {
                    return;
                }

                int columnSpan = Math.Abs(minValue.X - maxValue.X) + 1;
                int rowSpan = Math.Abs(minValue.Y - maxValue.Y) + 1;

                dgvCardModule.CurrentCell = dgvCardModule.Rows[minValue.Y].Cells[minValue.X];    //设置属性默认选中左上角的单元格

                if (columnSpan > 1 && rowSpan > 1)
                {
                    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan = columnSpan;
                    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan = rowSpan;
                }
                else if (columnSpan > 1)
                {
                    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan = columnSpan;
                }
                else if (rowSpan > 1)
                {
                    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan = rowSpan;
                }

                //if (columnSpan > 1)
                //{
                //    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan = columnSpan;
                //    for (int r = minValue.Y; r < minValue.Y + rowSpan; r++)
                //    {
                //        for (int c = minValue.X; c < minValue.X + columnSpan - 1; c++)
                //        {
                //            (dgvCardModule.Rows[r].Cells[c] as DataGridViewTextBoxCellEx).RightBorderColor = Color.Red;
                //        }
                //    }
                //}
                //if (rowSpan > 1)
                //{
                //    (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).RowSpan = rowSpan;
                //    for (int c = minValue.X; c < minValue.X + columnSpan; c++)
                //    {
                //        for (int r = minValue.Y; r < minValue.Y + rowSpan - 1; r++)
                //        {

                //            (dgvCardModule.Rows[r].Cells[c] as DataGridViewTextBoxCellEx).BottomBorderColor = Color.Red;
                //        }
                //    }
                //}
            }
        }
        
        /// <summary>
        /// 方法说明：取消单元格合并
        /// 作    者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        private void CancelMergeCell()
        {
            //Point p = FindSpanCell();
            //int rowIndex = p.X;
            //int colIndex = p.Y;

            CurrentSelectCell.ColumnSpan = 1;
            CurrentSelectCell.RowSpan = 1;

            CurrentSelectCell.RightBorderColor = Color.Empty;
            CurrentSelectCell.BottomBorderColor = Color.Empty;
            CurrentSelectCell.RightBorderWidth = 0;
            CurrentSelectCell.BottomBorderWidth = 0;

            minValue = new Point();
            maxValue = new Point();
        }

        /// <summary>
        /// 方法说明：取消左上角单元格
        /// 作    者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        private void GetLeftTopCell()
        {
            int init = 0;

            if (dgvCardModule.SelectedCells.Count == 0)
            {
                return;
            }

            foreach (DataGridViewCell cl in dgvCardModule.SelectedCells)
            {
                if (init <= 0)
                {
                    minValue = new Point(cl.ColumnIndex, cl.RowIndex);
                    maxValue = new Point(cl.ColumnIndex, cl.RowIndex);
                    init++;
                }
                else
                {
                    if (cl.RowIndex < minValue.Y || cl.ColumnIndex < minValue.X)
                    {
                        minValue = new Point(cl.ColumnIndex, cl.RowIndex);
                    }
                    else if (cl.RowIndex > maxValue.Y || cl.ColumnIndex > maxValue.X)
                    {
                        maxValue = new Point(cl.ColumnIndex, cl.RowIndex);
                    }
                }
            }
        }

        /// <summary>
        /// 方法说明：界面缩放
        /// 作    者：jason.tang
        /// 完成时间：2013-01-14
        /// </summary>
        /// <param name="_percent">百分比</param>
        /// <param name="delta">鼠标滚轮向上还是向下</param>
        private void SetPageZoom(double _percent, int delta)
        {
            //先去掉行列变化事件
            this.dgvCardModule.ColumnWidthChanged -= dgvCardModule_ColumnWidthChanged;
            this.dgvCardModule.RowHeightChanged -= dgvCardModule_RowHeightChanged;

            int totalWidth = 0;
            int totalHeight = 0;
            int index = 0;

            try
            {
                //string oldPercent = this.TabText.Substring(this.TabText.IndexOf("@") + 2, this.TabText.IndexOf("%") - this.TabText.IndexOf("@") - 2);
                //int diff = (int)Math.Round(double.Parse(oldPercent) - _percent);

                int diff = _percent.ToString().EndsWith("5") ? 10 : 9;
                //Dictionary<Point, Point> dicLocation = new Dictionary<Point, Point>();
                //foreach (DataGridViewRow row in dgvCardModule.Rows)
                //{
                //    foreach (DataGridViewCell cell in row.Cells)
                //    {
                //        Rectangle rect = dgvCardModule.GetCellDisplayRectangle(cell.ColumnIndex, cell.RowIndex, false);
                //        dicLocation.Add(new Point(cell.RowIndex, cell.ColumnIndex), new Point(rect.X, rect.Y));
                //    }
                //}

                //int diffHeight = (int)Math.Round(3 / 15);
                //int diffWidth = (int)Math.Round(10 / 15);
                //调整行高
                foreach (DataGridViewRow row in dgvCardModule.Rows)
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
                foreach (DataGridViewColumn column in dgvCardModule.Columns)
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

                foreach (Control control in dgvCardModule.Controls)
                {
                    if (control.GetType() == typeof(DataGridView) &&
                        control.Name.StartsWith("dgv"))  //明细框缩放
                    {
                        string row = control.Name.Substring(3, control.Name.IndexOf("-") - 3);
                        string col = control.Name.Substring(control.Name.IndexOf("-") + 1);

                        int rowIndex = int.Parse(row);
                        int colIndex = int.Parse(col);


                        control.Height = dgvCardModule.Rows[rowIndex].Height;
                        int rowspan = (dgvCardModule.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).RowSpan;

                        if (rowspan > 1)
                        {
                            for (int i = 1; i < rowspan; i++)
                            {
                                control.Height += dgvCardModule.Rows[rowIndex + i].Height;
                            }
                        }
                        int value = GetLeftValue(control.Height, (DataGridView)control, true);
                        ((DataGridView)control).Rows[((DataGridView)control).Rows.Count - 1].Height = value;

                        control.Width = dgvCardModule.Columns[colIndex].Width;
                        int colspan = (dgvCardModule.Rows[rowIndex].Cells[colIndex] as DataGridViewTextBoxCellEx).ColumnSpan;

                        if (colspan > 1)
                        {
                            for (int i = 1; i < colspan; i++)
                            {
                                control.Width += dgvCardModule.Columns[colIndex + i].Width;
                            }
                        }

                        value = GetLeftValue(control.Width, (DataGridView)control, false);
                        ((DataGridView)control).Columns[((DataGridView)control).Columns.Count - 1].Width = value;

                        Rectangle rect = dgvCardModule.GetCellDisplayRectangle(colIndex, rowIndex, false);
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

                pnModule.Width += totalWidth;
                pnModule.Height += totalHeight;

                string tabText = this.TabText;
                if (this.TabText.Contains("@"))
                {
                    tabText = tabText.Substring(0, tabText.IndexOf("@") - 1);
                }
                this.TabText = string.Format(tabText + " @ {0}%", _percent);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           
            //再注册行列变化事件
            this.dgvCardModule.ColumnWidthChanged += dgvCardModule_ColumnWidthChanged;
            this.dgvCardModule.RowHeightChanged += dgvCardModule_RowHeightChanged;
        }

        /// <summary>
        /// 方法说明：按字符串实际宽度进行分割
        /// 作    者：jason.tang
        /// 完成时间：2013-01-15
        /// </summary>
        /// <param name="src">源字符串</param>
        /// <param name="totalWidth">单元格宽度</param>
        /// <param name="font">字体</param>
        /// <param name="pCell">单元格索引</param>
        /// <returns></returns>
        private string splitStringByWidth(string src, Font font, Point pCell)
        {
            try
            {
                DataGridViewTextBoxCellEx cell = new DataGridViewTextBoxCellEx();
                Point p;
                if (pCell.X < 0 || pCell.Y < 0)
                {
                    p = (dgvCardModule.CurrentCell as DataGridViewTextBoxCellEx).SpanCell;
                    cell = (DataGridViewTextBoxCellEx)dgvCardModule.CurrentCell;
                }
                else
                {
                    p = ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[pCell.X].Cells[pCell.Y]).SpanCell;
                    cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[pCell.X].Cells[pCell.Y];
                }

                if (p.X > 0 && p.Y > 0)
                {
                    cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y];
                }
                else if ((p.X == 0 || p.Y == 0) &&
                    (((DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y]).ColumnSpan > 1 ||
                    ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y]).RowSpan > 1))
                {
                    cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[p.X].Cells[p.Y];
                }


                int colspan = cell.ColumnSpan;
                int totalWidth = dgvCardModule.Columns[cell.ColumnIndex].Width;
                if (colspan > 1)
                {
                    for (int i = 1; i < colspan; i++)
                    {
                        totalWidth += dgvCardModule.Columns[cell.ColumnIndex + i].Width;
                    }
                }
                Graphics g = dgvCardModule.CreateGraphics();
                StringBuilder sb = new StringBuilder();
                List<string> listNewValue = new List<string>();
                src = src.Replace("\r", string.Empty);
                src = src.Replace("\n", "@");
                foreach (char c in src)
                {
                    if (c.ToString() == "@")
                    {
                        if (!string.IsNullOrEmpty(sb.ToString()))
                        {
                            sb.Append("@");
                            listNewValue.Add(sb.ToString());
                        }
                        sb = new StringBuilder();
                        continue;
                    }
                    sb.Append(c.ToString());
                    float width = g.MeasureString(sb.ToString(), font).Width;
                    if (width > totalWidth - 12)
                    {
                        listNewValue.Add(sb.ToString());
                        sb = new StringBuilder();
                    }
                }
                if (sb != null && sb.Length > 0)
                {
                    listNewValue.Add(sb.ToString());
                }
                string value = string.Join("\r", listNewValue.ToArray());
                value = value.Replace("@", "\n");
                return value;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 方法说明：卡片模板保存
        /// 作    者：jason.tang
        /// 完成时间：2013-01-16
        /// </summary>
        /// <param name="isStorage">是否入库</param>
        public ProcessCardModule SaveTemplate(bool isStorage, DataGridView dgv)
        {
            if (dgv != null)
            {
                dgv.ClearSelection();
                dgvCardModule = dgv;
            }
            dgvCardModule.ClearSelection();
            pnModule.Focus();

            //标题提示框列明
            List<string> lstTitleColumns = new List<string>();
            List<string> lstDetailColumns = new List<string>();

            ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
            Model.ProcessCardModule pcm = new Model.ProcessCardModule();

            Model.CardsXML card = new Model.CardsXML();
            //Card module = card.Cards.FirstOrDefault<Card>();
            Card module = new Card();

            //模板的属性赋值
            module.CardRange = string.Format("A{0}", _breadth);
            //moude.CardDirection = "横向";
            module.MarginLeft = _padleft.ToString();
            module.MarginTop = _padtop.ToString();
            module.MarginRight = _padright.ToString();
            module.MarginBottom = _padbottom.ToString();
            //moude.PrintScale = 0;
            module.PrintAboveOffset = _offsettop;
            module.PrintUnderOffset = _offsetleft;
            module.Width = _width;
            module.Height = _height;
            List<ImageObject> listImages = new List<ImageObject>();
            ImageObject image = new ImageObject();
            foreach (Control control in dgvCardModule.Controls)
            {
                if (control.GetType() == typeof(PictureBox))
                {
                    //listPath.Add(((PictureBox)control).ImageLocation);
                    image = new ImageObject();                    
                    image.ImagePath = ((PictureBox)control).ImageLocation;
                    image.Width = ((PictureBox)control).Width;
                    image.Height = ((PictureBox)control).Height;
                    image.LocationX = ((PictureBox)control).Location.X;
                    image.LocationY = ((PictureBox)control).Location.Y;
                    listImages.Add(image);
                }
            }
            module.ImageObjects = listImages.ToArray();

            module.Name = CardModuleName;
            pcm.Name = CardModuleName;

            List<Model.Row> rowList = new List<Model.Row>();
            List<Model.Cell> cellList;
            List<Model.DetailCell> detailList;
            Model.Row rowProperty;
            Model.Cell cellProperty;
            Model.DetailCell detailProperty;

            int rowIndex = 0;
            int colIndex = 0;
            try
            {
                Dictionary<string, List<string>> dicColumns = new Dictionary<string, List<string>>();
                //明细框数量
                int dCount = 1;
                List<string> listTags = new List<string>();
                foreach (DataGridViewRow row in dgvCardModule.Rows)
                {
                    rowProperty = new Model.Row();
                    rowProperty.Height = rowIndex == dgvCardModule.Rows.Count - 1 ? row.Height + 3 : row.Height;                    
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
                        cellProperty.Width = colIndex == dgvCardModule.Columns.Count - 1 ? 
                            dgvCardModule.Columns[cell.ColumnIndex].Width + 3 : dgvCardModule.Columns[cell.ColumnIndex].Width;

                        cellProperty.LeftTopRightBottom = cell.LeftTopRightBottom;
                        cellProperty.LeftBottomRightTop = cell.LeftBottomRightTop;
                        cellProperty.ContentType = cell.CellContent.ToString();
                        cellProperty.CellTag = cell.CellTag;
                        cellProperty.CellSource = cell.CellSource;

                        if (listTags.Contains(cell.CellTag))
                        {
                            MessageBox.Show(string.Format("单元格名称{0}重复，请修改",cell.CellTag), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dgvCardModule.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Selected = true;
                            return null;
                        }
                        else
                            listTags.Add(cell.CellTag);

                        detailList = new List<Model.DetailCell>();
                        //明细单元格
                        if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                        {
                            if (cell.DetailProperty != null)
                            {
                                List<DetailGridViewTextBoxColumn> details = (List<DetailGridViewTextBoxColumn>)cell.DetailProperty;
                                foreach (DetailGridViewTextBoxColumn column in details)
                                {
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
                                    detailProperty.SpaceRows = cell.SpaceRows;
                                    detailProperty.Tag = column.Tag == null ? string.Empty : column.Tag.ToString();
                                    detailProperty.Type = column.Type.ToString();
                                    detailProperty.AdvanceProperty = column.AdvanceProperty.ToString();
                                    detailProperty.SerialStep = cell.SerialStep;
                                    detailList.Add(detailProperty);
                                    lstDetailColumns.Add(column.ColumnName);
                                }
                                dicColumns.Add("Detail" + dCount, lstDetailColumns);
                                dCount++;
                            }                            
                        }//标题提示框
                        else if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "1"))
                        {
                            lstTitleColumns.Add(string.Format("Column{0}-{1}", cell.RowIndex, cell.ColumnIndex));
                        }

                        cellProperty.DetailCells = detailList.ToArray();
                        cellList.Add(cellProperty);
                        colIndex++;
                    }
                    rowProperty.Cells = cellList.ToArray();
                    rowList.Add(rowProperty);
                    rowIndex++;
                }
                dicColumns.Add("Title", lstTitleColumns);

                module.Rows = rowList.ToArray();
                card.Cards = new Card[] { module };
                pcm.CardModule = card;
                Guid guid = new Guid();
                if (isStorage)
                {                  
                    string moduleid = this.Name.Substring(14);
                    bool isNewOrModify = false;
                    if (!string.IsNullOrEmpty(moduleid))
                    {
                        try
                        {
                            Guid gid = new Guid(moduleid);
                            Model.ProcessCardModule processCardModule = pcmDll.GetProcessCardModule(gid);
                            isNewOrModify = false;
                        }
                        catch
                        {
                            isNewOrModify = true;
                        }
                    }
                    else
                    {
                        bool exist = Kingdee.CAPP.BLL.ProcessCardModuleBLL.CheckModuleNameExist(pcm.Name);
                        if (exist)
                        {
                            MessageBox.Show("模版已经入库，请在模版导航界面打开进行编辑", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return null;
                        }
                    }

                    //已经存在的模板则更新
                    if (!isNewOrModify)
                    {
                        
                        pcm.Id = new Guid(moduleid);
                        bool result = pcmDll.UpdateProcessCardModule(pcm);
                        string notice = result ? "成功" : "失败";
                        MessageBox.Show(string.Format("模板更新{0}", notice), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valueChanged = false;

                        SqlServerControllerBLL.CreateTable(module.Name, dicColumns);

                        return null;
                    }
                    //模板新建
                    guid = pcmDll.AddProcessCard(pcm);
                    if (guid != null)
                    {
                        MessageBox.Show("模板入库成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valueChanged = false;

                        SqlServerControllerBLL.CreateTable(module.Name, dicColumns);

                        /// 初始化业务Id
                        pcm.Id = guid;
                        return pcm;
                    }
                    else
                    {
                        MessageBox.Show("模板入库失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    SaveFileDialog of = new SaveFileDialog();
                    of.Filter = "CAP files (*.cap)|*.cap";
                    if (of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string path = of.FileName;
                        Kingdee.CAPP.Common.Serialize.SerializeHelper.BinarySerialize<CardsXML>(card, path);
                        MessageBox.Show("模板保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        valueChanged = false;
                    }
                }                
            }
            catch
            {
                MessageBox.Show("模板保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return null;
        }

        /// <summary>
        /// 方法说明：根据模板ID打开模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-16 
        /// </summary>
        /// <param name="moduleId">模板ID</param>
        /// <param name="path">本地xml路径</param>
        public void GetTemplate(string moduleId, string path)
        {
            if (string.IsNullOrEmpty(moduleId) && string.IsNullOrEmpty(path))
            {
                return;
            }

            ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
            CardsXML cardmodule = new CardsXML();
            Model.ProcessCardModule pcm = new ProcessCardModule();

            if (!string.IsNullOrEmpty(path))
            {
                cardmodule = Kingdee.CAPP.Common.Serialize.SerializeHelper.BinaryDeSerialize<CardsXML>(path);
            }
            else if (!string.IsNullOrEmpty(moduleId))
            {
                try
                {
                    Guid gid = new Guid(moduleId);
                    pcm = pcmDll.GetProcessCardModule(gid);
                    cardmodule = pcm.CardModule;
                }
                catch
                {
                    return;
                }
            }
            Card card = cardmodule.Cards.FirstOrDefault<Card>();
            _cardModuleName = card.Name;
            this.Name = string.Format("CardModuleFrm-{0}", pcm.Id);

            int Width = Convert.ToInt32(card.Width);
            int Height = Convert.ToInt32(card.Height);
            _width = Width;
            _height = Height;
            _breadth = int.Parse(card.CardRange.Replace("A", ""));
            ResizeInitGridView(Width, Height, -1);

            List<DataGridViewRow> listRow = new List<DataGridViewRow>();


            if (card.Rows == null || card.Rows.Length == 0)
            {
                return;
            }

            int rows = card.Rows.Length;
            int columns = card.Rows[0].Cells.Length;
            dgvCardModule.Rows.Clear();
            dgvCardModule.Columns.Clear();

            List<int> listWidth = new List<int>();
            List<int> listHeight = new List<int>();

            foreach (Row row in card.Rows)
            {
                listHeight.Add(Convert.ToInt32(row.Height));
            }

            foreach(Cell cell in card.Rows[0].Cells)
            {
                listWidth.Add(Convert.ToInt32(cell.Width));
            }

            InitDataGridView(listHeight, listWidth, false);
            SetMenuEnable(false);
            DataGridViewTextBoxCellEx cellEx;
            List<DataGridViewCustomerCellStyle> listCellStyle;
            DataGridViewCustomerCellStyle cellStyle;
            List<int> listPadding;
            try
            {
                foreach (Row row in card.Rows)
                {
                    foreach (Cell cell in row.Cells)
                    {
                        cellEx = ((DataGridViewTextBoxCellEx)dgvCardModule.Rows[cell.PointX].Cells[cell.PointY]);
                        CurrentSelectCell = cellEx;
                        cellEx.Style.Alignment = (DataGridViewContentAlignment)Enum.Parse(typeof(DataGridViewContentAlignment), cell.Alignment);
                        cellEx.Style.BackColor = int.Parse(cell.BackGround) == 0 ? Color.White : Color.FromArgb(int.Parse(cell.BackGround));
                        cellEx.BottomBorderColor = Color.FromArgb(int.Parse(cell.BottomBorderColor));
                        cellEx.BottomBorderWidth = Convert.ToInt32(cell.BottomBorderWidth);
                        cellEx.CellEditType = (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), cell.CellType);
                        cellEx.ColumnSpan = cell.ColSpan;
                        cellEx.Value = cell.Content;
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
                        cellEx.CellContent = string.IsNullOrEmpty(cell.ContentType) ? (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent),"0"):
                            (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), cell.ContentType);
                        
                        cellEx.Style.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());
                        cellEx.CellTag = cell.CellTag;
                        cellEx.CellSource = cell.CellSource;

                        //明细框单元格
                        if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                        {
                            Rectangle rect = dgvCardModule.GetCellDisplayRectangle(cellEx.ColumnIndex, cellEx.RowIndex, false);
                            int top = rect.Y - 1;
                            int left = rect.X - 1;

                            List<DetailGridViewTextBoxColumn> dicColumns = new List<DetailGridViewTextBoxColumn>();
                            object objDetailProperty = cell.DetailCells;
                            //明细框
                            AddDetailGridView(top, left, objDetailProperty, dicColumns);
                            cellEx.DetailProperty = dicColumns;
                            if (cell.DetailCells != null && cell.DetailCells.Length > 0)
                            {
                                cellEx.SerialStep = cell.DetailCells[0].SerialStep;
                                cellEx.SpaceRows = cell.DetailCells[0].SpaceRows;
                            }
                        }

                    }
                }
                //读取图片
                if (card.ImageObjects != null && card.ImageObjects.Length > 0)
                {
                    foreach (ImageObject image in card.ImageObjects)
                    {
                        LoadImage(image);
                    }
                }
                valueChanged = false;
            }
            catch
            {
                dgvCardModule.Rows.Clear();
                dgvCardModule.Columns.Clear();
                MessageBox.Show("模板文件打开失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
            //dgvCardModule.Refresh();
        }

        /// <summary>
        /// 方法说明：装载图片文件
        /// 作    者：jason.tang
        /// 完成时间：2013-01-22
        /// </summary>
        /// <param name="image">图片对象</param>
        private void LoadImage(ImageObject image)
        {
            PictureBox pic = new PictureBox();
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Name = Guid.NewGuid().ToString();
            pic.Width = image.Width;
            pic.Height = image.Height;

            if (!File.Exists(image.ImagePath))
            {
                //MessageBox.Show(string.Format("图形文件加载失败\r\n请检查路径{0}",image.ImagePath), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
                pic.Image = Properties.Resources.none_img;
            }
            else
            {
                pic.Image = Image.FromFile(image.ImagePath);
                pic.ImageLocation = image.ImagePath;
            }            

            dgvCardModule.Controls.Add(pic);
            pic.Location = new Point(image.LocationX, image.LocationY);
            pic.Click += new EventHandler(pic_Click);
            ClearPictrueBoxBorder(true);

            ControlOperator operat = new ControlOperator(pic);

            operat.Size = true;  //是否能改变控件大小 
            operat.Move = true;  //是否能移动控件 
            operat.Max = false;   //是否能移动大于窗体的位置 
            operat.Min = false;   //是否能移动到窗体的最前面
        }

        /// <summary>
        /// 方法说明：获取特殊字符
        /// 作    者：jason.tang
        /// 完成时间：2013-07-17
        /// </summary>
        /// <param name="obj">特殊字符对象</param>
        public void GetSymbol(object obj)
        {
            if (obj != null)
            {
                string cellValue = CurrentSelectCell.Value.ToString();
                CurrentSelectCell.Value = cellValue + obj.ToString();
            }
        }

        #endregion           
                
        private void dgvCardModule_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)dgvCardModule.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dgvCardModule.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = cell.CellEditType.ToString();
            }
            catch { }
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
    }
}
