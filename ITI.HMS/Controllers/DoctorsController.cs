using ITI.HMS.Models;
using ITI.HMS.Requestes;
using ITI.HMS.Services;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ActionResult<List<Doctor>> Get()
        {
            var doctors = _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {
            var doctor = _doctorService.GetDoctorById(id);
            if (doctor == null)
                return NotFound($"Doctor with id {id} not found");

            return Ok(doctor);
        }

        [HttpPost]
        public ActionResult<Doctor> Post([FromBody] CreatDoctorRequest doctor)
        {
            var newDoctor = _doctorService.CreateDoctor(doctor);
            if (newDoctor == null)
                return BadRequest("Invalid doctor data");

            return Ok(newDoctor);
        }
    }
}
