namespace IntegratedSystems.Domain.Domain_Models
{
    public class VaccinationCenter : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int MaxCapacity { get; set; }
        public virtual ICollection<Vaccine>? Vaccines { get; set; }
    }
}
