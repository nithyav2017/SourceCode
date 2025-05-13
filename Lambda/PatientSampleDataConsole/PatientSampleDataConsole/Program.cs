// See https://aka.ms/new-console-template for more information
using clsArthritisPatient;
using System.Text.Json;

var patients = new List<Patient>();

for(int i= 0; i<=50;i++)
{
    patients.Add(new Patient()
    {

        Id = i,
        FirstName = $"Patient {i}",
        LastName = $"Patient {i + 1}",
        DateOfBirth = DateTime.Now.AddYears(-30).AddDays(i * 5),
        Email = $"patient{i}@example.com",
        Phone = $"123-456-{7800 + i}",
        HcpSpecialty = "General Medicine",
        Indication = "Routine Checkup",
        InsuranceType = 1,
        ConsentToEmail = i % 2 == 0,
        ConsentToText = i % 2 != 0,
        PinHash = $"hash{i}",
        CopayCardNumber = $"COPAY{i * 100}"
    });

    string jsonOutput = JsonSerializer.Serialize(patients, new JsonSerializerOptions { WriteIndented = true });
    Console.WriteLine(jsonOutput);
}