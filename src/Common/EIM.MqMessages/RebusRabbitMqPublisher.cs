using EIM.Core.Json;
using EIM.Core.MqMessages;
using EIM.Core.Threading;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using System.Threading.Tasks;

namespace EIM.MqMessages
{
    public class RebusRabbitMqPublisher : IMqMessagePublisher
    {
        private readonly IBus _bus;

        public ILogger Logger { get; set; }

        public RebusRabbitMqPublisher(IBus bus, ILoggerFactory factory)
        {
            _bus = bus;
            Logger = factory.CreateLogger<RebusRabbitMqPublisher>();
        }

        public void Publish(object mqMessages)
        {
            Logger.LogDebug(mqMessages.GetType().FullName + ":" + mqMessages.ToJsonString());

            AsyncHelper.RunSync(() => _bus.Publish(mqMessages));
        }

        public async Task PublishAsync(object mqMessages)
        {
            Logger.LogDebug(mqMessages.GetType().FullName + ":" + mqMessages.ToJsonString());

            await _bus.Publish(mqMessages);
        }
    }
}
