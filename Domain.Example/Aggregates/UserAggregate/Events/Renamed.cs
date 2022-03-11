using Domain.Components;
using Domain.Components.Abstractions;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class Renamed : Event, IEvent<User>
    {
        internal Renamed() { }

        public string Name { get; init; }

        void IEvent<User>.Apply(User state)
        {
            state.Name = Name;
        }

        public class JohnDoeFilter : IEventFilter<Renamed>
        {
            public bool Reduce(Renamed @event) => @event.Name == "JohnDoe";
        }
    }
}
