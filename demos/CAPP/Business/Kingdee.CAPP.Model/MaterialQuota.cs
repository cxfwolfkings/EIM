using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：材料定额实体
    /// 作      者：jason.tang
    /// 完成时间：2013-07-08
    /// </summary>
    public class MaterialQuota
    {
        /// <summary>
        /// 材料定额ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 材料特性
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// 比重
        /// </summary>
        public decimal Proportion { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public string WeightUnit { get; set; }
        /// <summary>
        /// 长度单位
        /// </summary>
        public string LengthUnit { get; set; }
        /// <summary>
        /// 有效位数
        /// </summary>
        public int ValidDigits { get; set; }
        /// <summary>
        /// 公式
        /// </summary>
        public string Formula { get; set; }
        /// <summary>
        /// 材料定额
        /// </summary>
        public decimal Quota { get; set; }
    }
}
