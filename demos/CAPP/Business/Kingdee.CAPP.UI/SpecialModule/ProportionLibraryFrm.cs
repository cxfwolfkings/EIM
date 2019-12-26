using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.SpecialModule
{
    /// <summary>
    /// 类型说明：米重量维护界面
    /// 作    者：jason.tang
    /// 完成时间：2013-09-25
    /// </summary>
    public partial class ProportionLibraryFrm : BaseSkinForm
    {
        public ProportionLibraryFrm()
        {
            InitializeComponent();
        }

        private void comboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData(comboType.SelectedIndex);
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMeterWeight.Text))
            {
                MessageBox.Show("米重量不能为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal meterWeight = 0;

            if (!decimal.TryParse(txtMeterWeight.Text, out meterWeight))
            {
                MessageBox.Show("米重量格式不正确，只能为整数或小数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvMaterialQuota.CurrentRow != null)
            {
                if (dgvMaterialQuota.CurrentRow.Cells["Id"] != null &&
                    dgvMaterialQuota.CurrentRow.Cells["Id"].Value != null &&
                    !string.IsNullOrEmpty(dgvMaterialQuota.CurrentRow.Cells["Id"].Value.ToString()))
                {
                    bool result = MaterialQuotaBLL.UpdateMeterWeight(dgvMaterialQuota.CurrentRow.Cells["Id"].Value.ToString(), comboType.SelectedIndex, meterWeight);
                    if (result)
                        BindData(comboType.SelectedIndex);
                }
            }
        }

        private void txtMeterWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键、小数点，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            else if (txtMeterWeight.Text.StartsWith("0") && !txtMeterWeight.Text.Contains(".")
                && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            else if (txtMeterWeight.Text.EndsWith(".") && e.KeyChar == (char)46)
            {
                e.Handled = true;
            }
            else if (txtMeterWeight.Text.Contains(".") && e.KeyChar == (char)46)
            {
                e.Handled = true;
            }
        }

        private void txtMeterWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvMaterialQuota_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMaterialQuota.CurrentRow != null)
            {
                if (dgvMaterialQuota.CurrentRow.Cells["Id"] != null &&
                    dgvMaterialQuota.CurrentRow.Cells["Id"].Value != null &&
                    !string.IsNullOrEmpty(dgvMaterialQuota.CurrentRow.Cells["Id"].Value.ToString()))
                {
                    txtMeterWeight.Text = dgvMaterialQuota.CurrentRow.Cells["colMeterWeight"].Value.ToString();
                }                
            }
        }

        private void ProportionLibraryFrm_Load(object sender, EventArgs e)
        {
            if (comboType.Items.Count > 0)
                comboType.SelectedIndex = 0;
            BindData(0);
        }

        private void BindData(int type)
        {
            DataTable dt = MaterialQuotaBLL.GetMeterWeight(type);            
            dgvMaterialQuota.DataSource = dt;
            if (dt.Columns.Contains("CategoryName"))
            {
                dgvMaterialQuota.Columns[4].Visible = type == 1;
                dgvMaterialQuota.Columns[4].HeaderText = "业务类型";
            }
        }
    }
}
