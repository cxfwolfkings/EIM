using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using System.Data;
using Kingdee.CAPP.DAL;

/*******************************
 * Created By franco
 * Description: PlanningCardRelation business access layer
 *******************************/

namespace Kingdee.CAPP.BLL
{
    public class PlanningCardRelationBLL
    {
        /// <summary>
        /// get process planning Relation list by processPlanningId
        /// </summary>
        /// <returns></returns>
        public static List<PlanningCardRelation> GetPlanningCardRelationListByProcessPlanningId(Guid ProcessPlanningId)
        {
            try
            {
                DataTable dt = PlanningCardRelationDAL
                    .GetPlanningCardRelationDataSetByProcessPlanningId(ProcessPlanningId).Tables[0];

                var cPlanningCardRelationList = (from c in dt.AsEnumerable()
                                                 select new PlanningCardRelation()
                                            {
                                                PlanningCardRelationId = c.Field<Guid>("PlanningCardRelationId"),
                                                ProcessPlanningId = c.Field<Guid>("ProcessPlanningId"),
                                                ProcessCardId = c.Field<Guid>("ProcessCardId"),
                                                CardSort = c.Field<int>("CardSort")
                                            }).ToList<PlanningCardRelation>();
                return cPlanningCardRelationList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// get process planning Relation list by processPlanningId
        /// </summary>
        /// <returns></returns>
        public static List<PlanningCardRelation> GetPlanningCardRelationListByProcessCardId(Guid ProcessCardId)
        {
            try
            {
                DataTable dt = PlanningCardRelationDAL
                    .GetPlanningCardRelationDataSetByprocessCardId(ProcessCardId).Tables[0];

                var cPlanningCardRelationList = (from c in dt.AsEnumerable()
                                                 select new PlanningCardRelation()
                                                 {
                                                     PlanningCardRelationId = c.Field<Guid>("PlanningCardRelationId"),
                                                     ProcessPlanningId = c.Field<Guid>("ProcessPlanningId"),
                                                     ProcessCardId = c.Field<Guid>("ProcessCardId"),
                                                     CardSort = c.Field<int>("CardSort")
                                                 }).ToList<PlanningCardRelation>();
                return cPlanningCardRelationList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据工艺规程Id得到工艺卡片列表
        /// </summary>
        /// <param name="ProcessPlanningId"></param>
        /// <returns></returns>
        public static List<ProcessCard> GetProcessCardListByProcessPlanningId(Guid ProcessPlanningId)
        {
            try
            {
                DataTable dt = PlanningCardRelationDAL
                    .GetProcesCardDataSetByProcessPlanningId(ProcessPlanningId).Tables[0];

                var cProcessCardList = (from c in dt.AsEnumerable()
                                        select new ProcessCard()
                                        {
                                            ID = c.Field<Guid>("Id"),
                                            Name = c.Field<String>("Name"),
                                            CardModuleId = c.Field<Guid>("CardModuleId"),
                                            CreateBy = c.Field<String>("CreateBy"),
                                            CreateTime = c.Field<DateTime>("CreateTime"),
                                            IsCheckOut = c.Field<bool>("IsCheckOut"),
                                            IsDelete = c.Field<Int16>("IsDelete"),
                                            UpdateTime = c.Field<DateTime>("UpdateTime"),
                                            CardSort = c.Field<int>("CardSort")
                                        }
                                        ).ToList<ProcessCard>();

                return cProcessCardList;
            }
            catch
            {
                throw;
            }
        }

        public static Guid AddProcesPlanningData(PlanningCardRelation planningCardRelation)
        {
            try
            {
                Guid id = PlanningCardRelationDAL.AddProcesPlanningData(planningCardRelation);
                return id;
            }
            catch
            {
                
                throw;
            }

        }

        /// <summary>
        /// 方法说明：节点移动时更新节点和规程的关系
        /// 作    者：jason.tang
        /// 完成时间：2013-03-15
        /// </summary>
        /// <param name="planningid">规程ID</param>
        /// <param name="prevPlanningid">当前节点移动前的规程ID</param>
        /// <param name="cardid">卡片ID</param>
        /// <returns></returns>
        public static bool UpdateProcessPlanningData(object planningid, object prevPlanningid, object cardid)
        {
            bool result = false;
            try
            {
                result = PlanningCardRelationDAL.UpdateProcessPlanningData(planningid, prevPlanningid, cardid);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：更换两个卡片节点的顺序
        /// 作    者：jason.tang
        /// 完成时间：2013-03-15
        /// </summary>
        /// <param name="movecardid">当前移动的节点ID</param>
        /// <param name="moveindex">当前移动的节点索引</param>
        /// <param name="targecardid">目标节点的ID</param>
        /// <param name="targeindex">目标节点的索引</param>
        /// <param name="processPlanningId">规程ID</param>
        /// <returns></returns>
        public static bool ChangeTwoCardSortOrder(object movecardid, string moveindex, object targecardid, string targeindex, string processPlanningId)
        {
            bool result = false;
            try
            {
                result = PlanningCardRelationDAL.ChangeTwoCardSortOrder(movecardid, moveindex, targecardid, targeindex, processPlanningId);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：根据规程ID和卡片ID删除规程与卡片的关系
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="cardId">卡片ID</param>
        /// <param name="planningId">工艺规程ID</param>
        /// <returns></returns>
        public static bool DeleteRelationByCardId(Guid cardId, Guid planningId)
        {
            bool result = true;

            try
            {
                result = PlanningCardRelationDAL.DeleteRelationByCardId(cardId, planningId);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
