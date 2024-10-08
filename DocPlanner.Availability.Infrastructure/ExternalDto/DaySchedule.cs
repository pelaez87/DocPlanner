namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record DaySchedule
    {
        public WorkPeriod WorkPeriod { get; set; }
        public Slot[] BusySlots { get; set; }
    }
}
