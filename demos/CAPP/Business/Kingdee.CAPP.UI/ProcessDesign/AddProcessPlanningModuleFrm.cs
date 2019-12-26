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
    public partial class AddProcessPlanningModuleFrm : BaseSkinForm
    {
        public AddProcessPlanningModuleFrm()
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
        public AddProcessPlanningModuleFrm(ProcessPlanningModule processPlanningModle)
        {
            InitializeComponent();
            this._processPlanningMod = processPlanningModle;
        }

        public Dictionary<string, ProcessPlanningModule> NewNodeNameDic
        {
            get;
            set;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProcessPlanningModule processPlanningModule = new ProcessPlanningModule();
            processPlanningModule.BusinessId = Guid.NewGuid();
            processPlanningModule.BType = BusinessType.Planning;
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

        private void txtBusinessName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 13)
            {
                e.SuppressKeyPress = true;
            }
        }
    }
}
