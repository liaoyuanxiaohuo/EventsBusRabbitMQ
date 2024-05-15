using EventBus.Contract;
using EventBusRabbitMQ.Handler.Events;
using System.Text.Json;

namespace EventBusRabbitMQ.Handler.Handles
{
    internal class CreateProductEventHandler : IEventBusHandle<CreateProductEvent>
    {
        public async Task HandleAsync(CreateProductEvent eventData)
        {
            Console.WriteLine($"队列CreateProduct处理商品创建事件： {JsonSerializer.Serialize(eventData)}");
            await Task.CompletedTask;
        }
    }
}
