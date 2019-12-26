using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;
using Kingdee.CAPP.IPlugIn;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml.Linq;

/*******************************
 * Created By franco
 * Description: add menu item
 *******************************/

namespace Kingdee.CAPP.UI.Plug_In
{
    public class AddMenuItem
    {
        #region add menu
        /// <summary>
        /// add menu
        /// </summary>
        public void AddMenu(MenuStrip menuStrip,MainFrm frm, DockPanel dockpanel)
        {
            string pluginPath = Application.StartupPath + @"\Plug-In\CappExstention.xml";
            

            XmlDocument document = new XmlDocument();
            if (!File.Exists(pluginPath))
            {

                XmlDeclaration declaration = document.CreateXmlDeclaration("1.0", "utf-8", null);
                document.AppendChild(declaration);

                XmlElement root = document.CreateElement("root");
                document.AppendChild(root);
                document.Save(pluginPath);
            }
            else
            {
                document.Load(pluginPath);

                /// find need load menu
                XmlNodeList nodeList = document.SelectNodes("/root/menu");

                bool isHasToolStripMenuItem = false;
                string currentMenuName = string.Empty;
                foreach (XmlNode xn in nodeList)
                {
                    string menumName = xn.Attributes["name"].Value;

                    ///judge is already contain item
                    foreach (ToolStripMenuItem ts in menuStrip.Items)
                    {
                        if (menumName.Trim() == ts.Text.Trim())
                        {
                            isHasToolStripMenuItem = true;
                            currentMenuName = ts.Name;
                            break;
                        }
                    }

                    /// system menu whether has the toolstripmenuitem
                    if (isHasToolStripMenuItem)
                    {
                        ToolStripMenuItem currentMenu = (ToolStripMenuItem)menuStrip.Items[currentMenuName];

                        foreach (XmlNode xnc in xn.ChildNodes)
                        {
                            ToolStripMenuItem ts = new ToolStripMenuItem(xnc.Attributes["name"].Value);
                            ts.Tag = xnc.Attributes["assemblyName"].Value;
                            ts.Click += new EventHandler(frm.ts_Click);
                            currentMenu.DropDownItems.Add(ts);
                        }
                        /// init ToolStripMenuItem status
                        isHasToolStripMenuItem = false;
                    }
                    /// new add system menu
                    else
                    {
                        int menuPosition = 0;

                        if (isHasAttribute("menuPosition", xn))
                        {
                            int.TryParse(xn.Attributes["menuPosition"].Value, out menuPosition);
                        }

                        ToolStripMenuItem ts = new ToolStripMenuItem(menumName);
                        if (isHasAttribute("assemblyName", xn))
                        {
                            ts.Tag = xn.Attributes["assemblyName"].Value;
                        }
                        if (menuPosition > 0 && menuPosition < menuStrip.Items.Count - 1)
                        {
                            menuStrip.Items.Insert(menuPosition, ts);
                        }
                        else
                        {
                            menuStrip.Items.Add(ts);
                        }


                        foreach (XmlNode xnc in xn.ChildNodes)
                        {
                            if (!isHasAttribute("name", xnc)) break;
                            ToolStripMenuItem tsc = new ToolStripMenuItem(xnc.Attributes["name"].Value);

                            if (isHasAttribute("assemblyName", xnc))
                            {
                                tsc.Tag = xnc.Attributes["assemblyName"].Value;
                            }
                            tsc.Click += new EventHandler(frm.ts_Click);
                            ts.DropDownItems.Add(tsc);
                        }
                    }
                }
            }

        }

        bool isHasAttribute(string attributeName,XmlNode node)
        {
            bool isHasAttri = false;
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name == attributeName)
                {
                    isHasAttri = true;
                    break;
                }
            }
            return isHasAttri;
        }       
        #endregion
    }
}
