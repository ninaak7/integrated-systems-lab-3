﻿namespace IntegratedSystems.Domain.Domain_Models
{
    public class Patient : BaseEntity
    {
        public string? Embg { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ICollection<Vaccine>? VaccinationSchedule { get; set; }
    }
}
