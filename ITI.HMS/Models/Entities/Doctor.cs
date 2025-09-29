namespace ITI.HMS.Models.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IEnumerable<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
    }
}
