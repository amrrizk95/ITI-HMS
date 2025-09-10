using ITI.HMS.Models;
using ITI.HMS.Requestes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITI.HMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly static List<Doctor> Doctors = new List<Doctor>
        {
            new Doctor
            {
                Id=1,
                Name="Dr. John Smith",
                Specialty="Cardiology",

            },
            new Doctor
            {
                Id=2,
                Name="Dr. Emily Johnson",
                Specialty="Neurology",

            },
            new Doctor
            {
                Id=3,
                Name="Dr. Michael Brown",
                Specialty="Pediatrics",

            },

        };

        //host(domain)/api/Doctors

        [HttpGet]
        public List<Doctor> Get()
        {
            return Doctors;
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> Get(int id)
        {
            if (id <=0)
                return BadRequest();

            var doctor = Doctors.Where(d => d.Id == id).FirstOrDefault();
            if (doctor == null)
                return NotFound($"Doctor with id {id} not found");
            return Ok(doctor);
        }


        [HttpPost]
        public ActionResult Post([FromBody] CreatDoctorRequest doctor)
        {
            if (doctor == null)
                return BadRequest();

            // todo validation and bad request 

            var maxId = Doctors.Max(d => d.Id);
            var newId = maxId + 1;
            var newDoctor = new Doctor
            {
                Id = newId,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Phone = doctor.Phone
            };
            Doctors.Add(newDoctor);
            return Ok(newDoctor);
        }
    }
}
