using ITI.HMS.Models.Entities.Enums;

namespace ITI.HMS.Models.Entities
{
    public class Appointment
    {
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }

        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
