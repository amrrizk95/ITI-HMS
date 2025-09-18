using ITI.HMS.Models;

namespace ITI.HMS.Requestes
{
    public class CreatDoctorRequest
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Doctor ToDoctor()
        {
            return new Doctor
            {
                Name = Name,
                Specialty = Specialty,
                Email = Email,
                Phone = Phone
            };
        }
    }
}
