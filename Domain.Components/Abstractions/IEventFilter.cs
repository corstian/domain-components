namespace Domain.Components.Abstractions
{
    public interface IEventFilter { }
    public interface IEventFilter<TEvent> : IEventFilter, IEventReducer<TEvent, bool>
        where TEvent : IEvent
    {
        
    }
}
