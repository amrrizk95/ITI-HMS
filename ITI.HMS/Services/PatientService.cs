using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using ITI.HMS.Services.Interfaces;

namespace ITI.HMS.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public List<Patient> Get()
        {
            return _patientRepository.Get().Where(p => p.IsActive).ToList();
        }

        public async Task<Patient> GetByUserIdAsync(int userId)
        {
            return await _patientRepository.GetByUserIdAsync(userId);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            // Add any business logic here
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeactivatePatientAsync(int id)
        {
            // Soft delete - set IsActive to false
            await _patientRepository.DeleteAsync(id);
        }
    }
}