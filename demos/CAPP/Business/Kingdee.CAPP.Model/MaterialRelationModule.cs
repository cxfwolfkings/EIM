using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：物料关系实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-06
    /// </summary>
    public class MaterialRelationModule
    {
        /// <summary>
        /// 父物料版本ID
        /// </summary>
        public string ParentVerId{ get; set; }
        /// <summary>
        /// 关系ID
        /// </summary>
        public string RelationId{ get; set; }
        /// <summary>
        /// 子物料版本ID
        /// </summary>
        public string ChildVerId{ get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplaySeq{ get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLock { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
    }
}
