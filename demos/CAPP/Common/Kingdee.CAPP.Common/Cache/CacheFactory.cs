using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
/*******************************
 * Created By franco
 * Description: log type
 *******************************/


namespace CacheManager
{
    public sealed class CacheFactory
    {
        public ICacheProvider GetCache()
        {
            var t = GetProviderType();
            return GetCache(t);

        }

        public ICacheProvider GetCache(Type type)
        {
            return GetProvider(type) ?? new MemoryCahce();
        }



        public ICacheProvider GetCache<T>()
            where T: ICacheProvider
        {
            return GetCache(typeof(T));
        }
        /// <summary>
        /// get cache instance
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        private static ICacheProvider GetProvider(Type t)
        {
            if (t == null) return null;
            return Activator.CreateInstance(t) as ICacheProvider;
        }

        /// <summary>
        /// get config type
        /// </summary>
        /// <returns></returns>
        private static Type GetProviderType()
        {
            var config = System.Configuration.ConfigurationManager.GetSection("CacheSetting") as NameValueCollection;
            var cacheType = config["cacheProviderType"];
            return string.IsNullOrEmpty(cacheType) ? null : Type.GetType(cacheType, true);
        }
    }
}
