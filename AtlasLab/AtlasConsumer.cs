using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class AtlasConsumer : IAtlasConsumer, IService
    {
        public IMqService MqService { get; set; }
        public ITimerService TimerService { get; set; }
        private readonly IOutputService _output;
        private readonly ILogger<AtlasConsumer> _logger;
        
        public AtlasConsumer(IMqService mqService, ITimerService timerService, 
            IOutputService output, ILogger<AtlasConsumer> logger)
        {
            MqService = mqService;
            TimerService = timerService;
            _output = output;
            _logger = logger;
        }
        
        public void Read(object state)
        {
            _logger.LogInformation(
                $"There are {MqService.MessageCount()} messages in the queue right now!");
            IMessage message = MqService.Get();
            while (message != null)
            {
                _output.Write($"{message.Number} - {message.Letter}");
                message = MqService.Get();
            }
        }
    }
}