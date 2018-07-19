using System;
using System.Threading;
using Xunit;
using Moq;
using Project.AtlasLab;

namespace Worker.Tests
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
            _fixture.MockConfig.Setup(c => c.Mode).Returns("Write");
            _fixture.MockInput.SetupSequence(i => i.Read()).Returns("works").Returns("quit it");
            _fixture.UserHandler.StartAsync(new CancellationToken());
            _fixture.MockInput.Verify(i => i.Read(), Times.Exactly(2));
            _fixture.MockMq.Verify(mq => mq.Publish(It.IsAny<Message>()), Times.Once());
        }
    }
}
