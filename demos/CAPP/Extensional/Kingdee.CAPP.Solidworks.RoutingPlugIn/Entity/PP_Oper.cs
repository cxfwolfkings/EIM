using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "PP_Oper")]
    public class CProcess
    {
        [Column(IsPrimaryKey = true)]
        public string OperId
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
        public string Remark
        {
            get;
            set;
        }
    }
}
