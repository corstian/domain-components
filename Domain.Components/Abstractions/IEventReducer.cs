namespace Domain.Components.Abstractions
{
    public interface IEventReducer<in TEvent> : IEventReducer<TEvent, Guid>
        where TEvent : IEvent
    {

    }

    public interface IEventReducer<in TEvent, TActivationType>
        where TEvent : IEvent
        where TActivationType : struct
    {
        TActivationType Reduce(TEvent @event);
    }
}
