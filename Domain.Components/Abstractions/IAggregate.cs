namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : IAggregate
    {
        // Command handlers
        Task<IResult<IEnumerable<IEvent<TAggregate>>>> Evaluate(ICommand<TAggregate> command);

        // Apply without snapshot return
        Task Apply(IEvent<TAggregate> @event);
        Task Apply(params IEvent<TAggregate>[] events);

        Task<TModel> GetSnapshot<TModel>()
            where TModel : ISnapshot<TAggregate>, new();
    }
}
