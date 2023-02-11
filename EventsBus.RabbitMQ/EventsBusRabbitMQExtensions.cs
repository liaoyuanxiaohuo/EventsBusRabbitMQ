using EventsBus.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.RabbitMQ
{
    public static class EventsBusRabbitMQExtensions
    {
        public static IServiceCollection AddEventsBusRabbitMQ(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddSingleton<RabbitMQFactory>();
            services.AddSingleton(typeof(RabbitMQEventsManage<>));

            //services.Configure<RabbitMQOptions>(configuration.GetSection(nameof(RabbitMQOptions)));
          
            services.AddSingleton<ILoadEventBus, RabbitMQLoadEventBus>();


            return services;
        }
    }
}
