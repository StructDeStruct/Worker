using System;
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class MqService : IDisposable, IMqService
    {
        private readonly ILogger _logger;
        private readonly SerializeService _serialize;
        private readonly DeserializeService _deserialize;
        
        public ConfigService Config { get; set; }
        //public readonly ConfigService Config;
        private readonly IConnection _conn;
        public readonly IModel Channel;

        public MqService(ILogger<MqService> logger, SerializeService serialize,
            DeserializeService deserialize, ConfigService config)
        {
            _logger = logger;
            _serialize = serialize;
            _deserialize = deserialize;
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

        public void Publish(Message message)
        {
            var data = _serialize.Serialize(message);
            
            Channel.BasicPublish("", Config.QueueName, null,
                Encoding.UTF8.GetBytes(data));
        }

        public Message Get()
        {
            var data = Channel.BasicGet(Config.QueueName, false);
            if (data == null)
            {
                return null;
            }
            Channel.BasicAck(data.DeliveryTag, false);
            return _deserialize.Deserialize(Encoding.Default.GetString(data.Body));
            /*return data != null
                ? _deserialize.Deserialize(Encoding.Default.GetString(data.Body))
                : null;*/
        }

        public uint MessageCount()
        {
            return Channel.MessageCount(Config.QueueName);
        }

        public void Purge()
        {
            Channel.QueuePurge(Config.QueueName);
        }
        public void Dispose()
        {
            Channel?.Close();
            _conn?.Close();
        }
    }
}