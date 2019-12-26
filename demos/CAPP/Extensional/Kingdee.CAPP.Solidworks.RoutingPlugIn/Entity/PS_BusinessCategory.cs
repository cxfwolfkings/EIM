using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "PS_BusinessCategory")]
    public class BusinessCategory
    {
        [Column(IsPrimaryKey=true)]
        public string CategoryId
        {
            get;
            set;
        }
        [Column]
        public string ParentCategory
        {
            get;
            set;
        }
        [Column]
        public string CategoryCode
        {
            get;
            set;
        }
        [Column]
        public string CategoryName
        {
            get;
            set;
        }
        [Column]
        public string Remark
        {
            get;
            set;
        }
        [Column]
        public string CodeRuleId
        {
            get;
            set;
        }
        [Column]
        public int DisplaySeq
        {
            get;
            set;
        }
        [Column]
        public int DeleteFlag
        {
            get;
            set;
        }
        [Column]
        public string MajorFormat
        {
            get;
            set;
        }
        [Column]
        public string MinorFormat
        {
            get;
            set;
        }
        [Column]
        public string Separate
        {
            get;
            set;
        }
        [Column]
        public int ObjectOption
        {
            get;
            set;
        }
        [Column]
        public string CommonType
        {
            get;
            set;
        }
        [Column]
        public bool IsShared
        {
            get;
            set;
        }
        [Column]
        public bool IsShareAll
        {
            get;
            set;
        }
        [Column]
        public bool IsUseForProduct
        {
            get;
            set;
        }
        [Column]
        public string Creator
        {
            get;
            set;
        }
        [Column]
        public string FolderId
        {
            get;
            set;
        }
        [Column]
        public decimal ArtificialCost
        {
            get;
            set;
        }
        [Column]
        public decimal ProcessCostRadix
        {
            get;
            set;
        }
        [Column]
        public decimal ProcessCost
        {
            get;
            set;
        }
        [Column]
        public decimal AssisCost
        {
            get;
            set;
        }
        [Column]
        public int ArtificialCostType
        {
            get;
            set;
        }
        [Column]
        public string DocFolderId
        {
            get;
            set;
        }
        [Column]
        public bool IsFlowByEA
        {
            get;
            set;
        }
        [Column]
        public bool IsFlowByPStart
        {
            get;
            set;
        }
        [Column]
        public bool IsFlowByPEnd
        {
            get;
            set;
        }
        [Column]
        public int IsShowPic
        {
            get;
            set;
        }
        [Column]
        public bool IsBOMChange
        {
            get;
            set;
        }
        [Column]
        public string ProjectPrefixMode
        {
            get;
            set;
        }
        [Column]
        public int IsShowOldVersion
        {
            get;
            set;
        }
        [Column]
        public bool FreezeCanFinish
        {
            get;
            set;
        }
        [Column]
        public string ECNCFFolderId
        {
            get;
            set;
        }
        [Column]
        public string ECNDocFolderId
        {
            get;
            set;
        }
        [Column]
        public string ProcessId
        {
            get;
            set;
        }
        [Column]
        public string ECNBandProcessId
        {
            get;
            set;
        }
        [Column]
        public int ChildCount
        {
            get;
            set;
        }
        [Column]
        public bool IsAddTaskName
        {
            get;
            set;
        }
        [Column]
        public string K3CategoryCode
        {
            get;
            set;
        }
        [Column]
        public string ImportByK3
        {
            get;
            set;
        }
        [Column]
        public int LinkageExtend
        {
            get;
            set;
        }
        [Column]
        public string ProjectChangeMan
        {
            get;
            set;
        }
        [Column]
        public string K3MaterialCode
        {
            get;
            set;
        }
        [Column]
        public int ShowExtend
        {
            get;
            set;
        }
        [Column]
        public string K3BOMGroup
        {
            get;
            set;
        }
    }
}
