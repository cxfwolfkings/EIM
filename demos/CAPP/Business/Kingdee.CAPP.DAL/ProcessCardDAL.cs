using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.Common.Serialize;
/*******************************
 * Created By franco
 * Description: Process card data access layer
 *******************************/

namespace Kingdee.CAPP.DAL
{
    public class ProcessCardDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// get card By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetProcessCardDataset(Guid id)
        {

            string strsql = @"Select 
                                Id,
                                Name,
                                CardModuleId,
                                CardXml,                                
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout                    
                            from ProcessCard WHERE Id = @Id";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@Id", DbType.Guid, id);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        /// <summary>
        /// 方法说明： 根据条件获取卡片列表
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="processCardName">卡片名称</param>
        /// <returns></returns>
        public static DataSet GetProcessCardDatasetByCondition(string processCardName)
        {
            try
            {
                string strsql = @"Select  top 100
                                Id,
                                Name,
                                CardModuleId,
                                CardXml,                                
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout                    
                            from ProcessCard
                            where Name like '%" + processCardName + @"%' 
                            order by CreateTime";

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
        /// 方法说明：获取前100条卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <returns></returns>
        public static DataSet GetDefaultProcessCardDataset()
        {
            try
            {
                string strsql = @"Select  top 100
                                Id,
                                Name,
                                CardModuleId,
                                CardXml,                                
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout from ProcessCard Order by CreateTime";

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
        /// 方法说明：获取工艺文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="parentFolder">父文件夹</param>
        /// <returns></returns>
        public static DataSet GetProcessFolderListDataSet(string parentFolder)
        {
            try
            {

                string strsql = string.Empty;
                if(string.IsNullOrEmpty(parentFolder.Trim()))
                {
                    strsql = @"Select  * from pp_pcfolder where ParentFolder is null ";
                }
                else
                    strsql = @"Select  * from pp_pcfolder where ParentFolder=@ParentFolder ";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentFolder", DbType.String, parentFolder);
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
        /// 方法说明：获取对应文件夹下的工艺文件
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <returns></returns>
        public static DataSet GetProcessCardByFolderIdDataSet(string folderId, Int32 categoryid)
        {
            try
            {
                string strsql = string.Empty;

                if (categoryid == 1)
                {
                    strsql = @"select v.* from PP_PCVersion v
                            inner join ProcessCard c
                            on v.BaseId = c.Id";
                }
                else
                {
                    strsql = @"select v.* from PP_PCVersion v
                            inner join ProcessPlanning c
                            on v.BaseId = c.ProcessPlanningId";
                }

                strsql = strsql + " where FolderId=@FolderId ";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@FolderId", DbType.String, folderId);
                    //db.AddInParameter(cmd, "@CategoryId", DbType.String, categoryid);
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
        /// get card by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCardXML(Guid id)
        {
            string strsql = @"Select 
                                CardXML
                            from ProcessCard WHERE Id = @Id";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@Id", DbType.Guid, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    string cardxml = string.Empty;
                    if (dr.Read())
                    {
                        cardxml = dr[0].ToString();
                    }
                    return cardxml;
                }
            }
        }

        /// <summary>
        /// insert process card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static Guid AddProcessCard(Kingdee.CAPP.Model.ProcessCard card)
        {
            string strsql = @"INSERT INTO [dbo].[ProcessCard]
                                    ([Id]
                                   ,Name
                                   ,[CardModuleId]
                                   ,[CardXml]
                                   ,[CreateTime]
                                   ,[CreateBy]
                                   ,[UpdateTime]
                                   ,[IsDelete]
                                   ,[IsCheckOut])
                             VALUES
                                   (
                                    @ID
                                   ,@Name
                                   ,@CardModuleId
                                   ,@CardXml
                                   ,@CreateTime
                                   ,@CreateBy
                                   ,@UpdateTime
                                   ,@IsDelete
                                   ,@IsCheckout)";

            string xmlstr = SerializeHelper.Serialize<CardsXML>(card.Card);
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@ID", DbType.Guid, id);
                db.AddInParameter(cmd, "@Name", DbType.String, card.Name);
                db.AddInParameter(cmd, "@CardModuleId", DbType.Guid, card.CardModuleId);
                db.AddInParameter(cmd, "@CardXML", DbType.Xml, xmlstr);
                db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@CreateBy", DbType.String, card.CreateBy);
                db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@IsDelete", DbType.Int16, card.IsDelete);
                db.AddInParameter(cmd, "@IsCheckout", DbType.Boolean, card.IsCheckOut);


                db.ExecuteScalar(cmd);
                return id;
            }
        }

        /// <summary>
        /// 方法说明：卡片版本新增
        /// 作    者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="version">卡片版本实体</param>
        /// <returns>True/False</returns>
        public static bool AddProcessVersion(ProcessVersion version, object material)
        {
            bool result = false;

            string strsql = @"INSERT INTO [PP_PCVersion]
                                           ([VerId]
                                           ,[BaseId]
                                           ,[VerCode]
                                           ,[VerName]
                                           ,[Code]
                                           ,[Name]
                                           ,[CategoryId]
                                           ,[MajorVer]
                                           ,[MinorVer]
                                           ,[State]
                                           ,[Creator]
                                           ,[CreateDate]
                                           ,[Updater]
                                           ,[UpdateDate]
                                           ,[CheckOutPerson]
                                           ,[CheckOutDate]
                                           ,[ArchiveDate]
                                           ,[ReleaseDate]
                                           ,[Remark]
                                           ,[IsChange]
                                           ,[IsEffective]
                                           ,[IsInFlow]
                                           ,[IsShow]
                                           ,[FolderId]
                                           ,[ObjectStatePath]
                                           ,[ObjectIconPath])
                                     VALUES
                                           (@VerId,
                                           @BaseId,
                                           @VerCode,
                                           @VerName,
                                           @Code,
                                           @Name,
                                           @CategoryId,
                                           @MajorVer,
                                           @MinorVer,
                                           @State,
                                           @Creator,
                                           @CreateDate,
                                           @Updater,
                                           @UpdateDate,
                                           @CheckOutPerson,
                                           @CheckOutDate,
                                           @ArchiveDate,
                                           @ReleaseDate,
                                           @Remark,
                                           @IsChange,
                                           @IsEffective,
                                           @IsInFlow,
                                           @IsShow,
                                           @FolderId,
                                           @ObjectStatePath,
                                           @ObjectIconPath)";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                string categroyId = GetCategroyByFolderId(version.FolderId);
                if (string.IsNullOrEmpty(categroyId))
                    categroyId = "ceff050d-31bd-47dd-b282-3a68729db064";
                string code = GetCode(version.Code, version.Name);

                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@VerId", DbType.Guid, id);
                db.AddInParameter(cmd, "@BaseId", DbType.String, version.BaseId);
                db.AddInParameter(cmd, "@VerCode", DbType.String, version.VerCode);
                db.AddInParameter(cmd, "@VerName", DbType.String, version.VerName);
                db.AddInParameter(cmd, "@Code", DbType.String, code);
                db.AddInParameter(cmd, "@Name", DbType.String, version.Name);
                db.AddInParameter(cmd, "@CategoryId", DbType.String, categroyId);
                db.AddInParameter(cmd, "@MajorVer", DbType.Int32, version.MajorVer);
                db.AddInParameter(cmd, "@MinorVer", DbType.Int32, version.MinorVer);
                db.AddInParameter(cmd, "@State", DbType.Int32, version.State);
                db.AddInParameter(cmd, "@Creator", DbType.String, version.Creator);
                db.AddInParameter(cmd, "@CreateDate", DbType.String, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                db.AddInParameter(cmd, "@Updater", DbType.String, version.Updater);
                db.AddInParameter(cmd, "@UpdateDate", DbType.String, version.UpdateDate);
                db.AddInParameter(cmd, "@CheckOutPerson", DbType.String, version.CheckOutPerson);
                db.AddInParameter(cmd, "@CheckOutDate", DbType.String, version.CheckOutDate);
                db.AddInParameter(cmd, "@ArchiveDate", DbType.String, version.ArchiveDate);
                db.AddInParameter(cmd, "@ReleaseDate", DbType.String, version.ReleaseDate);
                db.AddInParameter(cmd, "@Remark", DbType.String, version.Remark);
                db.AddInParameter(cmd, "@IsChange", DbType.Boolean, version.IsChange);
                db.AddInParameter(cmd, "@IsEffective", DbType.Boolean, true);
                db.AddInParameter(cmd, "@IsInFlow", DbType.Boolean, version.IsInFlow);
                db.AddInParameter(cmd, "@IsShow", DbType.Int32, version.IsShow);
                db.AddInParameter(cmd, "@FolderId", DbType.String, version.FolderId);
                db.AddInParameter(cmd, "@ObjectStatePath", DbType.String, "../skins/ObjectState/2.gif");
                db.AddInParameter(cmd, "@ObjectIconPath", DbType.String, "../skins/ObjectIcon/materialCard.PNG");


                if(db.ExecuteNonQuery(cmd) > 0)
                {
                    AddFolderRelation(version.BaseId, version.FolderId);

                    if (material != null)
                    {
                        MaterialModule materialModule = null;
                        string pbomId= string.Empty;
                        if (material.GetType() == typeof(MaterialModule))
                        {
                            materialModule = (MaterialModule)material;
                        }
                        else if(material.GetType() == typeof(String))
                        {
                            pbomId = material.ToString();
                        }
                        AddRelationObject(id.ToString(), materialModule, pbomId);
                    }
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 方法说明：根据文件夹ID获取分类ID
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        private static string GetCategroyByFolderId(string folderId)
        {
            try
            {
                string strsql = @"Select top 1 * from PP_PCVersion where FolderId=@FolderId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@FolderId", DbType.String, folderId);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0].Rows[0]["CategoryId"].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：检验编码是否存在，存在则自动生成一个新的
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static string GetCode(string code, string name)
        {
            try
            {
                string strsql = @"Select top 1 * from PP_PCVersion where Code=@Code";

                bool result = false;
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Code", DbType.String, code);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

                if (result)
                    return string.Format(name + "-{0}", DateTime.Now.ToString("yyyy-MM-dd HH:ss:dd"));
                else
                    return code;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：添加文件夹关系
        /// 作    者：jason.tang
        /// 完成时间：2013-08-28
        /// </summary>
        /// <param name="baseid"></param>
        /// <param name="folderid"></param>
        /// <returns></returns>
        private static bool AddFolderRelation(string baseid, string folderid)
        {
            string strsql = @"
                            INSERT INTO [PLM].[dbo].[PP_PCFolderRelation]
                                       ([RelationId]
                                       ,[RelationType]
                                       ,[ParentId]
                                       ,[ChildId])
                                 VALUES
                                       (@RelationId
                                       ,@RelationType
                                       ,@ParentId
                                       ,@ChildId)
                            ";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@RelationId", DbType.Guid, id);
                db.AddInParameter(cmd, "@RelationType", DbType.Int32, 1);
                db.AddInParameter(cmd, "@ParentId", DbType.String, folderid);
                db.AddInParameter(cmd, "@ChildId", DbType.String, baseid);

                return db.ExecuteNonQuery(cmd) > 0;
            }
        }

        /// <summary>
        /// 方法说明：增加关系对象记录
        /// 作    者：jason.tang
        /// 完成时间：2013-09-17
        /// </summary>
        /// <param name="verId">版本ID</param>
        /// <returns></returns>
        private static bool AddRelationObject(string verId, MaterialModule materialModule, string pbomid)
        {
            string materialVerId = string.Empty;
            string materialBaseId = string.Empty;
            string routingId = string.Empty;
            
            if (materialModule != null)
            {
                materialVerId = materialModule.materialverid;
                materialBaseId = materialModule.baseid;
                if (string.IsNullOrEmpty(pbomid))
                    pbomid = materialModule.pbomid;
            }

            if (!string.IsNullOrEmpty(pbomid))
            {
                DataSet ds = MaterialModuleDAL.GetRoutingByPBomId(pbomid);
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return false;
                }

                routingId = ds.Tables[0].Rows[0]["RoutingId"].ToString();
            }

            string strsql = @"
                            INSERT INTO [PLM].[dbo].[SYS_RelationObject]
                                   ([KeyId]
                                   ,[SrcObjectType]
                                   ,[SrcObjectId]
                                   ,[DesObjectType]
                                   ,[DesObjectId]
                                   ,[RelationType]
                                   ,[OriginalVerId]
                                   ,[OriginalMode]
                                   ,[SrcBasetId]
                                   ,[DesBasetId])
                             VALUES
                                   (@KeyId
                                   ,@SrcObjectType
                                   ,@SrcObjectId
                                   ,@DesObjectType
                                   ,@DesObjectId
                                   ,0
                                   ,null
                                   ,0
                                   ,@SrcBasetId
                                   ,@DesBasetId)
                            ";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@KeyId", DbType.Guid, id);
                db.AddInParameter(cmd, "@SrcObjectType", DbType.Int32, 30);                
                db.AddInParameter(cmd, "@DesObjectType", DbType.Int32, 30);
                if (!string.IsNullOrEmpty(routingId))
                {
                    db.AddInParameter(cmd, "@SrcObjectId", DbType.String, routingId);
                    db.AddInParameter(cmd, "@SrcBasetId", DbType.String, routingId);
                }
                else
                {
                    db.AddInParameter(cmd, "@SrcObjectId", DbType.String, materialVerId);
                    db.AddInParameter(cmd, "@SrcBasetId", DbType.String, materialBaseId);
                }
                db.AddInParameter(cmd, "@DesObjectId", DbType.String, verId);
                db.AddInParameter(cmd, "@DesBasetId", DbType.String, verId);

                return db.ExecuteNonQuery(cmd) > 0;
            }
        }

        /// <summary>
        /// 方法说明：卡片修改
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <param name="card">卡片实体</param>
        /// <returns>True/False</returns>
        public static bool UpdateProcessCard(Kingdee.CAPP.Model.ProcessCard card)
        {
            string strsql = @"UPDATE [dbo].[ProcessCard]                                   
                                   set [CardXml]=@CardXml
                                   ,[CreateTime]=@CreateTime
                                   ,[CreateBy]=@CreateBy
                                   ,[UpdateTime]=@UpdateTime
                                   ,[IsDelete]=@IsDelete
                                   ,[IsCheckout]=@IsCheckout
                             where ID=@ID";

            string xmlstr = SerializeHelper.Serialize<CardsXML>(card.Card);
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ID", DbType.Guid, card.ID);
                db.AddInParameter(cmd, "@CardXML", DbType.Xml, xmlstr);
                db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@CreateBy", DbType.String, card.CreateBy);
                db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@IsDelete", DbType.Int16, 0);
                db.AddInParameter(cmd, "@IsCheckout", DbType.Boolean, false);


                db.ExecuteScalar(cmd);
                return true;
            }
        }

        /// <summary>
        /// 方法说明：根据卡片ID删除卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="cardid">卡片ID</param>
        /// <returns></returns>
        public static bool DeleteProcessCard(string cardid)
        {
            bool result = true;

            string strsql = @"DELETE FROM ProcessCard where ID in ('{0}')";
            strsql = string.Format(strsql, cardid);

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {               
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：卡片版本删除
        /// 作    者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="baseid">卡片Id</param>
        /// <param name="folderid">文件夹ID</param>
        /// <returns>True/False</returns>
        public static bool DeleteProcessVersion(string baseid, string folderid)
        {
            bool result = true;

            string strsql = @"DELETE FROM PP_PCVersion where BaseID=@BaseId And FolderId=@FolderId ";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@BaseId", DbType.String, baseid);
                db.AddInParameter(cmd, "@FolderId", DbType.String, folderid);
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：获取用户数据
        /// 作      者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <returns></returns>
        public static DataSet GetUsersDataset()
        {
            try
            {
                string strsql = @"select UserId,UserName from SM_Users";

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
        /// 方法说明：获取工艺卡片数据
        /// 作      者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataSet GetProcessVersionDataSet(string condition)
        {
            try
            {
                string strsql = @"select Id, p.Name  from PP_PCVersion v
                                    inner join ProcessCard p
                                    on v.BaseId = p.Id ";
                if (condition.Contains("Type=2"))
                {
                    strsql = @"select pc.ProcessCardId Id, (select Name from ProcessCard where Id=pc.ProcessCardId) Name
                             from ProcessPlanning pl inner join PlanningCardRelation pc
                            on pl.ProcessPlanningId = pc.ProcessPlanningId
                            inner join PP_PCVersion v on v.BaseId = pc.ProcessCardId ";

                    condition = condition.Replace("Type=2", "");
                }
                condition = condition.Replace("Type=1", "");
                if (!string.IsNullOrEmpty(condition))
                {
                    condition = condition.Trim();
                    if (condition.StartsWith("And"))
                        condition = condition.Remove(0, 3);
                    strsql = string.Format(strsql + " where {0}", condition);
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
    }
}
