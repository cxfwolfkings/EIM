using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Kingdee.CAPP.IPlugIn;
using WeifenLuo.WinFormsUI.Docking;
using Kingdee.CAPP.Componect;
using System.Diagnostics;

namespace Kingdee.CAPP.UI.Test
{
    public partial class TestFrm : BaseForm,IplugIn
    {
        public TestFrm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
          textBox1.Text = new Random().Next(1, 100).ToString();
        }

        public void FormShow(Form mainfrm,DockPanel dockPanel)
        {
            this.MdiParent = mainfrm;
            this.Show(dockPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Process.Start(@"D:\Program Files\Autodesk\AutoCAD 2012 - Simplified Chinese\acad.exe", FileName);
            string startfileName = @"D:\Program Files\Autodesk\AutoCAD 2012 - Simplified Chinese\acad.exe";
            string fileName = "D;\\aa.dwg";
            string width = "100";
            string height = "200";

            Process.Start(startfileName, string.Format("{0} {1} {2}",fileName,width,height));
        }
    }
}
