using System.Threading;
using Moq;
using Xunit;

namespace Worker.Tests
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
            _fixture.MockConfig.Setup(c => c.Mode).Returns("Read");
            _fixture.MockTimer.Setup(t => t.Repeat(It.IsAny<TimerCallback>()))
                .Callback((TimerCallback call) => call.Invoke(new object()));
            _fixture.UserHandler.StartAsync(new CancellationToken());
            _fixture.MockMq.Verify(mq => mq.Get(), Times.Once);
        }
    }
}