using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVaccineService
    {
        public void ScheduleVaccine(string manufacturer, DateTime dateTaken, Guid patientId, Guid centerId);
        public List<Vaccine> GetVaccinesForPatient(Guid? id);
        public List<Vaccine> GetVaccinesForVaccinationCenter(Guid? id);
    }
}
