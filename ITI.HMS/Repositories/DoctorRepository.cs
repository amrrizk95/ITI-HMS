using ITI.HMS.Models;

namespace ITI.HMS.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private static List<Doctor> _doctors = new List<Doctor>
{
    new Doctor { Id = 1, Name = "Dr. John Smith", Specialty = "Cardiology" },
    new Doctor { Id = 2, Name = "Dr. Emily Johnson", Specialty = "Neurology" },
    new Doctor { Id = 3, Name = "Dr. Michael Brown", Specialty = "Pediatrics" }
};

        public List<Doctor> GetAll() => _doctors;

        public Doctor GetById(int id) => _doctors.FirstOrDefault(d => d.Id == id);

        public void Add(Doctor doctor)
        {
            var maxId = _doctors.Max(d => d.Id);
            doctor.Id = maxId + 1;
            _doctors.Add(doctor);
        }
    }
}
