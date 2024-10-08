using System.Text.Json.Serialization;

namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record TakeSlotRequest
    {
        [JsonConverter(typeof(SlotServiceDateFormatter))]
        public DateTime Start { get; set; }
        [JsonConverter(typeof(SlotServiceDateFormatter))]
        public DateTime End { get; set; }
        public string Comments { get; set; }
        public PatientDetails Patient { get; set; }
        public Guid FacilityId { get; set; }
    }
}
