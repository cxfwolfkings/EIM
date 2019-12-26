using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：物料与卡片关联实体
    /// 作    者：jason.tang
    /// 完成时间：2013-03-26
    /// </summary>
    public class MaterialCardRelation
    { 
        /// <summary>
        /// 工业规程和卡片的关联Id
        /// </summary>
        public Guid MaterialCardRelationId { get; set; }
        /// <summary>
        /// 工艺规程Id
        /// </summary>
        public Guid MaterialId { get; set; }
        /// <summary>
        /// 工艺卡片的Id
        /// </summary>
        public Guid ProcessCardId { get; set; }
        /// <summary>
        /// 卡片的排序
        /// </summary>
        public int CardSort { get; set; }
        /// <summary>
        /// 关系类型
        /// </summary>
        public int Type { get; set; }
    }
}
