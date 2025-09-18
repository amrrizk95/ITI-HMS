using ITI.HMS.Models;
using ITI.HMS.Requestes;
using ITI.HMS.Services.Interfaces;

namespace ITI.HMS.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly static List<Doctor> Doctors = new List<Doctor>
        {
            new Doctor
            {
                Id=1,
                Name="Dr. John Smith",
                Specialty="Cardiology",

            },
            new Doctor
            {
                Id=2,
                Name="Dr. Emily Johnson",
                Specialty="Neurology",

            },
            new Doctor
            {
                Id=3,
                Name="Dr. Michael Brown",
                Specialty="Pediatrics",

            },

        };
        public Result AddDoctor(CreatDoctorRequest doctorRequest)
        {
            // validations
            if (doctorRequest == null)
                throw new ArgumentNullException($"{nameof(doctorRequest)} can't be null");

            if (string.IsNullOrEmpty(doctorRequest.Name))
                return new Result(false, $"Doctor {nameof(doctorRequest.Name)} is required");

            if(string.IsNullOrEmpty(doctorRequest.Email))
                return new Result(false, $"Doctor {nameof(doctorRequest.Email)} is required");

            Doctor doctor = doctorRequest.ToDoctor();
            doctor.Id = Doctors.Max(d => d.Id) + 1;

            try
            {
                Doctors.Add(doctor);
                return new Result<Doctor>(true,doctor);
            }
            catch(Exception ex)
            {
                return new Result(false, error:ex.Message);
            }
        }

        public Result<List<Doctor>> GetAll()
        {
            List<Doctor> doctors = Doctors;
            return new Result<List<Doctor>>(true, doctors);
        }

        public Result<Doctor?> GetById(int id)
        {
            Doctor? doctor = Doctors.FirstOrDefault(x => x.Id == id);
            return new Result<Doctor?>(true, doctor);
        }
    }
}
