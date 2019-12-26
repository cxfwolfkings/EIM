using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
/*******************************
 * Created By franco
 * Description: 
 * Model and dataTable convert each other
 *******************************/


namespace Kingdee.CAPP.Common.ModuleHelper
{
    public class ModleHandler<T>
        where T : new()
    {

        /// <summary>
        /// get modle list by datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<T> GetModelsByDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
                return null;

            List<T> tList = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();

                #region by datatable structure
                //for (int i = 0; i < dr.Table.Columns.Count; i++)
                //{
                //    PropertyInfo property = t.GetType().GetProperty(dr[i].ToString());

                //} 
                #endregion

                foreach (PropertyInfo p in typeof(T).GetProperties())
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        if (dc.ColumnName.ToLower() == p.Name.ToLower() && dr[p.Name] != DBNull.Value)
                        {
                            t.GetType().GetProperty(p.Name).SetValue(t, dr[p.Name], null);
                            break;
                        }
                    }
                }
                tList.Add(t);
            }
            return tList;
        }

        /// <summary>
        /// get model by datarow
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public T GetModelByDataRow(DataRow dr)
        {
            if (dr.Table.Rows.Count <= 0)
                return default(T);

            T t = new T();
            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                if (dr.Table.Rows.Contains(p.Name))
                {
                    t.GetType().GetProperty(p.Name).SetValue(t, dr[p.Name], null);
                }
            }
            return t;
        }

        /// <summary>
        /// get model list by dataset' s defalut table
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public List<T> GetModelByDataSet(DataSet ds)
        {
            if (ds == null
                || ds.Tables[0] == null
                || ds.Tables[0].Rows.Count == 0)
                return null;

            List<T> tlist = GetModelsByDataTable(ds.Tables[0]);
            return tlist;
        }

        /// <summary>
        /// get model list by dataset and index
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<T> GetModelByDataSetAndIndex(DataSet ds, int index)
        {
            if (ds == null
                || ds.Tables[index] == null
                || ds.Tables[index].Rows.Count == 0)
                return null;

            List<T> tlist = GetModelsByDataTable(ds.Tables[index]);
            return tlist;
        }

        /// <summary>
        /// get datatable by T's list
        /// </summary>
        /// <param name="tlist"></param>
        /// <returns></returns>
        public DataTable GetDataTableByModel(List<T> tlist)
        {
            DataTable dt = CreateDataTable();

            foreach (T t in tlist)
            {
                DataRow dataRow = dt.NewRow();
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    dataRow[propertyInfo.Name] = propertyInfo.GetValue(t, null);
                }
                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// create by table
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable(typeof(T).Name);

            foreach (PropertyInfo p in typeof(T).GetProperties())
            {
                dt.Columns.Add(p.Name, p.PropertyType);
            }
            return dt;
        }

    }
}
