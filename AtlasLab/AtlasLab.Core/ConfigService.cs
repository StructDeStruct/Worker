using System;
using System.Configuration;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using AtlasLab.Abstract;
using Microsoft.Extensions.Configuration;

namespace AtlasLab.Core
{
    public class ConfigService : IConfigService, IService
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
            try
            {
                QueueName = config["QueueName"];
                UserName = config["UserName"];
                Password = config["Password"];
                VirtualHost = config["VirtualHost"];
                HostName = config["HostName"];
                Port = Int32.Parse(config["Port"]);
                Mode = config["Mode"];
            }
            catch(FormatException)
            {
                throw new ConfigurationException("\"Port\" field contains wrong type value");
            }

            string missingFields = "";
            if (QueueName == "")
            {
                missingFields += "QueueName ";
            }
            if (UserName == "")
            {
                missingFields += "UserName ";
            }
            if (Password == "")
            {
                missingFields += "Password ";
            }
            if (VirtualHost == "")
            {
                missingFields += "VirtualHost ";
            }
            if (HostName == "")
            {
                missingFields += "HostName ";
            }
            if (Mode == "")
            {
                missingFields += "Mode ";
            }
            
            if (missingFields != "")
            {
                throw new ArgumentException("Following fields are missing from the configuration file: " + missingFields);
            }
        }
    }
}