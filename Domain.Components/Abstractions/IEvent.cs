namespace Domain.Components.Abstractions
{
    public interface IEvent
    {
        public Guid AggregateId { get; }
    }

    public interface IEvent<in THandler> : IEvent
        where THandler : IAggregate<THandler>
    {
        public void Apply(THandler state);
    }
}
