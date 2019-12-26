namespace EIM.Router.Consul
{
    public interface IConsulServiceSubscriberFactory
    {
        IServiceSubscriber CreateSubscriber(string serviceName, ConsulSubscriberOptions consulOptions, bool watch);
    }
}
