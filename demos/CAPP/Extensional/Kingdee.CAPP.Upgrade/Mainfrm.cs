using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Resources;
using Kingdee.CAPP.UpgradeServer.Resource;
using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace Kingdee.CAPP.UpgradeServer
{
    /// <summary>
    /// 服务端生成升级文件
    /// </summary>
    public partial class Mainfrm : Form
    {
        public Mainfrm()
        {
            InitializeComponent();
        }

        XmlDocument xmlDoc = new XmlDocument();
        XmlElement root;

        /// <summary>
        /// 浏览生成升级文件存放目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            fbdPath.ShowDialog();
            string generatePath = fbdPath.SelectedPath;
            tbxPath.Text = generatePath;
        }
        /// <summary>
        /// 生成升级文件存放目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbxPath.Text.Trim()))
            {
                MessageBox.Show(Resource.Resource1.SELECTUPGRATEFOLDER);
                return;
            }

            if (!Directory.Exists(tbxPath.Text.Trim()))
            {
                MessageBox.Show(Resource1.NOTEXISTSDIRECTORY);
                return;
            }
            string upgradeFilePath = tbxPath.Text.Trim() + @"\Upgrade.xml";

            try
            {
                if (File.Exists(upgradeFilePath))
                {
                    xmlDoc.RemoveAll();
                }


                ///XML Create Element
                XmlProcessingInstruction xmlInstruction
                    = xmlDoc.CreateProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xmlDoc.AppendChild(xmlInstruction);
                CreateRootNode();

                xmlDoc.Save(upgradeFilePath);
                MessageBox.Show(Resource1.UPGRADEFILEALREADYGENEGRATE);

            }
            catch (Exception ex)
            {
                MessageBox.Show(Resource1.CREATEAUTOUPGRATEFILEFIELD + ex.Message);
            }



            this.Height = 223;

            lblSupplyDownloadServerPath.Visible = true;
            btnBrowserSupplyDownloadServerPath.Visible = true;
            btnCopyToSupplyDownloadServerPath.Visible = true;
            tbxSupplyDownloadServerPath.Visible = true;

        }
        /// <summary>
        /// 关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        void CreateRootNode()
        {

            root = xmlDoc.CreateElement("AutoUpdater");
            xmlDoc.AppendChild(root);

            XmlElement urladdressElement = xmlDoc.CreateElement("URLAddres");
            urladdressElement.SetAttribute("URL", "");
            root.AppendChild(urladdressElement);

            XmlElement updateInfoElement = xmlDoc.CreateElement("UpdateInfo");
            root.AppendChild(updateInfoElement);


            XmlElement updateTimeElement = xmlDoc.CreateElement("UpdateTime");
            updateTimeElement.SetAttribute("Date", DateTime.Now.ToShortDateString());
            updateInfoElement.AppendChild(updateTimeElement);


            XmlElement versionElement = xmlDoc.CreateElement("Version");
            versionElement.SetAttribute("Num", "");
            updateInfoElement.AppendChild(versionElement);


            XmlElement updateFileListElement = xmlDoc.CreateElement("UpdateFileList");
            root.AppendChild(updateFileListElement);

            XmlElement restartAppElement = xmlDoc.CreateElement("RestartApp");
            root.AppendChild(restartAppElement);


            XmlElement reStartElement = xmlDoc.CreateElement("ReStart");
            reStartElement.SetAttribute("Allow", "Yes");
            restartAppElement.AppendChild(reStartElement);


            XmlElement appNameElement = xmlDoc.CreateElement("AppName");
            appNameElement.SetAttribute("Name", "Kingdee.CAPP.UI.exe");
            restartAppElement.AppendChild(appNameElement);


            loopNodes(updateFileListElement, this.tbxPath.Text);

        }
        /// <summary>
        /// generate update file by file directory
        /// </summary>
        /// <param name="leaf"></param>
        /// <param name="path"></param>
        void loopNodes(XmlElement leaf, string path)
        {
            DirectoryInfo ofs = new DirectoryInfo(path);

            foreach (FileInfo fi in ofs.GetFiles())
            {
                if (fi.Name != "Upgrade.xml"
                    & !fi.Name.EndsWith(".pdb")
                    & !fi.Name.EndsWith(".config")
                    & !fi.Name.EndsWith(".vshost.exe")
                    & !fi.Name.EndsWith(".manifest")
                    & !fi.Name.EndsWith(".cs")
                    & !fi.Name.EndsWith(".user")
                    & !fi.Name.EndsWith(".resx")
                    & !fi.Name.EndsWith(".csproj")
                    & !fi.Name.EndsWith(".suo"))
                {
                    XmlElement updateFileElement = xmlDoc.CreateElement("UpdateFile");
                    updateFileElement.SetAttribute("FileName", fi.Name);
                    leaf.AppendChild(updateFileElement);
                }
            }
        }

        private void Mainfrm_Load(object sender, EventArgs e)
        {
            this.Height = 130;
        }

        /// <summary>
        /// select download path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowserSupplyDownloadServerPath_Click(object sender, EventArgs e)
        {
            fdbDownloadForServerPath.ShowDialog();
            string downLoadPath = fdbDownloadForServerPath.SelectedPath;
            tbxSupplyDownloadServerPath.Text = downLoadPath;
        }

        /// <summary>
        /// copy file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyToSupplyDownloadServerPath_Click(object sender, EventArgs e)
        {
            /// whether network path
            Regex reg = new Regex(@"^\\\\[^\\]+(\\[^\\]*?)*$");
            Regex regNetWork = new Regex(@"^\\\\[^\\]+");

            string serverpath = tbxSupplyDownloadServerPath.Text.Trim();
            string generatePath = tbxPath.Text;

            try
            {
                DirectoryInfo ofs = new DirectoryInfo(generatePath);
                string currentVersion = string.Empty;
                foreach (FileInfo fs in ofs.GetFiles())
                {
                    if (fs.Name.Contains("Kingdee.CAPP.UI.exe"))
                    {
                        FileVersionInfo version = FileVersionInfo.GetVersionInfo("Kingdee.CAPP.UI.exe");
                        currentVersion = version.FileVersion;
                    }
                    fs.CopyTo(serverpath + "\\" + fs.Name, true);
                }

                MatchCollection matchs = regNetWork.Matches(tbxSupplyDownloadServerPath.Text.Trim());
               
                string ipaddress = string.Empty;
                if (matchs.Count > 0)
                {
                   ipaddress = matchs[0].Value.Trim('\\');
                   IPAddress[] currentIps = Dns.GetHostAddresses(ipaddress);
                   ipaddress = currentIps[0].ToString();
                }

                if (File.Exists(serverpath + "\\Upgrade.xml"))
                {
                    XmlDocument doucment = new XmlDocument();
                    doucment.Load(serverpath + "\\Upgrade.xml");

                    XmlNode node = doucment.SelectSingleNode("//URLAddres[@URL='']");
                    node.Attributes["URL"].Value = ipaddress;


                    XmlNode node1 = doucment.SelectSingleNode("//Version[@Num='']");
                    node1.Attributes["Num"].Value = currentVersion;

                    doucment.Save(serverpath + "\\Upgrade.xml");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Resource1.COPYFILETOSERVERFIELD);
            }


        }
    }
}
