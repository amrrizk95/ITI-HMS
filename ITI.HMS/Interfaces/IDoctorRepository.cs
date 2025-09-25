using ITI.HMS.Models;

namespace ITI.HMS.Interfaces
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAll();
        Doctor GetById(int id);
        void Add(Doctor doctor);
        int MaxId();
    }
}
