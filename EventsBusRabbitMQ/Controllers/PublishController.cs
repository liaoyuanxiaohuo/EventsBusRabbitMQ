using EventsBus.Contract;
using EventsBusRabbitMQ.Api.Handle.Eto;
using Microsoft.AspNetCore.Mvc;

namespace EventsBusRabbitMQ.Api.Controllers
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
    }
}
