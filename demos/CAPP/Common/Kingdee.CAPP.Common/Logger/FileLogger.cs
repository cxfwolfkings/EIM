using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// Logger
    /// </summary>
    class FileLogger :ILog,IDisposable
    {
        private readonly LogLevel _logLevel;
        private readonly string _logFile;
        private readonly bool _isShowHeader;

        internal FileLogger()
        {
            _logLevel = LogConfig.Single().LogLevel;
            _logFile = LogConfig.Single().FileLogFullName;
            _isShowHeader = LogConfig.Single().IsShowHeader;

        }
        public FileLogger(LogLevel level, string logFile, bool isShowHeader)
        {
            _logLevel = level;
            _logFile = logFile;
            _isShowHeader = isShowHeader;

        }
        /// <summary>
        /// write log
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        private void WriteLog(LogLevel level, object message)
        {
            bool isAddHead = _isShowHeader;
            if (isAddHead)
            {
                isAddHead = !File.Exists(_logFile);
            }

            string head1 = "=============================================================";
            string head2 = "    Date       Time     MsgType            Message           ";
            string head3 = "=============================================================";

            lock (this)
            {
                FileStream fs = null;
                try
                {
                    fs = File.Open(_logFile, FileMode.Append, FileAccess.Write);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return;
                }

                LogSizeChecker logChecker = new LogSizeChecker(fs);
                if (logChecker.IsOver512kBytes())
                {
                    fs.Close();
                    FileInfo file = new FileInfo(_logFile);
                    LogFileMover fileMover = new LogFileMover(file);
                    fileMover.Move();

                    fs = File.Open(_logFile, FileMode.Append);
                    isAddHead = true;
                }
                StreamWriter sw = new StreamWriter(fs);
                if (isAddHead)
                {
                    sw.WriteLine(head1);
                    sw.WriteLine(head2);
                    sw.WriteLine(head3);
                }
                string preText = DateFormat.LogDate.GetLogDateFormat(DateTime.Now)
                                + " "
                                + LevelParser.GetLogTypeName(level);

                if (!_isShowHeader)
                {
                    preText = string.Empty;
                }
                //begin to write log
                if (message is Exception)
                {
                    Exception e = message as Exception;
                    if (LogConfig.Single().LogLevel == LogLevel.Debug)
                    {
                        sw.WriteLine(preText + e);
                    }
                    else
                    {
                        sw.WriteLine(preText + e.Message);
                    }
                }
                else
                {
                    sw.WriteLine(preText + message);
                }
                sw.Close();
                fs.Close();
            }

        }

        public void Error(string message, params object[] args)
        {
            string msg = string.Empty;
            try
            {
                msg = string.Format(message, args);
            }
            catch (Exception e)
            {
                WriteLog(LogLevel.Error, e.Message);
            }
            WriteLog(LogLevel.Error, msg);
        }

        public void Warning(string message, params object[] args)
        {
            if (_logLevel > LogLevel.Warning)
            {
                return;
            }
            WriteLog(LogLevel.Warning, string.Format(message, args));
        }

        /// <summary>
        /// record information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Info(string message, params object[] args)
        {
            if (_logLevel > LogLevel.Info)
            {
                return;
            }
            WriteLog(LogLevel.Info, string.Format(message, args));
        }

        /// <summary>
        /// .Logger.Logger
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Debug(string message, params object[] args)
        {
            if (_logLevel > LogLevel.Debug)
            {
                return;
            }
            WriteLog(LogLevel.Debug, string.Format(message, args));
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
