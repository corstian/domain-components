namespace Domain.Components.Abstractions
{
    public interface IEvent : IMarkCommandOutput
    {
        public Guid AggregateId { get; }
    }

    public interface IEvent<TAggregate> : IEvent, IMarkCommandOutput<TAggregate>
        where TAggregate : IAggregate
    {
        public void Apply(TAggregate state);
    }
}
