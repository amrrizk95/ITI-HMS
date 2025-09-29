using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITI.HMS.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HMSDbContext _dbContext;

        public PatientRepository(HMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Patient> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Patients
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public List<Patient> Get()
        {
            return _dbContext.Patients.Include(p => p.User).ToList();
        }

        public Patient GetById(int id)
        {
            var patient = _dbContext.Patients.Include(p => p.User).FirstOrDefault(p => p.Id == id);
            return patient;
        }

        public async Task UpdateAsync(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.IsActive = false; // Soft delete
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}