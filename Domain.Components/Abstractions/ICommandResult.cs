namespace Domain.Components.Abstractions
{
    public interface ICommandResult : IMarkCommandOutput
    {
        public IEnumerable<IEvent> Result { get; }
    }

    public interface ICommandResult<THandler> : ICommandResult, IMarkCommandOutput<THandler>
        where THandler : IAggregate
    {
        public new IEnumerable<IEvent<THandler>> Result { get; }

        IEnumerable<IEvent> ICommandResult.Result => Result;
    }

    public class CommandResult<THandler> : ICommandResult<THandler>
        where THandler : IAggregate
    {
        public CommandResult() { }
        public CommandResult(IEnumerable<IEvent<THandler>> result)
        {
            Result = result;
        }

        public IEnumerable<IEvent<THandler>> Result { get; init; }
    }
}
