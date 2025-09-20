using ITI.HMS.Models;
using ITI.HMS.Repositories;
using ITI.HMS.Requestes;

namespace ITI.HMS.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorsAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            return await _doctorRepository.GetByIdAsync(id);
        }

        public async Task<Doctor> CreateDoctorAsync(CreatDoctorRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var doctor = new Doctor
            {
                Name = request.Name,
                Specialty = request.Specialty,
                Email = request.Email,
                Phone = request.Phone
            };

            return await _doctorRepository.CreateAsync(doctor);
        }

        public async Task<Doctor?> UpdateDoctorAsync(int id, UpdateDoctorRequest request)
        {
            if (id <= 0 || request == null)
                return null;

            var doctorExists = await _doctorRepository.ExistsAsync(id);
            if (!doctorExists)
                return null;

            var doctor = new Doctor
            {
                Name = request.Name,
                Specialty = request.Specialty,
                Email = request.Email,
                Phone = request.Phone
            };

            return await _doctorRepository.UpdateAsync(id, doctor);
        }

        public async Task<bool> DeleteDoctorAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _doctorRepository.DeleteAsync(id);
        }

        public async Task<bool> DoctorExistsAsync(int id)
        {
            if (id <= 0)
                return false;

            return await _doctorRepository.ExistsAsync(id);
        }
    }
}