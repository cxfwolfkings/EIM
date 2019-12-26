using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：典型工艺DAL
    /// 作      者：jason.tang
    /// 完成时间：2013-06-20
    /// </summary>
    public class TypicalProcessDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// 方法说明：根据父节点获取工艺文件
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        public static DataSet GetTypicalProcesByParentNode(int parentNode)
        {

            try
            {
                string strsql = @"SELECT 
                               [TypicalProcessId]
                               ,[BussinessId]
                               ,[Name]
                               ,[Type]
                               ,[ParentNode]
                               ,[CurrentNode]
                               ,[Sort]                    
                            FROM [TypicalProcess] 
                            WHERE [ParentNode] = @ParentNode";

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
        /// 方法说明：根据名称获取典型工艺类型
        /// 作者：jason.tang
        /// 完成时间：2013-07-23
        /// </summary>
        /// <param name="name">类型名</param>
        /// <returns></returns>
        public static DataSet GetTypicalCategory(string name)
        {
            try
            {
                string strsql = @"SELECT Name, CurrentNode, Type FROM TypicalProcess ";
                if (!string.IsNullOrEmpty(name))
                {
                    strsql += string.Format(" Where Name like '%{0}%' ", name);
                }
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 方法说明：检查卡片是否已转为典型
        /// 作      者：jason.tang
        /// 完成时间：2013-07-24
        /// </summary>
        /// <param name="businessId">卡片业务ID</param>
        /// <param name="parentNode">分类</param>
        /// <returns></returns>
        public static DataSet ExistTypcialProcessCard(Guid businessId, int parentNode)
        {
            try
            {
                string strsql = @"SELECT * FROM TypicalProcess Where BussinessId=@BussinessId And ParentNode=@ParentNode";
                
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@BussinessId", DbType.Guid, businessId);
                    db.AddInParameter(cmd, "@ParentNode", DbType.Int32, parentNode);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch
            {
                return null;
            }
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
            string strsql = @"INSERT INTO [dbo].[TypicalProcess]
                                   (
                                    [TypicalProcessId]
                                   ,[BussinessId]
                                   ,[Name]
                                   ,[Type]
                                   ,[ParentNode]
                                   ,[Sort])
                             VALUES
                                   (
                                    @TypicalProcessId
                                   ,@BussinessId
                                   ,@Name
                                   ,@Type
                                   ,@ParentNode
                                   ,@Sort);
                            Select @@Identity;";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@TypicalProcessId", DbType.Guid, id);
                db.AddInParameter(cmd, "@BussinessId", DbType.Guid, typical.BusinessId);
                db.AddInParameter(cmd, "@Name", DbType.String, typical.Name);
                db.AddInParameter(cmd, "@Type", DbType.Int16, Convert.ToInt16(typical.BType));
                db.AddInParameter(cmd, "@ParentNode", DbType.Int32, typical.ParentNode);
                db.AddInParameter(cmd, "@Sort", DbType.Int32, typical.Sort);


                object currentNode = db.ExecuteScalar(cmd);
                return Convert.ToInt32(currentNode);
            }
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
            string strsql = @"INSERT INTO [dbo].[TypicalProcess]
                                   (
                                    [TypicalProcessId]
                                   ,[BussinessId]
                                   ,[Name]
                                   ,[Type]
                                   ,[ParentNode]
                                   ,[Sort])
                             VALUES
                                   (
                                    @TypicalProcessId
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
                    foreach (TypicalProcess ppm in typicalList)
                    {
                        cmd = db.GetSqlStringCommand(strsql);
                        Guid id = Guid.NewGuid();
                        db.AddInParameter(cmd, "@TypicalProcessId", DbType.Guid, id);
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
        /// 方法说明：根据ID删除典型工艺
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="typicalprocessid">典型工艺ID</param>
        /// <returns></returns>
        public static bool DeleteTypicalById(Guid typicalprocessid)
        {
            bool result = true;

            string strsql = @"Delete from TypicalProcess Where TypicalProcessId=@TypicalProcessId";
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@TypicalProcessId", DbType.Guid, typicalprocessid);
                result = db.ExecuteNonQuery(cmd) > 0;
            }

            return result;
        }
    }
}
