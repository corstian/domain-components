using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class AddGroup : ICommand<User, GroupAdded>
    {
        public Guid GroupId { get; init; }
        public string Name { get; init; } = "";

        IResult<GroupAdded> ICommand<User, GroupAdded>.Evaluate(User handler)
        {
            return DomainResult.Ok(new GroupAdded
            {
                GroupId = GroupId,
                Name = Name
            });
        }
    }
}
