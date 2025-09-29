using ITI.HMS.Models;
using ITI.HMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITI.HMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all endpoints
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")] // Only Admin and Doctor can view all patients
        public List<Patient> Get()
        {
            return _patientService.Get();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")] // All authenticated users can view patient details
        public ActionResult<Patient> Get(int id)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var patient = _patientService.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            // Patients can only view their own data
            if (currentUserRole == "Patient" && patient.UserId != currentUserId)
            {
                return Forbid();
            }

            return Ok(patient);
        }

        [HttpGet("my-profile")]
        [Authorize(Roles = "Patient")] // Only patients can access this endpoint
        public async Task<ActionResult<Patient>> GetMyProfile()
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");

            var patient = await _patientService.GetByUserIdAsync(currentUserId);

            if (patient == null)
            {
                return NotFound("Patient profile not found");
            }

            return Ok(patient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Patient")] // Admin can update any patient, patients can update themselves
        public async Task<ActionResult> Put(int id, [FromBody] Patient patient)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            var currentUserRole = User.FindFirst(ClaimTypes.Role)?.Value;

            var existingPatient = _patientService.GetById(id);

            if (existingPatient == null)
            {
                return NotFound();
            }

            // Patients can only update their own data
            if (currentUserRole == "Patient" && existingPatient.UserId != currentUserId)
            {
                return Forbid();
            }

            patient.Id = id; // Ensure the ID matches
            await _patientService.UpdatePatientAsync(patient);

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only Admin can deactivate patients
        public async Task<ActionResult> Delete(int id)
        {
            var patient = _patientService.GetById(id);

            if (patient == null)
            {
                return NotFound();
            }

            await _patientService.DeactivatePatientAsync(id);

            return Ok();
        }
    }
}