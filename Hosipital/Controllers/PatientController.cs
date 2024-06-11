using Microsoft.AspNetCore.Mvc;
using Services;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? sortBy = null, [FromQuery] string? filterBy = null)
        {
            var patients = await _patientService.GetAllAsync(sortBy, filterBy);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            await _patientService.AddAsync(patient);
            return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            _patientService.Update(patient);

            try
            {
                await _patientService.SaveChangesAsync();
            }
            catch (Exception)
            {
                if (!await _patientService.ExistsAsync(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _patientService.Remove(patient);
            await _patientService.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Patient patient)
        {
            try
            {
                await _patientService.AddAsync(patient);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Patient patient)
        {
            var authenticatedPatient = await _patientService.LoginAsync(patient.Name, patient.Password);
            if (authenticatedPatient == null)
            {
                return Unauthorized();
            }


            return Ok(authenticatedPatient);
        }
    }
}
