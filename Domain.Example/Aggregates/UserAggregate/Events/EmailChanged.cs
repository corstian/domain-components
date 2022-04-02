using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class EmailChanged : Event, IEvent<User>
    {
        internal EmailChanged() { }

        public string Email { get; init; } = "";

        void IEvent<User>.Apply(User state)
        {
            state.Email = Email;
        }
    }
}
