using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
using Kingdee.CAPP.Common;

namespace Kingdee.CAPP.UI
{
    /// <summary>
    /// 类型说明：一个模拟委托窗体，用来暴露属性设置窗体
    /// 作    者：jason.tang
    /// 完成时间：2013-01-08
    /// </summary>
    public class DelegateForm:IDisposable
    {
        public static PropertiesNavigate propertyForm { get; set; }
        //public static Dictionary<string, object> dicObjects { get; set; }
        //public static Dictionary<string, object> dicForm { get; set; }

        public void Dispose()
        {
            this.Dispose();
        }
    }

    public partial class PropertiesNavigate : DockContent
    {
        public PropertiesNavigate()
        {
            DelegateForm.propertyForm = this;
            InitializeComponent();
        }
    }
}
