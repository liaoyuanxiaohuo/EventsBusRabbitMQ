using EventBus.Contract;
using EventBus.RabbitMQ;

namespace EventBusRabbitMQ.Handler.Events
{
    [EventBus("ProductExchange", "create.product", "CreateProduct")]
    public class CreateProductEvent : IEvent
    {
        /// <summary>
        /// 商品id
        /// </summary>
        public string ProductId { get; set; } = string.Empty;

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductNo { get; set; } = string.Empty;

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ProductPrice { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string CreatePeople { get; set; } = string.Empty;
    }
}
