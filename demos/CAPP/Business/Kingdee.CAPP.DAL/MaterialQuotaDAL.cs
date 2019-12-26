using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：材料定额DAL
    /// 作      者：jason.tang
    /// 完成时间：2013-07-08
    /// </summary>
    public class MaterialQuotaDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// 方法说明：根据编码和材料属性获取材料定额信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-08
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="property">材料属性</param>
        /// <returns></returns>
        public static DataSet GetMaterialQuotaData(string code, string property)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"Select 
                                        Id,
                                        Code,
                                        Property,
                                        Proportion,
                                        WeightUnit,
                                        LengthUnit,
                                        ValidDigits,
                                        Formula                    
                                    from MaterialQuota WHERE 1=1 ");

            if (!string.IsNullOrEmpty(code))
            {
                sb.Append(string.Format(" And Code in ({0}) ", code));
            }

            if (!string.IsNullOrEmpty(property))
            {
                sb.Append(string.Format(" And  Property = '{0}'", property));
            }

            using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
            {
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        /// <summary>
        /// 方法说明：根据MaterialVerId获取物料信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-09
        /// </summary>
        /// <param name="materialVerId">物料表ID</param>
        /// <returns></returns>
        public static DataSet GetMaterialVersionData(string materialVerId)
        {
            string strSql = @"select case when v.MeterWeight Is null 
		                        then 
			                        case when (select top 1 m.MeterWeight from v_mat_materialversion m
                                                    inner join PS_BusinessCategory c
                                                    on m.CategoryId = c.CategoryId
                                                    where MaterialVerId = v.MaterialVerId) Is null 
                                    then 0
                                    else (select top 1 m.MeterWeight from v_mat_materialversion m
                                                    inner join PS_BusinessCategory c
                                                    on m.CategoryId = c.CategoryId
                                                    where MaterialVerId = v.MaterialVerId) 
                                    end
		                        else v.MeterWeight 
		                        end MeterWeight,
                                    v.Code,
                                    v.Name,
                                    v.DrawNumber,
                                    case when v.ChildCount is null or r.ChildCount is null then 0 else
                                    v.ChildCount * r.ChildCount end
                                     As ChildCount,
                                    case when r.Waste is null then 0 else r.Waste end Waste,
                                    Proportion,
                                    q.Id,
                                    ValidDigits,
                                    Formula,
                                    Quota
                                    from MAT_MaterialVersion v left join
                                    MAT_MaterialRelation r
                                    on r.ChildVerId = v.MaterialVerId
                                    Left join MaterialQuota q
                                    on v.Code = q.Code
                                    where ParentVerId = @ParentVerId ";

            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                db.AddInParameter(cmd, "@ParentVerId", DbType.Guid, new Guid(materialVerId));
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        /// <summary>
        /// 方法说明：获取米重量数据
        /// 作    者：jason.tang
        /// 完成时间：2013-09-26
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static DataSet GetMeterWeight(int type)
        {
            string strSql = @"select c.CategoryId Id,CategoryCode Code,
                                CategoryName Name,
                                MeterWeight from PS_BusinessCategory c
                                inner join PS_BusinessCategoryRelation r
                                on c.CategoryId=r.CategoryId 
                                where r.CommonType=4 and c.MeterWeight is not null";

            if (type == 1)
            {
                strSql = @"select m.MaterialVerId Id,m.Code,m.Name,m.MeterWeight,m.CategoryName from v_mat_materialversion m
                            inner join PS_BusinessCategory c
                            on c.CategoryId = m.CategoryId
                            inner join PS_BusinessCategoryRelation r
                            on c.CategoryId = r.CategoryId
                            where r.CommonType=4
                            and m.MeterWeight is not null";
            }

            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }
        

        /// <summary>
        /// 方法说明：根据Id更新材料定额数据
        /// 作      者：jason.tang
        /// 完成时间：2013-07-09
        /// </summary>
        /// <param name="materialQuota">材料定额实体</param>
        /// <returns></returns>
        public static bool UpdateMaterialQuotaData(MaterialQuota materialQuota)
        {
            bool result = false;

            try
            {
                string strsql = @"update MaterialQuota  set 
                                Proportion=@Proportion,
                                ValidDigits=@ValidDigits,
                                Formula=@Formula
                                where
                                Id=@Id";
                                
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Proportion", DbType.Decimal, materialQuota.Proportion);
                    db.AddInParameter(cmd, "@ValidDigits", DbType.Int32, materialQuota.ValidDigits);
                    db.AddInParameter(cmd, "@Formula", DbType.String, materialQuota.Formula);                   
                    db.AddInParameter(cmd, "@Id", DbType.Guid, materialQuota.Id);

                    result = db.ExecuteNonQuery(cmd) > 0;

                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：根据Id更新材料定额数据
        /// 作      者：jason.tang
        /// 完成时间：2013-07-09
        /// </summary>
        /// <param name="materialQuota">材料定额实体</param>
        /// <returns></returns>
        public static bool UpdateQuotaData(MaterialQuota materialQuota)
        {
            bool result = false;

            try
            {
                string strsql = @"update MaterialQuota  set 
                                        Quota=@Quota
                                    where
                                    Id=@Id";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {                    
                    if (materialQuota.Quota == decimal.MinValue)
                        db.AddInParameter(cmd, "@Quota", DbType.Decimal, DBNull.Value);
                    else
                        db.AddInParameter(cmd, "@Quota", DbType.Decimal, materialQuota.Quota);
                    db.AddInParameter(cmd, "@Id", DbType.Guid, materialQuota.Id);

                    result = db.ExecuteNonQuery(cmd) > 0;

                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：根据Id更新米重量
        /// 作    者：jason.tang
        /// 完成时间：2013-09-26
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public static bool UpdateMeterWeight(string id, int type, decimal meterWeight)
        {
            bool result = false;

            try
            {
                string strsql = @"update PS_BusinessCategory  set 
                                        MeterWeight=@MeterWeight
                                    where
                                    CategoryId=@CategoryId";

                if (type == 1)
                {
                    strsql = @"update mat_materialversion  set 
                                        MeterWeight=@MeterWeight
                                    where
                                    MaterialVerId=@MaterialVerId";
                }

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    if (type == 0)
                    {
                        db.AddInParameter(cmd, "@CategoryId", DbType.String, id);
                    }
                    else
                        db.AddInParameter(cmd, "@MaterialVerId", DbType.String, id);

                    db.AddInParameter(cmd, "@MeterWeight", DbType.Decimal, meterWeight);

                    result = db.ExecuteNonQuery(cmd) > 0;

                }
            }
            catch
            {
                throw;
            }

            return result;
        } 

        /// <summary>
        /// 方法说明：新增材料定额数据
        /// 作      者：jason.tang
        /// 完成时间：2013-07-10
        /// </summary>
        /// <param name="materialQuota">材料定额实体</param>
        /// <returns></returns>
        public static bool AddMaterialQuotaData(MaterialQuota materialQuota)
        {
            bool result = false;

            try
            {
                string strsql = @"INSERT INTO MaterialQuota
                                    ([Id]
                                   ,[Code]
                                   ,[Proportion]
                                   ,[ValidDigits]
                                   ,[Formula]
                                   ,[Quota])
                             VALUES
                                   (
                                    @ID
                                   ,@Code
                                   ,@Proportion
                                   ,@ValidDigits
                                   ,@Formula
                                   ,@Quota)";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Id", DbType.Guid, materialQuota.Id);
                    db.AddInParameter(cmd, "@Code", DbType.String, materialQuota.Code);
                    db.AddInParameter(cmd, "@Proportion", DbType.Decimal, materialQuota.Proportion);
                    db.AddInParameter(cmd, "@ValidDigits", DbType.Int32, materialQuota.ValidDigits);
                    db.AddInParameter(cmd, "@Formula", DbType.String, materialQuota.Formula);
                    db.AddInParameter(cmd, "@Quota", DbType.Decimal, materialQuota.Quota);
                    result = db.ExecuteNonQuery(cmd) > 0;

                }
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
