using DocPlanner.Availability.API.Dto;
using DocPlanner.Availability.API.Tests.Fixtures;
using DocPlanner.Availability.API.Tests.Infrastructure;
using System.Net;
using System.Net.Http.Json;

namespace DocPlanner.Availability.API.Tests
{
    [Collection(TestsFactoryCollection.FactoryCollectionName)]
    public class AvailabilityControllerTests : IClassFixture<AvailabilityControllerTestsClassFixture>, IDisposable
    {
        private const string _jsonMediaType = "application/json";
        private readonly HttpClient _httpClient;

        public AvailabilityControllerTests(TestsFactorySetup factory, AvailabilityControllerTestsClassFixture fixture)
        {
            _httpClient = factory.GetHttpClient();
            _httpClient.BaseAddress = new Uri(fixture.ApiEndpoint);
        }

        [Fact]
        public async Task GivenRequest_GetWeekSlots_ThenApiReturnsExpectedData()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.OK;

            // Act
            var response = await _httpClient.GetAsync("WeekSlots/2024-10-08", CancellationToken.None);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);

            // We should find an strategy for checking data in a way it doesn't change, as here we don't mock it
        }

        [Fact]
        public async Task GivenRequest_GetWeekSlots_WithInvalidQueryString_ThenApiReturnsExpectedData()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.BadRequest;

            // Act
            var response = await _httpClient.GetAsync("WeekSlots/2024-10-66", CancellationToken.None);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        [Fact]
        public async Task GivenRequest_TakeSlot_ThenApiReturnsExpectedData()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var expectedContent = new WeekSlotsDto();

            // Act
            var weekSlotsResponse = await _httpClient.GetAsync("WeekSlots/2024-09-08", CancellationToken.None);
            var availableSlots = await weekSlotsResponse.Content.ReadFromJsonAsync<WeekSlotsDto>();
            var allSlots = availableSlots.Monday
                .Concat(availableSlots.Tuesday)
                .Concat(availableSlots.Wednesday)
                .Concat(availableSlots.Thursday)
                .Concat(availableSlots.Friday)
                .Concat(availableSlots.Saturday)
                .Concat(availableSlots.Sunday);
            // It will fail if we run tests a lot of times, we should get the slot back to the system later
            // Or find another strategy for testing with real data
            var takeSlotRequest = new TakeSlotRequestDto
            {
                SlotDate = allSlots.First(),
                Comments = "I have headache.",
                Name = "David",
                SecondName = "Peláez",
                Email = "me@provider.com",
                Phone = "123-456-789"
            };
            var httpContent = JsonContent.Create(takeSlotRequest);
            var response = await _httpClient.PostAsync("TakeSlot", httpContent, CancellationToken.None);

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await response.Content.ReadFromJsonAsync<WeekSlotsDto>());
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
