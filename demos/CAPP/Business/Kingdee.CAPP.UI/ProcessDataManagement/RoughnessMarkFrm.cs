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
    /// 类型说明：粗糙度标注界面
    /// 作    者：jason.tang
    /// 完成时间：2013-02-27
    /// </summary>
    public partial class RoughnessMarkFrm : BaseSkinForm
    {
        #region 变量声明

        private Graphics g;

        private int textWidth = 0;

        private Image image;
        public Image RoughImage
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

        public RoughnessMarkFrm()
        {
            InitializeComponent();
        }

        private void RoughnessMarkFrm_Load(object sender, EventArgs e)
        {
            Kingdee.CAPP.Common.ComboBoxSourceHelper com = new Kingdee.CAPP.Common.ComboBoxSourceHelper();
            com.BinderEnum<Kingdee.CAPP.Common.ComboBoxSourceHelper.RoughnessSymbolValue>(this.cmbSymbolValue, 0);

            double stand = 0.025;
            //cmbValue.Items.Add("无");
            for (int i = 0; i < 16; i++)
            {
                if (i == 0)
                {
                    cmbValue.Items.Add("0.012");
                }
                else
                {
                    cmbValue.Items.Add(stand.ToString());
                    stand *= 2;
                    if (i == 8 || i == 9)
                    {
                        stand -= 0.1;
                    }
                }
            }

            g = pbPreview.CreateGraphics();
        }

        /// <summary>
        /// 显示符号选择
        /// </summary>
        private void btnShowSymbol_Click(object sender, EventArgs e)
        {
            if (pnSymbol.Visible)
                pnSymbol.Visible = false;
            else
                pnSymbol.Visible = true;
        }

        /// <summary>
        /// 符号1
        /// </summary>
        private void btnSymbol1_Click(object sender, EventArgs e)
        {
            SetButtonImage(sender);
            cmbValue.SelectedIndex = 8;
        }

        /// <summary>
        /// 符号2
        /// </summary>
        private void btnSymbol2_Click(object sender, EventArgs e)
        {
            SetButtonImage(sender);
            cmbValue.SelectedIndex = 8;
        }

        /// <summary>
        /// 符号3
        /// </summary>
        private void btnSymbol3_Click(object sender, EventArgs e)
        {
            SetButtonImage(sender);
            cmbValue.Text = string.Empty;
            cmbValue.SelectedItem = null;
        }
              
        /// <summary>
        /// 符号值变化
        /// </summary>
        private void cmbSymbolValue_SelectedValueChanged(object sender, EventArgs e)
        {
            SetSymbolValue();
        }

        /// <summary>
        /// 值下拉框变化
        /// </summary>        
        private void cmbValue_TextChanged(object sender, EventArgs e)
        {
            SetSymbolValue();
        }

        /// <summary>
        /// 限制只能输入数字和小数点
        /// </summary>
        private void cmbValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) &&
                e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
            else if (e.KeyChar == 46)                       //小数点
            {
                if (cmbValue.Text.Length <= 0 || cmbValue.SelectionStart == 0)//小数点不能在第一位
                {
                    e.Handled = true;
                }
                else
                {
                    //有且只能输入一个小数点
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(cmbValue.Text, out oldf);
                    b2 = float.TryParse(cmbValue.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
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

        /// <summary>
        /// 确定
        /// </summary>        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            #region 抓取预览区域的图片

            Graphics gp = this.CreateGraphics();
            Bitmap ibitMap = new Bitmap(300, 300, gp);
            Graphics iBitMap_gr = Graphics.FromImage(ibitMap);
            IntPtr iBitMap_hdc = iBitMap_gr.GetHdc();
            IntPtr me_hdc = gp.GetHdc();
            int x = textWidth - 148;
            BitBlt(iBitMap_hdc, x, -118, 300, 300, me_hdc, 0, 0, 13369376);
            gp.ReleaseHdc(me_hdc);
            iBitMap_gr.ReleaseHdc(iBitMap_hdc);
            pbTemp.Width = textWidth + 10;
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

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowSymbol_Leave(object sender, EventArgs e)
        {
            pnSymbol.Visible = false;
        }
        
        private void RoughnessMarkFrm_Click(object sender, EventArgs e)
        {
            pnSymbol.Visible = false;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：设置按钮图片
        /// 作    者：jason.tang
        /// 完成时间：2013-02-27
        /// </summary>
        private void SetButtonImage(object obj)
        {
            if (obj != null)
            {
                pnSymbol.Visible = false;
                Button btn = (Button)obj;
                btnShowSymbol.Image = btn.Image;
                pbPreview.Image = btn.Image;
                SetSymbolValue();
            }
        }

        /// <summary>
        /// 方法说明：设置预览
        /// 作    者：jason.tang
        /// 完成时间：2013-02-27
        /// </summary>
        private void SetSymbolValue()
        {
            string symbol = string.Empty;
            string value = string.Empty;
            if (cmbSymbolValue.SelectedItem != null)
            {
                symbol = cmbSymbolValue.SelectedItem.ToString();
            }
            if (!string.IsNullOrEmpty(cmbValue.Text))
            {
                value = cmbValue.Text;
            }
            lblSymbolValue.Text = symbol + value;            
            int textLength = lblSymbolValue.Text.Length; 
            //pbPreview.Refresh();
            if (g == null)
            {
                g = pbPreview.CreateGraphics();
            }
            float width = g.MeasureString(lblSymbolValue.Text, lblSymbolValue.Font).Width;
            textWidth = (int)Math.Round((double)width);
            int x = 148 - textWidth;
            lblSymbolValue.Location = new Point(x, 93);
            //g.DrawString(lblSymbolValue.Text, lblSymbolValue.Font, new SolidBrush(lblSymbolValue.ForeColor), x, 40);            
        }
        
        #endregion
        
    }
}
