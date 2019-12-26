using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI.ProcessDesign
{
    /// <summary>
    /// 类型说明：单元格行列数目设置窗体
    /// 作   者：jason.tang
    /// 完成时间：2012-12-17
    /// </summary>
    public partial class MergeCellFrm : BaseSkinForm
    {
        public MergeCellFrm(int _width, int _height)
        {
            InitializeComponent();
            lblTotalHeight.Text = _height.ToString();
            lblTotalWidth.Text = _width.ToString();
            groupBox2.Text = string.Format(groupBox2.Text, _height.ToString(), _width.ToString());
        }

        #region 属性和变量

        /// <summary>
        /// 属性：列数
        /// </summary>
        private decimal _columns;
        public decimal Columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
            }
        }

        /// <summary>
        /// 属性：行数
        /// </summary>
        private decimal _rows;
        public decimal Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        /// <summary>
        /// 属性：列的宽度集合
        /// </summary>
        private List<int> _listWidth;
        public List<int> ListWidth
        {
            get
            {
                return _listWidth;
            }
            set
            {
                _listWidth = value;
            }
        }

        /// <summary>
        /// 属性：行的高度集合
        /// </summary>
        private List<int> _listHeight;
        public List<int> ListHeight
        {
            get
            {
                return _listHeight;
            }
            set
            {
                _listHeight = value;
            }
        }

        #endregion

        #region 窗体控件事件

        private void MergeCellFrm_Load(object sender, EventArgs e)
        {
            numRows_ValueChanged(null, null);
            numColumns_ValueChanged(null, null);
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确认操作
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _columns = numColumns.Value;
            _rows = numRows.Value;
            if (Columns < 1 || Rows < 1)
            {
                MessageBox.Show("行与列必须大于0", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SetRowsAndColumns())
                return;

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 行数变化
        /// </summary>
        private void numRows_ValueChanged(object sender, EventArgs e)
        {
            dgvRows.Rows.Clear();
            if (numRows.Value > 0)
            {
                int height = int.Parse(lblTotalHeight.Text) / int.Parse(numRows.Value.ToString());
                ChangeDataGridView(int.Parse(numRows.Value.ToString()), dgvRows);
            }
        }

        /// <summary>
        /// 列数变化
        /// </summary>
        private void numColumns_ValueChanged(object sender, EventArgs e)
        {
            dgvColumns.Rows.Clear();
            if (numColumns.Value > 0)
            {
                int width = int.Parse(lblTotalWidth.Text) / int.Parse(numColumns.Value.ToString());
                ChangeDataGridView(int.Parse(numColumns.Value.ToString()), dgvColumns);
            }
        }

        /// <summary>
        /// 行数变化
        /// </summary>
        private void numRows_KeyUp(object sender, KeyEventArgs e)
        {
            numRows.Value = numRows.Value;
        }

        /// <summary>
        /// 列数变化
        /// </summary>
        private void numColumns_KeyUp(object sender, KeyEventArgs e)
        {
            numColumns.Value = numColumns.Value;
        }

        /// <summary>
        /// 方法说明：动态改变DataGridView的行
        /// 作   者：jason.tang
        /// 完成时间：2012-12-18
        /// </summary>
        /// <param name="rows">变化的行数</param>
        /// <param name="dgv">Grid对象</param>
        private void ChangeDataGridView(int rows, DataGridView dgv)
        {
            for (int i = 0; i < rows; i++)
            {
                dgv.Rows.Add();
                dgv.Rows[i].Cells[0].Value = string.Format("第{0}行", i + 1);
                if (dgv.Name == dgvColumns.Name)
                {
                    dgv.Rows[i].Cells[0].Value = string.Format("第{0}列", i + 1);
                }                
            }
        }

        /// <summary>
        /// 行是否等高
        /// </summary>
        private void ckSameHeight_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSameHeight.Checked)
            {
                int height = int.Parse(lblTotalHeight.Text) / int.Parse(numRows.Value.ToString());
                int mod = int.Parse(lblTotalHeight.Text) % int.Parse(numRows.Value.ToString());
                int rowIndex = 0;
                foreach (DataGridViewRow row in dgvRows.Rows)
                {
                    row.Cells[1].Value = string.Format("{0}", height.ToString());
                    if (rowIndex == dgvRows.Rows.Count - 1)
                    {
                        row.Cells[1].Value = string.Format("{0}", height + mod);
                    }
                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 列是否等宽
        /// </summary>
        private void ckSameWidth_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSameWidth.Checked)
            {
                int width = int.Parse(lblTotalWidth.Text) / int.Parse(numColumns.Value.ToString());
                int mod = int.Parse(lblTotalWidth.Text) % int.Parse(numColumns.Value.ToString());
                int rowIndex = 0;
                _listWidth = new List<int>();
                foreach (DataGridViewRow row in dgvColumns.Rows)
                {
                    row.Cells[1].Value = string.Format("{0}", width.ToString());                    
                    if (rowIndex == dgvColumns.Rows.Count - 1)
                    {
                        row.Cells[1].Value = string.Format("{0}", width + mod);
                    }
                    rowIndex++;
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：行高与列宽设置
        /// 作    者：jason.tang
        /// 完成时间：2012-12-18
        /// </summary>
        private bool SetRowsAndColumns()
        {
            int countHeight = 0;    //行参数高度和
            int countWidth = 0;     //列参数宽度和

            List<int> listRowIndex = new List<int>();       //行参数中行高为空的行索引
            List<int> listColIndex = new List<int>();       //列参数中列宽为空的行索引


            _listHeight = new List<int>();
            foreach (DataGridViewRow row in dgvRows.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    _listHeight.Add(int.Parse(row.Cells[1].Value.ToString()));
                    countHeight += int.Parse(row.Cells[1].Value.ToString());
                }
                else
                {
                    _listHeight.Add(0);
                    listRowIndex.Add(_listHeight.Count -1);
                }
            }

            _listWidth = new List<int>();
            foreach (DataGridViewRow row in dgvColumns.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    _listWidth.Add(int.Parse(row.Cells[1].Value.ToString()));
                    countWidth += int.Parse(row.Cells[1].Value.ToString());
                }
                else
                {
                    _listWidth.Add(0);
                    listColIndex.Add(_listWidth.Count - 1);
                }
            }

            int remainHeight = int.Parse(lblTotalHeight.Text) - countHeight;  //总高度与不为空的行高之差
            int remainWidth = int.Parse(lblTotalWidth.Text) - countWidth;     //总宽度与不为空的列宽之差

            if (remainHeight >= 0)
            {
                int height = remainHeight / listRowIndex.Count;
                int mod = remainHeight % listRowIndex.Count;
                foreach (int index in listRowIndex)
                {
                    _listHeight[index] = height;
                    if (listRowIndex.IndexOf(index) == listRowIndex.Count - 1)
                    {
                        _listHeight[index] = height + mod;
                    }
                }
            }
            else
            {
                MessageBox.Show("行高设置大于允许的总高度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (remainWidth >= 0)
            {
                int width = remainWidth / listColIndex.Count;
                int mod = remainWidth % listColIndex.Count;
                foreach (int index in listColIndex)
                {
                    _listWidth[index] = width;
                    if (listColIndex.IndexOf(index) == listColIndex.Count - 1)
                    {
                        _listWidth[index] = width + mod;
                    }
                }
            }
            else
            {
                MessageBox.Show("列宽设置大于允许的总宽度", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        #endregion
        
    }
}
