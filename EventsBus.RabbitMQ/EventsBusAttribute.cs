namespace EventsBus.RabbitMQ
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventsBusAttribute : Attribute
    {
        /// <summary>
        /// 交换机名称
        /// </summary>
        public readonly string ExchangeName;

        /// <summary>
        /// routingKey
        /// </summary>
        public readonly string RoutingKey;

        /// <summary>
        /// 队列名称
        /// </summary>
        public readonly string QueueName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchangeName">交换机名称</param>
        /// <param name="routingKey">routingKey</param>
        /// <param name="queueName">队列名称</param>
        public EventsBusAttribute(string exchangeName, string routingKey, string queueName)
        {
            ExchangeName = exchangeName;
            RoutingKey = routingKey;
            QueueName = queueName;
        }
    }
}
