namespace IntegratedSystems.Domain.Domain_Models
{
    public class Vaccine : BaseEntity
    {
        public string? Manufacturer { get; set; }
        public Guid? Certificate { get; set; }
        public DateTime DateTaken { get; set; }
        public Guid PatientId { get; set; }
        public virtual Patient? PatientFor { get; set; }
        public Guid VaccinationCenter { get; set; }
        public virtual VaccinationCenter? Center { get; set; }
    }
}
