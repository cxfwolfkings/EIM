using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;
using System.Data;
using Kingdee.CAPP.Common.Serialize;
/*******************************
 * Created By franco
 * Description: Process planning module business access layer
 *******************************/

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// Process planning module business access layer
    /// </summary>
    public class ProcessPlanningModuleBLL
    {

        /// <summary>
        /// get process planning module
        /// </summary>
        /// <returns></returns>
        public static List<ProcessPlanningModule> GetProcesPlanningModuleList(int parentNode)
        {
            try
            {
                DataTable dt = ProcessPlanningModuleDAL
                    .GetProcesPlanningModuleDatasetByParentNode(parentNode).Tables[0];

                var cProcessPlanningModuleList = (from c in dt.AsEnumerable()
                                       select new ProcessPlanningModule()
                                       {
                                           ProcessPlanningModuleId = c.Field<Guid>("ProcessPlanningModuleId"),
                                           BusinessId = c.Field<Guid>("BussinessId"),
                                           CurrentNode = c.Field<int>("CurrentNode"),
                                           ParentNode = c.Field<int>("ParentNode"),
                                           Name = c.Field<string>("Name"),
                                           Sort = c.Field<int>("Sort"),
                                           BType = (BusinessType)Enum.Parse(typeof(BusinessType),
                                                       c.Field<Int16>("Type").ToString())

                                       }).ToList<ProcessPlanningModule>();
                return cProcessPlanningModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get process planning module
        /// </summary>
        /// <returns></returns>
        public static List<ProcessPlanningModule> GetProcesPlanningModuleListByName(string name)
        {
            try
            {
                DataTable dt = ProcessPlanningModuleDAL
                    .GetProcesPlanningModuleDatasetByName(name).Tables[0];

                var cProcessPlanningModuleList = (from c in dt.AsEnumerable()
                                                  select new ProcessPlanningModule()
                                                  {
                                                      ProcessPlanningModuleId = c.Field<Guid>("ProcessPlanningModuleId"),
                                                      BusinessId = c.Field<Guid>("BussinessId"),
                                                      CurrentNode = c.Field<int>("CurrentNode"),
                                                      ParentNode = c.Field<int>("ParentNode"),
                                                      Name = c.Field<string>("Name"),
                                                      Sort = c.Field<int>("Sort"),
                                                      BType = (BusinessType)Enum.Parse(typeof(BusinessType),
                                                                  c.Field<Int16>("Type").ToString())

                                                  }).ToList<ProcessPlanningModule>();
                return cProcessPlanningModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get process card module  by process planning module id's
        /// </summary>
        /// <param name="ProcessPlanningModuleIds"></param>
        /// <returns></returns>
        public static List<ProcessCardModule> GetProcessCardModuleListByProcessPlanningModuleIds
            (List<Guid> ProcessPlanningModuleIds)
        {
            try
            {
                DataSet ds = ProcessPlanningModuleDAL
                    .GetProcessCardModuleListByProcessPlanningModuleIds(ProcessPlanningModuleIds);

                if (ds == null)
                {
                    return new List<ProcessCardModule>();
                }

                DataTable dt = ds.Tables[0];
                var cProcessCardModuleList = (from c in dt.AsEnumerable()
                                              select new ProcessCardModule()
                                              {
                                                  Id = c.Field<Guid>("Id"),
                                                  Name = c.Field<string>("Name"),
                                                  IsCheckout = c.Field<bool>("IsCheckout"),
                                                  FixedMapValues = c.Field<string>("FixedMapValues"),
                                                  DetailMapValues = c.Field<string>("DetailMapValues"),
                                                  TitleMapValues = c.Field<string>("TitleMapValues")
                                              }).ToList<ProcessCardModule>();
                return cProcessCardModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get process card module
        /// </summary>
        /// <returns></returns>
        public static List<ProcessCardModule> GetProcesCardModuleList(int processPlanningParentNode)
        {
            try
            {
                DataTable dt = ProcessPlanningModuleDAL
                    .GetProcessCardModuleDataSetByProcessPlanningNode(processPlanningParentNode).Tables[0];

                var cProcessCardModuleList = (from c in dt.AsEnumerable()
                                                  select new ProcessCardModule()
                                                  {
                                                      Id = c.Field<Guid>("Id"),
                                                      Name = c.Field<string>("Name"),
                                                      CardModule = SerializeHelper.DeserializeXMLChar<CardsXML>
                                                      (c.Field<string>("CardModuleXML")),
                                                      CreateTime = c.Field<DateTime>("CreateTime"),
                                                      UpdateTime = c.Field<DateTime>("UpdateTime"),
                                                      CreateBy = c.Field<string>("CreateBy"),
                                                      IsDelete = c.Field<Int16>("IsDelete"),
                                                      IsCheckout = c.Field<bool>("IsCheckout"),
                                                      FixedMapValues = c.Field<string>("FixedMapValues"),
                                                      DetailMapValues = c.Field<string>("DetailMapValues"),
                                                      TitleMapValues = c.Field<string>("TitleMapValues")
                                                  }).ToList<ProcessCardModule>();
                return cProcessCardModuleList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// add process card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int AddProcessPlanningModule(ProcessPlanningModule card)
        {
            int result;
            try
            {
                result = ProcessPlanningModuleDAL.AddProcessPlanningMoudle(card);
                card.CurrentNode = result;
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// add process planning module list
        /// </summary>
        /// <param name="moduleList"></param>
        public static void AddProcessPlanningModule(List<ProcessPlanningModule> moduleList)
        {
            try
            {
                ProcessPlanningModuleDAL.AddProcessPlanningMoudle(moduleList);                    
            }
            catch
            {
                throw;
            }
        
        }

        /// <summary>
        /// 方法说明：删除模版或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-24
        /// </summary>
        /// <param name="businessId">业务ID</param>
        /// <returns>True/False</returns>
        public static bool DeleteBusiness(Guid businessId)
        {
            bool result = true;

            try
            {
                result = ProcessPlanningModuleDAL.DeleteBusiness(businessId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：更换两个卡片模版的顺序
        /// 作    者：jason.tang
        /// 完成时间：2013-08-16
        /// </summary>
        /// <param name="movecardid">当前移动的节点ID</param>
        /// <param name="moveindex">当前移动的节点索引</param>
        /// <param name="targecardid">目标节点的ID</param>
        /// <param name="targeindex">目标节点的索引</param>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        public static bool ChangeTwoCardSortOrder(object movecardid, string moveindex, object targecardid, string targeindex, string parentNode)
        {
            bool result = false;
            try
            {
                result = ProcessPlanningModuleDAL.ChangeTwoCardSortOrder(movecardid, moveindex, targecardid, targeindex, parentNode);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：节点移动时更新节点和规程的关系
        /// 作    者：jason.tang
        /// 完成时间：2013-08-10
        /// </summary>
        /// <param name="parentNode">父节点ID</param>
        /// <param name="prevParenetNode">当前节点移动前的父节点</param>
        /// <param name="cardid">卡片ID</param>
        /// <returns>True/False</returns>
        public static bool UpdatePlanningModuleData(string parentNode, string prevParenetNode, object businessId)
        {
            bool result = false;
            try
            {
                result = ProcessPlanningModuleDAL.UpdatePlanningModuleData(parentNode, prevParenetNode, businessId);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
