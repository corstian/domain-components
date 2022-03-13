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

        // Apply without snapshot return
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        // Apply with snapshot
        Task<TModel> Apply<TModel>(IEvent<TAggregate> @event)
            where TModel : ISnapshot<TAggregate>, new();
        Task<TModel> Apply<TModel>(params IEvent<TAggregate>[] events)
            where TModel : ISnapshot<TAggregate>, new();
    }
}
