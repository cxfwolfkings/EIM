using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    /// <summary>
    /// 类型说明：单元格样式属性设置
    /// 作者：jason.tang
    /// 完成时间：2013-01-17
    /// </summary>
    public partial class DataGridViewCellStyleFrm : Form
    {
        #region 属性声明

        private List<DataGridViewCustomerCellStyle> _customerCellStyle;
        public List<DataGridViewCustomerCellStyle> CustomerCellStyle
        {
            get
            {
                return _customerCellStyle;
            }
            set
            {
                _customerCellStyle = value;
            }
        }

        #endregion

        #region 窗体控件事件

        public DataGridViewCellStyleFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            _customerCellStyle = new List<DataGridViewCustomerCellStyle>();
            _customerCellStyle.Add((DataGridViewCustomerCellStyle)pgrdCellStyle.SelectedObject);
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pgrdCellStyle_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

        }

        private void DataGridViewCellStyleFrm_Load(object sender, EventArgs e)
        {            
            if (_customerCellStyle == null || _customerCellStyle.Count == 0)
            {
                pgrdCellStyle.SelectedObject = new DataGridViewCustomerCellStyle();
            }
            else if (_customerCellStyle.Count > 0)
            {
                pgrdCellStyle.SelectedObject = _customerCellStyle[0];
            }

            Object obj = pgrdCellStyle.SelectedObject;
            //固定单元格时显示内容选项
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor pd in props)
            {
                if (pd.Name == "CardName")
                {
                    SetPropertyVisibility(obj, pd.Name, false);
                }
            }
            pgrdCellStyle.SelectedObject = obj;
        }

        /// <summary>
        /// 方法说明：动态设置指定属性是否可见
        /// 作者：jason.tang
        /// 完成时间：2013-03-07
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="visible">是否可见</param>
        private void SetPropertyVisibility(object obj, string propertyName, bool visible)
        {
            Type type = typeof(BrowsableAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("browsable", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], visible);
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
            btnMinimunSize.BackgroundImage = ResourceNotice.min_hover;
        }

        private void btnMinimunSize_MouseLeave(object sender, EventArgs e)
        {
            btnMinimunSize.BackgroundImage = ResourceNotice.minimum_d;
        }

        private void btnClose_MouseHover(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = ResourceNotice.close_hover;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackgroundImage = ResourceNotice.close_d;
        }

        private void Form_MouseDown(object sender, MouseEventArgs e)
        {
            //调用移动无窗体控件函数
            Kingdee.CAPP.Common.CommonHelper.MoveNoneBorderForm(this);
        }

        #endregion
    }
}
