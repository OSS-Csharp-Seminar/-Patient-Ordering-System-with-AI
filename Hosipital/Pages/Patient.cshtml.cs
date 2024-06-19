using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hosipital.Pages
{
    public class PatientsModel : PageModel
    {
        private readonly IPatientService _patientService;

        public PatientsModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [BindProperty]
        public Patient Patient { get; set; } = new Patient(); // Ensure Patient is initialized

        public List<Patient> Patients { get; set; } = new List<Patient>(); // Ensure Patients is initialized

        public async Task OnGetAsync()
        {
            Patients = (await _patientService.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Patient.Id == 0)
            {
                await _patientService.AddAsync(Patient);
            }
            else
            {
                _patientService.Update(Patient);
            }

            await _patientService.SaveChangesAsync();
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != Patient.Id)
            {
                return BadRequest("Patient ID mismatch.");
            }

            _patientService.Update(Patient);
            await _patientService.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _patientService.Remove(patient);
            await _patientService.SaveChangesAsync();

            return RedirectToPage();
        }


    }
}
