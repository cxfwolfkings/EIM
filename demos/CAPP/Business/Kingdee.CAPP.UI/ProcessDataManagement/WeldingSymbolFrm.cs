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
    /// 类型说明：焊接符号界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-07
    /// </summary>
    public partial class WeldingSymbolFrm : BaseSkinForm
    {
        #region 变量和属性声明

        /// <summary>
        /// 形位公差标图片
        /// </summary>
        private Image image;
        public Image WeldingSymbolImage
        {
            get
            {
                return image;
            }
        }

        #endregion

        #region 窗体控件事件

        public WeldingSymbolFrm()
        {
            InitializeComponent();
        }

        private void pbWeldingSymbol_Click(object sender, EventArgs e)
        {
            WeldingSymbolChooseFrm form = new WeldingSymbolChooseFrm();
            form.StartPosition = FormStartPosition.Manual;
            Point pt = MousePosition;//获取鼠标的屏幕坐标
            
            form.Left = pt.X;
            form.Top = pt.Y;
            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pbWeldingSymbol.Image = form.WeldingImage;
                picTemp.Image = pbWeldingSymbol.Image;
            }
        }
        
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Bitmap ibitMap = new Bitmap(picTemp.Image);
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
                picTemp.Refresh();
                picTemp.Image = ibitMap;
            }

            Bitmap bmp = new Bitmap(picTemp.Width, picTemp.Height);
            picTemp.DrawToBitmap(bmp, new Rectangle(0, 0, picTemp.Width, picTemp.Height));
            image = bmp;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WeldingSymbolFrm_Load(object sender, EventArgs e)
        {
            picTemp.Image = pbWeldingSymbol.Image;
        }

        #endregion

    }
}
