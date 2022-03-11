namespace Domain.Components.Abstractions
{
    public interface IHandle<TEvent>
        where TEvent : IEvent
    {
        Task Process(TEvent @event);
    }

    public interface IHandle<TEvent, TFilter> : IHandle<TEvent>
        where TEvent : IEvent
        where TFilter : IEventFilter<TEvent>
    {

    }

    public interface IGetActivatedBy<TEvent, TEventIdentity>
        where TEvent : IEvent
        where TEventIdentity : class
    {

    }
}
