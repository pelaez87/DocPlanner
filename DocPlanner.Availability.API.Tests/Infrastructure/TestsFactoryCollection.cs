namespace DocPlanner.Availability.API.Tests.Infrastructure
{
    [CollectionDefinition(FactoryCollectionName)]
    public class TestsFactoryCollection : ICollectionFixture<TestsFactorySetup>
    {
        public const string FactoryCollectionName = "TEST server";
    }
}
