using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangePassword : ICommand<User, PasswordChanged>
    {
        public string Password { get; init; } = "";

        IResult<PasswordChanged> ICommand<User, PasswordChanged>.Evaluate(User handler)
        {
            if (handler.PasswordSalt != null
                && handler.PasswordHash.SequenceEqual(User.GetHash(handler.PasswordSalt, Password)))
            {
                return DomainResult.Fail<PasswordChanged>("Password cannot be the same as a previous password");
            }

            var salt = handler.PasswordHash ?? User.GetSalt();

            return DomainResult.Ok(new PasswordChanged
            {
                PasswordHash = User.GetHash(salt, Password),
                PasswordSalt = salt
            });
        }
    }
}
