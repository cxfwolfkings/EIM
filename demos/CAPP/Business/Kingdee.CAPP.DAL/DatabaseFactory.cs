using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// Database Factory with Singleton
    /// </summary>
    public class DatabaseFactory
    {
        private static Database dbase=null;
        public static Database Instance()
        {
            if (dbase == null)
            {
                dbase = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("PLM"); 
            }
            return dbase;
        }

        /// <summary>
        /// 方法说明：重载Instance()方法
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="configName">配置名称</param>
        /// <returns></returns>
        public static Database PlmInstance(string configName)
        {
            if (!string.IsNullOrEmpty(configName))
            {
                dbase = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(configName);
            }
            return dbase;
        }
    }
}
