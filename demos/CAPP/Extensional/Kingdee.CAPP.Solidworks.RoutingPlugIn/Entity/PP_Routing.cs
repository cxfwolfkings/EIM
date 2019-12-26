using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "PP_Routing")]
    public class Routing
    {
        [Column(IsPrimaryKey = true)]
        public string RoutingId
        {
            get;
            set;
        }
        [Column]
        public string CategoryId
        {
            get;
            set;
        }
        [Column]
        public string Code
        {
            get;
            set;
        }
        [Column]
        public string Name
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
        public int CostingType
        {
            get;
            set;
        }
        [Column]
        public decimal Averageworking
        {
            get;
            set;
        }
        [Column]
        public int Status
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
        public string CreateDate
        {
            get;
            set;
        }
        [Column]
        public string UpdatePerson
        {
            get;
            set;
        }
        [Column]
        public string UpdateDate
        {
            get;
            set;
        }
        [Column]
        public string ObjectIconPath
        {
            get;
            set;
        }
        [Column]
        public string StateIconPath
        {
            get;
            set;
        }
        [Column]
        public int BatchFrom
        {
            get;
            set;
        }
        [Column]
        public int BatchTo
        {
            get;
            set;
        }
    }
}
