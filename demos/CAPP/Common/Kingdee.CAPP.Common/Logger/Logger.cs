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
    public static class Logger
    {
        /// Test Debug Message
        /// </summary>
        public static void LogDebug(string message, params object[] args)
        {
            //1
            if (LogConfig.Single().IsFileLogEnabled)
            {
                LogFactory.FileLog.Debug(message, args);
            }
        }

        public static void LogError(Exception exception)
        {
            #if DEBUG
                        LogError("{0}", exception);
            #else
                        LogError(exception.Message);
            #endif
        }


        /// <summary>
        /// Test Error Message
        /// </summary>
        public static void LogError(string message, params object[] args)
        {
            if (LogConfig.Single().IsFileLogEnabled)
            {
                LogFactory.FileLog.Error(message, args);
            }

            if (LogConfig.Single().IsColorConsoleLogEnabled)
            {
                LogFactory.ColorLog.Error(message, args);
            }

            if (LogConfig.Single().IsXmlLogEnabled)
            {
                LogFactory.XmlLog.Error(message, args);
            }
        }
        /// <summary>
        /// Test Warning Message
        /// </summary>
        public static void LogWarning(string message, params object[] args)
        {
            //1
            if (LogConfig.Single().IsFileLogEnabled)
            {
                LogFactory.FileLog.Warning(message, args);
            }
        }
        /// <summary>
        /// Test Information Message
        /// </summary>
        public static void LogInfo(string message, params object[] args)
        {
            if (LogConfig.Single().IsFileLogEnabled)
            {
                LogFactory.FileLog.Info(message, args);
            }
        }
    }
}
