using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace CacheManager
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICacheProvider
    {
        void Insert(string key,object value, DateTime absoluteExiration);
        void Insert(string key,object value);
        void Remove(string key);
        object this[string key]
        {
            get;
            set;
        }
        object Get(string key);
    }
}
