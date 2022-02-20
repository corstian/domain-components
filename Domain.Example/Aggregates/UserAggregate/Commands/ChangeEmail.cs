using Domain.Components;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeEmail : Command<User, EmailChanged>
    {
        public string Email { get; init; }

        public override DomainResult<EmailChanged> Evaluate(User handler)
        {
            if (!Email.Contains("@"))
                return DomainResult.Fail<EmailChanged>("No @");
            
            return DomainResult.Ok(new EmailChanged
            {
                Email = Email
            });
        }
    }
}
