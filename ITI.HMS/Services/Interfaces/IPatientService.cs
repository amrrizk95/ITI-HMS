using ITI.HMS.Models;

namespace ITI.HMS.Services.Interfaces
{
    public interface IPatientService
    {
        Patient GetById(int id);
        List<Patient> Get();
        Task<Patient> GetByUserIdAsync(int userId);
        Task UpdatePatientAsync(Patient patient);
        Task DeactivatePatientAsync(int id);
    }
}