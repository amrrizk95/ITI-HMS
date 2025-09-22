using ITI.HMS.Models;

namespace ITI.HMS.Repositories
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor GetById(int id);
        void Add(Doctor doctor);
    }
}
