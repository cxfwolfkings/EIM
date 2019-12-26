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
    /// 类型说明：物料BLL类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public class MaterialModuleBLL
    {
        private static string server;
        private static string user;
        private static string pass;

        /// <summary>
        /// 方法说明：获取物料类型表数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <returns>物料实体集合</returns>      
        public static List<MaterialModule> GetMaterialModuleList()
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetMaterialModuleDataset().Tables[0];

                var cMaterialModuleList = (from c in dt.AsEnumerable()
                                                  select new MaterialModule()
                                                  {
                                                      TypeId = c.Field<string>("TypeID"),   
                                                      TypeName = c.Field<string>("TypeName")
                                                  }).ToList<MaterialModule>();
                return cMaterialModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据物料类型获取物料分类表数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <returns>物料分类实体集合</returns>
        public static List<BusinessCategoryModule> GetCategoryModuleListByType(string commonType)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetCategoryByCommonType(commonType).Tables[0];

                var cCategoryModuleList = (from c in dt.AsEnumerable()
                                           select new BusinessCategoryModule()
                                           {
                                               CategoryId = c.Field<string>("CategoryId"),
                                               CategoryCode = c.Field<string>("CategoryCode"),
                                               CategoryName = c.Field<string>("CategoryName"),
                                               DisplaySeq = c.Field<int>("DisplaySeq")
                                           }).ToList<BusinessCategoryModule>();
                return cCategoryModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据物料版本ID查找子物料
        /// 作    者：jason.tang
        /// 完成时间：2013-03-08
        /// </summary>
        /// <param name="materialVerId">物料版本ID</param>
        /// <returns></returns>
        public static List<MaterialVersionModule> GetChildMaterialByVersionId(string materialVerId)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetChildMaterialByVersionId(materialVerId).Tables[0];

                var cVersionModuleList = (from c in dt.AsEnumerable()
                                           select new MaterialVersionModule()
                                           {
                                               MaterialVerId = c.Field<string>("MaterialVerId"),
                                               Code = c.Field<string>("Code"),
                                               Name = c.Field<string>("Name"),
                                               BaseId = c.Field<string>("BaseId")
                                           }).ToList<MaterialVersionModule>();
                return cVersionModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据物料分类父ID获取物料子分类数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <returns>物料分类实体集合</returns>
        public static List<BusinessCategoryModule> GetCategoryModuleListByParentId(string parentCategory)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetCategoryByParentCategory(parentCategory).Tables[0];

                var cCategoryModuleList = (from c in dt.AsEnumerable()
                                           select new BusinessCategoryModule()
                                           {
                                               CategoryId = c.Field<string>("CategoryId"),
                                               CategoryCode = c.Field<string>("CategoryCode"),
                                               CategoryName = c.Field<string>("CategoryName"),
                                               DisplaySeq = c.Field<int>("DisplaySeq")
                                           }).ToList<BusinessCategoryModule>();
                return cCategoryModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// 方法说明：根据物料分类ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// <param name="typeId">分类ID</param>
        /// <param name="categoryId">业务类型ID</param>
        /// <param name="conditions">其他条件</param>
        /// <returns>物料列表</returns>
        public static DataTable GetMaterialModuleDataByCategoryId(string typeId, string categoryId, string conditions)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetMaterialModuleDataByCategoryId(typeId, categoryId, conditions).Tables[0];

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// 方法说明：根据物料分类ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// <param name="categoryId">分类ID</param>
        /// <param name="code">物料编码</param>
        /// <returns>物料列表</returns>
        public static List<MaterialModule> GetMaterialModuleListByCategoryId(string categoryId, string code)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetMaterialModuleListByCategoryId(categoryId, code).Tables[0];

                var cMaterialModuleList = (from c in dt.AsEnumerable()
                                           select new MaterialModule()
                                           {
                                               categoryname = c.Field<string>("categoryname"),
                                               code = c.Field<string>("code"),
                                               count = c.Field<int>("count"),
                                               createdate = c.Field<string>("createdate"),
                                               drawnumber = c.Field<string>("drawnumber"),
                                               intproductmode = c.Field<string>("intproductmode"),
                                               //isvirtualdesign = c.Field<string>("isvirtualdesign"),                                               
                                               memberspec = c.Field<string>("memberspec"),
                                               name = c.Field<string>("name"),
                                               papercount = c.Field<int>("papercount"),
                                               productname = c.Field<string>("productname"),
                                               spec = c.Field<string>("spec"),
                                               TypeId = c.Field<string>("TypeId"),
                                               typename = c.Field<string>("typename"),
                                               vercode = c.Field<string>("vercode"),        
                                                materialverid = c.Field<string>("materialverid"),
                                                baseid = c.Field<string>("baseid")
                                           }).ToList<MaterialModule>();
                return cMaterialModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：获取物料版本数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <returns>物料版本结合</returns>
        public static List<MaterialVersionModule> GetMaterialVersionModuleList()
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetMaterialVersionModuleList().Tables[0];

                var cVersionModuleList = (from c in dt.AsEnumerable()
                                           select new MaterialVersionModule()
                                           {
                                               MaterialVerId = c.Field<string>("MaterialVerId"),
                                               BaseId = c.Field<string>("BaseId"),
                                               Code = c.Field<string>("Code"),
                                               Name = c.Field<string>("Name"),
                                               MaterialType = c.Field<int>("MaterialType"),
                                               ProductId = c.Field<string>("ProductId"),
                                               ObjectIconPath = c.Field<string>("ObjectIconPath")
                                           }).ToList<MaterialVersionModule>();
                return cVersionModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据PBOM ID获取下层对应的物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-08-22
        /// </summary>
        /// <param name="verid">Ver ID</param>
        /// <returns>物料版本数据集</returns>
        public static List<MaterialVersionModule> GetChildPbomMaterialByPbomId(string verid)
        {
            DataTable dt = MaterialModuleDAL
                    .GetChildPbomMaterialByPbomId(verid).Tables[0];

            var cVersionModuleList = (from c in dt.AsEnumerable()
                                      select new MaterialVersionModule()
                                      {
                                          VerId = c.Field<string>("VerId"),
                                          ChildId = c.Field<string>("ChildId"),
                                          MaterialVerId = c.Field<string>("MaterialVerId"),
                                          BaseId = c.Field<string>("BaseId"),
                                          Code = c.Field<string>("Code"),
                                          Name = c.Field<string>("Name"),
                                          MaterialType = c.Field<int>("MaterialType"),
                                          ProductId = c.Field<string>("ProductId"),
                                          ObjectIconPath = c.Field<string>("ObjectIconPath")
                                      }).ToList<MaterialVersionModule>();
            return cVersionModuleList;
        }

        /// <summary>
        /// 方法说明：根据父版本ID获取物料版本数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="versionId">版本ID</param>
        /// <returns>物料版本结合</returns>
        public static List<MaterialRelationModule> GetMaterialVersionModuleListByVersionId(string versionId)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetMaterialVersionModuleListByVersionId(versionId).Tables[0];

                var cRelationModuleList = (from c in dt.AsEnumerable()
                                          select new MaterialRelationModule()
                                          {
                                              ParentVerId = c.Field<string>("ParentVerId"),
                                              RelationId = c.Field<string>("RelationId"),
                                              ChildVerId = c.Field<string>("ChildVerId"),
                                              DisplaySeq = c.Field<int>("DisplaySeq"),
                                              IsLock = c.Field<bool>("IsLock"),
                                              name = c.Field<string>("name")
                                          }).ToList<MaterialRelationModule>();
                return cRelationModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据BaseId获取PBOM资料
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="baseId">BaseId</param>
        /// <returns></returns>
        public static List<PBomModule> GetPBomModuleListByBaseId(string baseId)
        {
            try
            {
                DataTable dt = MaterialModuleDAL
                    .GetPBomModuleListByBaseId(baseId).Tables[0];

                var cPbomModuleList = (from c in dt.AsEnumerable()
                                       select new PBomModule()
                                           {
                                               VerId = c.Field<string>("VerId"),
                                               PbomId = c.Field<string>("PbomId"),
                                               FolderName = c.Field<string>("FolderName"),
                                               FolderId = c.Field<string>("FolderId"),
                                               Creator = c.Field<string>("Creator"),
                                               CreateDate = c.Field<string>("CreateDate"),
                                               ArchivePerson = c.Field<string>("ArchivePerson"),
                                               ArchiveDate = c.Field<string>("ArchiveDate")
                                           }).ToList<PBomModule>();
                return cPbomModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据PBomId获取对应的工序
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="pbomid">PBOM ID</param>
        /// <returns></returns>
        public static DataTable GetOperByPBomId(string pbomid)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = MaterialModuleDAL.GetOperByPBomId(pbomid);
                dt = ds.Tables[0];
            }
            catch
            {
                return null;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：根据PBomId获取对应的工艺路线
        /// 作      者：jason.tang
        /// 完成时间：2013-08-27
        /// </summary>
        /// <param name="pbomid">PBOM ID</param>
        /// <returns></returns>
        public static DataTable GetRoutingByPBomId(string pbomid)
        {
            DataTable dt = null;
            try
            {
                DataSet ds = MaterialModuleDAL.GetRoutingByPBomId(pbomid);
                dt = ds.Tables[0];
            }
            catch
            {
                return null;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：根据物料版本ID获取相关对象
        /// 作   者：jason.tang
        /// 完成时间：2013-09-13
        /// </summary>
        /// <param name="verId">版本ID</param>
        /// <returns></returns>
        public static DataTable GetMaterialObjectByVerId(string verId)
        {
            DataTable dt = null;

            try
            {
                DataSet ds = MaterialModuleDAL.GetMaterialObjectByVerId(verId);
                dt = ds.Tables[0];
            }
            catch
            {
                return null;
            }

            return dt;
        }

        private static Proway.WebFileControl m_FtpControl = null;
        private static Proway.WebFileControl FtpControl
        {
            get
            {
                if (m_FtpControl == null)
                {                  

                    Proway.WebFileControl c = new Proway.WebFileControl();

                    c.FtpServer = server;
                    c.FtpPort = "21";
                    c.FtpUserName = user;
                    c.FtpPassword = pass;
                    c.IsApplyCompress = true;
                    c.FtpConnection();
                    m_FtpControl = c;
                }
                return m_FtpControl;
            }
        }

        /// <summary>
        /// 服务器端上传文件
        /// </summary>
        /// <param name="CopyId"></param>
        /// <param name="localfile"></param>
        /// <returns></returns>
        public static int UploadFile(string CopyId, string localfile, string uploadUrl, string userId, string password)
        {
            server = uploadUrl;
            user = userId;
            pass = password;

            string ftpfile = "Data" + CopyId + ".PDM";
            int Result = FtpControl.UploadFile(ref localfile, ref ftpfile);

            if (Result == 1)
            {
                return Result;
            }
            else
            {
                //throw;
                return 0;
            }
        }

        /// <summary>
        /// 服务器端下载文件
        /// </summary>
        /// <param name="CopyId"></param>
        /// <param name="localfile"></param>
        /// <returns></returns>
        public static int DownloadFile(string CopyId, string localfile, string uploadUrl, string userId, string password)
        {
            server = uploadUrl;
            user = userId;
            pass = password;

            string ftpfile = "Data" + CopyId + ".PDM";
            string FullPath = "";
            int Result = FtpControl.DownloadFile(ref ftpfile, ref localfile, ref FullPath);

            if (Result == 1)
            {
                return Result;
            }
            else
            {
                //throw;
                return 0;
            }
        }

    }
}
