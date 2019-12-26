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
    /// 类型说明：特殊字符窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-02-19
    /// </summary>
    public partial class SpecialSymbolFrm : BaseSkinForm
    {
        #region 变量声明

        /// <summary>
        /// 定义一个窗体传值的委托
        /// </summary>
        /// <param name="pParam">传递的对象</param>
        public delegate void DelegateForm(object pParam);
        public event DelegateForm SymbolAddEvent;

        private string startupPath = string.Empty;

        #endregion

        public SpecialSymbolFrm()
        {
            InitializeComponent();
        }

        #region 窗体控件事件

        private void SpecialSymbolFrm_Load(object sender, EventArgs e)
        {
            InitSymbolData();

            //this.MouseWheel += new MouseEventHandler(SpecialSymbolFrm_MouseWheel);
            //this.dgvSymbol.MouseWheel += new MouseEventHandler(SpecialSymbolFrm_MouseWheel);
        }

        void SpecialSymbolFrm_MouseWheel(object sender, MouseEventArgs e)
        {
             //获取光标位置
              Point mousePoint = new Point(e.X, e.Y);
             //换算成相对本窗体的位置
             mousePoint.Offset(this.Location.X, this.Location.Y);
             //判断是否在panel内
             if (innerPanel.RectangleToScreen(innerPanel.DisplayRectangle).Contains(mousePoint))
             {
                 //滚动
                 innerPanel.AutoScrollPosition = new Point(0, innerPanel.VerticalScroll.Value - e.Delta);
                 customScrollbar.Value = innerPanel.VerticalScroll.Value;
             }
        }

        /// <summary>
        /// 保存特殊字符到Xml文件
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgvSymbol.DataSource;
            DataSet ds = new DataSet();
            if (dt.DataSet != null)
            {
                dt.DataSet.Tables.Clear();
            }

            ds.Tables.Add(dt);
            ds.WriteXml(startupPath + @"\symbol.xml");
        }

        /// <summary>
        /// 设置单元格是否可以编辑
        /// </summary>
        private void dgvSymbol_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //判断是否可以编辑
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //只有单元格设置可以编辑时才可以编辑
            if (cell.Tag == null || cell.Tag.ToString() != "Edit")
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 编辑单元格
        /// </summary>
        private void btnModify_Click(object sender, EventArgs e)
        {
            dgvSymbol.CurrentCell.Tag = "Edit";
            dgvSymbol.Focus();
        }

        /// <summary>
        /// 单元格编辑完成
        /// </summary>
        private void dgvSymbol_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvSymbol.CurrentCell.Tag = null;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 双击单元格，插入该特殊字符
        /// </summary>
        private void dgvSymbol_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSymbol.CurrentCell.Value == null || string.IsNullOrEmpty(dgvSymbol.CurrentCell.Value.ToString()))
                return;
            SymbolAddEvent(SetSymbol(dgvSymbol.CurrentCell.Value));
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SymbolAddEvent(SetSymbol(dgvSymbol.CurrentCell.Value));
            this.Close();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 方法说明：初始化特殊字符数据
        /// 作    者：jason.tang
        /// 完成时间：2013-02-19
        /// </summary>
        private void InitSymbolData()
        {
            try
            {
                startupPath = Application.StartupPath + "\\Resources";
                DataSet ds = new DataSet();
                ds.ReadXml(startupPath + @"\symbol.xml");
                if (ds.Tables.Count > 0)
                {
                    dgvSymbol.DataSource = ds.Tables[0];

                    int totalHeight = 0;
                    foreach (DataGridViewRow row in dgvSymbol.Rows)
                    {
                        row.Height = 30;
                        totalHeight += 30;
                    }
                    foreach (DataGridViewColumn col in dgvSymbol.Columns)
                    {
                        col.Width = 30;
                    }
                    dgvSymbol.Height = totalHeight;
                }

                this.customScrollbar.Minimum = this.innerPanel.VerticalScroll.Minimum;
                this.customScrollbar.Maximum = this.innerPanel.VerticalScroll.Maximum;
                this.customScrollbar.LargeChange = this.innerPanel.VerticalScroll.LargeChange;
                this.customScrollbar.SmallChange = this.innerPanel.VerticalScroll.SmallChange;
                this.customScrollbar.Value = this.innerPanel.VerticalScroll.Value;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 方法说明：设置特殊字符
        /// 作    者：jason.tang
        /// 完成时间：2013-02-20
        /// </summary>
        private object SetSymbol(object symbol)
        {
            return symbol;
        }

        #endregion

        private void customScrollbar_Scroll(object sender, EventArgs e)
        {
            innerPanel.VerticalScroll.Value = customScrollbar.Value + 1;
        }
        
    }
}
