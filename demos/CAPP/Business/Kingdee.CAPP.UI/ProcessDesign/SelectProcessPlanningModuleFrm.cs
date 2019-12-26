using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.BLL;
/*******************************
 * Created By franco
 * Description: Select Process Planning Module
 *******************************/

namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class SelectProcessPlanningModuleFrm : BaseSkinForm
    {
        public NewProcessProcedureFrm NewProcessProcedure
        {
            get;
            set;
        }

        public SelectProcessPlanningModuleFrm(NewProcessProcedureFrm newProcessProcedureFrm)
        {
            InitializeComponent();
            NewProcessProcedure = newProcessProcedureFrm;
            this.dgvProcessProcedure.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectProcessPlanningModuleFrm_Load(object sender, EventArgs e)
        {
            BindProcessProcedure(null);
        }

        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// query by condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindProcessProcedure(txtProcessProcedureName.Text.Trim());
        }

        /// <summary>
        /// select process procedure model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<ProcessCardModule> processCardModuleList
                = new List<ProcessCardModule>();

            ProcessPlanningModuleBLL processPlanningModuleBLL = new ProcessPlanningModuleBLL();

            List<Guid> processPlanningModuleIdList = new List<Guid>();

            

            try
            {
                ///根据规程模板Id，得到卡片模版的Id
                foreach (var kv in selecedProcessProcedureDic)
                {                   
                    processPlanningModuleIdList.Add(kv.Key);
                }

                /// 得到选中的卡片模板列表
                processCardModuleList = ProcessPlanningModuleBLL
                    .GetProcessCardModuleListByProcessPlanningModuleIds(processPlanningModuleIdList);

                NewProcessProcedure.SetCklProcessCardModuleName(processCardModuleList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /// Close Current window and show Process Procedure TreeView
            Close();

            

        }



        /// <summary>
        /// bind process procedure to datagridview
        /// </summary>
        /// <param name="name"></param>
        private void BindProcessProcedure(string name)
        {
            try
            {
                List<ProcessPlanningModule> processPlanningModuleList
                    = ProcessPlanningModuleBLL.GetProcesPlanningModuleListByName(name);

                dgvProcessProcedure.DataSource = processPlanningModuleList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Dictionary<Guid, string> selecedProcessProcedureDic = new Dictionary<Guid, string>();

        /// <summary>
        /// get selected row value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessProcedure_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chkcel
                    = dgvProcessProcedure[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;

                DataGridViewTextBoxCell processProcedureNameCel
                    = dgvProcessProcedure[e.ColumnIndex + 2, e.RowIndex] as DataGridViewTextBoxCell;

                DataGridViewTextBoxCell processProcedureIdCel
                    = dgvProcessProcedure[e.ColumnIndex + 1, e.RowIndex] as DataGridViewTextBoxCell;


                string processProcedureName = processProcedureNameCel.Value.ToString();
                Guid processProcedureId = (Guid)processProcedureIdCel.Value;

                bool isChecked = Convert.ToBoolean(chkcel.EditedFormattedValue);


                if (isChecked)
                {
                    if (!selecedProcessProcedureDic.ContainsKey(processProcedureId))
                    {
                        selecedProcessProcedureDic.Add(processProcedureId, processProcedureName);
                    }
                }
                else
                {
                    if (selecedProcessProcedureDic.ContainsKey(processProcedureId))
                    {
                        selecedProcessProcedureDic.Remove(processProcedureId);
                    }
                }

                DataGridView dgv = (DataGridView)sender;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Index != e.RowIndex && isChecked)
                    {
                        row.Cells[e.ColumnIndex].Value = false;
                    }
                }
                dgv.Invalidate();

            }
        }

        /// <summary>
        /// 结合CellContentClick事件设置复选框单选
        /// </summary>
        private void dgvProcessProcedure_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void txtProcessProcedureName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
                
    }
}
