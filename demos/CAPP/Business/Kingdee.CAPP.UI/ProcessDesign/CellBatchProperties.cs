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
    /// 类型说明：批量设置单元格属性窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-01-04
    /// </summary>
    public partial class CellBatchProperties : BaseSkinForm
    {
        #region 变量声明
        /// <summary>
        /// 字体
        /// </summary>
        private Font font;

        #endregion

        public CellBatchProperties()
        {
            InitializeComponent();

            InitControls();
        }

        #region 属性声明

        /// <summary>
        /// 单元格属性
        /// </summary>
        private Dictionary<string, object> _properties;
        public Dictionary<string, object> BatchCellProperties
        {
            get
            {
                return this._properties;
            }
            set
            {
                this._properties = value;
            }
        }

        #endregion

        #region 界面事件

        /// <summary>
        /// 确定
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _properties = new Dictionary<string, object>();
            _properties.Add("Font", font);
            if (comboType.SelectedItem != null)
            {
                try
                {
                    int cellType = 0;
                    cellType = (int)((Kingdee.CAPP.Common.ComboBoxSourceHelper.CellType)Enum.Parse(typeof(Kingdee.CAPP.Common.ComboBoxSourceHelper.CellType), comboType.SelectedItem.ToString()));
                    _properties.Add("CellType", cellType);
                }
                catch
                {
                    _properties.Add("CellType", -1);
                }
            }
            _properties.Add("Wrap", comboStyle.SelectedIndex == 1);
            //排列
            string alignment = GetAlignment();
            _properties.Add("Alignment", alignment);
            //记录边距
            List<int> listPadding = new List<int>();
            listPadding.Add(int.Parse(txtLeft.Text));
            listPadding.Add(int.Parse(txtUp.Text));
            listPadding.Add(int.Parse(txtRight.Text));
            listPadding.Add(int.Parse(txtDown.Text));
            _properties.Add("Padding", listPadding);

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

        /// <summary>
        /// 字体对话框
        /// </summary>
        private void btnShowFontDialog_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                font = fd.Font;
                txtFont.Text = fd.Font.Name;
            }
        }

        /// <summary>
        /// 边距输入校验
        /// </summary>
        /// <param name="sender">事件引起的对象</param>
        /// <param name="e"></param>
        void TextBox_Validating(object sender, CancelEventArgs e)
        {
            CommonHelper.ValidateTextBoxInput((TextBox)sender, typeof(int), "边距只能为整数，请输入整型数字");
        }

        /// <summary>
        /// 边距输入动态调整
        /// </summary>
        /// <param name="sender">事件引起的对象</param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            CommonHelper.ValidateTextBoxInput((TextBox)sender, typeof(int), string.Empty);
        }        

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：初始化控件并赋默认值
        /// 作   者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        private void InitControls()
        {
            ComboBoxSourceHelper com = new ComboBoxSourceHelper();
            com.BinderEnum<ComboBoxSourceHelper.CellType>(this.comboType, 1);
            com.BinderEnum<ComboBoxSourceHelper.CellStyle>(this.comboStyle, 1);
            comboType.Items.RemoveAt(2);
            comboType.Items.RemoveAt(2);
            comboType.Items.RemoveAt(2);
            comboType.Items.Insert(0, "不改变类型");
            comboStyle.Items.Insert(0, "不改变样式");

            rdbtnCenter.Checked = true;
            rdbtnMiddle.Checked = true;
                      
            txtUp.Text = "0";
            txtDown.Text = "0";
            txtLeft.Text = "0";
            txtRight.Text = "0";

            #region TextBox校验事件

            txtUp.TextChanged += new EventHandler(TextBox_TextChanged);
            txtDown.TextChanged += new EventHandler(TextBox_TextChanged);
            txtLeft.TextChanged += new EventHandler(TextBox_TextChanged);
            txtRight.TextChanged += new EventHandler(TextBox_TextChanged);

            txtUp.Validating += new CancelEventHandler(TextBox_Validating);
            txtDown.Validating += new CancelEventHandler(TextBox_Validating);
            txtLeft.Validating += new CancelEventHandler(TextBox_Validating);
            txtRight.Validating += new CancelEventHandler(TextBox_Validating);

            #endregion
        }

        /// <summary>
        /// 方法说明：获取水平和垂直的排列
        /// 作   者：jason.tang
        /// 完成时间：2013-01-04
        /// </summary>
        private string GetAlignment()
        {
            string alignment = string.Empty;
            foreach (RadioButton rb in groupBox3.Controls)
            {
                if (rb.Checked)
                {
                    alignment = rb.Name.ToString().Substring(5);
                }
            }

            foreach (RadioButton rb in groupBox2.Controls)
            {
                if (rb.Checked)
                {
                    alignment += rb.Name.ToString().Substring(5);
                }
            }

            return alignment;
        }

        #endregion

    }
}
