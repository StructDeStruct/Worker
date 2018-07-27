using System.Threading;
using System.Threading.Tasks;
using AtlasLab.Abstract;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AtlasLab.Core
{
    public class UserHandler : IHostedService
    {
        private readonly IAtlasConsumer _consumer;
        private readonly ITimerService _timer;
        private readonly IAtlasPublisher _publisher;
        private readonly IConfigService _config;
        private readonly IMqService _mqService;
        private readonly ILogger<UserHandler> _logger;
        
        public UserHandler(IAtlasConsumer consumer, ITimerService timer, IAtlasPublisher publisher, IMqService mqService,
            IConfigService config, ILogger<UserHandler> logger)
        {
            _consumer = consumer;
            _timer = timer;
            _publisher = publisher;
            _config = config;
            _mqService = mqService;
            _logger = logger;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting");
            var mode = _config.Mode;
            switch (mode) 
            {
                case "Read":
                    _timer.Repeat(_consumer.Read);
                    break;
                case "Write":
                    _publisher.Write();
                    break;
                case "Look":
                    Look();
                    break;
                case "Purge":
                    Purge();
                    break;
                default:
                    _logger.LogError("Error: Wrong mode");
                    break;
            }    
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping.");
            _timer.StopRepeating();
            return Task.CompletedTask;
        }
        
        private void Look() {
            _logger.LogInformation(
                $"There are {_mqService.MessageCount()} messages in the queue right now!");
        }
        
        private void Purge() {
            _mqService.Purge();
            _logger.LogInformation("Queue was purged");
        }
    }
}