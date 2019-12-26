using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：基础资源库字段
    /// 作      者：jason.tang
    /// 完成时间：2013-11-04
    /// </summary>
    public class BaseResourceField
    {
        /// <summary>
        /// 字段ID
        /// </summary>
        public string FieldId { get; set; }
        /// <summary>
        /// 字段代码
        /// </summary>
        public string FieldCode { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 父字段
        /// </summary>
        public string ParentField { get; set; }
        /// <summary>
        /// 字段描述
        /// </summary>
        public string FieldDescription { get; set; }
    }
}
