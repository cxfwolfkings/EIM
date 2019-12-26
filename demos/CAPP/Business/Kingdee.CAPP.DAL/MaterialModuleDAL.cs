using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：物料DAL类
    /// 作    者：jason.tang
    /// 完成时间：2013-03-05
    /// </summary>
    public class MaterialModuleDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// 方法说明：获取物料类型表数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetMaterialModuleDataset()
        {

            try
            {
                string strsql = @"select 
                               [TypeId]
                               ,[TypeName]                    
                            from [MAT_CommonType]";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    //db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNode);
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
        /// 方法说明：根据物料类型获取物料分类表数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetCategoryByCommonType(string commonType)
        {

            try
            {
                string strsql = @"select CategoryId,CategoryCode,CategoryName,DisplaySeq from ps_BusinessCategory t1 
                                    where DisplaySeq is not null and t1.DeleteFlag =0 and t1.ObjectOption = 1 and t1.CategoryId in 
                                    (select t2.CategoryId from ps_BusinessCategoryRelation t2 where t2.CommonType = @CommonType)  --CommonType条件为上层的类别
                                    and t1.ParentCategory is null  --父类别为Null
                                    Order by t1.CategoryCode,t1.CategoryName";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@CommonType", DbType.String, commonType);
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
        /// 方法说明：根据物料版本ID查找子物料
        /// 作    者：jason.tang
        /// 完成时间：2013-03-08
        /// </summary>
        /// <param name="materialVerId">物料版本ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetChildMaterialByVersionId(string materialVerId)
        {
            try
            {
                string strsql = @"select t1.MaterialVerId,Code,Name,BaseId 
                                    from Mat_MaterialVersion t1 with(nolock) 
                                    left join Mat_MaterialRelation t2 with(nolock) 
                                    on t1.MaterialVerId = t2.ChildVerId 
                                    where ParentVerId = @ParentVerId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentVerId", DbType.String, materialVerId);
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
        /// 方法说明：根据物料分类父ID获取物料子分类数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="parentCategory">父分类ID</param>
        /// <returns>DataSet</returns>
        public static DataSet GetCategoryByParentCategory(string parentCategory)
        {

            try
            {
                string strsql = @"select CategoryId,CategoryCode,CategoryName,DisplaySeq from ps_BusinessCategory t1 
                                    where t1.DeleteFlag =0 and t1.ObjectOption = 1  
                                    and t1.ParentCategory = @ParentCategory --父类型的ID
                                    Order by t1.CategoryCode,t1.CategoryName";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentCategory", DbType.String, parentCategory);
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
        /// 方法说明：根据物料分类ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="typeId">分类ID</param>
        /// <param name="categoryId">业务类型ID</param>
        /// <param name="conditions">其他条件</param>
        /// <returns>DataSet</returns>
        public static DataSet GetMaterialModuleDataByCategoryId(string typeId, string categoryId, string conditions)
        {

            try
            {
                string strsql = @"select objecticonpath,isinflow,intimage,disginstateiconpath,designcycle,qualitystateiconpath,
                                technicscycle,lastarchive,baseid,technicsstateiconpath,materialverid,code,
                                name,drawnumber,spec,vercode,intproductmode,
                                createdate,count,productname,categoryname,categoryid_typeid,
                                isvirtualdesign,typename,papercount,memberspec,
                                IsCreateNew,ChangingApply from v_mat_materialversion WHERE ArticleType = 0 and IsEffect = 1  
                                and FactoryId = ''  And  LanguageId = 0 ";
                
                if (!string.IsNullOrEmpty(conditions))
                {
                    if (!string.IsNullOrEmpty(typeId))
                    {
                        strsql += string.Format("and TypeId ='{0}' ", typeId);
                    }

                    if (!string.IsNullOrEmpty(categoryId))
                    {
                        strsql += string.Format("and CategoryId in (select CategoryId from dbo.f_PS_GetCategoryId('{0}') )", categoryId);
                    }

                    strsql += conditions;
                }
                else
                {
                    string categoryId_typeId = categoryId + typeId;
                    strsql += string.Format(" and categoryId_typeId = '{0}'", categoryId_typeId);
                }

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
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
        /// 方法说明：根据物料分类ID获取物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-05
        /// </summary>
        /// <param name="categoryId">分类ID</param>
        /// <param name="code">物料编码</param>
        /// <returns>DataSet</returns>
        public static DataSet GetMaterialModuleListByCategoryId(string categoryId, string code)
        {

            try
            {
                string strsql = @"select objecticonpath,isinflow,intimage,disginstateiconpath,designcycle,qualitystateiconpath,
                                technicscycle,lastarchive,baseid,technicsstateiconpath,materialverid,code,
                                name,drawnumber,spec,vercode,intproductmode,
                                createdate,count,productname,categoryname,categoryid_typeid,
                                isvirtualdesign,typename,papercount,memberspec,typeid,
                                IsCreateNew,ChangingApply from v_mat_materialversion 
                                where IsFrozen = 0  
                                and (IsShow=1 OR (IsEffect=1 AND IsShow=2 AND BaseId NOT IN 
                                (SELECT mm.BaseId FROM MAT_MaterialVersion mm WHERE mm.IsShow=1 ) ))  
                                and DesignCycle in (1,2,3,4)  and CategoryId_TypeId = @CategoryId_TypeId 
                                and code = @code
                                and FactoryId in ('')  and ArticleType = 0  And  LanguageId = 0";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@CategoryId_TypeId", DbType.String, categoryId);
                    db.AddInParameter(cmd, "@code", DbType.String, code);
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
        /// 方法说明：获取物料版本数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <returns>物料版本结合</returns>
        public static DataSet GetMaterialVersionModuleList()
        {
            try
            {
                string strsql = @"select MaterialVerId,BaseId,Code,Name,MaterialType,ProductId,ObjectIconPath 
                                    from Mat_MaterialVersion with(nolock)";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
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
        /// 方法说明：根据PBOM ID获取下层对应的物料数据
        /// 作    者：jason.tang
        /// 完成时间：2013-08-22
        /// </summary>
        /// <param name="verId">Ver ID</param>
        /// <returns>物料版本数据集</returns>
        public static DataSet GetChildPbomMaterialByPbomId(string verId)
        {
            try
            {
                string strsql = @"select parent.VerId,a.ChildId,      
                                          MaterialVerId,
                                          BaseId,
                                          Code,
                                          Name,
                                          MaterialType,
                                          ProductId,
                                          parent.ObjectIconPath from pp_pbomChild a
                                        inner join pp_pbomVer parent 
                                        on a.ParentId=Parent.VerId
                                        left join pp_pbomVer child
                                        on a.ChildId=child.Verid
                                        inner join Mat_materialVersion mat
                                        on a.objectId=mat.BaseId and mat.IsEffect=1 
                                        where parent.VerId='{0}' ";

                strsql = string.Format(strsql, verId);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
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
        /// 方法说明：根据父版本ID获取物料版本数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="versionId">版本ID</param>
        /// <returns>物料版本数据集</returns>
        public static DataSet GetMaterialVersionModuleListByVersionId(string versionId)
        {
            try
            {
                string strsql = @"select ParentVerId,RelationId,ChildVerId,DisplaySeq,IsLock,name
                                    FROM Mat_MaterialRelation t1 with(nolock) 
                                    Left join Mat_MaterialVersion t2 on t1.ParentVerId = t2.MaterialVerId
                                    where t1.ParentVerId = @ParentVerId
                                    order by t1.DisplaySeq";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentVerId", DbType.String, versionId);
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
        /// 方法说明：根据BaseId获取PBOM资料
        /// 作    者：jason.tang
        /// 完成时间：2013-03-06
        /// </summary>
        /// <param name="baseId">BaseId</param>
        /// <returns>PBOM数据集</returns>
        public static DataSet GetPBomModuleListByBaseId(string baseId)
        {
            try
            {
                string strsql = @"select a.VerId,a.PbomId,c.MaterialVerId FolderId,c.Name FolderName,
                                            a.Creator,a.CreateDate,
                                            b.ArchivePerson,b.ArchiveDate from pp_pbomVer a
                                            inner join pp_pbom b
                                            on a.PBOMId=b.PBomId and a.Ver=b.CurrentVer
                                            inner join PP_Folder f
                                            on f.FolderId=b.FolderId
                                            inner join mat_materialversion c
                                            on b.ObjectId=c.BaseId and c.IsEffect=1 
                                            where c.BaseId in ({0})";

                strsql = string.Format(strsql, baseId);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    //db.AddInParameter(cmd, "@ObjectId", DbType.String, baseId);
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
        /// 方法说明：根据PBomId获取对应的工序
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="pbomid">PBOM ID</param>
        /// <returns></returns>
        public static DataSet GetOperByPBomId(string pbomid)
        {
            try
            {
                string strsql = @"select o.* from PP_PBOMVer v
                                        inner join pp_PBOMVerRouting r
                                        on v.VerId=r.VerId
                                        inner join PP_RoutingOper rt
                                        on r.RoutingId= rt.RoutingId
                                        inner join pp_oper o
                                        on o.OperId=rt.OperId
                                        where v.VerId='{0}' ";

                strsql = string.Format(strsql, pbomid);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
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
        /// 方法说明：根据PBomId获取对应的工艺路线
        /// 作      者：jason.tang
        /// 完成时间：2013-08-27
        /// </summary>
        /// <param name="pbomid">PBOM ID</param>
        /// <returns></returns>
        public static DataSet GetRoutingByPBomId(string pbomid)
        {
            try
            {
                string strsql = @"select top 1 pr.* from PP_PBOMVer v
                                        inner join pp_PBOMVerRouting r
                                        on v.VerId=r.VerId
                                        inner join PP_RoutingOper rt
                                        on r.RoutingId= rt.RoutingId                                        
                                        inner join PP_routing pr
                                        on rt.RoutingId=pr.RoutingId
                                        where v.VerId='{0}' ";

                strsql = string.Format(strsql, pbomid);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：根据物料版本ID获取相关对象
        /// 作   者：jason.tang
        /// 完成时间：2013-09-13
        /// </summary>
        /// <param name="verId">版本ID</param>
        /// <returns></returns>
        public static DataSet GetMaterialObjectByVerId(string verId)
        {
            try
            {
                string strsql = @"SELECT 
	                                case ObjectOption 
	                                when '0' then N'文档' 
	                                when '5' then N'业务表单' 
	                                when '8' then N'邮件' 
                                    when '13' then N'工装'
                                    when '11' then N'PBOM'
                                    when '30' then N'工艺卡片'
	                                end as ObjectOption,
	                                Name, 
                                    Code, 
                                    VerCode,
                                    CategoryName, 
	                                case RelationType
	                                when 1 then N'设计'
	                                when 2 then N'质量'
	                                when 3 then N'工艺'
	                                when 4 then N'生产'
	                                when 5 then N'其他'
	                                else '' end as RelationType, 
	                                case OriginalMode
	                                when 1 then '文档创建'
	                                else '' end as OriginalMode,
                                    ObjectOption as ObjOption,
                                    CheckOutState,
	                                KeyId, ObjectId, 
                                    (select CopyId from Doc_DocumentCopy where VerId= ObjectId And IsActive = 1 ) as CopyId,
                                    ObjectOption, CategoryId, State,CheckOutState, ObjectIconPath ,RelationType,
	                                SrcObjectId
	                                from V_Mat_RelationObject_New  v
	                                WHERE SrcObjectId = '{0}' 
	                                OR DesObjectId = '{1}' ";

                strsql = string.Format(strsql, verId, verId);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
