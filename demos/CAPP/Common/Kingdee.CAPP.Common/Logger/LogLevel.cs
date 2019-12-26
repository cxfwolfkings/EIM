using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/

namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// log type
    /// priority: Error>Warning>Info>Debug
    /// </summary>
    enum LogLevel
    {
        Error =0,
        Warning = 1,
        Info = 2,
        Debug = 3
    }
}
