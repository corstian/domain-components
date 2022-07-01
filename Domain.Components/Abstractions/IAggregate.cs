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

        // Event Application
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        // Snapshotting
        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new();
    }
}
