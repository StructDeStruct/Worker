using System;
using Microsoft.Extensions.Configuration;

namespace Project.AtlasLab
{
    public class ConfigService
    {
        public string QueueName { get; }
        public string Mode { get; }
        public string UserName { get; }
        public string Password { get; }
        public string VirtualHost { get; }
        public string HostName { get; }
        public int Port { get; }
        
        public ConfigService(IConfiguration config)
        {
            QueueName = config["QueueName"];
            Mode = config["Mode"];
            UserName = config["UserName"];
            Password = config["Password"];
            VirtualHost = config["VirtualHost"];
            HostName = config["HostName"];
            Port = Int32.Parse(config["Port"]);
        }
    }
}