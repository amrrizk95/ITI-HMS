using ITI.HMS.Models.Entities;
using ITI.HMS.Requestes;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(int id);
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<int> AddAsync(Doctor doctor);
        Task<int> UpdateAsync(Doctor doctor);
        Task<int> DeleteAsync(int id);
        Task<Doctor?> GetDoctorWithAppointmentsAsync(int doctorId);
    }
}
