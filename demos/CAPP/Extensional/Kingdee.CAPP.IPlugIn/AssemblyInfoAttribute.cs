using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: capp system module plug-in attribute
 *******************************/

namespace Kingdee.CAPP.IPlugIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AssemblyInfoAttribute:Attribute
    {
        /// <summary>
        /// Assembly name
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Assembly Version
        /// </summary>
        public string AssemblyVersion { get; set; }
        /// <summary>
        /// Menu name
        /// </summary>
        public string MenuName { get; set; }

        #region constructor
        public AssemblyInfoAttribute()
        {

        } 
        #endregion
    }
}
