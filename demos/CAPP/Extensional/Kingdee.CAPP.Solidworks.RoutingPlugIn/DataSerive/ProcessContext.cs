using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// Process object Context
    /// </summary>
    public class ProcessContext: DataContext
    {
        public ProcessContext(string connection) : base(connection) { }
        public Table<CProcess> Processes;
    }
}
