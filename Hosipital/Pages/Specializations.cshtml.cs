using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Pages
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _specializationService.AddAsync(Specialization);
            await _specializationService.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            await _specializationService.RemoveAsync(specialization);
            await _specializationService.SaveChangesAsync();

            return RedirectToPage(new { sortBy = SortBy, filterBy = FilterBy });
        }

        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            var specializationToUpdate = await _specializationService.GetByIdAsync(id);
            if (specializationToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(specializationToUpdate, "Specialization", s => s.Name))
            {
                await _specializationService.UpdateAsync(specializationToUpdate);
                await _specializationService.SaveChangesAsync();

                return RedirectToPage(new { sortBy = SortBy, filterBy = FilterBy });
            }

            return Page();
        }
    }
}
