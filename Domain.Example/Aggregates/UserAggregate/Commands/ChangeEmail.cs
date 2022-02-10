using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    internal class ChangeEmail : ICommand<User, EmailChanged>
    {
        public string Email { get; init; }

        Task<EmailChanged> ICommand<User, EmailChanged>.Evaluate(User handler)
        {
            return Task.FromResult(new EmailChanged
            {
                Email = Email
            });
        }
    }
}
