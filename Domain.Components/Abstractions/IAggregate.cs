namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        ValueTask<string> GetIdentity();

        Task<IResult<ICommandResult>> Evaluate(ICommand command);
        Task<IEnumerable<IResult<ICommandResult>>> Evaluate(params ICommand[] commands);
        Task Apply(params IEvent[] events);
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : class, IAggregate<TAggregate>
    {
        // IAggregate default implementations
        async Task<IResult<ICommandResult>> IAggregate.Evaluate(ICommand command)
        {
            // ToDo: Use reflection to retrieve this value
            var result = await Evaluate<ICommandResult<TAggregate>>(command);
            return new DomainResult<ICommandResult>()
                .WithValue(result.IsSuccess
                    ? result.Value
                    : null)
                .WithReasons(result.Reasons);
        }

        async Task<IEnumerable<IResult<ICommandResult>>> IAggregate.Evaluate(params ICommand[] commands)
        {
            var commandResults = new List<IResult<ICommandResult>>();

            foreach (var command in commands)
                commandResults.Add(await Evaluate(command));

            return commandResults.AsEnumerable();
        }

        async Task IAggregate.Apply(params IEvent[] events)
        {
            foreach (var @event in events)
                await Apply(@event as IEvent<TAggregate>);
        }

        // Command handlers
        Task<IResult<TResult>> Evaluate<TResult>(ICommand<TAggregate, TResult> command)
            where TResult : ICommandResult<TAggregate>;

        // Event Application
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        Task Apply(ICommandResult<TAggregate> commandResult)
            => Apply(commandResult.Events.ToArray());

        // Snapshotting
        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new();
    }
}
