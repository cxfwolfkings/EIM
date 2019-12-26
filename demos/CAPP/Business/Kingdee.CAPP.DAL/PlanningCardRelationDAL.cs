using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.DAL
{
    public class PlanningCardRelationDAL
    {
        private static Database db = DatabaseFactory.Instance();
        /// <summary>
        /// get process planning list by process planning id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetPlanningCardRelationDataSetByProcessPlanningId(Guid processPlanningId)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" 
                            SELECT
                                    * 
                            FROM 
                                PlanningCardRelation 
                            WHERE ProcessPlanningId =@ProcessPlanningId");

                using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    db.AddInParameter(cmd, "@ProcessPlanningId", DbType.String, processPlanningId);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get planning card relation dataset by process planning id
        /// </summary>
        /// <param name="processPlanningId"></param>
        /// <returns></returns>
        public static DataSet GetPlanningCardRelationDataSetByprocessCardId(Guid processCardId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" 
                            SELECT
                                    * 
                            FROM 
                                PlanningCardRelation 
                            WHERE ProcessCardId =@ProcessCardId");

                using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, processCardId);
                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        /// <summary>
        /// get process planning list by ProcessCard id
        /// 根据工艺卡片数据集
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetProcesCardDataSetByProcessPlanningId(Guid ProcessPlanningId)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(@" 
                            SELECT 
		                    Id,Name,CardModuleId,CreateTime,CreateBy,IsCheckOut,IsDelete,UpdateTime, CardSort  
	                        FROM  dbo.ProcessCard t1
	                        left join dbo.PlanningCardRelation t2
	                        on t1.Id = t2.ProcessCardId
	                        WHERE ProcessPlanningId = '{0}' order by cardsort ", ProcessPlanningId);

                using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
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
        /// get process card list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Guid AddProcesPlanningData(PlanningCardRelation planningCardRelation)
        {
            try
            {
                string strsql = @"
                                INSERT INTO [dbo].[PlanningCardRelation]
                                   ([PlanningCardRelationId]
                                   ,[ProcessPlanningId]
                                   ,[ProcessCardId]
                                   ,[CardSort])
                             VALUES
                                   (@PlanningCardRelationId
                                   ,@ProcessPlanningId
                                   ,@ProcessCardId                                
                                   ,@CardSort);";

                int cardSort = GetMaxCardSort(planningCardRelation.ProcessPlanningId);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@PlanningCardRelationId", DbType.Guid, planningCardRelation.PlanningCardRelationId);
                    db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, planningCardRelation.ProcessPlanningId);
                    db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, planningCardRelation.ProcessCardId);
                    db.AddInParameter(cmd, "@CardSort", DbType.Int32, cardSort);
                    db.ExecuteScalar(cmd);
                    return planningCardRelation.PlanningCardRelationId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：根据规程ID获取CardSort
        /// 作   者：jason.tang
        /// 完成时间：2013-03-13
        /// </summary>
        /// <param name="planningid">规程ID</param>
        /// <returns>最大的CardSort</returns>
        public static int GetMaxCardSort(Guid planningid)
        {
            int result = 0;

            try
            {
                string strsql = @"select Max(CardSort) from planningcardrelation 
                                where ProcessPlanningId=@ProcessPlanningId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, planningid);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                        {
                            return 1;
                        }
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
        /// 方法说明：节点移动时更新节点和规程的关系
        /// 作    者：jason.tang
        /// 完成时间：2013-03-15
        /// </summary>
        /// <param name="planningid">规程ID</param>
        /// <param name="prevPlanningid">当前节点移动前的规程ID</param>
        /// <param name="cardid">卡片ID</param>
        /// <returns>True/False</returns>
        public static bool UpdateProcessPlanningData(object planningid, object prevPlanningid, object cardid)
        {
            bool result = false;

            try
            {
                string strsql = @"update planningcardrelation  set 
                                ProcessPlanningId='{0}',
                                CardSort={1} 
                                where
                                ProcessPlanningId='{2}' and 
                                ProcessCardId='{3}'";

                int cardSort = GetMaxCardSort(new Guid(planningid.ToString()));

                strsql = string.Format(strsql, planningid, cardSort, prevPlanningid, cardid);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    //db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, new Guid(planningid.ToString()));
                    //db.AddInParameter(cmd, "@CardSort", DbType.Int16, cardSort);
                    //db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, new Guid(cardid.ToString()));

                    result = db.ExecuteNonQuery(cmd) > 0;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// <returns></returns>
        public static bool ChangeTwoCardSortOrder(object movecardid, string moveindex, object targecardid, string targeindex, string processPlanningId)
        {
            bool result = false;

            string strsql = @"update planningcardrelation  set 
                                CardSort={0} 
                                where
                                ProcessCardId='{1}' And 
                                ProcessPlanningId='{2}';
                                update planningcardrelation  set 
                                CardSort={3} 
                                where
                                ProcessCardId='{4}' And 
                                ProcessPlanningId='{5}'";

            strsql = string.Format(strsql, int.Parse(moveindex), targecardid.ToString(), processPlanningId, int.Parse(targeindex), movecardid.ToString(), processPlanningId);
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                
                result = db.ExecuteNonQuery(cmd) > 0;

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

            string strsql = @"Delete from PlanningCardRelation Where ProcessPlanningId=@ProcessPlanningId And ProcessCardId=@ProcessCardId";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, planningId);
                db.AddInParameter(cmd, "@ProcessCardId", DbType.Guid, cardId);
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }
    
    }
}
