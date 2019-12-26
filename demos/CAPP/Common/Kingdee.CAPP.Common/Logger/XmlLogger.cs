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
    class XmlLogger:ILog,IDisposable
    {
        public void Error(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Debug(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
