namespace Domain.Components.Abstractions
{
    public interface IHandle<TEvent>
        where TEvent : IEvent
    {
        Task Process(TEvent @event);
    }
}
