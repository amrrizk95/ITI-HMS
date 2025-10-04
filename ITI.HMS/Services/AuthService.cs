using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;
using ITI.HMS.Responses;
using ITI.HMS.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ITI.HMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IUserRepository userRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // Check if user already exists
            if (await _userRepository.GetByUsernameAsync(request.Username) != null)
                throw new ArgumentException("Username already exists");

            if (await _userRepository.GetByEmailAsync(request.Email) != null)
                throw new ArgumentException("Email already exists");

            // Validate role-specific fields
            if (request.Role == UserRole.Doctor)
            {
                if (string.IsNullOrEmpty(request.DoctorName))
                    throw new ArgumentException("Doctor name is required for doctor registration");
                if (string.IsNullOrEmpty(request.Specialty))
                    throw new ArgumentException("Specialty is required for doctor registration");
                if (string.IsNullOrEmpty(request.DoctorEmail))
                    throw new ArgumentException("Doctor email is required for doctor registration");
                if (string.IsNullOrEmpty(request.Phone))
                    throw new ArgumentException("Phone is required for doctor registration");
            }
            else if (request.Role == UserRole.Patient)
            {
                if (string.IsNullOrEmpty(request.PatientName))
                    throw new ArgumentException("Patient name is required for patient registration");
                if (string.IsNullOrEmpty(request.PatientEmail))
                    throw new ArgumentException("Patient email is required for patient registration");
                if (string.IsNullOrEmpty(request.PatientPhone))
                    throw new ArgumentException("Phone is required for patient registration");
                if (!request.DateOfBirth.HasValue)
                    throw new ArgumentException("Date of birth is required for patient registration");
                if (string.IsNullOrEmpty(request.Gender))
                    throw new ArgumentException("Gender is required for patient registration");
                if (string.IsNullOrEmpty(request.Address))
                    throw new ArgumentException("Address is required for patient registration");
            }

            // Create new user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role
            };

            await _userRepository.AddAsync(user);

            // Create role-specific records
            if (request.Role == UserRole.Doctor)
            {
                var doctor = new Doctor
                {
                    UserId = user.Id,
                    Name = request.DoctorName,
                    Specialty = request.Specialty,
                    Email = request.DoctorEmail,
                    Phone = request.Phone,
                    LicenseNumber = request.LicenseNumber,
                    Schedule = request.Schedule,
                    Qualifications = request.Qualifications
                };

                await _doctorRepository.AddAsync(doctor);
            }
            else if (request.Role == UserRole.Patient)
            {
                var patient = new Patient
                {
                    UserId = user.Id,
                    Name = request.PatientName,
                    Email = request.PatientEmail,
                    Phone = request.PatientPhone,
                    DateOfBirth = request.DateOfBirth.Value,
                    Gender = request.Gender,
                    Address = request.Address,
                    InsuranceNumber = request.InsuranceNumber,
                    EmergencyContact = request.EmergencyContact,
                    EmergencyContactPhone = request.EmergencyContactPhone,
                    BloodType = request.BloodType,
                    Allergies = request.Allergies,
                    MedicalHistory = request.MedicalHistory
                };

                await _patientRepository.AddAsync(patient);
            }

            // Generate tokens
            var token = GenerateJwtToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null || !VerifyPassword(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            if (!user.IsActive)
                throw new UnauthorizedAccessException("Account is deactivated");

            var token = GenerateJwtToken(user);
            var refreshToken = await GenerateRefreshTokenAsync(user.Id);

            return new AuthResponse
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = refreshToken.ExpiresAt
            };
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15), // Short-lived access token
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (storedToken == null)
                throw new UnauthorizedAccessException("Invalid refresh token");

            if (!storedToken.IsActive)
                throw new UnauthorizedAccessException("Refresh token is expired or revoked");

            // Revoke old refresh token
            await _refreshTokenRepository.RevokeAsync(storedToken);

            // Generate new tokens
            var user = storedToken.User;
            var newAccessToken = GenerateJwtToken(user);
            var newRefreshToken = await GenerateRefreshTokenAsync(user.Id);

            // Store token replacement
            storedToken.ReplacedByToken = newRefreshToken.Token;
            await _refreshTokenRepository.RevokeAsync(storedToken);

            return new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                RefreshTokenExpiresAt = newRefreshToken.ExpiresAt
            };
        }

        public async Task RevokeTokenAsync(string refreshToken)
        {
            var storedToken = await _refreshTokenRepository.GetByTokenAsync(refreshToken);

            if (storedToken == null)
                throw new ArgumentException("Invalid refresh token");

            if (!storedToken.IsActive)
                throw new ArgumentException("Token is already revoked or expired");

            await _refreshTokenRepository.RevokeAsync(storedToken);
        }

        private async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateSecureRandomToken(),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7), // Long-lived refresh token
                CreatedAt = DateTime.UtcNow
            };

            return await _refreshTokenRepository.CreateAsync(refreshToken);
        }

        private string GenerateSecureRandomToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}