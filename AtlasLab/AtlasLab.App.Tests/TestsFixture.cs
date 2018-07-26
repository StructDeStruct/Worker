using Microsoft.Extensions.Logging;
using Moq;
using AtlasLab.Abstract;
using AtlasLab.CoreAndInfrastructure;
using AtlasLab.Messaging;

namespace Worker.Tests
{
    public class TestsFixture 
    {
        public readonly Mock<IConfigService> MockConfig;
        public readonly Mock<IMqService> MockMq;
        public readonly Mock<IInputService> MockInput;
        public readonly AtlasPublisher Publisher;
        public readonly Mock<IOutputService> MockOutput;
        public readonly Mock<ITimerService> MockTimer;
        public readonly AtlasConsumer Consumer;
        public readonly UserHandler UserHandler;
        
        public TestsFixture()
        {
            MockConfig = new Mock<IConfigService>();
            MockConfig.Setup(c => c.QueueName).Returns("Queue");
            //MockConfig.Setup(c => c.Mode).Returns("Write");
            MockConfig.Setup(c => c.UserName).Returns("guest");
            MockConfig.Setup(c => c.Password).Returns("guest");
            MockConfig.Setup(c => c.VirtualHost).Returns("/");
            MockConfig.Setup(c => c.HostName).Returns("localhost");
            MockConfig.Setup(c => c.Port).Returns(5672);

            MockMq = new Mock<IMqService>();
            MockInput = new Mock<IInputService>();
            var loggerPublisher = new Mock<ILogger<AtlasPublisher>>();
            Publisher = new AtlasPublisher(MockMq.Object, MockInput.Object, loggerPublisher.Object);
            var loggerConsumer = new Mock<ILogger<AtlasConsumer>>();
            MockTimer = new Mock<ITimerService>();
            MockOutput = new Mock<IOutputService>();
            Consumer = new AtlasConsumer(MockMq.Object, MockTimer.Object, MockOutput.Object, loggerConsumer.Object);
            var loggerHandler = new Mock<ILogger<UserHandler>>();
            UserHandler = new UserHandler(Consumer, Publisher, MockMq.Object, MockConfig.Object, loggerHandler.Object);
        }
    }
}