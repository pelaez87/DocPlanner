namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record Slot
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
