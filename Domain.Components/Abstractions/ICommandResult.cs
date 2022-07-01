namespace Domain.Components.Abstractions
{
    public interface ICommandResult { }

    public interface ICommandResult<THandler> : ICommandResult
        where THandler : IAggregate
    {
        public IResult<IEnumerable<IEvent<THandler>>> Result { get; init; }
    }

    public interface ICommandResult<THandler, TEvent> : ICommandResult
        where THandler : IAggregate
        where TEvent : IEvent<THandler>
    {
        public IResult<TEvent> Result { get; init; }
    }

    public interface ICommandResult<THandler, TEvent1, TEvent2> : ICommandResult
        where THandler : IAggregate
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        public IResult<(TEvent1, TEvent2)> Result { get; init; }
    }

    // Concrete classes

    public class CommandResult<THandler> : ICommandResult<THandler>
        where THandler : IAggregate
    {
        public CommandResult() { }
        public CommandResult(IResult<IEnumerable<IEvent<THandler>>> result)
        {
            Result = result;
        }

        public IResult<IEnumerable<IEvent<THandler>>> Result { get; init; }
    }

    public class CommandResult<THandler, TEvent> : ICommandResult<THandler, TEvent>
        where THandler : IAggregate
        where TEvent : IEvent<THandler>
    {
        public CommandResult() { }
        public CommandResult(IResult<TEvent> result)
        {
            Result = result;
        }

        public IResult<TEvent> Result { get; init; }
    }

    public class CommandResult<THandler, TEvent1, TEvent2> : ICommandResult<THandler, TEvent1, TEvent2>
        where THandler : IAggregate
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        public CommandResult() { }
        public CommandResult(IResult<(TEvent1, TEvent2)> result)
        {
            Result = result;
        }

        public IResult<(TEvent1, TEvent2)> Result { get; init; }
    }
}
