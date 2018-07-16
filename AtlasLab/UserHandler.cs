using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class UserHandler : IHostedService
    {
        private readonly AtlasConsumer _cons;
        private readonly AtlasPublisher _pub;
        private readonly ILogger<UserHandler> _logger;
        
        public UserHandler(AtlasConsumer cons, AtlasPublisher pub, ILogger<UserHandler> logger)
        {
            _cons = cons;
            _pub = pub;
            _logger = logger;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting");
            var mode = _cons.MqService.Config.Mode;
            switch (mode) 
            {
                case "Read":
                    _cons.TimerService.Timer = new Timer(_cons.Read, null, TimeSpan.Zero,
                            TimeSpan.FromSeconds(5));
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
            _cons.TimerService.Timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        
        private void Look() {
            _logger.LogInformation(
                $"There are {_cons.MqService.Channel.MessageCount(_cons.MqService.Config.QueueName)} messages in the queue right now!");
        }
        
        private void Purge() {
            _cons.MqService.Channel.QueuePurge(_cons.MqService.Config.QueueName);
            _logger.LogInformation("Queue was purged");
        }
    }
}