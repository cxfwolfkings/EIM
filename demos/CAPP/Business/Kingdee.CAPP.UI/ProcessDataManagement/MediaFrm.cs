using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxWMPLib;

namespace Kingdee.CAPP.UI.ProcessDataManagement
{    
    /// <summary>
    /// 类型说明：三维动画播放窗体
    /// 作      者：jason.tang
    /// 完成时间：2013-08-26
    /// </summary>
    public partial class MediaFrm : BaseSkinForm
    {
        private AxWindowsMediaPlayer player;

        public string VideoPath { get; set; }

        public MediaFrm()
        {
            InitializeComponent();

            player = new AxWindowsMediaPlayer();
            this.pnBody.Controls.Add(player);
        }

        private void MediaFrm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(VideoPath))
                return;
            player.Dock = DockStyle.Fill;
            player.newMedia(VideoPath);
            player.URL = VideoPath;
            player.uiMode = "none";
            player.BringToFront();
        }
    }
}
