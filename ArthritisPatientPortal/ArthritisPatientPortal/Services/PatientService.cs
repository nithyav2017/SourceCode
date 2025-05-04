using ArthritisPatientPortal.Data;
using ArthritisPatientPortal.Interface;
using ArthritisPatientPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ArthritisPatientPortal.Services
{
    public class PatientService : IPatientService
    {
        private readonly ApplicationDbContext _context;
        
        public PatientService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CopayCard> GenerateCopayCardAsync(int patientId)
        {
            var patient = await _context.Patients
              .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
                throw new KeyNotFoundException("Patient not found");

            // Create new copay card
            var copayCard = new CopayCard
            {
                PatientId = patientId,
                CardNumber = GenerateCardNumber(),
                BinNumber = "123456", // Replace with your actual BIN
                PcnNumber = "ADV", // Replace with your actual PCN
                GroupNumber = "RX1234", // Replace with your actual Group
                ExpirationDate = DateTime.Now.AddYears(1),
                InsuranceType = patient.InsuranceType,
                patient = patient
            };

            _context.CopayCards.Add(copayCard);
            await _context.SaveChangesAsync();

            return copayCard;
        }

        public async Task<CopayCard> GetCopayCardAsync(int copayCardId)
        {
            var copayCard = await _context.CopayCards
              .FirstOrDefaultAsync(p => p.id == copayCardId);

            if (copayCard == null)
                throw new KeyNotFoundException("copayCard not found");

            // Create new copay card
            var newCopayCard = new CopayCard
            {
                PatientId = copayCard.PatientId,
                CardNumber = GenerateCardNumber(),
                BinNumber = copayCard.BinNumber, // Replace with your actual BIN
                PcnNumber = copayCard.PcnNumber, // Replace with your actual PCN
                GroupNumber = copayCard.GroupNumber, // Replace with your actual Group
                ExpirationDate = DateTime.Now.AddYears(1),
                InsuranceType = copayCard.InsuranceType
                
            };

            return newCopayCard;
        }

        public async Task<Patient> GetPatientAsync(string  pin,string Email)
        {
             var patient = await _context.Patients
            .FirstOrDefaultAsync(p => p.Pin == pin &&   p.Email == Email);

            return patient ?? throw new KeyNotFoundException("Patient not found");
        }

        public async Task<Patient> GetPatientByPinAsync(string pin)
        {
            var patient = await _context.Patients
           .FirstOrDefaultAsync(p => p.Pin == pin);

            return patient ?? throw new KeyNotFoundException("Patient not found");
        }

        public async Task<Patient> RegisterPatientAsync(Patient patient)
        {
            // Validate patient data
            if (string.IsNullOrEmpty(patient.Pin))
                throw new ArgumentException("PIN is required");

            // Check if patient already exists
            var existingPatient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Pin == patient.Pin);

            if (existingPatient != null)
                throw new InvalidOperationException("Patient with this PIN already exists");

            // Add patient to database
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private string GenerateCardNumber()
        {
            // Generate a unique 16-digit card number
            return $"{DateTime.Now.Ticks % 1000000000000:D12}{new Random().Next(1000, 9999)}";
        }
    }
}
