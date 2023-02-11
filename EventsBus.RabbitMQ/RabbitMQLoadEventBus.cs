using EventsBus.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace EventsBus.RabbitMQ
{
    public class RabbitMQLoadEventBus : ILoadEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQFactory _rabbitMQFactory;

        public RabbitMQLoadEventBus(IServiceProvider serviceProvider, RabbitMQFactory rabbitMQFactory)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQFactory = rabbitMQFactory;
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEto"></typeparam>
        /// <param name="eto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task PushAsync<TEto>(TEto eto) where TEto : class
        {
            using var channel = _rabbitMQFactory.CreateRabbitMQ();
            // 获取Eto中的EventsBusAttribute特性，获取名称，如果没有默认使用类名称
            var eventBus = typeof(TEto).GetCustomAttribute<EventsBusAttribute>();
            var name = eventBus?.Name ?? typeof(TEto).Name;

            // 创建通道
            channel.QueueDeclare(name, true, false, false, null);
            var propeties = channel.CreateBasicProperties();
            propeties.DeliveryMode = 2;

            channel.BasicPublish("", name, false, propeties, JsonSerializer.SerializeToUtf8Bytes(eto));

            var eventsManage = _serviceProvider.GetService<RabbitMQEventsManage<TEto>>();

            await Task.CompletedTask;
        }
    }
}
