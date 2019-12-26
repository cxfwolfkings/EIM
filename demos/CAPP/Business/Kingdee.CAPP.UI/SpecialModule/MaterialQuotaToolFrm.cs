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
using Kingdee.CAPP.Common;
using System.Reflection;

namespace Kingdee.CAPP.UI.SpecialModule
{
    /// <summary>
    /// 类型说明：材料定额设置界面
    /// 作      者：jason.tang
    /// 完成时间：2013-05-23
    /// </summary>
    public partial class MaterialQuotaToolFrm : BaseSkinForm
    {
        #region 变量声明

        private MaterialQuota materialQuota;
        private DataTable dtMaterialQuota;
        /// <summary>
        /// 操作符
        /// </summary>
        private List<string> listOperators;

        /// <summary>
        /// 数字
        /// </summary>
        private List<string> listNumbers;

        #endregion

        #region 属性

        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialVerId { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        private Dictionary<string, string> _keyWords = null;
        private Dictionary<string, string> KeyWords
        {
            set
            {
                _keyWords = value;
            }
            get
            {
                return _keyWords;
            }
        }  

        #endregion

        #region 窗体控件事件

        public MaterialQuotaToolFrm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新增
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgvMaterialQuota.CurrentRow == null || !CheckInput())
                return;

            materialQuota = new MaterialQuota();
            materialQuota.Proportion = decimal.Parse(txtProportion.Text);
            materialQuota.ValidDigits = int.Parse(txtValidDigits.Text);
            materialQuota.Formula = txtFormula.Text;
            materialQuota.Id = Guid.NewGuid();
            materialQuota.Code = dgvMaterialQuota.CurrentRow.Cells["colCode"].Value.ToString();

            bool result = MaterialQuotaBLL.AddMaterialQuotaData(materialQuota);
            if (result)
            {
                LoadData(MaterialVerId);
                dgvMaterialQuota.ClearSelection();
                MessageBox.Show("材料定额信息新增成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvMaterialQuota.Rows[dgvMaterialQuota.Rows.Count - 1].Selected = true;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMaterialQuota.CurrentRow == null || !CheckInput())
                return;

            string id = dgvMaterialQuota.CurrentRow.Cells["Id"].Value.ToString();
            int rowIndex = dgvMaterialQuota.CurrentRow.Index;

            materialQuota = new MaterialQuota();
            materialQuota.Proportion = decimal.Parse(txtProportion.Text);
            materialQuota.ValidDigits = int.Parse(txtValidDigits.Text);
            materialQuota.Formula = txtFormula.Text;            
            materialQuota.Id = new Guid(id);

            bool result = MaterialQuotaBLL.UpdateMaterialQuotaData(materialQuota);
            if (result)
            {
                _keyWords["Proportion"] = materialQuota.Proportion.ToString();
                _keyWords["ValidDigits"] = materialQuota.ValidDigits.ToString();
                _keyWords["Formula"] = materialQuota.Formula.ToString();

                string quota = Calc();
                if (string.IsNullOrEmpty(quota))
                    return;

                materialQuota.Quota = string.IsNullOrEmpty(quota) ? decimal.MinValue : decimal.Parse(quota);
                MaterialQuotaBLL.UpdateQuotaData(materialQuota);                
                
                LoadData(MaterialVerId);
                dgvMaterialQuota.ClearSelection();

                MessageBox.Show("材料定额修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvMaterialQuota.Rows[rowIndex].Selected = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 检索米重量
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string code = dgvMaterialQuota.CurrentRow.Cells["colCode"].Value.ToString();
            if (code != txtCode.Text)
                return;

            DataRow[] rows = dtMaterialQuota.Select(string.Format("Code='{0}'", code));

            if (rows != null && rows.Length > 0)
            {
                dgvMaterialQuota.CurrentRow.Cells["colProportion"].Value = rows[0]["Proportion"].ToString();
            }
        }

        private void MaterialQuotaToolFrm_Load(object sender, EventArgs e)
        {
            dgvMaterialQuota.AutoGenerateColumns = false;

            if (!string.IsNullOrEmpty(MaterialVerId))
            {
                LoadData(MaterialVerId);

                if (dgvMaterialQuota.Rows.Count > 0)
                {
                    if (dgvMaterialQuota.Rows[0].Cells["Id"] != null &&
                       dgvMaterialQuota.Rows[0].Cells["Id"].Value != null &&
                       !string.IsNullOrEmpty(dgvMaterialQuota.Rows[0].Cells["Id"].Value.ToString()))
                    {
                        txtProportion.Text = dgvMaterialQuota.Rows[0].Cells["colProportion"].Value.ToString();
                        txtValidDigits.Text = dgvMaterialQuota.Rows[0].Cells["colValidDigits"].Value.ToString();
                        txtFormula.Text = dgvMaterialQuota.Rows[0].Cells["colFormula"].Value.ToString();

                        btnAdd.Enabled = false;
                        btnUpdate.Enabled = true;

                        _keyWords = new Dictionary<string, string>();
                        _keyWords.Add("Proportion", txtProportion.Text);
                        _keyWords.Add("ValidDigits", txtValidDigits.Text);
                        _keyWords.Add("ChildCount", dgvMaterialQuota.CurrentRow.Cells["colChildCount"].Value.ToString());
                        _keyWords.Add("Waste", dgvMaterialQuota.CurrentRow.Cells["colWaste"].Value.ToString());
                        _keyWords.Add("Formula", dgvMaterialQuota.CurrentRow.Cells["colFormula"].Value.ToString());
                    }
                    else
                    {
                        btnAdd.Enabled = true;
                        btnUpdate.Enabled = false;

                        txtProportion.Text = "";
                        txtValidDigits.Text = "";
                        txtFormula.Text = "";

                        _keyWords = null;
                    }
                }
                //List<string> listCodes = new List<string>();
                //foreach (DataRow row in dt.Rows)
                //{
                //    listCodes.Add(string.Format("'{0}'", row["Code"].ToString()));
                //}
                //string codes = string.Join(",", listCodes.ToArray());
                //GetProportionData(codes);

                
            }

            listOperators = new List<string>();
            listOperators.Add("+");
            listOperators.Add("-");
            listOperators.Add("*");
            listOperators.Add("/");
            listOperators.Add("(");
            listOperators.Add(")");

            listNumbers = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                listNumbers.Add(i.ToString());
            }
        }

        /// <summary>
        /// 获取DataGridView行内容
        /// </summary>       
        private void dgvMaterialQuota_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 限制有效位数只能输入整数
        /// </summary>
        private void txtValidDigits_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if (txtValidDigits.Text.StartsWith("0") && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
            else if ((e.KeyChar < '0' || e.KeyChar > '8') && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 限制米重量只能输入整数或小数
        /// </summary>
        private void txtProportion_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键、小数点，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            else if (txtProportion.Text.StartsWith("0") && !txtProportion.Text.Contains(".")
                && e.KeyChar != (char)8 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            else if (txtProportion.Text.EndsWith(".") && e.KeyChar == (char)46)
            {
                e.Handled = true;
            }
            else if (txtProportion.Text.Contains(".") && e.KeyChar == (char)46)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 计算材料定额
        /// </summary>
        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (dgvMaterialQuota.CurrentRow == null)
                return;

            //string quota = Calc();

            //if (string.IsNullOrEmpty(quota))
            //    return;
            
            

            List<MaterialQuota> listOldMaterialQuota = new List<MaterialQuota>();
            List<MaterialQuota> listNewMaterialQuota = new List<MaterialQuota>();

            foreach (DataGridViewRow row in dgvMaterialQuota.Rows)
            {
                string id = row.Cells["Id"].Value.ToString();
                bool isNew = false;
                if (string.IsNullOrEmpty(id))
                {
                    id = Guid.NewGuid().ToString();
                    isNew = true;
                }

                materialQuota = new MaterialQuota();
                decimal meterWeight = decimal.Parse(row.Cells["colMeterWeight"].Value.ToString());
                decimal quota = decimal.Parse(row.Cells["colChildCount"].Value.ToString()) * meterWeight;
                dgvMaterialQuota.CurrentRow.Cells["colMaterialQuota"].Value = quota;                
                materialQuota.Quota = quota;
                materialQuota.Id = new Guid(id);

                if (isNew)
                {
                    materialQuota.Code = row.Cells["colCode"].Value.ToString();
                    listNewMaterialQuota.Add(materialQuota);
                }
                else
                    listOldMaterialQuota.Add(materialQuota);
            }

            foreach (MaterialQuota material in listOldMaterialQuota)
            {
                bool result = MaterialQuotaBLL.UpdateQuotaData(material);
            }

            foreach (MaterialQuota material in listNewMaterialQuota)
            {
                bool result = MaterialQuotaBLL.AddMaterialQuotaData(material);
            }
            LoadData(MaterialVerId);
        }        

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
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
                    txtProportion.Text = dgvMaterialQuota.CurrentRow.Cells["colProportion"].Value.ToString();
                    txtValidDigits.Text = dgvMaterialQuota.CurrentRow.Cells["colValidDigits"].Value.ToString();
                    txtFormula.Text = dgvMaterialQuota.CurrentRow.Cells["colFormula"].Value.ToString();

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;

                    _keyWords = new Dictionary<string, string>();
                    _keyWords.Add("Proportion", txtProportion.Text);
                    _keyWords.Add("ValidDigits", txtValidDigits.Text);
                    _keyWords.Add("ChildCount", dgvMaterialQuota.CurrentRow.Cells["colChildCount"].Value.ToString());
                    _keyWords.Add("Waste", dgvMaterialQuota.CurrentRow.Cells["colWaste"].Value.ToString());
                    _keyWords.Add("Formula", dgvMaterialQuota.CurrentRow.Cells["colFormula"].Value.ToString());
                }
                else
                {
                    btnAdd.Enabled = true;
                    btnUpdate.Enabled = false;

                    txtProportion.Text = "";
                    txtValidDigits.Text = "";
                    txtFormula.Text = "";

                    _keyWords = null;
                }
            }
        }

        /// <summary>
        /// 格式化小数位
        /// </summary>
        private void dgvMaterialQuota_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string digit = dgvMaterialQuota.Rows[e.RowIndex].Cells["colValidDigits"].Value.ToString();
            if (e.Value != null && e.Value != DBNull.Value && e.ColumnIndex == 7)
            {
                double value = double.Parse(e.Value.ToString());
                e.Value = value.ToString(string.Format("f{0}", digit));
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取米重量
        /// </summary>
        private void GetProportionData(string codes)
        {
            dtMaterialQuota = MaterialQuotaBLL.GetMaterialQuotaData(codes, txtProperty.Text);       
        }

        /// <summary>
        /// 加载材料定额数据
        /// </summary>
        /// <param name="materiaVerId"></param>
        private void LoadData(string materiaVerId)
        {
            DataTable dt = MaterialQuotaBLL.GetMaterialVersionData(materiaVerId);
            dgvMaterialQuota.DataSource = dt;
        }

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <returns>True/False</returns>
        private bool CheckInput()
        {
            bool result = true;

            foreach (Control control in groupBox3.Controls)
            {
                if (control is TextBox && string.IsNullOrEmpty(((TextBox)control).Text))
                {
                    MessageBox.Show(string.Format("{0}不能为空", ((TextBox)control).Tag));
                    ((TextBox)control).Focus();
                    return false;
                }
            }

            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) || 
                txtFormula.Text.EndsWith(".") || txtFormula.Text.EndsWith(listOperators[4]))
            {
                MessageBox.Show("公式不合法");
                return false;
            }

            return result;
        }

        /// <summary>
        /// 计算材料定额
        /// </summary>
        /// <returns></returns>
        private string Calc()
        {
            if (string.IsNullOrEmpty(_keyWords["Formula"]))
                return null;

            if (_keyWords == null)
                return null;

            string sourceExpression = _keyWords["Formula"].ToString();
            foreach (string key in _keyWords.Keys)
            {
                if (sourceExpression.Contains(key))
                {
                    sourceExpression = sourceExpression.Replace(key, _keyWords[key]);
                }
            }

            MaterialCalc calc = new MaterialCalc(sourceExpression);
            string digits = _keyWords["ValidDigits"];
            double quota = calc.EvaluateExpression();
            if (quota == double.MinValue)
            {
                MessageBox.Show("公式不合法", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            return quota.ToString(string.Format("f{0}", digits)); 
        }

        #endregion

        /// <summary>
        /// 加法
        /// </summary>
        private void btnAddition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }

            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) && !txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[0];
            }
        }

        /// <summary>
        /// 减法
        /// </summary>
        private void btnSubtraction_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }

            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) && !txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[1];
            }
        }

        /// <summary>
        /// 乘法
        /// </summary>
        private void btnMultiplication_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }

            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) && !txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[2];
            }
        }

        /// <summary>
        /// 除法
        /// </summary>
        private void btnDivision_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }

            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) && !txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[3];
            }
        }

        /// <summary>
        /// 左括号
        /// </summary>
        private void btnLeftDash_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listOperators[4];
                return;
            }

            if (txtFormula.Text.EndsWith(listOperators[5]) || txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||txtFormula.Text.EndsWith(btnWaste.Tag.ToString()))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[4];
            }
        }

        /// <summary>
        /// 右括号
        /// </summary>
        private void btnRightDash_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }

            string end = txtFormula.Text.Substring(txtFormula.Text.Length - 1);
            if ((listOperators.Contains(end) && end != listOperators[5]) || !txtFormula.Text.Contains(listOperators[4]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listOperators[5];
            }
        }

        /// <summary>
        /// 退格
        /// </summary>
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (txtFormula.Text.Length - 1 < 0)
                return;
            if (listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) ||
                listNumbers.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)))
            {
                txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.Length - 1);
            }
            else
            {
                if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()))
                {
                    txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.Length - btnProportion.Tag.ToString().Length).Trim();
                }
                else if (txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()))
                {
                    txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.Length - btnChildCount.Tag.ToString().Length).Trim();
                }
                else if (txtFormula.Text.EndsWith(btnWaste.Tag.ToString()))
                {
                    txtFormula.Text = txtFormula.Text.Substring(0, txtFormula.Text.Length - btnWaste.Tag.ToString().Length).Trim();
                }
            }
        }

        /// <summary>
        /// 比重
        /// </summary>
        private void btnProportion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = btnProportion.Tag.ToString();
                return;
            }

            if (!listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) || txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += btnProportion.Tag.ToString();
            }
        }

        /// <summary>
        /// 用量
        /// </summary>
        private void btnChildCount_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = btnChildCount.Tag.ToString();
                return;
            }

            if (!listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) || txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += btnChildCount.Tag.ToString();
            }
        }

        /// <summary>
        /// 损耗率
        /// </summary>
        private void btnWaste_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = btnWaste.Tag.ToString();
                return;
            }

            if (!listOperators.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)) || txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += btnWaste.Tag.ToString();
            }
        }

        /// <summary>
        /// One
        /// </summary>
        private void btnOne_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[1];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[1];
            }
        }

        /// <summary>
        /// Two
        /// </summary>
        private void btnTwo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[2];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) || 
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) || 
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[2];
            }
        }

        /// <summary>
        /// Three
        /// </summary>
        private void btnThree_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[3];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[3];
            }
        }

        /// <summary>
        /// Four
        /// </summary>
        private void btnFour_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[4];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[4];
            }
        }

        /// <summary>
        /// Five
        /// </summary>
        private void btnFive_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[5];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[5];
            }
        }

        /// <summary>
        /// Six
        /// </summary>
        private void btnSix_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[6];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[6];
            }
        }

        /// <summary>
        /// Serven
        /// </summary>
        private void btnServen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[7];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[7];
            }
        }

        /// <summary>
        /// Eight
        /// </summary>
        private void btnEight_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[8];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[8];
            }
        }

        /// <summary>
        /// Nine
        /// </summary>
        private void btnNine_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[9];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[9];
            }
        }

        /// <summary>
        /// Zero
        /// </summary>
        private void btnZero_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                txtFormula.Text = listNumbers[0];
                return;
            }

            if (txtFormula.Text.EndsWith(btnProportion.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnChildCount.Tag.ToString()) ||
                txtFormula.Text.EndsWith(btnWaste.Tag.ToString()) ||
                txtFormula.Text.EndsWith(listOperators[5]))
            {
                return;
            }
            else
            {
                txtFormula.Text += listNumbers[0];
            }
        }

        /// <summary>
        /// Dot
        /// </summary>
        private void btnDot_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFormula.Text))
            {
                return;
            }
            
            if (!listNumbers.Contains(txtFormula.Text.Substring(txtFormula.Text.Length - 1)))
            {
                return;
            }
            else
            {
                if (!CheckNumberDot())
                    return;
                txtFormula.Text += btnDot.Text;
            }
        }

        /// <summary>
        /// 方法说明：检查当前数字输入是否包含多个小数点
        /// </summary>
        private bool CheckNumberDot()
        {
            int startlength = txtFormula.Text.Length - 1;
            
            for (int i = 0; i < startlength; i++)
            {
                string str = txtFormula.Text.Substring(startlength - i, 1);
                string pre = txtFormula.Text.Substring(startlength - i - 1, 1);
                if (listNumbers.Contains(str) && pre == btnDot.Text)
                {
                    return false;
                }
                else if (!listNumbers.Contains(str))
                {
                    break;
                }
            }

            return true;
        }
    }
}
