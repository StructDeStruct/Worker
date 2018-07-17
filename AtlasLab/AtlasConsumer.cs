using System;
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class AtlasConsumer
    {
        public readonly MqService MqService;
        public readonly TimerService TimerService;
        private readonly DeserializeService _deserialize;
        private readonly OutputService _output;
        private readonly ILogger<AtlasConsumer> _logger;
        
        public AtlasConsumer(MqService mqService, TimerService timerService, DeserializeService deserialize, 
            OutputService output, ILogger<AtlasConsumer> logger)
        {
            MqService = mqService;
            TimerService = timerService;
            _deserialize = deserialize;
            _output = output;
            _logger = logger;
            
        }
        
        public void Read(object state)
        {
            _logger.LogInformation(
                $"There are {MqService.Channel.MessageCount(MqService.Config.QueueName)} messages in the queue right now!");
            Message message;
            BasicGetResult result = MqService.Channel.BasicGet(MqService.Config.QueueName, false);
            while (result != null)
            {
                message = _deserialize.Deserialize(Encoding.Default.GetString(result.Body));
                _output.Write($"{message.Number} - {message.Letter}");
                MqService.Channel.BasicAck(result.DeliveryTag, false);
                result = MqService.Channel.BasicGet(MqService.Config.QueueName, false);
            }
        }
    }
}