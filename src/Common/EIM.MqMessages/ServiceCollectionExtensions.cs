using EIM.Core.MqMessages;
using Microsoft.Extensions.DependencyInjection;

namespace EIM.MqMessages
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMqMessages(this IServiceCollection services
           )
        {
            services.AddSingleton<IMqMessagePublisher, RebusRabbitMqPublisher>();
            return services;
        }
    }
}
