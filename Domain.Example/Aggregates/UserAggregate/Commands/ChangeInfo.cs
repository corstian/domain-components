using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeInfo : ICommand<User, Renamed, EmailChanged>
    {
        public string Name { get; init; } = "";
        public string Email { get; init; } = "";

        IResult<(Renamed, EmailChanged)> ICommand<User, Renamed, EmailChanged>.Evaluate(User handler)
        {
            if (!Email.Contains('@')) return DomainResult.Fail<(Renamed, EmailChanged)>("No @");

            if (handler.Email == Email && handler.Name == Name) return DomainResult.Fail<(Renamed, EmailChanged)>("Nothing changed");

            return DomainResult.Ok((
                new Renamed { Name = Name },
                new EmailChanged { Email = Email }));
        }
    }
}
