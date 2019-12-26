using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Kingdee.CAPP.Model;
using System.Data;
using System.Data.Common;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：物料与卡片关联DAL
    /// 作    者：jason.tang
    /// 完成时间：2013-03-26
    /// </summary>
    public class MaterialCardRelationDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// 方法说明：根据物料ID查找卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="type">1-设计BOM视图 2-PBOM视图</param>
        /// <returns></returns>
        public static DataSet GetProcessCardByMaterialId(string materialId, int type)
        {
            try
            {
                string strsql = @"select ID,Name,CardSort from ProcessCard t1 
                                    left join MaterialCardRelation t2 on t1.ID = t2.ProcessCardId
                                    where MaterialId=@MaterialId and t2.Type=@Type";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@MaterialId", DbType.Guid, new Guid(materialId));
                    db.AddInParameter(cmd, "@Type", DbType.Int32, type);
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
        /// 方法说明：新增物料卡片关联记录
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="materialCardRelation">物料卡片关联实体</param>
        /// <returns></returns>
        public static Guid AddMaterialCardRelationData(MaterialCardRelation materialCardRelation)
        {
            try
            {
                string strsql = @"
                                INSERT INTO [dbo].[MaterialCardRelation]
                                   ([MaterialCardRelationId]
                                   ,[MaterialId]
                                   ,[ProcessCardId]
                                   ,[CardSort]
                                   ,[Type])
                             VALUES
                                   (@MaterialCardRelationId
                                   ,@MaterialId
                                   ,@ProcessCardId                                
                                   ,@CardSort
                                   ,@Type);";

                int cardSort = GetMaxCardSort(materialCardRelation.MaterialId);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@MaterialCardRelationId", DbType.Guid, materialCardRelation.MaterialCardRelationId);
                    db.AddInParameter(cmd, "@MaterialId", DbType.Guid, materialCardRelation.MaterialId);
                    db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, materialCardRelation.ProcessCardId);
                    db.AddInParameter(cmd, "@CardSort", DbType.Int32, cardSort);
                    db.AddInParameter(cmd, "@Type", DbType.Int32, materialCardRelation.Type);
                    db.ExecuteScalar(cmd);
                    return materialCardRelation.MaterialCardRelationId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：根据物料ID获取CardSort
        /// 作   者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="materialId">规程ID</param>
        /// <returns>最大的CardSort</returns>
        public static int GetMaxCardSort(Guid materialId)
        {
            int result = 0;

            try
            {
                string strsql = @"select count(MaterialCardRelationId) from MaterialCardRelation 
                                where MaterialId=@MaterialId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@MaterialId", DbType.Guid, materialId);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        result = int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：删除物料下挂的卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-08-10
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="processCardId">卡片ID</param>
        /// <param name="type">视图类型</param>
        /// <returns>True/False</returns>
        public static bool DeleteMaterialCard(string materialId, string processCardId, int type)
        {
            int result = 0;

            string strsql = @"delete from MaterialCardRelation where MaterialId=@MaterialId and ProcessCardId=@ProcessCardId and Type=@Type ";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid matid = new Guid(materialId);
                Guid cardId = new Guid(processCardId);
                db.AddInParameter(cmd, "@MaterialId", DbType.Guid, matid);
                db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, cardId);
                db.AddInParameter(cmd, "@Type", DbType.Int32, type);

                result = db.ExecuteNonQuery(cmd);
            }

            return result > 0;
        }

        /// <summary>
        /// 方法说明：根据零部件名查找卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <param name="name">零部件</param>
        /// <returns></returns>
        public static DataSet GetProcessCardByMaterialName(string name)
        {
            try
            {
                string strsql = @"select ID,t1.Name,CardSort from ProcessCard t1 
                                    left join MaterialCardRelation t2 on t1.ID = t2.ProcessCardId
                                    inner join MAT_MaterialVersion t3 on t2.MaterialId=t3.BaseId
                                    where t3.Name=@Name";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Name", DbType.String, name);                    
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
