using Domain.Components;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeInfo : Command<User, Renamed, EmailChanged>
    {
        public string? Name { get; init; }
        public string? Email { get; init; }

        public override DomainResult<(Renamed, EmailChanged)> Evaluate(User handler)
        {
            if (!Email?.Contains("@") ?? true) return DomainResult.Fail("No @");

            return DomainResult.Ok((
                new Renamed { Name = Name },
                new EmailChanged {  Email = Email }));
        }
    }
}
