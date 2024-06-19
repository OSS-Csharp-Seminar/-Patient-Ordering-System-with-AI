using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hosipital.Pages
{
    public class SpecializationsModel : PageModel
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationsModel(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [BindProperty]
        public Specialization Specialization { get; set; }
        public List<Specialization> Specializations { get; set; }

        public async Task OnGetAsync()
        {
            Specializations = (await _specializationService.GetAllAsync()).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _specializationService.AddAsync(Specialization);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationService.Remove(specialization);
            return RedirectToPage();
        }


    }
}
