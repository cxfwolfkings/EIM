using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.Common;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.Common.Serialize;

namespace Kingdee.CAPP.DAL
{
    public class ProcessCardModuleDAL
    {
        private static Database db = DatabaseFactory.Instance();

        /// <summary>
        /// get process card list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataRow GetProcessCardDataRow(Guid id)
        {
            try
            {

                string strsql = @"Select 
                                Id,
                                Name,
                                CardModuleXML,
                                FixedMapValues,
                                DetailMapValues,
                                TitleMapValues,
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout                    
                            from ProcessCardModule WHERE Id = @Id";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Id", DbType.Guid, id);
                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables[0] != null
                        || ds.Tables[0].Rows.Count > 0)
                        return ds.Tables[0].Rows[0];
                    else
                        return null;

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
        public static DataSet GetProcessCardDataset(Guid id)
        {

            try
            {
                string strsql = @"Select 
                                Id,
                                Name,
                                FixedMapValues,
                                DetailMapValues,
                                TitleMapValues,
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout                    
                            from ProcessCardModule WHERE Id = @Id";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Id", DbType.Guid, id);
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
        /// 方法说明：获取所有的卡片模版
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <returns>DataSet</returns>
        public static DataSet GetProcessCardData()
        {
            try
            {
                string strsql = @"Select 
                                Id,
                                Name                    
                            from ProcessCardModule";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
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
        /// get default process card list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetDefaultProcessCardDataset()
        {
            try
            {
                string strsql = @"Select  top 100
                                Id,
                                Name,
                                FixedMapValues,
                                DetailMapValues,
                                TitleMapValues,
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout,
                                (select t2.name from cardmanager t1
								left join cardmanager t2 
								on t1.parentnode = t2.currentnode
								where t1.businessid=id) typename                     
                            from ProcessCardModule Order by CreateTime";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
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
        /// get default process card list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataSet GetProcessCardDatasetByCondition(string processCardName)
        {
            try
            {
                string strsql = @"Select  top 100
                                Id,
                                Name,
                                FixedMapValues,
                                DetailMapValues,
                                TitleMapValues,
                                CreateTime,
                                CreateBy,
                                UpdateTime,
                                IsDelete,
                                IsCheckout                    
                            from ProcessCardModule
                            where Name like '%" + processCardName + @"%' 
                            order by CreateTime";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    //db.AddInParameter(cmd, "@Name", DbType.String, processCardName);
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
        /// 方法说明：检查模版名称是否存在
        /// 作      者：jason.tang
        /// 完成时间：2013-07-26
        /// </summary>
        /// <param name="processCardName"></param>
        /// <returns></returns>
        public static DataSet CheckModuleNameExist(string processCardName)
        {
            try
            {
                string strsql = @"Select Id, Name from ProcessCardModule where Name = @Name";

                using (DbCommand cmd = db.GetSqlStringCommand(strsql))
                {
                    db.AddInParameter(cmd, "@Name", DbType.String, processCardName);
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
        /// get CardModule card list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetCardModuleXML(Guid id)
        {
            string strsql = @"Select 
                                CardModuleXML
                            from ProcessCardModule WHERE Id = @Id";

            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@Id", DbType.Guid, id);

                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    string cardmodulexml = string.Empty;
                    if (dr.Read())
                    {
                        cardmodulexml = dr[0].ToString();
                    }
                    return cardmodulexml;
                }
            }
        }

        /// <summary>
        /// insert process card module
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static Guid AddProcessCard(Kingdee.CAPP.Model.ProcessCardModule card)
        {
            string strsql = @"INSERT INTO [dbo].[ProcessCardModule]
                                   (
                                    [ID]
                                   ,[Name]
                                   ,[CardModuleXML]
                                   ,[FixedMapValues]
                                   ,[DetailMapValues]
                                   ,[TitleMapValues]
                                   ,[CreateTime]
                                   ,[CreateBy]
                                   ,[UpdateTime]
                                   ,[IsDelete]
                                   ,[IsCheckout])
                             VALUES
                                   (
                                    @ID
                                   ,@Name
                                   ,@CardModuleXML
                                   ,@FixedMapValues
                                   ,@DetailMapValues
                                   ,@TitleMapValues
                                   ,@CreateTime
                                   ,@CreateBy
                                   ,@UpdateTime
                                   ,@IsDelete
                                   ,@IsCheckout)";

            string xmlstr = SerializeHelper.Serialize<CardsXML>(card.CardModule);
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                Guid id = Guid.NewGuid();
                db.AddInParameter(cmd, "@ID", DbType.Guid, id);
                db.AddInParameter(cmd, "@Name", DbType.String, card.Name);
                db.AddInParameter(cmd, "@CardModuleXML", DbType.String, xmlstr);
                db.AddInParameter(cmd, "@FixedMapValues", DbType.String, card.FixedMapValues);
                db.AddInParameter(cmd, "@DetailMapValues", DbType.String, card.DetailMapValues);
                db.AddInParameter(cmd, "@TitleMapValues", DbType.String, card.TitleMapValues);
                db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@CreateBy", DbType.String, card.CreateBy);
                db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@IsDelete", DbType.Int16, 0);
                db.AddInParameter(cmd, "@IsCheckout", DbType.Boolean, false);


                db.ExecuteScalar(cmd);
                card.Id = id;
                return id;
            }
        }

        /// <summary>
        /// Modified process cardmodule
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static bool UpdateProcessCardModule(Kingdee.CAPP.Model.ProcessCardModule card)
        {
            string strsql = @"UPDATE [dbo].[ProcessCardModule]                                   
                                   set [CardModuleXML]=@CardModuleXML
                                   ,[FixedMapValues]=@FixedMapValues
                                   ,[DetailMapValues]=@DetailMapValues
                                   ,[TitleMapValues]=@TitleMapValues
                                   ,[CreateTime]=@CreateTime
                                   ,[CreateBy]=@CreateBy
                                   ,[UpdateTime]=@UpdateTime
                                   ,[IsDelete]=@IsDelete
                                   ,[IsCheckout]=@IsCheckout
                             where ID=@ID";

            string xmlstr = SerializeHelper.Serialize<CardsXML>(card.CardModule);
            using (DbCommand cmd = db.GetSqlStringCommand(strsql))
            {
                db.AddInParameter(cmd, "@ID", DbType.Guid, card.Id);
                db.AddInParameter(cmd, "@CardModuleXML", DbType.String, xmlstr);
                db.AddInParameter(cmd, "@FixedMapValues", DbType.String, card.FixedMapValues);
                db.AddInParameter(cmd, "@DetailMapValues", DbType.String, card.DetailMapValues);
                db.AddInParameter(cmd, "@TitleMapValues", DbType.String, card.TitleMapValues);
                db.AddInParameter(cmd, "@CreateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@CreateBy", DbType.String, card.CreateBy);
                db.AddInParameter(cmd, "@UpdateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(cmd, "@IsDelete", DbType.Int16, 0);
                db.AddInParameter(cmd, "@IsCheckout", DbType.Boolean, false);


                db.ExecuteScalar(cmd);
                return true;
            }            
        }

    }
}
