using ITI.HMS.Models;
using ITI.HMS.Requestes;

namespace ITI.HMS.Services
{
    public interface IDoctorService
    {
        List<Doctor> GetAllDoctors();
        Doctor GetDoctorById(int id);
        Doctor CreateDoctor(CreatDoctorRequest request);
    }
}
