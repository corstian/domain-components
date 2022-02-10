namespace Domain.Components.Abstractions
{
    public interface IEvent
    {
        Guid AggregateId { get; }
    }

    public interface IEvent<THandler> : IEvent
        where THandler : IEventHandler<THandler>
    {
        public void Apply(THandler state);
    }
}
