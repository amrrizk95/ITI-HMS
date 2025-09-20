using ITI.HMS.Models;
using ITI.HMS.Requestes;
using ITI.HMS.Services;
using Microsoft.AspNetCore.Mvc;

namespace ITI.HMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// Get all doctors
        /// </summary>
        /// <returns>List of all doctors</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetAllDoctors()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return Ok(doctors);
        }

        /// <summary>
        /// Get a doctor by ID
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Doctor details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid doctor ID");

            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
                return NotFound($"Doctor with id {id} not found");

            return Ok(doctor);
        }

        /// <summary>
        /// Create a new doctor
        /// </summary>
        /// <param name="request">Doctor creation request</param>
        /// <returns>Created doctor</returns>
        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor([FromBody] CreatDoctorRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest("Doctor data is required");

            try
            {
                var doctor = await _doctorService.CreateDoctorAsync(request);
                return CreatedAtAction(nameof(GetDoctor), new { id = doctor.Id }, doctor);
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Invalid doctor data");
            }
        }

        /// <summary>
        /// Update an existing doctor
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <param name="request">Doctor update request</param>
        /// <returns>Updated doctor</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Doctor>> UpdateDoctor(int id, [FromBody] UpdateDoctorRequest request)
        {
            if (id <= 0)
                return BadRequest("Invalid doctor ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null)
                return BadRequest("Doctor data is required");

            var updatedDoctor = await _doctorService.UpdateDoctorAsync(id, request);
            if (updatedDoctor == null)
                return NotFound($"Doctor with id {id} not found");

            return Ok(updatedDoctor);
        }

        /// <summary>
        /// Delete a doctor
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Success status</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid doctor ID");

            var deleted = await _doctorService.DeleteDoctorAsync(id);
            if (!deleted)
                return NotFound($"Doctor with id {id} not found");

            return NoContent();
        }

        /// <summary>
        /// Check if a doctor exists
        /// </summary>
        /// <param name="id">Doctor ID</param>
        /// <returns>Boolean indicating if doctor exists</returns>
        [HttpHead("{id}")]
        public async Task<ActionResult> DoctorExists(int id)
        {
            if (id <= 0)
                return BadRequest();

            var exists = await _doctorService.DoctorExistsAsync(id);
            if (!exists)
                return NotFound();

            return Ok();
        }
    }
}
