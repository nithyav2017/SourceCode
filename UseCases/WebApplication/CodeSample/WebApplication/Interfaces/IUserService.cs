using Shared.Entities;
using Shared.Entities.DTO;

namespace WebApplication.Interfaces
{
    public interface IPersonService
    {
        Task<PersonProfileDto> GetProfileDTO(int ind);

        Task<Person> GetProfile(int ind);
        Task SaveProfile(PersonProfileDto userProfile);
    }
}
    