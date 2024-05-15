using EventBus.Contract;
using EventBus.RabbitMQ;

namespace EventBusRabbitMQ.Api.Handle.Eto
{
    /// <summary>
    /// 
    /// </summary>
    [EventBus("OrderExchange", "create.order", "CreateOrder")]
    public class CreateOrderEto : IEvent
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string CreatePeople { get; set; } = string.Empty;
    }
}
