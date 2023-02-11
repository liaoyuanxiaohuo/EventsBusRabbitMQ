using EventsBus.Contract;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventsBus.RabbitMQ
{
    public class RabbitMQEventsManage<TEto> where TEto : class
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
            var name = eventBus?.Name ?? typeof(TEto).Name;
            channel.QueueDeclare(name, true, false, false, null);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(name, false, consumer);
            //channel.BasicConsume(name, true, consumer); //消费消息
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
