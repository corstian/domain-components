using Domain.Components.Abstractions;

namespace Domain.Components
{
    public class AggregateIdEventReducer : IEventReducer<IEvent>
    {
        public Guid Reduce(IEvent @event) => @event.AggregateId;
    }
}
