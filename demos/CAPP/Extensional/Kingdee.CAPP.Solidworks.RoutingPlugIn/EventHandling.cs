using System;
using System.Collections;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Windows.Forms;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;




namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{

    public class DocumentEventHandler
    {
        protected ISldWorks iSwApp;
        protected ModelDoc2 document;
        protected SwAddin userAddin;

        protected Hashtable openModelViews;

        public DocumentEventHandler(ModelDoc2 modDoc, SwAddin addin)
        {
            document = modDoc;
            userAddin = addin;
            iSwApp = (ISldWorks)userAddin.SwApp;
            openModelViews = new Hashtable();
        }

        virtual public bool AttachEventHandlers()
        {
            return true;
        }

        virtual public bool DetachEventHandlers()
        {
            return true;
        }

        public bool ConnectModelViews()
        {
            IModelView mView;
            mView = (IModelView)document.GetFirstModelView();

            while (mView != null)
            {
                if (!openModelViews.Contains(mView))
                {
                    DocView dView = new DocView(userAddin, mView, this);

                    dView.AttachEventHandlers();
                    openModelViews.Add(mView, dView);
                }
                mView = (IModelView)mView.GetNext();
            }
            return true;
        }

        public bool DisconnectModelViews()
        {
            //Close events on all currently open docs
            DocView dView;
            int numKeys;
            numKeys = openModelViews.Count;

            if (numKeys == 0)
            {
                return false;
            }


            object[] keys = new object[numKeys];

            //Remove all ModelView event handlers
            openModelViews.Keys.CopyTo(keys, 0);
            foreach (ModelView key in keys)
            {
                dView = (DocView)openModelViews[key];
                dView.DetachEventHandlers();
                openModelViews.Remove(key);
                dView = null;
            }
            return true;
        }

        public bool DetachModelViewEventHandler(ModelView mView)
        {
            DocView dView;
            if (openModelViews.Contains(mView))
            {
                dView = (DocView)openModelViews[mView];
                openModelViews.Remove(mView);
                mView = null;
                dView = null;
            }
            return true;
        }
    }

    public class PartEventHandler : DocumentEventHandler
    {
        PartDoc doc;

        bool isRelation = false;
        string partName = string.Empty;
        List<string> routingIdList = new List<string>();
        ProcessFileRoutingContext pfr = null;

        public PartEventHandler(ModelDoc2 modDoc, SwAddin addin)
            : base(modDoc, addin)
        {
            doc = (PartDoc)document;

            
            string partName = modDoc.GetPathName().Substring(
                modDoc.GetPathName().LastIndexOf('\\') + 1);

            GlobalCache.Instance.ComponetName = partName;

            this.partName = partName;

            pfr = new ProcessFileRoutingContext(DbConfig.Connection);


            List<ProcessFileRouting> qlist = null;
            
            try
            {
                qlist = (from p in pfr.ProcessFileRoutings
                         where p.ProcessFileName == partName
                         select p).ToList<ProcessFileRouting>();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            Dictionary<string, bool> featureDic = new Dictionary<string, bool>();
            Feature feature = modDoc.FirstFeature();
            ModelDocExtension swModelExt = modDoc.Extension;
            while (feature != null)
            {
                string featureTypeName = feature.GetTypeName();
                string featureName = feature.Name;

                if (featureTypeName == "ProfileFeature")
                {
                    if (!featureDic.ContainsKey(featureName))
                    {
                        featureDic.Add(featureName,false);
                    }
                }

                feature = (Feature)feature.GetNextFeature();
            }
            GlobalCache.Instance.SketchStatusDic = featureDic;   


            List<string> routingIdList = new List<string>();
            foreach (var q in qlist)
            {
                routingIdList.Add(q.RoutingId);
            }

            this.routingIdList = routingIdList;
            

            if (routingIdList.Count > 0)
            {
                ///Exist the correlation component to  routing 
                Routing(modDoc, iSwApp, routingIdList);
            }
        }


        
        ProcessLine frm;
        /// <summary>
        /// Add form to taskpanel
        /// </summary>
        public void Routing(IModelDoc2 doc,ISldWorks swApp, List<string> routingIds)
        {
            ITaskpaneView pTaskPanView = null;
            if (GlobalCache.Instance.PTaskPanView == null)
            {
                pTaskPanView = iSwApp.CreateTaskpaneView2("", "关联工艺路线");
                GlobalCache.Instance.PTaskPanView = pTaskPanView;
            }
            else
            {
                pTaskPanView = GlobalCache.Instance.PTaskPanView;
            }
            
            

            frm = new ProcessLine(doc, swApp, routingIds);
            pTaskPanView.DisplayWindowFromHandle(frm.Handle.ToInt32());


        }
        override public bool AttachEventHandlers()
        {
            doc.DestroyNotify += new DPartDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify += new DPartDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);

            ConnectModelViews();

            return true;
        }

        override public bool DetachEventHandlers()
        {
            doc.DestroyNotify -= new DPartDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify -= new DPartDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);

            DisconnectModelViews();

            userAddin.DetachModelEventHandler(document);
            return true;
        }

        //Event Handlers
        public int OnDestroy()
        {
            GlobalCache.Instance.OperId = string.Empty;
            ProcessLine.CurrentProcessLine = null;
            if (frm != null)
            {
                frm.Dispose();
                frm = null;
            }

            DetachEventHandlers();
            return 0;
        }

        [HandleProcessCorruptedStateExceptions]
        public int OnNewSelection()
        {
            //ModelDoc2 swDoc = null;
            //Feature swFeature;

            //swDoc = iSwApp.ActiveDoc;

            //SelectionMgr swSelMgr;
            //swSelMgr = (SelectionMgr)swDoc.SelectionManager;


            //Feature feature = swDoc.FirstFeature();
            //Feature nextFeature = null;

            //bool reValue = false;

            //bool isSuppressSet = false;
            ////long suppressState = 0;
            //string featType = string.Empty;
            


            //Feature swFeat = default(Feature);
            //ModelDocExtension swModDocExt = default(ModelDocExtension);
            //Component2 swComp = default(Component2);
            //object vModelPathName = null;

            //int selObjType = 0;
            //int nRefCount = 0;
            //selObjType = swSelMgr.GetSelectedObjectType3(1, -1); ;

            //if (selObjType == (int)swSelectType_e.swSelBODYFEATURES)
            //{
            //    swFeature = swSelMgr.GetSelectedObject6(1, -1);
            //    if (swFeature == null) return 0;
            //    //MessageBox.Show(swFeature.Name);
            //}
            //else if (selObjType == (int)swSelectType_e.swSelCOMPONENTS)
            //{
            //    swComp = (Component2)swSelMgr.GetSelectedObject6(1, -1);

            //    if (swComp == null) return 0;
            //    //MessageBox.Show(swComp.GetPathName());

            //    swFeat = swComp.FirstFeature();


            //    EventHelper eh = EventHelper.GetInstance();
            //    SelectedEventArgs arg = new SelectedEventArgs();
            //    arg.ComponentName = swComp.Name;
            //    eh.RasieTesting(arg);
            //}
            //else if (selObjType == (int)swSelectType_e.swSelSKETCHES)
            //{

            //    swFeat = (Feature)swSelMgr.GetSelectedObject6(1, -1);
            //    if (swFeat == null) return 0;
            //    //if (!isRelation)
            //    //{
            //    //    ///First relate component with  process routing
            //    //    ///Then relate sketch with process

            //    //    DialogResult result = MessageBox.Show("该零件还没有和工艺路线进行关联，是否现在关联？",
            //    //                "关联工艺路线和零件",
            //    //                MessageBoxButtons.YesNoCancel,
            //    //                MessageBoxIcon.Exclamation);


            //    //    if (result == DialogResult.Yes)
            //    //    {
            //    //        /// relate component with process routing

            //    //        PartRelProcessLing newPartRelProcessLing = new PartRelProcessLing(partName, swDoc);
            //    //        newPartRelProcessLing.Show();
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    /// relate sketch with process
            //    //    var qlist = from p in pfr.SketchFileProcesses
            //    //                where routingIdList.Contains(p.RoutingId) && p.SketchName == swFeat.Name
            //    //                select p;

                    
                    

            //    //    if(qlist.Count() <= 0)
            //    //    {
            //    //        //如果没有找到就提示进行关联
            //    //        DialogResult result = MessageBox.Show("该草图还没有和工序进行关联，是否现在关联？",
            //    //                "关联工序和草图",
            //    //                MessageBoxButtons.YesNoCancel,
            //    //                MessageBoxIcon.Exclamation);

            //    //        if (result == DialogResult.Yes)
            //    //        {
            //    //            /// relate component with process routing

            //    //            SketchRefProcess sketchRefProcess = new SketchRefProcess(
            //    //                                                            routingIdList.FirstOrDefault().ToString(),
            //    //                                                            swFeat.Name,
            //    //                                                            partName);
            //    //            sketchRefProcess.ShowDialog();
            //    //        }
            //    //    }
            //    //    else
            //    //    {
            //    //        /// 如果找到就显示
            //    //        /// 事件传递参数
            //    //        SelectedEventArgs e = new SelectedEventArgs();
            //    //        e.SketchName = swFeat.Name;
            //    //        e.ComponentName = partName;
            //    //        ProcessLine.CurrentProcessLine.SeletedProcessByComponentName(e);
            //    //    }

            //    //}
                
            //    nRefCount = swFeat.ListExternalFileReferencesCount();
            //}
            return 0;
        }
    }

    public class AssemblyEventHandler : DocumentEventHandler
    {
        AssemblyDoc doc;
        SwAddin swAddin;

        public AssemblyEventHandler(ModelDoc2 modDoc, SwAddin addin)
            : base(modDoc, addin)
        {
            doc = (AssemblyDoc)document;
            swAddin = addin;
        }

        override public bool AttachEventHandlers()
        {
            doc.DestroyNotify += new DAssemblyDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify += new DAssemblyDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);
            doc.ComponentStateChangeNotify2 += new DAssemblyDocEvents_ComponentStateChangeNotify2EventHandler(ComponentStateChangeNotify2);
            doc.ComponentStateChangeNotify += new DAssemblyDocEvents_ComponentStateChangeNotifyEventHandler(ComponentStateChangeNotify);
            doc.ComponentVisualPropertiesChangeNotify += new DAssemblyDocEvents_ComponentVisualPropertiesChangeNotifyEventHandler(ComponentVisualPropertiesChangeNotify);
            doc.ComponentDisplayStateChangeNotify += new DAssemblyDocEvents_ComponentDisplayStateChangeNotifyEventHandler(ComponentDisplayStateChangeNotify);
            ConnectModelViews();

            return true;
        }

        override public bool DetachEventHandlers()
        {
            doc.DestroyNotify -= new DAssemblyDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify -= new DAssemblyDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);
            doc.ComponentStateChangeNotify2 -= new DAssemblyDocEvents_ComponentStateChangeNotify2EventHandler(ComponentStateChangeNotify2);
            doc.ComponentStateChangeNotify -= new DAssemblyDocEvents_ComponentStateChangeNotifyEventHandler(ComponentStateChangeNotify);
            doc.ComponentVisualPropertiesChangeNotify -= new DAssemblyDocEvents_ComponentVisualPropertiesChangeNotifyEventHandler(ComponentVisualPropertiesChangeNotify);
            doc.ComponentDisplayStateChangeNotify -= new DAssemblyDocEvents_ComponentDisplayStateChangeNotifyEventHandler(ComponentDisplayStateChangeNotify);
            DisconnectModelViews();

            userAddin.DetachModelEventHandler(document);
            return true;
        }

        //Event Handlers
        public int OnDestroy()
        {
            DetachEventHandlers();
            return 0;
        }

        public int OnNewSelection()
        {
            //var swSelMgr = document.SelectionManager;
            //var f = swSelMgr.GetSelectedObject6(1, 0);           

            ModelDoc2 swDoc = null;
            Feature swFeature;

            swDoc = iSwApp.ActiveDoc;

            SelectionMgr swSelMgr;
            swSelMgr = (SelectionMgr)swDoc.SelectionManager;

            Feature swFeat = default(Feature);
            ModelDocExtension swModDocExt = default(ModelDocExtension);
            Component2 swComp = default(Component2);
            object vModelPathName = null;

            int selObjType = 0;
            int nRefCount = 0;
            selObjType = swSelMgr.GetSelectedObjectType3(1, -1); ;

            if (selObjType == (int)swSelectType_e.swSelBODYFEATURES)
            {
                swFeature = swSelMgr.GetSelectedObject6(1, 1);
            }
            else if (selObjType == (int)swSelectType_e.swSelCOMPONENTS)
            {
                swComp = (Component2)swSelMgr.GetSelectedObjectsComponent3(1, -1);

                if (swComp == null) return 0;
                //MessageBox.Show(swComp.GetPathName());

                swFeat = swComp.FirstFeature();

                EventHelper eh = EventHelper.GetInstance();
                SelectedEventArgs arg = new SelectedEventArgs();
                arg.ComponentName = swComp.GetPathName();
                eh.RasieTesting(arg);

                //display feature's sensor
                //MessageBox.Show(swFeat.Name);
            }
            else if (selObjType == (int)swSelectType_e.swSelSKETCHES)
            {
                swFeat = (Feature)swSelMgr.GetSelectedObject6(1, -1);
                nRefCount = swFeat.ListExternalFileReferencesCount();
            }
            //Component2 swComp = null;
            //swComp = (Component2)swDwgComp.Component;

            //ModelDoc2 swCompModDoc = null;
            //swCompModDoc = (ModelDoc2)swComp.GetModelDoc2();


            //Debug.Print("dsfasdf");

            //var swSelMgr = modelDoc2.SelectionManager;
            //var swCompEnt = swSelMgr.GetSelectedObject6(1, 0);

            #region 递归输出Feature.Name
            //StringBuilder sb = new StringBuilder();

            //Feature feature = modelDoc2.FirstFeature();
            //Feature nextFeature = null;


            //sb.Append("名称：" + feature.Name + "----类型：" + feature.GetTypeName());
            //while (feature != null)
            //{
            //    nextFeature = (Feature)feature.GetNextFeature();
            //    if (nextFeature == null) { break; }


            //    sb.Append("名称：" + nextFeature.Name + "----类型：" + feature.GetTypeName());
            //    feature = null;
            //    feature = nextFeature;
            //    nextFeature = null;
            //} 
            #endregion


            #region 类型转换出错

            //ModelDocExtension swModelDocExt = default(ModelDocExtension);
            //SelectionMgr swSelMgr = default(SelectionMgr);

            //Feature swFeat = default(Feature);

            //string featName = null;

            //string featType = null;


            //swSelMgr = (SelectionMgr)document.SelectionManager;
            //swModelDocExt = (ModelDocExtension)document.Extension;


            //// Get the selected feature
            //swFeat = (Feature)swSelMgr.GetSelectedObject6(1, -1);
            //document.ClearSelection2(true);



            //// Get the feature's type and name

            //featName = swFeat.GetNameForSelection(out featType);

            //swModelDocExt.SelectByID2(featName, featType, 0, 0, 0, true, 0, null, 0);

            //MessageBox.Show(featName + "" + featType); 
            #endregion

            return 0;
        }

        //attach events to a component if it becomes resolved
        protected int ComponentStateChange(object componentModel, short newCompState)
        {
            ModelDoc2 modDoc = (ModelDoc2)componentModel;
            swComponentSuppressionState_e newState = (swComponentSuppressionState_e)newCompState;


            switch (newState)
            {

                case swComponentSuppressionState_e.swComponentFullyResolved:
                    {
                        if ((modDoc != null) & !this.swAddin.OpenDocs.Contains(modDoc))
                        {
                            this.swAddin.AttachModelDocEventHandler(modDoc);
                        }
                        break;
                    }

                case swComponentSuppressionState_e.swComponentResolved:
                    {
                        if ((modDoc != null) & !this.swAddin.OpenDocs.Contains(modDoc))
                        {
                            this.swAddin.AttachModelDocEventHandler(modDoc);
                        }
                        break;
                    }

            }
            return 0;
        }

        protected int ComponentStateChange(object componentModel)
        {
            ComponentStateChange(componentModel, (short)swComponentSuppressionState_e.swComponentResolved);
            return 0;
        }


        public int ComponentStateChangeNotify2(object componentModel, string CompName, short oldCompState, short newCompState)
        {
            return ComponentStateChange(componentModel, newCompState);
        }

        int ComponentStateChangeNotify(object componentModel, short oldCompState, short newCompState)
        {
            return ComponentStateChange(componentModel, newCompState);
        }

        int ComponentDisplayStateChangeNotify(object swObject)
        {
            Component2 component = (Component2)swObject;
            ModelDoc2 modDoc = (ModelDoc2)component.GetModelDoc();

            //StringBuilder sb = new StringBuilder();
            //sb.Append(modDoc.SceneName + "----" + modDoc.SceneUserName);

            return ComponentStateChange(modDoc);
        }

        int ComponentVisualPropertiesChangeNotify(object swObject)
        {
            Component2 component = (Component2)swObject;
            ModelDoc2 modDoc = (ModelDoc2)component.GetModelDoc();

            return ComponentStateChange(modDoc);
        }




    }

    public class DrawingEventHandler : DocumentEventHandler
    {
        DrawingDoc doc;

        public DrawingEventHandler(ModelDoc2 modDoc, SwAddin addin)
            : base(modDoc, addin)
        {
            doc = (DrawingDoc)document;
        }

        override public bool AttachEventHandlers()
        {
            doc.DestroyNotify += new DDrawingDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify += new DDrawingDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);

            ConnectModelViews();

            return true;
        }

        override public bool DetachEventHandlers()
        {
            doc.DestroyNotify -= new DDrawingDocEvents_DestroyNotifyEventHandler(OnDestroy);
            doc.NewSelectionNotify -= new DDrawingDocEvents_NewSelectionNotifyEventHandler(OnNewSelection);

            DisconnectModelViews();

            userAddin.DetachModelEventHandler(document);
            return true;
        }

        //Event Handlers
        public int OnDestroy()
        {
            DetachEventHandlers();
            return 0;
        }

        public int OnNewSelection()
        {
            return 0;
        }
    }

    public class DocView
    {
        ISldWorks iSwApp;
        SwAddin userAddin;
        ModelView mView;
        DocumentEventHandler parent;

        public DocView(SwAddin addin, IModelView mv, DocumentEventHandler doc)
        {
            userAddin = addin;
            mView = (ModelView)mv;
            iSwApp = (ISldWorks)userAddin.SwApp;
            parent = doc;
        }

        public bool AttachEventHandlers()
        {
            mView.DestroyNotify2 += new DModelViewEvents_DestroyNotify2EventHandler(OnDestroy);
            mView.RepaintNotify += new DModelViewEvents_RepaintNotifyEventHandler(OnRepaint);
            return true;
        }

        public bool DetachEventHandlers()
        {
            mView.DestroyNotify2 -= new DModelViewEvents_DestroyNotify2EventHandler(OnDestroy);
            mView.RepaintNotify -= new DModelViewEvents_RepaintNotifyEventHandler(OnRepaint);
            parent.DetachModelViewEventHandler(mView);
            return true;
        }

        //EventHandlers
        public int OnDestroy(int destroyType)
        {
            switch (destroyType)
            {
                case (int)swDestroyNotifyType_e.swDestroyNotifyHidden:
                    return 0;

                case (int)swDestroyNotifyType_e.swDestroyNotifyDestroy:
                    return 0;
            }

            return 0;
        }

        public int OnRepaint(int repaintType)
        {
            //单击每个目录触发的
            //MessageBox.Show("123123");

            return 0;
        }
    }

}
