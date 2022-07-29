namespace Domain.Components.Abstractions
{
    public interface ICommandResult
    {
        public IEnumerable<IEvent> Value { get; }
    }

    public interface ICommandResult<THandler> : ICommandResult
        where THandler : IAggregate
    {
        public new IEnumerable<IEvent<THandler>> Value { get; }

        IEnumerable<IEvent> ICommandResult.Value => Value;
    }

    public class CommandResult<THandler> : ICommandResult<THandler>
        where THandler : IAggregate
    {
        public CommandResult() { }
        public CommandResult(IEnumerable<IEvent<THandler>> value)
        {
            Value = value;
        }

        public IEnumerable<IEvent<THandler>> Value { get; init; }
    }
}
