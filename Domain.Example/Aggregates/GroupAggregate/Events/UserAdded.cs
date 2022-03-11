using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.GroupAggregate.Events
{
    internal class UserAdded : Event, IEvent<Group>
    {
        public Guid UserId { get; init; }
        public string Name { get; init; }

        void IEvent<Group>.Apply(Group state)
        {
            state._members.Add(UserId);
        }
    }
}
