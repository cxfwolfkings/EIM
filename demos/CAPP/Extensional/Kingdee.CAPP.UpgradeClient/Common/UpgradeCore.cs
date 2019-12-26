using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Kingdee.CAPP.UpgradeClient.Resource;

namespace Kingdee.CAPP.UpgradeClient.Common
{
    public class UpgradeCore
    {

        List<string> needUpdateFile = new List<string>();
        /// <summary>
        /// 远程文件路径
        /// </summary>
        const string remoteUri = @"\\10.50.71.42\db\ReferenceDlls";
        static string remoteNewUpgradeFile = remoteUri + @"\Upgrade\Upgrade.xml";
        /// <summary>
        /// 本地临时升级文件
        /// </summary>
        static string localTmpUpgradeFile = Application.StartupPath + @"\TmpUpgrade.xml";
        /// <summary>
        /// 本地的升级文件
        /// </summary>
        static string localOldUpgradeFile = Application.StartupPath + @"\Upgrade\Upgrade.xml";

        WebClient webClient = new WebClient();

        /// <summary>
        /// Download upgrade.xml
        /// </summary>
        public  void DownLoadUpgrade()
        {
            try
            {
                webClient.UseDefaultCredentials = true;
                webClient.DownloadFile(remoteNewUpgradeFile, localTmpUpgradeFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resource1.CANNOTFINDREMOTEUPGRADEFILE + "\r\n"
                            + ex.Message);
                Application.Exit();
            }
        }

        /// <summary>
        /// 检查是否需要更新
        /// </summary>
        /// <returns></returns>
        public bool CheckNeedUpdate()
        {
            string tmpUpdateDates = string.Empty;
            string oldUpdateDates = string.Empty;

            bool isTmpTransformSuc = false;
            DateTime tmpUpdateDate = DateTime.Now;


            bool isOldTransformSuc = false;
            DateTime oldUpdateDate = DateTime.Now;

            try
            {
                XmlDocument document = new XmlDocument();
                ///读取临时下载的升级文件
                document.Load(localTmpUpgradeFile);

                XmlNode nodeTmpUpdateDate = document.SelectSingleNode("UpdateTime");
                tmpUpdateDates = nodeTmpUpdateDate.Attributes["Date"].Value;

                ///读取上次更新的时间
                document.Load(localOldUpgradeFile);
                XmlNode nodeOldUpdateDate = document.SelectSingleNode("UpdateTime");
                oldUpdateDates = nodeOldUpdateDate.Attributes["Date"].Value;


                ///比较两个时间
                ///如果临时更新时间（服务器最新时间）比上次更新的时间要新，返回True，否则返回false.
                ///如果临时更新时间为空。则返回false.
                ///如果上次更新的时间为空。则返回 True.
                ///其它返回 false.

                isTmpTransformSuc = DateTime.TryParse(tmpUpdateDates, out tmpUpdateDate);

                if (!isTmpTransformSuc)
                {
                    return false;
                }

                isOldTransformSuc = DateTime.TryParse(oldUpdateDates, out oldUpdateDate);

                if (!isOldTransformSuc)
                {
                    return true;
                }

                if (tmpUpdateDate > oldUpdateDate)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 下载需要更新的文件
        /// </summary>
        public void DownLoadNeedUpdateFile(ProgressBar _progressBar)
        {
            string tempFileName  = string.Empty;
            string realFileName  = string.Empty;
            string fileName = string.Empty;
            try
            {
                XmlDocument document = new XmlDocument();
                ///读取临时下载的升级文件
                document.Load(localTmpUpgradeFile);
                XmlNodeList upgradeFileList = document.SelectNodes("/AutoUpdater/UpdateFileList/UpdateFile");


                _progressBar.Maximum = upgradeFileList.Count;

                foreach (XmlNode node in upgradeFileList)
                {
                    tempFileName = Application.StartupPath + "\\" 
                        + DateTime.Now.TimeOfDay.TotalMilliseconds;

                    fileName =  node.Attributes["FileName"].Value;
                    realFileName = Application.StartupPath + "\\" + fileName;

                    webClient.UseDefaultCredentials = true;
                    webClient.DownloadFile(remoteUri + "\\" + fileName, tempFileName);
                    File.Copy(tempFileName, realFileName, true);
                    File.SetLastWriteTimeUtc(realFileName, DateTime.Now);
                    File.Delete(tempFileName);

                    _progressBar.Value++;
                    Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(Resource1.DOWNLOADUPGRADEFILEERROR
                    , fileName
                    , ex.Message));
            }
        }

        private string GetVersion(string Version)
        {
            try
            {
                string[] x = Version.Split('.');
                return string.Format("{0:00000}{1:00000}{2:00000}{3:00000}"
                    , Convert.ToInt32(x[0]),
                    Convert.ToInt32(x[1]),
                    Convert.ToInt32(x[2]),
                    Convert.ToInt32(x[3]));
            }
            catch
            {
                return "";
            }
        }

        public string AppExe
        {
            get;
            set;
        }
             

        /// <summary>
        /// 正式更新前,关闭应用主程序.
        /// </summary>
        public void KillAppExe()
        {
            /// 关闭更新前删除临时文件
            if (File.Exists(localTmpUpgradeFile))
            {
                File.Delete(localTmpUpgradeFile);
            }

            XmlDocument document = new XmlDocument();
            string appExe = string.Empty;
            try
            {
                ///读取临时下载的升级文件
                document.Load(localTmpUpgradeFile);
                XmlNode node = document.SelectSingleNode("AppName");
                appExe = node.Attributes["Name"].Value;
            }
            catch
            {
                return;
            }

            if (string.IsNullOrEmpty(appExe))
            {
                return;
            }
            AppExe = appExe;

            XmlNodeList upgradeFileList = document.SelectNodes("/AutoUpdater/UpdateFileList/UpdateFile");
            

            Process[] local = Process.GetProcesses();
            int i = 0;
            for (i = 0; i <= local.Length - 1; i++)
            {
                if (local[i].ProcessName.ToLower().Trim()
                    != appExe.ToLower().Trim())
                    continue;
                
                try
                {
                    local[i].Kill();
                }
                catch
                {
                    MessageBox.Show(string.Format(Resource1.CLOSEAPPLICATIONPROCESSFAILED,appExe));                    
                }
                return;
            }
            MessageBox.Show(string.Format(Resource1.CANNOTFINDAPPLICATIONPROCESS, appExe));
        }

        /// <summary>
        /// 重新启动应用程序
        /// </summary>
        public void RestartApp()
        {
            string startupAppPath = Application.StartupPath + @"\" + AppExe;
            try
            {
                Process.Start(startupAppPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resource1.STARTUPAPPLICATIONFAILED, AppExe));
            }
        }
    }
}
