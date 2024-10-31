using Ke.Queue.RabbitMQ.Implementations.Listener;
using Ke.Queue.RabbitMQ.Implementations.Publisher;
using Ke.Queue.RabbitMQ.Interfaces.Listener;
using Ke.Queue.RabbitMQ.Interfaces.Publisher;
using Ke.Queue.RabbitMQ.Models;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ke.Queue.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceQueueRabbitMQ(this IServiceCollection services, Action<Options> configureOptions)
        {
            var options = new Options();
            configureOptions(options);

            services.AddSingleton(options);

            services.AddSingleton<IConnection>(serviceProvider =>
            {
                var connectionFactory = new ConnectionFactory()
                {
                    HostName = options.HostName,
                    Port = options.Port,
                    UserName = options.UserName,
                    Password = options.Password,
                };

                return connectionFactory.CreateConnection();
            });

            services.AddSingleton<IMessagePublisher, MessagePublisher>();
            services.AddSingleton<IMessageListener, MessageListener>();

            return services;
        }
    }
}
