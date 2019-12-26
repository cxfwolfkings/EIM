using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using Kingdee.CAPP.UpgradeClient.Common;
using Kingdee.CAPP.UpgradeClient.Resource;
using System.Diagnostics;

namespace Kingdee.CAPP.UpgradeClient
{
    public partial class frmAutoUpgrade : Form
    {
        public frmAutoUpgrade()
        {
            InitializeComponent();
        }
        private void frmAutoUpgrade_Shown(object sender, EventArgs e)
        {
            UpgradeCore core = new UpgradeCore();

            /// 下载配置文件
            core.DownLoadUpgrade();
            bool isneedUpdate = core.CheckNeedUpdate();

            ///检查需要更新
            if (!isneedUpdate)
            {
                lblNotice.Text = Resource1.CURRENTISLASTLYVERSON;
                progressBar1.Maximum = 10;
                progressBar1.Value = 10;
                return;
            }

            lblNotice.Text = Resource1.DOWNLOADINGFILE;
            ///下载更新文件
            core.DownLoadNeedUpdateFile(this.progressBar1);
            lblNotice.Text = Resource1.DOWNLOADCOMPLETE;
            ///关闭当前应用程序
            core.KillAppExe();
            ///安装升级文件
            lblNotice.Text = Resource1.INSTALLINGUPGRADEFILE;
            ///重启应用程序
            core.RestartApp();
            ///关闭升级的窗体
            this.Close();

        }
    }
}
