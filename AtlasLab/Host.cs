using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace Project.AtlasLab
{
    class Host
    {
        public static async Task Main(string[] args)
        {
            
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) => 
                {
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                       config.AddCommandLine(args);
                    }
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<InputService>();
                    services.AddSingleton<OutputService>();
                    services.AddSingleton<SerializeService>();
                    services.AddSingleton<DeserializeService>();
                    services.AddSingleton<ConfigService>();
                    services.AddSingleton<MqService>();
                    services.AddSingleton<TimerService>();
                    services.AddSingleton<AtlasConsumer>();
                    services.AddSingleton<AtlasPublisher>();
                    services.AddSingleton<IHostedService, UserHandler>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
