using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace Kingdee.CAPP.Common.Logger
{
    class LogHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            if (section == null)
            {
                System.Diagnostics.Debug.WriteLine("<section name='Test' type='Test.ConfigHandler,Test'/>");
            }
            return section;
        }
    }
}
