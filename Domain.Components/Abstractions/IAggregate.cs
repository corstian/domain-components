namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        //Task<IResult<IEnumerable<IEvent>>> Evaluate(ICommand command);
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : IAggregate<TAggregate>
    {
        //async Task<IResult<IEnumerable<IEvent>>> IAggregate.Evaluate(ICommand command) => await Evaluate((ICommand<TAggregate>)command);

        // Command handlers
        Task<IResult<IEnumerable<IEvent<TAggregate>>>> Evaluate(ICommand<TAggregate> command);

        // Apply without snapshot return
        Task<IEvent<TAggregate>> Apply(IEvent<TAggregate> @event);
        Task<IEnumerable<IEvent<TAggregate>>> Apply(params IEvent<TAggregate>[] events);

        // Apply with snapshot
        Task<TModel> Apply<TModel>(IEvent<TAggregate> @event)
            where TModel : ISnapshot<TAggregate>, new();
        Task<TModel> Apply<TModel>(params IEvent<TAggregate>[] events)
            where TModel : ISnapshot<TAggregate>, new();
    }
}
