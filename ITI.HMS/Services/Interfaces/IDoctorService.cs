using ITI.HMS.Models;
using ITI.HMS.Requestes;
using Microsoft.AspNetCore.Mvc;

namespace ITI.HMS.Services.Interfaces
{
    public interface IDoctorService
    {
        Doctor GetById(int id);
        List<Doctor> Get();
       void CreateDoctor(CreatDoctorRequest doctor);
    }
}
