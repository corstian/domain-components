using Domain.Components;
using Domain.Components.Abstractions;
using Domain.Example.Aggregates.UserAggregate.Events;

namespace Domain.Example.Aggregates.UserAggregate.Commands
{
    public class RemoveGroup : ICommand<User, GroupRemoved>
    {
        public Guid GroupId { get; init; }

        IResult<GroupRemoved> ICommand<User, GroupRemoved>.Evaluate(User handler)
        {
            return DomainResult.Ok(new GroupRemoved
            {
                GroupId = GroupId
            });
        }
    }
}
