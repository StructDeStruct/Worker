using System;
using System.Text;
using AtlasLab.Abstract;
using RabbitMQ.Client;

namespace AtlasLab.Messaging
{
    public class MqService : IDisposable, IMqService, IService
    {
        private readonly ISerializeService _serialize;
        private readonly IDeserializeService _deserialize;
        private IConfigService _config;
        private readonly IConnection _conn;
        private readonly IModel _channel;

        public MqService(ISerializeService serialize, IDeserializeService deserialize,
            IConfigService config)
        {
            _serialize = serialize;
            _deserialize = deserialize;
            _config = config;
            var factory = new ConnectionFactory();
            factory.UserName = _config.UserName;
            factory.Password = _config.Password;
            factory.VirtualHost = _config.VirtualHost;
            factory.HostName = _config.HostName;
            factory.Port = _config.Port;
            _conn = factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(_config.QueueName, true, false, false, null);
        }

        public void Publish(Message message)
        {
            var data = _serialize.Serialize(message);
            
            _channel.BasicPublish("", _config.QueueName, null,
                Encoding.UTF8.GetBytes(data));
        }

        public Message Get()
        {
            var data = _channel.BasicGet(_config.QueueName, false);
            if (data == null)
            {
                return null;
            }
            _channel.BasicAck(data.DeliveryTag, false);
            return _deserialize.Deserialize(Encoding.Default.GetString(data.Body));
        }

        public uint MessageCount()
        {
            return _channel.MessageCount(_config.QueueName);
        }

        public void Purge()
        {
            _channel.QueuePurge(_config.QueueName);
        }
        public void Dispose()
        {
            _channel?.Close();
            _conn?.Close();
        }
    }
}