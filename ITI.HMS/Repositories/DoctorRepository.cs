using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;

namespace ITI.HMS.Repositories
{
    public class DoctorRepository: IDoctorRepository
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

        public void Add(CreatDoctorRequest doctor)
        {

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
        }

        public List<Doctor> Get()
        {
            return Doctors;
        }

        public Doctor GetById(int id)
        {
            var doctor = Doctors.Where(d => d.Id == id).FirstOrDefault();
            return doctor;
        }
    }
}
