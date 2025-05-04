using clsArthritisPatient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AhritisPatientPortalAPI.Helpers
{
    public class JWTTokenHelper
    {
        public static string GenerateToken(Patient patient, IConfiguration configuration)
        {
            try
            {
                var claims = new[]
                {
                new Claim (ClaimTypes.NameIdentifier, patient.Id.ToString() ),
                new Claim(ClaimTypes.Name , patient.FirstName.ToString() + " "+patient.LastName.ToString() ),
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch(Exception ex)
            {
                return "Some Error Occur";  
            }
        }
    }
}
