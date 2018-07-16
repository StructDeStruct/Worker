using System;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class AtlasConsumer
    {
        public readonly MqService MqService;
        private readonly ILogger<AtlasConsumer> _logger;
        public readonly TimerService TimerService;
        
        public AtlasConsumer(MqService mqService, TimerService timerService, ILogger<AtlasConsumer> logger)
        {
            MqService = mqService;
            TimerService = timerService;
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
                message = JsonConvert.DeserializeObject<Message>(Encoding.Default.GetString(result.Body));
                Console.WriteLine($"{message.Number} - {message.Letter}");
                MqService.Channel.BasicAck(result.DeliveryTag, false);
                result = MqService.Channel.BasicGet(MqService.Config.QueueName, false);
            }
        }
    }
}