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
    public partial class ProcessPlanningModuleFolderFrm : BaseSkinForm
    {
        public ProcessPlanningModuleFolderFrm()
        {
            InitializeComponent();
        }

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
        /// <summary>
        /// init Process card module
        /// </summary>
        /// <param name="processPlanningModule"></param>
        public ProcessPlanningModuleFolderFrm(ProcessPlanningModule processPlanningModule)
        {
            InitializeComponent();

            _processPlanningMod = processPlanningModule;
        }


        /// <summary>
        /// Cancel add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Add ProcessPlanning module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProcessPlanningModule processPlanningModule = new ProcessPlanningModule();
            processPlanningModule.BusinessId = ProcessPlanningMod.BusinessId;
            processPlanningModule.BType = BusinessType.Folder;
            processPlanningModule.Name = txtBusinessName.Text.Trim();
            processPlanningModule.ParentNode = ProcessPlanningMod.ParentNode;
            processPlanningModule.ProcessPlanningModuleId = Guid.NewGuid();

            try
            {
                int currentNode 
                    = ProcessPlanningModuleBLL.AddProcessPlanningModule(processPlanningModule);

                NewNodeNameDic
                    = new Dictionary<string, ProcessPlanningModule>() 
                        {
                            { txtBusinessName.Text.Trim(), processPlanningModule } 
                        };
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.DialogResult = DialogResult.Cancel;
            }

            
        }
        /// <summary>
        /// add process card module name
        /// </summary>
        public Dictionary<string, ProcessPlanningModule> NewNodeNameDic
        {
            get;
            set;
        }

        private void txtBusinessName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
   }
}
