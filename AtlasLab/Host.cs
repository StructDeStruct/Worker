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
                    services.AddSingleton<IInputService, InputService>();
                    services.AddSingleton<IOutputService, OutputService>();
                    services.AddSingleton<ISerializeService, SerializeService>();
                    services.AddSingleton<IDeserializeService, DeserializeService>();
                    services.AddSingleton<IConfigService, ConfigService>();
                    services.AddSingleton<IMqService, MqService>();
                    services.AddSingleton<ITimerService, TimerService>();
                    services.AddSingleton<IAtlasConsumer, AtlasConsumer>();
                    services.AddSingleton<IAtlasPublisher, AtlasPublisher>();
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
