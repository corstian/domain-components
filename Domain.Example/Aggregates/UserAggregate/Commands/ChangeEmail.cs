using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeEmail : Command, ICommand<User, EmailChanged>
    {
        public string? Email { get; init; }

        IResult<EmailChanged> ICommand<User, EmailChanged>.Evaluate(User handler)
        {
            if (!Email?.Contains("@") ?? true)
                return DomainResult.Fail<EmailChanged>("No @");

            return DomainResult.Ok(new EmailChanged
            {
                Email = Email
            });
        }
    }

    //public class ChangeEmail : Command<User, EmailChanged>, ICommand<User, EmailChanged>
    //{
    //    public string? Email { get; init; }

    //    public override DomainResult<EmailChanged> Evaluate(User handler)
    //    {
    //        if (!Email?.Contains("@") ?? true)
    //            return DomainResult.Fail<EmailChanged>("No @");

    //        return DomainResult.Ok(new EmailChanged
    //        {
    //            Email = Email
    //        });
    //    }
    //}
}
