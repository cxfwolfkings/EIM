using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using Kingdee.CAPP.Common.FileRegister;


namespace Kingdee.CAPP.Uninstall
{
    [RunInstaller(true)]
    public partial class CADCuxUninstall : System.Configuration.Install.Installer
    {
        public CADCuxUninstall()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 卸载时删除已注册的CAP文件
        /// 同时删除CAD 文件
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            #region 卸载已注册的CAP文件
            RegistryKey capReg = Registry.ClassesRoot.OpenSubKey(".cap");
            RegistryKey capFileTypeReg = Registry.ClassesRoot.OpenSubKey(".CA_FileType");

            if (capReg != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(".cap");
            }
            if (capFileTypeReg != null)
            {
                Registry.ClassesRoot.DeleteSubKeyTree(".CA_FileType");

            }
            #endregion

            /// 重置AutoCAD菜单
            FileTypeRegister.ResetAutoCADMenu();

            #region 删除Solidworks插件
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{2eff6d31-932a-4191-ad00-1d705e27a64f}";
                hklm.DeleteSubKey(keyname);

                keyname = "Software\\SolidWorks\\AddInsStartup\\{2eff6d31-932a-4191-ad00-1d705e27a64f}";
                hkcu.DeleteSubKey(keyname);
            }
            catch (System.NullReferenceException nl)
            {
            }
            catch (System.Exception e)
            {
            }
            #endregion

        }
    }
}
