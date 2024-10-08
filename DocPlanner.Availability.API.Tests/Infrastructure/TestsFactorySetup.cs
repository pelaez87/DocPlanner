using Microsoft.AspNetCore.Mvc.Testing;

namespace DocPlanner.Availability.API.Tests.Infrastructure
{
    public class TestsFactorySetup : IAsyncDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public TestsFactorySetup()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_URLS", "http://+:5555;https://+:5556");

            _factory = new TestsFactory();

            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public IServiceProvider ServiceProvider => _factory.Server.Services;

        public HttpClient GetHttpClient()
        {
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }

        public async ValueTask DisposeAsync()
        {
            if (_factory != null)
            {
                await _factory.DisposeAsync();
            }
        }
    }
}
