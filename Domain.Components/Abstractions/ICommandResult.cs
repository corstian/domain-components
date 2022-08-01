namespace Domain.Components.Abstractions
{
    public interface ICommandResult
    {
        public IEnumerable<IEvent> Events { get; }
    }

    public interface ICommandResult<TAggregate> : ICommandResult
        where TAggregate : class, IAggregate
    {
        public new IEnumerable<IEvent<TAggregate>> Events { get; }

        IEnumerable<IEvent> ICommandResult.Events => Events;
    }
}
