using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
/*******************************
 * Created By franco
 * Description: file transfer
 *******************************/


namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// file backup
    /// </summary>
    class LogFileMover
    {
        readonly FileInfo _fileInfo;

        public LogFileMover(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        /// <summary>
        /// file move
        /// </summary>
        public void Move()
        {
            string standardName = CheckLogBackupFolder() + DateFormat.LogDate.GetDateName(DateTime.Now);
            int i = 0;
            string newFileName = standardName + ".0";

            while (File.Exists(newFileName))
            {
                newFileName = standardName + "." + (++i);
            }
            try
            {
                _fileInfo.MoveTo(newFileName);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        /// <summary>
        /// generate directory
        /// </summary>
        /// <returns></returns>
        static string CheckLogBackupFolder()
        {
            string path = Path.GetFullPath(LogConfig.Single().FileLogFullName)
                            .Replace(Path.GetFileName(LogConfig.Single().FileLogFullName), "");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

    }
}
