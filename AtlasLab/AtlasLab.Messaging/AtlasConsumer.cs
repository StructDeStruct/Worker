using Microsoft.Extensions.Logging;
using AtlasLab.Abstract;

namespace AtlasLab.Messaging
{
    public class AtlasConsumer : IAtlasConsumer, IService
    {
        private IMqService _mqService { get; set; }
        private readonly IOutputService _output;
        private readonly ILogger<AtlasConsumer> _logger;
        
        public AtlasConsumer(IMqService mqService, IOutputService output, ILogger<AtlasConsumer> logger)
        {
            _mqService = mqService;
            _output = output;
            _logger = logger;
        }
        
        public void Read(object state)
        {
            _logger.LogInformation(
                $"There are {_mqService.MessageCount()} messages in the queue right now!");
            Message message = _mqService.Get();
            while (message != null)
            {
                _output.Write($"{message.Number} - {message.Letter}");
                message = _mqService.Get();
            }
        }
    }
}