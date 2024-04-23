using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccinationCenterService : IVaccinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenterRepository;
        //private readonly IRepository<Vaccine> _vaccineRepository;
        //private readonly IRepository<Patient> _patientRepository;

        public VaccinationCenterService(IRepository<VaccinationCenter> vaccinationCenterRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
        }

        public void CreateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            _vaccinationCenterRepository.Insert(vaccinationCenter);
        }

        public void DeleteVaccinationCenter(Guid? id)
        {
            VaccinationCenter center = _vaccinationCenterRepository.Get(id);
            _vaccinationCenterRepository.Delete(center);
        }

        public VaccinationCenter GetVaccinationCenter(Guid? id)
        {
            return _vaccinationCenterRepository.Get(id);
        }

        public List<VaccinationCenter> GetVaccinationCenters()
        {
            return _vaccinationCenterRepository.GetAll().ToList();
        }

        public void LowerCapacity(Guid? id)
        {
            VaccinationCenter center = _vaccinationCenterRepository.Get(id);
            center.MaxCapacity--;
            _vaccinationCenterRepository.Update(center);
        }

        public void UpdateVaccinationCenter(VaccinationCenter vaccinationCenter)
        {
            _vaccinationCenterRepository.Update(vaccinationCenter);
        }
    }
}
