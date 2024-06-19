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
            Specialization = new Specialization();
            Specializations = new List<Specialization>();
        }

        [BindProperty]
        public Specialization Specialization { get; set; }
        public List<Specialization> Specializations { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }

        public async Task OnGetAsync()
        {
            Specializations = (await _specializationService.GetAllAsync(SortBy, FilterBy)).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (Specialization == null)
            {
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _specializationService.AddAsync(Specialization);
            return RedirectToPage(new { sortBy = SortBy, filterBy = FilterBy });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationService.Remove(specialization);
            return RedirectToPage(new { sortBy = SortBy, filterBy = FilterBy });
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var specializationToUpdate = await _specializationService.GetByIdAsync(Specialization.Id);
            if (specializationToUpdate == null)
            {
                return NotFound();
            }

            specializationToUpdate.Name = Specialization.Name;
            await _specializationService.SaveChangesAsync();

            return RedirectToPage(new { sortBy = SortBy, filterBy = FilterBy });
        }
    }
}
