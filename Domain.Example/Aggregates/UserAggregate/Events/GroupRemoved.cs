using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class GroupRemoved : Event<User>, IEvent<User>
    {
        public Guid GroupId { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state._groups.Remove(GroupId);
        }
    }
}
