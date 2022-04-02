using Domain.Components;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Domain.Example.Aggregates.UserAggregate
{
    public class User : Aggregate<User>
    {
        public string Name { get; internal set; } = "";
        public string Email { get; internal set; } = "";

        internal byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        internal byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        internal List<(DateTime, bool)> _loginAttempts = new();
        public IReadOnlyList<(DateTime, bool)> LoginAttempts => _loginAttempts.AsReadOnly();

        internal List<Guid> _groups = new();
        public IReadOnlyList<Guid> Groups => _groups.AsReadOnly();



        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        internal static byte[] GetSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rngCsp = RandomNumberGenerator.Create())
                rngCsp.GetNonZeroBytes(salt);

            return salt;
        }

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        internal static byte[] GetHash(byte[] salt, string password)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
        }
    }
}
