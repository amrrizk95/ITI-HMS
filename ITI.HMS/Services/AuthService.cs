using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Requestes;
using ITI.HMS.Responses;
using ITI.HMS.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITI.HMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IDoctorRepository doctorRepository, IPatientRepository patientRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
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

            // Generate token
            var token = GenerateJwtToken(user);

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
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

            return new AuthResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
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
                expires: DateTime.UtcNow.AddHours(24),
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
    }
}