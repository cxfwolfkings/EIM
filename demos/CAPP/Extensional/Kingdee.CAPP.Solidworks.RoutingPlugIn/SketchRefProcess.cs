using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;


namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class SketchRefProcess : Form
    {
        RoutingProcessRelationContext rprContext = null;
        SketchProcessContent spContent = null;
        private string _routingId = string.Empty;
        private string _operId = string.Empty;
        private string _componentName = string.Empty;


        IModelDoc2 pDoc = null;
        public SketchRefProcess(
            string routingId,
            string operId,
            string componentName,
            IModelDoc2 doc)
        {
            InitializeComponent();
            spContent = new SketchProcessContent(DbConfig.Connection);
            rprContext = new RoutingProcessRelationContext(DbConfig.Connection);

            _routingId = routingId;
            _operId = operId;
            _componentName = componentName;
            pDoc = doc;

            CurrentSketchRefProcess = this;
        }

        public static SketchRefProcess CurrentSketchRefProcess
        {
            get;
            set;
        }

        //List<CProcess> cprocessList = new List<CProcess>();
        List<LItem> items = new List<LItem>();

        private void SketchRefProcess_Load(object sender, EventArgs e)
        {
            GetAllSketch();
        }

        void GetAllSketch()
        {
            cklProcessList.Items.Clear();
            Dictionary<string, bool> sketchDic = GlobalCache.Instance.SketchStatusDic;

            if (sketchDic == null) return;
            foreach (var kv in sketchDic)
            {
                cklProcessList.Items.Add(kv.Key);
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> itemList = new List<string>();
            for (int i = 0; i < cklProcessList.Items.Count; i++)
            {
                if (cklProcessList.GetItemChecked(i))
                {
                    //LItem li = (LItem)cklProcessList.Items[i];
                    itemList.Add(cklProcessList.Items[i].ToString());
                }
            }

            ///save relation

            try
            {
                foreach (var sketchName in itemList)
                {
                    CSketchFileProcess sp = new CSketchFileProcess();
                    sp.SketchProcessId = Guid.NewGuid().ToString();
                    sp.OperId = _operId;
                    sp.RoutingId = _routingId;
                    sp.SketchName = sketchName;
                    sp.ComponentName = _componentName;
                    sp.ComponentPath = _componentName;

                    spContent.SketchFileProcesses.InsertOnSubmit(sp);
                }
                spContent.SubmitChanges();

                MessageBox.Show("关联草图到工序成功！");

                ProcessLine processline = ProcessLine.CurrentProcessLine;
                processline.SetTreeNodeBackColor(_operId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        /// <summary>
        /// new add process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            NewProcess process = new NewProcess(_routingId);
            process.ShowDialog();
        }

        private void SketchRefProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            SketchRefProcess.CurrentSketchRefProcess = null;
        }
    }
}
