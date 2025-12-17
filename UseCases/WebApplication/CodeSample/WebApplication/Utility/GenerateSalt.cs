using System.Security.Cryptography;

namespace WebApplications.Utility
{
    public static class GenerateSalt
    {
        public static string CreateSalt(int size=5)
        {
            //Generate a cryptographic random number.
            var rng = RandomNumberGenerator.Create();

            var buff = new byte[size];
            rng.GetBytes(buff);
            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
    }
}
