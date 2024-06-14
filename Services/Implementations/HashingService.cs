using ColorPaletteGeneratorApi.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ColorPaletteGeneratorApi.Services.Implementations
{
    public class HashingService : IHashingService
    {
        /// <summary>
        /// Creates a hash for the specified value using the provided key.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public byte[] CreateHash(string value, byte[] key)
        {
            using HMACSHA512 hmac = new(key);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        /// Generates a cryptographic key.
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateKey()
        {
            return RandomNumberGenerator.GetBytes(128);
        }

        /// <summary>
        /// Verifies that the specified value, when hashed with the provided key, matches the provided hash.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="key"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public bool VerifyHash(string value, byte[] key, byte[] hash)
        {
            byte[] hashToVerify = CreateHash(value, key);
            return hashToVerify.SequenceEqual(hash);
        }
    }
}
