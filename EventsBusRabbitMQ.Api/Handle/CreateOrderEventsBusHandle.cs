using EventsBus.Contract;
using EventsBusRabbitMQ.Api.Handle.Eto;
using System.Text.Json;

namespace EventsBusRabbitMQ.Api.Handle
{
    public class CreateOrderEventsBusHandle : IEventsBusHandle<CreateOrderEto>
    {
        public async Task HandleAsync(CreateOrderEto eventData)
        {
            Console.WriteLine($"处理订单创建事件： {JsonSerializer.Serialize(eventData)}");
            await Task.CompletedTask;
        }
    }
}
