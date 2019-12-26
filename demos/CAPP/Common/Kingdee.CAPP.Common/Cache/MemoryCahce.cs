using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace CacheManager
{
    public class MemoryCahce: ICacheProvider
    {
        /// <summary>
        /// Implement memory cache
        /// </summary>
        private static readonly System.Web.Caching.Cache cache = GetCache();

        private static System.Web.Caching.Cache GetCache()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx != null ? ctx.Cache : HttpRuntime.Cache;
        }


        public void Insert(string key, object value, DateTime absoluteExiration)
        {
            cache.Insert(key, value, null, absoluteExiration, TimeSpan.Zero);
        }

        public void Insert(string key, object value)
        {
            cache.Insert(key, value);
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }

        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                cache.Insert(key, value);
            }
        }

        public object Get(string key)
        {
            return cache.Get(key);
        }
    }
}
