using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate.Events;

namespace Domain.Example.Aggregates.GroupAggregate.Commands
{
    public class RemoveUser : ICommand<Group, UserRemoved>
    {
        public Guid UserId { get; init; }

        IResult<UserRemoved> ICommand<Group, UserRemoved>.Evaluate(Group handler)
        {
            return DomainResult.Ok(new UserRemoved
            {
                UserId = UserId
            });
        }
    }
}
