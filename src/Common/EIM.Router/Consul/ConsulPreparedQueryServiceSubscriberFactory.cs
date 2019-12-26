using Consul;

namespace EIM.Router.Consul
{
    public class ConsulPreparedQueryServiceSubscriberFactory : IConsulPreparedQueryServiceSubscriberFactory
    {
        private readonly IConsulClient _consulClient;

        public ConsulPreparedQueryServiceSubscriberFactory(IConsulClient consulClient)
        {
            _consulClient = consulClient;
        }

        public IServiceSubscriber CreateSubscriber(string queryName)
        {
            return new ConsulPreparedQueryServiceSubscriber(_consulClient, queryName);
        }
    }
}
