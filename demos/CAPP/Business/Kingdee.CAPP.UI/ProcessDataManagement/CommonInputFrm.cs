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
    /// 类型说明：公用输入界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-13
    /// </summary>
    public partial class CommonInputFrm : BaseSkinForm
    {
        #region 变量和属性声明

        public object CommonObject
        {
            get;
            set;
        }

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string FormTitle
        {
            get;
            set;
        }

        #endregion

        #region 窗体控件事件

        public CommonInputFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CommonObject = pgCommonProperty.SelectedObject;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Load事件
        /// </summary>
        private void CommonInputFrm_Load(object sender, EventArgs e)
        {
            pgCommonProperty.SelectedObject = CommonObject;
            this.Text = FormTitle;
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

    }
}
