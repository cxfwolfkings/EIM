using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kingdee.CAPP.Model;
using Kingdee.CAPP.DAL;
using System.Data;

namespace Kingdee.CAPP.BLL
{
    /// <summary>
    /// 类型说明：物料与卡片关联BLL
    /// 作    者：jason.tang
    /// 完成时间：2013-03-26
    /// </summary>
    public class MaterialCardRelationBLL
    {
        /// <summary>
        /// 方法说明：根据物料ID查找卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <returns></returns>
        public static List<ProcessCard> GetProcessCardByMaterialId(string materialId, int type)
        {
            try
            {
                DataTable dt = MaterialCardRelationDAL
                    .GetProcessCardByMaterialId(materialId, type).Tables[0];

                var cProcessCardList = (from c in dt.AsEnumerable()
                                                 select new ProcessCard()
                                          {
                                              ID = c.Field<Guid>("ID"),
                                              Name = c.Field<string>("Name"),
                                              CardSort = c.Field<int>("CardSort")
                                          }).ToList<ProcessCard>();
                return cProcessCardList;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 方法说明：新增物料卡片关联记录
        /// 作    者：jason.tang
        /// 完成时间：2013-03-26
        /// </summary>
        /// <param name="materialCardRelation">物料卡片关联实体</param>
        /// <returns></returns>
        public static Guid AddMaterialCardRelationData(MaterialCardRelation materialCardRelation)
        {
            try
            {
                Guid id = MaterialCardRelationDAL.AddMaterialCardRelationData(materialCardRelation);
                return id;
            }
            catch
            {

                throw;
            }

        }

        /// <summary>
        /// 方法说明：删除物料下挂的卡片
        /// 作      者：jason.tang
        /// 完成时间：2013-08-10
        /// </summary>
        /// <param name="materialId">物料ID</param>
        /// <param name="processCardId">卡片ID</param>
        /// <param name="type">视图类型</param>
        /// <returns>True/False</returns>
        public static bool DeleteMaterialCard(string materialId, string processCardId, int type)
        {
            bool result = false;
            try
            {
                result = MaterialCardRelationDAL.DeleteMaterialCard(materialId, processCardId, type);
            }
            catch
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 方法说明：根据零部件名查找卡片
        /// 作    者：jason.tang
        /// 完成时间：2013-09-10
        /// </summary>
        /// <param name="name">零部件</param>
        /// <returns></returns>
        public static List<ProcessCard> GetProcessCardByMaterialName(string name)
        {
            try
            {
                DataTable dt = MaterialCardRelationDAL
                    .GetProcessCardByMaterialName(name).Tables[0];

                var cProcessCardList = (from c in dt.AsEnumerable()
                                        select new ProcessCard()
                                        {
                                            ID = c.Field<Guid>("ID"),
                                            Name = c.Field<string>("Name"),
                                            CardSort = c.Field<int>("CardSort")
                                        }).ToList<ProcessCard>();
                return cProcessCardList;
            }
            catch
            {
                throw;
            }
        }
    }
}
