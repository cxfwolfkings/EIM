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
    /// 类型说明：上下标输入界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-04
    /// </summary>
    public partial class SuperscriptAndSubscriptFrm : BaseSkinForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 上下标图片
        /// </summary>
        private Image image;
        public Image ScriptImage
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

        #region 界面控件事件

        public SuperscriptAndSubscriptFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 显示/隐藏分割线
        /// </summary>
        private void cbSplit_CheckedChanged(object sender, EventArgs e)
        {
            lklblSplit1.Visible = cbSplit.Checked;
            lklblSplit2.Visible = lklblSplit1.Visible;
            lklblSplit3.Visible = lklblSplit1.Visible;
        }

        /// <summary>
        /// 展示/隐藏第二部分
        /// </summary>
        private void btnTwo_Click(object sender, EventArgs e)
        {
            this.Width = btnTwo.Text == ">>" ? 357 : 185;
            btnTwo.Text = btnTwo.Text == ">>" ? "<<" : ">>";
        }

        /// <summary>
        /// 展示/隐藏第三部分
        /// </summary>
        private void btnThree_Click(object sender, EventArgs e)
        {
            this.Width = btnThree.Text == ">>" ? 536 : 357;
            btnThree.Text = btnThree.Text == ">>" ? "<<" : ">>";
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Graphics gp = pnBody.CreateGraphics();
            int picWidth = 0;

            #region 绘制上下标数

            gp.DrawString(txtBase1.Text, txtBase1.Font, new SolidBrush(Color.Black), new Point(40, 140));
            //基准数的实际宽度
            float basewidth = gp.MeasureString(txtBase1.Text, txtBase1.Font).Width;
            int startX = 40 + (int)Math.Round(basewidth);
            gp.DrawString(txtSup1.Text, txtSup1.Font, new SolidBrush(Color.Black), new Point(startX - 3, 135));
            gp.DrawString(txtSub1.Text, txtSub1.Font, new SolidBrush(Color.Black), new Point(startX - 3, 146));
            picWidth += (int)Math.Round(basewidth);
            //上标数的实际宽度
            float supWidth = gp.MeasureString(txtSup1.Text, txtSup1.Font).Width;
            //下标数的实际宽度
            float subWidth = gp.MeasureString(txtSub1.Text, txtSub1.Font).Width;
            //取宽度大的那个做为分数线的长度
            float scrWidth = supWidth > subWidth ? supWidth : subWidth;
            picWidth += (int)Math.Round(scrWidth);
            int endX = startX + (int)Math.Round(scrWidth) - 5;

            if (cbSplit.Checked)
            {
                gp.DrawLine(new Pen(lklblSplit1.LinkColor), new Point(startX, 148), new Point(endX, 148));
            }

            basewidth = gp.MeasureString(txtBase2.Text, txtBase2.Font).Width;
            if (basewidth == 0)
            {
                basewidth = 1;
            }
            picWidth += (int)Math.Round(basewidth);
            gp.DrawString(txtBase2.Text, txtBase2.Font, new SolidBrush(Color.Black), new Point(endX, 140));
            startX = endX + (int)Math.Round(basewidth);
            gp.DrawString(txtSup2.Text, txtSup2.Font, new SolidBrush(Color.Black), new Point(startX - 3, 135));
            gp.DrawString(txtSub2.Text, txtSub2.Font, new SolidBrush(Color.Black), new Point(startX - 3, 146));

            supWidth = gp.MeasureString(txtSup2.Text, txtSup2.Font).Width;
            subWidth = gp.MeasureString(txtSub2.Text, txtSub2.Font).Width;
            scrWidth = supWidth > subWidth ? supWidth : subWidth;
            picWidth += (int)Math.Round(scrWidth);
            endX = startX + (int)Math.Round(scrWidth) - 5;
            if (cbSplit.Checked)
            {
                gp.DrawLine(new Pen(lklblSplit2.LinkColor), new Point(startX, 148), new Point(endX, 148));
            }

            basewidth = gp.MeasureString(txtBase3.Text, txtBase3.Font).Width;
            if (basewidth == 0)
            {
                basewidth = 1;
            }
            picWidth += (int)Math.Round(basewidth);
            gp.DrawString(txtBase3.Text, txtBase3.Font, new SolidBrush(Color.Black), new Point(endX, 140));
            startX = endX + (int)Math.Round(basewidth);
            gp.DrawString(txtSup3.Text, txtSup3.Font, new SolidBrush(Color.Black), new Point(startX - 3, 135));
            gp.DrawString(txtSub3.Text, txtSub3.Font, new SolidBrush(Color.Black), new Point(startX - 3, 146));

            supWidth = gp.MeasureString(txtSup3.Text, txtSup3.Font).Width;
            subWidth = gp.MeasureString(txtSub3.Text, txtSub3.Font).Width;
            scrWidth = supWidth > subWidth ? supWidth : subWidth;
            picWidth += (int)Math.Round(scrWidth);
            endX = startX + (int)Math.Round(scrWidth) - 5;
            if (cbSplit.Checked)
            {
                gp.DrawLine(new Pen(lklblSplit3.LinkColor), new Point(startX, 148), new Point(endX, 148));
            }

            #endregion

            #region 抓取预览区域的图片

            //Graphics gp = this.CreateGraphics();
            Bitmap ibitMap = new Bitmap(300, 300, gp);
            Graphics iBitMap_gr = Graphics.FromImage(ibitMap);
            IntPtr iBitMap_hdc = iBitMap_gr.GetHdc();
            IntPtr me_hdc = gp.GetHdc();
            BitBlt(iBitMap_hdc, -40, -136, 300, 300, me_hdc, 0, 0, 13369376);
            gp.ReleaseHdc(me_hdc);
            iBitMap_gr.ReleaseHdc(iBitMap_hdc);

            pbTemp.Width = picWidth - 2;
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

            #endregion

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void SuperscriptAndSubscriptFrm_Load(object sender, EventArgs e)
        {
            this.txtBase1.KeyPress += TextBox_KeyPress;
            this.txtBase2.KeyPress += TextBox_KeyPress;
            this.txtBase3.KeyPress += TextBox_KeyPress;

            this.txtSup1.KeyPress += TextBox_KeyPress;
            this.txtSup2.KeyPress += TextBox_KeyPress;
            this.txtSup3.KeyPress += TextBox_KeyPress;

            this.txtSub1.KeyPress += TextBox_KeyPress;
            this.txtSub2.KeyPress += TextBox_KeyPress;
            this.txtSub3.KeyPress += TextBox_KeyPress;
        }

        void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

    }
}
