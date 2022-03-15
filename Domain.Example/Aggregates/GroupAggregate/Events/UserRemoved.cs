using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.GroupAggregate.Events
{
    internal class UserRemoved : Event, IEvent<Group>
    {
        public Guid UserId { get; init; }

        void IEvent<Group>.Apply(Group state)
        {
            state._members.Remove(UserId);
        }
    }
}
