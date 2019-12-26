using EIM.Router.Cache;
using EIM.Router.Consul;
using EIM.Router.Throttle;

namespace EIM.Router
{
    public class ServiceSubscriberFactory : IServiceSubscriberFactory
    {
        private readonly IConsulServiceSubscriberFactory _consulServiceSubscriberFactory;
        private readonly ICacheServiceSubscriberFactory _cacheServiceSubscriberFactory;

        public ServiceSubscriberFactory(IConsulServiceSubscriberFactory consulServiceSubscriberFactory, ICacheServiceSubscriberFactory cacheServiceSubscriberFactory)
        {
            _consulServiceSubscriberFactory = consulServiceSubscriberFactory;
            _cacheServiceSubscriberFactory = cacheServiceSubscriberFactory;
        }

        public IPollingServiceSubscriber CreateSubscriber(string servicName)
        {
            return CreateSubscriber(servicName, ConsulSubscriberOptions.Default, ThrottleSubscriberOptions.Default);
        }

        public IPollingServiceSubscriber CreateSubscriber(string serviceName, ConsulSubscriberOptions consulOptions, ThrottleSubscriberOptions throttleOptions)
        {
            var consulSubscriber = _consulServiceSubscriberFactory.CreateSubscriber(serviceName, consulOptions, true);
            var throttleSubscriber = new ThrottleServiceSubscriber(consulSubscriber, throttleOptions.MaxUpdatesPerPeriod, throttleOptions.MaxUpdatesPeriod);
            return _cacheServiceSubscriberFactory.CreateSubscriber(throttleSubscriber);
        }
    }
}
