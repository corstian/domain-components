using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class ChangeEmail : ICommand<User, EmailChanged>
    {
        public string Email { get; init; }

        IResult<EmailChanged> ICommand<User, EmailChanged>.Evaluate(User handler)
        {
            if (!Email.Contains("@"))
                return DomainResult.Fail<EmailChanged>("No @");

            return DomainResult.Ok(new EmailChanged
            {
                Email = Email
            });
        }

        //EmailChanged ICommand<User, EmailChanged>.Evaluate(User handler)
        //    => new EmailChanged
        //    {
        //        Email = Email
        //    };

        //Result ICommand<User, EmailChanged>.Validate(User handler)
        //{
        //    if (!Email.Contains("@"))
        //        return Result.Fail("No @");

        //    return Result.Ok();
        //}
    }
}
