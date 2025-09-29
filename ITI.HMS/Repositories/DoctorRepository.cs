using ITI.HMS.Models;
using ITI.HMS.Models.Entities;
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

        public async Task<int> AddAsync(Doctor doctor)
        {
            await _dbContext.Doctors.AddAsync(doctor);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);

            int affectedRows = 0;
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                affectedRows = await _dbContext.SaveChangesAsync();
            }
            return affectedRows;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _dbContext.Doctors.ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == id);
            return doctor;
        }

        public async Task<Doctor?> GetDoctorWithAppointmentsAsync(int doctorId)
        {
            var doctor = await _dbContext.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
            if(doctor == null)
            {
                return null;
            }

            await _dbContext.Entry(doctor)
                .Collection(d => d.Appointments)
                .LoadAsync();

            return doctor;
        }

        public async Task<int> UpdateAsync(Doctor doctor)
        {
            _dbContext.Doctors.Update(doctor);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
