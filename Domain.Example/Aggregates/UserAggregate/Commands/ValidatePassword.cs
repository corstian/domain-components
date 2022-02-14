using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using FluentResults;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ValidatePassword : ICommand<User, PasswordValidationCompleted>
    {
        public string Password { get; init; }

        async Task<Result<PasswordValidationCompleted>> ICommand<User, PasswordValidationCompleted>.Evaluate(User handler)
        {
            var hash = KeyDerivation.Pbkdf2(
                password: Password,
                salt: handler.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8);

            var result = hash == handler.PasswordHash;

            return Result.Ok(
                new PasswordValidationCompleted
                {
                    Succeeded = result
                });
        }
    }
}
