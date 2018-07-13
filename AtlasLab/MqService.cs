using System;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class MqService
    {
        public readonly ILogger _logger;
        public Timer _timer;
        public readonly IConnection _conn;
        public readonly IModel _channel;
        public readonly IConfiguration _config;

        public MqService(ILogger<MqService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            var factory = new ConnectionFactory();
            factory.UserName = _config["UserName"];
            factory.Password = _config["Password"];
            factory.VirtualHost = _config["VirtualHost"];
            factory.HostName = _config["HostName"];
            factory.Port = Int32.Parse(_config["Port"]);
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(_config["QueueName"], true, false, false, null);
        }
    }
}