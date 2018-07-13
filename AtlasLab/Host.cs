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
                    services.AddSingleton<MqService>();
                    services.AddSingleton<AtlasConsumer>();
                    services.AddSingleton<AtlasPublisher>();
                    services.AddSingleton<IHostedService, User_Handler>();
                })
                .ConfigureLogging((hostingContext, logging) => {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
