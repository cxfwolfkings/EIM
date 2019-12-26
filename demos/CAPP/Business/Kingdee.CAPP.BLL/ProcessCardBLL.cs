using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;
using System.Data;
using Kingdee.CAPP.Common.ModuleHelper;
using Kingdee.CAPP.Common.Serialize;
/*******************************
 * Created By franco
 * Description: process card business layer
 *******************************/


namespace Kingdee.CAPP.BLL
{
    public class ProcessCardBLL
    {
        /// <summary>
        /// 方法说明：根据ID获取卡片数据
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <param name="Id">卡片ID</param>
        /// <returns></returns>
        public ProcessCard GetProcessCard(Guid Id)
        {
            //LazyProcessCard 
            List<LazyProcessCard> lazyProcessCardModuleList = new List<LazyProcessCard>();
            LazyProcessCard card = new LazyProcessCard();
            try
            {
                DataSet ds = ProcessCardDAL.GetProcessCardDataset(Id);

                ModleHandler<LazyProcessCard> processCardHandler
                    = new ModleHandler<LazyProcessCard>();


                lazyProcessCardModuleList = processCardHandler.GetModelByDataSet(ds);

                card = lazyProcessCardModuleList[0];
                card.LazyCardXMLLoader = GetCards;
            }
            catch
            {
                throw;
            }
            return card;
        }

        /// <summary>
        /// 方法说明： 根据条件获取卡片列表
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <param name="processCardName">卡片名称</param>
        /// <returns></returns>
        public static List<ProcessCard> GetProcessCardListByCondition(string processCardName)
        {
            DataSet ds = ProcessCardDAL.GetProcessCardDatasetByCondition(processCardName);

            ModleHandler<ProcessCard> processCardHandler = new ModleHandler<ProcessCard>();
            List<ProcessCard> processCardList = new List<ProcessCard>();
            try
            {
                processCardList = processCardHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return processCardList;
        }

        /// <summary>
        /// 方法说明：获取卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-06-20
        /// </summary>
        /// <returns></returns>
        public static List<ProcessCard> GetDefaultProcessCardList()
        {
            DataSet ds = ProcessCardDAL.GetDefaultProcessCardDataset();

            ModleHandler<ProcessCard> processCardHandler = new ModleHandler<ProcessCard>();
            List<ProcessCard> processCardList = new List<ProcessCard>();
            try
            {
                processCardList = processCardHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return processCardList;
        }

        /// <summary>
        /// 方法说明：获取工艺文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="parentFolder">父文件夹</param>
        /// <returns></returns>
        public static List<ProcessFolder> GetProcessFolderList(string parentFolder)
        {
            DataSet ds = ProcessCardDAL.GetProcessFolderListDataSet(parentFolder);

            ModleHandler<ProcessFolder> processFolderHandler = new ModleHandler<ProcessFolder>();
            List<ProcessFolder> processFolderList = new List<ProcessFolder>();
            try
            {
                processFolderList = processFolderHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return processFolderList;
        }

        /// <summary>
        /// 方法说明：获取对应文件夹下的工艺文件
        /// 作      者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <returns></returns>
        public static List<ProcessVersion> GetProcessCardByFolderId(string folderId, int categoryid)
        {
            DataSet ds = ProcessCardDAL.GetProcessCardByFolderIdDataSet(folderId, categoryid);

            ModleHandler<ProcessVersion> processVersionHandler = new ModleHandler<ProcessVersion>();
            List<ProcessVersion> processVersionList = new List<ProcessVersion>();
            try
            {
                processVersionList = processVersionHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return processVersionList;
        }


        /// <summary>
        /// get card module XML by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CardsXML GetCards(Guid id)
        {
            string cardxml = string.Empty;
            CardsXML card = null;
            try
            {
                cardxml = ProcessCardDAL.GetCardXML(id);

                card = SerializeHelper
                    .DeserializeXMLChar<CardsXML>(cardxml);

            }
            catch (Exception)
            {
                throw;
            }
            return card;
        }

        /// <summary>
        /// 方法说明：卡片新增
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <param name="card">卡片实体</param>
        /// <returns>Guid</returns>
        public Guid InsertProcessCard(ProcessCard card)
        {
            Guid result;
            try
            {
                result = ProcessCardDAL.AddProcessCard(card);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：卡片版本新增
        /// 作    者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="version">卡片版本实体</param>
        /// <returns>True/False</returns>
        public static bool InsertProcessVersion(ProcessVersion version, object material)
        {
            bool result = false;

            try
            {
                result = ProcessCardDAL.AddProcessVersion(version, material);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：卡片版本删除
        /// 作    者：jason.tang
        /// 完成时间：2013-08-23
        /// </summary>
        /// <param name="baseid">卡片Id</param>
        /// <param name="folderid">文件夹ID</param>
        /// <returns>True/False</returns>
        public static bool DeleteProcessVersion(string baseid, string folderid)
        {
            bool result = false;

            try
            {
                result = ProcessCardDAL.DeleteProcessVersion(baseid, folderid);
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// 方法说明：卡片修改
        /// 作    者：jason.tang
        /// 完成时间：2013-03-11
        /// </summary>
        /// <param name="card">卡片实体</param>
        /// <returns></returns>
        public bool UpdateProcessCard(ProcessCard card)
        {
            bool result = false;
            try
            {
                result = ProcessCardDAL.UpdateProcessCard(card);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：根据卡片ID删除卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-07-30
        /// </summary>
        /// <param name="cardid">卡片ID</param>
        /// <returns></returns>
        public static bool DeleteProcessCard(string cardid)
        {
            bool result = true;
            try
            {
                result = ProcessCardDAL.DeleteProcessCard(cardid);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：获取用户数据
        /// 作      者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUsers()
        {
            DataTable dt = null;

            try
            {
                DataSet ds = ProcessCardDAL.GetUsersDataset();
                if (ds != null && ds.Tables.Count > 0)
                    dt = ds.Tables[0];
            }
            catch
            { }
            
            return dt;
        }

        /// <summary>
        /// 方法说明：获取工艺卡片数据
        /// 作      者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static List<ProcessCard> GetProcessVersion(string condition)
        {
            DataSet ds = ProcessCardDAL.GetProcessVersionDataSet(condition);

            ModleHandler<ProcessCard> processCardHandler = new ModleHandler<ProcessCard>();
            List<ProcessCard> processCardList = new List<ProcessCard>();
            try
            {
                processCardList = processCardHandler.GetModelByDataSet(ds);
            }
            catch
            {
                throw;
            }
            return processCardList;
        }
    }
}
