using Microsoft.AspNetCore.Identity;
using Shared.Entities.DTO;

namespace WebApplications.Interfaces
{
    public interface IRegistrationService
    {
        Task CreateAsync(IdentityUser identityUser, RegistrationDTO model);
    }
}
