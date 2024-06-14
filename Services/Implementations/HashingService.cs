using ColorPaletteGeneratorApi.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class HashingService : IHashingService
    {
        public byte[] CreateHash(string value, byte[] key)
        {
            using HMACSHA512 hmac = new(key);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        public byte[] GenerateKey()
        {
            return RandomNumberGenerator.GetBytes(128);
        }

        public bool VerifyHash(string value, byte[] key, byte[] hash)
        {
            byte[] hashToVerify = CreateHash(value, key);
            return hashToVerify.SequenceEqual(hash);
        }
    }
}
