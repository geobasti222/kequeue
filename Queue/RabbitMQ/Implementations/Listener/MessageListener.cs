using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ke.Queue.RabbitMQ.Interfaces.Listener;
using System.Text;

namespace Ke.Queue.RabbitMQ.Implementations.Listener
{
    public class MessageListener : IMessageListener
    {
        private readonly IConnection _connection;

        public MessageListener(IConnection connection) {
            _connection = connection;
        }

        public async Task Listening(string queueName, Func<string, ulong, IBasicProperties, IModel, Task> callback, CancellationToken stoppingToken)
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await callback(message, ea.DeliveryTag, ea.BasicProperties, channel);
            };
            channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            
            while (!stoppingToken.IsCancellationRequested) {
                await Task.Delay(1000, stoppingToken);
            }

            channel.Close();
        }
    }
}
