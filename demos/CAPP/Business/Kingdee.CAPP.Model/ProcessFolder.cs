using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：工艺文件夹实体
    /// 作      者：jason.tang
    /// 完成时间：2013-08-23
    /// </summary>
    public class ProcessFolder
    {
        /// <summary>
        /// 文件夹ID
        /// </summary>
        public string FolderId { get; set; }
        /// <summary>
        /// 父文件夹
        /// </summary>
        public string ParentFolder { get; set; }
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 文件夹编码
        /// </summary>
        public string FolderCode { get; set; }
        /// <summary>
        /// 文件夹路径
        /// </summary>
        public string FolderPath { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string Updater { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplaySeq { get; set; }
        /// <summary>
        /// 子文件夹数目
        /// </summary>
        public int ChildCount { get; set; }
        /// <summary>
        /// 文件夹类型
        /// </summary>
        public int FolderType { get; set; }
    }
}
