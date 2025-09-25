using ITI.HMS.Common;
using ITI.HMS.Interfaces;
using ITI.HMS.Models;
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

        public ServiceResponse<Doctor> GetDoctorById(int id)
        {
            var doctor = _doctorRepository.GetById(id);

            if (doctor == null)
            {
                return new ServiceResponse<Doctor> { Message = $"Doctor with id {id} not found." };
            }

            return new ServiceResponse<Doctor> 
            { 
                Data = doctor,
                Message = "Doctor found successfully." ,
                Succeeded = true
            };
        }

        public ServiceResponse<Doctor> AddDoctor(CreatDoctorRequest doctor)
        {
            var nextId = GetNextId();
            var newDoctor = new Doctor
            {
                Id = nextId,
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Phone = doctor.Phone
            };

            _doctorRepository.Add(newDoctor);

            return new ServiceResponse<Doctor>
            {
                Data = newDoctor,
                Message = "Doctor added successfully.",
                Succeeded = true
            };
        }

        private int GetNextId()
        {
            var maxId = _doctorRepository.MaxId();
            var newId = maxId + 1;
            return newId;
        }
    }
}
