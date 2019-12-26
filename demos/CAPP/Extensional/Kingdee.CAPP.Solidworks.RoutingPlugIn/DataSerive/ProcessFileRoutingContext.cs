using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// ProcessFileRouting Data Context
    /// </summary>
    public class ProcessFileRoutingContext : DataContext
    {
        public ProcessFileRoutingContext(string connection) : base(connection) { }
        public Table<ProcessFileRouting> ProcessFileRoutings;
        public Table<CSketchFileProcess> SketchFileProcesses;
    }
}
