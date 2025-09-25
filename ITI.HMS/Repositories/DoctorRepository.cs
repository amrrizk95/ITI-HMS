using ITI.HMS.Interfaces;
using ITI.HMS.Models;

namespace ITI.HMS.Repositories
{
    public class DoctorRepository : IDoctorRepository
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

        public List<Doctor> GetAll()
        {
            return Doctors;
        }

        public Doctor GetById(int id)
        {
            var doctor = Doctors.FirstOrDefault(d => d.Id == id);
            return doctor;
        }

        public void Add(Doctor doctor)
        {
            Doctors.Add(doctor);
        }

        public int MaxId()
        {
            return Doctors.Max(d => d.Id);
        }
    }
}
