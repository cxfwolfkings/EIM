using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace CacheManager
{
    public class SerializedMemoryCache: ICacheProvider
    {
        private static readonly System.Web.Caching.Cache cache = GetCache();

        private static System.Web.Caching.Cache GetCache()
        {
            HttpContext ctx = HttpContext.Current;
            return ctx != null ? ctx.Cache : HttpRuntime.Cache;
        }



        public void Insert(string key, object value, DateTime absoluteExiration)
        {
            value = GetValue(value);
            cache.Insert(key, value, null, absoluteExiration, TimeSpan.Zero);
        }

        public void Insert(string key, object value)
        {
            value = GetValue(value);
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
            set { Insert(key, value); }
        }

        private static object GetValue(object value)
        {
            if (!(value is ValueType) && !(value is string))
            {
                value = SerializationHelper.Serialize(value);
            }
            return value;
        }



        public object Get(string key)
        {
            var o = cache.Get(key) as Byte[];
            return o == null ? cache.Get(key) : SerializationHelper.Deserialize(o);
        }
    }
}
