using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;
using FluentResults;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    internal class ChangeEmail : ICommand<User, EmailChanged>
    {
        public string Email { get; init; }

        async Task<Result<EmailChanged>> ICommand<User, EmailChanged>.Evaluate(User handler)
        {
            if (!Email.Contains("@"))
                return Result.Fail("No @");

            return Result.Ok(
                new EmailChanged
                {
                    Email = Email
                });
        }
    }
}
