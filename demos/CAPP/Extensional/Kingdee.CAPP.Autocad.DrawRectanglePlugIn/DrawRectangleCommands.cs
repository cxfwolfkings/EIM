// (C) Copyright 2013 by Kingdee 
//
using System;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using System.IO;
using System.Windows;
using cadApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.Colors;
using System.Threading;
using Autodesk.AutoCAD.Interop;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
/***************************
 * create by : franco zhan
 * create date : 2013-3-26
 ***************************/

// This line is not mandatory, but improves loading performances
[assembly: CommandClass(typeof(Kingdee.CAPP.Autocad.DrawRectanglePlugIn.DrawRectangleCommands))]

namespace Kingdee.CAPP.Autocad.DrawRectanglePlugIn
{

    // This class is instantiated by AutoCAD for each document when
    // a command is called by the user the first time in the context
    // of a given document. In other words, non static data in this class
    // is implicitly per-document!
    public class DrawRectangleCommands
    {
        // The CommandMethod attribute can be applied to any public  member 
        // function of any public class.
        // The function should take no arguments and return nothing.
        // If the method is an intance member then the enclosing class is 
        // intantiated for each document. If the member is a static member then
        // the enclosing class is NOT intantiated.
        //
        // NOTE: CommandMethod has overloads where you can provide helpid and
        // context menu.

        // Modal Command with localized name

        const string dwgExtesion = ".dwg";
        const string cutDwgExtesion = "_cut.dwg";
        const string orgDwgextesion = "_org.dwg";

        [CommandMethod("MyGroup", "DrawRectangle", "MyCommandLocal", CommandFlags.Modal)]
        public void DrawRectangleCommand() // This method can have any name
        {
            Editor ed = cadApplication.DocumentManager.MdiActiveDocument.Editor;


            Document document = cadApplication.DocumentManager.MdiActiveDocument;

            string[] args = Environment.GetCommandLineArgs();

            /// args 参数为3个: 
            ///  args[0] 为文件FullPath
            ///  args[1] 为文件的简图的路径
            ///  args[2] 为简图的Width
            ///  args[3] 为简图的Height
            ///  args[4] 是否已经生成工艺简图
            ///  

            //foreach (var arg in args)
            //{
            //    ed.WriteMessage(arg);
            //}
            if (args.Length < 5)
            {
                return;
            }

            bool isAlreadyGeneratePic = false;

            if (!bool.TryParse(args[4], out isAlreadyGeneratePic))
            {
                cadApplication.ShowAlertDialog("是否已经生成工艺简图参数传递错误！");
                return;
            }

            if (isAlreadyGeneratePic)
            {
                /// 如果是已经生成工艺简图的卡片，就不用设置框；
                return;
            }

            double width = 100;
            double height = 100;

            if (!double.TryParse(args[2], out width)
                    || !double.TryParse(args[3], out height))
            {
                cadApplication.ShowAlertDialog("参数传递错误！");
                return;
            }

            Environment.SetEnvironmentVariable("swidth", width.ToString());
            Environment.SetEnvironmentVariable("sheight", height.ToString());

            ViewTableRecord vtr = ed.GetCurrentView();
            Point2d center = vtr.CenterPoint;
            //ed.WriteMessage("整个文档的宽度和高度 x: " + vtr.Width + " Y: " + vtr.Height);
            ///中心的纵坐标
            double y = vtr.CenterPoint.Y - height / 2;
            ///中心的横坐标
            double x = vtr.CenterPoint.X - width / 2;

            ///线条的宽度
            double lineWidth = 1;

            Point2dCollection pList = new Point2dCollection();
            pList.Add(new Point2d(x, y));
            //ed.WriteMessage("原点坐标 x: " + x + " Y: " + y );

            pList.Add(new Point2d(x, y + height));
            //ed.WriteMessage("Y轴坐标 x: " + x + " Y: " + y + height );

            pList.Add(new Point2d(x + width, y + height));
            //ed.WriteMessage("对角点轴坐标 x: " + x + width + " Y: " + y + height );

            pList.Add(new Point2d(x + width, y));
            //ed.WriteMessage("X轴坐标 x: " + x + width + " Y: " + y );

            pList.Add(new Point2d(x - lineWidth / 2, y));
            //ed.WriteMessage("原点坐标 x: " + x + " Y: " + y);

            Polyline pl = AutoCADHelper.DrawPolyline(pList, lineWidth);
            pl.Color = Autodesk.AutoCAD.Colors.Color.FromColor(System.Drawing.Color.Yellow);

            Database db = HostApplicationServices.WorkingDatabase;

            AutoCADHelper.SaveModel(pl, db, "redId");

        }

        [CommandMethod("MyGroup", "NewOpen", "MyCommandLocal4", CommandFlags.Modal)]
        public static void NewOpen()
        {
            /// 简图路径
            string fileName = Environment.GetEnvironmentVariable("fileName");

            DocumentCollection acDocMgr = cadApplication.DocumentManager;

            string newfileName = fileName.Replace(dwgExtesion, orgDwgextesion);
            if (File.Exists(newfileName))
            {
                acDocMgr.Open(newfileName);
            }
            else
            {
                acDocMgr.Open(fileName);
            }
        }
        [CommandMethod("MyGroup", "Cutout", "MyCommandLocal2", CommandFlags.Modal)]
        public static void Cutout()
        {
            /// 简图路径
            string cutFileName = Environment
                .GetEnvironmentVariable("fileName").Replace(dwgExtesion, cutDwgExtesion);

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 5)
            {
                MessageBox.Show("参数传递错误");
                return;
            }


            double width = 100;
            double height = 100;

            string swidth = args[2];
            string sheight = args[3];


            if (!double.TryParse(swidth, out width)
                    || !double.TryParse(sheight, out height))
            {
                cadApplication.ShowAlertDialog("参数传递错误！");
                return;
            }


            Document doc = cadApplication.DocumentManager.MdiActiveDocument;

            Editor ed = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor;
            Autodesk.AutoCAD.Interop.AcadPreferences pref
                    = Autodesk.AutoCAD.ApplicationServices.Application.Preferences as Autodesk.AutoCAD.Interop.AcadPreferences;
            try
            {
                pref.Display.GraphicsWinModelBackgrndColor = (uint)ColorTranslator.ToWin32(System.Drawing.Color.White);
                
            }
            catch
            {
            }

            Bitmap map = doc.CapturePreviewImage((UInt32)width, (UInt32)height);
            map.Save(cutFileName.Replace(dwgExtesion, ".png"));

            Database db = HostApplicationServices.WorkingDatabase;
            db.SaveAs(cutFileName, DwgVersion.Current);

            try
            {
                pref.Display.GraphicsWinModelBackgrndColor = (uint)ColorTranslator.ToWin32(System.Drawing.Color.FromArgb(33, 40, 48));
            }
            catch
            {
            }

            #region 图片进行剪切
            //Bitmap bitmap = new Bitmap(ImageWidth, num, PixelFormat.Format32bppArgb); 
            //Graphics g = Graphics.FromImage(map);
            //g.Clear(System.Drawing.Color.Transparent);
            ////在指定位置并且按指定大小绘制 原图片 对象 
            //g.DrawImage(map, new Rectangle(0, 0, map.Width, map.Height));
            //map.Dispose();

            //try
            //{
            //    //将此 原图片 以指定格式并用指定的编解码参数保存到指定文件 

            //    string savepath = fileName.Replace(".dwg", ".png");
            //    (bitmap, HttpContext.Current.Server.MapPath(savepath), GetCodecInfo((string)htmimes[sExt]));
            //}
            //catch (System.Exception e)
            //{
            //    throw e;
            //}
            //finally
            //{
            //    bitmap.Dispose();
            //    g.Dispose();
            //} 
            #endregion
        }

        [CommandMethod("MyGroup", "ScreenShot", "MyCommandLocal1", CommandFlags.Modal)]
        public void ScreenShotCommand() // This method can have any name
        {
            DocumentCollection acDocMgr = cadApplication.DocumentManager;
            Document document = cadApplication.DocumentManager.MdiActiveDocument;


            string fileName = string.Empty;
            if (acDocMgr.Count > 1)
            {
                MessageBox.Show("已经有编辑的源文件在编辑,请手动关闭当前编辑的文件！");
                return;
            }
            else if (acDocMgr.Count == 1)
            {
                string[] args = Environment.GetCommandLineArgs();

                if (args.Length < 5)
                {
                    cadApplication.ShowAlertDialog("参数传递错误！");
                    return;
                }

                fileName = args[1];
                Environment.SetEnvironmentVariable("fileName", fileName);


                /// 如果是只有一个文档的话，且为源文件的话
                string curDocName = document.Name;

                if (curDocName.Contains(fileName))
                {

                    if (File.Exists(curDocName)
                        && curDocName.IndexOf(orgDwgextesion) > -1)
                    {
                        string fName = curDocName.Replace(orgDwgextesion, dwgExtesion);
                        if (File.Exists(fName))
                        {
                            File.Delete(fName);
                        }
                    }
                    else if (File.Exists(curDocName)
                        && curDocName.IndexOf(orgDwgextesion) < 0)
                    {
                        string fName = curDocName.Replace(dwgExtesion, orgDwgextesion);
                        if (File.Exists(fName))
                        {
                            File.Delete(fName);
                        }
                    }
                }
            }
            string newFileName = fileName.Replace(dwgExtesion, orgDwgextesion);

            Database db = HostApplicationServices.WorkingDatabase;
            Editor ed = cadApplication.DocumentManager.MdiActiveDocument.Editor;


            /// 当前最新位置的Polyline
            ObjectId objId = new ObjectId();
            Polyline pl = (Polyline)AutoCADHelper.GetObjFromDic(ref objId, "redId", db, ed);

            if (pl == null)
            {
                return;
            }

            ///保留剪切前的文件
            if (!File.Exists(fileName))
            {
                db.SaveAs(fileName, DwgVersion.Current);
            }
            else
            {
                db.SaveAs(newFileName, DwgVersion.Current);
            }
            //MessageBox.Show(newFileName);
            ///实行剪切操作
            AutoCADHelper.TrimMap(pl);

            /// 矩形右上角的点
            Point2d pThird = pl.GetPoint2dAt(2);
            /// 矩形左下角的点
            Point2d pFirst = pl.GetPoint2dAt(0);



            PromptSelectionResult result = ed.SelectCrossingWindow(
                new Point3d(pFirst.X + 10, pFirst.Y + 10, 0),
                new Point3d(pThird.X - 10, pThird.Y - 10, 0));


            if (result.Status == PromptStatus.Error)
            {
                cadApplication.ShowAlertDialog("没有选中坐标点！");
            }
            else if (result.Status == PromptStatus.OK)
            {
                ///剪切一些没有剪切干净的图形
                ///选中所有的实体对象
                PromptSelectionResult resultAll = ed.SelectAll();

                using (Transaction trans = document.TransactionManager.StartTransaction())
                {
                    ObjectId[] sIds = result.Value.GetObjectIds();
                    List<ObjectId> oIds = sIds.ToList<ObjectId>();
                    foreach (ObjectId pid in resultAll.Value.GetObjectIds())
                    {
                        if (!oIds.Contains(pid))
                        {
                            Entity ent = trans.GetObject(pid, OpenMode.ForWrite) as Entity;
                            if (ent == null) continue;
                            ent.Erase();
                        }
                    }
                    trans.Commit();
                }
                //if (!File.Exists(fileName))
                //{
                //    db.SaveAs(fileName, DwgVersion.Current);
                //}
                //else
                //{
                //    CADTools.RunCommand(true, "_qsave", "");
                //}
            }
        }

        // Modal Command with pickfirst selection
        [CommandMethod("MyGroup", "MyPickFirst", "MyPickFirstLocal", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public void MyPickFirst() // This method can have any name
        {
            PromptSelectionResult result = cadApplication.DocumentManager.MdiActiveDocument.Editor.GetSelection();
            if (result.Status == PromptStatus.OK)
            {
                // There are selected entities
                // Put your command using pickfirst set code here
            }
            else
            {
                // There are no selected entities
                // Put your command code here
            }
        }

        // Application Session Command with localized name
        [CommandMethod("MyGroup", "MySessionCmd", "MySessionCmdLocal", CommandFlags.Modal | CommandFlags.Session)]
        public void MySessionCmd() // This method can have any name
        {
            // Put your command code here
        }

        // LispFunction is similar to CommandMethod but it creates a lisp 
        // callable function. Many return types are supported not just string
        // or integer.
        [LispFunction("MyLispFunction", "MyLispFunctionLocal")]
        public int MyLispFunction(ResultBuffer args) // This method can have any name
        {
            // Put your command code here

            // Return a value to the AutoCAD Lisp Interpreter
            return 1;
        }

    }

}
