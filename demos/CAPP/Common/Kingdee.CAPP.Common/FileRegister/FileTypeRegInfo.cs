using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.Common.FileRegister
{
    public class FileTypeRegInfo
    {

        public FileTypeRegInfo(string extendName)
        {
            this._extendName = extendName;
        }

        /// <summary>
        /// 目标类型文件的扩展名
        /// </summary>
        private string _extendName;
        public string ExtendName
        {
            get
            {
                return _extendName;
            }
            set 
            {
                _extendName = value;
            }
        }
        /// <summary>
        /// 目标文件类型说明
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 目标类型关联的图片
        /// </summary>
        public string IcoPath
        {
            get;
            set;
        }
        /// <summary>
        /// 打开目标类型文件的应用程序
        /// </summary>
        public string ExePath
        {
            get;
            set;
        }
        

    }
}
