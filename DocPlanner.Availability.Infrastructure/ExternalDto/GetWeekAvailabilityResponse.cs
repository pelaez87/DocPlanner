﻿namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record GetWeekAvailabilityResponse
    {
        public FacilityDetails Facility { get; set; }
        public int SlotDurationMinutes { get; set; }
        public DaySchedule Monday { get; set; }
        public DaySchedule Tuesday { get; set; }
        public DaySchedule Wednesday { get; set; }
        public DaySchedule Thursday { get; set; }
        public DaySchedule Friday { get; set; }
        public DaySchedule Saturday { get; set; }
        public DaySchedule Sunday { get; set; }
    }
}
