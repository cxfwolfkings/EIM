using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "PP_RoutingOper")]
    public class RoutingProcessRelation
    {
        [Column(IsPrimaryKey = true)]
        public string RelationId
        {
            get;
            set;
        }
        [Column]
        public string OperId
        {
            get;
            set;
        }
        [Column]
        public string RoutingId
        {
            get;
            set;
        }
        [Column]
        public int Seq
        {
            get;
            set;
        }
        [Column]
        public string WorkcenterId
        {
            get;
            set;
        }
        [Column]
        public int Persons
        {
            get;
            set;
        }
        [Column]
        public decimal ProcessTime
        {
            get;
            set;
        }
        [Column]
        public int ProcessTimeUnit
        {
            get;
            set;
        }
        [Column]
        public string ProcessCosts
        {
            get;
            set;
        }
        [Column]
        public string LaborCosts
        {
            get;
            set;
        }
        [Column]
        public string OperCosts
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
    }
}
