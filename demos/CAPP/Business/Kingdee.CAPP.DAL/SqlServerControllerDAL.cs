using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;

namespace Kingdee.CAPP.DAL
{
    /// <summary>
    /// 类型说明：SQL Server操作类DAL
    /// 作    者：jason.tang
    /// 完成时间：2013-03-25
    /// </summary>
    public class SqlServerControllerDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// 方法说明：新建数据表
        /// 作    者：jason.tang
        /// 完成时间：2013-03-25
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="columnNames">列名集合</param>
        /// <returns>True/False</returns>
        public static bool CreateTable(string tableName, string[] columnNames)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" CREATE TABLE ");
                query.Append(tableName);
                query.Append(" ( [Id] [uniqueidentifier] NOT NULL, ");

                for (int i = 0; i < columnNames.Length; i++)
                {
                    query.Append(columnNames[i]);
                    query.Append(" [nvarchar](max) NULL");
                    //query.Append(columnTypes[i]);
                    query.Append(", ");
                }

                if (columnNames.Length > 1) { query.Length -= 2; }  //Remove trailing ", "
                query.Append(")");

                DropTable(tableName);

                using (DbCommand cmd = db.GetSqlStringCommand(query.ToString()))
                {
                    db.ExecuteNonQuery(cmd);
                    return true;
                }

                //using (SqlCommand cmd = new SqlCommand("CREATE TABLE [dbo].['" + TName + "']("
                //                + "[ID] [int] IDENTITY(1,1) NOT NULL,"
                //                + "[DateTime] [date] NOT NULL,"
                //                + "[BarCode] [nvarchar](max) NOT NULL,"
                //                + "[ArtNumber] [nvarchar](max) NOT NULL,"
                //                + "[ProductName] [nvarchar](50) NOT NULL,"
                //                + "[Quantity] [int] NOT NULL,"
                //                + "[SelfPrice] [decimal](18, 2) NOT NULL,"
                //                + "[Price] [decimal](18, 2) NOT NULL,"
                //                + "[Disccount] [int] NULL,"
                //                + "[Comment] [nvarchar](max) NULL,"
                //                + "CONSTRAINT ['" + TName + "'] PRIMARY KEY CLUSTERED "
                //                + "("
                //                + "[ID] ASC"
                //                + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                //                + ") ON [PRIMARY]", new SqlConnection(ConString)))
                //{
                //    cmd.Connection.Open();
                //    cmd.ExecuteNonQuery();
                //    cmd.Connection.Close();
                //}
            }
            catch
            {                
                //throw ex;
                return false;
            }
        }

        /// <summary>
        /// 方法说明：新增表前，先删除
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static bool DropTable(string tableName)
        {
            try
            {
                string query = string.Format(" DROP TABLE {0}", tableName);
                
                using (DbCommand cmd = db.GetSqlStringCommand(query))
                {
                    db.ExecuteNonQuery(cmd);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 方法说明：根据内容查找工艺文件
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="cardModuleId">模版ID</param>
        /// <returns></returns>
        public static DataSet SearchProcessCardByContent(string content, string cardModuleId)
        {
            try
            {
                string strsql = @"ALTER DATABASE PLM  
                                SET ARITHABORT ON
                                select ID from dbo.ProcessCard 
                                where CardModuleId='{0}' and
                                (isnull(cast(CardXml.query('//Cell[@Content=''{1}'']') as varchar(max)),'') != ''
                                or isnull(cast(CardXml.query('//DetailCell[@ColumnValue=''{2}'']') as varchar(max)),'') != '')";

                strsql = string.Format(strsql, cardModuleId, content, content);

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {                    
                    DataSet ds = db.ExecuteDataSet(cmd);

                    if(ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        List<string> lstCardIds = new List<string>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            lstCardIds.Add(string.Format("'{0}'", row[0].ToString()));
                        }

                        DataSet dataset = GetProcessCardByCardId(string.Join(",", lstCardIds.ToArray()));
                        
                        return dataset;
                    }

                    return ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：根据卡片ID获取卡片对应的信息
        /// 作    者：jason.tang
        /// 完成时间：2013-03-27
        /// </summary>
        /// <param name="cardid">卡片ID</param>
        /// <returns></returns>
        private static DataSet GetProcessCardByCardId(string cardid)
        {
            try
            {
                string strsql = @"select t7.name, drawnumber, t6.name processName,t3.status,t7.createtime, '' pagenumber, 
                                    K3BOMNumber prodrawnumber from materialcardrelation t1
                                    left join pp_pbom t2 on t1.MaterialId = t2.ObjectId
                                    left join pp_pbomver t3 on t2.pbomid = t3.pbomid
                                    left join PP_PBOMVerRouting t4 on t3.VerId = t4.VerId
                                    left join PP_RoutingOper t5 on t4.routingid = t5.routingid
                                    left join pp_oper t6 on t5.operid = t6.operid
                                    left join processcard t7 on t1.ProcessCardId = t7.Id
                                    left join MAT_MaterialVersion t8 on t1.materialid = t8.baseid
                                    inner join MAT_MaterialBase t9 on t8.baseid = t9.baseid
                                    where t1.ProcessCardId in ({0})";

                strsql = string.Format(strsql, cardid);
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
        /// 方法说明：获取列名
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DataSet GetTableColumnByName(string name)
        {
            try
            {
                string strsql = @"SELECT Name FROM SysColumns WHERE id=Object_Id('{0}')";

                strsql = string.Format(strsql, name);
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
        /// 方法说明：根据用户名获取用户信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-23
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static DataSet GetUserInfo(string userName)
        {
            try
            {
                string strsql = @"SELECT * FROM SM_Users WHERE UserCode=@UserCode";
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@UserCode", DbType.String, userName);
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
        /// 方法说明：获取基础资源库数据
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <returns></returns>
        public static DataSet GetBaseResource()
        {
            try
            {
                string strsql = @"SELECT * FROM PP_BaseResource ";
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
        /// 方法说明：根据父字段获取对应的子字段
        /// 作    者：jason.tang
        /// 完成时间：2013-11-04
        /// </summary>
        /// <param name="parentField">父字段</param>
        /// <returns></returns>
        public static DataSet GetResourceField(string parentField)
        {
            try
            {
                string strsql = @"select * from dbo.PP_BaseResourceField where ParentField=@ParentField";
                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@ParentField", DbType.String, parentField);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    return ds;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
