using ITI.HMS.Common;
using ITI.HMS.Models;
using ITI.HMS.Requestes;

namespace ITI.HMS.Interfaces
{
    public interface IDoctorService
    {
        List<Doctor> GetAllDoctors();
        ServiceResponse<Doctor> GetDoctorById(int id);
        ServiceResponse<Doctor> AddDoctor(CreatDoctorRequest doctor);
    }
}
