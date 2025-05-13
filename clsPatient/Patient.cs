namespace clsArthritisPatient
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? HcpSpecialty { get; set; }
        public string? Indication { get; set; }
        public int? InsuranceType { get; set; }
        public bool ConsentToEmail { get; set; }
        public bool ConsentToText { get; set; }
        public string? PinHash { get; set; }  // Store hashed PIN
        public string? CopayCardNumber { get; set; }

    }
}
