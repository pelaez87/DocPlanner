namespace DocPlanner.Availability.API.Tests.Fixtures
{
    public class AvailabilityControllerTestsClassFixture : IAsyncLifetime
    {
        public string ApiEndpoint { get; } = "https://localhost:5556/availability/";

        public virtual Task InitializeAsync() => Task.CompletedTask;

        public virtual Task DisposeAsync() => Task.CompletedTask;
    }
}
