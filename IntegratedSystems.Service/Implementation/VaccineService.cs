using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Service.Implementation
{
    public class VaccineService : IVaccineService
    {
        private readonly IRepository<VaccinationCenter> _vaccinationCenterRepository;
        private readonly IRepository<Vaccine> _vaccineRepository;
        private readonly IRepository<Patient> _patientRepository;

        public VaccineService(IRepository<VaccinationCenter> vaccinationCenterRepository, IRepository<Vaccine> vaccineRepository, IRepository<Patient> patientRepository)
        {
            _vaccinationCenterRepository = vaccinationCenterRepository;
            _vaccineRepository = vaccineRepository;
            _patientRepository = patientRepository;
        }

        public List<Vaccine> GetVaccinesForPatient(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(z => z.PatientId == id).ToList();    
        }

        public List<Vaccine> GetVaccinesForVaccinationCenter(Guid? id)
        {
            return _vaccineRepository.GetAll().Where(z => z.VaccinationCenter == id).ToList();
        }

        public void ScheduleVaccine(string manufacturer, DateTime dateTaken, Guid patientId, Guid centerId)
        {
            Vaccine newVaccine = new Vaccine
            {
                Id = Guid.NewGuid(),
                Manufacturer = manufacturer,
                DateTaken = dateTaken,
                Certificate = Guid.NewGuid(),
                PatientId = patientId,
                PatientFor = _patientRepository.Get(patientId),
                VaccinationCenter = centerId,
                Center = _vaccinationCenterRepository.Get(centerId),
            };

            _vaccineRepository.Insert(newVaccine);
        }
    }
}
