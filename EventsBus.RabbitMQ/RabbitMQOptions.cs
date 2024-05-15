using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.RabbitMQ
{
    /// <summary>
    /// 连接属性
    /// </summary>
    public class RabbitMQOptions
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string HostName { get; set; } = string.Empty;

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; } = AmqpTcpEndpoint.UseDefaultPort;

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; } = string.Empty;


    }
}
