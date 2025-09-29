using ITI.HMS.Models;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Patient GetById(int id);
        List<Patient> Get();
        Task AddAsync(Patient patient);
        Task<Patient> GetByUserIdAsync(int userId);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(int id);
    }
}