using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class AtlasPublisher
    {
        public readonly MqService MqService;
        private readonly SerializeService _serialize;
        private readonly InputService _input;
        private readonly ILogger<AtlasPublisher> _logger;
            
        public AtlasPublisher(MqService mqService, SerializeService serialize,
            InputService input, ILogger<AtlasPublisher> logger)
        {
            MqService = mqService;
            _serialize = serialize;
            _input = input;
            _logger = logger;
        }
        
        public void Write()
        {
            _logger.LogInformation(
                "You can now send messages, type them till you get bored, then type 'quit it'");
            try
            {
                var letter = _input.Read();
                var number = 0;
                while (letter != "quit it")
                {
                    var message = _serialize.Serialize(new Message
                    {
                        Letter = letter,
                        Number = number++
                    });
                    MqService.Channel.BasicPublish("", MqService.Config.QueueName, null,
                        Encoding.UTF8.GetBytes(message));
                    letter = _input.Read();
                }
            }
            catch
            {
                new IOException();
            }
        }
    }
}