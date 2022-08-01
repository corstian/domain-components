namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        Task<string> GetIdentity();

        Task<IResult<ICommandResult>> Evaluate(ICommand command)
             => Task.FromResult(command.Evaluate(this));

        Task<IEnumerable<IResult<ICommandResult>>> Evaluate(params ICommand[] commands)
        {
            var commandResults = new List<IResult<ICommandResult>>();

            foreach (var command in commands)
                commandResults.Add(command.Evaluate(this));

            return Task.FromResult(
                commandResults.AsEnumerable());
        }

        Task Apply(params IEvent[] events)
        {
            foreach (var @event in events)
                @event.Apply(this);

            return Task.CompletedTask;
        }
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : class, IAggregate<TAggregate>
    {
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
