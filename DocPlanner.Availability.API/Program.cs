using DocPlanner.Availability.API.Dto;
using DocPlanner.Availability.API.Middleware;
using DocPlanner.Availability.API.Validation;
using DocPlanner.Availability.Domain.Contract;
using DocPlanner.Availability.Infrastructure;
using FluentValidation;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace DocPlanner.Availability.API
{
    /// <summary>
    /// Program type.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AddAvailabilityServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // That's the old way to have a global error handling, we should look into the new approach with IExceptionHandler chain
            // I'm not familiar still with the new approach
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }

        private static void AddAvailabilityServices(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IValidator<TakeSlotRequestDto>, TakeSlotRequestDtoValidator>();

            var endpoint = builder.Configuration.GetValue<string>("AvailabilityAPI:Endpoint");
            ArgumentException.ThrowIfNullOrWhiteSpace(endpoint);
            // Those below should ideally be retrieved from Key Vaults
            var endpointUser = builder.Configuration.GetValue<string>("AvailabilityAPI:User");
            ArgumentException.ThrowIfNullOrWhiteSpace(endpointUser);
            var endpointPassword = builder.Configuration.GetValue<string>("AvailabilityAPI:Password");
            ArgumentException.ThrowIfNullOrWhiteSpace(endpointPassword);

            builder.Services.AddHttpClient(SlotServiceClient.HttpClientKey, client =>
            {
                client.BaseAddress = new Uri(endpoint);

                var base64AuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{endpointUser}:{endpointPassword}"));
                client.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Basic {base64AuthenticationString}");
            }).AddStandardResilienceHandler();

            builder.Services.AddTransient<IAvailabilityService, SlotServiceClient>();
        }
    }
}
