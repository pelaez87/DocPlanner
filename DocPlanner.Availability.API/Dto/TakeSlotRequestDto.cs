namespace DocPlanner.Availability.API.Dto
{
    public record TakeSlotRequestDto
    {
        public DateTime SlotDate { get; set; }
        public string Comments { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}