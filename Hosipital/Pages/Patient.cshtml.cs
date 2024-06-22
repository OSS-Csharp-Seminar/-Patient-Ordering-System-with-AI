using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Pages
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

        [BindProperty]
        public Patient Patient { get; set; }

        public List<Patient> Patients { get; set; } = new List<Patient>();

        public async Task OnGetAsync()
        {
            Patients = (await _patientService.GetAllAsync(SortBy, FilterBy)).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _patientService.AddAsync(Patient);
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

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var patientToUpdate = await _patientService.GetByIdAsync(id);
            if (patientToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(patientToUpdate, "Patient", p => p.Name, p => p.Surname, p => p.Contact, p => p.Password))
            {
                await _patientService.UpdateAsync(patientToUpdate);
                await _patientService.SaveChangesAsync();
                return RedirectToPage();
            }

            return Page();
        }
    }
}
