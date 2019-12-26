using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: Process Planning
 *******************************/


namespace Kingdee.CAPP.Model
{
    public  class ProcessPlanning
    {
        /// <summary>
        /// 工艺规程Id
        /// </summary>
        public Guid ProcessPlanningId { get; set; }
        /// <summary>
        /// 工艺规程名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 显示在树上的顺序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public Int16 IsDelete { get; set; }
        /// <summary>
        /// 是否签出
        /// </summary>
        public bool IsCheckOut { get; set; }
        /// <summary>
        /// 所属文件夹
        /// </summary>
        public string FolderId { get; set; }
    }
}
