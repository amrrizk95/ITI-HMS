using ITI.HMS.Models;

namespace ITI.HMS.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly List<Doctor> _doctors;
        private int _nextId;

        public DoctorRepository()
        {
            _doctors = new List<Doctor>
            {
                new Doctor
                {
                    Id = 1,
                    Name = "Dr. John Smith",
                    Specialty = "Cardiology",
                    Email = "john.smith@hospital.com",
                    Phone = "+1234567890"
                },
                new Doctor
                {
                    Id = 2,
                    Name = "Dr. Emily Johnson",
                    Specialty = "Neurology",
                    Email = "emily.johnson@hospital.com",
                    Phone = "+1234567891"
                },
                new Doctor
                {
                    Id = 3,
                    Name = "Dr. Michael Brown",
                    Specialty = "Pediatrics",
                    Email = "michael.brown@hospital.com",
                    Phone = "+1234567892"
                }
            };
            _nextId = _doctors.Count + 1;
        }

        public Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return Task.FromResult(_doctors.AsEnumerable());
        }

        public Task<Doctor?> GetByIdAsync(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            return Task.FromResult(doctor);
        }

        public Task<Doctor> CreateAsync(Doctor doctor)
        {
            doctor.Id = _nextId++;
            _doctors.Add(doctor);
            return Task.FromResult(doctor);
        }

        public Task<Doctor?> UpdateAsync(int id, Doctor doctor)
        {
            var existingDoctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (existingDoctor == null)
                return Task.FromResult<Doctor?>(null);

            existingDoctor.Name = doctor.Name;
            existingDoctor.Specialty = doctor.Specialty;
            existingDoctor.Email = doctor.Email;
            existingDoctor.Phone = doctor.Phone;

            return Task.FromResult<Doctor?>(existingDoctor);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var doctor = _doctors.FirstOrDefault(d => d.Id == id);
            if (doctor == null)
                return Task.FromResult(false);

            _doctors.Remove(doctor);
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(int id)
        {
            var exists = _doctors.Any(d => d.Id == id);
            return Task.FromResult(exists);
        }
    }
}