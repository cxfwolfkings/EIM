using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingdee.CAPP.UpgradeClient.Enum
{
    public enum RegistryKeyValue
    {
        /// <summary>
        /// 注册表基项 HKEY_CLASSES_ROOT
        /// </summary>
        HKEY_CLASS_ROOT,
        /// <summary>
        /// 注册表基项 HKEY_CURRENT_USER
        /// </summary>
        HKEY_CURRENT_USER,
        /// <summary>
        /// 注册表基项 HKEY_LOCAL_MACHINE
        /// </summary>
        HKEY_LOCAL_MACHINE,
        /// <summary>
        /// 注册表基项 HKEY_USERS
        /// </summary>
        HKEY_USERS,
        /// <summary>
        /// 注册表基项 HKEY_CURRENT_CONFIG
        /// </summary>
        HKEY_CURRENT_CONFIG 
    }
}
