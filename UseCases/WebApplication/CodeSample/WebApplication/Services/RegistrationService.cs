using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Shared.Entities;
using Shared.Entities.DTO;
using WebApplication.Data;
using WebApplications.Interfaces;

namespace WebApplications.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly AdventureworksContext _context;

        public RegistrationService(AdventureworksContext context)
        {
            _context = context;
        }
        public Task CreateAsync(IdentityUser identityUser, RegistrationDTO model)
        {
           var businessEntity = _context.BusinessEntity.Add(new BusinessEntity
           {
               rowguid = Guid.NewGuid()
              
           });
            _context.SaveChanges();

            var user = _context.Persons.Add(new Shared.Entities.Person
           {
               BusinessEntityID = businessEntity.Entity.BusinessEntityID,
               FirstName = model.FirstName,
               LastName = model.LastName,
               EmailAddresses = new List<EmailAddress>
              {
                  new EmailAddress
                  {
                      BusinessEntityID = businessEntity.Entity.BusinessEntityID,
                      Email = model.EmailAddress!,                      
                      rowguid = Guid.NewGuid()
                  }
              },
                Password = new Password { 
                    BusinessEntityID= businessEntity.Entity.BusinessEntityID, 
                    PasswordHash = identityUser.PasswordHash!,
                    PasswordSalt = model.PasswordSalt
                }
           });

                return _context.SaveChangesAsync();
        }
    }
}
