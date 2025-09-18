using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;

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

        public void Add(Doctor doctor)
        {
            doctor.Id = Doctors.Max(d => d.Id) + 1;
            Doctors.Add(doctor);
        }

        public Doctor? Get(int id)
        {
            return Doctors.FirstOrDefault(d => d.Id == id);
        }

        public List<Doctor> GetAll()
        {
            return Doctors;
        }
    }
}
