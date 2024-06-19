using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Pages
{
    public class DoctorsModel : PageModel
    {
        private readonly IDoctorService _doctorService;

        public DoctorsModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [BindProperty]
        public Doctor Doctor { get; set; }
        public List<Doctor> Doctors { get; set; }

        public async Task OnGetAsync()
        {
            Doctors = (await _doctorService.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _doctorService.AddAsync(Doctor);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id != Doctor.Id)
            {
                return BadRequest("Doctor ID mismatch.");
            }

            _doctorService.Update(Doctor);
            await _doctorService.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _doctorService.Remove(doctor);
            await _doctorService.SaveChangesAsync();

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
                await _doctorService.RegisterAsync(Doctor);
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

            var authenticatedDoctor = await _doctorService.LoginAsync(Doctor.Name, Doctor.Password);
            if (authenticatedDoctor == null)
            {
                ModelState.AddModelError("", "Invalid login credentials.");
                return Page();
            }

            return RedirectToPage();
        }
    }
}
