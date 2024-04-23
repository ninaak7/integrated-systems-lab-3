using IntegratedSystems.Domain.Domain_Models;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccinationCenterService
    {
        public List<VaccinationCenter> GetVaccinationCenters();
        public VaccinationCenter GetVaccinationCenter(Guid? id);
        public void CreateVaccinationCenter(VaccinationCenter vaccinationCenter);
        public void UpdateVaccinationCenter(VaccinationCenter vaccinationCenter);
        public void DeleteVaccinationCenter(Guid? id);
        public void LowerCapacity(Guid? id);
    }
}
