using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Componect;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Common;
using System.Reflection;
using System.IO;

namespace Kingdee.CAPP.UI.ProcessDesign
{
    /// <summary>
    /// 类型说明：工艺规程查看界面
    /// 作    者：jason.tang
    /// 完成时间：2013-08-01
    /// </summary>
    public partial class ProcessPlanningDetailFrm : BaseForm
    {
        #region 变量声明

        private int cardWidth = 0;
        private int cardHeight = 0;
        private int cardBreadth = 0;

        private Font preFont = null;
        private Color preColor = Color.Empty;

        /// <summary>
        /// 界面卡片和
        /// </summary>
        private int pageCount = 1;

        #endregion

        #region 属性声明

        /// <summary>
        /// 物料/产品对象
        /// </summary>
        public object ModuleObject
        {
            get;
            set;
        }

        /// <summary>
        /// 行高
        /// </summary>
        private List<int> listRowHeight;

        /// <summary>
        /// 列宽
        /// </summary>
        private List<int> listColWidth;

        private int _breadth = 0;

        private List<ProcessCard> _processPlanningCards;
        public List<ProcessCard> ProcessPlanningCards
        {
            get
            {
                return _processPlanningCards;
            }
            set
            {
                _processPlanningCards = value;
            }
        }

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string FormText { get; set; }

        #endregion

        public ProcessPlanningDetailFrm()
        {
            InitializeComponent();
        }

        private void ProcessPlanningDetailFrm_Load(object sender, EventArgs e)
        {
            if (_processPlanningCards != null && _processPlanningCards.Count > 0)
            {
                GetPlanningCard();

                this.Name = "ReadOnly-" + FormText;
                this.TabText = FormText.Substring(0, FormText.IndexOf("-"));
            }
        }

        /// <summary>
        /// 方法说明：得到工艺规程卡片
        /// 作     者：jason.tang
        /// 完成时间：2013-08-01
        /// </summary>
        private void GetPlanningCard()
        {
            DataGridView dgv = new DataGridView();
            Label lbl = new Label();
            int index = 0;
            Point p = new Point();
            //循环设置对应的工艺规程卡片
            foreach (ProcessCard card in _processPlanningCards)
            {
                lbl = new Label();
                lbl.Text = card.Name;
                lbl.AutoSize = true;
                //lbl.Dock = DockStyle.Top;
                pnlPlanningCard.Controls.Add(lbl);

                dgv = new DataGridView();
                dgv.Name = string.Format("dgvCard{0}", card.ID.ToString());
                dgv.RowHeadersVisible = false;
                dgv.ColumnHeadersVisible = false;
                //dgv.Dock = DockStyle.Top;
                dgv.AllowUserToResizeColumns = false;
                dgv.AllowUserToResizeRows = false;
                dgv.ReadOnly = true;
                dgv.ScrollBars = ScrollBars.None;
                dgv.DefaultCellStyle.SelectionBackColor = dgv.DefaultCellStyle.BackColor;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.AutoGenerateColumns = false;
                pnlPlanningCard.Controls.Add(dgv);

                if (index > 0)
                    lbl.Location = new Point(p.X, p.Y + 10);

                GetCard(card, dgv);

                if (index > 0)
                {                    
                    dgv.Location = new Point(0, lbl.Location.Y + 20);
                }
                else
                {
                    dgv.Location = new Point(0, lbl.Location.Y +20);
                }

                p = GetPageNumber(dgv.Name);

                index++;
            }
        }

        /// <summary>
        /// 方法说明：设置页码、页数
        /// 作    者：jason.tang
        /// 完成时间：2013-02-18
        /// </summary>
        private Point GetPageNumber(string name)
        {
            Point p = new Point();
            List<DataGridView> listPanels = new List<DataGridView>();
            //遍历窗体控件，得到卡片的总数
            foreach (Control control in pnlPlanningCard.Controls)
            {
                if (control is DataGridView && control.Name.StartsWith(name))
                {
                    p = new Point(control.Location.X, control.Location.Y + control.Height);
                }
            }

            return p;
        }

        private void GetCard(ProcessCard _card, DataGridView datagridview)
        {
            ProcessCardBLL pcBll = new ProcessCardBLL();
            ProcessCard processCard = new ProcessCard();
            CardsXML cards = new CardsXML();
            //DataGridView datagridview = new DataGridView();
            Guid cardid = _card.ID;
            if (cardid != null)
            {
                processCard = pcBll.GetProcessCard(cardid);
                cards = processCard.Card;
                //if (!isNew)
                //    this.Tag = id;
            }

            if (cards == null)
                return;

            datagridview.Tag = processCard.Name;

            int index = 0;
            //Panel pn = new Panel();
            foreach (Card card in cards.Cards)
            {                
                    //pn = new Panel();
                    //pn.BorderStyle = BorderStyle.FixedSingle;
                    //pn.Width = pnlPlanningCard.Width;
                    //pn.Height = pnlPlanningCard.Height;
                    //pn.AutoScroll = false;

                    //pn.Name = string.Format("pnCard{0}@{1}", Guid.NewGuid().ToString(), pageCount + 1);

                if (index > 0)
                {
                    datagridview = new DataGridView();
                    datagridview.Name = string.Format("dgvCard{0}", card.Id.ToString());
                    datagridview.RowHeadersVisible = false;
                    datagridview.ColumnHeadersVisible = false;
                    //datagridview.Dock = DockStyle.Top;
                    datagridview.AllowUserToResizeColumns = false;
                    datagridview.AllowUserToResizeRows = false;
                    datagridview.ReadOnly = true;
                    datagridview.ScrollBars = ScrollBars.None;
                    datagridview.DefaultCellStyle.SelectionBackColor = datagridview.DefaultCellStyle.BackColor;
                    datagridview.DefaultCellStyle.SelectionForeColor = Color.Black;
                    datagridview.AutoGenerateColumns = false;
                    pnlPlanningCard.Controls.Add(datagridview);
                }
                
                index++;
                int Width = Convert.ToInt32(card.Width);
                int Height = Convert.ToInt32(card.Height);
                int breadth = int.Parse(card.CardRange.Replace("A", ""));

                cardWidth = Width;
                cardHeight = Height;
                cardBreadth = breadth;

                //if (datagridview.Name == "dgvCard")
                //{
                    ResizeControls(Width, Height, breadth, datagridview);
                //}

                List<DataGridViewRow> listRow = new List<DataGridViewRow>();

                if (card.Rows == null)
                    continue;

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
                            if (!string.IsNullOrEmpty(cellEx.CellSource) && ModuleObject != null)
                            {
                                foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
                                {
                                    if (pi.Name.ToLower() == cellEx.CellSource.ToLower())
                                    {
                                        cellEx.Value = pi.GetValue(ModuleObject, new object[] { });
                                        break;
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
                                AddDetailGridView(top, left, objDetailProperty, cellEx, datagridview, dicColumns);
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
                    return;
                }
            }

        }

        /// <summary>
        /// 方法说明：重置控件尺寸
        /// 作   者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="_breadth">纸张</param>
        private void ResizeControls(int width, int height, int _breadth, DataGridView datagridview)
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


            if ((int)Math.Round(widPixel * percent) > pnlPlanningCard.Width)
                pnlPlanningCard.Width = (int)Math.Round(widPixel * percent);
            pnlPlanningCard.Height += (int)Math.Round(heightPixel * percent);
            datagridview.Height = (int)Math.Round(heightPixel * percent);
            datagridview.Width = pnlPlanningCard.Width;
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
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(pnlPlanningCard.Handle);
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

        /// <summary>
        /// 方法说明：增加明细框Grid
        /// 作者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="top">上边距</param>
        /// <param name="left">下边距</param>
        /// <param name="objColumns">明细列集合</param>
        /// <param name="cellEx">单元格</param>
        private void AddDetailGridView(int top, int left, object objColumns, DataGridViewTextBoxCellEx cellEx, DataGridView datagridview, List<DetailGridViewTextBoxColumn> _dicColumns)
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
            if (objColumns != null)
            {
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
            //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly = true;
            dgv.ScrollBars = ScrollBars.None;
            dgv.BorderStyle = BorderStyle.None;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
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

            int colRange = 0;
            foreach (DetailGridViewTextBoxColumn column in listColumns)
            {
                string columnValue = ((DetailGridViewTextBoxColumn)column).ColumnValue;
                if (!string.IsNullOrEmpty(columnValue) && string.IsNullOrEmpty(column.Source))
                {
                    string[] strValues = columnValue.Split(new char[] { ',' });
                    int rowRange = 0;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        row.Cells[colRange].Value = strValues[rowRange];
                        rowRange++;
                    }
                }//来源不为空，则自动带出来源数据
                else if (!string.IsNullOrEmpty(column.Source) && ModuleObject != null)
                {
                    foreach (PropertyInfo pi in ModuleObject.GetType().GetProperties())
                    {
                        if (pi.Name.ToLower() == column.Source.ToLower() && pi.GetValue(ModuleObject, new object[] { }) != null)
                        {
                            columnValue = pi.GetValue(ModuleObject, new object[] { }).ToString();
                            break;
                        }
                    }
                    int rowRange = 0;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        row.Cells[colRange].Value = columnValue;
                        rowRange++;
                    }
                }

                colRange++;
            }

            //dgv.CellClick += dgv_CellClick;
            //dgv.CellDoubleClick += DataGridView_CellDoubleClick;
            //dgv.CellMouseDown += DataGridView_CellMouseDown;
            //dgv.EditingControlShowing += dgv_EditingControlShowing;
            //dgv.KeyDown += dgv_KeyDown;
            //dgv.CellBeginEdit += dgv_CellBeginEdit;

            //dgvTemp = dgv;
            SetSerialNumberColumn(dgv);
            datagridview.Controls.Add(dgv);

            #endregion
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
            List<DataGridView> listPanels = new List<DataGridView>();
            //遍历窗体控件，得到卡片的总数
            foreach (Control control in this.Controls)
            {
                if (control is DataGridView && control.Name.StartsWith("dgvCard"))
                {
                    range++;
                    listPanels.Add((DataGridView)control);
                    if (range < this.Controls.Count)
                    {
                        p = new Point(control.Location.X, control.Location.Y);
                    }
                }
            }

            //foreach (Panel pn in listPanels)
            //{
            //    if (pn.HasChildren)
            //    {
            //        string parentName = pn.Name;
            //        //遍历Panel内的控件，设置页码与页数
            //        foreach (Control control in pn.Controls)
            //        {
            //            if (control is DataGridView)
            //            {
            //                foreach (DataGridViewRow row in ((DataGridView)control).Rows)
            //                {
            //                    foreach (DataGridViewTextBoxCellEx cell in row.Cells)
            //                    {
            //                        if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "3"))  //页码
            //                        {
            //                            cell.Value = parentName.Contains("@") ? parentName.Substring(parentName.IndexOf("@") + 1) : "1";
            //                        }
            //                        else if (cell.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "4"))  //页数
            //                        {
            //                            cell.Value = pageCount;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            return p;
        }

        /// <summary>
        /// 方法说明：设置自动序号列
        /// 作    者：jason.tang
        /// 完成时间：2013-03-12
        /// </summary>
        /// <param name="dgv">当前DataGrid</param>
        private void SetSerialNumberColumn(DataGridView dgv)
        {
            foreach (DetailGridViewTextBoxColumn column in dgv.Columns)
            {
                //间隔行
                if (!string.IsNullOrEmpty(column.SpaceRows))
                {
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

                    for (int i = startIndex; i < dgv.Rows.Count; i += spaceRows)
                    {
                        //自动序号列
                        if (column.AdvanceProperty == (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1"))
                        {

                            realRow = ((int.Parse(pageNumber) - 1) * dgv.Rows.Count + i + 1);
                            //根据步长得到序号
                            dgv.Rows[i].Cells[column.Index].Value = realRow * serialStep;
                            //dgv.Rows[i].Cells[column.Index].Value = (spaceEqualStep ? realRow : realRow - realRow / spaceRows) * serialStep;                               

                        }
                    }
                }
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

            if (!File.Exists(image.ImagePath))
            {
                pic.Image = Properties.Resources.none_img;
            }
            else
            {
                pic.Image = Image.FromFile(image.ImagePath);
                pic.ImageLocation = image.ImagePath;
            }

            datagridview.Controls.Add(pic);
            pic.Location = new Point(image.LocationX, image.LocationY);

        }
    }
}
