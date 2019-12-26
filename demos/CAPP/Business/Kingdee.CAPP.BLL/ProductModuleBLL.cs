using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using System.Data;
using Kingdee.CAPP.DAL;

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// 类型说明：产品DAL类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public class ProductModuleBLL
    {
        /// 方法说明：根据父文件夹获取子文件夹数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// <param name="parentFolder">父文件夹</param>
        /// <returns>产品实体集合</returns>
        public static List<ProductModule> GetProductModuleList(string parentFolder)
        {
            try
            {
                DataTable dt = ProductModuleDAL
                    .GetProductModuleDataset(parentFolder).Tables[0];

                var cProductModuleList = (from c in dt.AsEnumerable()
                                           select new ProductModule()
                                           {
                                               FolderId = c.Field<string>("FolderId"),
                                               FolderCode = c.Field<string>("FolderCode"),
                                               FolderName = c.Field<string>("FolderName"),
                                               ParentFolder = c.Field<string>("ParentFolder")
                                           }).ToList<ProductModule>();
                return cProductModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// 方法说明：根据文件夹ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// <param name="folderId">文件夹ID</param>
        /// <returns>物料列表</returns>
        public static DataTable GetMaterialListByFolderId(string folderId)
        {
            try
            {
                DataTable dt = ProductModuleDAL
                    .GetMaterialListByFolderId(folderId).Tables[0];

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// 方法说明：获取PBom文件夹数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// <param name="parentFolderId">父文件夹ID</param>
        /// <returns>产品实体集合</returns>
        public static List<ProductModule> GetPBomFolderList(string parentFolderId)
        {
            try
            {
                DataTable dt = ProductModuleDAL
                    .GetPBomFolderDataset(parentFolderId).Tables[0];

                var cPBomFolderList = (from c in dt.AsEnumerable()
                                          select new ProductModule()
                                          {
                                              FolderId = c.Field<string>("FolderId"),
                                              FolderCode = c.Field<string>("FolderCode"),
                                              FolderName = c.Field<string>("FolderName")
                                          }).ToList<ProductModule>();
                return cPBomFolderList;
            }
            catch
            {
                throw;
            }
        }

        /// 方法说明：根据PBOM文件夹ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// <param name="folderId">文件夹ID</param>
        /// <returns>物料列表</returns>
        public static DataTable GetMaterialBomByFolderId(string folderId)
        {
            try
            {
                DataTable dt = ProductModuleDAL
                    .GetMaterialBomByFolderId(folderId).Tables[0];

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：PBOM文件夹新增
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="product">产品文件夹实体</param>
        /// <returns>Guid</returns>
        public static Guid InsertPBomFolder(ProductModule product)
        {
            Guid result;
            try
            {
                result = ProductModuleDAL.InsertPBomFolder(product);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：删除文件夹
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="folderid">文件夹ID</param>
        /// <returns>True/False</returns>
        public static bool DeletePBomFolder(string folderid)
        {
            bool result = false;
            try
            {
                result = ProductModuleDAL.DeletePBomFolder(folderid);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：PBOM新增
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="baseid">物料BaseId</param>
        /// <param name="folderid">文件夹ID</param>
        /// <param name="groupid">文件夹组ID</param>
        /// <returns>Guid</returns>
        public static Guid InsertPBom(string baseid, string folderid, string groupid)
        {
            Guid result;
            try
            {
                result = ProductModuleDAL.InsertPBom(baseid, folderid, groupid);
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
