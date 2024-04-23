using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Service.Implementation
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<Patient> _patientRepository;

        public PatientService(IRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public void CreatePatient(Patient patient)
        {
            _patientRepository.Insert(patient);
        }

        public void DeletePatient(Guid? id)
        {
            Patient patient = _patientRepository.Get(id); 
            _patientRepository.Delete(patient);
        }

        public Patient GetPatient(Guid? id)
        {
            return _patientRepository.Get(id);
        }

        public List<Patient> GetPatients()
        {
            return _patientRepository.GetAll().ToList();
        }

        public void UpdatePatient(Patient patient)
        {
            _patientRepository.Update(patient);
        }
    }
}
