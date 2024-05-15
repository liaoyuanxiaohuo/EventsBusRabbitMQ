namespace EventBus.Contract
{
    public interface IEventBusHandle<in TEto> where TEto : IEvent
    {
        /// <summary>
        /// 接收处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task HandleAsync(TEto eventData);
    }
}
