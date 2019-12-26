using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log factory
 *******************************/

namespace Kingdee.CAPP.Common.Logger
{
    static class LogFactory
    {
        private static ILog _filelog;
        public static ILog FileLog
        {
            get 
            {
                if (_filelog == null)
                {
                    _filelog = new FileLogger();
                }
                return _filelog;
            }
        }

        private static ILog _xmlLog;
        public static ILog XmlLog
        {
            get
            {
                if (_xmlLog == null)
                {
                    _xmlLog = new XmlLogger();
                }
                return _xmlLog;
            }
        }

        private static ILog _colorLog;
        public static ILog ColorLog
        {
            get
            {
                if (_colorLog == null)
                {
                    _colorLog = new ColorLogger();
                }
                return _colorLog;
            }
        }
    }
}
