using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Kingdee.CAPP.Model;
/*******************************
 * Created By franco
 * Description: Process planning data access layer
 *******************************/

namespace Kingdee.CAPP.DAL
{
    public class ProcessPlanningDAL
    {
        private static Database db = DatabaseFactory.Instance();
        /// <summary>
        /// get process card list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetProcesPlanningDataSet(string name)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" SELECT
                                    p.*, v.FolderId 
                            FROM 
                                ProcessPlanning p
                                left join PP_PCVersion v
                                on p.ProcessPlanningId = v.BaseId
                            WHERE IsDelete = 0 ");

                if (!string.IsNullOrEmpty(name))
                {
                    sb.AppendFormat(" And name like '%@name%'");
                }

                using (DbCommand cmd = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    db.AddInParameter(cmd, "@name", DbType.String, name);
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
        public static Guid AddProcesPlanningData(ProcessPlanning processPlanning)
        {
            try
            {
                string strsql = @"
                                INSERT INTO [dbo].[ProcessPlanning]
                                   ([ProcessPlanningId]
                                   ,[Name]
                                   ,[CreateTime]
                                   ,[CreateBy]
                                   ,[UpdateTime]
                                   ,[IsDelete]
                                   ,[IsCheckOut])
                             VALUES
                                   (@ProcessPlanningId
                                   ,@Name
                                   ,@CreateTime
                                   ,@CreateBy
                                   ,@UpdateTime
                                   ,@IsDelete
                                   ,@IsCheckOut);";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, processPlanning.ProcessPlanningId);
                    db.AddInParameter(cmd, "@Name", DbType.String, processPlanning.Name);
                    db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@CreateBy", DbType.String, processPlanning.CreateBy);
                    db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(cmd, "@IsDelete", DbType.Int16, 0);
                    db.AddInParameter(cmd, "@IsCheckOut", DbType.Boolean, false);

                    db.ExecuteScalar(cmd);
                    return processPlanning.ProcessPlanningId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：根据规程ID删除规程
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="planningId">工艺规程ID</param>
        /// <returns></returns>
        public static bool DeletePlanningById(Guid planningId)
        {
            bool result = true;

            string strsql = @"Delete from ProcessPlanning Where ProcessPlanningId=@ProcessPlanningId";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ProcessPlanningId", DbType.Guid, planningId);
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }
    }
}
