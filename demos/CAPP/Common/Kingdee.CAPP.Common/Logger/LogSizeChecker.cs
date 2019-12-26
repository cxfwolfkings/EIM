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
    class LogSizeChecker
    {
        private readonly FileStream _fileStream;

        public LogSizeChecker(FileStream fs)
        {
            _fileStream = fs;
        }

        /// <summary>
        /// File size is more than 512 k
        /// </summary>
        /// <returns></returns>
        public bool IsOver512kBytes()
        {
            long length = this._fileStream.Length / 1024;
            if (length > 512)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
