namespace Domain.Components.Abstractions
{
    public interface IEventReducer<in TEvent, TResult>
        where TEvent : IEvent
        where TResult : struct
    {
        TResult Reduce(TEvent @event);
    }
}
