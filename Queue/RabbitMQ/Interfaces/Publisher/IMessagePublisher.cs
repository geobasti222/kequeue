using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ke.Queue.RabbitMQ.Interfaces.Publisher
{
    public interface IMessagePublisher
    {
        Task PublishMessage(string queueName, string message);
        Task PublishMessage<T>(string queueName, T message);
        Task PublishMessageWithProperties<T>(string queueName, IBasicProperties properties, T message);
    }
}
