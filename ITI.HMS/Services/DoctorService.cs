using ITI.HMS.Models;
using ITI.HMS.Repositories;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;
using ITI.HMS.Services.Interfaces;

namespace ITI.HMS.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public void CreateDoctor(CreatDoctorRequest doctor)
        {
            // some logic before adding docotor to db

            ///logic 
            ///add to database 
            _doctorRepository.Add(doctor);
        }

        public List<Doctor> Get()
        {
            return _doctorRepository.Get();
        }

        public Doctor GetById(int id)
        {
            // repository service
            var doctor = _doctorRepository.GetById(id);
            return doctor;
        }
    }
}
