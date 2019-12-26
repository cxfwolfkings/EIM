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
    /// 类型说明：卡片导出为图片文件界面
    /// 作    者：jason.tang
    /// 完成时间：2013-02-19
    /// </summary>
    public partial class DataGridViewToBmpFrm : BaseSkinForm
    {
        #region 属性声明

        /// <summary>
        /// 文件路径
        /// </summary>
        private string _path;
        public string FilePath
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        /// <summary>
        /// 图像类型
        /// </summary>
        private string _type;
        public string ImageType
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        /// <summary>
        /// 文件名
        /// </summary>
        private string _name;
        public string FileName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// 页面范围
        /// </summary>
        private string _range;
        public string PageRange
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public string TotalPage
        {
            get;
            set;
        }

        #endregion

        #region 窗体控件事件

        public DataGridViewToBmpFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        private void DataGridViewToBmpFrm_Load(object sender, EventArgs e)
        {
            Kingdee.CAPP.Common.ComboBoxSourceHelper com = new Kingdee.CAPP.Common.ComboBoxSourceHelper();             
            com.BinderEnum<Kingdee.CAPP.Common.ComboBoxSourceHelper.ImageType>(this.cmbImageType, 0);
            groupBox1.Text = string.Format(groupBox1.Text, TotalPage);
        }

        /// <summary>
        /// 浏览文件路径
        /// </summary>
        private void btnFilePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePath.Text = dialog.SelectedPath;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
            {
                return;
            }
            _path = txtFilePath.Text + "\\";
            _name = txtFileName.Text;
            if (rbtnPageRange.Checked)
            {
                string text = string.Empty;
                try
                {
                    text = PageRangeAnaly();
                }
                catch
                {
                    return;
                }
                if (string.IsNullOrEmpty(text))
                {
                    return;
                }
                _range = text;
            }
            else
            {
                _range = rbtnAllPage.Checked ? "All" : "Current";
            }
            _type = cmbImageType.SelectedItem.ToString();
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
        /// 页面范围选中事件
        /// </summary>
        private void rbtnPageRange_CheckedChanged(object sender, EventArgs e)
        {
            txtPageRange.Enabled = rbtnPageRange.Checked;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：校验界面输入框
        /// 作    者：jason.tang
        /// 完成时间：2013-02-19
        /// </summary>
        /// <returns>True/False</returns>
        private bool CheckInput()
        {
            if (txtPageRange.Enabled)
            {
                if (string.IsNullOrEmpty(txtPageRange.Text))
                {
                    MessageBox.Show(string.Format("{0}不能为空", txtPageRange.Tag), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPageRange.Focus();
                    return false;
                }
                else
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[1-9,\-]*$");
                    if (!regex.IsMatch(txtPageRange.Text))
                    {
                        MessageBox.Show("页面范围格式错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPageRange.Focus();
                        return false;
                    }
                }


            }
            foreach (Control control in groupBox2.Controls)
            {
                if (control is TextBox && 
                    string.IsNullOrEmpty(((TextBox)control).Text))
                {
                    MessageBox.Show(string.Format("{0}不能为空", control.Tag), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    control.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 方法说明：解析页码范围输入文本
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        /// <returns>解析后的文本</returns>
        private string PageRangeAnaly()
        {
            string[] text = txtPageRange.Text.Split(new char[] { ',' });
            int totalPage = int.Parse(TotalPage);
            List<string> listRanges = new List<string>();
            foreach (string str in text)
            {
                if (str.Contains("-"))
                {
                    int start = int.Parse(str.Substring(0, str.IndexOf("-")));
                    int end = int.Parse(str.Substring(str.IndexOf("-") + 1));

                    if (start > end)
                    {
                        MessageBox.Show(string.Format("区间页面范围({0})中开始页面必须小于结束页面", str), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPageRange.Focus();
                        return null;
                    }
                    if (start > totalPage)
                    {
                        MessageBox.Show(string.Format("页码({0})超出总页数", start), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPageRange.Focus();
                        return null;
                    }
                    if (end > totalPage)
                    {
                        MessageBox.Show(string.Format("页码({0})超出总页数", end), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPageRange.Focus();
                        return null;
                    }

                    for (int i = start; i <= end; i++)
                    {
                        if (!listRanges.Contains(i.ToString()))
                        {
                            listRanges.Add(i.ToString());
                        }
                    }
                }
                else
                {
                    if (int.Parse(str) > totalPage)
                    {
                        MessageBox.Show(string.Format("页码({0})超出总页数", str), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPageRange.Focus();
                        return null;
                    }
                    if (!listRanges.Contains(str))
                    {
                        listRanges.Add(str);
                    }
                }
                
            }

            return string.Join(",", listRanges.ToArray());
        }

        #endregion
    }
}
