using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Common;
using System.Reflection;

namespace Kingdee.CAPP.Common.DataGridViewHelp
{
    /// <summary>
    /// 类型说明：明细单元格列属性设置
    /// 作者：jason.tang
    /// 完成时间：2013-01-09
    /// </summary>
    public partial class CardDetailPropertyFrm : Form
    {
        #region 变量声明

        /// <summary>
        /// 明细列集合
        /// </summary>
        private Dictionary<string, DetailGridViewTextBoxColumn> dicColumns;

        #endregion

        #region 属性声明

        private List<DetailGridViewTextBoxColumn> _dicDetailColumns;
        public List<DetailGridViewTextBoxColumn> PropertyDetailColumns
        {
            get
            {
                return _dicDetailColumns;
            }
            set
            {
                _dicDetailColumns = value;
            }
        }

        /// <summary>
        /// 当前明细单元格的宽度
        /// </summary>
        private int _totalWidth;
        public int TotalWidth
        {
            get{
                return _totalWidth;
            }
            set
            {
                _totalWidth = value;
            }
        }

        #endregion

        public CardDetailPropertyFrm()
        {
            InitializeComponent();
        }

        #region 窗体控件事件

        /// <summary>
        /// 上移
        /// </summary>
        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbColumns.Text))
            {
                MoveItems(lbColumns.Text, true);
            }
        }

        /// <summary>
        /// 下移
        /// </summary>
        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lbColumns.Text))
            {
                MoveItems(lbColumns.Text, false);
            }
        }

        /// <summary>
        /// 增加列
        /// </summary>
        private void btnAddColumn_Click(object sender, EventArgs e)
        {
            AddColumn();
        }

        /// <summary>
        /// 删除列
        /// </summary>
        private void btnDeleteColumn_Click(object sender, EventArgs e)
        {
            if (lbColumns.SelectedValue != null)
            {
                //得到当前选中项的索引
                int currIndex = lbColumns.SelectedIndex;
                try
                {
                    DetailGridViewTextBoxColumn col = (DetailGridViewTextBoxColumn)lbColumns.SelectedValue;
                    dicColumns.Remove(col.ColumnName);
                    
                    if (dicColumns.Count == 0)
                    {
                        lbColumns.DataSource = null;
                        InitVariable(true);
                    }
                    else
                    {
                        lbColumns.DataSource = new BindingSource(dicColumns, null);
                    }
                    //删除当前项之后，默认选择前一项
                    lbColumns.SelectedIndex = currIndex - 1;

                    //当选择删除的项为第一项，但其下面还有其他项时，默认选择删除后的第一项
                    if (currIndex == 0 && lbColumns.Items.Count > 0)
                    {
                        lbColumns.SelectedIndex = 0;
                    }
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            if (dicColumns.Count == 0)
            {
                _dicDetailColumns = null;
            }
            else
            {
                _dicDetailColumns = new List<DetailGridViewTextBoxColumn>();
            }

            DetailGridViewTextBoxColumn col = new DetailGridViewTextBoxColumn();
            int range = 1;
            //当前要改变宽度的Key
            string currKey = string.Empty;
            //已经存在的不为5的宽度
            int greaterWidth = 0;
            //为5的宽度个数
            List<string> lstFiveWidth = new List<string>();
            foreach (string key in dicColumns.Keys)
            {
                if (range == dicColumns.Keys.Count)
                {
                    currKey = key;
                }
                range++;

                if (dicColumns[key].Width > 5)
                {
                    greaterWidth += dicColumns[key].Width;
                }
                else
                {
                    lstFiveWidth.Add(key);
                }
            }

            if (lstFiveWidth.Count > 0)
            {
                int avgWidht = (_totalWidth - greaterWidth) / lstFiveWidth.Count;
                int remainder = (_totalWidth - greaterWidth) % lstFiveWidth.Count;

                range = 0;
                foreach (string str in lstFiveWidth)
                {
                    if (range == lstFiveWidth.Count - 1)
                    {
                        dicColumns[str].Width = avgWidht + remainder;
                    }
                    dicColumns[str].Width = avgWidht;
                    range++;
                }
            }

            int allColWidth = GetColumnWidthCount();
            int diff = _totalWidth - allColWidth;
            int avg = 0;
            int rem = 0;
            if (diff > 0 && dicColumns.Count > 0)
            {
                avg = diff / dicColumns.Count;
                rem = diff % dicColumns.Count;
            }
            
            int rang = 0;
            foreach (string key in dicColumns.Keys)
            {
                col = new DetailGridViewTextBoxColumn();
                col = dicColumns[key];
                //col.Name = dicColumns[key].Name;
                col.SerialNumber = dicColumns[key].SerialNumber;
                col.Rows = dicColumns[key].Rows;
                col.Visible = dicColumns[key].Visible;
                col.ColumnName = dicColumns[key].ColumnName;
                col.HeaderText = dicColumns[key].HeaderText;
                if(rang == dicColumns.Count - 1)
                    col.Width = dicColumns[key].Width + avg + rem;
                else
                    col.Width = dicColumns[key].Width + avg;               

                _dicDetailColumns.Add(col);
                rang++;
            }

            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dicColumns == null)
            {
                this.Close();
                return;
            }

            int width = 0;
            int range = 1;
            string lastKey = string.Empty;
            //取消时，如果列宽度变化了，则最后一列补齐
            foreach (string key in dicColumns.Keys)
            {
                width += dicColumns[key].Width;
                if (range == dicColumns.Keys.Count)
                {
                    lastKey = key;
                }
                range++;
            }
            if (_totalWidth > width && dicColumns.ContainsKey(lastKey))
            {
                dicColumns[lastKey].Width += _totalWidth - width;
            }

            this.Close();
        }

        /// <summary>
        /// 列表选择值变化
        /// </summary>
        private void lbColumns_SelectedValueChanged(object sender, EventArgs e)
        {
            pgrdColumns.SelectedObject = ((ListBox)sender).SelectedValue;
            btnMoveUp.Enabled = lbColumns.SelectedIndex != 0;
            btnMoveDown.Enabled = lbColumns.SelectedIndex != lbColumns.Items.Count - 1;

            if (pgrdColumns.SelectedObject == null)
                return;

            Object obj = ((ListBox)sender).SelectedValue;
            DetailGridViewTextBoxColumn column = (DetailGridViewTextBoxColumn)obj;
            bool readOnly = column.AdvanceProperty != (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1");
            //固定单元格时显示内容选项
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            foreach (PropertyDescriptor pd in props)
            {
                if (pd.Name == "SerialStep")
                {
                    SetPropertyReadOnly(obj, pd.Name, readOnly);
                }
            }
            pgrdColumns.SelectedObject = obj;
        }

        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardDetailPropertyFrm_Load(object sender, EventArgs e)
        {
            InitVariable(false);
            if (dicColumns != null)
            {
                lbColumns.DataSource = new BindingSource(dicColumns, null);
            }
        }

        private void pgrdColumns_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            object obj = pgrdColumns.SelectedObject;
            DetailGridViewTextBoxColumn column = (DetailGridViewTextBoxColumn)obj;
            int rows = 0;
            int spaceRows = 0;
            if (!string.IsNullOrEmpty(column.Rows))
            {
                rows = int.Parse(column.Rows);
                if (rows <= 0)
                {
                    rows = 1;
                }
            }
            //if (!string.IsNullOrEmpty(column.SpaceRows))
            //{
            //    spaceRows = int.Parse(column.SpaceRows);
            //    if (spaceRows <= 0)
            //    {
            //        spaceRows = 0;
            //    }
            //}
            if (dicColumns == null)
            {
                return;
            }

            Dictionary<string, DetailGridViewTextBoxColumn> dicTemp = new Dictionary<string, DetailGridViewTextBoxColumn>();
                       

            int width = 0;
            foreach (string key in dicColumns.Keys)
            {
                dicColumns[key].Rows = rows.ToString();
                //dicColumns[key].SpaceRows = spaceRows.ToString();
                width += dicColumns[key].Width;

                if (e.OldValue != null && e.OldValue.ToString() == key)
                {
                    dicTemp.Add(dicColumns[key].ColumnName, dicColumns[key]);
                }
                else
                {
                    dicTemp.Add(key, dicColumns[key]);
                }
            }

            if (e.ChangedItem.Label == "名称")
            {
                dicColumns = dicTemp;        
                lbColumns.DataSource = new BindingSource(dicColumns, null);
            }

            if (width > _totalWidth)
            {
                int oldValue = 0;
                bool isDigit = int.TryParse(e.OldValue.ToString(), out oldValue);
                if (isDigit)
                {
                    column.Width = oldValue;
                }
            }

            if (column != null)
            {
                bool readOnly = column.AdvanceProperty != (ComboBoxSourceHelper.AdvanceProperty)Enum.Parse(typeof(ComboBoxSourceHelper.AdvanceProperty), "1");
                //固定单元格时显示内容选项
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
                foreach (PropertyDescriptor pd in props)
                {
                    if (pd.Name == "SerialStep")
                    {
                        SetPropertyReadOnly(obj, pd.Name, readOnly);
                    }
                }
                pgrdColumns.SelectedObject = obj;
            }
        }

            /// <summary>
        /// 方法说明：动态设置指定属性是否只读
        /// 作者：jason.tang
        /// 完成时间：2013-01-10
        /// </summary>
        /// <param name="obj">当前对象</param>
        /// <param name="propertyName">属性名</param>
        /// <param name="readOnly">是否只读</param>
        private void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        {
            Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
            AttributeCollection attrs = props[propertyName].Attributes;
            FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fld.SetValue(attrs[type], readOnly);          
        }

        /// <summary>
        /// 高级属性
        /// </summary>
        private void comboAdvanceProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboAdvanceProperty.SelectedIndex == 1)
            //{
            //    lblStep.Visible = true;
            //    txtStep.Visible = true;
            //    txtStep.Text = "10";
            //}
            //else
            //{
            //    lblStep.Visible = false;
            //    txtStep.Visible = false;                
            //}

            object obj = pgrdColumns.SelectedObject;
            if (obj != null)
            {
                ((DetailGridViewTextBoxColumn)obj).Visible = comboAdvanceProperty.SelectedIndex != 3;
            }
        }
        
        /// <summary>
        /// 步长输入框校验
        /// </summary>
        //private void txtStep_Validating(object sender, CancelEventArgs e)
        //{
        //    object obj = pgrdColumns.SelectedObject;
            
        //    int serialNumber = 0;
        //    int step = 0;

        //    if (obj != null)
        //    {
        //        DetailGridViewTextBoxColumn col = (DetailGridViewTextBoxColumn)obj;
        //        serialNumber = col.SerialNumber;
        //    }

        //    bool isDigit = int.TryParse(txtStep.Text, out step);
        //    if (isDigit)
        //    {
        //        if (step < 1)
        //        {
        //            MessageBox.Show("请输入大于0的整数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            txtStep.Focus();
        //            return;
        //        }
        //        if (obj != null)
        //        {
        //            DetailGridViewTextBoxColumn col = (DetailGridViewTextBoxColumn)obj;
        //            col.SerialNumber = step * serialNumber;
        //            pgrdColumns.SelectedObject = col;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("请输入大于0的整数", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        txtStep.Focus();
        //    }
        //}

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

        #region 方法

        /// <summary>
        /// 方法说明：初始化变量
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        private void InitVariable(bool isDelete)
        {
            if (_dicDetailColumns != null && !isDelete)
            {
                dicColumns = new Dictionary<string, DetailGridViewTextBoxColumn>();
                foreach (DetailGridViewTextBoxColumn col in _dicDetailColumns)
                {
                    dicColumns.Add(col.ColumnName, col);
                }
            }
            lbColumns.DisplayMember = "Key";
            lbColumns.ValueMember = "Value";

            ComboBoxSourceHelper com = new ComboBoxSourceHelper();
            com.BinderEnum<ComboBoxSourceHelper.AdvanceProperty>(this.comboAdvanceProperty, 0);
        }

        /// <summary>
        /// 方法说明：上移或下移项
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        /// <param name="key">当前选中项</param>
        private void MoveItems(string key, bool upOrDown)
        {
            List<DetailGridViewTextBoxColumn> listColumns = new List<DetailGridViewTextBoxColumn>();
            int index = 0;
            int currIndex = 0;

            int currSerial = 0;  //当前选中项的序号
            int moveToSerial = 0; //移动到的序号

            foreach (string k in dicColumns.Keys)
            {
                //找到当前选择项的索引
                if (k == key)
                {
                    currIndex = index;
                }
                listColumns.Add(dicColumns[k]);
                index++;
            }

            DetailGridViewTextBoxColumn currColumn;   //当前列
            DetailGridViewTextBoxColumn moveToColumn; //要移至的列 
            
            //交换移动列的位置
            if (upOrDown)
            {
                currColumn = listColumns[currIndex];
                moveToColumn = listColumns[currIndex - 1];
                currSerial = currColumn.SerialNumber;
                moveToSerial = moveToColumn.SerialNumber;

                currColumn.SerialNumber = moveToSerial;
                moveToColumn.SerialNumber = currSerial;

                listColumns[currIndex] = moveToColumn;
                listColumns[currIndex - 1] = currColumn;
            }
            else
            {
                currColumn = listColumns[currIndex];
                moveToColumn = listColumns[currIndex + 1];

                currSerial = currColumn.SerialNumber;
                moveToSerial = moveToColumn.SerialNumber;

                currColumn.SerialNumber = moveToSerial;
                moveToColumn.SerialNumber = currSerial;

                listColumns[currIndex] = moveToColumn;
                listColumns[currIndex + 1] = currColumn;
            }

            //移除集合，重绑定
            dicColumns.Clear();
            foreach (DetailGridViewTextBoxColumn col in listColumns)
            {
                dicColumns.Add(col.ColumnName, col);
            }

            lbColumns.DataSource = new BindingSource(dicColumns, null);
            lbColumns.SelectedIndex = upOrDown ? currIndex - 1 : currIndex + 1;
      
        }

        /// <summary>
        /// 方法说明：增加列
        /// 作    者：jason.tang
        /// 完成时间：2013-01-09
        /// </summary>
        private void AddColumn()
        {
            DetailGridViewTextBoxColumn col = new DetailGridViewTextBoxColumn();
            if (dicColumns == null)
                dicColumns = new Dictionary<string, DetailGridViewTextBoxColumn>();
            int totalWidth = GetColumnWidthCount();
            //当剩余的列宽不够时，返回
            if (_totalWidth - totalWidth <= 0)
            {
                DialogResult result = MessageBox.Show("列宽已经达到明细框的最大宽度了，是否需要重新分配？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(result == System.Windows.Forms.DialogResult.No)
                    return;

                pgrdColumns.SelectedObject = null;
                ResizeColumnWidth();
            }

            int number = lbColumns.Items.Count + 1;
            string key = string.Format("Column{0}", number);
            int colIndex = number;
            while(dicColumns.ContainsKey(key))
            {
                colIndex++;
                key = string.Format("Column{0}", colIndex);
            }
            col.ColumnName = string.Format("Column{0}", colIndex);
            col.SerialNumber = number;

            col.Rows = "1";
            //col.SpaceRows = "0";
            if (number > 1)
            {
                int index = number - 1;
                key = string.Format("Column{0}", index);
                while (!dicColumns.ContainsKey(key))
                {        
                    index--;            
                    key = string.Format("Column{0}", index);

                    if (index < 0)
                        return;
                }

                col.Rows = dicColumns[string.Format("Column{0}", index)].Rows;
                //col.SpaceRows = dicColumns[string.Format("Column{0}", number - 1)].SpaceRows;
            }
            
            col.Visible = true;
            //if (comboAdvanceProperty.SelectedIndex == 1 && !string.IsNullOrEmpty(txtStep.Text))
            //{
            //    int step = 0;
            //    bool isDigit = int.TryParse(txtStep.Text, out step);
            //    if (isDigit)
            //    {
            //        col.SerialNumber = step * number;
            //    }
            //}

            //if (!string.IsNullOrEmpty(col.SpaceRows))
            //{
            //    int step = 0;
            //    bool isDigit = int.TryParse(col.SpaceRows, out step);
            //    if (isDigit)
            //    {
            //        col.SerialNumber = step * number;
            //    }
            //}

            //col.ColumnName = col.Name;
            col.HeaderText = col.ColumnName;
            col.Width = 5;
            dicColumns.Add(col.ColumnName, col);
            lbColumns.DataSource = new BindingSource(dicColumns, null);
            lbColumns.SelectedIndex = lbColumns.Items.Count - 1;
        }

        /// <summary>
        /// 方法说明：得到所有列的宽度
        /// 作    者：jason.tang
        /// 完成时间：2013-01-10
        /// </summary>
        /// <returns>列宽度和</returns>
        private int GetColumnWidthCount()
        {
            int total = 0;

            foreach (string key in dicColumns.Keys)
            {
                total += dicColumns[key].Width;
            }

            return total;
        }

        /// <summary>
        /// 方法说明：重置所有列宽
        /// 作      者：jason.tang
        /// 完成时间：2013-07-29
        /// </summary>
        private void ResizeColumnWidth()
        {
            DetailGridViewTextBoxColumn col = new DetailGridViewTextBoxColumn();
            Dictionary<string, DetailGridViewTextBoxColumn> dic = new Dictionary<string, DetailGridViewTextBoxColumn>();
            foreach (var key in dicColumns.Keys)
            {
                col = new DetailGridViewTextBoxColumn();
                col = dicColumns[key];
                //col.Name = dicColumns[key].Name;
                col.SerialNumber = dicColumns[key].SerialNumber;
                col.Rows = dicColumns[key].Rows;
                col.Visible = dicColumns[key].Visible;
                col.ColumnName = dicColumns[key].ColumnName;
                col.HeaderText = dicColumns[key].HeaderText;
                col.Width = 5;
                dic.Add(key, col);
            }

            dicColumns.Clear();
            dicColumns = dic;
            lbColumns.DataSource = new BindingSource(dic, null);
            lbColumns.SelectedIndex = lbColumns.Items.Count - 1;

        }

        #endregion

    }
}
