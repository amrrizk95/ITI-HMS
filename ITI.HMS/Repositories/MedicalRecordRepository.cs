using ITI.HMS.Models;
using ITI.HMS.Models.Entities;
using ITI.HMS.Models.Entities.Enums;
using ITI.HMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITI.HMS.Repositories
{
    public class MedicalRecordRepository : IMedicalRecordRepository
    {
        private readonly HMSDbContext _dbContext;

        public MedicalRecordRepository(HMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(MedicalRecord medicalRecord)
        {
            await _dbContext.AddAsync(medicalRecord); 
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(MedicalRecord medicalRecord)
        {
            _dbContext.Update(medicalRecord);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.Id == id);
            int affectedRows = 0;
            if(doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                affectedRows = await _dbContext.SaveChangesAsync();
            }
            return affectedRows;
        }

        public async Task<IEnumerable<MedicalRecord>> GetByDateRangeAsync(int patientId, DateTime start, DateTime end)
        {
            var medicalRecords = await _dbContext.MedicalRecords
                .Include(x => x.Details)
                .Where(x => x.CreatedAt >= start && x.CreatedAt <= end && x.PatientId == patientId)
                .ToListAsync();
            return medicalRecords;
        }

        public async Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(int doctorId)
        {
            var medicalRecords = await _dbContext.MedicalRecords
                .Include(x => x.Details)
                .Where(x => x.DoctorId == doctorId)
                .ToListAsync();
            return medicalRecords;
        }

        public async Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId)
        {
            var medicalRecords = await _dbContext.MedicalRecords
                .Include(x => x.Details)
                .Where(x => x.PatientId == patientId)
                .ToListAsync();
            return medicalRecords;
        }

        public async Task<IEnumerable<MedicalRecord>> GetByTypeAsync(int patientId, MedicalRecordType type)
        {
            var medicalRecords = await _dbContext.MedicalRecords
                .AsNoTracking()
                .Include(x => x.Details.Where(d => d.Type == type))
                .Where(x => x.PatientId == patientId)
                .ToListAsync();
            return medicalRecords;
        }
    }
}
