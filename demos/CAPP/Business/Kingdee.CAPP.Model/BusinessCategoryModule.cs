using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：物料分类实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public class BusinessCategoryModule
    {
        /// <summary>
        /// 物料分类ID
        /// </summary>
        public string CategoryId { get; set; }
        /// <summary>
        /// 物料分类代码
        /// </summary>
        public string CategoryCode { get; set; }
        /// <summary>
        /// 物料分类名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 显示排序
        /// </summary>
        public int DisplaySeq { get; set; }
    }
}
