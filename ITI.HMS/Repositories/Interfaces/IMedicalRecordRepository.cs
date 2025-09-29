using ITI.HMS.Models.Entities;
using ITI.HMS.Models.Entities.Enums;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IMedicalRecordRepository
    {
        Task<int> AddAsync(MedicalRecord medicalRecord);
        Task<int> UpdateAsync(MedicalRecord medicalRecord);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<MedicalRecord>> GetByPatientIdAsync(int patientId);
        Task<IEnumerable<MedicalRecord>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<MedicalRecord>> GetByDateRangeAsync(int patientId, DateTime start, DateTime end);
        Task<IEnumerable<MedicalRecord>> GetByTypeAsync(int patientId, MedicalRecordType type);
    }
}
