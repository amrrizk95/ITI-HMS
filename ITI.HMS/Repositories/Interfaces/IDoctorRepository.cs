using ITI.HMS.Models;
using ITI.HMS.Requestes;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor GetById(int id);
        List<Doctor> Get();
        void Add(CreatDoctorRequest doctor);
        Task AddAsync(Doctor doctor); // Add this for direct Doctor entity
        Task<Doctor> GetByUserIdAsync(int userId); // Add this to get doctor by user ID
    }
}
