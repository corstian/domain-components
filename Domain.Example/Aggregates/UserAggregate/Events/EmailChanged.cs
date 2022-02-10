using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class EmailChanged : IEvent<User>
    {
        internal EmailChanged() { }

        Guid IEvent.AggregateId => throw new NotImplementedException();

        public string Email { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state.Email = Email;
        }
    }
}
