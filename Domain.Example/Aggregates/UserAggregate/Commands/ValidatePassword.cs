using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ValidatePassword : ICommand<User, PasswordValidationCompleted>
    {
        public string Password { get; init; } = "";

        IResult<PasswordValidationCompleted> ICommand<User, PasswordValidationCompleted>.Evaluate(User handler)
        {
            var hash = User.GetHash(handler.PasswordSalt, Password);

            var result = hash.SequenceEqual(handler.PasswordHash);

            return DomainResult.Ok(
                new PasswordValidationCompleted
                {
                    Succeeded = result
                });
        }
    }
}
