using System.Threading;
using AtlasLab.Abstract;
using AtlasLab.Core;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AtlasLab.App.Tests
{
    [Collection("Tests collection")]
    public class UnitTestInput
    {
       private readonly TestsFixture _fixture;
        
        public UnitTestInput(TestsFixture data)
        {
            _fixture = data;
        }
        
        [Fact]
        public void TestInput1()
        {
            _fixture.MockConfig.Setup(c => c["Mode"]).Returns("Write");
            _fixture.Config = new ConfigService(_fixture.MockConfig.Object);
            _fixture.MockInput.SetupSequence(i => i.Read()).Returns("works").Returns("quit it");
            var loggerHandler = new Mock<ILogger<UserHandler>>();
            _fixture.UserHandler = new UserHandler(_fixture.Consumer, _fixture.MockTimer.Object, _fixture.Publisher, _fixture.MockMq.Object, _fixture.Config, loggerHandler.Object);
            _fixture.UserHandler.StartAsync(new CancellationToken());
            _fixture.MockInput.Verify(i => i.Read(), Times.Exactly(2));
            _fixture.MockMq.Verify(mq => mq.Publish(It.IsAny<Message>()), Times.Once());
        }
    }
}
