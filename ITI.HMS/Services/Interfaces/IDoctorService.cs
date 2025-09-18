using ITI.HMS.Models;
using ITI.HMS.Requestes;

namespace ITI.HMS.Services.Interfaces
{
    public interface IDoctorService
    {
        Result<Doctor?> GetById(int id);
        Result<List<Doctor>> GetAll();
        Result AddDoctor(CreatDoctorRequest doctor);
    }
}
