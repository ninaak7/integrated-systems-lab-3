using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IVaccineService _vaccineService;
        private readonly IVaccinationCenterService _vaccinationCenterService;

        public VaccinationCentersController(IPatientService patientService, IVaccineService vaccineService, IVaccinationCenterService vaccinationCenterService)
        {
            _patientService = patientService;
            _vaccineService = vaccineService;
            _vaccinationCenterService = vaccinationCenterService;
        }

        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vaccinationCenterService.GetVaccinationCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenter(id);
            vaccinationCenter.Vaccines = _vaccineService.GetVaccinesForVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                _vaccinationCenterService.CreateVaccinationCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vaccinationCenterService.UpdateVaccinationCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vaccinationCenterService.GetVaccinationCenter(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _vaccinationCenterService.DeleteVaccinationCenter(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vaccinationCenterService.GetVaccinationCenter(id) != null;
        }

        public IActionResult NoMoreCapacity()
        {
            return View();
        }

        public IActionResult ScheduleVaccination(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var selectedCenter = _vaccinationCenterService.GetVaccinationCenter(id);
            if (selectedCenter.MaxCapacity <= 0)
            {
                return RedirectToAction(nameof(NoMoreCapacity));
            }

            var patients = _patientService.GetPatients();
            ViewBag.PatientList = patients.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = $"{p.FirstName} {p.LastName}"
            });
            ViewBag.CenterId = selectedCenter.Id;

            Vaccine v = new Vaccine();
            v.VaccinationCenter = selectedCenter.Id;

            return View(v);
        }

        [HttpPost, ActionName("ScheduleVaccination")]
        [ValidateAntiForgeryToken]
        public IActionResult ScheduleVaccination(string manufacturer, DateTime dateTaken, Guid patientId, Guid centerId)
        {
            _vaccineService.ScheduleVaccine(manufacturer, dateTaken, patientId, centerId);
            _vaccinationCenterService.LowerCapacity(centerId);
            return RedirectToAction("Index");
        }
    }
}
