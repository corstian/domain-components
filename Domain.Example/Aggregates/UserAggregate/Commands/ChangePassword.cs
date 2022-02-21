using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangePassword : ICommand<User, PasswordChanged>
    {
        public string Password { get; init; }

        // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
        private byte[] _getSalt()
        {
            byte[] salt = new byte[128 / 8];

            using (var rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetNonZeroBytes(salt);

            return salt;
        }

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        private byte[] _getHash(byte[] salt, string password)
        {
            return KeyDerivation.Pbkdf2(
                password: Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8);
        }

        IResult<PasswordChanged> ICommand<User, PasswordChanged>.Evaluate(User handler)
        {
            if (handler.PasswordSalt != null
                && handler.PasswordHash.SequenceEqual(_getHash(handler.PasswordSalt, Password)))
            {
                return DomainResult.Fail<PasswordChanged>("Password cannot be the same as a previous password");
            }

            var salt = handler.PasswordHash ?? _getSalt();

            return DomainResult.Ok(new PasswordChanged
            {
                PasswordHash = _getHash(salt, Password),
                PasswordSalt = salt
            });
        }
    }
}
