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

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public partial class ProcessLine : Form
    {
        RoutingContext rcontext = null;
        RoutingProcessRelationContext rprContext = null;
        ProcessFileRoutingContext pfrContext = null;

        private ISldWorks swApp = null;
        private IModelDoc2 pDoc = null;
        static List<string> _routingIds = null;

        Dictionary<string, List<CSketchFileProcess>> dicSketchRefProcess = null;
        public ProcessLine(IModelDoc2 pDoc, ISldWorks swApp, List<string> routingIds)
        {
            _routingIds = routingIds;

            dicSketchRefProcess = new Dictionary<string, List<CSketchFileProcess>>();

            InitializeComponent();
            this.AutoSize = true;
            this.tvProcessLine.Height = Height;
            this.pDoc = pDoc;
            this.swApp = swApp;

            rcontext = new RoutingContext(DbConfig.Connection);
            rprContext = new RoutingProcessRelationContext(DbConfig.Connection);
            pfrContext = new ProcessFileRoutingContext(DbConfig.Connection);

            try
            {
                var slist = (from s in pfrContext.SketchFileProcesses
                             where _routingIds.Contains(s.RoutingId)
                             && s.ComponentName == GlobalCache.Instance.ComponetName
                             select s).ToList<CSketchFileProcess>();

                List<CSketchFileProcess> cSketchFileProcessList = null;
                slist.ForEach((x) =>
                {
                    if (dicSketchRefProcess.ContainsKey(x.RoutingId))
                    {
                        cSketchFileProcessList = dicSketchRefProcess[x.RoutingId];
                        cSketchFileProcessList.Add(x);
                        dicSketchRefProcess[x.RoutingId] = cSketchFileProcessList;
                    }
                    else
                    {
                        dicSketchRefProcess.Add(x.RoutingId, new List<CSketchFileProcess>() { x });
                    }

                });


                var qList = (from q in rcontext.Routings
                             where _routingIds.Contains(q.RoutingId)
                             select q).ToList<Routing>();

                foreach (var r in qList)
                {
                    TreeNode tn = new TreeNode(r.Name);
                    tn.Name = r.Code;
                    tn.Tag = r.RoutingId;
                    tn.ImageKey = "Routing";
                    tn.Expand();
                    AddChildTreeNode(r.RoutingId, tn);
                    tvProcessLine.Nodes.Add(tn);
                }
            }
            catch
            {
                MessageBox.Show("访问数据库失败");
            }

            ///默认第一个选中的Routing
            TreeNode[] tnodes = tvProcessLine.Nodes.Find(_routingIds.FirstOrDefault(), true);
            if (tnodes != null)
            {
                SelectedCurrentRouting(
                    tnodes.FirstOrDefault<TreeNode>(),
                    _routingIds.FirstOrDefault());

                ///初始状态没有
                GlobalCache.Instance.OperId = string.Empty;
            }

            ///初始化SketchDic
            //InitSketchDic(pDoc);

            EventHelper eh = EventHelper.GetInstance();
            eh.Testing += new TestEventHandler(SeletedProcessByComponentName);

            CurrentProcessLine = this;
        }

        //void InitSketchDic(IModelDoc2 pDoc)
        //{
        //    Dictionary<Feature, bool> dicSketch = new Dictionary<Feature, bool>();

        //    Feature feature = pDoc.FirstFeature();
        //    ModelDocExtension swModelExt = pDoc.Extension;
        //    try
        //    {

        //        while (feature != null)
        //        {
        //            string featureTypeName = feature.GetTypeName();
        //            //var res = modDoc.SelectByID(feature.Name, "BODYFEATURE", 0, 0, 0);

        //            string featureName = feature.Name;

        //            if (featureTypeName == "ProfileFeature"
        //                && !dicSketch.ContainsKey(feature))
        //            {
        //                dicSketch.Add(feature, true);
        //            }
        //            feature = (Feature)feature.GetNextFeature();

        //        }

        //        GlobalCache.Instance.SketchStatusDic = dicSketch;
        //    }
        //    catch
        //    {
        //        MessageBox.Show("操作SolidWorks 失败！");
        //    }
        //}
        /// <summary>       
        /// 在右边的工艺路线树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddProcessLine(object sender, RoutingEventArgs e)
        {
            TreeNode tn = new TreeNode(e.RoutingName);
            tn.Name = e.RoutingName;
            tn.Tag = e.RoutingId;
            tn.ImageKey = "Routing";
            tn.Expand();

            AddChildTreeNode(e.RoutingId, tn);
            tvProcessLine.Nodes.Add(tn);


            SelectedCurrentRouting(tn, e.RoutingId);
            GlobalCache.Instance.OperId = string.Empty;

        }

        /// <summary>
        /// 设置当前的工艺路线
        /// </summary>
        /// <param name="node"></param>
        /// <param name="routingId"></param>
        void SelectedCurrentRouting(TreeNode node, string routingId)
        {
            tvProcessLine.SelectedNode = node;
            foreach (TreeNode t in tvProcessLine.Nodes)
            {
                if (t.ImageKey == "Routing")
                {
                    if (t.Tag.ToString() != routingId)
                    {
                        t.BackColor = Color.White;
                    }
                    else
                    {
                        t.BackColor = Color.FromArgb(75, 169, 230);
                    }
                }
            }

            GlobalCache.Instance.RoutingId = routingId;
        }
        /// <summary>
        /// 新增工序到右边工序树下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CurrentProcess_AddProcess(object sender, ProcessEventArgs e)
        {
            TreeNode newTreeNode = new TreeNode();
            newTreeNode.Name = e.ProcessId;
            newTreeNode.Tag = e.ProcessId;
            newTreeNode.Text = e.ProcessName;

            TreeNode[] roots = tvProcessLine.Nodes.Find(e.RoutingId, true);
            if (roots.Length > 0)
            {
                TreeNode root = roots.FirstOrDefault();
                root.Nodes.Add(newTreeNode);
            }

            GlobalCache.Instance.OperId = e.ProcessId;
        }

        public static ProcessLine CurrentProcessLine
        {
            get;
            set;
        }

        void AddChildTreeNode(string routingId, TreeNode parentNode)
        {
            ///Join
            List<CSketchFileProcess> sketchProcesList = new List<CSketchFileProcess>();

            if (dicSketchRefProcess.ContainsKey(routingId))
            {
                sketchProcesList = dicSketchRefProcess[routingId];
            }
            try
            {
                var pList = (from p in rprContext.RoutingProcessRelation
                             from o in rprContext.Processes
                             from q in rprContext.ProcessFileRoutings
                             where 
                                p.RoutingId == routingId
                                && p.RoutingId == q.RoutingId
                                && p.OperId == o.OperId
                                && q.ProcessFileName == GlobalCache.Instance.ComponetName
                             select o).ToList<CProcess>();

                foreach (var c in pList)
                {
                    TreeNode childNode = new TreeNode(c.Name);
                    childNode.Name = c.OperId;
                    childNode.Tag = c.OperId;
                    childNode.ImageKey = "Process";
                    childNode.BackColor = Color.Red;

                    ///如果已经关联则背景为绿色;
                    ///否则背景为红色
                    sketchProcesList.ForEach((x) =>
                    {
                        if (x.OperId == c.OperId)
                        {
                            childNode.BackColor = Color.Lime;
                            return;
                        }
                    });
                    parentNode.Nodes.Add(childNode);
                }
            }
            catch
            {
                MessageBox.Show("访问数据库失败！");
            }
        }

        /// <summary>
        /// 工序关联上了草图后，改变该节点的背景色
        /// </summary>
        /// <param name="routingId">工艺路线Id</param>
        /// <param name="operId">工序Id</param>
        public void SetTreeNodeBackColor(string operId)
        {
            TreeNode[] tns = tvProcessLine.Nodes.Find(operId, true);
            if (tns == null || tns.Length == 0)
            {
                return;
            }

            TreeNode tn = tns.FirstOrDefault<TreeNode>();
            tn.BackColor = Color.Lime;
            tvProcessLine.SelectedNode = tn;
        }

        private Point p;
        private void tvProcessLine_MouseDown(object sender, MouseEventArgs e)
        {
            p = new Point(e.X, e.Y);
            TreeNode selectedNode = tvProcessLine.GetNodeAt(p);
            if (selectedNode == null) return;

            if (selectedNode.ImageKey.ToLower() == "routing")
            {
                ///设置当前的工艺路线
                SelectedCurrentRouting(selectedNode, selectedNode.Tag.ToString());
                GlobalCache.Instance.OperId = string.Empty;

            }
            else
            {
                ///如果选中的不工艺路线节点
                ///而是工序节点，则找到父节点的Tag
                SelectedCurrentRouting(selectedNode.Parent, selectedNode.Parent.Tag.ToString());

                selectedNode.SelectedImageKey = selectedNode.ImageKey;

                tvProcessLine.SelectedNode = selectedNode;

                #region

                /////Global cache
                //GlobalCache cahce = GlobalCache.Instance;
                //cahce.RoutingId = selectedNode.Tag.ToString();

                //if (selectedNode.SelectedImageKey == "Routing")
                //{
                //    selectedNode.ContextMenuStrip = contextMenuScript1;
                //}

                //SketchBlockDefinition swBlockDefinition;
                //SketchBlockInstance swBlockInstance;
                //object[] blocks = null;
                //int i = 0;
                //SelectionMgr swSelMgr;
                //swSelMgr = (SelectionMgr)pDoc.SelectionManager;


                //feature = (Feature)swSelMgr.GetSelectedObject6(1, -1);
                //if (feature == null)
                //{
                //    MessageBox.Show("Select a sketch block in the FeatureManager design tree, then rerun the macro.");
                //}
                //else
                //{
                //    swBlockDefinition = (SketchBlockDefinition)feature.GetSpecificFeature2();
                //    if ((swBlockDefinition != null))
                //    {
                //        blocks = (object[])swBlockDefinition.GetInstances();
                //        for (i = blocks.GetLowerBound(0); i <= blocks.GetUpperBound(0); i++)
                //        {

                //            swBlockInstance = (SketchBlockInstance)blocks[i];

                //            // Hide or show the sketch block instance 
                //            long status = 0;
                //            status = swBlockInstance.Visible;
                //            switch (status)
                //            {
                //                case (int)swAnnotationVisibilityState_e.swAnnotationHidden:
                //                    swBlockInstance.Visible = (int)swAnnotationVisibilityState_e.swAnnotationVisible;
                //                    break;
                //                case (int)swAnnotationVisibilityState_e.swAnnotationVisible:
                //                    swBlockInstance.Visible = (int)swAnnotationVisibilityState_e.swAnnotationHidden;
                //                    break;
                //                case (int)swAnnotationVisibilityState_e.swAnnotationHalfHidden:
                //                    MessageBox.Show("This block is half hidden.");
                //                    break;
                //                case (int)swAnnotationVisibilityState_e.swAnnotationVisibilityUnknown:
                //                    MessageBox.Show("Failed to determine visibility of this block.");
                //                    break;

                //            }
                //        }
                //    }

                //    blocks = null;
                //}
                #endregion

                string operId = selectedNode.Tag.ToString();
                GlobalCache.Instance.OperId = operId;

                List<string> refSketchName = new List<string>();
                try
                {
                    var cSketchProcesses = (from o in pfrContext.SketchFileProcesses
                                            where o.OperId == operId 
                                                && o.RoutingId == GlobalCache.Instance.RoutingId
                                                && o.ComponentName == GlobalCache.Instance.ComponetName
                                            select o).ToList<CSketchFileProcess>();

                    /// 工序还未关联草图时，可以显示右键菜单
                    if (cSketchProcesses.Count <= 0)
                    {
                        this.tvProcessLine.ContextMenuStrip = contextMenuScript1;
                        return;
                    }
                    else
                    {
                        this.tvProcessLine.ContextMenuStrip = null;
                    }
                    cSketchProcesses.ForEach((x) =>
                    {
                        refSketchName.Add(x.SketchName);

                    });
                }
                catch
                {
                    MessageBox.Show("访问数据库失败！");
                }



                try
                {
                    var res = false;
                    if (GlobalCache.Instance.LastSeletedSketchName == null)
                    {
                        #region MyRegion
                        //foreach (var kv in GlobalCache.Instance.SketchStatusDic)
                        //{
                        //    string featureName = kv.Key.Name;
                        //    res = pDoc.SelectByID(featureName, "BODYFEATURE", 0, 0, 0);
                        //    if (!refSketchName.Contains(featureName))
                        //    {
                        //        //feature.SetSuppression2(
                        //        //    (int)swFeatureSuppressionAction_e.swSuppressFeature,
                        //        //    (int)swInConfigurationOpts_e.swThisConfiguration,
                        //        //    "");
                        //        res = pDoc.EditSuppress();
                        //    }
                        //    else if (refSketchName.Contains(featureName))
                        //    {

                        //        res = pDoc.EditUnsuppress();
                        //    }
                        //    // feature = (Feature)feature.GetNextFeature();


                        //}
                        #endregion

                        Feature feature = pDoc.FirstFeature();
                        ModelDocExtension swModelExt = pDoc.Extension;
                        while (feature != null)
                        {
                            string featureTypeName = feature.GetTypeName();
                            string featureName = feature.Name;

                            res = pDoc.SelectByID(featureName, "BODYFEATURE", 0, 0, 0);

                            if (featureTypeName == "ProfileFeature"
                                && !refSketchName.Contains(featureName))
                            {
                                //feature.SetSuppression2(
                                //    (int)swFeatureSuppressionAction_e.swSuppressFeature,
                                //    (int)swInConfigurationOpts_e.swThisConfiguration,
                                //    "");
                                res = pDoc.EditSuppress();
                            }
                            else if (featureTypeName == "ProfileFeature"
                                && refSketchName.Contains(featureName))
                            {
                                res = pDoc.EditUnsuppress();
                            }

                            feature = (Feature)feature.GetNextFeature();
                        }
                    }
                    else
                    {
                        List<string> sketchNameList = GlobalCache.Instance.LastSeletedSketchName;

                        Feature feature = pDoc.FirstFeature();
                        ModelDocExtension swModelExt = pDoc.Extension;
                        while (feature != null)
                        {
                            string featureTypeName = feature.GetTypeName();
                            string featureName = feature.Name;

                            res = pDoc.SelectByID(featureName, "BODYFEATURE", 0, 0, 0);
                            if (featureTypeName == "ProfileFeature"
                                && sketchNameList.Contains(featureName))
                            {
                                //feature.SetSuppression2(
                                //    (int)swFeatureSuppressionAction_e.swSuppressFeature,
                                //    (int)swInConfigurationOpts_e.swThisConfiguration,
                                //    "");
                                res = pDoc.EditSuppress();
                            }
                            else if (featureTypeName == "ProfileFeature"
                                && refSketchName.Contains(featureName))
                            {
                                res = pDoc.EditUnsuppress();
                            }

                            feature = (Feature)feature.GetNextFeature();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("操作SolidWorks 失败！");
                }

                GlobalCache.Instance.LastSeletedSketchName = refSketchName;
                GC.Collect();
            }
        }

        private void cp_AddProcess(object sender, NodeEventArgs e)
        {
            TreeNode selectedNode = tvProcessLine.GetNodeAt(p);
            if (selectedNode == null) return;

            TreeNode tn = new TreeNode()
                {
                    Name = e.OperId,
                    Text = e.Name,
                    Tag = e.OperId
                };
            selectedNode.Nodes.Add(tn);
            tn.BackColor = Color.Green;
        }

        public void SeletedProcessByComponentName(SelectedEventArgs e)
        {
            string componentName = e.ComponentName.Trim();
            string sketchName = e.SketchName.Trim();
            ///gobal RoutingId

            List<CSketchFileProcess> cList = null;

            try
            {
                if (GlobalCache.Instance.RoutingId == null)
                {
                    cList = (from p in pfrContext.SketchFileProcesses
                             where p.SketchName == sketchName && p.ComponentName == componentName
                             select p).ToList<CSketchFileProcess>();
                }
                else
                {
                    cList = (from p in pfrContext.SketchFileProcesses
                             where p.RoutingId == GlobalCache.Instance.RoutingId
                                    && p.ComponentName == componentName
                                    && p.SketchName == sketchName
                             select p).ToList<CSketchFileProcess>();
                }
            }
            catch
            {
                MessageBox.Show("访问数据库失败！");
            }


            if (cList == null || cList.Count <= 0)
            {
                return;
            }

            /// Recursive routing and find treenode's tag equal globalcache routingid
            TreeNode parentNode = null;
            foreach (TreeNode n in tvProcessLine.Nodes)
            {
                if (GlobalCache.Instance.RoutingId != null
                    && n.Tag.ToString() == GlobalCache.Instance.RoutingId)
                {
                    parentNode = n;
                    n.Expand();
                    break;
                }
            }

            List<string> processIds = new List<string>();

            cList.ForEach((x) =>
            {
                processIds.Add(x.OperId);

            });
            ///defalut choice a treenode
            foreach (TreeNode cn in parentNode.Nodes)
            {
                if (processIds.Contains(cn.Tag.ToString())
                    && !string.IsNullOrWhiteSpace(cn.Tag.ToString()))
                {
                    tvProcessLine.SelectedNode = cn;
                    tvProcessLine.SelectedNode.BackColor = Color.Green;
                }

            }
        }

        /// <summary>
        /// 显示工序关联草图界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RefProcessToSketchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tvProcessLine.GetNodeAt(p);
            if (selectedNode == null) return;


            string routingId = GlobalCache.Instance.RoutingId;
            string operid = selectedNode.Tag.ToString();
            string componectName = GlobalCache.Instance.ComponetName;

            if (SketchRefProcess.CurrentSketchRefProcess != null)
            {
                SketchRefProcess sketchRefProcess = SketchRefProcess.CurrentSketchRefProcess;
                sketchRefProcess.ShowDialog();
            }
            else
            {
                SketchRefProcess sketchRefProcess = new SketchRefProcess(routingId, operid, componectName, pDoc);
                sketchRefProcess.ShowDialog();
            }

        }
    }

    public enum swUIStates
    {
        swIsHiddenInFeatureMgr = 1
    }

}
