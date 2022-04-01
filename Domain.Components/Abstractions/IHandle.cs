namespace Domain.Components.Abstractions
{
    public interface IHandle<in TEvent, TReducer> : IHandle<TEvent, Guid, TReducer>
        where TEvent : IEvent
        where TReducer : IEventReducer<TEvent, Guid>
    {

    }

    public interface IHandle<in TEvent, TActivationType, TReducer>
        where TEvent : IEvent
        where TActivationType : struct
        where TReducer : IEventReducer<TEvent, TActivationType>
    {
        Task Process(TEvent @event);
    }
}
