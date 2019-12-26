using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：PBOM实体类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-06
    /// </summary>
    public class PBomModule
    {
        /// <summary>
        /// PBOM ID
        /// </summary>
        public string PbomId { get; set; }
        /// <summary>
        /// 文件夹ID
        /// </summary>
        public string FolderId { get; set; }
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string FolderName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 存档人
        /// </summary>
        public string ArchivePerson { get; set; }
        /// <summary>
        /// 存档时间
        /// </summary>
        public string ArchiveDate { get; set; }
        /// <summary>
        /// VerId
        /// </summary>
        public string VerId { get; set; }
    }
}
