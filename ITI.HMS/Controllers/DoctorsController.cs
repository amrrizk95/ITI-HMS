using ITI.HMS.Models.Entities;
using ITI.HMS.Requestes;
using ITI.HMS.Services;
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
        public List<Doctor> Get()
        {
            return _doctorService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {

            /// controller => service (logic) => repository (data)

            // http call only 
            var doctor = _doctorService.GetById(id);// 
            return Ok(doctor);
        }


        [HttpPost]
        public ActionResult Post([FromBody] CreatDoctorRequest doctor)
        {
           _doctorService.CreateDoctor(doctor);
            return Ok();
        }
    }
}
