namespace ITI.HMS.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Schedule { get; set; }
        public string? Qualifications { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation property
        public User User { get; set; }
    }
}
