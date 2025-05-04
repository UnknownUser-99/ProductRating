using System.Text;
using System.Security.Cryptography;
using ProductRating.Contracts.Authorization;
using ProductRating.Data.Configurations;
using Microsoft.Extensions.Options;
using Isopoh.Cryptography.Argon2;

namespace ProductRating.Services.Authorization
{
    public class HashService : IHashService
    {
        private readonly HashServiceOptions _options;

        public HashService(IOptions<HashServiceOptions> options)
        {
            _options = options.Value;
        }

        public string HashPassword(string password)
        {
            byte[] salt = new byte[_options.SaltSize];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            string hash = Argon2.Hash(passwordBytes, salt);

            string hashPassword = $"{Convert.ToBase64String(salt)}:{hash}";

            return hashPassword;
        }

        public bool VerifyPassword(string password, string hashPassword)
        {
            string[] parts = hashPassword.Split(_options.Spliter);

            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);

            string hash = parts[1];

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            bool result = Argon2.Verify(hash, passwordBytes, salt);

            return result;
        }
    }
}