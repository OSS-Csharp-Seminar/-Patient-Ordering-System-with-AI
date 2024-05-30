using Microsoft.AspNetCore.Mvc;
using N_Tier.Core.Entities;
using N_Tier.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace N_Tier.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialization>>> GetAllSpecializations()
        {
            var specializations = await _specializationService.GetAllAsync();
            return Ok(specializations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Specialization>> GetSpecializationById(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return Ok(specialization);
        }

        [HttpPost]
        public async Task<ActionResult<Specialization>> CreateSpecialization(Specialization specialization)
        {
            await _specializationService.AddAsync(specialization);
            return CreatedAtAction(nameof(GetSpecializationById), new { id = specialization.Id }, specialization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSpecialization(int id, Specialization specialization)
        {
            if (id != specialization.Id)
            {
                return BadRequest();
            }

            _specializationService.Update(specialization);

            try
            {
                await _specializationService.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!await _specializationService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _specializationService.GetByIdAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }

            _specializationService.Remove(specialization);
            await _specializationService.SaveChangesAsync();

            return NoContent();
        }
    }
}
