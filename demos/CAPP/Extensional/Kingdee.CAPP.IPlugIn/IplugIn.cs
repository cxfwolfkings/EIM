using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Windows.Forms;
/*******************************
 * Created By franco
 * Description: capp system module plug-in interface
 *******************************/

namespace Kingdee.CAPP.IPlugIn
{
    public interface IplugIn
    {
        void FormShow(Form mainfrm,DockPanel dockPanel);
    }
}
