using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangePassword : ICommand<User, PasswordChanged>
    {
        public string Password { get; init; }

        IResult<PasswordChanged> ICommand<User, PasswordChanged>.Evaluate(User handler)
        {
            if (handler.PasswordSalt != null
                && handler.PasswordHash.SequenceEqual(handler._getHash(handler.PasswordSalt, Password)))
            {
                return DomainResult.Fail<PasswordChanged>("Password cannot be the same as a previous password");
            }

            var salt = handler.PasswordHash ?? handler._getSalt();

            return DomainResult.Ok(new PasswordChanged
            {
                PasswordHash = handler._getHash(salt, Password),
                PasswordSalt = salt
            });
        }
    }
}
