namespace Domain.Components.Abstractions
{
    public interface ICommand
    {

    }

    public interface ICommand<THandler> : ICommand
        where THandler : IAggregate
    {
        public IResult<IEnumerable<IEvent<THandler>>> Evaluate(THandler handler);
        public ICommandResult<THandler> EvaluateToResult(THandler handler) => new CommandResult<THandler>(Evaluate(handler));
    }

    public interface ICommand<THandler, out TEvent> : ICommand, ICommand<THandler>
            where THandler : IAggregate
            where TEvent : IEvent<THandler>
    {
        public IResult<TEvent> Evaluate(THandler handler);
        public ICommandResult<THandler, TEvent> EvaluateToResult(THandler handler) => new CommandResult<THandler, TEvent>(Evaluate(handler));

        // Explicit implementations
        IResult<IEnumerable<IEvent<THandler>>> ICommand<THandler>.Evaluate(THandler handler) {
            var result = Evaluate(handler);

            return new DomainResult<IEnumerable<IEvent<THandler>>>()
                .WithValue(result.IsFailed ? default : new IEvent<THandler>[] { result.Value })
                .WithReasons(result.Reasons);
        }
    }

    public interface ICommand<THandler, TEvent1, TEvent2> : ICommand, ICommand<THandler>
        where THandler : IAggregate
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        public IResult<(TEvent1, TEvent2)> Evaluate(THandler handler);
        public ICommandResult<THandler, TEvent1, TEvent2> EvaluateToResult(THandler handler) => new CommandResult<THandler, TEvent1, TEvent2>(Evaluate(handler));

        // Explicit implementations
        IResult<IEnumerable<IEvent<THandler>>> ICommand<THandler>.Evaluate(THandler handler)
        {
            var result = Evaluate(handler);

            return new DomainResult<IEnumerable<IEvent<THandler>>>()
                .WithValue(result.IsFailed ? default : new IEvent<THandler>[] { result.Value.Item1, result.Value.Item2 })
                .WithReasons(result.Reasons);
        }
    }
}
