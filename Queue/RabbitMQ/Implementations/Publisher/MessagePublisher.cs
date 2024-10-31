using Newtonsoft.Json;
using RabbitMQ.Client;
using Ke.Queue.RabbitMQ.Interfaces.Publisher;
using System.Text;

namespace Ke.Queue.RabbitMQ.Implementations.Publisher
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IConnection _connection;

        public MessagePublisher(IConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishMessage(string queueName, string message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);
            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }

        public async Task PublishMessage<T>(string queueName, T message) {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
        }

        public async Task PublishMessageWithProperties<T>(string queueName, IBasicProperties properties, T message)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var messageJson = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(messageJson);

            await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body));
        }
    }
}
