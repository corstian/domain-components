using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class GroupAdded : Event<User>, IEvent<User>
    {
        public Guid GroupId { get; init; }
        public string Name { get; init; } = "";

        void IEvent<User>.Apply(User state)
        {
            state._groups.Add(GroupId);
        }
    }
}
