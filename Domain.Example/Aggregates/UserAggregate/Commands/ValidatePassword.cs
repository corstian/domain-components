using Domain.Components;
using Domain.Example.Aggregates.UserAggregate.Events;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ValidatePassword : Command<User, PasswordValidationCompleted>
    {
        public string Password { get; init; }

        public override DomainResult<PasswordValidationCompleted> Evaluate(User handler)
        {
            var hash = KeyDerivation.Pbkdf2(
                password: Password,
                salt: handler.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8);

            var result = hash.SequenceEqual(handler.PasswordHash);

            return DomainResult.Ok(
                new PasswordValidationCompleted
                {
                    Succeeded = result
                });
        }
    }
}
