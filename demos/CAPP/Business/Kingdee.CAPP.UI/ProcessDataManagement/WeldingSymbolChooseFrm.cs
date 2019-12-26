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
    /// 类型说明：焊接符号选择界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-07
    /// </summary>
    public partial class WeldingSymbolChooseFrm : Form
    {
        #region 变量和属性声明

        /// <summary>
        /// 焊接符号
        /// </summary>
        private Image weldingimage;
        public Image WeldingImage
        {
            get
            {
                return weldingimage;
            }
        }

        #endregion

        public WeldingSymbolChooseFrm()
        {
            InitializeComponent();
        }

        private void WeldingSymbolChooseFrm_Load(object sender, EventArgs e)
        {
            this.Width = 92;
            this.Height = 520;
        }

        private void WeldingSymbolChooseFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void SymbolButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            weldingimage = button.Image;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_hover;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_d;
        }
    }
}
