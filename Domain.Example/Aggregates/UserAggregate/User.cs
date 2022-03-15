using Domain.Components;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Domain.Example.Aggregates.UserAggregate
{
    public class User : Aggregate<User>
    {
        public string Name { get; internal set; }
        public string Email { get; internal set; }

        internal byte[] PasswordSalt { get; set; }
        internal byte[] PasswordHash { get; set; }

        internal List<(DateTime, bool)> _loginAttempts = new();
        public IReadOnlyList<(DateTime, bool)> LoginAttempts => _loginAttempts.AsReadOnly();

        internal List<Guid> _groups = new();
        public IReadOnlyList<Guid> Groups => _groups.AsReadOnly();



        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        internal byte[] _getSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetNonZeroBytes(salt);

            return salt;
        }

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        internal byte[] _getHash(byte[] salt, string password)
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
