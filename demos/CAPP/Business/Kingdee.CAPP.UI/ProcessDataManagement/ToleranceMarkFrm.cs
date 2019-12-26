using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：公差标注窗体
    /// 作      者：jason.tang
    /// 完成时间：2013-04-10
    /// </summary>
    public partial class ToleranceMarkFrm : BaseSkinForm
    {
        #region 变量和属性声明

        private PictureBox currPicture;
        private Dictionary<string, int> dicPictureWidth;
        private string startupPath = string.Empty;

        /// <summary>
        /// 公差标注图片
        /// </summary>
        private Image image;
        public Image ToleranceImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        #endregion

        #region 窗体控件事件

        public ToleranceMarkFrm()
        {
            InitializeComponent();
        }

        private void ToleranceMarkFrm_Load(object sender, EventArgs e)
        {
            Kingdee.CAPP.Common.ComboBoxSourceHelper com = new Kingdee.CAPP.Common.ComboBoxSourceHelper();
            com.BinderEnum<Kingdee.CAPP.Common.ComboBoxSourceHelper.ToleranceType>(this.comboToleranceType, 0);

            picStyle1.Click += PictureBox_Click;
            picStyle2.Click += PictureBox_Click;
            picStyle3.Click += PictureBox_Click;

            currPicture = picStyle1;
            startupPath = Application.StartupPath + "\\Resources\\size\\{0}";
        }    

        void PictureBox_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                currPicture = (PictureBox)sender;
                string index = currPicture.Name.Substring(currPicture.Name.Length - 1);
                foreach (Control ctrl in groupBox3.Controls)
                {
                    if (ctrl is Panel)
                    {
                        ctrl.BackColor = SystemColors.Control;
                        if(ctrl.Name.EndsWith(index))
                            ctrl.BackColor = Color.Orange;
                    }                    
                }
            }
        }

        //引用gdi32.dll API函数
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
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
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Graphics gp = currPicture.CreateGraphics();
            Bitmap ibitMap = new Bitmap(300, 300, gp);
            Graphics iBitMap_gr = Graphics.FromImage(ibitMap);
            IntPtr iBitMap_hdc = iBitMap_gr.GetHdc();
            IntPtr me_hdc = gp.GetHdc();
            BitBlt(iBitMap_hdc, 0, 0, 300, 300, me_hdc, 0, 0, 13369376);
            gp.ReleaseHdc(me_hdc);
            iBitMap_gr.ReleaseHdc(iBitMap_hdc);

            if (!dicPictureWidth.ContainsKey(currPicture.Name))
            {
                return;
            }

            if (currPicture.Name == picStyle3.Name)
            {
                pbTemp.Width = dicPictureWidth[currPicture.Name] - 20;
            }
            else
            {
                pbTemp.Width = dicPictureWidth[currPicture.Name] - 3;
            }
            pbTemp.Height = 22;
            pbTemp.Image = ibitMap;

            Color c = new Color();
            for (int i = 0; i < ibitMap.Width; i++)
            {
                for (int j = 0; j < ibitMap.Height; j++)
                {
                    c = ibitMap.GetPixel(i, j);
                    if (c.ToArgb() == this.BackColor.ToArgb())
                    {
                        c = Color.FromArgb(0, c); //如果是背景色就做全透明处理Alpha = 0；
                    }
                    ibitMap.SetPixel(i, j, c);
                }
                pbTemp.Refresh();
                pbTemp.Image = ibitMap;
            }

            Bitmap bmp = new Bitmap(pbTemp.Width, pbTemp.Height);
            pbTemp.DrawToBitmap(bmp, new Rectangle(0, 0, pbTemp.Width, pbTemp.Height));
            image = bmp;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void comboToleranceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboToleranceType.SelectedIndex == 0)
            {
                SetImageValue("shaft");
                picStyle3.Visible = true;
                panel3.Visible = true;
            }
            else if (comboToleranceType.SelectedIndex == 1)
            {
                SetImageValue("hole");
                picStyle3.Visible = true;
                panel3.Visible = true;
            }
            else if (comboToleranceType.SelectedIndex == 2)
            {
                SetImageValue("base-shaft");
                picStyle3.Visible = false;
                panel3.Visible = false;
            }
            else if (comboToleranceType.SelectedIndex == 3)
            {
                SetImageValue("base-hole");
                picStyle3.Visible = false;
                panel3.Visible = false;
            }
        }

        /// <summary>
        /// 单击单元格获取对应的值
        /// </summary>
        private void dgvTolerance_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex + 25 >= dgvTolerance.Columns.Count)
            {
                return;
            }
            object cellValue = dgvTolerance.Rows[e.RowIndex].Cells[e.ColumnIndex + 25].Value;

            if (cellValue != null && cellValue.ToString().Contains("&"))
            {
                string value = cellValue.ToString();
                txtToleranceCode.Text = value.Substring(value.IndexOf("&") + 1).Replace("-", "/").Replace(".png","");
                DrawImage(txtBasicSize.Text, txtToleranceCode.Text);                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置不同公差类型的图片表
        /// 作       者：jason.tang
        /// 完成时间：2013-04-10
        /// </summary>
        /// <param name="folder">文件夹名</param>
        private void SetImageValue(string folder)
        {
            #region 初始化表格数据源

            startupPath = Application.StartupPath + "\\Resources\\size\\{0}";
            DataTable dt = new DataTable();
            dt.Columns.Clear();
            dt.Rows.Clear();

            int rows = 13;
            int columns = 25;

            for (int i = 43; i <= 50; i++)
            {
                string name = string.Format("Column{0}", i);
                dgvTolerance.Columns[name].Visible = true;
            }

            if (folder.Contains("-") && folder.Contains("shaft"))
            {
                rows = 8;
                columns = 17;
                for (int i = 43; i <= 50; i++)
                {
                    string name = string.Format("Column{0}", i);
                    dgvTolerance.Columns[name].Visible = false;
                }
            }
            else if (folder.Contains("-") && folder.Contains("hole"))
            {
                rows = 8;
                columns = 21;

                for (int i = 47; i <= 50; i++)
                {
                    string name = string.Format("Column{0}", i);
                    dgvTolerance.Columns[name].Visible = false;
                }
            }

            for (int i = 1; i <= columns; i++)
            {
                dt.Columns.Add(string.Format("Column{0}", i));
            }

            for (int i = 0; i < rows; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < columns; j++)
                {
                    string prefix = string.Format("{0}-{1}", i, j);
                    
                    string fileName = GetFileName(string.Format(startupPath, folder), prefix);
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        dr[j] = fileName;
                    }
                    else
                    {
                        dr[j] = string.Format("{0}-{1}", i, j);
                    }
                }
                dt.Rows.Add(dr.ItemArray);
            }
            
            dgvTolerance.DataSource = dt;

            #endregion

            #region 根据单元格值(图片路径)设置图片

            foreach (DataGridViewRow row in dgvTolerance.Rows)
            {
                int cellIndex = 25;

                row.Height = 28;
                if (folder.Contains("-"))
                {
                    row.Height = 38;
                }

                foreach (DataGridViewCell cell in row.Cells)
                {
                    dgvTolerance.Columns[cell.ColumnIndex].Width = 33;

                    if (cellIndex >= row.Cells.Count && folder.Contains("-"))
                    {
                        break;
                    }

                    //设置图片单元格图片路径
                    if (cell is DataGridViewImageCell)
                    {                      
                        if (row.Cells[cellIndex].Value != null)
                        {
                            string path = string.Format(startupPath + @"\{1}", folder, row.Cells[cellIndex].Value.ToString());

                            if (File.Exists(path))
                            {
                                cell.Value = Image.FromFile(path);                                
                                cell.Style.SelectionBackColor = Color.Blue;
                            }
                            else
                            {
                                string empName = folder.Contains("-") ? "baseemp.png" : "empty.png";
                                cell.Value = Image.FromFile(string.Format(startupPath, empName));
                                cell.Style.SelectionBackColor = dgvTolerance.DefaultCellStyle.BackColor;
                            }
                        }
                    }

                    cellIndex++;
                }
            }

            #endregion

            for (int i = 18; i <= 25; i++)
            {
                string name = string.Format("Column{0}", i);
                if (dgvTolerance.Columns.Contains(name))
                {
                    dgvTolerance.Columns[name].Visible = false;
                }
            }
        }

        /// <summary>
        /// 方法说明：获取指定文件夹下以指定前缀开始的文件
        /// 作       者：jason.tang
        /// 完成时间：2013-04-09
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="prefix">前缀</param>
        /// <returns></returns>
        private string GetFileName(string path, string prefix)
        {
            string fileName = string.Empty;
            try
            {
                string[] files = Directory.GetFiles(path);//得到文件
                foreach (string file in files)//循环文件
                {
                    string exname = file.Substring(file.LastIndexOf(".") + 1);//得到后缀名
                    if (exname == "png")//如果后缀名为.png文件
                    {
                        FileInfo fi = new FileInfo(file);//建立FileInfo对象
                        if (fi.Name.StartsWith(prefix) && fi.Name.Substring(0, fi.Name.IndexOf("&")) == prefix)
                        {
                            return fi.Name; 
                        }
                    }
                }
            }
            catch
            {

            }

            return fileName;
        }

        /// <summary>
        /// 方法说明：按格式画出当前选中单元个的值
        /// 作      者：jason.tang
        /// 完成时间：2013-04-10
        /// </summary>
        /// <param name="basicSize">基本尺寸</param>
        /// <param name="code">公差代号</param>
        private void DrawImage(string basicSize, string code)
        {
            picStyle1.Refresh();
            picStyle2.Refresh();
            picStyle3.Refresh();
            dicPictureWidth = new Dictionary<string, int>();

            float picWidth = 0;

            Graphics g1 = picStyle1.CreateGraphics();          
            g1.DrawString(basicSize, txtBasicSize.Font, new SolidBrush(Color.Black), new Point(0, -2));
            float basewidth = g1.MeasureString(basicSize, txtBasicSize.Font).Width;
            int sizeWidth = 0;
            if (basewidth == 0)
            {
                basewidth = 1;
            }
            else
            {
                sizeWidth = (int)Math.Round(basewidth) - 4;
            }
            g1.DrawString(code, txtToleranceCode.Font, new SolidBrush(Color.Black), new Point(sizeWidth, -2));

            picWidth = basewidth + g1.MeasureString(code, txtToleranceCode.Font).Width;
            dicPictureWidth.Add(picStyle1.Name, (int)Math.Round(picWidth));

            Graphics g2 = picStyle2.CreateGraphics();
            if (comboToleranceType.SelectedIndex <= 1)
            {
                g2.DrawString(basicSize, txtBasicSize.Font, new SolidBrush(Color.Black), new Point(0, 3));
                g2.DrawString(txtTopOffset.Text, txtTopOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth, -2));
                g2.DrawString(txtBottomOffset.Text, txtBottomOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth, 8));

                string strOffset = txtTopOffset.Text.Length > txtBottomOffset.Text.Length ? txtTopOffset.Text : txtBottomOffset.Text;
                picWidth = g2.MeasureString(basicSize, txtBasicSize.Font).Width + g2.MeasureString(strOffset, txtTopOffset.Font).Width;
                dicPictureWidth.Add(picStyle2.Name, (int)Math.Round(picWidth));

                Graphics g3 = picStyle3.CreateGraphics();
                g3.DrawString(basicSize, txtBasicSize.Font, new SolidBrush(Color.Black), new Point(0, 3));
                g3.DrawString(code, txtToleranceCode.Font, new SolidBrush(Color.Black), new Point(sizeWidth, 3));
                basewidth += g1.MeasureString(code, txtToleranceCode.Font).Width;
                if (basewidth == 0)
                {
                    basewidth = 1;
                }
                else
                {
                    sizeWidth = (int)Math.Round(basewidth) - 8;
                }
                float offset = 0;
                if (!string.IsNullOrEmpty(txtTopOffset.Text) || !string.IsNullOrEmpty(txtBottomOffset.Text))
                {
                    g3.DrawString("(", txtBottomOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth, 3));
                    g3.DrawString(txtTopOffset.Text, txtTopOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth + 3, -2));
                    g3.DrawString(txtBottomOffset.Text, txtBottomOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth + 3, 8));

                    float top = g3.MeasureString(txtTopOffset.Text, txtTopOffset.Font).Width;
                    float bottom = g3.MeasureString(txtBottomOffset.Text, txtBottomOffset.Font).Width;

                    offset = top > bottom ? top : bottom;
                    sizeWidth += (int)Math.Round(offset) - 3;
                    g3.DrawString(")", txtBottomOffset.Font, new SolidBrush(Color.Black), new Point(sizeWidth, 3));

                    offset = g3.MeasureString("(", txtBottomOffset.Font).Width + offset + g3.MeasureString(")", txtBottomOffset.Font).Width;
                }
                picWidth = basewidth + offset;
                dicPictureWidth.Add(picStyle3.Name, (int)Math.Round(picWidth));
            }
            else
            {
                g2.DrawString(basicSize, txtBasicSize.Font, new SolidBrush(Color.Black), new Point(0, 3));
                string top = code.Substring(0, code.IndexOf("/"));
                string bottom = code.Substring(code.IndexOf("/") + 1);

                float topWid = g2.MeasureString(top, txtToleranceCode.Font).Width;
                float bottomWid = g2.MeasureString(bottom, txtToleranceCode.Font).Width;

                float offset = topWid > bottomWid ? topWid : bottomWid;
                int lineWidth = (int)Math.Round(offset)  - 2;


                g2.DrawString(top, txtToleranceCode.Font, new SolidBrush(Color.Black), new Point(sizeWidth, -2));
                g2.DrawLine(new Pen(Color.Black), new Point(sizeWidth + 2, 11), new Point(sizeWidth + lineWidth, 11));
                g2.DrawString(bottom, txtToleranceCode.Font, new SolidBrush(Color.Black), new Point(sizeWidth, 10));

                picWidth = g2.MeasureString(basicSize, txtBasicSize.Font).Width + offset;
                dicPictureWidth.Add(picStyle2.Name, (int)Math.Round(picWidth));
            }

        }

        #endregion
        
    }
}
