using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// 类型说明：典型工艺BLL类
    /// 作      者：jason.tang
    /// 创建时间：2013-06-20
    /// </summary>
    public class TypicalProcessBLL
    {
        /// <summary>
        /// 方法说明：根据父节点获取工艺文件
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        public static List<TypicalProcess> GetTypicalProcesList(int parentNode)
        {
            try
            {
                DataTable dt = TypicalProcessDAL
                    .GetTypicalProcesByParentNode(parentNode).Tables[0];

                var cTypicalProcessList = (from c in dt.AsEnumerable()
                                           select new TypicalProcess()
                                                  {
                                                      TypicalProcessId = c.Field<Guid>("TypicalProcessId"),
                                                      BusinessId = c.Field<Guid>("BussinessId"),
                                                      CurrentNode = c.Field<int>("CurrentNode"),
                                                      ParentNode = c.Field<int>("ParentNode"),
                                                      Name = c.Field<string>("Name"),
                                                      Sort = c.Field<int>("Sort"),
                                                      BType = (BusinessType)Enum.Parse(typeof(BusinessType),
                                                                  c.Field<Int16>("Type").ToString())

                                                  }).ToList<TypicalProcess>();
                return cTypicalProcessList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：根据名称获取典型工艺类型
        /// 作者：jason.tang
        /// 完成时间：2013-07-23
        /// </summary>
        /// <param name="name">类型名</param>
        /// <returns></returns>
        public static DataTable GetTypicalCategory(string name)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = TypicalProcessDAL.GetTypicalCategory(name);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：检查卡片是否已转为典型
        /// 作      者：jason.tang
        /// 完成时间：2013-07-24
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public static bool ExistTypcialProcessCard(Guid businessId, int parentId)
        {
            bool result = true;

            try
            {
                DataSet ds = TypicalProcessDAL.ExistTypcialProcessCard(businessId, parentId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0].Rows.Count > 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：新增典型工艺
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="typical">典型工艺实体</param>
        /// <returns></returns>
        public static int AddTypicalProcess(TypicalProcess typical)
        {
            int result;
            try
            {
                result = TypicalProcessDAL.AddTypicalProcess(typical);
                typical.CurrentNode = result;
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：新增典型工艺
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="typicalList">典型工艺实体集合</param>
        /// <returns></returns>
        public static void AddTypicalProcess(List<TypicalProcess> typicalList)
        {
            try
            {
                TypicalProcessDAL.AddTypicalProcess(typicalList);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// 方法说明：根据ID删除典型工艺
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="typicalprocessid">典型工艺ID</param>
        /// <returns></returns>
        public static bool DeleteTypicalById(Guid typicalprocessid)
        {
            bool result = true;

            try
            {
                result = TypicalProcessDAL.DeleteTypicalById(typicalprocessid);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
