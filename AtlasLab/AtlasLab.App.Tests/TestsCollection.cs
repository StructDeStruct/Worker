using Xunit;

namespace AtlasLab.App.Tests
{
    [CollectionDefinition("Tests collection")]
    public class TestsCollection : ICollectionFixture<TestsFixture>
    {
        
    }
}