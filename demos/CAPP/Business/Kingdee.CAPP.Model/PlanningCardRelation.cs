using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: Planning And Card Relation
 *******************************/

namespace Kingdee.CAPP.Model
{
    public class PlanningCardRelation
    {
        /// <summary>
        /// 工业规程和卡片的关联Id
        /// </summary>
        public Guid PlanningCardRelationId { get; set; }  
        /// <summary>
        /// 工艺规程Id
        /// </summary>
        public Guid ProcessPlanningId { get; set; }
        /// <summary>
        /// 工艺卡片的Id
        /// </summary>
        public Guid  ProcessCardId { get; set; }
        /// <summary>
        /// 卡片的排序
        /// </summary>
        public int CardSort { get; set; }
    }
}
