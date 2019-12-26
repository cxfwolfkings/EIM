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

namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class AddProcessCardModuleFrm : BaseSkinForm
    {
        #region 变量声明

        private List<ProcessPlanningModule> procesPlanningModuleList;

        #endregion

        #region 构造函数

        public AddProcessCardModuleFrm()
        {
            InitializeComponent();            
        }

        public AddProcessCardModuleFrm(ProcessPlanningModule processPlanningModule)
        {
            InitializeComponent();
            this._processPlanningMod = processPlanningModule;
        }

        #endregion

        #region 属性

        private ProcessPlanningModule _processPlanningMod;
        public ProcessPlanningModule ProcessPlanningMod
        {
            get
            {
                return _processPlanningMod;
            }
            set
            {
                _processPlanningMod = value;
            }
        }

        #endregion

        #region 窗体控件事件

        /// <summary>
        /// load process card module list
        /// defalut 100 row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddProcessCardModuleFrm_Load(object sender, EventArgs e)
        {
            try
            {
                List<ProcessCardModule> processCardModuleList
                    = ProcessCardModuleBLL.GetDefaultProcessCardList();
                this.dgvModuleCardList.AutoGenerateColumns = false;
                this.dgvModuleCardList.DataSource = processCardModuleList;

                procesPlanningModuleList = ProcessPlanningModuleBLL.GetProcesPlanningModuleList(_processPlanningMod.ParentNode);

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
                List<ProcessPlanningModule> _processCardModuleList = new List<ProcessPlanningModule>();
                try
                {
                    List<ProcessPlanningModule> listPlanningModules = ProcessPlanningModuleBLL.GetProcesPlanningModuleList(_processPlanningMod.ParentNode);
                    int sort = listPlanningModules.Count + 1;

                    /// add process planning module
                    foreach (var kv in selecedCardModuleNameIdDic)
                    {
                        ProcessPlanningModule p = new ProcessPlanningModule();
                        p.ProcessPlanningModuleId = Guid.NewGuid();
                        p.BType = BusinessType.Card;
                        p.ParentNode = ProcessPlanningMod.ParentNode;
                        p.Name = kv.Value;
                        p.BusinessId = new Guid(kv.Key);
                        p.Sort = sort;

                        _processCardModuleList.Add(p);

                        sort++;
                    }

                    ProcessPlanningModuleBLL.AddProcessPlanningModule(_processCardModuleList);

                    Dictionary<string, ProcessPlanningModule> pDic = new Dictionary<string, ProcessPlanningModule>();
                    foreach (ProcessPlanningModule p in _processCardModuleList)
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
        
        Dictionary<string, string> selecedCardModuleNameIdDic = new Dictionary<string, string>();
        /// <summary>
        /// Get selected value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvModuleCardList_CellContentClick(
            object sender, 
            DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chkcel 
                    = dgvModuleCardList[e.ColumnIndex,e.RowIndex] as DataGridViewCheckBoxCell;

                DataGridViewTextBoxCell cardModuleNameCel
                    = dgvModuleCardList[e.ColumnIndex + 1, e.RowIndex] as DataGridViewTextBoxCell;

                DataGridViewTextBoxCell cardModuleIdCel
                    = dgvModuleCardList[e.ColumnIndex + 2, e.RowIndex] as DataGridViewTextBoxCell;

              
                string cardModuleName = cardModuleNameCel.Value.ToString();
                string cardModuleId = cardModuleIdCel.Value.ToString();
                bool isChecked = Convert.ToBoolean(chkcel.EditedFormattedValue);
                
                if (isChecked)
                {
                    if (!selecedCardModuleNameIdDic.ContainsKey(cardModuleId))
                    {
                        selecedCardModuleNameIdDic.Add(cardModuleId,cardModuleName);
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

        /// <summary>
        /// process card module list
        /// </summary>
        public Dictionary<string, ProcessPlanningModule> ProcessPlanningModuleDic
        {
            get;
            set;
        }

        /// <summary>
        /// Get process planning module by serach
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerach_Click(object sender, EventArgs e)
        {
            string cardmoduleName = txtProcessCardModuleName.Text.Trim();
            //if (string.IsNullOrEmpty(cardmoduleName))
            //    return;

            try
            {
                List<ProcessCardModule> processCardModuleList
                    = ProcessCardModuleBLL.GetProcessCardListByCondition(cardmoduleName);

                this.dgvModuleCardList.DataSource = processCardModuleList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }       

        private void txtProcessCardModuleName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void dgvModuleCardList_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            //已经在规程里存在的不允许再次添加
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                string cardModuleId = dgv[e.ColumnIndex + 2, e.RowIndex].Value.ToString();
                if (procesPlanningModuleList != null && procesPlanningModuleList.Count > 0)
                {
                    ProcessPlanningModule planningModule = procesPlanningModuleList.FirstOrDefault(p => p.BusinessId == new Guid(cardModuleId));
                    if (planningModule != null)
                    {
                        e.Cancel = true;
                    }
                }                
            }
        }

        #endregion

    }
}
