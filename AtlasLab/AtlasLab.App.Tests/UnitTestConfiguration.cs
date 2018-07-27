using System;
using System.Configuration;
using AtlasLab.Core;
using Xunit;

namespace AtlasLab.App.Tests
{
    [Collection("Tests collection")]
    public class UnitTestConfiguration
    {
        private readonly TestsFixture _fixture;
        
        public UnitTestConfiguration(TestsFixture data)
        {
            _fixture = data;
        }
        
        [Fact]
        public void TestConfiguration1()
        {
            _fixture.MockConfig.Setup(c => c["Port"]).Returns("7b8h");
            Assert.Throws<ConfigurationException>(() => new ConfigService(_fixture.MockConfig.Object));
            // After test is done we return standart value for the next tests
            _fixture.MockConfig.Setup(c => c["Port"]).Returns("5672");
        }

        [Fact]
        public void TestConfiguration2()
        {
            _fixture.MockConfig.Setup(c => c["Username"]).Returns("");
            _fixture.MockConfig.Setup(c => c["VirtualHost"]).Returns("");
            Assert.Throws<ArgumentException>(() => new ConfigService(_fixture.MockConfig.Object));
            _fixture.MockConfig.Setup(c => c["Username"]).Returns("guest");
            _fixture.MockConfig.Setup(c => c["VirtualHost"]).Returns("/");
        }
    }
}