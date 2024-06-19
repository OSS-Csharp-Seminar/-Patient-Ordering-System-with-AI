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

        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        public List<Patient> Patients { get; set; } = new List<Patient>();

        public async Task OnGetAsync()
        {
            Patients = (await _patientService.GetAllAsync(SortBy, FilterBy)).ToList();
        }

        [BindProperty]
        public Patient NewPatient { get; set; } = new Patient(); // Corrected from previous

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _patientService.AddAsync(NewPatient); // Updated to use NewPatient
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var patientToUpdate = await _patientService.GetByIdAsync(id);
            if (patientToUpdate == null)
            {
                return NotFound();
            }

            patientToUpdate.Name = NewPatient.Name; // Example of updating fields
            patientToUpdate.Surname = NewPatient.Surname;
            patientToUpdate.Contact = NewPatient.Contact;

            _patientService.Update(patientToUpdate);
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
            return RedirectToPage();
        }
    }
}
