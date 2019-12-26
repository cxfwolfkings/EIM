using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: ProcessCard
 *******************************/

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 工艺卡片
    /// </summary>
    [Serializable]
    public class ProcessCard
    {
        /// <summary>
        /// 卡片的ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 卡片的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 卡片模板的ID
        /// </summary>
        public Guid CardModuleId  { get; set; }
        /// <summary>
        /// 卡片的内容
        /// </summary>
        public virtual CardsXML Card { get; set; }
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
        /// 0 不删除
        /// 1 逻辑删除
        /// 2 物理删除
        /// </summary>
        public Int16 IsDelete { get; set; }
        /// <summary>
        /// 是否签出
        /// </summary>
        public bool IsCheckOut { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int CardSort { get; set; }
    }
}
