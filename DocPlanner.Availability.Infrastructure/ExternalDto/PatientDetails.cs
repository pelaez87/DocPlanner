﻿namespace DocPlanner.Availability.Infrastructure.ExternalDto
{
    public record PatientDetails
    {
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
