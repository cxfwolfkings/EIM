using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Kingdee.CAPP.Common;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：产品实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    [TypeConverter(typeof(PropertySorter))]
    public class ProductModule
    {
        /// <summary>
        /// 文件夹ID
        /// </summary>
        [Browsable(false)]
        public string FolderId { get; set; }
        /// <summary>
        /// 文件夹代码
        /// </summary>
        [CategoryAttribute("编辑文件夹"), DescriptionAttribute("文件夹编号"), DisplayName("文件夹编号"), PropertyOrder(10)]
        public string FolderCode { get; set; }
        /// <summary>
        /// 文件夹名字
        /// </summary>
        [CategoryAttribute("编辑文件夹"), DescriptionAttribute("文件夹名称"), DisplayName("文件夹名称"), PropertyOrder(11)]
        public string FolderName { get; set; }
        /// <summary>
        /// 父文件夹
        /// </summary>
        [Browsable(false)]
        public string ParentFolder { get; set; }
        /// <summary>
        /// 父文件夹ID
        /// </summary>
        [Browsable(false)]
        public string ParentFolderId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Browsable(false)]
        public string Createtor { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Browsable(false)]
        public string CreateDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Browsable(false)]
        public string Updater { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Browsable(false)]
        public string UpdateDate { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        [Browsable(false)]
        public int DisplaySeq { get; set; }
        /// <summary>
        /// 子文件夹数目
        /// </summary>
        [Browsable(false)]
        public int ChildCount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [CategoryAttribute("编辑文件夹"), DescriptionAttribute("备注"), DisplayName("备注"), PropertyOrder(12)]
        public string Remark { get; set; }
    }
}
