using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;
using System.Data;
using Kingdee.CAPP.Common.ModuleHelper;
using Kingdee.CAPP.Common.Serialize;

namespace Kingdee.CAPP.BLL
{
    public class ProcessCardModuleBLL
    {
        /// <summary>
        /// get ProcessCardModule by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProcessCardModule GetProcessCardModule(Guid Id)
        {
            //LazyProcessCardModule 
            List<LazyProcessCardModule> lazyProcessCardModuleList = new List<LazyProcessCardModule>();
            LazyProcessCardModule cardmodule = new LazyProcessCardModule();
            try
            {
                DataSet ds = ProcessCardModuleDAL.GetProcessCardDataset(Id);

                ModleHandler<LazyProcessCardModule> processCardModuleHandler
                    = new ModleHandler<LazyProcessCardModule>();


                lazyProcessCardModuleList = processCardModuleHandler.GetModelByDataSet(ds);

                cardmodule = lazyProcessCardModuleList[0];
                cardmodule.CardModuleLazyLoader = GetCardModule;
            }
            catch
            {
                throw;
            }
            return cardmodule;
        }

        /// <summary>
        /// 方法说明：获取所有的卡片模版
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable GetProcessCardData()
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet ds = ProcessCardModuleDAL.GetProcessCardData();
                if (ds != null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch
            {
                throw;
            }

            return dt;
        }

        /// <summary>
        /// get processCard module list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<ProcessCardModule> GetProcessCardList(Guid id)
        {
            DataSet ds = ProcessCardModuleDAL.GetProcessCardDataset(id);

            ModleHandler<ProcessCardModule> processCardHandler = new ModleHandler<ProcessCardModule>();
            List<ProcessCardModule> processCardList = new List<ProcessCardModule>();
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
        /// get defalut processCard module list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ProcessCardModule> GetDefaultProcessCardList()
        {
            DataSet ds = ProcessCardModuleDAL.GetDefaultProcessCardDataset();

            ModleHandler<ProcessCardModule> processCardHandler = new ModleHandler<ProcessCardModule>();
            List<ProcessCardModule> processCardList = new List<ProcessCardModule>();
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
        /// get defalut processCard list 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<ProcessCardModule> GetProcessCardListByCondition(string processCardName)
        {
            DataSet ds = ProcessCardModuleDAL.GetProcessCardDatasetByCondition(processCardName);

            ModleHandler<ProcessCardModule> processCardHandler = new ModleHandler<ProcessCardModule>();
            List<ProcessCardModule> processCardList = new List<ProcessCardModule>();
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
        /// 方法说明：检查模版名称是否存在
        /// 作      者：jason.tang
        /// 完成时间：2013-07-26
        /// </summary>
        /// <param name="processCardName"></param>
        /// <returns></returns>
        public static bool CheckModuleNameExist(string processCardName)
        {
            bool result = false;
            DataSet ds = ProcessCardModuleDAL.CheckModuleNameExist(processCardName);

            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    result = true;
            }
            catch
            {
                throw;
            }

            return result;
        }

        /// <summary>
        /// add process card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public Guid AddProcessCard(ProcessCardModule card)
        {
            Guid result;
            try
            {
                result = ProcessCardModuleDAL.AddProcessCard(card);
            }
            catch
            {               
                throw;
            }
            return result;             
        }

        /// <summary>
        /// Modified process card
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool UpdateProcessCardModule(ProcessCardModule card)
        {
            bool result = false;
            try
            {
                result = ProcessCardModuleDAL.UpdateProcessCardModule(card);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// get card module XML by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CardsXML GetCardModule(Guid id)
        {
            string cardmodulexml = string.Empty;
            CardsXML cardModule = null;
            try
            {
                cardmodulexml = ProcessCardModuleDAL.GetCardModuleXML(id);

                cardModule = SerializeHelper
                    .DeserializeXMLChar<CardsXML>(cardmodulexml);

            }
            catch (Exception)
            {
                throw;
            }
            return cardModule;
        }
    }
}
