using EventBus.Contract;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text.Json;

namespace EventBus.RabbitMQ
{
    public class RabbitMQEventsManage<TEto> where TEto : IEvent
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly RabbitMQFactory _rabbitMQFactory;

        public RabbitMQEventsManage(IServiceProvider serviceProvider, RabbitMQFactory rabbitMQFactory)
        {
            _serviceProvider = serviceProvider;
            _rabbitMQFactory = rabbitMQFactory;
            _ = Task.Run(Start);
        }

        private void Start()
        {
            var channel = _rabbitMQFactory.CreateRabbitMQ();
            var eventBus = typeof(TEto).GetCustomAttribute<EventBusAttribute>();

            var exchangeName = eventBus?.ExchangeName ?? typeof(TEto).Name;
            var routingKey = eventBus?.RoutingKey ?? typeof(TEto).Name;
            var queueName = eventBus?.QueueName ?? typeof(TEto).Name;

            // 创建队列
            channel.QueueDeclare(queueName, true, false, false, null);

            // 通过routingkey将队列绑定到交换机
            channel.QueueBind(queueName, exchangeName, routingKey);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queueName, false, consumer);

            //消费消息
            consumer.Received += async (model, ea) =>
            {
                var bytes = ea.Body.ToArray();
                try
                {
                    var events = _serviceProvider.GetServices<IEventBusHandle<TEto>>();
                    foreach (var handle in events)
                    {
                        await handle?.HandleAsync(JsonSerializer.Deserialize<TEto>(bytes));
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
                catch (Exception ex)
                {
                    EventbusOptions.ReceiveExceptionEvent?.Invoke(_serviceProvider, ex, bytes);
                }
            };
        }
    }
}
