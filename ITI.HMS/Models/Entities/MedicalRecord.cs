namespace ITI.HMS.Models.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string SummaryNotes { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int DoctorId { get; set; }
        public int PatientId { get; set; }

        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }

        public IEnumerable<MedicalRecordDetails> Details { get; set; } = new List<MedicalRecordDetails>();
    }
}
