using EventBus.Contract;
using EventBus.RabbitMQ;
using EventBus.RabbitMQ.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EventBusRabbitMQExtensions
    {
        public static IServiceCollection AddEventBusRabbitMQ(this IServiceCollection services)
        {
            services.AddSingleton<RabbitMQFactory>();
            services.AddSingleton(typeof(RabbitMQEventsManage<>));
            services.AddSingleton<ILoadEventBus, RabbitMQLoadEventBus>();

            //注入事件处理服务
            //builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrderEto>), typeof(CreateOrderEventBusHandle));
            //builder.Services.AddSingleton(typeof(IEventBusHandle<CreateOrder1Eto>), typeof(CreateOrder1EventBusHandle));

            //services.AddClassesAsImplementedInterface(Assembly.GetEntryAssembly()!, typeof(IEventBusHandle<>), ServiceLifetime.Singleton);
            services.AddClassesAsImplementedInterface(Assembly.GetCallingAssembly(), typeof(IEventBusHandle<>), ServiceLifetime.Singleton);

            return services;
        }
    }
}
