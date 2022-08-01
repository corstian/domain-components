using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeInfo : ICommand<User, ChangeInfo.Result>
    {
        public string Name { get; init; } = "";
        public string Email { get; init; } = "";

        public IResult<Result> Evaluate(User handler)
        {
            if (!Email.Contains('@')) 
                return DomainResult.Fail<Result>("No @");
            if (handler.Email == Email && handler.Name == Name) 
                return DomainResult.Fail<Result>("Nothing changed");

            return DomainResult.Ok(
                new Result
                {
                    Renamed = new Renamed { Name = Name },
                    EmailChanged = new EmailChanged { Email = Email }
                });
        }

        public class Result : ICommandResult<User>
        {
            public Renamed Renamed { get; init; }
            public EmailChanged EmailChanged { get; init; }

            IEnumerable<IEvent<User>> ICommandResult<User>.Events => new IEvent<User>[] { Renamed, EmailChanged };
        }
    }
}
