using ITI.HMS.Models;

namespace ITI.HMS.Requestes
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; } = UserRole.Patient;

        // Doctor-specific fields (only required if Role == Doctor)
        public string? DoctorName { get; set; }
        public string? Specialty { get; set; }
        public string? DoctorEmail { get; set; }
        public string? Phone { get; set; }
        public string? LicenseNumber { get; set; }
        public string? Schedule { get; set; }
        public string? Qualifications { get; set; }

        // Patient-specific fields (only required if Role == Patient)
        public string? PatientName { get; set; }
        public string? PatientEmail { get; set; }
        public string? PatientPhone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? InsuranceNumber { get; set; }
        public string? EmergencyContact { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public string? BloodType { get; set; }
        public string? Allergies { get; set; }
        public string? MedicalHistory { get; set; }
    }
}