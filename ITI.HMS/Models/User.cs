using System.Text.Json.Serialization;

namespace ITI.HMS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; } = new();
    }

    public enum UserRole
    {
        Patient = 1,
        Doctor = 2,
        Admin = 3
    }
}