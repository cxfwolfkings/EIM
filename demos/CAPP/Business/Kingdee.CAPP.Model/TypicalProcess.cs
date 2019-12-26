using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：典型工艺实体类
    /// 作      者：jason.tang
    /// 完成时间：2013-06-20
    /// </summary>
    public class TypicalProcess
    {
        /// <summary>
        /// 典型工艺ID
        /// </summary>
        public Guid TypicalProcessId { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessId { get; set; }
        /// <summary>
        /// 典型工艺的名称
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
