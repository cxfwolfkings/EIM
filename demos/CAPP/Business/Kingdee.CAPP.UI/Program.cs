using System;
using System.Windows.Forms;

namespace Kingdee.CAPP.UI
{
    delegate void RegetryFileEventHandler();
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            if (args != null && args.Length > 0)
            {
                string filePath = string.Empty;
                for (int i = 0; i < args.Length; i++)
                {
                    filePath += "" + args[i];
                }

                MainFrm.FilePath = filePath;
            }

            //创建windows用户主题
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (string.IsNullOrEmpty(MainFrm.FilePath))
            {
                var login = new LoginFrm();
                if (login.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainFrm());
                }
            }
            else
            {
                Application.Run(new MainFrm());
            }
        }
    }
}
