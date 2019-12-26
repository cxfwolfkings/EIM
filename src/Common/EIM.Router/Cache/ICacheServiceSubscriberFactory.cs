namespace EIM.Router.Cache
{
    public interface ICacheServiceSubscriberFactory
    {
        IPollingServiceSubscriber CreateSubscriber(IServiceSubscriber serviceSubscriber);
    }
}
