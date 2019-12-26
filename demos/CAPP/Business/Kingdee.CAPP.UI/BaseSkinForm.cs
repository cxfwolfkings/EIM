using System;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI
{
    public partial class BaseSkinForm : Form
    {
        public BaseSkinForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 隐藏最小化按钮
        /// </summary>
        protected virtual bool MinimunSize
        {
            set
            {
                btnMinimunSize.Visible = value;
            }
        }

        /// <summary>
        /// 隐藏最大化按钮
        /// </summary>
        protected virtual bool MaximumSize
        {
            set
            {
                btnMaximumSize.Visible = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimunSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMinimunSize_MouseHover(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.min_hover;
        }

        private void btnMinimunSize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.minimum_d;
        }

        private void btnMaximumSize_MouseHover(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_hover;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_hover;
            }
        }

        private void btnMaximumSize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_hover;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_hover;
            }
        }

        private void btnMaximumSize_MouseLeave(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.max_d;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                btnMaximumSize.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.restore_d;
            }
        }
        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_hover;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = Kingdee.CAPP.UI.Properties.Resources.close_d;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        private void BaseSkinForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = this.Text;
            if (btnMaximumSize.Visible)
            {
                btnMaximumSize.Left = btnClose.Left - btnMaximumSize.Width;
                btnMinimunSize.Left = btnMaximumSize.Left - btnMinimunSize.Width;
            }
            else
            {
                btnMinimunSize.Left = btnClose.Left - btnMinimunSize.Width;
            }
        }

    }
}
