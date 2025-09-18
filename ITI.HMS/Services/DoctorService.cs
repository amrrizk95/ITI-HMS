using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;
using ITI.HMS.Services.Interfaces;

namespace ITI.HMS.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Result AddDoctor(CreatDoctorRequest doctorRequest)
        {
            // validations
            if (doctorRequest == null)
                throw new ArgumentNullException($"{nameof(doctorRequest)} can't be null");

            if (string.IsNullOrEmpty(doctorRequest.Name))
                return new Result(false, $"Doctor {nameof(doctorRequest.Name)} is required");

            if(string.IsNullOrEmpty(doctorRequest.Email))
                return new Result(false, $"Doctor {nameof(doctorRequest.Email)} is required");

            Doctor doctor = doctorRequest.ToDoctor();
            try
            {
                _doctorRepository.Add(doctor);
                return new Result<Doctor>(true,doctor);
            }
            catch(Exception ex)
            {
                return new Result(false, error:ex.Message);
            }
        }

        public Result<List<Doctor>> GetAll()
        {
            List<Doctor> doctors = _doctorRepository.GetAll();
            return new Result<List<Doctor>>(true, doctors);
        }

        public Result<Doctor?> GetById(int id)
        {
            Doctor? doctor = _doctorRepository.Get(id);
            return new Result<Doctor?>(true, doctor);
        }
    }
}
