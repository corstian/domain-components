using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class Renamed : IEvent<User>
    {
        internal Renamed() { }

        private Guid _aggregateId;
        Guid IEvent.AggregateId => _aggregateId;

        public string Name { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state.Name = Name;
        }
    }
}
