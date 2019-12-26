using System;
using System.Threading;
using System.Threading.Tasks;

namespace EIM.Mediatr
{
    public class InMemoryRequestManager : IRequestManager
    {
        private readonly CircularQueue<Guid> _queue = new CircularQueue<Guid>();

        public Task<bool> IsRegistered(Guid id, CancellationToken cancellationToken = default) =>
            Task.FromResult(_queue.Contains(id));

        public Task Register(Guid id, CancellationToken cancellationToken = default)
        {
            _queue.Enqueue(id);
            return Task.CompletedTask;
        }
    }
}
