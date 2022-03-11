namespace Domain.Components.Abstractions
{
    public interface IEvent
    {
        public Guid AggregateId { get; }
    }

    public interface IEvent<TAggregate> : IEvent
        where TAggregate : IAggregate<TAggregate>
    {
        public void Apply(TAggregate state);
    }
}
