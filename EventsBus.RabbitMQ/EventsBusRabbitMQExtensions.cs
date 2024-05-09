using EventsBus.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventsBus.RabbitMQ
{
    public static class EventsBusRabbitMQExtensions
    {
        public static IServiceCollection AddEventsBusRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMQFactory>();
            services.AddSingleton(typeof(RabbitMQEventsManage<>));
            services.AddSingleton<ILoadEventBus, RabbitMQLoadEventBus>();

            return services;
        }
    }
}
