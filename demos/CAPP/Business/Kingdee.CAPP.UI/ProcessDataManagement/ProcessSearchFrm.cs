using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：工艺文件查找界面
    /// 作    者：jason.tang
    /// 完成时间：2013-03-19
    /// </summary>
    public partial class ProcessSearchFrm : BaseSkinForm
    {
        #region 窗体控件事件

        public ProcessSearchFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                DataTable dt = new DataTable();
                DataTable data = SqlServerControllerBLL.SearchProcessCardByContent(txtProduct.Text, comboCardModule.SelectedValue.ToString());
                if (data != null && data.Rows.Count > 0)
                {
                    dt = data;
                }
                dgvProcessCard.DataSource = dt;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProcessSearchFrm_Load(object sender, EventArgs e)
        {
            DataTable dt = ProcessCardModuleBLL.GetProcessCardData();
            comboCardModule.DisplayMember = "Name";
            comboCardModule.ValueMember = "Id";
            comboCardModule.DataSource = dt;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：检查是否所有条件都未输入
        /// 作    者：jason.tang
        /// 完成时间：2013-03-27
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool result = true;

            int count = 0;
            int countNull = 0;
            foreach (Control ctr in pnSearchConditions.Controls)
            {
                if (ctr is TextBox)
                {
                    count++;
                    if (string.IsNullOrEmpty(ctr.Text))
                    {
                        countNull++;
                    }
                }
            }

            if (countNull == count)
            {
                MessageBox.Show("查询条件(产品、零部件、编制人员)必输其一", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                result = false;
                txtProduct.Focus();
            }

            return result;
        }

        #endregion

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
        
    }
}
