using Domain.Components.Abstractions;

namespace Domain.Components
{
    public abstract class Event : IEvent
    {
        public Guid AggregateId { get; internal set; }
        public IAuthorizationContext? AuthorizationContext { get; internal set; }
    }
}
