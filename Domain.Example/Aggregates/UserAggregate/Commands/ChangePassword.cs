using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using FluentResults;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangePassword : ICommand<User, PasswordChanged>
    {
        public string Password { get; init; }

        async Task<Result<PasswordChanged>> ICommand<User, PasswordChanged>.Evaluate(User handler)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];

            using (var rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetNonZeroBytes(salt);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = KeyDerivation.Pbkdf2(
                password: Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);

            return Result.Ok(
                new PasswordChanged
                {
                    PasswordHash = hashed,
                    PasswordSalt = salt
                });
        }
    }
}
