using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Kingdee.CAPP.Model;
/*******************************
 * Created By franco
 * Description: Process planning module  data access layer
 *******************************/

namespace Kingdee.CAPP.DAL
{
    public class ProcessPlanningModuleDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// get process card list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetProcesPlanningModuleDatasetByParentNode(int parentNode)
        {

            try
            {
                string strsql = @"SELECT 
                               [ProcessPlanningModuleId]
                               ,[BussinessId]
                               ,[Name]
                               ,[Type]
                               ,[ParentNode]
                               ,[CurrentNode]
                               ,[Sort]                    
                            FROM [ProcessPlanningModule] 
                            WHERE [ParentNode] = @ParentNode order by sort ";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNode);
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
        /// Get name by planning procedure module
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataSet GetProcesPlanningModuleDatasetByName(string name)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"SELECT 
                               [ProcessPlanningModuleId]
                               ,[BussinessId]
                               ,[Name]
                               ,[Type]
                               ,[ParentNode]
                               ,[CurrentNode]
                               ,[Sort]                    
                            FROM [ProcessPlanningModule] 
                            WHERE PARENTNODE !=0 AND TYPE = 4");

                if (!string.IsNullOrEmpty(name))
                {
                    sb.AppendFormat(" AND Name like '%{0}%'", name);
                }
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
        /// get process card module list by process planning module'id list
        /// </summary>
        /// <param name="ProcessPlanningIds"></param>
        /// <returns></returns>
        public static DataSet GetProcessCardModuleListByProcessPlanningModuleIds(List<Guid> ProcessPlanningModuleIds)
        {
            if (ProcessPlanningModuleIds.Count <= 0)
            {
                return null;
            }

            try
            {
                StringBuilder sb = new StringBuilder();

                ProcessPlanningModuleIds.ForEach(x =>
                {
                    sb.AppendFormat("{0},", x.ToString());
                });

                StringBuilder sbsql = new StringBuilder();
                sbsql.AppendFormat(@"                            
                    SELECT 
                        Id,Name,FixedMapValues,DetailMapValues,TitleMapValues,isCheckOut,isDelete 
                    FROM dbo.ProcessCardModule 
                    WHERE Id 
                            IN(
	                    SELECT 
                            BussinessId 
                        FROM dbo.ProcessPlanningModule 
                        WHERE ParentNode 
                                IN(
		                    SELECT 
                                CurrentNode 
                            FROM dbo.ProcessPlanningModule 
			                WHERE ProcessPlanningModuleId 
                                    IN('{0}')))",
                                    sb.ToString().TrimEnd(','));

                using (DbCommand cmd = db.GetSqlStringCommand(sbsql.ToString()))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// get process card list by parentNode
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static DataSet GetProcessCardModuleDataSetByProcessPlanningNode(int parentNode)
        {
            string strsql = @"select * from ProcessCardModule a 
                                where exists(
		                            select bussinessId from ProcessPlanningModule  b 
		                                where b.parentNode = @parentNode and b.BussinessId = a.id)";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNode);
                DataSet ds = db.ExecuteDataSet(cmd);
                return ds;
            }
        }

        /// <summary>
        /// insert process card module
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static int AddProcessPlanningMoudle(ProcessPlanningModule module)
        {
            string strsql = @"INSERT INTO [dbo].[ProcessPlanningModule]
                                   (
                                    [ProcessPlanningModuleId]
                                   ,[BussinessId]
                                   ,[Name]
                                   ,[Type]
                                   ,[ParentNode]
                                   ,[Sort])
                             VALUES
                                   (
                                    @ProcessPlanningModuleId
                                   ,@BussinessId
                                   ,@Name
                                   ,@Type
                                   ,@ParentNode
                                   ,@Sort);
                            Select @@Identity;";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@ProcessPlanningModuleId", DbType.Guid, id);
                db.AddInParameter(cmd, "@BussinessId", DbType.Guid, module.BusinessId);
                db.AddInParameter(cmd, "@Name", DbType.String, module.Name);
                db.AddInParameter(cmd, "@Type", DbType.Int16, Convert.ToInt16(module.BType));
                db.AddInParameter(cmd, "@ParentNode", DbType.Int32, module.ParentNode);
                db.AddInParameter(cmd, "@Sort", DbType.Int32, module.Sort);


                object currentNode = db.ExecuteScalar(cmd);
                return Convert.ToInt32(currentNode);
            }
        }

        /// <summary>
        /// insert process card module
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static void AddProcessPlanningMoudle(List<ProcessPlanningModule> moduleList)
        {
            string strsql = @"INSERT INTO [dbo].[ProcessPlanningModule]
                                   (
                                    [ProcessPlanningModuleId]
                                   ,[BussinessId]
                                   ,[Name]
                                   ,[Type]
                                   ,[ParentNode]
                                   ,[Sort])
                             VALUES
                                   (
                                    @ProcessPlanningModuleId
                                   ,@BussinessId
                                   ,@Name
                                   ,@Type
                                   ,@ParentNode
                                   ,@Sort)
                            Select @@Identity;";



            using (DbConnection conn = db.CreateConnection())
            {
                conn.Open();
                DbTransaction trans = conn.BeginTransaction();
                DbCommand cmd = null;
                try
                {
                    foreach (ProcessPlanningModule ppm in moduleList)
                    {
                        cmd = db.GetSqlStringCommand(strsql);
                        Guid id = Guid.NewGuid();
                        db.AddInParameter(cmd, "@ProcessPlanningModuleId", DbType.Guid, id);
                        db.AddInParameter(cmd, "@BussinessId", DbType.Guid, ppm.BusinessId);
                        db.AddInParameter(cmd, "@Name", DbType.String, ppm.Name);
                        db.AddInParameter(cmd, "@Type", DbType.Int16, Convert.ToInt16(ppm.BType));
                        db.AddInParameter(cmd, "@ParentNode", DbType.Int32, ppm.ParentNode);
                        db.AddInParameter(cmd, "@Sort", DbType.Int32, ppm.Sort);
                        ppm.CurrentNode = Convert.ToInt32(db.ExecuteScalar(cmd));
                    }

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    cmd.Dispose();
                }
            }
        }

        /// <summary>
        /// 方法说明：删除卡片管理表模版或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-22
        /// </summary>
        /// <param name="businessId">业务ID</param>
        /// <returns>True/False</returns>
        public static bool DeleteBusiness(Guid businessId)
        {
            bool result = true;
            string strsql = @"Delete from ProcessPlanningModule where BussinessId=@BussinessId";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@BussinessId", DbType.Guid, businessId);
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：更换两个卡片节点的顺序
        /// 作    者：jason.tang
        /// 完成时间：2013-08-10
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

            string strsql = @"update ProcessPlanningModule  set 
                                Sort={0} 
                                where
                                ParentNode={1} And 
                                BussinessId='{2}';
                                update ProcessPlanningModule  set 
                                Sort={3} 
                                where
                                ParentNode={4} And 
                                BussinessId='{5}';";

            //moveindex = GetSort(moveindex, movecardid.ToString());
           // targeindex = GetSort(targeindex, targecardid.ToString());

            strsql = string.Format(strsql, int.Parse(moveindex), parentNode, targecardid.ToString(), int.Parse(targeindex), parentNode, movecardid.ToString());
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                result = db.ExecuteNonQuery(cmd) > 0;
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
                string strsql = @"update ProcessPlanningModule  set 
                                ParentNode='{0}',
                                Sort={1} 
                                where
                                BussinessId='{2}' And
                                ParentNode={3}";

                int sort = GetMaxSort(parentNode);

                strsql = string.Format(strsql, parentNode, sort, businessId.ToString(), prevParenetNode);
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
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
        /// 方法说明：根据父节点获取Sort
        /// 作   者：jason.tang
        /// 完成时间：2013-08-10
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns>最大的CardSort</returns>
        public static int GetMaxSort(string parentNode)
        {
            int result = 0;

            try
            {
                string strsql = @"select Max(Sort) from ProcessPlanningModule 
                                where ParentNode=@ParentNode";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentNode", DbType.Int32, int.Parse(parentNode));
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

        public static string GetSort(string currentNode, string bussiessId)
        {
            string result = "1";

            try
            {
                string strsql = @"select Sort from ProcessPlanningModule 
                                where CurrentNode=@CurrentNode And BussinessId=@BussinessId";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@CurrentNode", DbType.Int32, int.Parse(currentNode));
                    db.AddInParameter(cmd, "@BussinessId", DbType.Guid, new Guid(bussiessId));
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
                            return "1";
                        result = ds.Tables[0].Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
