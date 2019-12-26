using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Diagnostics;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace Kingdee.CAPP.Common.Logger
{
    class LogConfig: ILogConfig
    {
        private const string cDefaultLogFileName = "log.log";
        private const string cDefaultXmlFileName = "log.xml";
        private const bool cDefaultLogConsole = false;
        private const bool cDefaultShowHeaderValue = true;
        private const int cDefaultLen = int.MaxValue;
        private const bool cDefaultShowXmlLog = false;
        private const bool cDefaultShowTraceLog = true;
        private const LogLevel cDefaultLogLevel = LogLevel.Debug;
        private const bool cDefaultEnableFileLog = true;
        private const string cLogLevel = "LogLevel";
        private const string cLogFile = "LogFile";
        private const string cLogConsole = "Console";
        private const string cShowHeader = "ShowHeader";
        private const string cXmlLog = "XmlLog";
        private const string cEnabled = "enabled";
        private const string cKeyXmlLogEnabled = "KeyXmlLogEnabled";
        private const string cKeyFileLogEnabled = "KeyFileLogEnabled";
        private const string cLen = "len";
        private const string clevel = "level";
        private const string SectionName = "Log";

        private readonly Hashtable _appSettings;
        private static ILogConfig _single;
        private ILog _logger;

        internal static ILogConfig Single()
        {
            if (null == _single)
            {
                _single = new LogConfig();

            }
            return _single;
        }


        /// <summary>
        /// init
        /// </summary>
        LogConfig()
        {
            _appSettings = new Hashtable();
            ReadXml();
            _logger = new FileLogger(LogLevel, FileLogFullName, IsShowHeader);
        }

        private class Factory
        {
            public static XmlNode GetConfig()
            {
                XmlNode node = null;
                XmlDocument xml = new XmlDocument();
                try
                {
                    xml.Load(AppDomain.CurrentDomain.BaseDirectory + "App_Data\\Data\\Data.xml");
                }
                catch
                {
                    try
                    {
                        xml.Load(AppDomain.CurrentDomain.BaseDirectory + "Data.xml");
                    }
                    catch (FileNotFoundException)
                    {
                        return null;
                    }
                }
                XmlNodeList nodes = xml.SelectNodes("/Config/Log");//xpath
                foreach (XmlNode n in nodes)
                {
                    if (n.Name == "Log")
                    {
                        node = n;
                        break;
                    }
                }
                return node;
            }
        }

        /// <summary>
        /// Log.config
        /// </summary>
        private void ReadXml()
        {
            XmlNode root = null;
            try
            {
                root = ConfigurationManager.GetSection(SectionName) as XmlNode;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            if (root == null)
            {

                string file = AppDomain.CurrentDomain.BaseDirectory + "Log.config";
                Debug.WriteLine(file);
                if (!File.Exists(file))
                {
                    root = Factory.GetConfig();
                }
                else
                {
                    ConfigurationFileMap fileMap = new ConfigurationFileMap(file);
                    Configuration configuration = ConfigurationManager.OpenMappedMachineConfiguration(fileMap);
                    ConfigurationSection section = configuration.GetSection(SectionName);
                    string rawxml = section.SectionInformation.GetRawXml();
                    Debug.WriteLine(rawxml);
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(rawxml);
                    root = document.FirstChild;
                    if (root == null)
                    {
                        root = Factory.GetConfig();
                    }
                }

            }
            if (root == null)
            {
                return;
            }
            if (!_appSettings.Contains(cLogLevel))
            {
                //level
                Debug.WriteLine(string.Format("{0}={1}", cLogLevel, root.Attributes[clevel].Value));
                _appSettings.Add(cLogLevel, root.Attributes[clevel].Value);
            }
            for (int i = 0, t = root.ChildNodes.Count; i < t; i++)
            {
                string key = root.ChildNodes[i].Name;
                string value = root.ChildNodes[i].InnerText;
                //file
                if (key == cLogFile && !_appSettings.ContainsKey(cLogFile))
                {
                    _appSettings.Add(key, value);
                    if (!_appSettings.ContainsKey(cKeyFileLogEnabled))
                    {
                        if (root.ChildNodes[i].Attributes.Count != 0)
                        {
                            _appSettings.Add(cKeyFileLogEnabled, root.ChildNodes[i].Attributes[cEnabled].Value);
                            Debug.WriteLine(string.Format("{0}={1}", cKeyFileLogEnabled, root.ChildNodes[i].Attributes[cEnabled].Value));
                        }
                    }
                }
                //XML
                else if (key == cXmlLog && !_appSettings.Contains(cXmlLog))
                {
                    _appSettings.Add(key, value);
                    Debug.WriteLine(string.Format("{0}={1}", key, value));
                    if (!_appSettings.ContainsKey(cKeyXmlLogEnabled))
                    {
                        if (root.ChildNodes[i].Attributes.Count != 0)
                        {
                            _appSettings.Add(cKeyXmlLogEnabled, root.ChildNodes[i].Attributes[cEnabled].Value);
                        }
                    }
                }
                //ColorConsole
                else if (key == cLogConsole && !_appSettings.Contains(cLogConsole))
                {
                    _appSettings.Add(key, value);
                    Debug.WriteLine(string.Format("{0}={1}", key, value));
                    if (!_appSettings.ContainsKey(cLen))
                    {
                        if (root.ChildNodes[i].Attributes.Count != 0)
                        {
                            _appSettings.Add(cLen, root.ChildNodes[i].Attributes[cLen].Value);
                        }
                    }
                }
                //other
                else if (!_appSettings.Contains(key))
                {
                    Debug.WriteLine(string.Format("{0}={1}", key, value));
                    _appSettings.Add(key, value);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public LogLevel LogLevel
        {
            get
            {
                if (_appSettings.Contains(cLogLevel))
                {
                    string level = _appSettings[cLogLevel].ToString();
                    if (level != null && level.Trim().ToLower() == "debug")
                    {
                        return LogLevel.Debug;
                    }
                    else if (level != null && level.Trim().ToLower() == "info")
                    {
                        return LogLevel.Info;
                    }
                    else if (level != null && level.Trim().ToLower() == "warning")
                    {
                        return LogLevel.Warning;
                    }
                    else if (level != null && level.Trim().ToLower() == "error")
                    {
                        return LogLevel.Error;
                    }
                    else//default, if no setting or wrong setting
                    {
                        return cDefaultLogLevel;
                    }
                }
                return cDefaultLogLevel;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public bool IsColorConsoleLogEnabled
        {
            get
            {
                if (_appSettings.Contains(cLogConsole))
                {
                    string result = _appSettings[cLogConsole] + "";
                    return result.Trim().ToLower() == "true" ? true : false;
                }
                return cDefaultLogConsole;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsXmlLogEnabled
        {
            get
            {
                if (_appSettings.Contains(cKeyXmlLogEnabled))
                {
                    string result = _appSettings[cKeyXmlLogEnabled] + ""; //注意cKeyXmlLogEnabled
                    return result.Trim().ToLower() == "true" ? true : false;
                }
                return cDefaultShowXmlLog;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public bool IsFileLogEnabled
        {
            get
            {
                if (_appSettings.Contains(cKeyFileLogEnabled))
                {
                    string r = _appSettings[cKeyFileLogEnabled] + "";
                    return r.Trim().ToLower() == "true" ? true : false;
                }
                return cDefaultEnableFileLog;
            }
        }

        /// <summary>
        /// log.xml
        /// </summary>
        public string XmlLogName
        {
            get
            {
                string path;
                if (_appSettings.Contains(cXmlLog))
                {
                    path = _appSettings[cXmlLog] + "";
                }
                else
                {
                    path = cDefaultXmlFileName;
                }
                string file = AppDomain.CurrentDomain.BaseDirectory + path;
                string dir = file.Replace(Path.GetFileName(file), "");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return file;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string FileLogFullName
        {
            get
            {
                string path;
                if (_appSettings.Contains(cLogFile))
                {
                    path = _appSettings[cLogFile] + "";
                }
                else
                {
                    path = cDefaultLogFileName;
                }
                string file = AppDomain.CurrentDomain.BaseDirectory + path;
                string dir = file.Replace(Path.GetFileName(file), "");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return file;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ColorConsoleLogMaxLength
        {
            get
            {
                if (_appSettings.Contains(cLen))
                {
                    int len;
                    if (!Int32.TryParse(_appSettings[cLen] + "", out len))
                    {
                        len = Int32.MaxValue;
                    }
                    return len;
                }
                else
                {
                    return cDefaultLen;
                }
            }
        }

        public bool IsShowHeader
        {
            get
            {
                if (_appSettings.Contains(cShowHeader))
                {
                    string r = _appSettings[cShowHeader] + "";
                    return r.Trim().ToLower() == "true" ? true : false;
                }
                return cDefaultShowHeaderValue;
            }

        }
    }
}
