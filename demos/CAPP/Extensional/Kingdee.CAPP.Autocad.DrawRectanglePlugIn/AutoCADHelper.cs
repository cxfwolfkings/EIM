using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using Autodesk.AutoCAD.EditorInput;
using cadApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using DNA;
/***************************
 * create by : franco zhan
 * create date : 2013-3-26
 ***************************/

namespace Kingdee.CAPP.Autocad.DrawRectanglePlugIn
{
    public class AutoCADHelper
    {
        /// <summary>
        /// 绘制多段线
        /// </summary>
        /// <param name="pts">二维点的集合</param>
        /// <param name="width">绘制线段的宽度</param>
        /// <returns>多段线对象</returns>
        public static Polyline DrawPolyline(Point2dCollection pts, double width)
        {
            try
            {
                Polyline ent = new Polyline();
                for (int i = 0; i < pts.Count; i++)
                {
                    ent.AddVertexAt(i, pts[i], 0, width, width);
                }


                return ent;
            }
            catch
            {
                return new Polyline();
            }

        }

        /// <summary> 
        /// 数据库克隆 
        /// </summary> 
        /// <param name="idCollection">克隆的对象ID集合</param> 
        /// <param name="fileName">克隆到的文件名</param> 
        public static void WBClone(ObjectIdCollection idCollection, string fileName)
        {
            Database TargetDb = new Database(true, true); 
            ObjectId IdBtr = new ObjectId(); 
            Database db = idCollection[0].Database; 
            IdMapping Map = new IdMapping();

            using (Transaction trans = TargetDb.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(TargetDb.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead);
                IdBtr = btr.ObjectId;
                trans.Commit();
            }
            db.WblockCloneObjects(idCollection, IdBtr, Map, DuplicateRecordCloning.Replace, false);
            TargetDb.SaveAs(fileName, DwgVersion.Current);
        }

        /// <summary>
        /// 保存数据到字典中
        /// </summary>
        /// <param name="dataobj">待保存的对象</param>
        /// <param name="name">键</param>
        /// <param name="db">数据库</param>
        public static void AddObjToDic(DBObject dataobj, string name, Database db)
        {
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                DBDictionary dbDic = trans.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite) as DBDictionary;
                if (!dbDic.Contains(name))
                {
                    dbDic.SetAt(name, dataobj);
                }
                trans.Commit();
            }
        }

        /// <summary>
        /// 保存对象到数据库
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="db"></param>
        public static Entity SaveModel(Polyline ent, Database db, string dicName)
        {
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForRead);
                BlockTableRecord btr
                    = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                btr.AppendEntity(ent);



                DBDictionary dbDic = trans.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite) as DBDictionary;
                if (!dbDic.Contains(dicName))
                {
                    dbDic.SetAt(dicName, ent);
                }

                ///// 定义图层
                //LayerTable lyrTable = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;
                //string rectLayer = "rectLayer";

                //string curLayer = "curLayer";

                //if (!lyrTable.Has(rectLayer))
                //{
                //    LayerTableRecord lyrTableRec = new LayerTableRecord();
                //    lyrTableRec.Name = rectLayer;

                //    // Upgrade the Layer table for write以写升级打开图层表
                //    lyrTable.UpgradeOpen();

                //    /// 把新层加入到了层库内
                //    lyrTable.Add(lyrTableRec);

                //    lyrTableRec.IsLocked = true;
                //    //lyrTableRec.IsFrozen = false;

                //    trans.AddNewlyCreatedDBObject(lyrTableRec, true);

                //}

                ///// 初始化一个当前操作的图层
                //if (!lyrTable.Has(curLayer))
                //{
                //    LayerTableRecord curLyrTableRec = new LayerTableRecord();
                //    curLyrTableRec.Name = curLayer;
                //    lyrTable.UpgradeOpen();

                //    lyrTable.Add(curLyrTableRec);
                //    curLyrTableRec.IsOff = false;
                //    curLyrTableRec.IsFrozen = false;
                //    curLyrTableRec.IsLocked = false;

                //    trans.AddNewlyCreatedDBObject(curLyrTableRec, true);
                //}

                ///// 设置这个实体属于这个层
                //ent.Layer = rectLayer;

                ///// 设置当前的图层为操作的图层
                //db.Clayer = lyrTable[curLayer];              

                trans.AddNewlyCreatedDBObject(ent, true);
                trans.Commit();

            }
            return ent;
        }

        /// <summary>
        /// 从字典中找对象
        /// </summary>
        /// <param name="keyName">查找的键</param>
        /// <param name="db">数据库</param>
        /// <returns>查找的对象</returns>
        public static DBObject GetObjFromDic(ref ObjectId objId,string keyName, Database db, Editor ed)
        {
            DBObject obj = null;
            DBObject newObj = null;

            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                #region 加层

                //LayerTable lyrTable = trans.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                //LayerTableRecord lyrTableRec
                //    = trans.GetObject(lyrTable["rectLayer"], OpenMode.ForWrite) as LayerTableRecord;


                ///// 解开锁
                //lyrTableRec.IsLocked = false; 
                #endregion

                DBDictionary dbDic = trans.GetObject(db.NamedObjectsDictionaryId, OpenMode.ForWrite) as DBDictionary;
                objId = dbDic.ObjectId;

                if (!dbDic.Contains(keyName))
                {
                    return null;
                }
                obj = trans.GetObject(dbDic.GetAt(keyName), OpenMode.ForWrite);
                if (obj == null)
                {
                    return null;
                }

                newObj = trans.GetObject(obj.Id, OpenMode.ForWrite);

                trans.Commit();
            }
            return newObj;
        }

        /// <summary>
        /// 根据对象ID 得到对象
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="openMode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static Polyline GetObjByObjectId(ObjectId objId, OpenMode openMode, Database db)
        {
            Polyline obj;
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                obj = (Polyline)trans.GetObject(objId, openMode);
                trans.Commit();
            }
            return obj;
        }

        /// <summary>
        /// 在某个点的坐标上插入文字，高度
        /// </summary>
        /// <param name="position"></param>
        /// <param name="content"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static DBText DBText(Point3d position, string content, double height)
        {
            DBText ent = new DBText();
            ent.Position = position;
            ent.TextString = content;
            ent.Height = height;
            return ent;
        }

        /// <summary>
        /// 从当前的编辑区截取图片
        /// </summary>
        /// <param name="wd">cadWindow</param>
        /// <param name="filename">fileName</param>
        /// <param name="top">Top</param>
        /// <param name="bottom">Bottom</param>
        /// <param name="left">Left</param>
        /// <param name="right">Right</param>
        public static void ScreenShotToFile(
            Autodesk.AutoCAD.Windows.Window wd,
            string filename,
            int top,
            int bottom,
            int left,
            int right)
        {
            System.Drawing.Point pt = wd.Location;
            System.Drawing.Size sz = wd.Size;
            pt.X += left;
            pt.Y += top;
            sz.Height -= top + bottom;
            sz.Width -= left + right;

            // Set the bitmap object to the size of the screen

            Bitmap bmp =
              new Bitmap(sz.Width, sz.Height,
                PixelFormat.Format32bppArgb);

            using (bmp)
            {

                // Create a graphics object from the bitmap
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    // Take a screenshot of our window

                    //MessageBox.Show("x:" + pt.X + "y:" + pt.Y);
                    gfx.CopyFromScreen(
                    pt.X, pt.Y, 0, 0, sz,
                                CopyPixelOperation.SourceCopy
                              );

                    // Save the screenshot to the specified location

                    bmp.Save(filename, ImageFormat.Png);
                }

            }

        }

        /// <summary>
        /// 放大矩形
        /// </summary>
        /// <param name="cutBox"></param>
        public static void ZoomRetangle(Polyline cutBox)
        {
            //获取最小与最大点
            Point3d minPoint = cutBox.GeometricExtents.MinPoint;
            Point3d maxPoint = cutBox.GeometricExtents.MaxPoint;

            //设置缩放视口
            CADTools.RunCommand(true, "Zoom", "e",
                new Point3d(minPoint.X - 10, minPoint.Y - 10, 0), new Point3d(maxPoint.X + 10, maxPoint.Y + 10, 0));
        }

        /// <summary>
        /// 剪切矩形区内的图形
        /// </summary>
        /// <param name="cutBox"></param>
        public static void TrimMap(Polyline cutBox)
        {
            //关闭对象捕捉功能避免误删除
            cadApplication.SetSystemVariable("SNAPMODE", 0);
            cadApplication.SetSystemVariable("MODEMACRO", "正在修剪");
            ////获取最小与最大点
            Point3d minPoint = cutBox.GeometricExtents.MinPoint;
            Point3d maxPoint = cutBox.GeometricExtents.MaxPoint;
            ////设置缩放视口
            //设置缩放视口
            //CADTools.RunCommand(true, "Zoom", "e",
            //    new Point3d(minPoint.X - 10, minPoint.Y - 10, 0), new Point3d(maxPoint.X + 10, maxPoint.Y + 10, 0));
            //CADTools.RunCommand(false, "Zoom", "W"
            //    , new Point3d(minPoint.X - 10, minPoint.Y - 10, 0), new Point3d(maxPoint.X + 10, maxPoint.Y + 10, 0));
            //裁剪精度
            Polyline offsetBox = cutBox.GetOffsetCurves(0.2)[0] as Polyline;
            if (offsetBox.Area < cutBox.Area)
            {
                offsetBox = cutBox.GetOffsetCurves(-0.2)[0] as Polyline;
            }
            //裁剪
            for (int i = 0; i < offsetBox.NumberOfVertices; i++)
            {
                Point3d p1 = offsetBox.GetPoint3dAt(i);
                Point3d p2 = new Point3d();
                if (i == offsetBox.NumberOfVertices - 1)
                {
                    p2 = offsetBox.GetPoint3dAt(0);
                }
                else
                {
                    p2 = offsetBox.GetPoint3dAt(i + 1);
                }

                //调用Line命令，由用户结束
                //CADTools.RunCommand(true, "_.line");
                //调用Line命令并结束
                // CADTools.RunCommand(false, "_.line", Point3d.Origin, new Point3d(10, 10, 0));


                //CADTools.RunCommand(false,new ResultTree
                // CADTools.RunCommand(false, params new object[]{});
                //CADTools.RunCommand(true, ".trim"
                //    , cutBox.ObjectId, "", "F", p1, p2, "", "");
            }
            offsetBox.Dispose();
            cadApplication.SetSystemVariable("MODEMACRO", "修剪完成");
        }


    }
}
