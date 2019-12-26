using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    /// <summary>
    /// 类型说明：典型工艺分类界面
    /// 作      者：jason.tang
    /// 完成时间：2013-07-23
    /// </summary>
    public partial class TypicalCategoryFrm : BaseSkinForm
    {
        #region 属性

        public string ProcessCardId { get; set; }
        public string ProcessCardName { get; set; }
        public Dictionary<string,string> ProcessCardIds { get; set; }

        #endregion

        #region 变量

        private DataTable dt = null;

        #endregion

        #region 构造函数

        public TypicalCategoryFrm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体控件事件

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {            
            DataRow[] rows = dt.Select(string.Format("Type = 4 And Name like '%{0}%'", txtName.Text));
            DataTable table = dt.Clone();
            foreach (DataRow dr in rows)
            {
                DataRow row = table.NewRow();
                row = dr;
                table.Rows.Add(row.ItemArray);
            }
            dgvTypicalCategory.DataSource = table;
        }

        private void TypicalCategoryFrm_Load(object sender, EventArgs e)
        {
            dgvTypicalCategory.AutoGenerateColumns = false;
            
            GetTypicalCategory();

            if (!string.IsNullOrEmpty(ProcessCardName))
                txtName.Text = ProcessCardName;
            else if (dgvTypicalCategory.Rows.Count > 0)
            {
                if (dgvTypicalCategory.Rows[0].Cells["colName"] != null &&
                   dgvTypicalCategory.Rows[0].Cells["colName"].Value != null &&
                   !string.IsNullOrEmpty(dgvTypicalCategory.Rows[0].Cells["colName"].Value.ToString()))
                {
                    txtName.Text = dgvTypicalCategory.Rows[0].Cells["colName"].Value.ToString();
                }
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string name = string.Empty;
            string parentNode = string.Empty;
            foreach (DataGridViewRow row in dgvTypicalCategory.Rows)
            {
                if ((bool)row.Cells[0].EditedFormattedValue == true &&
                    row.Cells["colName"].Value != null)
                    {
                        name = row.Cells["colName"].Value.ToString();
                        parentNode = row.Cells["colCurrentNode"].Value.ToString();
                        break;
                    }               
            }

            if(string.IsNullOrEmpty(name))
                return;

            try
            {
                if (ProcessCardIds != null && ProcessCardIds.Count > 0)
                {
                    foreach (string key in ProcessCardIds.Keys)
                    {
                        Guid cardid = new Guid(key);
                        bool result = TypicalProcessBLL.ExistTypcialProcessCard(cardid, int.Parse(parentNode));
                        if (!result)
                        {
                            TypicalProcess typical = new TypicalProcess();
                            typical.Name = ProcessCardIds[key];
                            typical.CurrentNode = dt.Rows.Count + 1;
                            typical.BusinessId = cardid;
                            typical.BType = BusinessType.Card;
                            typical.ParentNode = int.Parse(parentNode);
                            TypicalProcessBLL.AddTypicalProcess(typical);
                        }
                    }
                }
                else
                {
                    Guid cardid = new Guid(ProcessCardId);
                    bool result = TypicalProcessBLL.ExistTypcialProcessCard(cardid, int.Parse(parentNode));
                    if (!result)
                    {
                        TypicalProcess typical = new TypicalProcess();
                        typical.Name = ProcessCardName;
                        typical.CurrentNode = dt.Rows.Count + 1;
                        typical.BusinessId = cardid;
                        typical.BType = BusinessType.Card;
                        typical.ParentNode = int.Parse(parentNode);
                        TypicalProcessBLL.AddTypicalProcess(typical);
                    }
                }

                if (TypicalProcessFrm.typicalProcessForm != null)
                {
                    TypicalProcessFrm.typicalProcessForm.ChangeToTypical();
                }

                MessageBox.Show("卡片转为典型成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch
            {
                MessageBox.Show("卡片转为典型失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvTypicalCategory_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == 0 && dgv.Rows.Count > 0)
            {                
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                bool value = (bool)checkCell.Value;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Index != e.RowIndex && value)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
                dgv.Invalidate();
            }
        }

        private void dgvTypicalCategory_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvTypicalCategory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTypicalCategory.CurrentRow != null)
            {
                if (dgvTypicalCategory.CurrentRow.Cells["colName"] != null &&
                    dgvTypicalCategory.CurrentRow.Cells["colName"].Value != null &&
                    !string.IsNullOrEmpty(dgvTypicalCategory.CurrentRow.Cells["colName"].Value.ToString()))
                {
                    txtName.Text = dgvTypicalCategory.CurrentRow.Cells["colName"].Value.ToString();
                }
            }
        }

        #endregion

        #region 方法

        private void GetTypicalCategory()
        {
            dt = TypicalProcessBLL.GetTypicalCategory(string.Empty);
            DataRow[] rows = dt.Select("Type = 4 ");
            DataTable table = dt.Clone();
            foreach (DataRow dr in rows)
            {
                DataRow row = table.NewRow();
                row = dr;
                table.Rows.Add(row.ItemArray);
            }
            dgvTypicalCategory.DataSource = table;
        }

        #endregion
        
    }
}
