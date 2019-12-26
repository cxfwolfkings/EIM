using EIM.Core.Registry;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EIM.Router
{
    public interface IServiceSubscriber : IDisposable
    {
        Task<List<RegistryInformation>> Endpoints(CancellationToken ct = default);
    }
}
