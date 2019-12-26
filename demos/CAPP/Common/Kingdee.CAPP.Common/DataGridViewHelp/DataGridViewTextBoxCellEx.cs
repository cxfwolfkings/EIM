using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using Kingdee.CAPP.Common.DataGridViewHelp;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace Kingdee.CAPP.Common
{
    /// <summary>
    /// 类型说明：DataGridViewTextBoxCellEx单元格扩展类
    /// 作   者：jason.tang
    /// 完成时间：2012-12-17
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class DataGridViewTextBoxCellEx : DataGridViewTextBoxCell, ISpannedCell
    {
        #region 变量声明

        private int m_ColumnSpan = 1;
        private int m_RowSpan = 1;
        private DataGridViewTextBoxCellEx m_OwnerCell;
        private Kingdee.CAPP.Common.ComboBoxSourceHelper.CellType m_CellType;
        private Kingdee.CAPP.Common.ComboBoxSourceHelper.CellContent m_CellContent;
        private string m_Tag;
        private string m_CorreTag;

        private bool _leftLine = false;
        private bool _topLine = false;
        private bool _rightLine = false;
        private bool _bottomLine = false;

        private Color m_color = Color.Empty;
        private bool isAllBorder = false;

        private List<Point> listSpan = new List<Point>();

        /// <summary>
        /// 画笔
        /// </summary>
        private Pen p;

        #endregion

        #region 用绘制边框替换
        /*
        /// <summary>
        /// DataGridView单元格边框样式调整
        /// </summary>
        public override DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(
            DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput,
            DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceHolder,
            bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded,
            bool firstVisibleColumn,
            bool firstVisibleRow)
        {
            dataGridViewAdvancedBorderStylePlaceHolder.Left = DataGridViewAdvancedCellBorderStyle.None;
            dataGridViewAdvancedBorderStylePlaceHolder.Right = dataGridViewAdvancedBorderStyleInput.Right;
            
            if (m_leftLine)
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Left = dataGridViewAdvancedBorderStyleInput.Left;
            }
            else
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Left = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (m_topLine)
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Top = dataGridViewAdvancedBorderStyleInput.Top;
            }
            else
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (m_rightLine)
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Right = dataGridViewAdvancedBorderStyleInput.Right;
            }
            else
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Right = DataGridViewAdvancedCellBorderStyle.None;
            }
            if (m_bottomLine)
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Bottom = dataGridViewAdvancedBorderStyleInput.Bottom;
            }
            else
            {
                dataGridViewAdvancedBorderStylePlaceHolder.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            }
       
            return dataGridViewAdvancedBorderStylePlaceHolder; 
        }
        */
        #endregion

        #region 隐藏基类继承的属性
        
        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        [Browsable(false)]
        public override int MaxInputLength
        {
            get
            {
                return base.MaxInputLength;
            }
            set
            {
                base.MaxInputLength = value;
            }
        }

        [Browsable(false)]
        public new int ColumnIndex
        {
            get { return base.ColumnIndex; }
        }

        [Browsable(false)]
        public new object Tag
        {
            get { return base.Tag; }
            set { base.Tag = value; }
        }

        
       
        [EditorAttribute(typeof(Kingdee.CAPP.Common.DataGridViewHelp.CustomerCellStyleEditor), typeof(System.Drawing.Design.UITypeEditor)), CategoryAttribute("外表"), DescriptionAttribute("设置单元格的样式"), DisplayName("单元格样式")]
        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        public object CustStyle
        {
            get;
            set;
        }

        //[CategoryAttribute("外表"), DescriptionAttribute("设置单元格的样式"), DisplayName("单元格样式")]
        //[ReadOnlyAttribute(false)]
        [Browsable(false)]
        public new DataGridViewCellStyle Style
        {
            get { return base.Style; }
            set { base.Style = value; }
        }

        //[CategoryAttribute("外表"), DescriptionAttribute("设置单元格的样式"), DisplayName("单元格样式")]
        //[ReadOnlyAttribute(false)]
        //public new DataGridViewCellStyle Style
        //{
        //    get { return base.Style; }
        //    set { base.Style = value; }
        //}

        //[CategoryAttribute("外表"), DescriptionAttribute("设置单元格的样式"), DisplayName("单元格样式")]
        //[ReadOnlyAttribute(false)]
        //public new DataGridViewCellStyle Style
        //{
        //    get { return base.Style; }
        //    set { base.Style = value; }
        //}

        //[CategoryAttribute("外表"), DescriptionAttribute("设置单元格的样式"), DisplayName("单元格样式")]
        //[ReadOnlyAttribute(false)]
        //public new DataGridViewCellStyle Style
        //{
        //    get { return base.Style; }
        //    set { base.Style = value; }
        //}


        #endregion

        #region 自定义属性声明

        /// <summary>
        /// 属性说明：是否合并(0:不发生操作, -1:取消合并, 1:合并)
        /// 作   者：jason.tang
        /// 完成时间：2013-01-11
        /// </summary>
        [Browsable(false)]
        public int Merge
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：列合并
        /// 作   者：jason.tang
        /// 完成时间：2012-12-17
        /// </summary>
        [CategoryAttribute("单元格合并"), DescriptionAttribute("设置合并的列数"), DisplayName("跨列数"), PropertyOrder(10)]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public int ColumnSpan
        {
            get { return m_ColumnSpan; }
            set
            {
                if (DataGridView == null || m_OwnerCell != null)
                    return;
                if (value < 1 || ColumnIndex + value - 1 >= DataGridView.ColumnCount)
                    throw new System.ArgumentOutOfRangeException("value");
                if (m_ColumnSpan != value)
                    SetSpan(value, m_RowSpan);
            }
        }

        /// <summary>
        /// 属性说明：行合并
        /// 作   者：jason.tang
        /// 完成时间：2012-12-17
        /// </summary>
        [CategoryAttribute("单元格合并"), DescriptionAttribute("设置合并的行数"), DisplayName("跨行数"), PropertyOrder(11)]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public int RowSpan
        {
            get { return m_RowSpan; }
            set
            {
                if (DataGridView == null || m_OwnerCell != null)
                    return;
                if (value < 1 || RowIndex + value - 1 >= DataGridView.RowCount)
                    throw new System.ArgumentOutOfRangeException("value");
                if (m_RowSpan != value)
                    SetSpan(m_ColumnSpan, value);
            }
        }

        /// <summary>
        /// 合并单元格的起点
        /// </summary>
        [Browsable(false)]
        public Point SpanCell
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：自身的单元格
        /// 作   者：jason.tang
        /// 完成时间：2012-12-17
        /// </summary>
        [Browsable(false)]
        public DataGridViewCell OwnerCell
        {
            get { return m_OwnerCell; }
            private set { m_OwnerCell = value as DataGridViewTextBoxCellEx; }
        }

        /// <summary>
        /// 属性说明：只读属性
        /// 作   者：jason.tang
        /// 完成时间：2012-12-17
        /// </summary>
        [CategoryAttribute("单元格合并"), DescriptionAttribute("设置合并的列数")]
        [Browsable(false)]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;

                if (m_OwnerCell == null
                    && (m_ColumnSpan > 1 || m_RowSpan > 1)
                    && DataGridView != null)
                {
                    foreach(var col in Enumerable.Range(ColumnIndex, m_ColumnSpan))
                        foreach(var row in Enumerable.Range(RowIndex, m_RowSpan))
                            if (col != ColumnIndex || row != RowIndex)
                            {
                                DataGridView[col, row].ReadOnly = value;
                            }
                }
            }
        }
                
        /// <summary>
        /// 属性说明：单元格类型
        /// 作    者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        [CategoryAttribute("单元格基本属性"), DescriptionAttribute("设置单元格的类型【固定提示框,标题填写框,明细填写框,页码显示框,页数显示框,签名名称框,签名日期框】"), DisplayName("类型"), PropertyOrder(12)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Kingdee.CAPP.Common.ComboBoxSourceHelper.CellType CellEditType
        {
            get
            {
                return m_CellType;
            }
            set
            {
                m_CellType = value;
            }
        }

        /// <summary>
        /// 属性说明：单元格类型
        /// 作    者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        [CategoryAttribute("单元格基本属性"), DescriptionAttribute("设置单元格的内容【文本对象,图形对象,OLD对象】"), DisplayName("内容"), PropertyOrder(13)]
        [Browsable(true)]
        public Kingdee.CAPP.Common.ComboBoxSourceHelper.CellContent CellContent
        {
            get
            {
                return m_CellContent;
            }
            set
            {
                m_CellContent = value;
            }
        }

        /// <summary>
        /// 属性说明：单元格标签
        /// 作    者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        [CategoryAttribute("单元格基本属性"), DescriptionAttribute("设置单元格的名称"), DisplayName("名称"), PropertyOrder(14)]
        [Browsable(true)]
        [ReadOnlyAttribute(false)]
        public string CellTag
        {
            get
            {
                return m_Tag;
            }
            set
            {
                m_Tag = value;
            }
        }

        /// <summary>
        /// 属性说明：单元格对应的标签
        /// 作    者：jason.tang
        /// 完成时间：2012-12-22
        /// </summary>
        [Browsable(false)]
        public string CorrespondingTag
        {
            get
            {
                return m_CorreTag;
            }
            set
            {
                m_CorreTag = value;
            }
        }

        /// <summary>
        /// 单元格左边框
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置是否显示左边框"), DisplayName("左边框")]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public bool CellBorderLeft
        {
            get
            {
                return _leftLine;
            }
            set
            {
                _leftLine = value;
            }
        }

        /// <summary>
        /// 单元格上边框
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置是否显示上边框"), DisplayName("上边框")]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public bool CellBorderTop
        {
            get
            {
                return _topLine;
            }
            set
            {
                _topLine = value;
            }
        }

        /// <summary>
        /// 单元格右边框
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置是否显示右边框"), DisplayName("右边框")]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public bool CellBorderRight
        {
            get
            {
                return _rightLine;
            }
            set
            {
                _rightLine = value;
            }
        }

        /// <summary>
        /// 单元格下边框
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置是否显示下边框"), DisplayName("下边框")]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public bool CellBorderBottom
        {
            get
            {
                return _bottomLine;
            }
            set
            {
                _bottomLine = value;
            }
        }

        /// <summary>
        /// 左侧边框颜色
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置左边框颜色"), DisplayName("左边框颜色"), PropertyOrder(15)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Color LeftBorderColor
        {
            get;
            set;
        }
       
        /// <summary>
        /// 头部边框颜色
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置上边框颜色"), DisplayName("上边框颜色"), PropertyOrder(18)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Color TopBorderColor
        {
            get;
            set;
        }
        
        /// <summary>
        /// 右侧边框颜色
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置右边框颜色"), DisplayName("右边框颜色"), PropertyOrder(21)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Color RightBorderColor
        {
            get;
            set;
        }
        
        /// <summary>
        /// 底部边框颜色
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置下边框颜色"), DisplayName("下边框颜色"), PropertyOrder(24)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Color BottomBorderColor
        {
            get;
            set;
        }

        /// <summary>
        /// 全边框颜色
        /// </summary>
        [CategoryAttribute("全边框"), DescriptionAttribute("设置单元格四边框颜色"), DisplayName("四边颜色"), PropertyOrder(27)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public Color AllBorderColor
        {
            get
            {
                return m_color;
            }
            set
            {
                if (m_color != value)
                    SetColor(value);
            }
        }
        
        /// <summary>
        /// 左边框粗细
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置左边框宽度"), DisplayName("左边框宽度"), PropertyOrder(16)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int LeftBorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 上边框粗细
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置上边框宽度"), DisplayName("上边框宽度"), PropertyOrder(19)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int TopBorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 右边框粗细
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置右边框宽度"), DisplayName("右边框宽度"), PropertyOrder(22)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int RightBorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 底边框粗细
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置下边框宽度"), DisplayName("下边框宽度"), PropertyOrder(25)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int BottomBorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 全边框粗细
        /// </summary>
        [CategoryAttribute("全边框"), DescriptionAttribute("设置单元格四边框宽度"), DisplayName("四边宽度"), PropertyOrder(28)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int AllBorderWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 左上右下对角线
        /// </summary>
        [CategoryAttribute("单元格对角线"), DescriptionAttribute("设置左上右下对角线(0代表没有对角线)"), DisplayName("左上右下对角线"), PropertyOrder(30)]
        [Browsable(true)]
        public int LeftTopRightBottom
        {
            get;
            set;
        }

        /// <summary>
        /// 左下右上对角线
        /// </summary>
         [CategoryAttribute("单元格对角线"), DescriptionAttribute("设置左下右上对角线(0代表没有对角线)"), DisplayName("左下右上对角线"), PropertyOrder(31)]
        [Browsable(true)]
        public int LeftBottomRightTop
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：左单元格边框样式(实线、虚线、点线)
        /// 作    者：jason.tang
        /// 完成时间：2012-12-28
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置左边框线条样式"), DisplayName("左边框线条样式"), PropertyOrder(17)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public System.Drawing.Drawing2D.DashStyle LeftBorderStyle
        {
            get;            
            set;
        }

        /// <summary>
        /// 属性说明：上单元格边框样式(实线、虚线、点线)
        /// 作    者：jason.tang
        /// 完成时间：2012-12-28
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置上边框线条样式"), DisplayName("上边框线条样式"), PropertyOrder(20)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public System.Drawing.Drawing2D.DashStyle TopBorderStyle
        {
            get;
            set;
        }
        
        /// <summary>
        /// 属性说明：右单元格边框样式(实线、虚线、点线)
        /// 作    者：jason.tang
        /// 完成时间：2012-12-28
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置右边框线条样式"), DisplayName("右边框线条样式"), PropertyOrder(23)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public System.Drawing.Drawing2D.DashStyle RightBorderStyle
        {
            get;
            set;
        }
        
        /// <summary>
        /// 属性说明：底单元格边框样式(实线、虚线、点线)
        /// 作    者：jason.tang
        /// 完成时间：2012-12-28
        /// </summary>
        [CategoryAttribute("单元格边框"), DescriptionAttribute("设置下边框线条样式"), DisplayName("下边框线条样式"), PropertyOrder(26)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public System.Drawing.Drawing2D.DashStyle BottomBorderStyle
        {
            get;
            set;
        }

        /// <summary>
        /// 全边框样式
        /// </summary>
        [CategoryAttribute("全边框"), DescriptionAttribute("设置单元格四边框线条样式"), DisplayName("四边线条样式"), PropertyOrder(29)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public System.Drawing.Drawing2D.DashStyle AllBorderStyle
        {
            get;
            set;
        }

        [EditorAttribute(typeof(Kingdee.CAPP.Common.DataGridViewHelp.CustomerEditor), typeof(System.Drawing.Design.UITypeEditor)), CategoryAttribute("明细单元格"), DescriptionAttribute("明细框列属性设置"), DisplayName("明细列"), PropertyOrder(35)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public object DetailProperty
        {
            get;
            set;
        }

        [CategoryAttribute("明细单元格"), DescriptionAttribute("设置序号列的步长"), DisplayName("步长"), PropertyOrder(33)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int SerialStep
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：间隔行数
        /// </summary>
        [CategoryAttribute("明细单元格"), DescriptionAttribute("间隔行数"), DisplayName("间隔行数"), PropertyOrder(34)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public int SpaceRows
        {
            get;
            set;
        }

        [EditorAttribute(typeof(Kingdee.CAPP.Common.DataGridViewHelp.SourceEditor), typeof(System.Drawing.Design.UITypeEditor)), CategoryAttribute("单元格基本属性"), DescriptionAttribute("对象的来源"), DisplayName("来源"), PropertyOrder(32)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public string CellSource
        {
            get;
            set;
        }

        /// <summary>
        /// 单元格宽度
        /// </summary>
        [Browsable(false)]
        public int CellWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 放大后的字体
        /// </summary>
        [Browsable(false)]
        public float ZoomFontSize
        {
            get;
            set;
        }

        /// <summary>
        /// Size旧值
        /// </summary>
        [Browsable(false)]
        public float OldSizeValue
        {
            get;
            set;
        }

        #endregion

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewTextBoxCellEx cell =
                (DataGridViewTextBoxCellEx)base.Clone();

            cell.TopBorderColor = this.TopBorderColor;
            cell.RightBorderColor = this.RightBorderColor;
            cell.BottomBorderColor = this.BottomBorderColor;
            cell.LeftBorderColor = this.LeftBorderColor;
            return cell;
        }

        private void WriteDebugInfo(string content)
        {
            string path = Application.StartupPath + @"\debug.txt";
            if (!File.Exists(path))
            {
                FileStream fs1 = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1);
                sw.WriteLine(content);//开始写入值
                sw.Close();
                fs1.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter sr = new StreamWriter(fs);
                sr.WriteLine(content);//开始写入值
                sr.Close();
                fs.Close();
            }
        }
        
        #region 重写基类绘制方法.

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (m_OwnerCell != null && m_OwnerCell.DataGridView == null)
                m_OwnerCell = null; //owner cell was removed.
  
            if (DataGridView == null
                || (m_OwnerCell == null && m_ColumnSpan == 1 && m_RowSpan == 1))
            {

                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle,
                        paintParts);
                //自定义绘制边框
                CustomerPaintBorder(rowIndex, ColumnIndex, graphics, cellBounds);

                //WriteDebugInfo("Paint");

                return;
            }            

            //WriteDebugInfo("No Paint");

            var ownerCell = this;
            var columnIndex = ColumnIndex;
            var columnSpan = m_ColumnSpan;
            var rowSpan = m_RowSpan;
            if (m_OwnerCell != null)
            {
                ownerCell = m_OwnerCell;
                columnIndex = m_OwnerCell.ColumnIndex;
                rowIndex = m_OwnerCell.RowIndex;
                columnSpan = m_OwnerCell.ColumnSpan;
                rowSpan = m_OwnerCell.RowSpan;
                value = m_OwnerCell.GetValue(rowIndex);
                errorText = m_OwnerCell.GetErrorText(rowIndex);
                cellState = m_OwnerCell.State;
                cellStyle = m_OwnerCell.GetInheritedStyle(null, rowIndex, true);
                formattedValue = m_OwnerCell.GetFormattedValue(value,
                    rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Display);
            }            

            //ownerCell.Style.SelectionBackColor = Color.Red;
            //ownerCell.p.Color = Color.Red;
            if (CellsRegionContainsSelectedCell(columnIndex, rowIndex, columnSpan, rowSpan))
                cellState |= DataGridViewElementStates.Selected;
            var cellBounds2 = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(
                this,
                cellBounds,
                DataGridView.SingleVerticalBorderAdded(),
                DataGridView.SingleHorizontalBorderAdded());
            clipBounds = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds2,
                DataGridView.SingleVerticalBorderAdded(),
                DataGridView.SingleHorizontalBorderAdded());
                       

            //clipBounds = new Rectangle(clipBounds.X, clipBounds.Y, clipBounds.Width - 1, clipBounds.Height - 1);
            //cellBounds2 = new Rectangle(cellBounds2.X, cellBounds2.Y, cellBounds2.Width - 1, cellBounds2.Height - 1);

            //Rectangle rect = clipBounds;
            //BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            //BufferedGraphics myBuffer = currentContext.Allocate(graphics, clipBounds);
            using (var g = DataGridView.CreateGraphics())//myBuffer.Graphics
            {
                //g.Clear(this.Style.SelectionBackColor);          
                g.SetClip(clipBounds);

                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;                

                //Paint the content.
                advancedBorderStyle = DataGridViewCellExHelper.AdjustCellBorderStyle(ownerCell);
                ownerCell.NativePaint(g, clipBounds, cellBounds2, rowIndex, cellState,
                    value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle,
                    paintParts & ~DataGridViewPaintParts.Border);
                //base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

                //合并单元格时，画OLE标识
                if (ownerCell != null)
                {
                    DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)ownerCell;
                    try
                    {
                        Image oleImage = Image.FromFile(Application.StartupPath + "\\Resources\\oleimg.png");
                        if (cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "1") ||
                            cell.CellContent == (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "2"))
                        {
                            Rectangle rectImage = new Rectangle(clipBounds.X, clipBounds.Y, oleImage.Width, oleImage.Height);
                            if (cell.RowSpan == 1 && cell.ColumnSpan == 1)
                            {
                                graphics.DrawImage(oleImage, rectImage);
                            }
                            else
                            {
                                g.DrawImage(oleImage, rectImage);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                
                //Paint the borders.
                if ((paintParts & DataGridViewPaintParts.Border) != DataGridViewPaintParts.None)
                {
                    var leftTopCell = ownerCell;
                    var advancedBorderStyle2 = new DataGridViewAdvancedBorderStyle
                    {
                        Left = advancedBorderStyle.Left,
                        Top = advancedBorderStyle.Top,
                        Right = DataGridViewAdvancedCellBorderStyle.None,
                        Bottom = DataGridViewAdvancedCellBorderStyle.None
                    };
                    leftTopCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle2);

                    var rightBottomCell = DataGridView[columnIndex + columnSpan - 1, rowIndex + rowSpan - 1] as DataGridViewTextBoxCellEx
                                          ?? this;
                    var advancedBorderStyle3 = new DataGridViewAdvancedBorderStyle
                    {
                        Left = DataGridViewAdvancedCellBorderStyle.None,
                        Top = DataGridViewAdvancedCellBorderStyle.None,
                        Right = advancedBorderStyle.Right,//DataGridViewAdvancedCellBorderStyle.None,
                        Bottom = advancedBorderStyle.Bottom//DataGridViewAdvancedCellBorderStyle.None
                    };
                    //clipBounds = new Rectangle(clipBounds.X, clipBounds.Y, clipBounds.Width - 1, clipBounds.Height - 1);
                    //cellBounds2 = new Rectangle(cellBounds2.X, cellBounds2.Y, cellBounds2.Width - 1, cellBounds2.Height - 1);
                    rightBottomCell.PaintBorder(g, clipBounds, cellBounds2, cellStyle, advancedBorderStyle3);
                }

                //myBuffer.Render();
                //myBuffer.Render(graphics);
                //g.Dispose();
                //myBuffer.Dispose();//释放资源      

                Color color = Color.Empty;
                Point p = ownerCell.SpanCell;
                bool isSpanCell = false;
                float width = 0;
                if (p.X >= 0 && p.Y >= 0)
                {
                    if (p.X >= DataGridView.Rows.Count || p.Y >= DataGridView.Columns.Count)
                    {
                        return;
                    }
                    DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)DataGridView.Rows[p.X].Cells[p.Y];
                    if (cell.RowSpan > 1 || cell.ColumnSpan > 1)
                    {                        
                        isSpanCell = true;
                        Rectangle rectLine = new Rectangle(clipBounds.X - 1, clipBounds.Y - 1, clipBounds.Width, clipBounds.Height);
                        //if (!m_color.IsEmpty)
                        //{
                        //    g.DrawRectangle(new Pen(m_color, width), rect);
                        //    color = m_color;
                        //    WriteDebugInfo(string.Format("ALL:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));
                        //}
                        //else
                        //{
                        //    g.DrawRectangle(new Pen(color, width), rect);
                        //    //WriteDebugInfo(string.Format("ALL:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));
                        //}

                        //左上右下
                        if (ownerCell.LeftTopRightBottom > 0)
                        {
                            Pen pen = new Pen(Color.Black, ownerCell.LeftTopRightBottom);
                            g.DrawLine(pen, rectLine.X - 1, rectLine.Y, rectLine.X + rectLine.Width, rectLine.Y + rectLine.Height - 1);
                        }
                        //左下右上
                        if (ownerCell.LeftBottomRightTop > 0)
                        {
                            Pen pen = new Pen(Color.Black, ownerCell.LeftBottomRightTop);
                            g.DrawLine(pen, rectLine.X - 1, rectLine.Y + rectLine.Height, rectLine.X + rectLine.Width, rectLine.Y - 1);
                        }


                        //左                        
                        color = cell.LeftBorderColor;
                        width = cell.LeftBorderWidth;
                        Point p1 = new Point(clipBounds.X - 1, clipBounds.Y);
                        Point p2 = new Point(clipBounds.X - 1, clipBounds.Y + clipBounds.Height);
                        g.DrawLine(new Pen(color, width), p1, p2);
                        
                        //上
                        color = cell.TopBorderColor;
                        width = cell.TopBorderWidth;
                        p1 = new Point(clipBounds.X, clipBounds.Y - 1);
                        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y - 1);
                        g.DrawLine(new Pen(color, width), p1, p2);

                        //右
                        color = cell.RightBorderColor;
                        width = cell.RightBorderWidth;
                        if (columnIndex != DataGridView.Columns.Count - 1)
                        {
                            //p1 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y);
                            //p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height);
                            //g.DrawLine(new Pen(color, width), p1, p2);

                            for (int i = 0; i < ownerCell.RowSpan; i++)
                            {
                                Rectangle rect = DataGridView.GetCellDisplayRectangle(ownerCell.ColumnIndex + ownerCell.ColumnSpan - 1, ownerCell.RowIndex + i, false);
                                DataGridViewTextBoxCellEx currCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[ownerCell.RowIndex + i].Cells[ownerCell.ColumnIndex + ownerCell.ColumnSpan - 1];
                                color = currCell.RightBorderColor;
                                width = currCell.RightBorderWidth;

                                p1 = new Point(rect.X + rect.Width - 1, rect.Y);
                                p2 = new Point(rect.X + rect.Width - 1, rect.Y + rect.Height);
                                g.DrawLine(new Pen(color, width), p1, p2);
                            }
                        }

                        //下
                        color = cell.BottomBorderColor;
                        width = cell.BottomBorderWidth;
                        if (rowIndex != DataGridView.Rows.Count - 1)
                        {
                            //p1 = new Point(clipBounds.X, clipBounds.Y + clipBounds.Height - 1);
                            //p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height - 1);
                            //g.DrawLine(new Pen(color, width), p1, p2);

                            for (int i = 0; i < ownerCell.ColumnSpan; i++)
                            {
                                Rectangle rect = DataGridView.GetCellDisplayRectangle(ownerCell.ColumnIndex + i, ownerCell.RowIndex + ownerCell.RowSpan - 1, false);
                                DataGridViewTextBoxCellEx currCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[ownerCell.RowIndex + ownerCell.RowSpan - 1].Cells[ownerCell.ColumnIndex + i];
                                color = currCell.BottomBorderColor;
                                width = currCell.BottomBorderWidth;

                                p1 = new Point(rect.X, rect.Y + rect.Height - 1);
                                p2 = new Point(rect.X + rect.Width - 1, rect.Y + rect.Height - 1);
                                g.DrawLine(new Pen(color, width), p1, p2);
                            }
                        }

                        //WriteDebugInfo("Step1");                       

                    }
                }
                if (!isSpanCell)
                {
                    //左
                    color = ownerCell.LeftBorderColor;
                    width = ownerCell.LeftBorderWidth;
                    Point p1 = new Point(clipBounds.X - 1, clipBounds.Y);
                    Point p2 = new Point(clipBounds.X - 1, clipBounds.Y + clipBounds.Height);
                    g.DrawLine(new Pen(color, width), p1, p2);

                    //上
                    color = ownerCell.TopBorderColor;
                    width = ownerCell.TopBorderWidth;
                    p1 = new Point(clipBounds.X, clipBounds.Y - 1);
                    p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y - 1);
                    g.DrawLine(new Pen(color, width), p1, p2);

                    //右
                    color = ownerCell.RightBorderColor;
                    width = ownerCell.RightBorderWidth;
                    if (columnIndex != DataGridView.Columns.Count - 1)
                    {
                        p1 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y);
                        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height);
                        g.DrawLine(new Pen(color, width), p1, p2);
                    }

                    //下
                    color = ownerCell.BottomBorderColor;
                    width = ownerCell.BottomBorderWidth;
                    if (rowIndex != DataGridView.Rows.Count - 1)
                    {
                        p1 = new Point(clipBounds.X, clipBounds.Y + clipBounds.Height - 1);
                        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height - 1);
                        g.DrawLine(new Pen(color, width), p1, p2);
                    }
                    //WriteDebugInfo("Step2");
                }
                //}
                //    //左
                //    Point p1 = new Point(clipBounds.X - 1, clipBounds.Y);
                //    Point p2 = new Point(clipBounds.X - 1, clipBounds.Y + clipBounds.Height);
                //    color = cell.LeftBorderColor;
                //    width = cell.LeftBorderWidth;
                //    if (color != Color.Empty && width > 0)
                //    {
                //        //if (color != Color.White)

                //        //g.DrawLine(new Pen(color, width), p1, p2);
                //        WriteDebugInfo(string.Format("Left:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));

                //        if (p.Y - 1 >= 0)
                //        {
                //            for (int i = 0; i < m_RowSpan; i++)
                //            {
                //                DataGridViewTextBoxCellEx preCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[p.X + i].Cells[p.Y - 1];
                //                preCell.RightBorderColor = color;
                //                preCell.RightBorderWidth = (int)width;
                //            }
                //        }
                //    }

                //    //右
                //    color = cell.RightBorderColor;
                //    width = cell.RightBorderWidth;
                //    if (color != Color.Empty && width > 0)
                //    {
                //        //if (color == Color.White)
                //        //    color = Color.Empty;
                //        p1 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y);
                //        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height);
                //        //g.DrawLine(new Pen(color, width), p1, p2);
                //        WriteDebugInfo(string.Format("Right:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));
                        
                //        if (p.Y + m_ColumnSpan <= DataGridView.Columns.Count)
                //        {
                //            for (int i = 0; i < m_RowSpan; i++)
                //            {
                //                DataGridViewTextBoxCellEx preCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[p.X + i].Cells[p.Y + m_ColumnSpan];
                //                preCell.LeftBorderColor = color;
                //                preCell.LeftBorderWidth = (int)width;
                //            }
                //        }
                //    }

                //    //上
                //    color = cell.TopBorderColor;
                //    width = cell.TopBorderWidth;
                //    if (color != Color.Empty && width > 0)
                //    {
                //        //if (color == Color.White)
                //        //    color = Color.Empty;
                //        p1 = new Point(clipBounds.X, clipBounds.Y);
                //        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y);
                //        //g.DrawLine(new Pen(color, width), p1, p2);
                //        WriteDebugInfo(string.Format("Top:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));

                //        if (p.X - 1 >= 0)
                //        {
                //            for (int i = 0; i < m_ColumnSpan; i++)
                //            {
                //                DataGridViewTextBoxCellEx preCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[p.X - 1].Cells[p.Y + i];
                //                preCell.BottomBorderColor = color;
                //                preCell.BottomBorderWidth = (int)width;
                //            }
                //        }
                //    }

                //    //下
                //    color = cell.BottomBorderColor;
                //    width = cell.BottomBorderWidth;
                //    if (color != Color.Empty && width > 0)
                //    {
                //        //if (color == Color.White)
                //        //    color = Color.Empty;
                //        p1 = new Point(clipBounds.X, clipBounds.Y + clipBounds.Height - 1);
                //        p2 = new Point(clipBounds.X + clipBounds.Width - 1, clipBounds.Y + clipBounds.Height - 1);
                //        //g.DrawLine(new Pen(color, width), p1, p2);
                //        WriteDebugInfo(string.Format("Bottom:A{0},R{1},G{2},B{3}", color.A.ToString(), color.R.ToString(), color.G.ToString(), color.B.ToString()));

                //        if (p.X + m_RowSpan <= DataGridView.Rows.Count)
                //        {
                //            for (int i = 0; i < m_ColumnSpan; i++)
                //            {
                //                DataGridViewTextBoxCellEx preCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[p.X + m_RowSpan].Cells[p.Y + i];
                //                preCell.TopBorderColor = color;
                //                preCell.TopBorderWidth = (int)width;
                //            }
                //        }
                //    }
                //}
                //WriteDebugInfo(string.Format("1:A{0},R{1},G{2},B{3}", m_color.A.ToString(), m_color.R.ToString(), m_color.G.ToString(), m_color.B.ToString()));
                //rect = new Rectangle(cellBounds2.X, cellBounds2.Y, cellBounds2.Width-1, cellBounds2.Height-1);
                //if (!m_color.IsEmpty)
                //{
                //    g.DrawRectangle(new Pen(m_color, 1), rect);
                //    color = m_color;
                //}
                //else
                //{
                //    g.DrawRectangle(new Pen(color, 1), rect);
                //}
                //WriteDebugInfo(string.Format("2:A{0},R{1},G{2},B{3}", m_color.A.ToString(), m_color.R.ToString(), m_color.G.ToString(), m_color.B.ToString()));
                //}
            }

        }

        private void NativePaint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        #endregion

        #region 设置合并

        private void SetColor(Color color)
        {
            isAllBorder = true;
            m_color = color;
        }

        private void SetSpan(int columnSpan, int rowSpan)
        {
            int prevColumnSpan = m_ColumnSpan;
            int prevRowSpan = m_RowSpan;
            m_ColumnSpan = columnSpan;
            m_RowSpan = rowSpan;

            if (DataGridView != null)
            {
                // clear.
                foreach (int rowIndex in Enumerable.Range(RowIndex, prevRowSpan))
                    foreach (int columnIndex in Enumerable.Range(ColumnIndex, prevColumnSpan))
                    {
                        var cell = DataGridView[columnIndex, rowIndex] as DataGridViewTextBoxCellEx;
                        if (cell != null)
                            cell.OwnerCell = null;
                    }

                // set.
                foreach (int rowIndex in Enumerable.Range(RowIndex, m_RowSpan))
                    foreach (int columnIndex in Enumerable.Range(ColumnIndex, m_ColumnSpan))
                    {
                        var cell = DataGridView[columnIndex, rowIndex] as DataGridViewTextBoxCellEx;
                        if (cell != null && cell != this)
                        {
                            if (cell.ColumnSpan > 1) cell.ColumnSpan = 1;
                            if (cell.RowSpan > 1) cell.RowSpan = 1;
                            cell.OwnerCell = this;
                        }
                    }

                OwnerCell = null;
                DataGridView.Invalidate();
            }
        }

        #endregion

        #region 编辑面板

        public override Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            if (m_OwnerCell == null
                && m_ColumnSpan == 1 && m_RowSpan == 1)
            {
                cellClip = new Rectangle(cellClip.X, cellClip.Y, cellClip.Width - 2, cellClip.Height - 2);
                cellBounds = new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 2, cellBounds.Height - 2);
                return base.PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
            }

            var ownerCell = this;
            if (m_OwnerCell != null)
            {
                var rowIndex = m_OwnerCell.RowIndex;
                cellStyle = m_OwnerCell.GetInheritedStyle(null, rowIndex, true);
                m_OwnerCell.GetFormattedValue(m_OwnerCell.Value, rowIndex, ref cellStyle, null, null, DataGridViewDataErrorContexts.Formatting);
                var editingControl = DataGridView.EditingControl as IDataGridViewEditingControl;
                if (editingControl != null)
                {
                    editingControl.ApplyCellStyleToEditingControl(cellStyle);
                    var editingPanel = DataGridView.EditingControl.Parent;
                    if (editingPanel != null)
                        editingPanel.BackColor = cellStyle.BackColor;
                }
                ownerCell = m_OwnerCell;
            }
            cellBounds = DataGridViewCellExHelper.GetSpannedCellBoundsFromChildCellBounds(
                this,
                cellBounds,
                singleVerticalBorderAdded,
                singleHorizontalBorderAdded);
            cellClip = DataGridViewCellExHelper.GetSpannedCellClipBounds(ownerCell, cellBounds, singleVerticalBorderAdded, singleHorizontalBorderAdded);
            cellClip = new Rectangle(cellClip.X, cellClip.Y, cellClip.Width - 2, cellClip.Height - 2);
            cellBounds = new Rectangle(cellBounds.X, cellBounds.Y, cellBounds.Width - 2, cellBounds.Height - 2);
            return base.PositionEditingPanel(
                 cellBounds, cellClip, cellStyle,
                 singleVerticalBorderAdded,
                 singleHorizontalBorderAdded,
                 ownerCell.InFirstDisplayedColumn(),
                 ownerCell.InFirstDisplayedRow());
        }

        protected override object GetValue(int rowIndex)
        {
            if (m_OwnerCell != null)
                return m_OwnerCell.GetValue(m_OwnerCell.RowIndex);
            return base.GetValue(rowIndex);
        }

        protected override bool SetValue(int rowIndex, object value)
        {
            if (m_OwnerCell != null)
                return m_OwnerCell.SetValue(m_OwnerCell.RowIndex, value);
            return base.SetValue(rowIndex, value);
        }

        #endregion

        #region 其他重载

        protected override void OnDataGridViewChanged()
        {
            base.OnDataGridViewChanged();

            if (DataGridView == null)
            {
                m_ColumnSpan = 1;
                m_RowSpan = 1;
            }
        }

        protected override Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            if (m_OwnerCell == null
                && m_ColumnSpan == 1 && m_RowSpan == 1)
            {
                return base.BorderWidths(advancedBorderStyle);
            }

            if (m_OwnerCell != null)
                return m_OwnerCell.BorderWidths(advancedBorderStyle);

            var leftTop = base.BorderWidths(advancedBorderStyle);
            var rightBottomCell = DataGridView[
                ColumnIndex + ColumnSpan - 1,
                RowIndex + RowSpan - 1] as DataGridViewTextBoxCellEx;
            var rightBottom = rightBottomCell != null
                ? rightBottomCell.NativeBorderWidths(advancedBorderStyle)
                : leftTop;
            return new Rectangle(leftTop.X, leftTop.Y, rightBottom.Width, rightBottom.Height);
        }

        private Rectangle NativeBorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            return base.BorderWidths(advancedBorderStyle);
        }

        protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (OwnerCell != null) return new Size(0, 0);
            var size = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);
            var grid = DataGridView;
            var width = size.Width - Enumerable.Range(ColumnIndex + 1, ColumnSpan - 1)
                                           .Select(index => grid.Columns[index].Width)
                                           .Sum();
            var height = size.Height - Enumerable.Range(RowIndex + 1, RowSpan - 1)
                                           .Select(index => grid.Rows[index].Height)
                                           .Sum();
            return new Size(width, height);
        }

        #endregion

        #region 私有方法

        private bool CellsRegionContainsSelectedCell(int columnIndex, int rowIndex, int columnSpan, int rowSpan)
        {
            if (DataGridView == null)
                return false;

            return (from col in Enumerable.Range(columnIndex, columnSpan)
                    from row in Enumerable.Range(rowIndex, rowSpan)
                    where DataGridView[col, row].Selected
                    select col).Any();
        }

        /// <summary>
        /// 方法说明：自定义边框绘制
        /// 作    者：jason.tang
        /// 完成时间：2012-12-28
        /// </summary>
        /// <param name="rowIndex">当前行索引</param>
        /// <param name="columnIndex">列索引</param>
        /// <param name="isSpan">是否合并单元格</param>
        /// <param name="graphics"></param>
        /// <param name="cellBounds">单元格矩形框</param>
        private void CustomerPaintBorder(int rowIndex, int columnIndex, Graphics graphics, Rectangle cellBounds)
        {
            DataGridViewTextBoxCellEx cell = (DataGridViewTextBoxCellEx)DataGridView.Rows[rowIndex].Cells[ColumnIndex];
            Point pt = cell.SpanCell;
            if (pt.X >= 0 && pt.Y >= 0)
            {
                DataGridViewTextBoxCellEx spCell = (DataGridViewTextBoxCellEx)DataGridView.Rows[pt.X].Cells[pt.Y];
                if (spCell.RowSpan > 1 || spCell.ColumnSpan > 1)
                {
                    return;
                }
            }

            //第一行，Y坐标加1
            int diffRow = rowIndex == 0 ? 1 : 0;
            //第一列，X坐标加1
            int diffColumn = columnIndex == 0 ? 1 : 0;

            //左
            if (LeftBorderWidth > 0)
            {
                //如果边线为白色则减一，否则不变
                //int diffWhite = LeftBorderColor == Color.White ? 1 : 0;
                p = new Pen(LeftBorderColor, LeftBorderWidth);
                p.DashStyle = LeftBorderStyle;
                graphics.DrawLine(p, cellBounds.X - 1, cellBounds.Y + diffRow, cellBounds.X - 1, cellBounds.Y + cellBounds.Height -2);
            }
            //上            
            if (TopBorderWidth > 0)
            {
                //如果边线为白色则减一，否则不变
                //int diffWhite = TopBorderColor == Color.White ? 1 : 0;
                p = new Pen(TopBorderColor, TopBorderWidth);
                p.DashStyle = TopBorderStyle;
                graphics.DrawLine(p, cellBounds.X + diffColumn, cellBounds.Y - 1, cellBounds.X + cellBounds.Width - 2, cellBounds.Y - 1);
            }
            //右
            if (RightBorderWidth > 0)
            {
                p = new Pen(RightBorderColor, RightBorderWidth);
                p.DashStyle = RightBorderStyle;
                graphics.DrawLine(p, cellBounds.X + cellBounds.Width - 1, cellBounds.Y + diffRow, cellBounds.X + cellBounds.Width - 1, cellBounds.Y + cellBounds.Height - 2);
            }
            //底
            if (BottomBorderWidth > 0)
            {
                p = new Pen(BottomBorderColor, BottomBorderWidth);
                p.DashStyle = BottomBorderStyle;
                graphics.DrawLine(p, cellBounds.X + diffColumn, cellBounds.Y + cellBounds.Height - 1, cellBounds.X + cellBounds.Width - 2, cellBounds.Y + cellBounds.Height - 1);
            }


            //左上右下
            if (LeftTopRightBottom > 0)
            {
                p = new Pen(Color.Black, LeftTopRightBottom);
                graphics.DrawLine(p, cellBounds.X - 1, cellBounds.Y, cellBounds.X + cellBounds.Width, cellBounds.Y + cellBounds.Height - 1);
            }
            //左下右上
            if (LeftBottomRightTop > 0)
            {
                p = new Pen(Color.Black, LeftBottomRightTop);
                graphics.DrawLine(p, cellBounds.X - 1, cellBounds.Y + cellBounds.Height, cellBounds.X + cellBounds.Width, cellBounds.Y - 1);
            }
            //WriteDebugInfo("Step3");
        }

        #endregion
    }

    /// <summary>
    /// 类型说明：DataGridViewCustomerCellStyle单元格样式扩展类
    /// 作   者：jason.tang
    /// 完成时间：2013-01-17
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class DataGridViewCustomerCellStyle : DataGridViewCellStyle
    {
        [CategoryAttribute("卡片属性"), DescriptionAttribute("设置卡片名称"), DisplayName("卡片名称"), PropertyOrder(10)]
        [ReadOnlyAttribute(true)]
        [Browsable(false)]
        public string CardName
        {
            get;
            set;
        }

        [CategoryAttribute("外表"), DescriptionAttribute("设置单元格的背景颜色"), DisplayName("背景颜色"), PropertyOrder(11)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        [CategoryAttribute("外表"), DescriptionAttribute("设置单元格的字体"), DisplayName("字体"), PropertyOrder(12)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [CategoryAttribute("外表"), DescriptionAttribute("设置单元格的字体颜色"), DisplayName("字体颜色"), PropertyOrder(13)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        [Browsable(false)]
        public new Color SelectionBackColor
        {
            get { return base.SelectionBackColor; }
            set { base.SelectionBackColor = value; }
        }

        [Browsable(false)]
        public new Color SelectionForeColor
        {
            get { return base.SelectionForeColor; }
            set { base.SelectionForeColor = value; }
        }

        [Browsable(false)]
        public new string Format
        {
            get { return base.Format; }
            set { base.Format = value; }
        }

        [Browsable(false)]
        public new object NullValue
        {
            get { return base.NullValue; }
            set { base.NullValue = value; }
        }

        /// <summary>
        /// 单元格类型：1-非明细单元格 2-明细单元格
        /// </summary>
        [Browsable(false)]
        public int CellType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否增加的空行 0-否 1-是
        /// </summary>
        [Browsable(false)]
        public int EmptyRow
        {
            get;
            set;
        }

        [CategoryAttribute("布局"), DescriptionAttribute("设置明细单元格的内容位置"), DisplayName("明细单元格对齐方式"), PropertyOrder(14)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new HorizontalAlignment RichAlignment
        {
            get;
            set;
        }

        [CategoryAttribute("布局"), DescriptionAttribute("设置单元格之外的内容位置"), DisplayName("单元格对齐方式"), PropertyOrder(14)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new DataGridViewContentAlignment Alignment
        {
            get { return base.Alignment; }
            set { base.Alignment = value; }
        }

        [CategoryAttribute("布局"), DescriptionAttribute("设置单元格的边距"), DisplayName("边距"), PropertyOrder(15)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new Padding Padding
        {
            get { return base.Padding; }
            set { base.Padding = value; }
        }

        [CategoryAttribute("布局"), DescriptionAttribute("设置单元格的内容显示方式"), DisplayName("是否换行"), PropertyOrder(16)]
        [ReadOnlyAttribute(false)]
        [Browsable(true)]
        public new DataGridViewTriState WrapMode
        {
            get { return base.WrapMode; }
            set { base.WrapMode = value; }
        }

        public override DataGridViewCellStyle Clone()
        {
            DataGridViewCustomerCellStyle style =
                (DataGridViewCustomerCellStyle)base.Clone();
            return style;
        }
    }

    /// <summary>
    /// 类型说明：DetailGridViewTextBoxColumn明细列扩展类
    /// 作   者：jason.tang
    /// 完成时间：2013-01-09
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class DetailGridViewTextBoxColumn : DataGridViewColumn
    {
        #region 隐藏基类继承的属性

        [Browsable(false)]
        public new string ToolTipText
        {
            get { return base.ToolTipText; }
            set { base.ToolTipText = value; }
        }

        [Browsable(false)]
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get
            {
                return base.DefaultCellStyle;
            }
            set
            {
                base.DefaultCellStyle = value;
            }
        }

        [Browsable(false)]
        public override bool Frozen
        {
            get
            {
                return base.Frozen;
            }
            set
            {
                base.Frozen = value;
            }
        }

        [Browsable(false)]
        public override bool ReadOnly
        {
            get
            {
                return base.ReadOnly;
            }
            set
            {
                base.ReadOnly = value;
            }
        }

        [Browsable(false)]
        public override DataGridViewTriState Resizable
        {
            get
            {
                return base.Resizable;
            }
            set
            {
                base.Resizable = value;
            }
        }               

        [Browsable(false)]
        public new DataGridViewColumnSortMode SortMode
        {
            get { return base.SortMode; }
            set { base.SortMode = value; }
        }

        [Browsable(false)]
        public new string DataPropertyName
        {
            get { return base.DataPropertyName; }
            set { base.DataPropertyName = value; }
        }

        [Browsable(false)]
        public new DataGridViewAutoSizeColumnMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set { base.AutoSizeMode = value; }
        }

        [Browsable(false)]
        public new int DividerWidth
        {
            get { return base.DividerWidth; }
            set { base.DividerWidth = value; }
        }

        [Browsable(false)]
        public new int MinimumWidth
        {
            get { return base.MinimumWidth; }
            set { base.MinimumWidth = value; }
        }

        [Browsable(false)]
        public new float FillWeight
        {
            get { return base.FillWeight; }
            set { base.FillWeight = value; }
        }

        #endregion

        #region 属性声明

        [CategoryAttribute("外表"), DescriptionAttribute("设置列头的内容"), DisplayName("列头"), PropertyOrder(10)]
        [ReadOnlyAttribute(false)]
        public new string HeaderText
        {
            get { return base.HeaderText; }
            set { base.HeaderText = value; }
        }

        [CategoryAttribute("外表"), DescriptionAttribute("设置列的宽度"), DisplayName("列宽"), PropertyOrder(11)]
        [ReadOnlyAttribute(false)]
        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }
        
        [Browsable(false)]
        public new bool Visible
        {
            get { return base.Visible; }
            set { base.Visible = value; }
        }

        [CategoryAttribute("列高级属性"), DescriptionAttribute("明细列高级属性[明细单元,自动序号,空明细列,隐藏明细列]"), DisplayName("高级属性"), PropertyOrder(12)]
        [ReadOnlyAttribute(false)]
        public ComboBoxSourceHelper.AdvanceProperty AdvanceProperty
        {
            get;
            set;
        }

        [CategoryAttribute("列高级属性"), DescriptionAttribute("当高级属性为自动序号列时，设置序号的步长"), DisplayName("步长"), PropertyOrder(13)]
        [Browsable(false)]
        public int SerialStep
        {
            get;
            set;
        }

        [Browsable(false)]
        public override ContextMenuStrip ContextMenuStrip
        {
            get
            {
                return base.ContextMenuStrip;
            }
            set
            {
                base.ContextMenuStrip = value;
            }
        }

        /// <summary>
        /// 属性说明：序号
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("序号"), DisplayName("序号"), PropertyOrder(14)]
        [ReadOnly(true)]
        public int SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：名称
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("名称"), DisplayName("名称"), PropertyOrder(8)]
        [ReadOnlyAttribute(false)]
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：列值
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("列值"), DisplayName("列值")]
        [Browsable(false)]
        public string ColumnValue
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：标识
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("标识"), DisplayName("标识"), PropertyOrder(9)]
        [ReadOnlyAttribute(false)]
        public string ColumnTag
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：栏数
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("栏数"), DisplayName("栏数"), PropertyOrder(15)]
        [ReadOnlyAttribute(false)]
        public int Lans
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：总行数
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("总行数"), DisplayName("总行数")]
        [ReadOnlyAttribute(false)]
        public string Rows
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：每工序行
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("每工序行"), DisplayName("每工序行"), PropertyOrder(16)]
        [ReadOnlyAttribute(false)]
        public int PerProcessRow
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：间隔行数
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("间隔行数"), DisplayName("间隔行数")]
        [ReadOnlyAttribute(false)]
        [Browsable(false)]
        public string SpaceRows
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：明细横隔线
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("明细横隔线"), DisplayName("明细横隔线"), PropertyOrder(17)]
        [ReadOnlyAttribute(false)]
        public bool DetailSplitLine
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：工序内显示明细横隔线
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("工序内显示明细横隔线"), DisplayName("工序内显示明细横隔线"), PropertyOrder(18)]
        [ReadOnlyAttribute(false)]
        public bool SplitLineInProcess
        {
            get;
            set;
        }

        private string _content = "文本";
        /// <summary>
        /// 属性说明：显示内容
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("显示内容"), DisplayName("显示内容"), PropertyOrder(19)]
        [ReadOnly(true)]
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private string _field = "文本";
        /// <summary>
        /// 属性说明：字段
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("字段"), DisplayName("字段"), PropertyOrder(20)]
        [ReadOnly(true)]
        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }

        /// <summary>
        /// 属性说明：样式
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("样式"), DisplayName("样式"), PropertyOrder(21)]
        [ReadOnlyAttribute(false)]
        public Kingdee.CAPP.Common.ComboBoxSourceHelper.CellStyle Type
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：长度
        /// </summary>
        [CategoryAttribute("列基本属性"), DescriptionAttribute("长度"), DisplayName("长度"), PropertyOrder(22)]
        [ReadOnlyAttribute(false)]
        public int Length
        {
            get;
            set;
        }

        /// <summary>
        /// 属性说明：来源
        /// </summary>
        [EditorAttribute(typeof(Kingdee.CAPP.Common.DataGridViewHelp.SourceEditor), typeof(System.Drawing.Design.UITypeEditor)), CategoryAttribute("列基本属性"), DescriptionAttribute("来源"), DisplayName("来源"), PropertyOrder(23)]
        [ReadOnlyAttribute(false)]
        public string Source
        {
            get;
            set;
        }

        #endregion

        //Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DetailGridViewTextBoxColumn column =
                (DetailGridViewTextBoxColumn)base.Clone();
            return column;
        }

        public DetailGridViewTextBoxColumn()
            : base(new DataGridViewRichTextBoxCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (!(value is DataGridViewRichTextBoxCell))
                    throw new InvalidCastException("CellTemplate must be a DataGridViewRichTextBoxCell");

                base.CellTemplate = value;
            }
        }
    }

    /// <summary>
    /// 类型说明：DataGridViewRichTextBoxCell类
    /// 作   者：jason.tang
    /// 完成时间：2013-03-01
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class DataGridViewRichTextBoxCell : DataGridViewTextBoxCell
    {
        private static readonly RichTextBox _editingControl = new RichTextBox();

        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewRichTextBoxEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
            set
            {
                base.ValueType = value;
            }
        }

        public override Type FormattedValueType
        {
            get
            {
                return typeof(string);
            }
        }

        /// <summary>
        /// 设置RichTextBox的值
        /// </summary>
        private static void SetRichTextBoxText(RichTextBox ctl, string text)
        {
            try
            {
                ctl.Rtf = text;
            }
            catch (ArgumentException)
            {
                ctl.Text = text;
            }
        }

        /// <summary>
        /// 将Rtf格式转为Image形式
        /// </summary>
        private Image GetRtfImage(int rowIndex, object value, bool selected, DataGridViewCellStyle cellStyle)
        {
            Size cellSize = GetSize(rowIndex);

            if (cellSize.Width < 1 || cellSize.Height < 1)
                return null;

            RichTextBox ctl = null;

            if (ctl == null)
            {
                ctl = _editingControl;
                ctl.Size = GetSize(rowIndex);
                SetRichTextBoxText(ctl, Convert.ToString(value));
            }

            if (ctl != null)
            {
                // Print the content of RichTextBox to an image.
                Size imgSize = new Size(cellSize.Width - 3, cellSize.Height);
                Image rtfImg = null;

                //if (selected)
                //{
                    // Selected cell state
                    //ctl.BackColor = DataGridView.DefaultCellStyle.SelectionBackColor;
                    //ctl.ForeColor = DataGridView.DefaultCellStyle.SelectionForeColor;

                if (selected)
                {
                    ctl.BackColor = cellStyle.SelectionBackColor;
                }
                else
                {
                    ctl.BackColor = cellStyle.BackColor;
                }
                ctl.ForeColor = cellStyle.ForeColor;
                ctl.Font = cellStyle.Font;

                if (cellStyle.Alignment.ToString().EndsWith("Left"))
                {
                    ctl.SelectionAlignment = HorizontalAlignment.Left;
                }
                else if (cellStyle.Alignment.ToString().EndsWith("Right"))
                {
                    ctl.SelectionAlignment = HorizontalAlignment.Right;
                }
                else if (cellStyle.Alignment.ToString().EndsWith("Center"))
                {
                    ctl.SelectionAlignment = HorizontalAlignment.Center;
                }

                    // Print image
                    rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);

                    // Restore RichTextBox
                    //ctl.BackColor = DataGridView.DefaultCellStyle.BackColor;
                    //ctl.ForeColor = DataGridView.DefaultCellStyle.ForeColor;
                //}
                //else
                //{
                    //rtfImg = RichTextBoxPrinter.Print(ctl, imgSize.Width, imgSize.Height);
                //}

                return rtfImg;
            }

            return null;
        }

        /// <summary>
        /// 初始化正在编辑的控件(RichTextBox)
        /// </summary>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            RichTextBox ctl = DataGridView.EditingControl as RichTextBox;
            ctl.ScrollBars = RichTextBoxScrollBars.None;
            ctl.Multiline = false;
            if (ctl != null)
            {
                RichTextBox rich = new RichTextBox();
                if (initialFormattedValue != null)
                {
                    try
                    {
                        rich.Rtf = initialFormattedValue.ToString();
                    }
                    catch
                    {
                        rich.Text = initialFormattedValue.ToString();
                    }
                }
                rich.BackColor = dataGridViewCellStyle.BackColor;
                rich.ForeColor = dataGridViewCellStyle.ForeColor;
                rich.Font = dataGridViewCellStyle.Font;
                if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Left"))
                {
                    rich.SelectionAlignment = HorizontalAlignment.Left;
                }
                else if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Right"))
                {
                    rich.SelectionAlignment = HorizontalAlignment.Right;
                }
                else if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Center"))
                {
                    rich.SelectionAlignment = HorizontalAlignment.Center;
                }

                //RichTextBox temp = new RichTextBox();
                //if(initialFormattedValue != null)
                //    temp.Rtf = initialFormattedValue.ToString();

                //rich.Text = temp.Text;
                initialFormattedValue = rich.Rtf;

                SetRichTextBoxText(ctl, Convert.ToString(initialFormattedValue));                
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            return value;
        }

        /// <summary>
        /// 重写Paint方法，绘制图片
        /// </summary>
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, null, null, errorText, cellStyle, advancedBorderStyle, paintParts);

            Image img = GetRtfImage(rowIndex, value, this.Selected, cellStyle);

            if (img != null)
                graphics.DrawImage(img, cellBounds.Left + 2, cellBounds.Top);
        }

        #region 处理编辑事件，从DataGridViewTextBoxCell复制

        private byte flagsState;

        protected override void OnEnter(int rowIndex, bool throughMouseClick)
        {
            base.OnEnter(rowIndex, throughMouseClick);

            if ((base.DataGridView != null) && throughMouseClick)
            {
                this.flagsState = (byte)(this.flagsState | 1);
            }
        }

        protected override void OnLeave(int rowIndex, bool throughMouseClick)
        {
            base.OnLeave(rowIndex, throughMouseClick);

            if (base.DataGridView != null)
            {
                this.flagsState = (byte)(this.flagsState & -2);
            }
        }

        protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (base.DataGridView != null)
            {
                Point currentCellAddress = base.DataGridView.CurrentCellAddress;

                if (((currentCellAddress.X == e.ColumnIndex) && (currentCellAddress.Y == e.RowIndex)) && (e.Button == MouseButtons.Left))
                {
                    if ((this.flagsState & 1) != 0)
                    {
                        this.flagsState = (byte)(this.flagsState & -2);
                    }
                    else if (base.DataGridView.EditMode != DataGridViewEditMode.EditProgrammatically)
                    {
                        base.DataGridView.BeginEdit(false);
                    }
                }
            }
        }

        public override bool KeyEntersEditMode(KeyEventArgs e)
        {
            return (((((char.IsLetterOrDigit((char)((ushort)e.KeyCode)) && ((e.KeyCode < Keys.F1) || (e.KeyCode > Keys.F24))) || ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.Divide))) || (((e.KeyCode >= Keys.OemSemicolon) && (e.KeyCode <= Keys.OemBackslash)) || ((e.KeyCode == Keys.Space) && !e.Shift))) && (!e.Alt && !e.Control)) || base.KeyEntersEditMode(e));
        }

        #endregion
    }

    /// <summary>
    /// 类型说明：DataGridViewRichTextBoxEditingControl类
    /// 作   者：jason.tang
    /// 完成时间：2013-03-01
    /// </summary>
    public class DataGridViewRichTextBoxEditingControl : RichTextBox, IDataGridViewEditingControl
    {
        private DataGridView _dataGridView;
        private int _rowIndex;
        private bool _valueChanged;

        public DataGridViewRichTextBoxEditingControl()
        {
            this.BorderStyle = BorderStyle.None;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            _valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
        }

        protected override bool IsInputKey(Keys keyData)
        {
            Keys keys = keyData & Keys.KeyCode;
            if (keys == Keys.Return)
            {
                return this.Multiline;
            }

            return base.IsInputKey(keyData);
        }

        #region IDataGridViewEditingControl Members

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.BackColor = dataGridViewCellStyle.BackColor;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Left"))
            {
                this.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Right"))
            {
                this.SelectionAlignment = HorizontalAlignment.Right;
            }
            else if (dataGridViewCellStyle.Alignment.ToString().EndsWith("Center"))
            {
                this.SelectionAlignment = HorizontalAlignment.Center;
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return _dataGridView;
            }
            set
            {
                _dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Rtf;
            }
            set
            {
                if (value is string)
                    this.Text = value as string;
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return _valueChanged;
            }
            set
            {
                _valueChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.Return:
                    if ((((keyData & (Keys.Alt | Keys.Control | Keys.Shift)) == Keys.Shift) && this.Multiline))
                    {
                        return true;
                    }
                    break;
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }

            return !dataGridViewWantsInputKey;
        }

        public Cursor EditingPanelCursor
        {
            get { return this.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Rtf;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }

        #endregion
    }

    /// <summary>
    /// 类型说明：RichTextBoxPrinter打印帮助类
    /// 作   者：jason.tang
    /// 完成时间：2013-03-01
    /// </summary>
    public class RichTextBoxPrinter
    {
        //Convert the unit used by the .NET framework (1/100 inch) 
        //and the unit used by Win32 API calls (twips 1/1440 inch)
        private const double anInch = 14.4;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE
        {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE
        {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        // Render the contents of the RichTextBox for printing
        //	Return the last character printed + 1 (printing start from this point for next page)
        public static int Print(IntPtr richTextBoxHandle, int charFrom, int charTo, PrintPageEventArgs e)
        {
            //Calculate the area to render and print
            RECT rectToPrint;
            rectToPrint.Top = (int)(e.MarginBounds.Top * anInch);
            rectToPrint.Bottom = (int)(e.MarginBounds.Bottom * anInch);
            rectToPrint.Left = (int)(e.MarginBounds.Left * anInch);
            rectToPrint.Right = (int)(e.MarginBounds.Right * anInch);

            //Calculate the size of the page
            RECT rectPage;
            rectPage.Top = (int)(e.PageBounds.Top * anInch);
            rectPage.Bottom = (int)(e.PageBounds.Bottom * anInch);
            rectPage.Left = (int)(e.PageBounds.Left * anInch);
            rectPage.Right = (int)(e.PageBounds.Right * anInch);

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = charTo;				//Indicate character from to character to 
            fmtRange.chrg.cpMin = charFrom;
            fmtRange.hdc = hdc;                    //Use the same DC for measuring and rendering
            fmtRange.hdcTarget = hdc;              //Point at printer hDC
            fmtRange.rc = rectToPrint;             //Indicate the area on page to print
            fmtRange.rcPage = rectPage;            //Indicate size of page

            IntPtr res = IntPtr.Zero;

            IntPtr wparam = IntPtr.Zero;
            wparam = new IntPtr(1);

            //Get the pointer to the FORMATRANGE structure in memory
            IntPtr lparam = IntPtr.Zero;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, lparam, false);

            //Send the rendered data for printing 
            res = SendMessage(richTextBoxHandle, EM_FORMATRANGE, wparam, lparam);

            //Free the block of memory allocated
            Marshal.FreeCoTaskMem(lparam);

            //Release the device context handle obtained by a previous call
            e.Graphics.ReleaseHdc(hdc);

            // Release and cached info
            SendMessage(richTextBoxHandle, EM_FORMATRANGE, (IntPtr)0, (IntPtr)0);

            //Return last + 1 character printer
            return res.ToInt32();
        }

        public static Image Print(RichTextBox ctl, int width, int height)
        {
            Image img = new Bitmap(width, height);
            float scale;

            using (Graphics g = Graphics.FromImage(img))
            {
                // --- Begin code addition D_Kondrad

                // HorizontalResolution is measured in pix/inch         
                scale = (float)(width * 100) / img.HorizontalResolution;
                width = (int)scale;

                // VerticalResolution is measured in pix/inch
                scale = (float)(height * 100) / img.VerticalResolution;
                height = (int)scale;

                // --- End code addition D_Kondrad

                Rectangle marginBounds = new Rectangle(0, 0, width, height);
                Rectangle pageBounds = new Rectangle(0, 0, width, height);
                PrintPageEventArgs args = new PrintPageEventArgs(g, marginBounds, pageBounds, null);

                Print(ctl.Handle, 0, ctl.Text.Length, args);
            }

            return img;
        }

    }
    
    public class DataGridViewTextBoxColumnEx : DataGridViewColumn
    {
        #region ctor
        public DataGridViewTextBoxColumnEx()
            : base(new DataGridViewTextBoxCellEx())
        {
        }
        #endregion
    }

}
