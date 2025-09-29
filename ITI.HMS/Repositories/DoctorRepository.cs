using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;
using Microsoft.EntityFrameworkCore;

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
                // Note: This method is now deprecated in favor of AddAsync
                Name = doctor.Name,
                Specialty = doctor.Specialty,
                Email = doctor.Email,
                Phone = doctor.Phone
            };
            _dbContext.Doctors.Add(newDoctor);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Doctor> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.UserId == userId);
        }

        public List<Doctor> Get()
        {
            return _dbContext.Doctors.Include(d => d.User).ToList();
        }

        public Doctor GetById(int id)
        {
            var doctor = _dbContext.Doctors.Include(d => d.User).FirstOrDefault(d => d.Id == id);
            return doctor;
        }
    }
}
