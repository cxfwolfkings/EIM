using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class NewProcess : Form
    {
        private string _routingId;
        RoutingProcessRelationContext rprConext = null;

        public event EventHandler<ProcessEventArgs> AddProcess;
        public NewProcess(string routingId)
        {
            InitializeComponent();
            _routingId = routingId;
            rprConext = new RoutingProcessRelationContext(DbConfig.Connection);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            CProcess cprocess = new CProcess();
            cprocess.OperId = Guid.NewGuid().ToString();
            cprocess.Name = tbxProcessName.Text.Trim();
            cprocess.Code = tbxProcessCode.Text.Trim();
            cprocess.CreateDate = DateTime.Now.ToString();
            cprocess.Creator = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";
            cprocess.Remark = "";
            cprocess.UpdateDate = DateTime.Now.ToString();
            cprocess.UpdatePerson = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";

            RoutingProcessRelation routingProcessRelation = new RoutingProcessRelation();
            routingProcessRelation.RelationId = Guid.NewGuid().ToString();
            routingProcessRelation.OperId = cprocess.OperId;
            routingProcessRelation.RoutingId = _routingId;
            routingProcessRelation.Seq = 1;
            routingProcessRelation.WorkcenterId = "";
            routingProcessRelation.Persons = 1;
            routingProcessRelation.ProcessTime = 0;
            routingProcessRelation.ProcessTimeUnit = 1;
            routingProcessRelation.LaborCosts = "0";
            routingProcessRelation.OperCosts = "";
            routingProcessRelation.ProcessCosts = "0";
            routingProcessRelation.Creator = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";
            routingProcessRelation.CreateDate = DateTime.Now.ToString();
            routingProcessRelation.UpdateDate = DateTime.Now.ToString();
            routingProcessRelation.UpdatePerson = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";


            try
            {
                rprConext.Processes.InsertOnSubmit(cprocess);
                rprConext.RoutingProcessRelation.InsertOnSubmit(routingProcessRelation);

                rprConext.SubmitChanges();

                MessageBox.Show("新增工序成功！");
                this.Close();

                /// 新增工序到右边工序树下
                if (ProcessLine.CurrentProcessLine != null)
                {
                    AddProcess += new EventHandler<ProcessEventArgs>(ProcessLine.CurrentProcessLine.CurrentProcess_AddProcess);
                }
                /************************************************************************/
                /* 触发事件                                                                     
                /************************************************************************/
                if(AddProcess != null)
                {
                    AddProcess(this, new ProcessEventArgs() 
                    { 
                        ProcessName = tbxProcessName.Text.Trim(), 
                        ProcessId = cprocess.OperId 
                    });
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
} 