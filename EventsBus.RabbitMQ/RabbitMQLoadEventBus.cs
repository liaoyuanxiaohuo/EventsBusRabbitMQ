using EventBus.Contract;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace EventBus.RabbitMQ
{
    public class RabbitMQLoadEventBus : ILoadEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQFactory _rabbitMQFactory;

        public RabbitMQLoadEventBus(IServiceProvider serviceProvider,
            RabbitMQFactory rabbitMQFactory)
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
        public async Task PushAsync<TEto>(TEto eto) where TEto : IEvent
        {
            using var channel = _rabbitMQFactory.CreateRabbitMQ();

            // 获取Eto中的EventBusAttribute特性，获取名称，如果没有默认使用类名称
            var eventBus = typeof(TEto).GetCustomAttribute<EventBusAttribute>();

            var exchangeName = eventBus?.ExchangeName ?? typeof(TEto).Name;
            var routingKey = eventBus?.RoutingKey ?? typeof(TEto).Name;

            //var queueName = eventBus?.QueueName ?? typeof(TEto).Name;

            // 创建topic类型的交换机
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);

            // 创建队列
            //channel.QueueDeclare(queueName, true, false, false, null);

            // 通过routingkey将队列绑定到交换机
            //channel.QueueBind(queueName, exchangeName, routingKey);

            // 持久化消息
            var propeties = channel.CreateBasicProperties();
            propeties.DeliveryMode = 2;

            // 发送消息到exchange、routingkey
            channel.BasicPublish(exchangeName, routingKey, false, propeties, JsonSerializer.SerializeToUtf8Bytes(eto));

            var eventsManage = _serviceProvider.GetService<RabbitMQEventsManage<TEto>>();

            await Task.CompletedTask;
        }
    }
}
