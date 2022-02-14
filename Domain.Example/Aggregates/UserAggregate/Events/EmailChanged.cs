using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class EmailChanged : IEvent<User>
    {
        internal EmailChanged() { }

        public string Email { get; init; }

        public Guid AggregateId { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state.Email = Email;
        }
    }
}
