using ITI.HMS.Models;
using ITI.HMS.Models.Entities;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;

namespace ITI.HMS.Repositories
{
    public class DoctorRepository: IDoctorRepository
    {
        private readonly HMSDbContext _dbContext;
        public DoctorRepository(HMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(CreatDoctorRequest doctor)
        {
            var newDoctor = new Doctor
            {
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Phone = doctor.Phone
            };
            _dbContext.Doctors.Add(newDoctor);
            _dbContext.SaveChanges();
        }

        public List<Doctor> Get()
        {
            return _dbContext.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            var doctor = _dbContext.Doctors.FirstOrDefault(d=>d.Id==id);
            return doctor;
        }
    }
}
