using ITI.HMS.Models;
using ITI.HMS.Requestes;
using ITI.HMS.Services.Interfaces;
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

        //host(domain)/api/Doctors
        [HttpGet]
        public ActionResult<List<Doctor>> Get()
        {
            var doctorResults = _doctorService.GetAll();
            return Ok(doctorResults.Value);
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {
            var doctorResult = _doctorService.GetById(id);

            Doctor? doctor = doctorResult.Value;

            if(doctor != null) 
                return Ok(doctor);

            return NotFound($"Doctor with id {id} not found");
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreatDoctorRequest doctor)
        {
            if (doctor == null)
                return BadRequest();
            
            var result = _doctorService.AddDoctor(doctor);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
