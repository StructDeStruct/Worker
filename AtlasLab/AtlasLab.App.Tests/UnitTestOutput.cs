using System.Threading;
using AtlasLab.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AtlasLab.App.Tests
{
    [Collection("Tests collection")]
    public class UnitTestOutput
    {
        private readonly TestsFixture _fixture;
        
        public UnitTestOutput(TestsFixture data)
        {
            _fixture = data;
        }
        
        
        [Fact]
        public void TestOutput1()
        {
            _fixture.MockConfig.Setup(c => c["Mode"]).Returns("Read");
            _fixture.Config = new ConfigService(_fixture.MockConfig.Object);
            _fixture.MockTimer.Setup(t => t.Repeat(It.IsAny<TimerCallback>()))
                .Callback((TimerCallback call) => call.Invoke(new object()));
            var loggerHandler = new Mock<ILogger<UserHandler>>();
            _fixture.UserHandler = new UserHandler(_fixture.Consumer, _fixture.MockTimer.Object, _fixture.Publisher, _fixture.MockMq.Object, _fixture.Config, loggerHandler.Object);
            _fixture.UserHandler.StartAsync(new CancellationToken());
            _fixture.MockMq.Verify(mq => mq.Get(), Times.Once);
        }
    }
}