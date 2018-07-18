using System;
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class MqService : IDisposable, IMqService
    {
        private readonly ISerializeService _serialize;
        private readonly IDeserializeService _deserialize;
        public IConfigService Config { get; set; }
        private readonly IConnection _conn;
        public readonly IModel Channel;

        public MqService(ISerializeService serialize, IDeserializeService deserialize,
            IConfigService config)
        {
            _serialize = serialize;
            _deserialize = deserialize;
            Config = config;
            var factory = new ConnectionFactory();
            factory.UserName = Config.UserName;
            factory.Password = Config.Password;
            factory.VirtualHost = Config.VirtualHost;
            factory.HostName = Config.HostName;
            factory.Port = Config.Port;
            _conn = factory.CreateConnection();
            Channel = _conn.CreateModel();
            Channel.QueueDeclare(Config.QueueName, true, false, false, null);
        }

        public void Publish(IMessage message)
        {
            var data = _serialize.Serialize(message);
            
            Channel.BasicPublish("", Config.QueueName, null,
                Encoding.UTF8.GetBytes(data));
        }

        public IMessage Get()
        {
            var data = Channel.BasicGet(Config.QueueName, false);
            if (data == null)
            {
                return null;
            }
            Channel.BasicAck(data.DeliveryTag, false);
            return _deserialize.Deserialize(Encoding.Default.GetString(data.Body));
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