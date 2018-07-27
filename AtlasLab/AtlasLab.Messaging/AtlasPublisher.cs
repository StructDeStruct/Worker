using System.IO;
using AtlasLab.Abstract;
using Microsoft.Extensions.Logging;

namespace AtlasLab.Messaging
{
    public class AtlasPublisher : IAtlasPublisher, IService
    {
        private readonly IMqService _mqService;
        private readonly IInputService _input;
        private readonly ILogger<AtlasPublisher> _logger;
            
        public AtlasPublisher(IMqService mqService, IInputService input,
            ILogger<AtlasPublisher> logger)
        {
            _mqService = mqService;
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
                    _mqService.Publish(message);
                    letter = _input.Read();
                }
            }
            catch(IOException)
            {
                throw new IOException("Cancelation message \"quit it\" was awaited");
            }
        }
    }
}