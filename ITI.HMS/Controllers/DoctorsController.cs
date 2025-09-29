using ITI.HMS.Models;
using ITI.HMS.Requestes;
using ITI.HMS.Services;
using ITI.HMS.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.HMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Require authentication for all endpoints
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        //host(domain)/api/Doctors

        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")] // Only Admin and Doctor can view all doctors
        public List<Doctor> Get()
        {
            return _doctorService.Get();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Doctor,Patient")] // All authenticated users can view doctor details
        public ActionResult<Doctor> Get(int id)
        {

            /// controller => service (logic) => repository (data)

            // http call only
            var doctor = _doctorService.GetById(id);//
            return Ok(doctor);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")] // Only Admin can create doctors
        public ActionResult Post([FromBody] CreatDoctorRequest doctor)
        {
           _doctorService.CreateDoctor(doctor);
            return Ok();
        }
    }
}
