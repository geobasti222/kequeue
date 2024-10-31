using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ke.Queue.RabbitMQ.Interfaces.Listener
{
    public interface IMessageListener
    {
        Task Listening(string queueName, Func<string, ulong, IBasicProperties, IModel, Task> callback, CancellationToken stoppingToken);
    }
}
