using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Common;

namespace Kingdee.CAPP.UI.ProcessDesign
{
    /// <summary>
    /// 类型说明：卡片幅面设置窗体
    /// 作   者：jason.tang
    /// 完成时间：2012-12-18
    /// </summary>
    public partial class CardPageSettingFrm : BaseSkinForm
    {
        #region 变量声明

        private Dictionary<string, Point> dicBreadth;

        #endregion

        #region 构造函数

        public CardPageSettingFrm()
        {
            InitializeComponent();

            InitControls();
        }

        #endregion

        #region 属性

        /// <summary>
        /// 卡片名称
        /// </summary>
        private string _cardModuleName;
        public string CardModuleName
        {
            get
            {
                return _cardModuleName;
            }
            set
            {
                _cardModuleName = value;
            }
        }

        /// <summary>
        /// 卡片模板标识
        /// </summary>
        private string _cardModuleTag;
        public string CardModuleTag
        {
            get
            {
                return _cardModuleTag;
            }
            set
            {
                _cardModuleTag = value;
            }
        }

        /// <summary>
        /// 宽度
        /// </summary>
        private int _width;
        public int PageWidth
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        private int _height;
        public int PageHeight
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        /// <summary>
        /// 左边距
        /// </summary>
        private int _padleft;
        public int PaddingLeft
        {
            get
            {
                return _padleft;
            }
            set
            {
                _padleft = value;
            }
        }

        /// <summary>
        /// 上边距
        /// </summary>
        private int _padtop;
        public int PaddingTop
        {
            get
            {
                return _padtop;
            }
            set
            {
                _padtop = value;
            }
        }

        /// <summary>
        /// 右边距
        /// </summary>
        private int _padright;
        public int PaddingRight
        {
            get
            {
                return _padright;
            }
            set
            {
                _padright = value;
            }
        }

        /// <summary>
        /// 下边距
        /// </summary>
        private int _padbottom;
        public int PaddingBottom
        {
            get
            {
                return _padbottom;
            }
            set
            {
                _padbottom = value;
            }
        }

        /// <summary>
        /// 左偏移
        /// </summary>
        private int _offsetleft;
        public int OffsetLeft
        {
            get
            {
                return _offsetleft;
            }
            set
            {
                _offsetleft = value;
            }
        }

        /// <summary>
        /// 上偏移
        /// </summary>
        private int _offsettop;
        public int OffsetTop
        {
            get
            {
                return _offsettop;
            }
            set
            {
                _offsettop = value;
            }
        }

        /// <summary>
        /// 卡片幅面
        /// </summary>
        private int _breadth;
        public int CardBreadth
        {
            get
            {
                return _breadth;
            }
            set
            {
                _breadth = value;
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        private int _cardtype;
        public int CardType
        {
            get
            {
                return _cardtype;
            }
            set
            {
                _cardtype = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：初始化控件并赋默认值
        /// 作   者：jason.tang
        /// 完成时间：2012-12-18
        /// </summary>
        private void InitControls()
        {
            ComboBoxSourceHelper com = new ComboBoxSourceHelper();
            com.BinderEnum<ComboBoxSourceHelper.PageBreadth>(comboBreadth, 4);
            com.BinderEnum<ComboBoxSourceHelper.CardType>(comboCardType, 0);


            dicBreadth = new Dictionary<string, Point>();
            List<Point> listSize = new List<Point>();
            listSize.Add(new Point(1189, 841));
            listSize.Add(new Point(841, 594));
            listSize.Add(new Point(594, 420));
            listSize.Add(new Point(420, 297));
            listSize.Add(new Point(297, 210));
            listSize.Add(new Point(210, 148));

            for (int i = 0; i < 6; i++)
            {          
                dicBreadth.Add(comboBreadth.Items[i].ToString(), listSize[i]);
            }                       

            txtWidth.Text = dicBreadth["A4"].X.ToString();
            txtHeight.Text = dicBreadth["A4"].Y.ToString();            

            ckPrintAdjust.Checked = true;
            txtPrintScale.Text = "100";
            txtOffsetLeft.Text = "0";
            txtOffsetTop.Text = "0";

            rdbtnHorizontal.Select();

            txtPaddingLeft.Text = "5";
            txtPaddingRight.Text = "5";
            txtPaddingTop.Text = "5";
            txtPaddingBottom.Text = "5";

            txtCardName.Height = 23;
        }

        #endregion

        #region 窗体控件事件

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<Control> listControls = new List<Control>();
            listControls.Add(txtCardName);
            listControls.Add(txtCardTag);
            bool valid = CommonHelper.CheckNullInput(listControls);
            if (!valid)
            {
                return;
            }

            bool exist = Kingdee.CAPP.BLL.ProcessCardModuleBLL.CheckModuleNameExist(txtCardName.Text);
            if (exist)
            {
                MessageBox.Show("模版名称已经存在，请使用另一个名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCardName.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            _cardModuleName = txtCardName.Text;
            _cardModuleTag = txtCardTag.Text;
            _width = int.Parse(txtWidth.Text);
            _height = int.Parse(txtHeight.Text);
            _padleft = int.Parse(txtPaddingLeft.Text);
            _padtop = int.Parse(txtPaddingTop.Text);
            _padright = int.Parse(txtPaddingRight.Text);
            _padbottom = int.Parse(txtPaddingBottom.Text);
            _offsetleft = int.Parse(txtOffsetLeft.Text);
            _offsettop = int.Parse(txtOffsetTop.Text);
            _breadth = comboBreadth.SelectedIndex;
            _cardtype = comboCardType.SelectedIndex;

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 是否可用使用打印调整项
        /// </summary>
        private void ckPrintAdjust_CheckedChanged(object sender, EventArgs e)
        {
            txtPrintScale.Enabled = ckPrintAdjust.Checked;
            txtOffsetTop.Enabled = ckPrintAdjust.Checked;
            txtOffsetLeft.Enabled = ckPrintAdjust.Checked;
        }

        /// <summary>
        /// 幅面改变尺寸变动
        /// </summary>
        private void comboBreadth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBreadth.SelectedItem == null)
            {
                return;
            }

            if (comboBreadth.SelectedIndex == 6)
            {
                txtWidth.Enabled = true;
                txtHeight.Enabled = true;
                return;
            }

            txtWidth.Enabled = false;
            txtHeight.Enabled = false;
            string selectValue = comboBreadth.SelectedItem.ToString();
            
            //横向
            if (rdbtnHorizontal.Checked)
            {
                txtWidth.Text = dicBreadth[selectValue].X.ToString();
                txtHeight.Text = dicBreadth[selectValue].Y.ToString();
            }//纵向
            else if (rdbtnVertical.Checked)
            {
                txtWidth.Text = dicBreadth[selectValue].Y.ToString();
                txtHeight.Text = dicBreadth[selectValue].X.ToString();
            }
        }

        /// <summary>
        /// 纵向
        /// </summary>
        private void rdbtnVertical_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnVertical.Checked)
            {
                int width = int.Parse(txtWidth.Text);
                int height = int.Parse(txtHeight.Text);

                if (width > height)
                {
                    txtWidth.Text = height.ToString();
                    txtHeight.Text = width.ToString();
                }
                picOrientation.Image = Properties.Resources.ver;
                rdbtnHorizontal.Checked = false;
            }
        }

        /// <summary>
        /// 横向
        /// </summary>
        private void rdbtnHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnHorizontal.Checked)
            {
                int width = int.Parse(txtWidth.Text);
                int height = int.Parse(txtHeight.Text);

                if (width < height)
                {
                    txtWidth.Text = height.ToString();
                    txtHeight.Text = width.ToString();
                }
                
                picOrientation.Image = Properties.Resources.hor;
                rdbtnVertical.Checked = false;
            }
        }

        /// <summary>
        /// 事件说明：文本框为了设置高度，设置多行，并限制不能回车
        /// </summary>
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        /// <summary>
        /// 限制文本框输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && 
                e.KeyChar != 13 && e.KeyChar != 45 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            //输入为负号时，只能输入一次且只能输入一次
            if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
            if (e.KeyChar == 46 && ((TextBox)sender).Text.IndexOf(".") >= 0) e.Handled = true;
        }

        #endregion

    }
}
