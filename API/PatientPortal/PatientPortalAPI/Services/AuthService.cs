using PatientPortalAPI.Data;
using PatientPortalAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace PatientPortalAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _configuration;
        public AuthService(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }
        public async Task<string> LoginAsync(string email, string pin)
        {
            var user = await _appDbContext.patients.SingleOrDefaultAsync(x => x.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(pin, user.PinHash))
                return null;

            return JWTTokenHelper.GenerateToken(user, _configuration);
        }
    }
}
