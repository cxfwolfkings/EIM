using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log Interface
 *******************************/

namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// All log interface    
    /// </summary>
    interface ILog
	{
        /// <summary>
        /// Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Error(string message, params object[] args);
        /// <summary>
        /// Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Warning(string message, params object[] args);
        /// <summary>
        /// Info
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Info(string message, params object[] args);   
        /// <summary>
        /// Debug
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Debug(string message, params object[] args);          
		
	}
    /// <summary>
    /// Get date format
    /// </summary>
    interface ILogDate
    {
        /// <summary>
        /// custom format
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetLogDateFormat(DateTime dt);
        /// <summary>
        /// get date
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        string GetDateName(DateTime dt);
    }

    /// <summary>
    /// Get log config
    /// </summary>
    interface ILogConfig
    {
        /// <summary>
        /// all log
        /// </summary>
        LogLevel LogLevel { get; }

        /// <summary>
        /// file log name
        /// </summary>
        string FileLogFullName { get; }
        /// <summary>
        /// Xml name
        /// </summary>
        string XmlLogName { get; }

        /// <summary>
        /// ColorConsole
        /// </summary>
        bool IsColorConsoleLogEnabled { get; }

        /// <summary>
        /// XML
        /// </summary>
        bool IsXmlLogEnabled { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsFileLogEnabled { get; }

        /// <summary>
        /// ColorConsole
        /// </summary>
        int ColorConsoleLogMaxLength { get; }

        /// <summary>
        /// FileLog
        /// </summary>
        bool IsShowHeader { get; }
    }
}
