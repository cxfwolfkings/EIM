using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Kingdee.CAPP.Common.FileRegister;
using System.IO;
using Microsoft.Win32;


namespace Kingdee.CAPP.Install
{
    [RunInstaller(true)]
    public partial class CAPPRegeisterInstaller : System.Configuration.Install.Installer
    {
        public CAPPRegeisterInstaller()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 安装时注册CAP文件
        /// </summary>
        /// <param name="savedState"></param>
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            string currentInstallPath = this.Context.Parameters["targetdir"];
            currentInstallPath = currentInstallPath.Replace("\\/", "\\");

            #region 注册CAP

            FileTypeRegInfo reginfo = new FileTypeRegInfo(".cap");

            reginfo.ExePath = currentInstallPath;   //Application.ExecutablePath;
            reginfo.Description = "CAPP程序";
            reginfo.IcoPath = currentInstallPath + @"Resources\cap.ico";

            try
            {
                FileTypeRegister.RegisterFileType(reginfo);
            }
            catch { } 
            #endregion

            #region 修改CAD默认菜单

            /// 判断是否已经注册AutoCAD
            if (!FileTypeRegister.IsRegeditExit())
            {
                try
                {
                    FileTypeRegister.RegisterAutoCADPlugIn(currentInstallPath);
                }
                catch { }
            }
            /// 增加工艺简图菜单到AutoCAD 中
            FileTypeRegister.UpdateAutoCADMenu(currentInstallPath);
            #endregion

            #region 注册Solidworks 插件
            try
            {
                Microsoft.Win32.RegistryKey hklm = Microsoft.Win32.Registry.LocalMachine;
                Microsoft.Win32.RegistryKey hkcu = Microsoft.Win32.Registry.CurrentUser;

                string keyname = "SOFTWARE\\SolidWorks\\Addins\\{2eff6d31-932a-4191-ad00-1d705e27a64f}";
                Microsoft.Win32.RegistryKey addinkey = hklm.CreateSubKey(keyname);
                addinkey.SetValue(null, 0);

                addinkey.SetValue("Description", "Solidworks AddIn Description");
                addinkey.SetValue("Title", "Routing");

                keyname = "Software\\SolidWorks\\AddInsStartup\\{2eff6d31-932a-4191-ad00-1d705e27a64f}";
                addinkey = hkcu.CreateSubKey(keyname);
                addinkey.SetValue(null, true, Microsoft.Win32.RegistryValueKind.DWord);
            }
            catch (System.NullReferenceException nl)
            {
                Console.WriteLine("There was a problem registering this dll: SWattr is null. \n\"" + nl.Message + "\"");
            }

            #endregion

            #region 解决 Win7不能操作注册表问题

            /**
             * 当前用户是管理员的时候，直接启动应用程序
             * 如果不是管理员，则使用启动对象启动程序，以确保使用管理员身份运行
             */
            //获得当前登录的Windows用户标示
            //System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            ////判断当前登录用户是否为管理员
            //if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            //{
            //    //如果是管理员，则直接运行               

            //    if (string.IsNullOrEmpty(MainFrm.FilePath))
            //    {
            //        var login = new LoginFrm();
            //        if (login.ShowDialog() == DialogResult.OK)
            //        {
            //            Application.Run(new MainFrm());
            //        }
            //    }
            //    else
            //    {
            //        Application.Run(new MainFrm());
            //    }
            //}
            //else
            //{
            //    //创建启动对象
            //    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //    //设置运行文件
            //    startInfo.FileName = System.Windows.Forms.Application.ExecutablePath;
            //    //设置启动参数
            //    startInfo.Arguments = String.Join(" ", args);
            //    //设置启动动作,确保以管理员身份运行
            //    startInfo.Verb = "runas";
            //    //如果不是管理员，则启动UAC
            //    System.Diagnostics.Process.Start(startInfo);
            //    //退出
            //    System.Windows.Forms.Application.Exit();
            //}   
            #endregion   
        }
    }
}
