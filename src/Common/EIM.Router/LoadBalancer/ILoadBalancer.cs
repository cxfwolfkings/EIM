using EIM.Core.Registry;
using System.Threading;
using System.Threading.Tasks;

namespace EIM.Router.LoadBalancer
{
    public interface ILoadBalancer
    {
        Task<RegistryInformation> Endpoint(CancellationToken ct = default);
    }
}
