using EIM.Router.Cache.Internal;

namespace EIM.Router.Cache
{
    public class CacheServiceSubscriberFactory : ICacheServiceSubscriberFactory
    {
        private readonly ICacheClient _cacheClient;

        public CacheServiceSubscriberFactory(ICacheClient cacheClient)
        {
            _cacheClient = cacheClient;
        }

        public IPollingServiceSubscriber CreateSubscriber(IServiceSubscriber serviceSubscriber)
        {
            return new CacheServiceSubscriber(serviceSubscriber, _cacheClient);
        }
    }
}
