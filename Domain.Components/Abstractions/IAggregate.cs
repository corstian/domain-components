namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        ValueTask<string> GetIdentity();
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : class, IAggregate<TAggregate>
    {
        // Command handlers
        Task<IResult<ICommandResult<TAggregate>>> Evaluate(ICommand<TAggregate> command);        
        Task<IEnumerable<IResult<ICommandResult<TAggregate>>>> Evaluate(params ICommand<TAggregate>[] commands);
        

        // Event Application
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        Task Apply(ICommandResult<TAggregate> commandResult)
            => Apply(commandResult.Events.ToArray());

        // Snapshotting
        /*
         * Once again, since Orleans does not like "nested generics", we're omitting them here.
         * Otherwise the constraint on `TModel` is supposed to be `ISnapshot<TModel>`.
         */
        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot, new();
    }
}
