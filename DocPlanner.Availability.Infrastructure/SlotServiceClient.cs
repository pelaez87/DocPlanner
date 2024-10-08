using DocPlanner.Availability.Domain;
using DocPlanner.Availability.Domain.Contract;
using DocPlanner.Availability.Domain.Model;
using DocPlanner.Availability.Infrastructure.ExternalDto;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace DocPlanner.Availability.Infrastructure
{
    /// <summary>
    /// Handles available slots on doctor's agenda.
    /// </summary>
    /// <seealso cref="IAvailabilityService" />
    public sealed class SlotServiceClient : IAvailabilityService
    {
        public const string HttpClientKey = "slot-service-client";
        private readonly HttpClient _httpClient;
        private readonly ILogger<SlotServiceClient> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlotServiceClient" /> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        public SlotServiceClient(ILogger<SlotServiceClient> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(HttpClientKey);
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task<WeekSlots> GetWeekAvailableSlots(DateTime slotDate, CancellationToken cancellationToken)
        {
            var mondayDate = slotDate.GetMondayOfTheWeek();
            var response = await GetWeekAvailability(slotDate, cancellationToken);

            return GenerateAvailableWeekSlots(mondayDate, response);
        }

        /// <inheritdoc/>
        public async Task TakeSlot(DateTime slotDate, string comments, Patient patient, CancellationToken cancellationToken)
        {
            // Can a slot be taken for some reason if already booked?
            // Given sample data on 2024-10-07 there are a couple of busy slots on 2024-10-07T12:30:00
            // So we can do a validation here, but I guess we should let to external API handle it
            // Maybe there are multiple doctors available in a next iteration of the external API, and it's not our responsability here

            var weekAvailability = await GetWeekAvailability(slotDate, cancellationToken);

            var requestPath = "TakeSlot";
            var requestContent = new TakeSlotRequest
            {
                Start = slotDate,
                End = slotDate.AddMinutes(weekAvailability.SlotDurationMinutes), // why is that needed? can we book several slots at once maybe?
                Comments = comments,
                Patient = new PatientDetails
                {
                    Name = patient.Name,
                    SecondName = patient.SecondName,
                    Email = patient.Email,
                    Phone = patient.PhoneNumber
                },
                FacilityId = weekAvailability.Facility.FacilityId
            };

            _logger.LogInformation("Calling external service with path {urlPath}", requestPath);
            var httpResult = await _httpClient.PostAsJsonAsync(requestPath, requestContent, cancellationToken);

            if (!httpResult.IsSuccessStatusCode)
            {
                var errorMessage = await httpResult.Content.ReadAsStringAsync();
                _logger.LogError("External service failed with status code {httpStatusCode} and message {errorMessage}.", errorMessage, httpResult.StatusCode);
                throw new InvalidOperationException(errorMessage);
            }
        }

        private async Task<GetWeekAvailabilityResponse> GetWeekAvailability(DateTime slotDate, CancellationToken cancellationToken)
        {
            // Consider caching result and invalidate in TakeSlot method
            // if data is not changing so often and can take advantage when being queried

            var mondayDate = slotDate.GetMondayOfTheWeek();
            var requestPath = $"GetWeeklyAvailability/{mondayDate:yyyyMMdd}";

            _logger.LogInformation("Calling external service with path {urlPath}", requestPath);
            var response = await _httpClient.GetFromJsonAsync<GetWeekAvailabilityResponse>(requestPath, cancellationToken);

            return response;
        }

        private WeekSlots GenerateAvailableWeekSlots(DateTime mondayDate, GetWeekAvailabilityResponse weekAvailability)
        {
            // Maybe there is a more elegant way to handle daily available slots than needing to use each day specific date as parameter
            var mondaySlots = GetAvailableSlots(mondayDate, weekAvailability.Monday, weekAvailability.SlotDurationMinutes);
            var tuesdaySlots = GetAvailableSlots(mondayDate.AddDays(1), weekAvailability.Tuesday, weekAvailability.SlotDurationMinutes);
            var wednesdaySlots = GetAvailableSlots(mondayDate.AddDays(2), weekAvailability.Wednesday, weekAvailability.SlotDurationMinutes);
            var thursdaySlots = GetAvailableSlots(mondayDate.AddDays(3), weekAvailability.Thursday, weekAvailability.SlotDurationMinutes);
            var fridaySlots = GetAvailableSlots(mondayDate.AddDays(4), weekAvailability.Friday, weekAvailability.SlotDurationMinutes);
            var saturdaySlots = GetAvailableSlots(mondayDate.AddDays(5), weekAvailability.Saturday, weekAvailability.SlotDurationMinutes);
            var sundaySlots = GetAvailableSlots(mondayDate.AddDays(6), weekAvailability.Sunday, weekAvailability.SlotDurationMinutes);

            var facility = new Facility(id: weekAvailability.Facility.FacilityId, name: weekAvailability.Facility.Name, address: weekAvailability.Facility.Address);

            return new WeekSlots(facility, mondaySlots, tuesdaySlots, wednesdaySlots, thursdaySlots, fridaySlots, saturdaySlots, sundaySlots);
        }

        private DateTime[] GetAvailableSlots(DateTime dayScheduleDate, DaySchedule daySchedule, int slotDurationMinutes)
        {
            // Days can have no information around the work period at all
            if (daySchedule == null || daySchedule.WorkPeriod == null)
            {
                return Array.Empty<DateTime>();
            }

            var startTime = dayScheduleDate.WithHour(daySchedule.WorkPeriod.StartHour);
            var endTime = dayScheduleDate.WithHour(daySchedule.WorkPeriod.EndHour);
            var startLunchTime = dayScheduleDate.WithHour(daySchedule.WorkPeriod.LunchStartHour);
            var endLunchTime = dayScheduleDate.WithHour(daySchedule.WorkPeriod.LunchEndHour);

            var availableBeforeLunch = GetAvailableSlots(startTime, startLunchTime, slotDurationMinutes, daySchedule.BusySlots);
            var availableAfterLunch = GetAvailableSlots(endLunchTime, endTime, slotDurationMinutes, daySchedule.BusySlots);

            return availableBeforeLunch.Concat(availableAfterLunch).ToArray();
        }

        private IEnumerable<DateTime> GetAvailableSlots(DateTime startTime, DateTime endTime, int slotDurationMinutes, Slot[] busySlots)
        {
            if (endTime < startTime)
            {
                throw new ArgumentOutOfRangeException("Start time should be a date before end time.");
            }

            _logger.LogInformation("Generating available slots from {startTime} to {endTime}.", startTime, endTime);

            var proposedSlot = startTime;
            while (proposedSlot < endTime)
            {
                if (!(busySlots?.Any(busySlot => busySlot.Start.Equals(proposedSlot)) ?? false))
                {
                    yield return proposedSlot;
                }

                proposedSlot = proposedSlot.AddMinutes(slotDurationMinutes);
            }
        }
    }
}
