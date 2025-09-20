using ITI.HMS.Models;
using ITI.HMS.Requestes;

namespace ITI.HMS.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllDoctorsAsync();
        Task<Doctor?> GetDoctorByIdAsync(int id);
        Task<Doctor> CreateDoctorAsync(CreatDoctorRequest request);
        Task<Doctor?> UpdateDoctorAsync(int id, UpdateDoctorRequest request);
        Task<bool> DeleteDoctorAsync(int id);
        Task<bool> DoctorExistsAsync(int id);
    }
}