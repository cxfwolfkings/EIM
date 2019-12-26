using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：形位公差界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public partial class ShapeToleranceFrm : BaseSkinForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 形位公差标图片
        /// </summary>
        private Image image;
        public Image ShapeImage
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

        public ShapeToleranceFrm()
        {
            InitializeComponent();
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

        /// <summary>
        /// 确定
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 绘制形位公差样式

            pbTemp.Width = 500;
            pbTemp.Refresh();            
            float totalWidth = 0;
            Graphics g = pbTemp.CreateGraphics();
            //固定位：F0
            g.DrawString("F0", txtFirst.Font, new SolidBrush(Color.Black), -2, 0);
            float width = g.MeasureString("F0", txtFirst.Font).Width;
            totalWidth += width;            
            int startX = (int)Math.Round(width);
            //当前位后面还有不为空的位则画线条
            if (!string.IsNullOrEmpty(txtFirst.Text) || !string.IsNullOrEmpty(txtSecond.Text) ||
                !string.IsNullOrEmpty(txtThird.Text) || !string.IsNullOrEmpty(txtFourth.Text))
            {
                g.DrawLine(new Pen(Color.Black), new Point(startX, 0), new Point(startX, pbTemp.Height));
            }
            else
            {
                PictureBoxToBitmap(totalWidth, g);
                return;
            }            
            g.DrawString(txtFirst.Text, txtFirst.Font, new SolidBrush(txtFirst.ForeColor), new Point(startX, 0));
            
            width = g.MeasureString(txtFirst.Text, txtFirst.Font).Width;
            if (width == 0)
            {
                width = 2;
            }
            totalWidth += width;
            startX += (int)Math.Round(width);
            if (!string.IsNullOrEmpty(txtSecond.Text) || !string.IsNullOrEmpty(txtThird.Text) || !string.IsNullOrEmpty(txtFourth.Text))
            {
                g.DrawLine(new Pen(Color.Black), new Point(startX, 0), new Point(startX, pbTemp.Height));
            }
            else
            {
                PictureBoxToBitmap(totalWidth, g);
                return;
            }
            g.DrawString(txtSecond.Text, txtSecond.Font, new SolidBrush(txtSecond.ForeColor), new Point(startX, 0));

            width = g.MeasureString(txtSecond.Text, txtSecond.Font).Width;
            if (width == 0)
            {
                width = 2;
            }
            totalWidth += width;
            startX += (int)Math.Round(width);
            if (!string.IsNullOrEmpty(txtThird.Text) || !string.IsNullOrEmpty(txtFourth.Text))
            {
                g.DrawLine(new Pen(Color.Black), new Point(startX, 0), new Point(startX, pbTemp.Height));
            }
            else
            {
                PictureBoxToBitmap(totalWidth, g);
                return;
            }
            g.DrawString(txtThird.Text, txtThird.Font, new SolidBrush(txtThird.ForeColor), new Point(startX, 0));

            width = g.MeasureString(txtThird.Text, txtThird.Font).Width;
            if (width == 0)
            {
                width = 2;
            }
            totalWidth += width;
            startX += (int)Math.Round(width);
            if (!string.IsNullOrEmpty(txtFourth.Text))
            {
                g.DrawLine(new Pen(Color.Black), new Point(startX, 0), new Point(startX, pbTemp.Height));
            }
            else
            {
                PictureBoxToBitmap(totalWidth, g);
                return;
            }
            totalWidth += g.MeasureString(txtFourth.Text, txtFourth.Font).Width;
            g.DrawString(txtFourth.Text, txtFourth.Font, new SolidBrush(txtFourth.ForeColor), new Point(startX, 0));
            PictureBoxToBitmap(totalWidth, g);

            #endregion
        }

        /// <summary>
        /// 取消
        /// </summary>
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
        /// 方法说明：绘制指定区域
        /// 作    者：jason.tang
        /// 完成时间：2013-02-05
        /// </summary>
        /// <param name="totalWidth">总宽度</param>
        private void PictureBoxToBitmap(float totalWidth, Graphics g)
        {
            pbTemp.Width = (int)Math.Round(totalWidth);

            pbResult.Width = pbTemp.Width;
            pbResult.Height = pbTemp.Height;
            pbResult.Image = pbTemp.Image;

            Bitmap ibitMap = new Bitmap(pbResult.Width, pbResult.Height, g);
            Graphics iBitMap_gr = Graphics.FromImage(ibitMap);
            IntPtr iBitMap_hdc = iBitMap_gr.GetHdc();
            IntPtr me_hdc = g.GetHdc();
            BitBlt(iBitMap_hdc, 0, 0, pbResult.Width, pbResult.Height, me_hdc, 0, 0, 13369376);
            g.ReleaseHdc(me_hdc);
            iBitMap_gr.ReleaseHdc(iBitMap_hdc);

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
                pbResult.Refresh();
                pbResult.Image = ibitMap;
            }

            Bitmap bmp = new Bitmap(pbResult.Width, pbResult.Height);
            pbResult.DrawToBitmap(bmp, new Rectangle(0, 0, pbResult.Width, pbResult.Height));
            image = bmp;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #endregion

    }
}
