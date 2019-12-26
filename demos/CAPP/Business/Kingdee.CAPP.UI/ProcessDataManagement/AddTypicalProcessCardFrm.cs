using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{
    public partial class AddTypicalProcessCardFrm : BaseSkinForm
    {
        public AddTypicalProcessCardFrm()
        {
            InitializeComponent();
            this.dgvCardList.AutoGenerateColumns = false;
        }

        private TypicalProcess _typicalProcessMod;
        public TypicalProcess TypicalProcessMod
        {
            get
            {
                return _typicalProcessMod;
            }
            set
            {
                _typicalProcessMod = value;
            }
        }
        public AddTypicalProcessCardFrm(TypicalProcess typicalProcess)
        {
            InitializeComponent();
            this._typicalProcessMod = typicalProcess;
        }

        /// <summary>
        /// process card module list
        /// </summary>
        public Dictionary<string, TypicalProcess> ProcessPlanningModuleDic
        {
            get;
            set;
        }

        private void btnSerach_Click(object sender, EventArgs e)
        {
            string cardName = txtProcessCardName.Text.Trim();
            if (string.IsNullOrEmpty(cardName))
                return;

            try
            {
                List<ProcessCard> processCardModuleList
                    = ProcessCardBLL.GetProcessCardListByCondition(cardName);

                this.dgvCardList.DataSource = processCardModuleList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selecedCardModuleNameIdDic.Count <= 0)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                List<TypicalProcess> _typicalProcessList = new List<TypicalProcess>();
                try
                {
                    int sort = 1;
                    /// add process planning module
                    foreach (var kv in selecedCardModuleNameIdDic)
                    {
                        TypicalProcess p = new TypicalProcess();
                        p.TypicalProcessId = Guid.NewGuid();
                        p.BType = BusinessType.Card;
                        p.ParentNode = TypicalProcessMod.ParentNode;
                        p.Name = kv.Value;
                        p.BusinessId = new Guid(kv.Key);
                        p.Sort = sort;

                        _typicalProcessList.Add(p);

                        sort++;
                    }

                    TypicalProcessBLL.AddTypicalProcess(_typicalProcessList);

                    Dictionary<string, TypicalProcess> pDic = new Dictionary<string, TypicalProcess>();
                    foreach (TypicalProcess p in _typicalProcessList)
                    {
                        pDic.Add(p.Name, p);
                    }

                    ProcessPlanningModuleDic = pDic;

                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.DialogResult = DialogResult.Cancel;
                }
            }
        }

        private void AddTypicalProcessCardFrm_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProcessCard> processCardModuleList
                    = ProcessCardBLL.GetDefaultProcessCardList();
                this.dgvCardList.DataSource = processCardModuleList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Dictionary<string, string> selecedCardModuleNameIdDic = new Dictionary<string, string>();

        private void dgvCardList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chkcel
                    = dgvCardList[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;

                DataGridViewTextBoxCell cardModuleNameCel
                    = dgvCardList[e.ColumnIndex + 2, e.RowIndex] as DataGridViewTextBoxCell;

                DataGridViewTextBoxCell cardModuleIdCel
                    = dgvCardList[e.ColumnIndex + 1, e.RowIndex] as DataGridViewTextBoxCell;


                string cardModuleName = cardModuleNameCel.Value.ToString();
                string cardModuleId = cardModuleIdCel.Value.ToString();
                bool isChecked = Convert.ToBoolean(chkcel.EditedFormattedValue);

                if (isChecked)
                {
                    if (!selecedCardModuleNameIdDic.ContainsKey(cardModuleId))
                    {
                        selecedCardModuleNameIdDic.Add(cardModuleId, cardModuleName);
                    }
                }
                else
                {
                    if (selecedCardModuleNameIdDic.ContainsKey(cardModuleId))
                    {
                        selecedCardModuleNameIdDic.Remove(cardModuleId);
                    }
                }

            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
