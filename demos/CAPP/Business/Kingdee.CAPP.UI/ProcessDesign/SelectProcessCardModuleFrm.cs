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

namespace Kingdee.CAPP.UI.ProcessDesign
{
    public partial class SelectProcessCardModuleFrm : BaseSkinForm
    {
        public ProcessPlanningTreeFrm ProcessPlanningTree
        {
            get;
            set;
        }
             
        public SelectProcessCardModuleFrm(ProcessPlanningTreeFrm processPlanningTree)
        {
            InitializeComponent();
            this.dgvProcessCardModule.AutoGenerateColumns = false;
            ProcessPlanningTree = processPlanningTree;
        }

        

        private void SelectProcessCardModuleFrm_Load(object sender, EventArgs e)
        {
            BindProcessCardModule(null);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            BindProcessCardModule(this.txtProcessCardName.Text.Trim());
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<ProcessCardModule> processCardModules = new List<ProcessCardModule>();
            ProcessCardModules.ForEach(p =>
                    {
                        if (selecedProcessCardModuleDic.ContainsKey(p.Id.ToString()))
                        {
                            processCardModules.Add(p);
                        }
                    });
            ProcessPlanningTree.SetCurrentNodeNewAddProcessCard(processCardModules);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        public List<ProcessCardModule> ProcessCardModules
        {
            get;
            set;
        }

        /// <summary>
        /// data bind
        /// </summary>
        /// <param name="name"></param>
        public void BindProcessCardModule(string name)
        {
            try
            {
                List<ProcessCardModule> processPlanningModuleList
                    = ProcessCardModuleBLL.GetProcessCardListByCondition(name);

                ProcessCardModules = processPlanningModuleList;

                dgvProcessCardModule.DataSource = processPlanningModuleList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        Dictionary<string, string> selecedProcessCardModuleDic = new Dictionary<string, string>();

        /// <summary>
        /// get selected row value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProcessCardModule_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                DataGridViewCheckBoxCell chkcel
                    = dgvProcessCardModule[e.ColumnIndex, e.RowIndex] as DataGridViewCheckBoxCell;

                DataGridViewTextBoxCell processProcedureNameCel
                    = dgvProcessCardModule[e.ColumnIndex + 2, e.RowIndex] as DataGridViewTextBoxCell;

                DataGridViewTextBoxCell processProcedureIdCel
                    = dgvProcessCardModule[e.ColumnIndex + 1, e.RowIndex] as DataGridViewTextBoxCell;


                string processProcedureName = processProcedureNameCel.Value.ToString();
                string processProcedureId = processProcedureIdCel.Value.ToString();

                bool isChecked = Convert.ToBoolean(chkcel.EditedFormattedValue);


                if (isChecked)
                {
                    if (!selecedProcessCardModuleDic.ContainsKey(processProcedureId))
                    {
                        selecedProcessCardModuleDic.Add(processProcedureId, processProcedureName);
                    }
                }
                else
                {
                    if (selecedProcessCardModuleDic.ContainsKey(processProcedureId))
                    {
                        selecedProcessCardModuleDic.Remove(processProcedureId);
                    }
                }

            }
        }

        private void txtProcessCardName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }
       
    }
}
