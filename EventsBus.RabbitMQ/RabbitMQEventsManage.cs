using EventsBus.Contract;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Reflection;
using System.Text.Json;

namespace EventsBus.RabbitMQ
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
            var eventBus = typeof(TEto).GetCustomAttribute<EventsBusAttribute>();

            var queueName = eventBus?.QueueName ?? typeof(TEto).Name;
            channel.QueueDeclare(queueName, true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queueName, false, consumer);

            //消费消息
            consumer.Received += async (model, ea) =>
            {
                var bytes = ea.Body.ToArray();
                try
                {
                    var events = _serviceProvider.GetServices<IEventsBusHandle<TEto>>();
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
