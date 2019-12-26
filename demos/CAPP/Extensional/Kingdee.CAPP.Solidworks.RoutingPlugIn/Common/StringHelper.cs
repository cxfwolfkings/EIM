using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Solidworks.RoutingPlugIn
{
    /// <summary>
    /// String helper
    /// </summary>
    public class StringHelper
    {
        public static bool IsValidEmpty(string validstr)
        {
            return string.IsNullOrWhiteSpace(validstr.Trim());
        }
    }
}
