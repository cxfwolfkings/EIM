using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.DAL;
using System.Data;
using Kingdee.CAPP.Common.ModuleHelper;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// 类型说明：SQL Server操作类BLL
    /// 作    者：jason.tang
    /// 完成时间：2013-03-25
    /// </summary>
    public class SqlServerControllerBLL
    {
        /// <summary>
        /// 方法说明：新建数据表
        /// 作    者：jason.tang
        /// 完成时间：2013-03-25
        /// </summary>
        /// <param name="tableName">数据表名</param>
        /// <param name="columnNames">列名集合</param>
        /// <returns>True/False</returns>
        public static bool CreateTable(string tableName, Dictionary<string, List<string>> columnNames)
        {
            bool result = false;
            try
            {
                foreach (string key in columnNames.Keys)
                {
                    string name = string.Format(tableName + "_" + key); 
                    result = SqlServerControllerDAL.CreateTable(name, columnNames[key].ToArray());

                    if (!result)
                    {
                        return false;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
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
        public static DataTable SearchProcessCardByContent(string content, string cardModuleId)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = SqlServerControllerDAL.SearchProcessCardByContent(content, cardModuleId);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataTable GetTableColumnByName(string name)
        {
            DataTable dt = new DataTable();

            try
            {
                DataSet ds = SqlServerControllerDAL.GetTableColumnByName(name);
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
        /// 方法说明：根据用户名获取用户信息
        /// 作      者：jason.tang
        /// 完成时间：2013-07-23
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public static DataTable GetUserInfo(string userName)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = SqlServerControllerDAL.GetUserInfo(userName);
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        /// <summary>
        /// 方法说明：获取基础资源库数据
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <returns></returns>
        public static List<BaseResource> GetBaseResource()
        {
            DataSet ds = SqlServerControllerDAL.GetBaseResource();

            ModleHandler<BaseResource> resourceHandler = new ModleHandler<BaseResource>();
            List<BaseResource> baseResourceList = new List<BaseResource>();
            try
            {
                baseResourceList = resourceHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return baseResourceList;
        }

        /// <summary>
        /// 方法说明：根据父字段获取对应的子字段
        /// 作    者：jason.tang
        /// 完成时间：2013-11-04
        /// </summary>
        /// <param name="parentField">父字段</param>
        /// <returns></returns>
        public static List<BaseResourceField> GetResourceField(string parentField)
        {
            DataSet ds = SqlServerControllerDAL.GetResourceField(parentField);

            ModleHandler<BaseResourceField> resourceHandler = new ModleHandler<BaseResourceField>();
            List<BaseResourceField> baseResourceFieldList = new List<BaseResourceField>();
            try
            {
                baseResourceFieldList = resourceHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return baseResourceFieldList;
        }
    }
}
