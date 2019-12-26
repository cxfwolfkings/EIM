using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    [Table(Name = "SketchFileProcess")]
    public class CSketchFileProcess
    {
        [Column(IsPrimaryKey = true)]
        public string SketchProcessId
        {
            get;
            set;
        }
        [Column]
        public string SketchName
        {
            get;
            set;
        }
        [Column]
        public string ComponentName
        {
            get;
            set;
        }

        [Column]
        public string ComponentPath
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
    }
}
