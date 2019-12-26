using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: Lazy load ProcessCardModule
 *******************************/

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 工艺卡片的模板
    /// </summary>
    public class ProcessCardModule
    {
        /// <summary>
        /// 卡片模板的ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 卡片模板的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 卡片模板的内容
        /// </summary>   
        public virtual CardsXML CardModule { get; set; }
        /// <summary>
        /// 固定提示框
        /// </summary>
        public string FixedMapValues { get; set; }
        /// <summary>
        /// 明细提示框
        /// </summary>
        public string DetailMapValues { get; set; }
        /// <summary>
        /// 标题提示框
        /// </summary>
        public string TitleMapValues { get; set; }
        /// <summary>
        /// 创建卡片模板的时间
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
        /// 默认为：
        /// 0 不删除
        /// 1 逻辑删除
        /// 2 实际删除
        /// </summary>
        public Int16 IsDelete { get; set; }
        /// <summary>
        /// 是否签出
        /// </summary>
        public bool IsCheckout { get; set; }

        /// <summary>
        /// 模板分类（文件夹）名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
