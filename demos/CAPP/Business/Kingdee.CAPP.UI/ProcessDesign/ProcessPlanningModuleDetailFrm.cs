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
using System.IO;

namespace Kingdee.CAPP.UI.ProcessDesign
{
    /// <summary>
    /// 类型说明：工艺规程模板查看界面
    /// 作    者：jason.tang
    /// 完成时间：2013-01-21
    /// </summary>
    public partial class ProcessPlanningModuleDetailFrm : BaseForm
    {
        #region 属性声明

        /// <summary>
        /// 行高
        /// </summary>
        private List<int> listRowHeight;

        /// <summary>
        /// 列宽
        /// </summary>
        private List<int> listColWidth;

        private int _breadth=0;

        private List<ProcessCardModule> _processPlanningModules;
        public List<ProcessCardModule> ProcessPlanningModules
        {
            get
            {
                return _processPlanningModules;
            }
            set
            {
                _processPlanningModules = value;
            }
        }

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string FormText { get; set; }

        #endregion

        public ProcessPlanningModuleDetailFrm()
        {
            InitializeComponent();
        }

        #region 界面控件事件

        private void ProcessPlanningModuleDetailFrm_Load(object sender, EventArgs e)
        {
            if (_processPlanningModules != null && _processPlanningModules.Count > 0)
            {
                GetPlanningModule();

                //this.Name = FormText;
                this.TabText = FormText;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：得到工艺规程模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-21
        /// </summary>
        private void GetPlanningModule()
        {
            DataGridView dgv = new DataGridView();
            Label lbl = new Label();
            //循环设置对应的工艺规程模板
            foreach (ProcessCardModule module in _processPlanningModules)
            {
                lbl = new Label();
                lbl.Text = module.Name;
                lbl.Dock = DockStyle.Top;
                pnlPlanningModule.Controls.Add(lbl);

                dgv = new DataGridView();
                dgv.Name = string.Format("dgv{0}", module.Id.ToString());
                dgv.RowHeadersVisible = false;
                dgv.ColumnHeadersVisible = false;                
                dgv.Dock = DockStyle.Top;
                dgv.AllowUserToResizeColumns = false;
                dgv.AllowUserToResizeRows = false;
                dgv.ReadOnly = true;
                dgv.ScrollBars = ScrollBars.None;
                dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(215, 228, 242);
                dgv.AutoGenerateColumns = false;

                pnlPlanningModule.Controls.Add(dgv);

                GetTemplate(module, dgv);
            }
        }

        /// <summary>
        /// 方法说明：根据模板ID打开模板
        /// 作    者：jason.tang
        /// 完成时间：2013-01-21
        /// </summary>
        /// <param name="moduleId">模板ID</param>
        /// <param name="dgv">操作的DataGrid</param>
        private void GetTemplate(ProcessCardModule module, DataGridView dgv)
        {            

            ProcessCardModuleBLL pcmDll = new ProcessCardModuleBLL();
            CardsXML cardmodule = new CardsXML();
            Model.ProcessCardModule pcm = new ProcessCardModule();

            string moduleId = module.Id.ToString();

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

            Card card = cardmodule.Cards.FirstOrDefault<Card>();
            
            int Width = Convert.ToInt32(card.Width);
            int Height = Convert.ToInt32(card.Height);
            _breadth = int.Parse(card.CardRange.Replace("A", ""));
            ResizeInitGridView(Width, Height, dgv);

            List<DataGridViewRow> listRow = new List<DataGridViewRow>();


            if (card.Rows == null || card.Rows.Length == 0)
            {
                return;
            }

            int rows = card.Rows.Length;
            int columns = card.Rows[0].Cells.Length;
            dgv.Rows.Clear();
            dgv.Columns.Clear();

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

            InitDataGridView(listHeight, listWidth, dgv);
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
                        cellEx = ((DataGridViewTextBoxCellEx)dgv.Rows[cell.PointX].Cells[cell.PointY]);
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
                        cellEx.CellContent = string.IsNullOrEmpty(cell.ContentType) ? (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), "0") :
                            (ComboBoxSourceHelper.CellContent)Enum.Parse(typeof(ComboBoxSourceHelper.CellContent), cell.ContentType);

                        cellEx.Style.WrapMode = (DataGridViewTriState)Enum.Parse(typeof(DataGridViewTriState), cell.WrapMode.ToString());
                        cellEx.CellSource = cell.CellSource;

                        //明细框单元格
                        if (cellEx.CellEditType == (ComboBoxSourceHelper.CellType)Enum.Parse(typeof(ComboBoxSourceHelper.CellType), "2"))
                        {
                            Rectangle rect = dgv.GetCellDisplayRectangle(cellEx.ColumnIndex, cellEx.RowIndex, false);
                            int top = rect.Y - 1;
                            int left = rect.X - 1;

                            List<DetailGridViewTextBoxColumn> dicColumns = new List<DetailGridViewTextBoxColumn>();
                            object objDetailProperty = cell.DetailCells;
                            //明细框
                            AddDetailGridView(top, left, objDetailProperty, dicColumns, dgv, cellEx);
                            cellEx.DetailProperty = dicColumns;
                        }

                    }
                }
                //读取图片
                if (card.ImageObjects != null && card.ImageObjects.Length > 0)
                {
                    foreach (ImageObject image in card.ImageObjects)
                    {
                        LoadImage(image, dgv);
                    }
                }
            }
            catch
            {
                dgv.Rows.Clear();
                dgv.Columns.Clear();
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
        private void LoadImage(ImageObject image, DataGridView datagridview)
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

            datagridview.Controls.Add(pic);
            pic.Location = new Point(image.LocationX, image.LocationY);

        }

        /// <summary>
        /// 方法说明：设定初始尺寸
        /// 作   者：jason.tang
        /// 完成时间：2012-12-18
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="_percent">百分比</param>
        private void ResizeInitGridView(int width, int height,  DataGridView dgv)
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


            int widPixel = MillimetersToPixelsWidth(width, true);
            int heightPixel = MillimetersToPixelsWidth(height, false);


            if((int)Math.Round(widPixel * percent) > pnlPlanningModule.Width)
                pnlPlanningModule.Width = (int)Math.Round(widPixel * percent);
            pnlPlanningModule.Height += (int)Math.Round(heightPixel * percent);
            dgv.Height = (int)Math.Round(heightPixel * percent);        

        }

        /// <summary>
        /// 方法说明：根据毫米数得到像素
        /// 作    者：jason.tang
        /// 完成时间：2013-01-21
        /// </summary>
        /// <param name="length">长度(mm)</param>
        /// <param name="widthOrHeight">宽或者高</param>
        /// <returns></returns>
        private int MillimetersToPixelsWidth(int length, bool widthOrHeight)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromHwnd(pnlPlanningModule.Handle);
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
        /// 完成时间：2012-12-17
        /// </summary>
        /// <param name="listRows">行数</param>
        /// <param name="listColumns">列数</param>
        /// <param name="isInit">是否新建的初始化</param>
        private void InitDataGridView(List<int> listRows, List<int> listColumns, DataGridView datagridview)
        {
            int index = 0;

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


            float pageSize = (float)_breadth;
            float size = _breadth < 4 ? 8f - _breadth * 1.5f : 4f - _breadth;
            Font defaultFont = this.Font;
            foreach (DataGridViewRow row in datagridview.Rows)
            {
                foreach (DataGridViewColumn col in datagridview.Columns)
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
                }
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
        private void AddDetailGridView(int top, int left, object objColumns, List<DetailGridViewTextBoxColumn> _dicColumns, DataGridView datagridview, DataGridViewTextBoxCellEx cellEx)
        {
            DataGridView dgv = new DataGridView();
            dgv.BackgroundColor = Color.White;

            #region 明细框位置设定

            dgv.Top = top;
            dgv.Left = left;

            CheckIsInSpanCells(dgv, datagridview, cellEx);

            dgv.Name = string.Format("dgv{0}-{1}", cellEx.RowIndex.ToString(), cellEx.ColumnIndex.ToString());

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
                        textBoxColumn.SpaceRows = cellEx.SpaceRows.ToString(); //cell.SpaceRows.ToString();
                        textBoxColumn.Tag = cell.Tag;
                        textBoxColumn.Type = string.IsNullOrEmpty(cell.Type) ? (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), "0") :
                            (ComboBoxSourceHelper.CellStyle)Enum.Parse(typeof(ComboBoxSourceHelper.CellStyle), cell.Type);
                        textBoxColumn.Visible = cell.AdvanceProperty != ((ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "3")).ToString();
                        textBoxColumn.AdvanceProperty = !string.IsNullOrEmpty(cell.AdvanceProperty) ? (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), cell.AdvanceProperty) :
                            (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "0");
                        textBoxColumn.SerialStep = cellEx.SerialStep; //cell.SerialStep;
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
                        col.SerialStep = cellEx.SerialStep;
                        col.SpaceRows = cellEx.SpaceRows.ToString();
                        listColumns.Add(col);
                    }
                }

            }

            if (datagridview.Controls.ContainsKey(dgv.Name) && objColumns != null)
            {
                SetDataGridViewColumns(listColumns, dgv.Name, datagridview);
                return;
            }

            dgv.Width = datagridview.Columns[datagridview.CurrentCell.ColumnIndex].Width;
            dgv.Height = datagridview.Rows[datagridview.CurrentCell.RowIndex].Height;

            #endregion

            #region 明细框：处理合并的单元格

            int colspan = (datagridview.CurrentCell as DataGridViewTextBoxCellEx).ColumnSpan;
            int rowspan = (datagridview.CurrentCell as DataGridViewTextBoxCellEx).RowSpan;

            if (colspan > 1)
            {
                for (int i = 1; i < colspan; i++)
                {
                    dgv.Width += datagridview.Columns[datagridview.CurrentCell.ColumnIndex + i].Width;
                }
            }
            if (rowspan > 1)
            {
                for (int i = 1; i < rowspan; i++)
                {
                    dgv.Height += datagridview.Rows[datagridview.CurrentCell.RowIndex + i].Height;
                }
            }

            #endregion

            #region 明细框列表属性配置

            Control[] controls = datagridview.Controls.Find(dgv.Name, false);
            if (controls.Length > 0)
            {
                dgv = (DataGridView)controls[0];
                dgv.Rows.Clear();
                dgv.Columns.Clear();
            }


            dgv.Columns.Add("", "");
            dgv.Columns.Add("", "");
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
            //dgv.RowHeightChanged += dgv_RowHeightChanged;
            datagridview.Controls.Add(dgv);
            if (listColumns.Count > 0)
            {
                SetDataGridViewColumns(listColumns, dgv.Name, datagridview);
            }

            #endregion
        }

        /// <summary>
        /// 方法说明：检查当前单元格是否包含在合并的单元格中
        /// 作    者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        private void CheckIsInSpanCells(DataGridView dgv, DataGridView datagridview, DataGridViewTextBoxCellEx cellEx)
        {
            //Point p = FindSpanCell();
            int rowIndex = cellEx.RowIndex;
            int colIndex = cellEx.ColumnIndex;

            int colspan = cellEx.ColumnSpan;
            int rowspan = cellEx.RowSpan;

            if (cellEx.RowIndex < rowIndex + rowspan &&
                cellEx.ColumnIndex < colIndex + colspan)
            {
                datagridview.CurrentCell = datagridview.Rows[rowIndex].Cells[colIndex];
                Rectangle rect = datagridview.GetCellDisplayRectangle(colIndex, rowIndex, false);
                dgv.Top = rect.Y - 1;
                dgv.Left = rect.X - 1;
            }
        }

        /// <summary>
        /// 方法说明：设定明细框列
        /// 作   者：jason.tang
        /// 完成时间：2013-01-05
        /// <param name="listColumns">明细列集合</param>
        /// <param name="gridName">明细Grid名称</param>
        private void SetDataGridViewColumns(List<DetailGridViewTextBoxColumn> listColumns, string gridName, DataGridView datagridview)
        {
            Control[] controls = datagridview.Controls.Find(gridName, false);

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
            foreach (DetailGridViewTextBoxColumn column in listColumns)
            {
                //if (column.Tag != null && !string.IsNullOrEmpty(column.Tag.ToString()))
                //{
                //    tag = column.Tag.ToString();
                //}
                column.Visible = column.AdvanceProperty != ((ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "3"));
                dgv.Columns.Add(column);
                totalWidth += column.Width;
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

        #endregion

    }
}
