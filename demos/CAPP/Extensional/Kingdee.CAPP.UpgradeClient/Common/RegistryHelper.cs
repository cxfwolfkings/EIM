using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.UpgradeClient.Enum;
using Microsoft.Win32;

namespace Kingdee.CAPP.UpgradeClient.Common
{
    public class RegistryHelper
    {

        /// <summary>
        /// 写入注册表,如果指定项已经存在,则修改指定项的值
        /// </summary>
        /// <param name="rkv"></param>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public bool SetValue(
            RegistryKeyValue rkv, 
            string key, 
            string name, 
            string value)
        {
            try
            {
                RegistryKey rk = (RegistryKey)GetRegistryKey(rkv);
                RegistryKey rkt = rk.CreateSubKey(key);
                if (rkt != null)
                {
                    rkt.SetValue(name, value);
                }
                else
                {
                    throw (new Exception("要写入的项不存在"));
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <param name="rkv"></param>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValue(
            RegistryKeyValue rkv, 
            string key, 
            string name)
        {
            try
            {
                RegistryKey rk = (RegistryKey)GetRegistryKey(rkv);
                RegistryKey rkt = rk.OpenSubKey(key);
                if (rkt != null)
                {
                    return rkt.GetValue(name).ToString();
                }
                else
                {
                    throw (new Exception("无法找到指定项"));
                }
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 删除注册表中的值
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表项名称,不包括基项</param>
        /// <param name="name">值名称</param>
        /// <returns>返回布尔值,指定操作是否成功</returns>
        public bool RemoveValue(RegistryKeyValue keytype, string key, string name)
        {
            try
            {
                RegistryKey rk = (RegistryKey)GetRegistryKey(keytype);

                RegistryKey rkt = rk.OpenSubKey(key, true);

                if (rkt != null)
                {
                    rkt.DeleteValue(name, true);
                }
                else
                {
                    throw (new Exception("无法找到指定项"));
                }
                return true;

            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 删除注册表中的指定项
        /// </summary>
        /// <param name="keytype">注册表基项枚举</param>
        /// <param name="key">注册表中的项,不包括基项</param>
        /// <returns>返回布尔值,指定操作是否成功</returns>
        public bool RemoveSubKey(RegistryKeyValue keytype, string key)
        {
            try
            {
                RegistryKey rk = (RegistryKey)GetRegistryKey(keytype);
                rk.DeleteSubKeyTree(key);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断指定项是否存在
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsExistRegistryKey(RegistryKeyValue keyvalue, string key)
        {
            RegistryKey rk = (RegistryKey)GetRegistryKey(keyvalue);
            if (rk.OpenSubKey(key) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        static object GetRegistryKey(RegistryKeyValue rkv)
        {
            RegistryKey rk = null;
            switch (rkv)
            {
                ///HKEY_CLASSES_ROOT该主键包含了文件的扩展名和应用程序的关联信息
                ///以及Window Shell和OLE用于储存注册表的信息。
                /// 该主键下的子键决定了在WINDOWS中如何显示该类文件以及他们的图标，
                /// 该主键是从HKEY_LCCAL_MACHINE\SOFTWARE\Classes映射过来的.
                case RegistryKeyValue.HKEY_CLASS_ROOT:
                    rk = Registry.ClassesRoot;
                    break;
                ///该主键包含了如用户窗口信息，桌面设置等当前用户的信息.
                case RegistryKeyValue.HKEY_CURRENT_USER:
                    rk = Registry.CurrentUser;
                    break;
                ///主键包含了计算机软件和硬件的安装和配置信息，该信息可供所有用户使用.
                case RegistryKeyValue.HKEY_LOCAL_MACHINE:
                    rk = Registry.LocalMachine;
                    break;
                ///该主键记录了当前用户的设置信息，每次用户登入系统时，
                ///就会在该主键下生成一个与用户登入名一样的子键，
                ///该子键保存了当前用户的桌面设置、背景位图、快捷键，字体等信息。
                case RegistryKeyValue.HKEY_USERS:
                    rk = Registry.Users;
                    break;
                ///该主键保存了计算机当前硬件的配置信息，
                ///这些配置可以根据当前所连接的网络类型或硬件驱动软件安装的改变而改
                case RegistryKeyValue.HKEY_CURRENT_CONFIG:
                    rk = Registry.CurrentConfig;
                    break;
            }
            return rk;
        }
    }
}
