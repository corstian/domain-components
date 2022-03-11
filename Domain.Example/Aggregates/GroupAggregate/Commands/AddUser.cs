using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.GroupAggregate.Events;

namespace Domain.Example.Aggregates.GroupAggregate.Commands
{
    public class AddUser : ICommand<Group, UserAdded>
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }

        IResult<UserAdded> ICommand<Group, UserAdded>.Evaluate(Group handler)
        {
            return DomainResult.Ok(new UserAdded
            {
                UserId = UserId,
                Name = Name
            });
        }
    }
}
