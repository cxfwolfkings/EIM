using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// Sketch process relation content
    /// </summary>
    public class SketchProcessContent: DataContext
    {
        public SketchProcessContent(string connection) : base(connection) { }
        public Table<CSketchFileProcess> SketchFileProcesses;
    }
}
