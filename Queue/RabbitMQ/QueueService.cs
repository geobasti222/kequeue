using RabbitMQ.Client;
using Ke.Queue.RabbitMQ.Implementations.Listener;
using Ke.Queue.RabbitMQ.Implementations.Publisher;
using Ke.Queue.RabbitMQ.Interfaces.Listener;
using Ke.Queue.RabbitMQ.Interfaces.Publisher;
using Ke.Queue.RabbitMQ.Models;

namespace Ke.Queue.RabbitMQ
{
    public class QueueService : IQueueService
    {
        private readonly IConnection _connection;
        public readonly IMessageListener _listener;
        public readonly IMessagePublisher _publisher;

        public QueueService(Options options)
        {
            _connection = CreateConnection(options);
            _listener = new MessageListener(_connection);
            _publisher = new MessagePublisher(_connection);
        }

        public IConnection CreateConnection(Options options)
        {
            var factory = new ConnectionFactory
            {
                HostName = options.HostName,
                UserName = options.UserName,
                Port = options.Port,
                Password = options.Password
            };
            return factory.CreateConnection();
        }
    }
}
