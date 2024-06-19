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
        public Patient Patient { get; set; }
        public List<Patient> Patients { get; set; }

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

            await _patientService.AddAsync(Patient);
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

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _patientService.AddAsync(Patient);
                return RedirectToPage();
            }
            catch
            {
                ModelState.AddModelError("", "Error occurred during registration.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var authenticatedPatient = await _patientService.LoginAsync(Patient.Name, Patient.Password);
            if (authenticatedPatient == null)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
