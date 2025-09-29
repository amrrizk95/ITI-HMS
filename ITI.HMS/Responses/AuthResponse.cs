using ITI.HMS.Models;

namespace ITI.HMS.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}