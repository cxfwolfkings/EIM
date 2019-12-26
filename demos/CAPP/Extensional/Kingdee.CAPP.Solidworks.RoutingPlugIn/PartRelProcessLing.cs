using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SolidWorks.Interop.sldworks;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class PartRelProcessLing : Form
    {
        RoutingContext rcontext = null;

        string _processFilePath = string.Empty;
        IModelDoc2 _doc = null;
        ISldWorks _swApp = null;
        public PartRelProcessLing(string processfilePath, IModelDoc2 doc, ISldWorks iSwApp)
        {
            InitializeComponent();

            rcontext = new RoutingContext(DbConfig.Connection);
            _processFilePath = processfilePath;
            _doc = doc;
            _swApp = iSwApp;
        }

        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        public static extern IntPtr GetDesktopWindow();

        protected override CreateParams CreateParams
        {
            get
            {
                return base.CreateParams;
                //var cp = base.CreateParams; // Retrieve the normal parameters.
                //cp.Style = 0x40000000 | 0x4000000; // WS_CHILD | WS_CLIPSIBLINGS
                //cp.ExStyle &= 0x00080000; // WS_EX_LAYERED
                //cp.Parent = GetDesktopWindow(); // Make "GetDesktopWindow()" from your own namespace.
                //return cp;
            }
        }

        private void PartRelProcessLing_Load(object sender, EventArgs e)
        {
            ///Get ProcessLine

            try
            {
                var qList = (from q in rcontext.Routings
                             select q).ToList<Routing>();

                cbxProcessLine.DataSource = qList;
                cbxProcessLine.DisplayMember = "Name";
                cbxProcessLine.ValueMember = "RoutingId";
            }
            catch
            {
                MessageBox.Show("数据库连接出错！");
            }

            
        }

        private void btnComfirm_Click(object sender, EventArgs e)
        {

            var routings = from p in rcontext.ProcessFileRoutinges
                           where p.ProcessFileName == GlobalCache.Instance.ComponetName
                           && p.RoutingId == cbxProcessLine.SelectedValue.ToString()
                           select p;
            if (routings.Count() > 0)
            {
                MessageBox.Show("该零件已和选中的工艺路线进行了关联！");
                return;
            }

            ProcessFileRouting processFileRouting = new ProcessFileRouting();
            processFileRouting.RoutingId = cbxProcessLine.SelectedValue.ToString();
            processFileRouting.ProcessFileRoutingId = Guid.NewGuid().ToString();
            processFileRouting.ProcessFileName = _processFilePath;
            processFileRouting.ProcessFilePath = _processFilePath;
            processFileRouting.OperId = "";

            

            try
            {
                rcontext.ProcessFileRoutinges.InsertOnSubmit(processFileRouting);
                rcontext.SubmitChanges();
                MessageBox.Show("关联零件到工艺路线成功！");

                ITaskpaneView pTaskPanView = null;
                if (GlobalCache.Instance.PTaskPanView == null)
                {
                    pTaskPanView = _swApp.CreateTaskpaneView2("", "关联工艺路线");

                    GlobalCache.Instance.PTaskPanView = pTaskPanView;
                }
                else
                {
                    pTaskPanView = GlobalCache.Instance.PTaskPanView;
                }

                ProcessLine frm = null;
                if (ProcessLine.CurrentProcessLine == null)
                {
                    frm = new ProcessLine(
                        _doc, 
                        _swApp, 
                        new List<string>() { processFileRouting.RoutingId });
                    pTaskPanView.DisplayWindowFromHandle(frm.Handle.ToInt32());

                }
                else
                {
                    ///show in task plane
                    ProcessLine.CurrentProcessLine.AddProcessLine(
                        null,
                        new RoutingEventArgs()
                        {
                            RoutingId = processFileRouting.RoutingId,
                            RoutingName = ((Routing)cbxProcessLine.SelectedItem).Name
                        });
                }
               
            }
            catch
            {
                MessageBox.Show("关联零件到工艺路线出错！");
            }



            this.Dispose();
            GC.Collect();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void btnNewProcessLine_Click(object sender, EventArgs e)
        {
            NewProcessLine processLine = new NewProcessLine(
                Guid.NewGuid().ToString(), _doc);
            processLine.ShowDialog();
        }

    }
}
