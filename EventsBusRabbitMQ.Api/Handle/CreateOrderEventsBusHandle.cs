using EventBus.Contract;
using EventBusRabbitMQ.Api.Handle.Eto;
using System.Text.Json;

namespace EventBusRabbitMQ.Api.Handle
{
    public class CreateOrderEventBusHandle : IEventBusHandle<CreateOrderEto>
    {
        public async Task HandleAsync(CreateOrderEto eventData)
        {
            Console.WriteLine($"队列CreateOrder处理订单创建事件： {JsonSerializer.Serialize(eventData)}");
            await Task.CompletedTask;
        }
    }
}
