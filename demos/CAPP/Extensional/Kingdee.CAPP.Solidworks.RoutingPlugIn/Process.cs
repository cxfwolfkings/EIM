using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Collections;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class Process : Form
    {
        ProcessContext pContext = null;
        RoutingProcessRelationContext rpContext = null;

        IModelDoc2 pDoc = null;
        ISldWorks swApp = null;
        string routingId = null;
        ProcessLine processLine = null;

        private SolidWorks.Interop.sldworks.IModelDoc2 pDoc_2;

        public event EventHandler<NodeEventArgs> AddProcess = null;
        public Process(
            string routingId, 
            IModelDoc2 pDoc, 
            ISldWorks swApp)
        {
            InitializeComponent();

            this.routingId = routingId;
            this.pDoc = pDoc;
            this.swApp = swApp;

            pContext = new ProcessContext(DbConfig.Connection);
            rpContext = new RoutingProcessRelationContext(DbConfig.Connection);

            ///初始化组件
            InitCombox();

        }

        /// <summary>
        /// Add componect to active doc
        /// </summary>
        void AddComponectToActiveDoc(string partFileName)
        {          
            ModelDocExtension swDocExt;
            AssemblyDoc swAssy;
            string tmpPath;
            ModelDoc2 tmpObj;
            bool boolstat;
            Component2 swcomponent;
            Feature matefeature;
            string MateName;
            string FirstSelection;
            string SecondSelection;
            swMateAlign_e Alignment;
            string strCompName;
            string AssemblyTitle;
            string AssemblyName;
            int errors = 0;
            int warnings = 0;
            int mateError;

            // Get title of assembly document
            AssemblyTitle = pDoc.GetTitle();

            // Split the title into two strings using the period (.) as the delimiter
            string[] strings = AssemblyTitle.Split(new Char[] { '.' });

            // Use AssemblyName when mating the component with the assembly
            AssemblyName = (string)strings[0];


            boolstat = true;

            string strCompModelname = null;

            strCompModelname = partFileName;

            // Because the component resides in the same folder as the assembly, get
            // the assembly's path, strip out the assembly filename, concatenate
            // the rest of the path to the component filename, and use this string to
            // open the component
            tmpPath = pDoc.GetPathName();
            //int idx;
            //idx = tmpPath.LastIndexOf(AssemblyTitle);
            string compPath;
            //tmpPath = tmpPath.Substring(0, (idx));
            //compPath = string.Concat(tmpPath, strCompModelname);
            compPath = strCompModelname;

            // Open the component
            tmpObj = (ModelDoc2)swApp.OpenDoc6(compPath, (int)swDocumentTypes_e.swDocPART, 0, "", ref errors, ref warnings);

            // Check to see if the file is read-only or cannot be found; display error
            // messages if either
            if (warnings == (int)swFileLoadWarning_e.swFileLoadWarning_ReadOnly)
            {
                MessageBox.Show("This file is read-only.");
                boolstat = false;
            }

            if (tmpObj == null)
            {
                MessageBox.Show("Cannot locate the file.");
                boolstat = false;
            }

            //Re-activate the assembly so that you can add the component to it
            pDoc = (ModelDoc2)swApp.ActivateDoc2(AssemblyTitle, true, ref errors);
            swAssy = (AssemblyDoc)pDoc;


            // Add the component to the assembly document.
            // Currently only one option, 
            // swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, 
            // works for adding a part using AddComponent5

            // The other options, 
            // swAddComponentConfigOptions_e.swAddComponentConfigOptions_NewConfigWithAllReferenceModels  
            // and swAddComponentConfigOptions_e.swAddComponentConfigOptions_NewConfigWithAsmStructure, 
            // work only for adding assemblies using AddComponent5
            swcomponent = (Component2)swAssy.AddComponent5(strCompModelname, (int)swAddComponentConfigOptions_e.swAddComponentConfigOptions_CurrentSelectedConfig, "", false, "", -1, -1, -1);

            // Get the name of the component for the mate
            strCompName = swcomponent.Name2;

            // Create the name of the mate and the names of the planes to use for the mate
            MateName = "top_coinc_" + strCompName;
            FirstSelection = "Top@" + strCompName + "@" + AssemblyName;
            SecondSelection = "Front@" + AssemblyName;
            swDocExt = (ModelDocExtension)pDoc.Extension;
            pDoc.ClearSelection2(true);

            // Select the planes for the mate
            boolstat = swDocExt.SelectByID2(FirstSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);
            boolstat = swDocExt.SelectByID2(SecondSelection, "PLANE", 0, 0, 0, true, 1, null, (int)swSelectOption_e.swSelectOptionDefault);

            // Add the mate
            matefeature = (Feature)swAssy.AddMate3((int)swMateType_e.swMateCOINCIDENT, (int)swMateAlign_e.swMateAlignALIGNED, false, 0, 0, 0, 0, 0, 0, 0,
            0, false, out mateError);
            matefeature.Name = MateName;

            pDoc.ViewZoomtofit2();
        }

        /// <summary>
        /// binding combox
        /// </summary>
        void InitCombox()
        {
            Component2 swRootComp;
            Configuration swConf;
            ConfigurationManager swConfMgr;

            swConfMgr = (ConfigurationManager)pDoc.ConfigurationManager;
            swConf = (Configuration)swConfMgr.ActiveConfiguration;
            swRootComp = (Component2)swConf.GetRootComponent3(true);

            //TraverseModelFeatures((ModelDoc2)pDoc, 1);

            if (pDoc.GetType() == (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                TraverseComponent(swRootComp, 1);
            }

            foreach (var h in htComponent)
            {
                alist.Add(h);
            }

            cbxComponentList.DataSource = alist;
            cbxComponentList.DisplayMember = "Key";
            cbxComponentList.ValueMember = "Value";
            cbxComponentList.SelectedIndex = 0;

            //Get process list
            var pList = (from rp in rpContext.RoutingProcessRelation
                        from p in rpContext.Processes
                        where rp.OperId == p.OperId && rp.RoutingId == routingId
                        select p).ToList<CProcess>();

            cbxProcessName.DataSource = pList;
            cbxProcessName.DisplayMember = "Name";
            cbxProcessName.ValueMember = "OperId";
            cbxProcessName.SelectedIndex = 0;

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        /// <summary>
        /// Correlation component to routing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCorrelationComponentToRouting_Click(
            object sender, 
            EventArgs e)
        {
            ///零件和工序文件关联表
            ProcessFileRouting pfr = new ProcessFileRouting();
            pfr.ProcessFileRoutingId = Guid.NewGuid().ToString();
            pfr.OperId = cbxProcessName.SelectedValue.ToString();
            pfr.RoutingId = routingId;
            pfr.ProcessFileName = cbxProcessName.Text.Trim();
            pfr.ProcessFilePath = cbxComponentList.SelectedValue.ToString();
            pfr.Sort = 0;

            try
            {
                ///add process file routing
                rpContext.ProcessFileRoutings.InsertOnSubmit(pfr);
                rpContext.SubmitChanges();

                MessageBox.Show(UIResource.NEWADDPROCESSSUCESS);

                AddComponectToActiveDoc(cbxComponentList.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(UIResource.NEWADDPROCESSFAILURE + ex.Message + "!");
            }
        }

        Hashtable htComponent = new Hashtable();
        ArrayList alist = new ArrayList();  
        public void TraverseComponent(Component2 swComp, long nLevel)
        {

            object[] vChildComp;

            Component2 swChildComp;

            string sPadStr = " ";

            long i = 0;

            for (i = 0; i <= nLevel - 1; i++)
            {
                sPadStr = sPadStr + " ";
            }



            vChildComp = (object[])swComp.GetChildren();

            for (i = 0; i < vChildComp.Length; i++)
            {

                swChildComp = (Component2)vChildComp[i];

                //TraverseComponentFeatures(swChildComp, nLevel);
                htComponent.Add(swChildComp.Name, swChildComp.GetPathName());

                TraverseComponent(swChildComp, nLevel + 1);

            }

        }

        public void TraverseModelFeatures(ModelDoc2 swModel, long nLevel)
        {

            Feature swFeat;

            swFeat = (Feature)swModel.FirstFeature();

            TraverseFeatures(swFeat, nLevel);

        }

        public void TraverseComponentFeatures(Component2 swComp, long nLevel)
        {

            Feature swFeat;

            swFeat = (Feature)swComp.FirstFeature();

            TraverseFeatures(swFeat, nLevel);

        }

        public void TraverseFeatures(Feature swFeat, long nLevel)
        {

            Feature swSubFeat;

            string sPadStr = " ";

            long i = 0;

            for (i = 0; i <= nLevel; i++)
            {

                sPadStr = sPadStr + " ";

            }

            while ((swFeat != null))
            {
                swSubFeat = (Feature)swFeat.GetFirstSubFeature();
                if ((swSubFeat != null))
                {
                    TraverseFeatures(swSubFeat, nLevel + 1);
                }

                if (nLevel == 1)
                {
                    swFeat = (Feature)swFeat.GetNextFeature();
                }

                else
                {
                    swFeat = (Feature)swFeat.GetNextSubFeature();
                }
            }
        }



        /// <summary>
        /// 增加零件到装配体中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddPartToAssembly_Click(object sender, EventArgs e)
        {
            if (GlobalCache.Instance.RoutingId == null)
            {
                MessageBox.Show(UIResource.NEWADDPROCESSPLEASECHOICEROUTING);
                return;
            }
            if (StringHelper.IsValidEmpty(tbxProcessCode.Text))
            {
                MessageBox.Show(UIResource.PROCESSCODECANOTNULL);
                return;
            }
            if (StringHelper.IsValidEmpty(tbxProcessName.Text))
            {
                MessageBox.Show(UIResource.PROCESSNAMECANOTNULL);
                return;
            }

            if (StringHelper.IsValidEmpty(tbxSelectedComponent.Text))
            {
                MessageBox.Show(UIResource.PLEASESELECTPARTFILE);
                return;
            }

            ///工艺文件
            CProcess cp = new CProcess();
            cp.OperId = Guid.NewGuid().ToString();
            cp.Name = tbxProcessName.Text;
            cp.Code = tbxProcessCode.Text;
            cp.CreateDate = cp.UpdateDate = DateTime.Now.ToString();

            cp.Creator = cp.UpdatePerson = "FFB0AC2D-C1B5-49E2-89B2-F4058523DF18";
            cp.Remark = string.Empty;

            
            ///工序和工艺路线关联表
            RoutingProcessRelation rp = new RoutingProcessRelation();
            rp.RelationId = Guid.NewGuid().ToString();
            rp.RoutingId = routingId;
            rp.OperId = cp.OperId;
            rp.Seq = 2;
            rp.WorkcenterId = string.Empty;
            rp.Persons = 1;
            rp.ProcessTime = 0;
            rp.ProcessTimeUnit = 1;
            rp.ProcessCosts = "0";
            rp.LaborCosts = "0";
            rp.OperCosts = "0";
            rp.Creator = rp.UpdatePerson = cp.Creator;
            rp.CreateDate = rp.UpdateDate = cp.CreateDate;

            ///零件和工序文件关联表
            ProcessFileRouting pfr = new ProcessFileRouting();
            pfr.ProcessFileRoutingId = Guid.NewGuid().ToString();
            pfr.RoutingId = routingId;
            pfr.ProcessFileName = tbxProcessName.Text.Trim();
            pfr.OperId = cp.OperId;
            pfr.ProcessFilePath = tbxSelectedComponent.Text.Trim();
            pfr.Sort = 0;

            try
            {
                //add component to feature tree
                AddComponectToActiveDoc(tbxSelectedComponent.Text.Trim());

                //add process
                rpContext.Processes.InsertOnSubmit(cp);
                //add process and routing relation
                rpContext.RoutingProcessRelation.InsertOnSubmit(rp);
                ///add process file routing
                rpContext.ProcessFileRoutings.InsertOnSubmit(pfr);

                rpContext.SubmitChanges();

                MessageBox.Show(UIResource.NEWADDPROCESSSUCESS);


                AddProcess(sender, new NodeEventArgs() 
                {
                    Name = tbxProcessName.Text.Trim(),
                    OperId = cp.OperId
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(UIResource.NEWADDPROCESSFAILURE + ex.Message + "!");
            }
        }
       
        /// <summary>
        /// 浏览零件文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "零件文件（*.sldprt）|*.sldprt";

            if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbxSelectedComponent.Text = op.FileName;
                return;
            }
            MessageBox.Show(UIResource.PLEASESELECTPARTFILE);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }

    /// <summary>
    /// Node eventarg
    /// </summary>
    public class NodeEventArgs : EventArgs
    {
        public string Name
        {
            get;
            set;
        }
        public string OperId
        {
            get;
            set;
        }
    }
}
