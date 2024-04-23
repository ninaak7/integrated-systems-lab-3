using IntegratedSystems.Domain.Domain_Models;

namespace IntegratedSystems.Service.Interface
{
    public interface IPatientService
    {
        public List<Patient> GetPatients();
        public Patient GetPatient(Guid? id);
        public void CreatePatient(Patient patient);
        public void UpdatePatient(Patient patient);
        public void DeletePatient(Guid? id);
    }
}
