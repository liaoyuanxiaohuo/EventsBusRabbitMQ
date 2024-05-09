using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace EventsBus.RabbitMQ
{
    public class RabbitMQFactory : IDisposable
    {
        private readonly RabbitMQOptions _options;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection? _connection;

        public RabbitMQFactory(IOptions<RabbitMQOptions> options)
        {
            _options = options.Value;

            _connectionFactory = new ConnectionFactory
            {
                HostName = _options.HostName, 
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
            };

            //_connectionFactory.HostName = "127.0.0.1";
            //_connectionFactory.UserName = "admin";
            //_connectionFactory.Password = "123456";
        }

        public IModel CreateRabbitMQ()
        {
            _connection ??= _connectionFactory.CreateConnection();
            return _connection.CreateModel();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
