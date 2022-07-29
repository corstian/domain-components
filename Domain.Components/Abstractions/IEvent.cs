namespace Domain.Components.Abstractions
{
    public interface IEvent : ICommandResult
    {
        public Guid AggregateId { get; }

        IEnumerable<IEvent> ICommandResult.Value => new [] { this };
    }

    public interface IEvent<TAggregate> : IEvent, ICommandResult<TAggregate>
        where TAggregate : IAggregate
    {
        public void Apply(TAggregate state);

        IEnumerable<IEvent> ICommandResult.Value => new[] { this };
        IEnumerable<IEvent<TAggregate>> ICommandResult<TAggregate>.Value => new[] { this };
    }
}
