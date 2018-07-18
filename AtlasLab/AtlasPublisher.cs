using System.IO;
using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class AtlasPublisher : IAtlasPublisher
    {
        public readonly IMqService MqService;
        private readonly IInputService _input;
        private readonly ILogger<AtlasPublisher> _logger;
            
        public AtlasPublisher(MqService mqService, InputService input,
            ILogger<AtlasPublisher> logger)
        {
            MqService = mqService;
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
                    var message = new Message
                    {
                        Letter = letter,
                        Number = number++
                    };
                    MqService.Publish(message);
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