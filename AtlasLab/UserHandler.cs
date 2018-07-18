using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class UserHandler : IHostedService
    {
        private readonly IAtlasConsumer _cons;
        private readonly IAtlasPublisher _pub;
        private readonly IConfigService _config;
        private readonly IMqService _mqService;
        private readonly ILogger<UserHandler> _logger;
        
        public UserHandler(AtlasConsumer cons, AtlasPublisher pub, MqService mqService,
            ConfigService config, ILogger<UserHandler> logger)
        {
            _cons = cons;
            _pub = pub;
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
                    _cons.TimerService.Repeat(_cons.Read);
                    break;
                case "Write":
                    _pub.Write();
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
            _cons.TimerService.StopRepeating();
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