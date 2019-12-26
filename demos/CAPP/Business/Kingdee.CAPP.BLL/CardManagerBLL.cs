using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;
using System.Data;
/*******************************
 * Created By franco
 * Description:Card Manager business layer
 *******************************/

namespace Kingdee.CAPP.BLL
{

    public class CardManagerBLL
    {
        /// <summary>
        /// Get card manager list by parent node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static List<CardManager> GetCardManagerListById(int parentNode)
        {
            try
            {
                DataTable dt = CardManagerDAL.GetCardManagerByParentNode(parentNode).Tables[0];

                var cardManagerList = (from c in dt.AsEnumerable()
                                       select new CardManager()
                                       {
                                           ProcessModuleId = c.Field<Guid>("ProcessModuleId"),
                                           BusinessId = c.Field<Guid>("BusinessId"),
                                           CurrentNode = c.Field<int>("CurrentNode"),
                                           ParentNode = c.Field<int>("ParentNode"),
                                           Name = c.Field<string>("Name"),
                                           BType = (BusinessType)Enum.Parse(typeof(BusinessType),
                                                       c.Field<Int16>("Type").ToString())

                                       }).ToList<CardManager>();
                return cardManagerList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get display level process
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public static List<CardManager> GetDefaultCardManagerListByLevel(int currentNode)
        {
            try
            {
                DataTable dt = CardManagerDAL.GetDefalutCardManager(currentNode).Tables[0];

                var cardManagerList = (from c in dt.AsEnumerable()
                                       select new CardManager()
                                       {
                                           ProcessModuleId = c.Field<Guid>("ProcessModuleId"),
                                           BusinessId = c.Field<Guid>("BusinessId"),
                                           CurrentNode = c.Field<int>("CurrentNode"),
                                           ParentNode = c.Field<int>("ParentNode"),
                                           Name = c.Field<string>("Name"),
                                           BType = (BusinessType)Enum.Parse(typeof(BusinessType),
                                                       c.Field<Int16>("Type").ToString())
                                       }).ToList<CardManager>();

                return cardManagerList;
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
            try
            {
                int currentNode = CardManagerDAL.AddBusiness(name, type, parentNode, businessId);
                return currentNode;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 方法说明：删除模版或文件夹
        /// 作      者：jason.tang
        /// 完成时间：2013-07-22
        /// </summary>
        /// <param name="businessId">业务ID</param>
        /// <returns>True/False</returns>
        public static bool DeleteBusiness(Guid businessId)
        {
            bool result = true;

            try
            {
                result = CardManagerDAL.DeleteBusiness(businessId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
