using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：产品DAL类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public class ProductModuleDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// 方法说明：根据父文件夹获取子文件夹数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="parentFolder">父文件夹</param>
        /// <returns>DataSet</returns>
        public static DataSet GetProductModuleDataset(string parentFolder)
        {
            try
            {
                string strsql = @"select FolderId,FolderCode,FolderName,ParentFolder from MAT_Folder a 
                                    where a.ParentFolder is null  --父文件夹ID
                                    order by a.FolderCode, a.FolderName";
                if (!string.IsNullOrEmpty(parentFolder))
                {
                    strsql = @"select FolderId,FolderCode,FolderName,ParentFolder from MAT_Folder a 
                                    where a.ParentFolder = @ParentFolder --父文件夹ID
                                    order by a.FolderCode, a.FolderName";
                }

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    if (!string.IsNullOrEmpty(parentFolder))
                    {
                        db.AddInParameter(cmd, "@ParentFolder", DbType.String, parentFolder);
                    }
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// 方法说明：根据文件夹ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// <param name="folderId">文件夹ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetMaterialListByFolderId(string folderId)
        {
            try
            {
                string strsql = @"select iseffect,baseid,lastarchive,technicsstateiconpath,intimage,qualitystateiconpath,
                                    materialverid,objecticonpath,disginstateiconpath,microimage,name,code,
                                    spec,model,drawnumber,creator,vercode,isrohs,
                                    count,fullpath,categoryname,memberspec,IsCreateNew,ChangingApply 
                                    from v_mat_product where IsShow=2  and FolderId = @FolderId
                                    and ArticleType = 0  and FactoryId = ''  And  LanguageId = 0";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@FolderId", DbType.String, folderId);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        /// 方法说明：获取PBOM文件夹数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="parentFolderId">父文件夹ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetPBomFolderDataset(string parentFolderId)
        {
            try
            {
                string strsql = @"select FolderId,FolderCode,FolderName from PP_Folder where ParentFolderId is null";
                if (!string.IsNullOrEmpty(parentFolderId))
                {
                    strsql = @"select FolderId,FolderCode,FolderName from PP_Folder where ParentFolderId=@ParentFolderId";
                }
                
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    if (!string.IsNullOrEmpty(parentFolderId))
                    {
                        db.AddInParameter(cmd, "@ParentFolderId", DbType.String, parentFolderId);
                    }
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// 方法说明：根据PBOM文件夹ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// <param name="folderId">文件夹ID</param>
        /// <returns>物料列表</returns>
        public static DataSet GetMaterialBomByFolderId(string folderId)
        {
            try
            {
                string strsql = @"select code,name,groupid,currentver,t1.creator,t1.createdate,
                                    t1.updateperson,t1.updatedate,status,t1.remark,objecticonpath,
                                    disginstateiconpath from PP_PBOM t1
                                    left join Mat_MaterialVersion t2 on t1.ObjectId = t2.BaseId
                                    left join PP_Folder t3 on t1.FolderId = t3.FolderId
                                    where t3.FolderId=@FolderId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@FolderId", DbType.String, folderId);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：PBOM文件夹新增
        /// 作    者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="product">产品文件夹实体</param>
        /// <returns>Guid</returns>
        public static Guid InsertPBomFolder(Kingdee.CAPP.Model.ProductModule product)
        {
            string strsql = @"INSERT INTO [dbo].[PP_Folder]
                                    ([FolderId]
                                   ,[FolderCode]
                                   ,[FolderName]
                                   ,[ParentFolderId]
                                   ,[Creator]
                                   ,[CreateDate]
                                   ,[Updater]
                                   ,[UpdateDate]
                                   ,[DisplaySeq]
                                   ,[ChildCount]
                                   ,[Remark])
                             VALUES
                                   (
                                    @FolderId
                                   ,@FolderCode
                                   ,@FolderName
                                   ,@ParentFolderId
                                   ,@Creator
                                   ,@CreateDate
                                   ,@Updater
                                   ,@UpdateDate
                                   ,@DisplaySeq
                                   ,@ChildCount
                                   ,@Remark)";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@FolderId", DbType.Guid, id);
                db.AddInParameter(cmd, "@FolderCode", DbType.String, product.FolderCode);
                db.AddInParameter(cmd, "@FolderName", DbType.String, product.FolderName);
                db.AddInParameter(cmd, "@ParentFolderId", DbType.String, product.ParentFolderId);
                db.AddInParameter(cmd, "@Creator", DbType.String, product.Createtor);
                db.AddInParameter(cmd, "@CreateDate", DbType.String, DateTime.Now.ToShortDateString());
                db.AddInParameter(cmd, "@Updater", DbType.String, product.Updater);
                db.AddInParameter(cmd, "@UpdateDate", DbType.String, DateTime.Now.ToShortDateString());
                db.AddInParameter(cmd, "@DisplaySeq", DbType.Int16, product.DisplaySeq);
                db.AddInParameter(cmd, "@ChildCount", DbType.Int16, product.ChildCount);
                db.AddInParameter(cmd, "@Remark", DbType.String, product.Remark);

                db.ExecuteScalar(cmd);
                return id;
            }
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
            int result = 0;
            string strsql = @"delete from [dbo].[PP_Folder] where FolderId=@FolderId";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = new Guid(folderid);
                db.AddInParameter(cmd, "@FolderId", DbType.Guid, id);
                result = db.ExecuteNonQuery(cmd);
            }

            return result > 0;
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
            string strsql = @"insert into pp_pbom(
                                    PbomId,
                                    FolderId,
                                    CategoryId,
                                    ObjectId,
                                    FactoryId,
                                    GroupId,
                                    Remark,
                                    Status,
                                    CurrentVer,
                                    ChangeVer,
                                    IntegrationMode,
                                    ERPBOMNumber,
                                    Creator,
                                    CreateDate,
                                    ArchivePerson,
                                    ArchiveDate,
                                    UnArchivePerson,
                                    UnArchiveDate,
                                    UpdatePerson,
                                    UpdateDate)
                                    values (
                                    @PbomId,
                                    @FolderId,
                                    @CategoryId,
                                    @ObjectId,
                                    @FactoryId,
                                    @GroupId,
                                    @Remark,
                                    @Status,
                                    @CurrentVer,
                                    @ChangeVer,
                                    @IntegrationMode,
                                    @ERPBOMNumber,
                                    @Creator,
                                    @CreateDate,
                                    @ArchivePerson,
                                    @ArchiveDate,
                                    @UnArchivePerson,
                                    @UnArchiveDate,
                                    @UpdatePerson,
                                    @UpdateDate)";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@PbomId", DbType.Guid, id);
                db.AddInParameter(cmd, "@FolderId", DbType.String, folderid);
                db.AddInParameter(cmd, "@CategoryId", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@ObjectId", DbType.String, baseid);
                db.AddInParameter(cmd, "@FactoryId", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@GroupId", DbType.String, groupid);
                db.AddInParameter(cmd, "@Remark", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@Status", DbType.String, "1");
                db.AddInParameter(cmd, "@CurrentVer", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@ChangeVer", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@IntegrationMode", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@ERPBOMNumber", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@Creator", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@CreateDate", DbType.String, DateTime.Now.ToShortDateString());
                db.AddInParameter(cmd, "@ArchivePerson", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@ArchiveDate", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@UnArchivePerson", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@UnArchiveDate", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@UpdatePerson", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@UpdateDate", DbType.String, DateTime.Now.ToShortDateString());

                int result = db.ExecuteNonQuery(cmd);

                if (result > 0)
                {
                    Guid gid = InsertPBomVer(id);
                }

                return id;
            }
        }

        /// <summary>
        /// 方法说明：新增PBom是同时需要增加PBomVersion
        /// 作者：jason.tang
        /// 完成时间：2013-03-14
        /// </summary>
        /// <param name="pbomid">PBOM主键值</param>
        /// <returns>Guid</returns>
        private static Guid InsertPBomVer(Guid pbomid)
        {
            string strsql = @"insert into pp_pbomver(
                                    VerId,
                                    PbomId,
                                    Ver,
                                    Remark,
                                    Status,
                                    Creator,
                                    CreateDate,
                                    UpdatePerson,
                                    UpdateDate)
                                    values (
                                    @VerId,
                                    @PbomId,
                                    @Ver,
                                    @Remark,
                                    @Status,
                                    @Creator,
                                    @CreateDate,
                                    @UpdatePerson,
                                    @UpdateDate)";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@VerId", DbType.Guid, id);
                db.AddInParameter(cmd, "@PbomId", DbType.Guid, pbomid);                
                db.AddInParameter(cmd, "@Ver", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@Remark", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@Status", DbType.String, "1");
                db.AddInParameter(cmd, "@Creator", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@CreateDate", DbType.String, DateTime.Now.ToShortDateString());                
                db.AddInParameter(cmd, "@UpdatePerson", DbType.String, string.Empty);
                db.AddInParameter(cmd, "@UpdateDate", DbType.String, DateTime.Now.ToShortDateString());

                db.ExecuteScalar(cmd);
                return id;
            }

        }
    }
}
