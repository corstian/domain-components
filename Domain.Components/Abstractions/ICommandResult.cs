namespace Domain.Components.Abstractions
{
    //public interface ICommandResult
    //{
    //    public IEnumerable<IEvent> Result { get; }
    //}

    public interface ICommandResult<THandler>// : ICommandResult
        where THandler : IAggregate
    {
        public IEnumerable<IEvent<THandler>> Result { get; }

        //IEnumerable<IEvent> ICommandResult.Result => this.Result;
    }

    // Concrete classes

    public class CommandResult<THandler> : ICommandResult<THandler>
        where THandler : IAggregate
    {
        public CommandResult() { }
        public CommandResult(IEnumerable<IEvent<THandler>> result)
        {
            Result = result;
        }

        public IEnumerable<IEvent<THandler>> Result { get; init; }

        public static explicit operator List<IEvent<THandler>>(CommandResult<THandler> value)
            => value.Result.ToList();
    }
}
