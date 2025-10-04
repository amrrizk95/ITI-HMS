namespace ITI.HMS.BlazorWASM.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? LicenseNumber { get; set; }
        public string? Schedule { get; set; }
        public string? Qualifications { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; } = new();
    }
}