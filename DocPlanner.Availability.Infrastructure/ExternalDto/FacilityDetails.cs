namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record FacilityDetails
    {
        public Guid FacilityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
