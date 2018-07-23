using System;
using System.Linq;
using System.Reflection;
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
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    var serviceClasses = assembly.GetTypes().Where(s => s.GetInterface("IService") != null);
                    foreach (var service in serviceClasses)
                    {
                        var serviceInterface = service.GetInterface("I" + service.Name);
                        services.AddSingleton(serviceInterface, service);
                    }
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
