namespace ITI.HMS.Models.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IEnumerable<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
