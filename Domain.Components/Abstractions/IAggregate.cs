namespace Domain.Components.Abstractions
{
    public interface IAggregate
    {
        
    }

    public interface IAggregate<TAggregate> : IAggregate
        where TAggregate : IAggregate<TAggregate>
    {
        // Command handlers
        public Task<IResult<IEnumerable<IEvent<TAggregate>>>> Evaluate(ICommand<TAggregate> command);

        // Apply without snapshot return
        public Task<IEvent<TAggregate>> Apply(IEvent<TAggregate> @event);
        public Task<IEnumerable<IEvent<TAggregate>>> Apply(params IEvent<TAggregate>[] events);

        // Apply with snapshot
        public Task<TModel> Apply<TModel>(IEvent<TAggregate> @event)
            where TModel : ISnapshot<TAggregate>, new();
        public Task<TModel> Apply<TModel>(params IEvent<TAggregate>[] events)
            where TModel : ISnapshot<TAggregate>, new();
    }
}
