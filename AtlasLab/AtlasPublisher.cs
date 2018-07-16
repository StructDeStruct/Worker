using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Project.AtlasLab
{
    public class AtlasPublisher
    {
        public readonly MqService MqService;
        private readonly ILogger<AtlasPublisher> _logger;
            
        public AtlasPublisher(MqService mqService, ILogger<AtlasPublisher> logger)
        {
            MqService = mqService;
            _logger = logger;
        }
        
        public void Write()
        {
            _logger.LogInformation(
                "You can now send messages, type them till you get bored, then type 'quit it'");
            try
            {
                var letter = Console.ReadLine();
                var number = 0;
                while (letter != "quit it")
                {
                    var message = JsonConvert.SerializeObject(new Message
                    {
                        Letter = letter,
                        Number = number++
                    });
                    MqService.Channel.BasicPublish("", MqService.Config.QueueName, null,
                        Encoding.UTF8.GetBytes(message));
                    letter = Console.ReadLine();
                }
            }
            catch
            {
                new IOException();
            }
        }
    }
}