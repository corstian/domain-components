namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {

    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : IAggregate<TAggregate>
    {
        // Command handlers
        Task<IResult<IEnumerable<IEvent<TAggregate>>>> Evaluate(ICommand<TAggregate> command);
        Task<IResult<TResult>> Evaluate<TResult>(ICommand<TAggregate, TResult> command)
            where TResult : IMarkCommandOutput<TAggregate>;

        // Event Application
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);
        Task Apply(ICommandResult<TAggregate> commandResult);

        // Snapshotting
        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new();
    }
}
