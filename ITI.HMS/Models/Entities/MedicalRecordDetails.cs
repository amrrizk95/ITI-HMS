using ITI.HMS.Models.Entities.Enums;

namespace ITI.HMS.Models.Entities
{
    public class MedicalRecordDetails
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MedicalRecordType Type { get; set; }

        public int MedicalRecordId { get; set; }

        public MedicalRecord MedicalRecord { get; set; }
    }
}
