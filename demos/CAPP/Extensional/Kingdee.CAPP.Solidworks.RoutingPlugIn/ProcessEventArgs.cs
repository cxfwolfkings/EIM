using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    public class ProcessEventArgs: EventArgs
    {
        /// <summary>
        /// 工序的名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 工序的ID 
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 工艺路线的ID
        /// </summary>
        public string RoutingId { get; set; }
    }

    /// <summary>
    /// 工艺路线
    /// </summary>
    public class RoutingEventArgs : EventArgs
    {
        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string RoutingName { get; set; }
        /// <summary>
        /// 工艺路线Id
        /// </summary>
        public string RoutingId { get; set; }
    }
}
