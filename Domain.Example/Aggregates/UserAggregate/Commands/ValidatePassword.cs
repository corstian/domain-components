using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ValidatePassword : ICommand<User, PasswordValidationCompleted>
    {
        public string Password { get; init; }

        IResult<PasswordValidationCompleted> ICommand<User, PasswordValidationCompleted>.Evaluate(User handler)
        {
            var hash = KeyDerivation.Pbkdf2(
                password: Password,
                salt: handler.PasswordSalt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100_000,
                numBytesRequested: 256 / 8);

            var result = hash == handler.PasswordHash;

            return DomainResult.Ok(
                new PasswordValidationCompleted
                {
                    Succeeded = result
                });
        }

        //async Task<Result<PasswordValidationCompleted>> ICommand<User, PasswordValidationCompleted>.Evaluate(User handler)
        //{


        //    var result = hash == handler.PasswordHash;

        //    return Result.Ok(
        //        new PasswordValidationCompleted
        //        {
        //            Succeeded = result
        //        });
        //}

        //PasswordValidationCompleted ICommand<User, PasswordValidationCompleted>.Evaluate(User handler)
        //{

        //}

        //Result ICommand<User, PasswordValidationCompleted>.Validate(User handler)
        //{
        //    var hash = KeyDerivation.Pbkdf2(
        //        password: Password,
        //        salt: handler.PasswordSalt,
        //        prf: KeyDerivationPrf.HMACSHA256,
        //        iterationCount: 100_000,
        //        numBytesRequested: 256 / 8);

        //    if (hash == handler.PasswordHash) return Result.Ok();

        //    return Result.Fail("Incorrect password");
        //}
    }
}
