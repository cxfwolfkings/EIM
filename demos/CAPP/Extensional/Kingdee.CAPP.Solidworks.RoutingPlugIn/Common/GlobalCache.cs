using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolidWorks.Interop.sldworks;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public class GlobalCache
    {
        private static readonly GlobalCache m_instance = new GlobalCache();
        private GlobalCache() { }
        public static GlobalCache Instance
        {
            get { return m_instance; }
        }

        /// <summary>
        /// routing's id
        /// </summary>
        public string RoutingId
        {
            get;
            set;
        }
        /// <summary>
        /// OperId
        /// </summary>
        public string OperId
        {
            get;
            set;
        }
        /// <summary>
        /// Component Name
        /// </summary>
        public string ComponetName
        {
            get;
            set;
        }
        /// <summary>
        /// Sketch Name
        /// </summary>
        public string SketchName
        {
            get;
            set;
        }
        /// <summary>
        /// 草图状态字典
        /// </summary>
        public Dictionary<string, bool> SketchStatusDic
        {
            get;
            set;
        }
        /// <summary>
        /// 只能有一个任务面板
        /// </summary>
        public ITaskpaneView PTaskPanView
        {
            get;
            set;
        }
        ///// <summary>
        ///// 是否初始化压缩所有的草图
        ///// </summary>
        //public bool IsInitSupressAllSketch
        //{
        //    get;
        //    set;
        //}
        /// <summary>
        /// 上一次选中草图的列表
        /// </summary>
        public List<string> LastSeletedSketchName 
        { 
            get; 
            set; 
        }
    }
}
