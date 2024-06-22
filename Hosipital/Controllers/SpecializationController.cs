using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpecializationController : ControllerBase
    {
        private readonly SpecializationService _specializationService;

        public SpecializationController(SpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Specialization specialization)
        {
            await _specializationService.AddAsync(specialization);
            return Ok(specialization);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization != null)
            {
                return Ok(specialization);
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Specialization specialization)
        {
            if (id != specialization.Id)
            {
                return BadRequest("Specialization ID mismatch.");
            }

            _specializationService.UpdateAsync(specialization);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationService.RemoveAsync(specialization);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? sortBy = null, [FromQuery] string? filterBy = null)
        {
            var specializations = await _specializationService.GetAllAsync(sortBy, filterBy);
            return Ok(specializations);
        }
    }
}
