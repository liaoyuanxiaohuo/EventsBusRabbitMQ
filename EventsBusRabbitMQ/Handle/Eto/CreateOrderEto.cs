using EventsBus.RabbitMQ;
using System.Diagnostics.Tracing;

namespace EventsBusRabbitMQ.Api.Handle.Eto
{
    [EventsBus("CreateOrder")]
    public class CreateOrderEto
    {

        public string OrderNo { get; set; }

        public decimal Price { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string CreatePeople { get; set; }
    }
}
