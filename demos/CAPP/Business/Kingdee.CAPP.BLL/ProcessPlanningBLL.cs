using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using System.Data;
using Kingdee.CAPP.DAL;
/*******************************
 * Created By franco
 * Description: Process Procedure business access layer
 *******************************/

namespace Kingdee.CAPP.BLL
{
    public class ProcessPlanningBLL
    {
        /// <summary>
        /// get process planning  by condition
        /// </summary>
        /// <returns></returns>
        public static List<ProcessPlanning> GetProcesPlanningList(string name)
        {
            try
            {
                DataTable dt = ProcessPlanningDAL.GetProcesPlanningDataSet(name).Tables[0];

                var cProcessPlanningList = (from c in dt.AsEnumerable()
                                            select new ProcessPlanning()
                                            {
                                                ProcessPlanningId = c.Field<Guid>("ProcessPlanningId"),
                                                Name = c.Field<string>("Name"),
                                                Sort = c.Field<int>("Sort"),
                                                FolderId = c.Field<string>("FolderId")
                                            }).ToList<ProcessPlanning>();
                return cProcessPlanningList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// add process planning module
        /// </summary>
        /// <returns></returns>
        public static Guid AddProcesPlanning(ProcessPlanning processPlanning)
        {
            try
            {
                Guid id = ProcessPlanningDAL.AddProcesPlanningData(processPlanning);
                return id;
            }
            catch
            {
                throw;
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

            try
            {
                result = ProcessPlanningDAL.DeletePlanningById(planningId);
            }
            catch
            {
                throw;
            }

            return result;
        }
    }
}
