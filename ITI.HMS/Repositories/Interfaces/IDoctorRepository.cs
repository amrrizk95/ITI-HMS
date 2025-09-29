using ITI.HMS.Models.Entities;
using ITI.HMS.Requestes;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Doctor GetById(int id);
        List<Doctor> Get();
        void Add(CreatDoctorRequest doctor);
    }
}
