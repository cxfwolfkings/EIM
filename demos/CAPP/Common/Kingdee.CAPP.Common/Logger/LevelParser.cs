using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log type string 
 *******************************/

namespace Kingdee.CAPP.Common.Logger
{
    class LevelParser
    {
        const string cError = "[Error]";
        const string cWarning = "[Warning]";
        const string cDebug = "[Debug]";
        const string cInfo = "[Info]";

        public static string GetLogTypeName(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Error:
                    return cError;
                case LogLevel.Warning:
                    return cWarning;
                case LogLevel.Info:
                    return cInfo;
                case LogLevel.Debug:
                    return cDebug;
                default:
                    return "*********inner error***********";
            }
        }
    }
}
