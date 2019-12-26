namespace EIM.Router.Consul
{
    public interface IConsulPreparedQueryServiceSubscriberFactory
    {
        IServiceSubscriber CreateSubscriber(string queryName);
    }
}
