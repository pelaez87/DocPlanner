using DocPlanner.Availability.Domain.Model;

namespace DocPlanner.Availability.API.Dto
{
    public record WeekSlotsDto
    {
        public string FacilityName { get; set; }
        public string FacilityAddress { get; set; }

        public DateTime[] Monday { get; set; }
        public DateTime[] Tuesday { get; set; }
        public DateTime[] Wednesday { get; set; }
        public DateTime[] Thursday { get; set; }
        public DateTime[] Friday { get; set; }
        public DateTime[] Saturday { get; set; }
        public DateTime[] Sunday { get; set; }

        public static WeekSlotsDto Create(WeekSlots model)
        {
            return new WeekSlotsDto
            {
                FacilityName = model.Facility.Name,
                FacilityAddress = model.Facility.Address,
                Monday = model.Monday ?? Array.Empty<DateTime>(),
                Tuesday = model.Tuesday ?? Array.Empty<DateTime>(),
                Wednesday = model.Wednesday ?? Array.Empty<DateTime>(),
                Thursday = model.Thursday ?? Array.Empty<DateTime>(),
                Friday = model.Friday ?? Array.Empty<DateTime>(),
                Saturday = model.Saturday ?? Array.Empty<DateTime>(),
                Sunday = model.Sunday ?? Array.Empty<DateTime>()
            };
        }
    }
}
