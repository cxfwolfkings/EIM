using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kingdee.CAPP.DAL;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// 类型说明：材料定额BLL
    /// 作      者：jason.tang
    /// 完成时间：2013-07-08
    /// </summary>
    public class MaterialQuotaBLL
    {
        /// <summary>
        /// 方法说明：根据编码和材料属性获取材料定额信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-08
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="property">材料属性</param>
        /// <returns></returns>
        public static DataTable GetMaterialQuotaData(string code, string property)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = MaterialQuotaDAL.GetMaterialQuotaData(code, property);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：根据MaterialVerId获取物料信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-09
        /// </summary>
        /// <param name="materialVerId">物料表ID</param>
        /// <returns></returns>
        public static DataTable GetMaterialVersionData(string materialVerId)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = MaterialQuotaDAL.GetMaterialVersionData(materialVerId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：获取米重量数据
        /// 作    者：jason.tang
        /// 完成时间：2013-09-26
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static DataTable GetMeterWeight(int type)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = MaterialQuotaDAL.GetMeterWeight(type);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch
            {
                throw;
            }

            return dt;
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
                result = MaterialQuotaDAL.AddMaterialQuotaData(materialQuota);
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
        public static bool UpdateMaterialQuotaData(MaterialQuota materialQuota)
        {
            bool result = false;

            try
            {
                result = MaterialQuotaDAL.UpdateMaterialQuotaData(materialQuota);
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
        /// 完成时间：2013-07-27
        /// </summary>
        /// <param name="materialQuota">材料定额实体</param>
        /// <returns></returns>
        public static bool UpdateQuotaData(MaterialQuota materialQuota)
        {
            bool result = false;

            try
            {
                result = MaterialQuotaDAL.UpdateQuotaData(materialQuota);
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
        /// <param name="id">材料定额实体</param>
        /// <returns></returns>
        public static bool UpdateMeterWeight(string id, int type, decimal meterWeight)
        {
            bool result = false;

            try
            {
                result = MaterialQuotaDAL.UpdateMeterWeight(id, type, meterWeight);
            }
            catch
            {
                throw;
            }

            return result;
        } 
    }
}
