using EIM.Core.Registry;
using System.Threading;
using System.Threading.Tasks;

namespace EIM.Router.LoadBalancer
{
    /// <summary>
    /// 主机地址的轮播负载均衡
    /// </summary>
    public class RoundRobinLoadBalancer : ILoadBalancer
    {
        private readonly IServiceSubscriber _subscriber;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private int _index;

        public RoundRobinLoadBalancer(IServiceSubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        public async Task<RegistryInformation> Endpoint(CancellationToken ct = default)
        {
            var endpoints = await _subscriber.Endpoints(ct).ConfigureAwait(false);
            if (endpoints.Count == 0)
            {
                return null;
            }

            await _lock.WaitAsync(ct).ConfigureAwait(false);
            try
            {
                if (_index >= endpoints.Count)
                {
                    _index = 0;
                }
                var uri = endpoints[_index];
                _index++;

                return uri;
            }
            finally
            {
                _lock.Release();
            }
        }
    }
}
