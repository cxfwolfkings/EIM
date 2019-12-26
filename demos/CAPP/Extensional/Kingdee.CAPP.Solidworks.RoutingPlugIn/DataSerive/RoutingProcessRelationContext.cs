using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// RoutingProcessRelation object Context
    /// </summary>
    public class RoutingProcessRelationContext : DataContext
    {
        public RoutingProcessRelationContext(string connection) : base(connection) { }
        public Table<RoutingProcessRelation> RoutingProcessRelation;
        public Table<CProcess> Processes;
        public Table<ProcessFileRouting> ProcessFileRoutings;
    }
}
