using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "ProcessFileRouting")]
    public class ProcessFileRouting
    {
        [Column(IsPrimaryKey=true)]
        public string ProcessFileRoutingId
        {
            get;
            set;
        }
        [Column]
        public string ProcessFileName
        {
            get;
            set;
        }
        [Column]
        public string ProcessFilePath
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
        public string OperId
        {
            get;
            set;
        }
        [Column]
        public int Sort
        {
            get;
            set;
        }
    }
}
