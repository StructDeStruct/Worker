using AtlasLab.Abstract;
using AtlasLab.Core;
using AtlasLab.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace AtlasLab.App.Tests
{
    public class TestsFixture 
    {
        public readonly Mock<IConfiguration> MockConfig;
        public readonly Mock<IMqService> MockMq;
        public ConfigService Config;
        public readonly Mock<IInputService> MockInput;
        public readonly AtlasPublisher Publisher;
        public readonly Mock<IOutputService> MockOutput;
        public readonly Mock<ITimerService> MockTimer;
        public readonly AtlasConsumer Consumer;
        public UserHandler UserHandler;
        
        public TestsFixture()
        {
            MockConfig = new Mock<IConfiguration>();
            MockConfig.Setup(c => c["QueueName"]).Returns("Queue");
            MockConfig.Setup(c => c["UserName"]).Returns("guest");
            MockConfig.Setup(c => c["Password"]).Returns("guest");
            MockConfig.Setup(c => c["VirtualHost"]).Returns("/");
            MockConfig.Setup(c => c["HostName"]).Returns("localhost");
            MockConfig.Setup(c => c["Port"]).Returns("5672");
            
            MockMq = new Mock<IMqService>();
            MockInput = new Mock<IInputService>();
            var loggerPublisher = new Mock<ILogger<AtlasPublisher>>();
            Publisher = new AtlasPublisher(MockMq.Object, MockInput.Object, loggerPublisher.Object);
            var loggerConsumer = new Mock<ILogger<AtlasConsumer>>();
            MockTimer = new Mock<ITimerService>();
            MockOutput = new Mock<IOutputService>();
            Consumer = new AtlasConsumer(MockMq.Object, MockOutput.Object, loggerConsumer.Object);  
        }
    }
}