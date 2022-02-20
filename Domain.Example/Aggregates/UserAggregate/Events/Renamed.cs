using Domain.Components;

namespace Domain.Example.Aggregates.UserAggregate.Events
{
    public class Renamed : Event<User>
    {
        internal Renamed() { }

        public string Name { get; init; }

        public override void Apply(User state)
        {
            state.Name = Name;
        }
    }
}
