using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：物料版本实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-06
    /// </summary>
    public class MaterialVersionModule
    {
        /// <summary>
        /// 物料版本ID
        /// </summary>
        public string MaterialVerId { get; set; }
        /// <summary>
        /// BaseId
        /// </summary>
        public string BaseId { get; set; }
        /// <summary>
        /// 版本代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 版本名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public int MaterialType { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 对象图片路径
        /// </summary>
        public string ObjectIconPath { get; set; }
        /// <summary>
        /// pp_pbomVer VerId
        /// </summary>
        public string VerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ChildId { get; set; }
    }
}
