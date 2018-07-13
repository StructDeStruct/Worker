using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Project.AtlasLab
{
    public class User_Handler : IHostedService, IDisposable
    {
        private readonly AtlasConsumer _cons;
        private readonly AtlasPublisher _pub;
        
        public User_Handler(AtlasConsumer cons, AtlasPublisher pub)
        {
            _cons = cons;
            _pub = pub;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cons._mqService._logger.LogInformation("Starting");
            _cons._mqService._logger.LogInformation("Type 'r' or 'w' to choose mode");
            var mode = Console.ReadKey(true).KeyChar;
            _cons._mqService._logger.LogInformation($"mode - {mode}");

            switch (mode) 
            {
                case 'r':
                    _cons._mqService._timer = new Timer(_cons.Read, null, TimeSpan.Zero,
                            TimeSpan.FromSeconds(5));
                    break;
                case 'w':
                    _pub.Write();
                    break;
                case 'l':
                    Look();
                    break;
                case 'p':
                    Purge();
                    break;
                default:
                    _cons._mqService._logger.LogError("Error: Wrong mode");
                    break;
            }    
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cons._mqService._logger.LogInformation("Stopping.");

            _cons._mqService._timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        
        private void Look() {
            _cons._mqService._logger.LogInformation(
                $"There are {_cons._mqService._channel.MessageCount(_cons._mqService._config["QueueName"])} messages in the queue right now!");
        }
        
        private void Purge() {
            _cons._mqService._channel.QueuePurge(_cons._mqService._config["QueueName"]);
            _cons._mqService._logger.LogInformation("Queue was purged");
        }

        public void Dispose()
        {
            _cons._mqService._timer?.Dispose();
            _cons._mqService._channel?.Close();
            _cons._mqService._conn?.Close();
        }
    }
}