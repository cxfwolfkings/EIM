using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Kingdee.CAPP.Common.Logger
{
    /// <summary>
    /// Not Found Data Xml 
    /// </summary>
    public class DataXmlNotFound: FileNotFoundException
    {
        public DataXmlNotFound()
            : base()
        {
            /// ToDo
        }
        public DataXmlNotFound(string message)
            : base(message)
        { 
        
        }
    }
}
