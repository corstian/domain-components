using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Event : IEvent
    {
        public Guid AggregateId { get; internal set; }
        public IAuthorizationContext? AuthorizationContext { get; internal set; }
    }

    public abstract class Event<THandler> : Event, IEvent<THandler>
        where THandler : IAggregate<THandler>
    {
        public abstract void Apply(THandler state);
    }
}
