using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication.Data;
using Shared.Entities;
using Shared.Entities.DTO;
using WebApplication.Interfaces;
 
namespace WebApplication.Services
{
    public class PersonService : IPersonService
    {
        private readonly AdventureworksContext _context;

        public PersonService(AdventureworksContext context)
        {
            _context = context;
        }   
        public async Task<PersonProfileDto> GetProfileDTO(int ind)
        {
            var result =  await (from person in _context.Persons
                          join email in _context.EmailAddresses
                               on person.BusinessEntityID equals email.BusinessEntityID
                               into emailGroup
                          from email in emailGroup.DefaultIfEmpty()
                          join phone in _context.PersonPhones
                               on person.BusinessEntityID equals phone.BusinessEntityID
                               into phoneGroup
                          from phone in phoneGroup.DefaultIfEmpty()
                          join bea in _context.BusinessEntityAddresses
                               on person.BusinessEntityID equals bea.BusinessEntityID
                               into beaGroup
                          from bea in beaGroup.DefaultIfEmpty()
                          join address in _context.Addresses
                               on bea.AddressID equals address.AddressID
                               into addressGroup
                          from address in addressGroup.DefaultIfEmpty()
                          where person.BusinessEntityID == ind
                          select new PersonProfileDto 
                          {
                             FirstName=  person.FirstName,
                             LastName =  person.LastName,
                             EmailAddress = email != null ? email.Email   : null,
                             PhoneNumber = phone != null ? phone.PhoneNumber : null,
                             AddressLine1 = address != null ? address.AddressLine1 : null
                          }).FirstOrDefaultAsync();
            return result;       
        }
        
        public async Task<Person?> GetProfile(int id)
        {
            return await _context.Persons                 
                .FirstOrDefaultAsync(p => p.BusinessEntityID == id);
        }
        public async Task SaveProfile(PersonProfileDto userProfile)
        {
            try
            {  
             var person = await  _context.Persons.FirstOrDefaultAsync(p => p.BusinessEntityID == userProfile.BusinessEntityID) ;
             
             person.FirstName = userProfile.FirstName;
             person.LastName = userProfile.LastName;

             await _context.SaveChangesAsync();                 
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving profile", ex);
            }
        }
         
    }
}
