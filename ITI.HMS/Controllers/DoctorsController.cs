using ITI.HMS.Interfaces;
using ITI.HMS.Models;
using ITI.HMS.Requestes;
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

        //host(domain)/api/Doctors

        [HttpGet]
        public List<Doctor> Get()
        {
            return _doctorService.GetAllDoctors();
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid ID" });

            var result = _doctorService.GetDoctorById(id);

            if (!result.Succeeded)
                return NotFound(new { message = result.Message });

            return Ok(result.Data);
        }


        [HttpPost]
        public ActionResult Post([FromBody] CreatDoctorRequest doctor)
        {

            var result = _doctorService.AddDoctor(doctor);

            return Ok(result.Data);
        }
    }
}
