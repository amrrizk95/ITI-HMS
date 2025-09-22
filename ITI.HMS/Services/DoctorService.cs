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

        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor GetDoctorById(int id)
        {
            if (id <= 0)
                return null;

            return _doctorRepository.GetById(id);
        }

        public Doctor CreateDoctor(CreatDoctorRequest request)
        {
            if (request == null)
                return null;

            var newDoctor = new Doctor
            {
                Name = request.Name,
                Specialty = request.Specialty,
                Email = request.Email,
                Phone = request.Phone
            };

            _doctorRepository.Add(newDoctor);
            return newDoctor;
        }
    }
}
