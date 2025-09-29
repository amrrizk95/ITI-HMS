using ITI.HMS.Models;
using ITI.HMS.Models.Entities;
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

        public async Task<int> AddAsync(Patient patient)
        {
            await _dbContext.Patients.AddAsync(patient);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            int affectedRows = 0;
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                affectedRows = await _dbContext.SaveChangesAsync();
            }
            return affectedRows;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _dbContext.Patients.ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> UpdateAsync(Patient patient)
        {
            _dbContext.Patients.Update(patient);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
