using ITI.HMS.Models;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        void Add(Doctor doctor);
        Doctor? Get(int id);
        List<Doctor> GetAll();
    }
}
