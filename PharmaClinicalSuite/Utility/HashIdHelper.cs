using System.Security.Cryptography;
using HashidsNet;

namespace PharmaClinicalSuite.Utility
{
    public static class HashIdHelper
    {
        static string  saltKey = GenerateSalt();
        public static readonly Hashids _hashIds = new Hashids(saltKey,5);

        public static string EncodeId(int id) => _hashIds.Encode(id);

        public static int? DecodeId(string hash)
        {
            var numbers = _hashIds.Decode(hash);
            return numbers.Length > 0 ? numbers[0] : (int?)null;
        }

        public static string GenerateSalt(int byteLength = 16)
        {
            byte[] saltBytes= new byte[byteLength];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

    }

   
     
}
