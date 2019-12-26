using System;
using System.Threading;
using System.Threading.Tasks;

namespace EIM.Mediatr
{
    public interface IRequestManager
    {
        Task<bool> IsRegistered(Guid id, CancellationToken cancellationToken = default);

        Task Register(Guid id, CancellationToken cancellationToken = default);
    }
}
