using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Kingdee.CAPP.Common.FileRegister
{
    public class FileTypeRegister
    {
        ///使文件类型与对应的图标及应用程序关联起来。
        public static void RegisterFileType(FileTypeRegInfo fileTypeRegInfo)
        {
            try
            {
                if (IsFileTypeRegistered(fileTypeRegInfo.ExtendName))
                {
                    return;
                }
                string relationName = fileTypeRegInfo.ExtendName.Substring(0,
                    fileTypeRegInfo.ExtendName.Length - 1).ToUpper() + "_FileType";

                
                ///Create file type 
                /// .cap and set value is .ca_FileType
                RegistryKey fileTypeKey = Registry.ClassesRoot.CreateSubKey(fileTypeRegInfo.ExtendName);
                fileTypeKey.SetValue("", relationName);
                fileTypeKey.Close();

                /// create relate ioc and run path
                RegistryKey relationKey = Registry.ClassesRoot.CreateSubKey(relationName);
                relationKey.SetValue("", fileTypeRegInfo.Description);

                RegistryKey iconKey = relationKey.CreateSubKey("DefaultIcon");
                iconKey.SetValue("", fileTypeRegInfo.IcoPath);

                RegistryKey shellKey = relationKey.CreateSubKey("Shell");
                RegistryKey openKey = shellKey.CreateSubKey("Open");
                RegistryKey commandKey = openKey.CreateSubKey("Command");
                commandKey.SetValue("", fileTypeRegInfo.ExePath + " %1");

                relationKey.Close();
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// GetFileTypeRegInfo 得到指定文件类型关联信息
        /// </summary>        
        public static FileTypeRegInfo GetFileTypeRegInfo(string extendName)
        {
            if (!IsFileTypeRegistered(extendName))
            {
                return null;
            }

            FileTypeRegInfo regInfo = new FileTypeRegInfo(extendName);

            string relationName = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName);
            regInfo.Description = relationKey.GetValue("").ToString();

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon");
            regInfo.IcoPath = iconKey.GetValue("").ToString();

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command");
            string temp = commandKey.GetValue("").ToString();
            regInfo.ExePath = temp.Substring(0, temp.Length - 3);

            return regInfo;
        }

        /// <summary>
        /// UpdateFileTypeRegInfo 更新指定文件类型关联信息
        /// </summary>    
        public static bool UpdateFileTypeRegInfo(FileTypeRegInfo regInfo)
        {
            if (!IsFileTypeRegistered(regInfo.ExtendName))
            {
                return false;
            }


            string extendName = regInfo.ExtendName;
            string relationName = extendName.Substring(1, extendName.Length - 1).ToUpper() + "_FileType";
            RegistryKey relationKey = Registry.ClassesRoot.OpenSubKey(relationName, true);
            relationKey.SetValue("", regInfo.Description);

            RegistryKey iconKey = relationKey.OpenSubKey("DefaultIcon", true);
            iconKey.SetValue("", regInfo.IcoPath);

            RegistryKey shellKey = relationKey.OpenSubKey("Shell");
            RegistryKey openKey = shellKey.OpenSubKey("Open");
            RegistryKey commandKey = openKey.OpenSubKey("Command", true);
            commandKey.SetValue("", regInfo.ExePath + " %1");

            relationKey.Close();

            return true;
        }

        /// <summary>
        /// 判断文件是否已经注册
        /// </summary>
        /// <param name="extendName"></param>
        /// <returns></returns>
        public static bool IsFileTypeRegistered(string extendName)
        {
            RegistryKey softWareKey = Registry.ClassesRoot.OpenSubKey(extendName);
            if (softWareKey != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册autocad PlugIn
        /// </summary>
        /// <param name="plugInPath">插件的路径</param>
        /// <returns>是否注册成功</returns>
        public static bool RegisterAutoCADPlugIn(string plugInPath)
        {
            plugInPath = plugInPath + "Plug-In";

            RegistryKey cadKey = Registry.LocalMachine
                .OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R18.2\ACAD-A001:804\Applications",true);
            
            if (cadKey == null)
            {
                return false;
            }

            try
            {
                RegistryKey drawRectangleSubKey = cadKey.CreateSubKey("DrawRectangle");
                drawRectangleSubKey.SetValue("DESCRIPTION", "DrawRectangle.");
                drawRectangleSubKey.SetValue("LOADCTRLS", "2", RegistryValueKind.DWord);
                drawRectangleSubKey.SetValue("LOADER", plugInPath + "\\Kingdee.CAPP.Autocad.DrawRectanglePlugIn.dll");
                drawRectangleSubKey.SetValue("MANAGED", "1",RegistryValueKind.DWord);
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// 判断注册表是否存在
        /// </summary>
        /// <returns></returns>
        public static bool IsRegeditExit()
        {
            bool _exit = false;

            RegistryKey cadKey = Registry.LocalMachine
                .OpenSubKey(@"SOFTWARE\Autodesk\AutoCAD\R18.2\ACAD-A001:804\Applications", true);

            if (cadKey != null)
            {
                RegistryKey drawRectangleSubKey = cadKey.OpenSubKey("DrawRectangle", true);

                if (drawRectangleSubKey != null)
                {
                    _exit = true;
                }
            }
            return _exit;
        }
        /// <summary>
        ///  增加工艺简图菜单
        /// </summary>
        public static void UpdateAutoCADMenu(string plugInPath)
        {
            string appPath = plugInPath + "Plug-In\\cadMenuCUIX\\CraftPicture";

            string regCADPath = @"Software\Autodesk\AutoCAD\R18.2\ACAD-A001:804\Profiles\<<未命名配置>>\General Configuration";
            RegistryKey cadKey = Registry.CurrentUser.OpenSubKey(regCADPath, true);

            if(cadKey != null)
            {
                string cadmenu = cadKey.GetValue("MenuFile").ToString();

                if (!cadmenu.Contains(appPath))
                {
                    cadKey.SetValue("MenuFile", appPath, RegistryValueKind.String);
                }
            }

        }
        /// <summary>
        /// 重置Autocad菜单
        /// </summary>
        public static void ResetAutoCADMenu()
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            appPath = appPath + @"\Autodesk\AutoCAD 2012 - Simplified Chinese\R18.2\chs\Support\acad";
            string regCADPath = @"Software\Autodesk\AutoCAD\R18.2\ACAD-A001:804\Profiles\<<未命名配置>>\General Configuration";
            RegistryKey cadKey = Registry.CurrentUser.OpenSubKey(regCADPath, true);
            
            if (cadKey != null)
            {
                string cadmenu = cadKey.GetValue("MenuFile").ToString();

                if (!cadmenu.Contains(appPath))
                {
                    cadKey.SetValue("MenuFile", appPath, RegistryValueKind.String);
                }
            }
        }
    }
}
