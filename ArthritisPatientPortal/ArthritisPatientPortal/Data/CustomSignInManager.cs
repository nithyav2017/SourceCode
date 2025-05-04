using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;

namespace ArthritisPatientPortal.Data
{
    public class CustomSignInManager : SignInManager<IdentityUser>
    {
        public CustomSignInManager(UserManager<IdentityUser> userManager, 
                                    IHttpContextAccessor contextAccessor,
                                    IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
                                    IOptions<IdentityOptions> optionsAccessor,
                                    ILogger<SignInManager<IdentityUser>> logger, 
                                    IAuthenticationSchemeProvider schemes,
                                    IUserConfirmation<IdentityUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {

        }

        public async Task<SignInResult> CheckPINAsync(string email, string pin)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user == null || !VerifyPIN(pin, user.pin))
                return SignInResult.Failed;

            return SignInResult.Success;
        }

        private bool VerifyPIN(string inputPin, string storedHash)
        {
            return HashPIN(inputPin) == storedHash;
        }

        private string HashPIN(string pin)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(pin);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
