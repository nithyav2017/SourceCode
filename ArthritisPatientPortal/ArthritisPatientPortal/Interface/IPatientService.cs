using ArthritisPatientPortal.Models;

namespace ArthritisPatientPortal.Interface
{
    public interface IPatientService
    {
        Task<Patient> RegisterPatientAsync(Patient patient);
        Task<Patient> GetPatientAsync(string pin,string Email);
        Task<CopayCard> GenerateCopayCardAsync(int patientId);
        Task<CopayCard> GetCopayCardAsync(int patientId);
        Task<Patient> GetPatientByPinAsync(string pin);
    }
}
