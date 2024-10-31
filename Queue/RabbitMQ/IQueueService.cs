using Ke.Queue.RabbitMQ.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ke.Queue.RabbitMQ
{
    public interface IQueueService
    {
        IConnection CreateConnection(Options options);
    }
}
