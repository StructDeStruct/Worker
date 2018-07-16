using System;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class MqService : IDisposable
    {
        private readonly ILogger _logger;
        public readonly ConfigService Config;
        private readonly IConnection _conn;
        public readonly IModel Channel;

        public MqService(ILogger<MqService> logger, ConfigService config)
        {
            _logger = logger;
            Config = config;
            var factory = new ConnectionFactory();
            factory.UserName = config.UserName;
            factory.Password = config.Password;
            factory.VirtualHost = config.VirtualHost;
            factory.HostName = config.HostName;
            factory.Port = config.Port;
            _conn = factory.CreateConnection();
            Channel = _conn.CreateModel();
            Channel.QueueDeclare(config.QueueName, true, false, false, null);
        }
        
        public void Dispose()
        {
            Channel?.Close();
            _conn?.Close();
        }
    }
}