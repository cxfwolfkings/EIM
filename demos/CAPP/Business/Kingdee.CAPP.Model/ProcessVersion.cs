using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Model
{
    /// <summary>
    /// 类型说明：工艺文件版本实体类
    /// 作      者：jason.tang
    /// 完成时间：2013-08-23
    /// </summary>
    public class ProcessVersion
    {
        public string VerId { get; set; }
        public string BaseId { get; set; }
        public string VerCode { get; set; }
        public string VerName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public int MajorVer { get; set; }
        public int MinorVer { get; set; }
        public int State { get; set; }
        public string Creator { get; set; }
        public string CreateDate { get; set; }
        public string Updater { get; set; }
        public string UpdateDate { get; set; }
        public string CheckOutPerson { get; set; }
        public string CheckOutDate { get; set; }
        public string ArchiveDate { get; set; }
        public string ReleaseDate { get; set; }
        public string Remark { get; set; }
        public bool IsChange { get; set; }
        public bool IsEffective { get; set; }
        public bool IsInFlow { get; set; }
        public int IsShow { get; set; }
        public string FolderId { get; set; }
        public string ObjectStatePath { get; set; }
        public string ObjectIconPath { get; set; }
    }
}
