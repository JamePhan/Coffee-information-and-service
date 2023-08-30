using System.Security.Cryptography;
using System.Text;

namespace Back.Utilities
{
    public class PasswordUtils
    {
        private const int KEY_SIZE = 64;
        private const int ITERATIONS = 30122000;
        private readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

        public string HashPassword(string password, out string salt)
        {
            byte[] saltByte = RandomNumberGenerator.GetBytes(KEY_SIZE);

            salt = Convert.ToBase64String(saltByte);

            byte[]? hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                saltByte,
                ITERATIONS,
                _algorithm,
                KEY_SIZE
                );

            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hash, string salt)
        {
            byte[]? hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                ITERATIONS,
                _algorithm,
                KEY_SIZE
                );

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromBase64String(hash));
        }
    }
}