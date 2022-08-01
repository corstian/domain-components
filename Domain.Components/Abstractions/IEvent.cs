namespace Domain.Components.Abstractions
{
    public interface IEvent : ICommandResult
    {
        public Guid AggregateId { get; }
        public void Apply(IAggregate state);

        IEnumerable<IEvent> ICommandResult.Events => new [] { this };
    }

    public interface IEvent<TAggregate> : IEvent, ICommandResult<TAggregate>
        where TAggregate : class, IAggregate
    {
        public void Apply(TAggregate state);
        void IEvent.Apply(IAggregate state) => Apply(state as TAggregate);

        IEnumerable<IEvent> ICommandResult.Events => new[] { this };
        IEnumerable<IEvent<TAggregate>> ICommandResult<TAggregate>.Events => new[] { this };
    }
}
