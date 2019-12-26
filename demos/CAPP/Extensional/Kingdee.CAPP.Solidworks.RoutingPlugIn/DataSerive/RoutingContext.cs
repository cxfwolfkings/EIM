using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// Routing object Context
    /// </summary>
    public class RoutingContext : DataContext
    {
        public RoutingContext(string connection) : base(connection) { }
        public Table<Routing> Routings;
        public Table<ProcessFileRouting> ProcessFileRoutinges;
    }
}
