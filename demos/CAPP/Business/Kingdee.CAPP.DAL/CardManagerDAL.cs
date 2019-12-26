using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
/*******************************
 * Created By franco
 * Description:Card Manager Data Access layer
 *******************************/

namespace Kingdee.CAPP.DAL
{
    public class CardManagerDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// get CardManager By ParentNode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetCardManagerByParentNode(int parentNode)
        {
            try
            {
                string strsql = @"Select 
                                [ProcessModuleId]
                                  ,[BusinessId]
                                  ,[Name]
                                  ,[Type]
                                  ,[ParentNode]
                                  ,[CurrentNode]                    
                            from [CardManager] WHERE [ParentNode] = @ParentNode";

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
        /// defalut display level
        /// </summary>
        /// <param name="parentNodeLevel"></param>
        /// <returns></returns>
        public static DataSet GetDefalutCardManager(int parentNodeLevel)
        {
            try
            {
                string strsql = @"Select 
                                [ProcessModuleId]
                                  ,[BusinessId]
                                  ,[Name]
                                  ,[Type]
                                  ,[ParentNode]
                                  ,[CurrentNode]                    
                            from [CardManager] WHERE [ParentNode] <= @ParentNode";
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNodeLevel);
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
        /// defalut display level
        /// </summary>
        /// <param name="parentNodeLevel"></param>
        /// <returns></returns>
        public static int AddBusiness(string name, int type, int parentNode,Guid businessId)
        {
            string strsql = @"INSERT INTO [dbo].[CardManager]
                                   ([ProcessModuleId]
                                    ,[BusinessId]
                                    ,[Name]
                                    ,[Type]
                                    ,[ParentNode])
                            VALUES
                                   (@ProcessModuleId,
                                    @BusinessId,
                                    @Name,
                                    @Type,
                                    @ParentNode);
                            Select @@Identity";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ProcessModuleId", DbType.Guid, Guid.NewGuid());
                db.AddInParameter(cmd, "@BusinessId", DbType.Guid, businessId);
                db.AddInParameter(cmd, "@Name", DbType.String, name);
                db.AddInParameter(cmd, "@Type", DbType.Int16, type);
                db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNode);
                object currentNode = db.ExecuteScalar(cmd);
                return Convert.ToInt32(currentNode);
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
            string strsql = @"Delete from CardManager where BusinessId=@BusinessId";
            using(DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@BusinessId", DbType.Guid, businessId);
                 result = db.ExecuteNonQuery(cmd) > 0;              
            }
            if (result)
                DeleteCardModule(businessId);

            return result;
        }

        /// <summary>
        /// 方法说明：删除卡片模版
        /// 作      者：jason.tang
        /// 完成时间：2013-07-24
        /// </summary>
        /// <param name="businessId">业务ID</param>
        private static void DeleteCardModule(Guid businessId)
        {
            string strsql = @"Delete from ProcessCardModule where Id=@Id";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@Id", DbType.Guid, businessId);
                db.ExecuteNonQuery(cmd);
            }
        }
    }
}
