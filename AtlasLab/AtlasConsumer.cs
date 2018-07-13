using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class AtlasConsumer
    {
        public readonly MqService _mqService;
        
        public AtlasConsumer(MqService mqService)
        {
            _mqService = mqService;
        }
        
        public void Read(object state)
        {
            _mqService._logger.LogInformation(
                $"There are {_mqService._channel.MessageCount(_mqService._config["QueueName"])} messages in the queue right now!");
            Message message;
            BasicGetResult result = _mqService._channel.BasicGet(_mqService._config["QueueName"], false);
            while (result != null)
            {
                message = JsonConvert.DeserializeObject<Message>(Encoding.Default.GetString(result.Body));
                Console.WriteLine($"{message.number} - {message.letter}");
                _mqService._channel.BasicAck(result.DeliveryTag, false);
                result = _mqService._channel.BasicGet(_mqService._config["QueueName"], false);
            }
        }
    }
}