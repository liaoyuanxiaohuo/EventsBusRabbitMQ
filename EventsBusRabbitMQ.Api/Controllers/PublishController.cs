using EventBus.Contract;
using EventBusRabbitMQ.Api.Handle.Eto;
using EventBusRabbitMQ.Handler.Events;
using Microsoft.AspNetCore.Mvc;

namespace EventBusRabbitMQ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishController : ControllerBase
    {
        private readonly ILoadEventBus _loadEventBus;

        public PublishController(ILoadEventBus loadEventBus)
        {
            _loadEventBus = loadEventBus;
        }

        /// <summary>
        /// 测试创建订单
        /// </summary>
        /// <param name="eto"></param>
        /// <returns></returns>
        [HttpPost("order-created")]
        public async Task SendOrderCreated(CreateOrderEto eto)
        {
            await _loadEventBus.PushAsync(eto);
        }

        /// <summary>
        /// 测试创建商品
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost("product-created")]
        public async Task CreateProduct(CreateProductEvent dto)
        {
            await _loadEventBus.PushAsync(dto);
        }
    }
}
