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
        public readonly MqService _mqService;
        
        public AtlasPublisher(MqService mqService)
        {
            _mqService = mqService;
        }
        
        public void Write()
        {
            _mqService._logger.LogInformation(
                "You can now send messages, type them till you get bored, then type 'quit it'");
            try
            {
                var letter = Console.ReadLine();
                var number = 0;
                while (letter != "quit it")
                {
                    var message = JsonConvert.SerializeObject(new Message
                    {
                        letter = letter,
                        number = number++
                    });
                    _mqService._channel.BasicPublish("", _mqService._config["QueueName"], null,
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