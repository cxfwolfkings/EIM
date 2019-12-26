using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: ProcessPlanningModule
 *******************************/

namespace Kingdee.CAPP.Model
{
    public class ProcessPlanningModule
    {
        /// <summary>
        /// 工艺规程模板ID
        /// </summary>
        public Guid ProcessPlanningModuleId { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessId { get; set; }
        /// <summary>
        /// 模板的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public BusinessType BType { get; set; }
        /// <summary>
        /// 父节点的级别
        /// </summary>
        public int ParentNode { get; set; }
        /// <summary>
        /// 当前节点的级别
        /// </summary>
        public int CurrentNode { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
    }
}
